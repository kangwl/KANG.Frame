﻿using System;
using System.Collections.Generic;
using System.Data;
using KANG.DB.Bridge;
using KANG.IDAL;
using KANG.MODEL;

namespace KANG.MySqlDAL {
    public class User_DAL: IUser<MODEL.User_MODEL>{
        private string _primaryKey;
        private string _tableName;

        public bool Insert(User_MODEL t) {
            return true;
        }

        public bool Update(User_MODEL model) {
            throw new NotImplementedException();
        }

        public bool UpdateName(int id, string name) {
            throw new NotImplementedException();
        }

        public bool UpdateName(User_MODEL model) {
            throw new NotImplementedException();
        }

        public bool Delete(int id) {
            throw new NotImplementedException();
        }

        public User_MODEL GetOne(int id) {
            throw new NotImplementedException();
        }

        public List<User_MODEL> GetList(Where objWhere = null, string @orderby = "ID ASC", int pageIndex = 0, int pageSize = 10) {
            throw new NotImplementedException();
        }

        public DataTable GetDataTable(string fields, Where objWhere = null, string @orderby = "ID ASC", int pageIndex = 0,
            int pageSize = 10) {
            throw new NotImplementedException();
        }


        public string PrimaryKey
        {
            get { return _primaryKey; }
        }

        public string TableName
        {
            get { return _tableName; }
        }
    }
}
