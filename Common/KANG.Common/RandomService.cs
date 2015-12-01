using System;

namespace KANG.Common
{
    public static class RandomService
    {
        /// <summary>
        /// 随机数种子值
        /// </summary>
        public static int RandomSeed
        {
            get
            {
                byte[] bytes = new byte[4];
                System.Security.Cryptography.RNGCryptoServiceProvider rng =
                    new System.Security.Cryptography.RNGCryptoServiceProvider();
                rng.GetBytes(bytes);
                return BitConverter.ToInt32(bytes, 0);
            }
        }

        /// <summary>
        /// 数字随机数
        /// </summary>
        /// <returns></returns>
        public static string GetRndNum(int len = 4) {
            string code = string.Empty;
            Random random = new Random();
            for (int i = 0; i < len; i++) {
                code += random.Next(9);
            }
            return code;
        }


        /// <summary>
        ///  英文随机
        /// </summary>
        /// <returns></returns>
        public static string GetRndStr(int len = 4) {
            string Vchar = "A,B,C,D,E,F,G,H,I,J,K,L,M,N,O,P,Q,R,S,T,U,V,W,X,Y,Z";
            string[] VcArray = Vchar.Split(',');
            string checkCode = string.Empty;
            Random rand = new Random();
            for (int i = 0; i < len; i++) {
                int t = rand.Next(VcArray.Length);
                checkCode += VcArray[t];
            }
            return checkCode;
        }

        /// <summary>
        /// 中文随机
        /// </summary>
        /// <returns></returns>
        public static string GetRndCh(int len = 4) {
            System.Text.Encoding gb = System.Text.Encoding.Default; //获取GB2312编码页（表）
            object[] bytes = CreateRegionCode(len); //生4个随机中文汉字编码
            string[] str = new string[len];
            System.Text.StringBuilder sb = new System.Text.StringBuilder();
            for (int i = 0; i < len; i++) {
                //根据汉字编码的字节数组解码出中文汉字
                str[i] = gb.GetString((byte[]) Convert.ChangeType(bytes[i], typeof (byte[])));
                sb.Append(str[i].ToString());
            }
            return sb.ToString();
        }

        /// <summary>
        /// 产生随机中文字符
        /// </summary>
        /// <param name="strlength"></param>
        /// <returns></returns>
        private static object[] CreateRegionCode(int strlength)
        {
            //定义一个字符串数组储存汉字编码的组成元素
            string[] rBase = new String[16] { "0", "1", "2", "3", "4", "5", "6", "7", "8", "9", "a", "b", "c", "d", "e", "f" };
            Random rnd = new Random();
            object[] bytes = new object[strlength];

            for (int i = 0; i < strlength; i++)
            {
                //区位码第1位
                int r1 = rnd.Next(11, 14);
                string str_r1 = rBase[r1].Trim();
                //区位码第2位
                rnd = new Random(r1 * unchecked((int)DateTime.Now.Ticks) + i);
                int r2;
                if (r1 == 13)
                {
                    r2 = rnd.Next(0, 7);
                }
                else
                {
                    r2 = rnd.Next(0, 16);
                }
                string str_r2 = rBase[r2].Trim();

                //区位码第3位
                rnd = new Random(r2 * unchecked((int)DateTime.Now.Ticks) + i);//更换随机种子
                int r3 = rnd.Next(10, 16);
                string str_r3 = rBase[r3].Trim();

                //区位码第4位
                rnd = new Random(r3 * unchecked((int)DateTime.Now.Ticks) + i);
                int r4;
                if (r3 == 10)
                {
                    r4 = rnd.Next(1, 16);
                }
                else if (r3 == 15)
                {
                    r4 = rnd.Next(0, 15);
                }
                else
                {
                    r4 = rnd.Next(0, 16);
                }
                string str_r4 = rBase[r4].Trim();
                //定义两个字节变量存储产生的随机汉字区位码
                byte byte1 = Convert.ToByte(str_r1 + str_r2, 16);
                byte byte2 = Convert.ToByte(str_r3 + str_r4, 16);

                //将两个字节变量存储在字节数组中
                byte[] str_r = new byte[] { byte1, byte2 };

                //将产生的一个汉字的字节数组放入object数组中
                bytes.SetValue(str_r, i);
            }
            return bytes;
        }
    }
}
