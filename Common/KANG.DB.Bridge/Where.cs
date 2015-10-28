using System;
using System.Collections.Generic;

namespace KANG.DB.Bridge {
    /// <summary>
    /// 用对象的方式替换string类型的where条件
    /// </summary>
    public class Where {
        public Where() {
            WherelistLazy = new Lazy<List<string>>();
            WhereItems = new List<Item>();
        }

        public Where(params Item[] items):this() {
            foreach (Item item in items) {
                Add(item);
            }
        }
        /// <summary>
        /// EXP:id=@id
        /// age>@age
        /// </summary>
        /// <param name="item"></param>
        /// <returns></returns>
        public Where Add(Item item) {
            WhereItems.Add(item);
            WherelistLazy.Value.Add($"[{item.Field}] {item.Sign} @{item.Field}");
            return this;
        }

        public void AddRange(List<Item> items) {
            items.ForEach(one => Add(one));
        }

        public int Count => WherelistLazy.Value.Count;
        private Lazy<List<string>> WherelistLazy { get; set; }

        public List<Item> WhereItems { get; set; } 

        /// <summary>
        /// 组装完成的where条件
        /// </summary>
        public string Result => string.Join(" and ", WherelistLazy.Value);

        public class Item {
            public Item(string field,string sign,dynamic value) {
                Field = field;
                Sign = sign;
                Value = value;
            }
            public string Field { get; set; }
            public string Sign { get; set; }
            public dynamic Value { get; set; }
        }
    }
}
