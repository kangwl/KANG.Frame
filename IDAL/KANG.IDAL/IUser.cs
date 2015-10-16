using System.Collections.Generic;

namespace KANG.IDAL {
    public interface IUserOperate<T> {
        bool Insert(T t);
        List<T> GetList(string where, int pageIndex, int pageSize);
    }
}