﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using KANG.IDAL;
using KANG.MODEL;

namespace KANG.DAL {
    public class User_DAL : IUser<MODEL.User_MODEL> {

        public bool Insert(User_MODEL t) {
            return true;
        }

        public bool Update(User_MODEL model) {
            throw new NotImplementedException();
        }

        public List<User_MODEL> GetList(string @where, int pageIndex, int pageSize) {
            throw new NotImplementedException();
        }
    }
}