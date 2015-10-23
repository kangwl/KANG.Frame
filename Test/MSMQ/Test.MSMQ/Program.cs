using System;
using System.Collections.Generic;
using System.Linq;
using System.Messaging;
using System.Text;
using System.Threading.Tasks;

namespace Test.MSMQ {
    class Program {

        private static string _msmqPath = @".\Private$\kangMSMQ";
        public static string _msmqTransPath = @".\Private$\kangTransMSMQ";
        static void Main(string[] args) {
            //SendMessage();
            RecieveMessage();
            Console.Read();
        }

        private static void SendMessage() {

            //MessageQueue messageQueue = InitMSMQ(_msmqPath);

            //Message message = new Message();
            //message.Body = "kangwl";
            //message.Priority = MessagePriority.High;
            //messageQueue.Send(message);
            MSMQHelper.MessageModel<string> messageModel=new MSMQHelper.MessageModel<string>();
            messageModel.Data = "kwl123";
            messageModel.Label = "message1";
            MSMQHelper msmqHelper = new MSMQHelper(_msmqPath);
            msmqHelper.SendMessages(new List<MSMQHelper.MessageModel<string>>() {messageModel, messageModel, messageModel, messageModel, messageModel });
            Console.WriteLine("send success");
        }

        private static void RecieveMessage() {
            //MessageQueue messageQueue = InitMSMQ(_msmqPath);

            //messageQueue.MessageReadPropertyFilter.Priority = true;
            //Message message = messageQueue.Receive();
            //message.Formatter = new XmlMessageFormatter(new Type[] { typeof(string) });
            //Console.WriteLine(message.Body);

            MSMQHelper msmqHelper = new MSMQHelper(_msmqPath);
            //IEnumerable<MSMQHelper.MessageModel<string>> data = msmqHelper.RecieveMuti<string>();
            //foreach (MSMQHelper.MessageModel<string> model in data) {
            //    Console.WriteLine(model.Data);
            //}
            msmqHelper.RecieveAsync<string>(one => { Console.WriteLine(one.Data); });
        }

        private static MessageQueue InitMSMQ(string msmqPath) {
            //  queue = new MessageQueue(@"Formatname:DIRECT=tcp:192.168.1.200\private$\OrgMngUserOprtLog");
            MessageQueue messageQueue = null;
            messageQueue = !MessageQueue.Exists(msmqPath) ? MessageQueue.Create(msmqPath) : new MessageQueue(msmqPath);
            return messageQueue;
        }

    }

    public class MSMQHelper {

        public class MessageModel<TModel> {
           
            /// <summary>
            /// 消息内容 body
            /// </summary>
            public TModel Data { get; set; }
            /// <summary>
            /// 消息标签
            /// </summary>
            public string Label { get; set; }

 
        }

        /// <summary>
        /// 初始化
        /// </summary>
        /// <param name="msmqPath">
        /// queue的path
        /// EXP:@".\Private$\kangMSMQ"
        /// @"Formatname:DIRECT=tcp:192.168.1.200\private$\OrgMngUserOprtLog"
        /// </param>
        /// <param name="isTranscationQueue">是否创建事务性队列</param>
        public MSMQHelper(string msmqPath, bool isTranscationQueue = false) {
            MSMQPath = msmqPath;
            IsTranscationQueue = isTranscationQueue;
        }

        private string MSMQPath { get; set; }
        private bool IsTranscationQueue { get; set; }

        /// <summary>
        /// 初始化 MessageQueue
        /// </summary>
        /// <returns></returns>
        private MessageQueue GetMQ() {
            //  queue = new MessageQueue(@"Formatname:DIRECT=tcp:192.168.1.200\private$\OrgMngUserOprtLog");
            MessageQueue messageQueue = null;
            messageQueue = !MessageQueue.Exists(MSMQPath) ? MessageQueue.Create(MSMQPath,IsTranscationQueue) : new MessageQueue(MSMQPath);
            return messageQueue;
        }

        /// <summary>
        /// 发送一个消息
        /// </summary>
        /// <typeparam name="TModel"></typeparam>
        /// <param name="messageModel"></param>
        /// <returns></returns>
        public bool SendMessage<TModel>(MessageModel<TModel> messageModel) {
            using (MessageQueue messageQueue = GetMQ()) {
                Send(messageQueue, messageModel);
                return true;
            }
        }

