namespace DB.Interface {
    public interface IDB {
        bool Insert<T>(T t);
        bool Exist(string where);
    }
}