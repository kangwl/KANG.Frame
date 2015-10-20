using System;
using System.Collections.Generic;
using System.Linq;
using System.ServiceModel;
using System.ServiceModel.Description;
using System.Text;
using System.Threading.Tasks;
using KANG.Common;

namespace ConsoleApp.Test {
    public class WcfServiceTest {
        private static ServiceHost serviceHost = null;

        public static void StartService() {
            serviceHost = new ServiceHost(typeof (KANG.DAL.User_DAL), new Uri("http://127.0.0.1:9989/user"));
            serviceHost.Description.Behaviors.Add(new ServiceMetadataBehavior() {
                HttpGetEnabled = true
            });
            serviceHost.Opened += (s, e) => {
                Log4net.Info("start");
            };
            serviceHost.Closed += (s, e) => {
                Log4net.Info("end");
            };
            serviceHost.Open();
        }

        public static void StopService() {
            if (serviceHost != null) {
                serviceHost.Close();
            }
        }
    }
}
