using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DB.Base {
    public class DBBase {
        private readonly Interface.IDB _iDb;
        public DBBase(DB.Interface.IDB iDb) {
            _iDb = iDb;
        }

        public bool Insert<T>(T t) {
            return _iDb.Insert(t);
        }
    }
}
