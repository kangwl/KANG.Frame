using System;

namespace KANG.MODEL
{
    public partial class Course_MODEL
    {
        public int ID { get; set; }
        public string Name { get; set; }
        public string Describe { get; set; }
        public double? Price { get; set; }
        public string Author { get; set; }
        public int? AddUser { get; set; }
        public DateTime? AddDateTime { get; set; }
        public DateTime? UpdateDateTime { get; set; }
    }
}
