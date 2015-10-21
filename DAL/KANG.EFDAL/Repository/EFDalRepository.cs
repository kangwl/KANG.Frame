using System.Data.Entity;

namespace KANG.EFDAL.Repository {
    internal class EFDalRepository : DbContext {
        public EFDalRepository(string nameOrConnstring = "defaultConn") : base(nameOrConnstring) {
            //采用db first
            Database.SetInitializer(new NullDatabaseInitializer<EFDalRepository>());
        }

        public DbSet<MODEL.User_MODEL> User { get; set; }

        protected override void OnModelCreating(DbModelBuilder modelBuilder) {
            //映射数据库配置
            modelBuilder.Entity<MODEL.User_MODEL>().Map(map => {
                map.ToTable("User");//映射数据库表名
            });
            
            base.OnModelCreating(modelBuilder);
        }
    }
}
