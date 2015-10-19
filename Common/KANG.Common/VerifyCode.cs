#region

using System;
using System.Drawing;
using System.Drawing.Drawing2D;

#endregion

namespace KANG.Common {
    public static class VerifyCode {
        /// <summary>
        ///     随机产生指定长度的验证码字符串
        /// </summary>
        /// <param name="codeCount">验证码字符的个数</param>
        /// <returns>验证码字符串</returns>
        public static string GeneratorVerifyCode(int codeCount) {
            // 随机产生的非负数
            // 根据产生的随机数，按一定规律得到的单个字符
            // 验证码字符串
            var verifyCode = string.Empty;
            var random = new Random();
            for (var i = 0; i < codeCount; i++) {
                var randomNumber = random.Next();
                //// 如果随机数能够整除3的情况
                char singleCode;
                if (randomNumber%3 == 0) {
                    // 产生一个'0'到'9'的字符
                    singleCode = (char) ('0' + randomNumber%10);
                }
                else {
                    if (randomNumber%3 == 1) {
                        // 产生一个'A'到'Z'的字符
                        singleCode = (char) ('A' + randomNumber%26);
                    }
                    else {
                        // 产生一个'a'到'z'的字符
                        singleCode = (char) ('a' + randomNumber%26);
                    }
                }
                verifyCode += singleCode.ToString();
            }
            return verifyCode;
        }

        /// <summary>
        ///     创建验证码图片
        /// </summary>
        public static Image CreateVerifyCodeImage(string verifyCode) {
            if (string.IsNullOrEmpty(verifyCode)) {
                return null;
            }
            var image = new Bitmap(verifyCode.Length*16, 27);
            using (var g = Graphics.FromImage(image)) {
                var random = new Random();
                // 清空验证码图片的背景色
                g.Clear(Color.White);
                // 画验证码图片的背景噪音线
                for (var i = 0; i < 25; i++) {
                    var x1 = random.Next(image.Width);
                    var y1 = random.Next(image.Height);
                    var x2 = random.Next(image.Width);
                    var y2 = random.Next(image.Height);
                    // 在验证码图片上画单条背景噪音线
                    g.DrawLine(new Pen(Color.Silver), x1, y1, x2, y2);
                }

                // 画验证码图片
                var font = new Font("Arial", 15,
                    (FontStyle.Bold | FontStyle.Italic));
                var brush =
                    new LinearGradientBrush(new Rectangle(0, 0, image.Width, image.Height),
                        Color.Blue, Color.DarkRed, 1.2f, true);
                g.DrawString(verifyCode, font, brush, 2, 2);

                // 画验证码图片的前景噪音点
                for (var i = 0; i < 100; i++) {
                    var x = random.Next(image.Width);
                    var y = random.Next(image.Height);
                    image.SetPixel(x, y, Color.FromArgb(random.Next()));
                }

                // 画验证码图片的边框线
                g.DrawRectangle(new Pen(Color.Silver), 0, 0, image.Width - 1, image.Height - 1);
            }
            return image;
        }
    }
}