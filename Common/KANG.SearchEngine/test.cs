using System;
using JiebaNet.Segmenter;

namespace XK.SearchEngine {
   public class test {
        public static void Run() {
            while (true) {

                var str = Console.ReadLine();
                var segmenter = new JiebaSegmenter();
                var segments = segmenter.Cut(str, cutAll: true);
                Console.WriteLine("【全模式】：{0}", string.Join("/ ", segments));

                segments = segmenter.Cut(str); // 默认为精确模式
                Console.WriteLine("【精确模式】：{0}", string.Join("/ ", segments));

                segments = segmenter.Cut(str); // 默认为精确模式，同时也使用HMM模型
                Console.WriteLine("【新词识别】：{0}", string.Join("/ ", segments));

                segments = segmenter.CutForSearch(str); // 搜索引擎模式
                Console.WriteLine("【搜索引擎模式】：{0}", string.Join("/ ", segments));

                segments = segmenter.Cut(str);
                Console.WriteLine("【歧义消除】：{0}", string.Join("/ ", segments));
            }
        }
    }
}
