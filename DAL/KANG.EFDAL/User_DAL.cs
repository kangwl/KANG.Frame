using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Data.SqlClient;
using System.Linq;

using KANG.DAL.BaseOperate;
using KANG.DB.Bridge;
using KANG.EFDAL.Ext;
using KANG.EFDAL.Repository;
using KANG.MODEL;

namespace KANG.EFDAL {
    public class User_DAL : BaseDal, IDAL.IUser<MODEL.User_MODEL> {
        private string _primaryKey = "[ID]";
        private string _tableName = "[User]";

        public string PrimaryKey
        {
            get { return _primaryKey; }
        }

        public string TableName
        {
            get { return _tableName; }
        }


        public bool Insert(User_MODEL model) {
            using (EFDataContext dataContext = new EFDataContext()) {
                dataContext.User.Add(model);
                dataContext.SaveChanges();
                return true;
            }
        }

        public bool Update(MODEL.User_MODEL model) {
            using (EFDataContext dataContext = new EFDataContext()) {
                dataContext.Entry(model).State = EntityState.Modified;
                dataContext.SaveChanges();
                return true;
            }
        }
        /// <summary>
        /// 根据主键ID查找实体，然后更新其Name属性
        /// </summary>
        /// <param name="id"></param>
        /// <param name="name"></param>
        /// <returns></returns>
        public bool UpdateName(int id, string name) {
            using (EFDataContext dataContext = new EFDataContext()) {
                MODEL.User_MODEL userModel = dataContext.User.Find(id);
                if (userModel != null) {
                    dataContext.User.Attach(userModel);
                    userModel.Name = name;
                    dataContext.SaveChanges();
                }
                return true;
            }
        }
        /// <summary>
        /// 更新用户姓名
        /// userModel 包含了已更新的Name
        /// </summary>
        /// <param name="userModel"></param>
        /// <returns></returns>
        public bool UpdateName(MODEL.User_MODEL userModel) {
            using (EFDataContext dataContext=new EFDataContext()) {
                dataContext.User.Attach(userModel);
                dataContext.Entry(userModel).Property(one => one.Name).IsModified = true;
                dataContext.SaveChanges();
                return true;
            }
        }

        public bool Delete(int id) {
            using (EFDataContext dataContext=new EFDataContext()) {
                MODEL.User_MODEL userModel = dataContext.User.Find(id);
                if (userModel != null) {
                    dataContext.User.Remove(userModel);
                    dataContext.SaveChanges();
                    return true;
                }
                return false;
            }
        }

        public bool Delete(MODEL.User_MODEL userModel) {
            using (EFDataContext dataContext=new EFDataContext()) {
                dataContext.User.Attach(userModel);
                dataContext.User.Remove(userModel);
                dataContext.SaveChanges();
                return true;
            }
        }


        public MODEL.User_MODEL GetOne(int id) {
            using (EFDataContext dataContext = new EFDataContext()) {
                return dataContext.User.Find(id);
            }
        }


        public List<MODEL.User_MODEL> GetList( Where objWhere = null, string orderField = null, bool asc = true,
            int pageIndex = 0, int pageSize = 10) {
            using (EFDataContext dataContext = new EFDataContext()) {

                orderField = orderField ?? PrimaryKey;
                string sortDirection = asc ? "asc" : "desc";
                string orderby = $" {orderField} {sortDirection} ";

                string where = (objWhere == null) ? "1=1" : objWhere.Result;
                string sql = $"select * from {TableName} where {where}";
                const string sqlPageBase = @"select * from( 
                            select *,ROW_NUMBER() OVER (ORDER BY {1}) as rank from ({0})a 
                          )as t where t.rank between {2} and {3}";

                int startPageIndex = pageIndex*pageSize + 1;
                int endPageIndex = pageIndex*pageSize + pageSize;

                string sqlPage = string.Format(sqlPageBase, sql, orderby, startPageIndex, endPageIndex);
                return (objWhere == null)
                    ? dataContext.Database.SqlQuery<MODEL.User_MODEL>(sqlPage)
                        //.Select(data => new User_MODEL() {Name = data.Name})//选取需要的字段
                        .ToList()
                    : dataContext.Database.SqlQuery<MODEL.User_MODEL>(sqlPage, CreateWhereSqlParameters(objWhere))
                        //.Select(data => new User_MODEL() {Name = data.Name})
                        .ToList();
            }
        }

        public DataTable GetDataTable(string fields = "*", Where objWhere = null, string orderField = null,
            bool asc = true, int pageIndex = 0,
            int pageSize = 10) {
            using (EFDataContext dataContext = new EFDataContext()) {

                orderField = orderField ?? PrimaryKey;
                string sortDirection = asc ? "asc" : "desc";
                string orderby = $" {orderField} {sortDirection} ";

                if (objWhere == null) {
                    objWhere = new Where().Add(new Where.Item("1", "=", "1"));
                }
                string sql = $"select {fields} from {TableName} where {objWhere.Result}";
                const string sqlPageBase = @"select * from( 
                            select *,ROW_NUMBER() OVER (ORDER BY {1}) as rank from ({0})a 
                          )as t where t.rank between {2} and {3}";

                int startPageIndex = pageIndex*pageSize + 1;
                int endPageIndex = pageIndex*pageSize + pageSize;

                string sqlPage = string.Format(sqlPageBase, sql, orderby, startPageIndex, endPageIndex);
                return dataContext.Database.SqlQueryForDataTatable(sqlPage, CreateWhereSqlParameters(objWhere));
            }
        }

        public int GetRecordCount(Where objWhere = null) {
            using (EFDataContext dataContext = new EFDataContext()) {
                if (objWhere == null) {
                    objWhere = new Where().Add(new Where.Item("1", "=", "1"));
                }
                return dataContext.Database.SqlQuery<int>(
                    $"select count({PrimaryKey}) from {TableName} where {objWhere.Result}",
                    CreateWhereSqlParameters(objWhere)).First();
            }
        }

    }
}
