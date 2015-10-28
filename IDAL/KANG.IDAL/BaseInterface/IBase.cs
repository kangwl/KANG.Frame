using System.Collections.Generic;
using System.Data;
using KANG.DB.Bridge;

namespace KANG.IDAL.BaseInterface {
    public interface IBase<TModel> {
        /// <summary>
        /// 数据库表主键名 exp：ID
        /// </summary>
        string PrimaryKey { get; }
        /// <summary>
        /// 数据库表表名
        /// </summary>
        string TableName { get; }

        bool Insert(TModel model);
        bool Update(TModel model);
        bool Delete(int id);
        TModel GetOne(int id);

        List<TModel> GetList(Where objWhere = null, string orderField = null, bool asc = true,
            int pageIndex = 0, int pageSize = 10);

        int GetRecordCount(Where objWhere = null);
    }
}