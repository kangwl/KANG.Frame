using System.Collections.Generic;
using System.Linq;
using StackExchange.Redis;
using KANG.Common;

namespace KANG.Redis.Extension {

    public static class SetExtension {

        public static bool SetAddModel<TModel>(this IDatabase db, string key, TModel tModel,
            CommandFlags flags = CommandFlags.None) where TModel : class {
            return db.SetAdd(key, tModel.ToJson(), flags);
            //db.SetCombine()
        }

        /// <summary>
        /// 返回添加的数量
        /// </summary>
        /// <typeparam name="TModel"></typeparam>
        /// <param name="db"></param>
        /// <param name="key"></param>
        /// <param name="tModels"></param>
        /// <param name="flags"></param>
        /// <returns></returns>
        public static long SetAddModels<TModel>(this IDatabase db, string key, List<TModel> tModels, CommandFlags flags = CommandFlags.None)where TModel:class {
            RedisValue[] redisValues = tModels.Select(t => (RedisValue) t.ToJson()).ToArray();
            return db.SetAdd(key, redisValues, flags);
        }

        public static List<TModel> SetCombineModels<TModel>(this IDatabase db, SetOperation operation, string firstKey, string secondKey, CommandFlags flags = CommandFlags.None)where TModel:class {
            RedisValue[] redisValues = db.SetCombine(operation, firstKey, secondKey, flags);
            return redisValues.Select(value => value.ToModel<TModel>()).ToList();
        }

        public static List<TModel> SetCombineModels<TModel>(this IDatabase db, SetOperation operation, List<string> keys, CommandFlags flags = CommandFlags.None) where TModel:class {
            RedisValue[] redisValues = db.SetCombine(operation, keys.Select(s => (RedisKey) s).ToArray(), flags);
            return redisValues.Select(value => value.ToModel<TModel>()).ToList();
           
        }
        /// <summary>
        /// 判断set中是否存在此实体
        /// </summary>
        /// <typeparam name="TModel"></typeparam>
        /// <param name="db"></param>
        /// <param name="key"></param>
        /// <param name="tModel"></param>
        /// <param name="flags"></param>
        /// <returns></returns>
        public static bool SetContainsModel<TModel>(this IDatabase db, string key, TModel tModel,
            CommandFlags flags = CommandFlags.None) where TModel : class {
            return db.SetContains(key, tModel.ToJson(), flags);
            
        }

        public static List<TModel> SetMembersModels<TModel>(this IDatabase db,string key, CommandFlags flags = CommandFlags.None) where TModel : class {
            RedisValue[] redisValues = db.SetMembers(key, flags);
            return redisValues.Select(value => value.ToModel<TModel>()).ToList();

            
        }

        public static bool SetMoveModel<TModel>(this IDatabase db, string sourceKey, string destinationKey, TModel tModel,
            CommandFlags flags = CommandFlags.None) where TModel : class {
            return db.SetMove(sourceKey, destinationKey, tModel.ToJson(), flags);
 
        }

        public static TModel SetPopModel<TModel>(this IDatabase db, string key, CommandFlags flags = CommandFlags.None)where TModel:class {
            RedisValue redisValue = db.SetPop(key, flags);
            return redisValue.ToModel<TModel>();
            
        }

        public static TModel SetRandomMemberModel<TModel>(this IDatabase db, string key, CommandFlags flags = CommandFlags.None) where TModel:class {
            RedisValue redisValue = db.SetRandomMember(key, flags);
            return redisValue.ToModel<TModel>();
        }

        public static List<TModel> SetRandomMemberModels<TModel>(this IDatabase db, string key, long count, CommandFlags flags = CommandFlags.None) where TModel:class {
            RedisValue[] redisValues = db.SetRandomMembers(key, count, flags);
            return redisValues.Select(value => value.ToModel<TModel>()).ToList();
        }

        public static bool SetRemoveModel<TModel>(this IDatabase db, string key, TModel tModel,
            CommandFlags flags = CommandFlags.None) where TModel : class {
            return db.SetRemove(key, tModel.ToJson(), flags);
        }

        public static long SetRemoveModels<TModel>(this IDatabase db, string key, List<TModel> tModels,
            CommandFlags flags = CommandFlags.None) where TModel : class {
            RedisValue[] redisValues = tModels.Select(t => (RedisValue) t.ToJson()).ToArray();
 
            return db.SetRemove(key, redisValues, flags);
        }

        public static IEnumerable<TModel> SetScanModels<TModel>(this IDatabase db, string key, string pattern, int pageSize, CommandFlags flags)where TModel:class {
            IEnumerable<RedisValue> enumerable = db.SetScan(key, pattern, pageSize, flags);
            IEnumerable<TModel> tModels = enumerable.Select(one => one.ToModel<TModel>());
            return tModels;
        }

        public static List<TModel> SetSortModels<TModel>(this IDatabase db, string key, long skip = 0, long take = -1, Order order = Order.Ascending, SortType sortType = SortType.Numeric, RedisValue by = default(RedisValue), List<TModel> get = null, CommandFlags flags = CommandFlags.None) where TModel : class {
            RedisValue[] redisValues = null;
            if (get != null) {
                redisValues = get.Select(one => (RedisValue)one.ToJson()).ToArray();
            }
            RedisValue[] sortValueses = db.Sort(key, skip, take, order, sortType, @by, redisValues, flags);
            return sortValueses.Select(one => one.ToModel<TModel>()).ToList();


        }

        public static long SetSortAndStoreModels<TModel>(this IDatabase db, string destinationKey, string key,
            long skip = 0, long take = -1, Order order = Order.Ascending, SortType sortType = SortType.Numeric,
            RedisValue by = default(RedisValue), List<TModel> get = null, CommandFlags flags = CommandFlags.None)
            where TModel : class {
            RedisValue[] redisValues = null;
            if (get != null) {
                redisValues = get.Select(one => (RedisValue)one.ToJson()).ToArray();
            }
            return db.SortAndStore(destinationKey, key, skip, take, order, sortType, by, redisValues, flags);
        }






    }
}
