using System;
using System.Collections.Generic;
using System.Data;
using System.ServiceModel;
using KANG.DB.Bridge;
using KANG.IDAL.BaseInterface;

namespace KANG.IDAL {
    [ServiceContract(Name = "UserService")]
    public interface IUser : IBase<MODEL.User_MODEL> {

        [OperationContract]
        bool UpdateName(int id, string name);

        [OperationContract]
        bool UpdateName(MODEL.User_MODEL model);

        bool Delete(MODEL.User_MODEL userModel);

        DataTable GetDataTable(string fields = "*", Where objWhere = null, string orderField = null, bool asc = true,
            int pageIndex = 0, int pageSize = 10);
    }
}