        /// <summary>
        /// 批量发送消息
        /// </summary>
        /// <typeparam name="TModel"></typeparam>
        /// <param name="messageModels"></param>
        public void SendMessages<TModel>(List<MessageModel<TModel>> messageModels) {
            using (MessageQueue messageQueue = GetMQ()) {
                messageModels.ForEach(one => Send(messageQueue, one));
            }
        }
        /// <summary>
        /// 使用事务队列 发送多条消息
        /// 注意：MessageQueue 必须声明为事务性的才有效
        /// </summary>
        /// <typeparam name="TModel"></typeparam>
        /// <param name="messageModels"></param>
        public void SendMessageTrans<TModel>(List<MessageModel<TModel>> messageModels) {
            using (MessageQueue messageQueue = GetMQ()) {
                if (!messageQueue.Transactional) {
                    throw new Exception("该消息队列不支持事务,请重新定义");
                }
                using (MessageQueueTransaction transaction = new MessageQueueTransaction()) {
                    try {
                        transaction.Begin();
                        messageModels.ForEach(one => {
                            Message message = Convert2Message(one);
                            messageQueue.Send(message, transaction);
                        });
                        transaction.Commit();
                    }
                    catch (Exception) {
                        transaction.Abort();
                        throw;
                    }
                }
            }
        }
        /// <summary>
        /// 接收一个消息
        /// </summary>
        /// <typeparam name="TModel"></typeparam>
        /// <returns></returns>
        public MessageModel<TModel> Recieve<TModel>() {
            using (MessageQueue messageQueue = GetMQ()) {
                messageQueue.MessageReadPropertyFilter.Priority = true;
                Message message = messageQueue.Receive();
                if (message == null) return null;
                message.Formatter = new XmlMessageFormatter(new Type[] {typeof (TModel)});

                return Convert2MessageModel<TModel>(message);
            }
        }

        /// <summary>
        ///  批量获取消息，不会删除消息
        /// </summary>
        /// <typeparam name="TModel"></typeparam>
        /// <returns></returns>
        public IEnumerable<MessageModel<TModel>> RecieveMuti<TModel>() {
            using (MessageQueue messageQueue = GetMQ()) {
                messageQueue.MessageReadPropertyFilter.Priority = true;

                MessageEnumerator enumerator = messageQueue.GetMessageEnumerator2();

                while (enumerator.MoveNext()) {

                    yield return Convert2MessageModel<TModel>(enumerator.Current);
                }
            }
        }

        /// <summary>
        /// 批量获取消息，然后删除
        /// </summary>
        /// <typeparam name="TModel"></typeparam>
        /// <returns></returns>
        public IEnumerable<MessageModel<TModel>> RecieveMutiThenDelete<TModel>() {
            using (MessageQueue messageQueue = GetMQ()) {
                messageQueue.MessageReadPropertyFilter.Priority = true;

                MessageEnumerator enumerator = messageQueue.GetMessageEnumerator2();

                while (enumerator.MoveNext()) {

                    Message message = enumerator.RemoveCurrent();
                    enumerator = messageQueue.GetMessageEnumerator2();
                    yield return Convert2MessageModel<TModel>(message);
                }
            }
        }

        //common send
        private void Send<TModel>(MessageQueue messageQueue, MessageModel<TModel> messageModel) {
            messageQueue.Send(Convert2Message(messageModel));
        }

        private Message Convert2Message<TModel>(MessageModel<TModel> messageModel) {
            Message message = new Message();
            message.Label = messageModel.Label;
            message.Body = messageModel.Data;
 
            return message;
        }

        private MessageModel<TModel> Convert2MessageModel<TModel>(Message message) {
            message.Formatter = new XmlMessageFormatter(new[] {typeof (TModel)});
            MessageModel<TModel> messageModel = new MessageModel<TModel>();
            messageModel.Data = (TModel) message.Body;
            messageModel.Label = message.Label; 
            return messageModel;
        }

        public void RecieveAsync<TModel>(Action<MessageModel<TModel>> func) {
            using (MessageQueue messageQueue = GetMQ()) {

                messageQueue.ReceiveCompleted += (sender, e) => {
                    Message message = e.Message;
                    func(Convert2MessageModel<TModel>(message));
                };
                IAsyncResult result = messageQueue.BeginReceive();
                messageQueue.EndReceive(result);
            }
        }
 


    }

     
}
