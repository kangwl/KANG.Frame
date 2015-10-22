using System.Collections.Generic;
using System.ServiceModel;

namespace KANG.IDAL {
    [ServiceContract(Name = "UserService")]
    public interface IUser<TModel> {
        [OperationContract]
        bool Insert(TModel model);
        [OperationContract]
        bool Update(TModel model);
        [OperationContract]
        List<TModel> GetList(string where, int pageIndex = 0, int pageSize = 10);
    }
}