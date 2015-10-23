namespace KANG.IDAL.BaseInterface {
    public interface IBase {
        /// <summary>
        /// 数据库表主键名 exp：ID
        /// </summary>
        string PrimaryKey { get;  }
        /// <summary>
        /// 数据库表表名
        /// </summary>
        string TableName { get; }
    }
}