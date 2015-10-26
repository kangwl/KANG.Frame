using System;
using System.Collections.Generic;
using System.Messaging;
using System.Threading;
using System.Threading.Tasks;

public class MSMQHelper : IDisposable {

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
    /// 初始化队列，如不存在则创建
    /// </summary>
    /// <param name="msmqPath">
    /// queue的path
    /// EXP:@".\Private$\kangMSMQ"
    /// @"Formatname:DIRECT=tcp:192.168.1.200\private$\OrgMngUserOprtLog"
    /// </param>
    /// <param name="isTranscationQueue">
    /// 是否创建事务性队列
    /// 只有队列不存在 创建时才有用
    /// </param>
    public MSMQHelper(string msmqPath, bool isTranscationQueue = false) {
        MSMQPath = msmqPath;
        IsTranscationQueue = isTranscationQueue;

        MQ = !MessageQueue.Exists(MSMQPath)
            ? MessageQueue.Create(MSMQPath, IsTranscationQueue)
            : new MessageQueue(MSMQPath);
        IsTransQueue = MQ.Transactional;
    }

    private string MSMQPath { get; set; }
    private bool IsTranscationQueue { get; set; }

    /// <summary>
    /// 表示队列是否是事务队列
    /// </summary>
    public bool IsTransQueue { get; set; }

    /// <summary>
    /// 初始化 MessageQueue
    /// </summary>
    /// <returns></returns>
    private MessageQueue MQ { get; set; }

    /// <summary>
    /// 发送一个消息
    /// </summary>
    /// <typeparam name="TModel"></typeparam>
    /// <param name="messageModel"></param>
    /// <returns></returns>
    public bool SendMessage<TModel>(MessageModel<TModel> messageModel) {

        Send(MQ, messageModel);
        return true;
    }


    /// <summary>
    /// 批量发送消息
    /// </summary>
    /// <typeparam name="TModel"></typeparam>
    /// <param name="messageModels"></param>
    public bool SendMessages<TModel>(List<MessageModel<TModel>> messageModels) {
        messageModels.ForEach(one => Send(MQ, one));
        return true;
    }

    /// <summary>
    /// 针对事务队列 发送消息
    /// </summary>
    /// <typeparam name="TModel"></typeparam>
    /// <param name="messageModel"></param>
    public bool SendMessageTran<TModel>(MessageModel<TModel> messageModel) {
        return SendTran(MQ, messageModel);
    }

    /// <summary>
    /// 针对事务队列 发送多条消息
    /// 注意：MessageQueue 必须声明为事务性的才有效
    /// </summary>
    /// <typeparam name="TModel"></typeparam>
    /// <param name="messageModels"></param>
    public bool SendMessagesTran<TModel>(List<MessageModel<TModel>> messageModels) {

        if (!MQ.Transactional) {
            throw new Exception("该消息队列不支持事务,请重新定义");
        }
        using (MessageQueueTransaction transaction = new MessageQueueTransaction()) {
            try {
                transaction.Begin();
                messageModels.ForEach(one => {
                    Message message = Convert2Message(one);
                    MQ.Send(message, transaction);
                });
                transaction.Commit();
                return true;
            }
            catch (Exception) {
                transaction.Abort();
                throw;
            }
        }
    }

    /// <summary>
    /// 接收一个消息
    /// </summary>
    /// <typeparam name="TModel"></typeparam>
    /// <returns></returns>
    public MessageModel<TModel> Receive<TModel>() {
        Message message = MQ.Receive();
        return Convert2MessageModel<TModel>(message);
    }

    /// <summary>
    /// 对于事务队列，发送必须使用事务
    /// 但是，接收可以不用事务
    /// 可以直接用 Receive 方法
    /// </summary>
    /// <typeparam name="TModel"></typeparam>
    /// <returns></returns>
    public MessageModel<TModel> ReceiveTran<TModel>() {
        MessageQueueTransaction transaction = new MessageQueueTransaction();
        try {
            transaction.Begin();
            Message message = MQ.Receive(transaction);
            var messageModel = Convert2MessageModel<TModel>(message);
            transaction.Commit();
            return messageModel;
        }
        catch (Exception) {
            transaction.Abort();
            throw;
        }

    } 

    /// <summary>
    ///  批量获取消息，不会删除消息
    /// </summary>
    /// <typeparam name="TModel"></typeparam>
    /// <returns></returns>
    public IEnumerable<MessageModel<TModel>> ReceiveMuti<TModel>() {

        MessageEnumerator enumerator = MQ.GetMessageEnumerator2();

        while (enumerator.MoveNext()) {

            yield return Convert2MessageModel<TModel>(enumerator.Current);
        }
    }

    /// <summary>
    /// 批量获取消息，然后删除
    /// </summary>
    /// <typeparam name="TModel"></typeparam>
    /// <returns></returns>
    public IEnumerable<MessageModel<TModel>> ReceiveMutiThenDelete<TModel>() {


        MessageEnumerator enumerator = MQ.GetMessageEnumerator2();

        while (enumerator.MoveNext()) {

            Message message = enumerator.RemoveCurrent();
            enumerator = MQ.GetMessageEnumerator2();
            yield return Convert2MessageModel<TModel>(message);
        }
    }

    //common send
    private bool Send<TModel>(MessageQueue messageQueue, MessageModel<TModel> messageModel) {
        messageQueue.Send(Convert2Message(messageModel));
        return true;
    }

    /// <summary>
    /// 发送消息，用于事务队列
    /// </summary>
    /// <typeparam name="TModel"></typeparam>
    /// <param name="messageQueue"></param>
    /// <param name="messageModel"></param>
    /// <returns></returns>
    private bool SendTran<TModel>(MessageQueue messageQueue, MessageModel<TModel> messageModel) {
        MessageQueueTransaction transaction = new MessageQueueTransaction();
        try {
            transaction.Begin();
            messageQueue.Send(Convert2Message(messageModel), transaction);
            transaction.Commit();
            return true;
        }
        catch (Exception) {
            transaction.Abort();
            transaction.Dispose();
            throw;
        }
    }

    private Message Convert2Message<TModel>(MessageModel<TModel> messageModel) {
        Message message = new Message {
            Label = messageModel.Label,
            Body = messageModel.Data
        };

        return message;
    }

    private MessageModel<TModel> Convert2MessageModel<TModel>(Message message) {
        if (message == null) return null;
        message.Formatter = new XmlMessageFormatter(new[] {typeof (TModel)});
        MessageModel<TModel> messageModel = new MessageModel<TModel> {
            Data = (TModel) message.Body,
            Label = message.Label
        };
        return messageModel;
    }

    /// <summary>
    /// 返回消息，异步操作，不会阻塞
    /// 实时性
    /// 每当进来一个消息，此方法就会即时接收这个消息
    /// </summary>
    /// <typeparam name="TModel"></typeparam>
    /// <param name="func">带消息的回调委托</param>
    public void ReceiveAsync<TModel>(Action<MessageModel<TModel>> func) {

        MQ.ReceiveCompleted += (sender, e) => {
            MessageQueue mq = (MessageQueue) sender;
            Message message = mq.EndReceive(e.AsyncResult);
            func(Convert2MessageModel<TModel>(message));
            mq.BeginReceive();
        };
        MQ.BeginReceive();
    }


    public void Dispose() {
        MQ.Close();
        MQ.Dispose();
    }
}
