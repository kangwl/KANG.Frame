using System;
using System.Collections.Generic;
using System.Messaging;

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

        MQ = !MessageQueue.Exists(MSMQPath)
            ? MessageQueue.Create(MSMQPath, IsTranscationQueue)
            : new MessageQueue(MSMQPath);
    }

    private string MSMQPath { get; set; }
    private bool IsTranscationQueue { get; set; }

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
    public void SendMessages<TModel>(List<MessageModel<TModel>> messageModels) {
        messageModels.ForEach(one => Send(MQ, one));
    }

    /// <summary>
    /// 使用事务队列 发送多条消息
    /// 注意：MessageQueue 必须声明为事务性的才有效
    /// </summary>
    /// <typeparam name="TModel"></typeparam>
    /// <param name="messageModels"></param>
    public void SendMessageTrans<TModel>(List<MessageModel<TModel>> messageModels) {

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
    public MessageModel<TModel> Recieve<TModel>() {
        MQ.MessageReadPropertyFilter.Priority = true;
        Message message = MQ.Receive();
        if (message == null) return null;
        message.Formatter = new XmlMessageFormatter(new Type[] {typeof (TModel)});

        return Convert2MessageModel<TModel>(message);
    }

    /// <summary>
    ///  批量获取消息，不会删除消息
    /// </summary>
    /// <typeparam name="TModel"></typeparam>
    /// <returns></returns>
    public IEnumerable<MessageModel<TModel>> RecieveMuti<TModel>() {
        MQ.MessageReadPropertyFilter.Priority = true;

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
    public IEnumerable<MessageModel<TModel>> RecieveMutiThenDelete<TModel>() {

        MQ.MessageReadPropertyFilter.Priority = true;

        MessageEnumerator enumerator = MQ.GetMessageEnumerator2();

        while (enumerator.MoveNext()) {

            Message message = enumerator.RemoveCurrent();
            enumerator = MQ.GetMessageEnumerator2();
            yield return Convert2MessageModel<TModel>(message);
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

    /// <summary>
    /// 返回消息，异步操作，不会阻塞
    /// </summary>
    /// <typeparam name="TModel"></typeparam>
    /// <param name="func">带消息的回调委托</param>
    public void RecieveAsync<TModel>(Action<MessageModel<TModel>> func) {

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
