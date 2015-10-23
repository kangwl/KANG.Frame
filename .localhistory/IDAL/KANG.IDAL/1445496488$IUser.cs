using System;
using System.Collections.Generic;
using System.ServiceModel;
using KANG.IDAL.BaseInterface;

namespace KANG.IDAL {
    [ServiceContract(Name = "UserService")]
    public interface IUser<TModel> : IBase {
        [OperationContract]
        bool Insert(TModel model);

        [OperationContract]
        bool Update(TModel model);

        [OperationContract]
        bool UpdateName(int id, string name);

        [OperationContract]
        bool UpdateName(TModel model);

        TModel GetOne(int id);
    }
}