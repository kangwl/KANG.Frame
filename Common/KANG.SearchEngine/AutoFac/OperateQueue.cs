using System.Collections.Concurrent;

namespace KANG.SearchEngine.AutoFac {
    public class OperateQueue {
        private OperateQueue() { }
        static OperateQueue() { }

        public static OperateQueue Instance = new OperateQueue();

        private static readonly ConcurrentQueue<OperateModel> _operateQueue = new ConcurrentQueue<OperateModel>();
        public ConcurrentQueue<OperateModel> OperateQueueModels { get { return _operateQueue; } }

    }
}
