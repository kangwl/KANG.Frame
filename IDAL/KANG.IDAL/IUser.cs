using System.Collections.Generic;
using System.ServiceModel;

namespace KANG.IDAL {
    [ServiceContract(Name = "UserService")]
    public interface IUser<TModel> {
        [OperationContract]
        bool Insert(TModel tModel);

        [OperationContract]
        List<TModel> GetList(string where, int pageIndex = 0, int pageSize = 10);
    }
}