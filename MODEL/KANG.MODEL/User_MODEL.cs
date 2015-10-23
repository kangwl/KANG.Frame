using System;

namespace KANG.MODEL
{
    public partial class User_MODEL
    {
        public int ID { get; set; }
        public string Name { get; set; }
        public int? Sex { get; set; }
        public int? Age { get; set; }
        public string UserID { get; set; }
        public string Password { get; set; }
        public int? UserType { get; set; }
    }
}
