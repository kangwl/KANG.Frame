using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace KANG.Repository {
    /// <summary>
    /// user操作仓库
    /// </summary>
    public class User_Repository {

        private readonly IDAL.IUserOperate<MODEL.User_MODEL> _iUser;

        public User_Repository(IDAL.IUserOperate<MODEL.User_MODEL> iUser) {
            _iUser = iUser;
        }

        public bool Insert(MODEL.User_MODEL userModel) {
            return _iUser.Insert(userModel);
        }
    }
}
