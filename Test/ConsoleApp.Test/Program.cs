using System;
using System.Collections.Generic;
using System.Linq;
using System.ServiceModel;
using System.ServiceModel.Description;
using System.ServiceProcess;
using System.Text;
using System.Threading.Tasks; 
using KANG.MODEL;

namespace ConsoleApp.Test {
    class Program {
        private static void Main(string[] args) {
            // KANG.BLL.User_BLL.Insert(new User_MODEL());
            //KANG.Common.RandomService.GetRndCh();
            //using (ServiceHost serviceHost = new ServiceHost(typeof (KANG.DAL.User_DAL))) {
            //    serviceHost.AddServiceEndpoint(typeof (KANG.IDAL.IUser<KANG.MODEL.User_MODEL>),
            //        new NetHttpBinding(), new Uri("http://localhost:8882/userservice"));
            //    serviceHost.Open();
            //}
            //Console.WriteLine("ok");
            //Console.Read();
            //wshttp 较安全
            //using (ServiceHost host = new ServiceHost(typeof(KANG.DAL.User_DAL))) {
            //    host.AddServiceEndpoint(typeof(KANG.IDAL.IUser<KANG.MODEL.User_MODEL>), new WSHttpBinding(),
            //        "http://127.0.0.1:9988/UserServ");
            //    if (host.Description.Behaviors.Find<ServiceMetadataBehavior>() == null) {
            //        ServiceMetadataBehavior behavior = new ServiceMetadataBehavior();
            //        behavior.HttpGetEnabled = true;
            //        behavior.HttpGetUrl = new Uri("http://127.0.0.1:9988/UserServ/metadata");
            //        host.Description.Behaviors.Add(behavior);
            //    }
            //    host.Opened += delegate {
            //        Console.WriteLine("CalculaorService已经启动，按任意键终止服务！");
            //    };

            //    host.Open();
            //    Console.Read();
            //}
            //basichttp 兼容性强
            using (ServiceHost serviceHost = new ServiceHost(typeof(KANG.DAL.User_DAL),
                new Uri("http://127.0.0.1:9988/UserServ"))) {

                if (serviceHost.Description.Behaviors.Find<ServiceMetadataBehavior>() == null) {
                    ServiceMetadataBehavior behavior = new ServiceMetadataBehavior();
                    behavior.HttpGetEnabled = true;
                    //behavior.HttpGetBinding=
                    //behavior.HttpGetUrl = new Uri("http://127.0.0.1:9988/UserServ.svc/metadata");
                    serviceHost.Description.Behaviors.Add(behavior);
                }
                serviceHost.Open();
                Console.WriteLine("ok");
                Console.Read();
            }


        }
    }
}
