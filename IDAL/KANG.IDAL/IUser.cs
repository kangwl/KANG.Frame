using System.Collections.Generic;
using System.ServiceModel;

namespace KANG.IDAL {
    [ServiceContract]
    public interface IUserOperate<T> {
        [OperationContract]
        bool Insert(T t);
        [OperationContract]
        List<T> GetList(string where, int pageIndex, int pageSize);
    }
}