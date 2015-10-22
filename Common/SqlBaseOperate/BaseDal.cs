using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using KANG.DB.Bridge;

namespace KANG.DAL.BaseOperate {
    public class BaseDal {
        /// <summary>
        /// 生成 where sqlparameter参数
        /// </summary>
        /// <param name="objWhere"></param>
        /// <returns></returns>
        public  SqlParameter[] CreateWhereSqlParameters(Where objWhere) {
            List<Where.Item> items = objWhere.WhereItems;
            List<SqlParameter> parameters = items.Select(item => new SqlParameter($"@{item.Field}", item.Value)).ToList();
            return parameters.ToArray();
        }

        /// <summary>
        /// 生成 update sqlparameter参数
        /// </summary>
        /// <param name="update"></param>
        /// <returns></returns>
        public  SqlParameter[] CreateUpdateSqlParams(Update update) {
            Dictionary<string, dynamic> dic = update.Dic;
            List<SqlParameter> parameters = dic.Select(pair => new SqlParameter($"@{pair.Key}", pair.Value)).ToList();
            Where where = update.WhereCore;
            List<Where.Item> items = where.WhereItems;
            parameters.AddRange(items.Select(item => new SqlParameter($"@{item.Field}", item.Value)));
            return parameters.ToArray();
        }
    }
}
