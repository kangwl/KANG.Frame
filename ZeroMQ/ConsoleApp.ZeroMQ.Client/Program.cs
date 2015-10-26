using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using NetMQ;

namespace ConsoleApp.ZeroMQ.Client {
    class Program {
        static void Main(string[] args) {
            Task.Factory.StartNew(() => {
                using (var mqcontext = NetMQContext.Create())
                using (var client = mqcontext.CreateRequestSocket()) {
                    client.Connect("tcp://localhost:5555");

                    while (true) {
                        Console.WriteLine("Sending Hello");
                        client.SendFrame("Hello");
                        Thread.Sleep(2000);
                        var message = client.ReceiveFrameString();
                        Console.WriteLine("Received {0}", message);
                    }
                }

            });
            Console.Read();
        }
    }
}
