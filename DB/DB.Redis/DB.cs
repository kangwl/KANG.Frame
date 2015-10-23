using System;

namespace RedisDB {
    public class DB  {
        public bool Insert<T>(T t) {
            return true;
        }

        public bool Exist(string @where) {
            throw new NotImplementedException();
        }
    }
}
