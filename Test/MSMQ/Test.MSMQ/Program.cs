using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Messaging;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace Test.MSMQ {
    class Program {

        private static string _msmqPath = @".\Private$\kangMSMQ";
        public static string _msmqTransPath = @".\Private$\kangTransMSMQ";
        static void Main(string[] args) {
            Task.Factory.StartNew(SendMessage);
            Console.WriteLine("okkkkkkkkkkkkk");
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
          
            MSMQHelper msmqHelper = new MSMQHelper(_msmqPath);

            //Stopwatch stopwatch = new Stopwatch();
            //stopwatch.Start();
            //var re = Parallel.For(0, 30, (i, s) => {
            //    messageModel.Data = "k"+i;
            //    messageModel.Label = "message1";
            //    msmqHelper.SendMessage(messageModel);
            //    Console.WriteLine(i);
            //});

            //var re = Parallel.ForEach(Partitioner.Create(0, 20000), (a, b, c) => {
            //    for (int i = a.Item1; i < a.Item2; i++) {
            //        msmqHelper.SendMessage(messageModel);
            //        Console.WriteLine(i);
            //    }
            //});
            for (int i = 0; i < 20; i++) {
                messageModel.Data = "k" + i;
                messageModel.Label = "message1";
                msmqHelper.SendMessage(messageModel);
                Console.WriteLine(i);
            }
            //stopwatch.Stop();
            //Console.WriteLine("send ok");
            //Console.WriteLine(stopwatch.ElapsedMilliseconds);
            //if (re.IsCompleted) {


            //    Console.WriteLine("send ok");
            //    Console.WriteLine(stopwatch.ElapsedMilliseconds);
            //    msmqHelper.Dispose();
            //}

            //msmqHelper.SendMessages(new List<MSMQHelper.MessageModel<string>>() {messageModel, messageModel, messageModel, messageModel, messageModel });
            //Console.WriteLine("send success");
        }

            //using (MSMQHelper msmqHelper = new MSMQHelper(_msmqTransPath,true)) {
            //    Console.WriteLine(msmqHelper.IsTransQueue);
            //    bool success = msmqHelper.SendMessageTran(messageModel);
            //    Console.WriteLine(success);
            //}
        }
       
 
 
 

    


     
}
