using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Test.MSMQ.Recieve {
    class Program {
        private static string _msmqPath = @".\Private$\kangMSMQ";
        static void Main(string[] args) {
            RecieveMessage();
            Console.Read();
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
           // msmqHelper.RecieveAsync<string>(one => { Console.WriteLine(one.Data); });//异步操作，不会阻塞
            //Console.WriteLine(msmqHelper.Recieve<string>().Data);//同步操作会阻塞主线程
            Console.WriteLine(123);
        }
    }
}
