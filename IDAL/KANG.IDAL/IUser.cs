using System;
using System.Collections.Generic;
using System.Data;
using System.ServiceModel;
using KANG.DB.Bridge;
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

        bool Delete(int id);
        bool Delete(TModel userModel);

        TModel GetOne(int id);

        List<TModel> GetList(Where objWhere = null, string orderField = null, bool asc = true,
            int pageIndex = 0, int pageSize = 10);

        DataTable GetDataTable(string fields="*", Where objWhere = null, string orderField = null, bool asc = true,
            int pageIndex = 0, int pageSize = 10);

        int GetRecordCount(Where objWhere = null);
    }
}