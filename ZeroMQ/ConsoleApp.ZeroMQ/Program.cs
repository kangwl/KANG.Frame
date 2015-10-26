using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using NetMQ;
using NetMQ.Sockets;

namespace ConsoleApp.ZeroMQ {
    class Program {
        static void Main(string[] args) {
            Task.Factory.StartNew(() => {
                using (var context = NetMQContext.Create())
                using (var server = context.CreateResponseSocket()) {
                    server.Bind("tcp://localhost:5555");
                    while (true) {
                        string message = server.ReceiveFrameString();
                        Console.WriteLine("received:" + message);
                        Thread.Sleep(100);
                        server.SendFrame(DateTime.Now.ToString());
                    }
                }
            });
            Console.Read();

        }
    }

}
