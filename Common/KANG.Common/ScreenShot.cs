#region

using System;
using System.Drawing;
using System.Windows.Forms;

#endregion

namespace KANG.Common {
    public class ScreenShot {
        /// <summary>
        ///     根据坐标点获取屏幕图像
        /// </summary>
        /// <param name="x1">左上角横坐标</param>
        /// <param name="y1">左上角纵坐标</param>
        /// <param name="x2">右下角横坐标</param>
        /// <param name="y2">右下角纵坐标</param>
        /// <returns></returns>
        public static Image GetScreen(int x1, int y1, int x2, int y2) {
            var w = (x2 - x1);
            var h = (y2 - y1);
            Image myImage = new Bitmap(w, h);
            var g = Graphics.FromImage(myImage);
            g.CopyFromScreen(new Point(x1, y1), new Point(0, 0), new Size(w, h));
            var dc1 = g.GetHdc();
            g.ReleaseHdc(dc1);
            return myImage;
        }

        /// <summary>
        ///     获取桌面截图
        /// </summary>
        /// <returns></returns>
        public static Bitmap GetImgDesk() {
            Bitmap img;
            try {
                var rect = SystemInformation.VirtualScreen;
                //获取屏幕分辨率
                var x_ = rect.Width;
                var y_ = rect.Height;
                //截屏
                img = new Bitmap(x_, y_);
                using (var g = Graphics.FromImage(img)) {
                    g.CopyFromScreen(new Point(0, 0), new Point(0, 0), new Size(x_, y_));
                }
            }
            catch (Exception) {
                img = null;
            }
            return img;
        }

        /// <summary>
        ///     保存屏幕截图
        /// </summary>
        /// <param name="fileName">完全路径</param>
        public static void SaveScreenImage(string fileName) {
            using (var image = GetImgDesk()) {
                image.Save(fileName);
            }
        }
    }
}