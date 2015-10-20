using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Web;

namespace KANG.MVC.Test.Models
{
    public class KANGMVCTestContext : DbContext
    {
        // You can add custom code to this file. Changes will not be overwritten.
        // 
        // If you want Entity Framework to drop and regenerate your database
        // automatically whenever you change your model schema, please use data migrations.
        // For more information refer to the documentation:
        // http://msdn.microsoft.com/en-us/data/jj591621.aspx
    
        public KANGMVCTestContext() : base("name=KANGMVCTestContext")
        {
        }

        public System.Data.Entity.DbSet<KANG.MVC.Test.Models.User_Model> User_Model { get; set; }

        public System.Data.Entity.DbSet<KANG.MVC.Test.Models.Course_Model> Course_Model { get; set; }
    }
}
