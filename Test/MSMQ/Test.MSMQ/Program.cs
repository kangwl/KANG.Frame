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
            SendMessage();
            //RecieveMessage();
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
            using (MSMQHelper msmqHelper = new MSMQHelper(_msmqPath)) {
                msmqHelper.SendMessage(messageModel);
                //msmqHelper.SendMessages(new List<MSMQHelper.MessageModel<string>>() {messageModel, messageModel, messageModel, messageModel, messageModel });
                Console.WriteLine("send success");
            }
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


     
}
