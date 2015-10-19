using System;
using System.Collections.Generic;
using System.Threading;

namespace KANG.SearchEngine.AutoFac {

    /*例子：
        
        */

    /// <summary>
    /// 自动化处理
    /// </summary>
    public class Enter {
    
       private Enter() { }

        static Enter() { }

        private static Enter _enter = new Enter();

        public static Enter Instance {
            get { return _enter; }
        }

        /// <summary>
        /// 外部调用入口
        /// 调用即可，会自动处理
        /// </summary>
        /// <param name="operateModel"></param>
        public void Add(OperateModel operateModel) {
            Add(new List<OperateModel>() {operateModel});
        }

        /// <summary>
        /// 外部调用入口
        /// 调用即可，会自动处理
        /// </summary>
        /// <param name="operateModel"></param>
        public void Add(List<OperateModel> operateModel) {
            operateModel.ForEach(OperateQueue.Instance.OperateQueueModels.Enqueue); 
        }

        /// <summary>
        /// 在程序启动时调用此方法即可
        /// 例如：global.asax
        /// 启动lucene的文档自动化工作线程
        /// </summary>
        public static void InitLuceneWorkThread() {
            new Thread(DoWork).Start();
        }

        private static void DoWork() {
            while (true) {
                try {

                    while (OperateQueue.Instance.OperateQueueModels.Count > 0) {
                        OperateModel model;
                        bool success = OperateQueue.Instance.OperateQueueModels.TryDequeue(out model);
                        if (success) {
                            if (string.IsNullOrEmpty(model.DataBasePath) || string.IsNullOrEmpty(model.FilePath)) {
                                throw new ArgumentNullException("DataBasePath,FilePath", "不能缺少"); 
                            }
                            DocIndex docIndex = new DocIndex(model.FilePath, model.DataBasePath);
                            switch (model.OperateEnum) {
                                case OperateEnum.Add:
                                    docIndex.AddLuceneIndex(model.Dic);
                                    break;
                                case OperateEnum.Delete:
                                    docIndex.DeleteLuceneIndexRecord(model.Dic);
                                    break;
                            }
                        }
                    }
                    Thread.Sleep(200);
                }
                catch (Exception ex) {
                    KANG.Common.Log4net.Error(ex);
                }
            }
        }

    }

    public class OperateModel {
 
        public OperateEnum OperateEnum { get; set; }
        public Dictionary<string, string> Dic { get; set; }
        public string DataBasePath { get; set; }
        public string FilePath { get; set; }
    }


    public enum OperateEnum {
        Add = 1,
        Delete = 2
    }
}
