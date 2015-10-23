using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using KANG.DAL.BaseOperate;
using KANG.DB.Bridge;
using KANG.MODEL;

namespace KANG.EFDAL {
    public class Course_DAL : BaseDal, IDAL.ICourse {
        private string _primaryKey;
        private string _tableName;

        public string PrimaryKey
        {
            get { return _primaryKey; }
        }

        public string TableName
        {
            get { return _tableName; }
        }

        public bool Insert(Course_MODEL model) {
            throw new NotImplementedException();
        }

        public bool Update(Course_MODEL model) {
            throw new NotImplementedException();
        }

        public bool Delete(int id) {
            throw new NotImplementedException();
        }

        public Course_MODEL GetOne(int id) {
            throw new NotImplementedException();
        }

        public List<Course_MODEL> GetList(Where objWhere = null, string orderField = null, bool asc = true, int pageIndex = 0, int pageSize = 10) {
            throw new NotImplementedException();
        }

        public DataTable GetDataTable(string fields = "*", Where objWhere = null, string orderField = null, bool asc = true,
            int pageIndex = 0, int pageSize = 10) {
            throw new NotImplementedException();
        }

        public int GetRecordCount(Where objWhere = null) {
            throw new NotImplementedException();
        }
    }
}
