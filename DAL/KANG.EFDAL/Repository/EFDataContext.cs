﻿using System.Data.Entity;

namespace KANG.EFDAL.Repository {
    public class EFDataContext : DbContext {
        public EFDataContext(string nameOrConnstring = "defaultConn") : base(nameOrConnstring) {
            //采用db first
            Database.SetInitializer(new NullDatabaseInitializer<EFDataContext>());
        }

        public DbSet<MODEL.User_MODEL> User { get; set; }
        public DbSet<MODEL.Course_MODEL> Course { get; set; } 




        protected override void OnModelCreating(DbModelBuilder modelBuilder) {
            //映射数据库配置
            //modelBuilder.Entity<MODEL.User_MODEL>().Map(map => {
            //    map.ToTable("User");//映射数据库表名
            //});
            //独立配置
            modelBuilder.Configurations.Add(new Mapping.UserMap());
            modelBuilder.Configurations.Add(new Mapping.CourseMap());
            base.OnModelCreating(modelBuilder);
        }
    }
}
