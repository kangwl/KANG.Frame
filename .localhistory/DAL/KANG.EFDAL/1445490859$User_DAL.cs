using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Data.Entity.Migrations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using KANG.EFDAL.Repository;
using KANG.MODEL;

namespace KANG.EFDAL {
    public class User_DAL : IDAL.IUser<MODEL.User_MODEL> {
         
        
        public bool Insert(User_MODEL model) {
            using (EFDalRepository dalRepository = new EFDalRepository()) {
                dalRepository.User.Add(model);
                dalRepository.SaveChanges();
                return true;
            }
        }

        public bool Update(MODEL.User_MODEL model) {
            using (EFDalRepository dalRepository = new EFDalRepository()) {
                dalRepository.Entry(model).State = EntityState.Modified;
                dalRepository.SaveChanges();
                return true;
            }
        }

        public bool UpdateName(int id,string name) {
            dalRepository
        }
 
        public List<User_MODEL> GetList(string @where, int pageIndex = 0, int pageSize = 10) {
                int skip = pageIndex*pageSize;
                var enumrableUsers = dalRepository.User.SqlQuery("").Skip(skip).Take(pageSize);
                return enumrableUsers.ToList();
        }
 
    }
}
