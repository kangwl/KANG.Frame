using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using KANG.EFDAL.Repository;
using KANG.MODEL;

namespace KANG.EFDAL {
    public class User_DAL : IDAL.IUser<MODEL.User_MODEL> {
        public bool Insert(User_MODEL tModel) {
            using (EFDalRepository dalRepository = new EFDalRepository()) {
                dalRepository.User.Add(tModel);
                dalRepository.SaveChanges();
            }
            return true;
        }

        public List<User_MODEL> GetList(string @where, int pageIndex = 0, int pageSize = 10) {
            using (EFDalRepository dalRepository = new EFDalRepository()) {
                int skip = pageIndex*pageSize;
                var enumrableUsers = dalRepository.User.SqlQuery("").Skip(skip).Take(pageSize);
                return enumrableUsers.ToList();
            }
        }

    }
}
