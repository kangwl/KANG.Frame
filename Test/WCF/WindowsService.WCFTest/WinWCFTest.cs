using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.ServiceProcess;
using System.Text;
using System.Threading.Tasks;
using KANG.Common;

namespace WindowsService.WCFTest {
    partial class WinWCFTest : ServiceBase {
        public WinWCFTest() {
            InitializeComponent();
        }

        protected override void OnStart(string[] args) {
            // TODO: 在此处添加代码以启动服务。
            Log4net.Init(Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "Config\\log4net_n.xml"));
            WcfServiceTest.StartService();
        }

        protected override void OnStop() {
            // TODO: 在此处添加代码以执行停止服务所需的关闭操作。
            WcfServiceTest.StopService();
        }
    }
}
