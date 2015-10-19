#region

using System.Management;

#endregion

namespace KANG.Common {
    public class HardWareInfo {
        /// <summary>
        ///     cpu序列号
        /// </summary>
        /// <returns></returns>
        public static string getID_CpuId() {
            var cpuInfo = ""; //cpu序列号
            var cimobject = new ManagementClass("Win32_Processor");
            var moc = cimobject.GetInstances();
            foreach (var o in moc) {
                var mo = (ManagementObject) o;
                cpuInfo = mo.Properties["ProcessorId"].Value.ToString();
            }
            return cpuInfo;
        }

        /// <summary>
        ///     硬盘ID号
        /// </summary>
        /// <returns></returns>
        public static string getID_HardDiskId() {
            var HDid = "";
            var cimobject = new ManagementClass("Win32_DiskDrive");
            var moc = cimobject.GetInstances();
            foreach (var o in moc) {
                var mo = (ManagementObject) o;
                HDid = (string) mo.Properties["Model"].Value;
            }
            return HDid;
        }

        /// <summary>
        ///     获取网卡MacAddress
        /// </summary>
        /// <returns></returns>
        public static string getID_NetCardId() {
            var NCid = "";
            var mc = new ManagementClass("Win32_NetworkAdapterConfiguration");
            var moc = mc.GetInstances();
            foreach (var o in moc) {
                var mo = (ManagementObject) o;
                if ((bool) mo["IPEnabled"])
                    NCid = mo["MacAddress"].ToString();
                mo.Dispose();
            }
            return NCid;
        }
    }
}