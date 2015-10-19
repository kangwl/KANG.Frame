using System.Collections.Generic;
using System.Linq;
using StackExchange.Redis;
using KANG.Common;

namespace KANG.Redis.Extension {
    public static class ListExtension {
        /// <summary>
        /// 返回列表长度
        /// </summary>
        /// <typeparam name="TModel"></typeparam>
        /// <param name="db"></param>
        /// <param name="key"></param>
        /// <param name="tModel"></param>
        /// <param name="when"></param>
        /// <param name="flags"></param>
        /// <returns></returns>
        public static long ListLeftPushModel<TModel>(this IDatabase db, string key, TModel tModel,
            When when = When.Always, CommandFlags flags = CommandFlags.None) where TModel : class, new() {
            return db.ListLeftPush(key, tModel.ToJson(), when, flags);
        }

        public static long ListRightPushModel<TModel>(this IDatabase db, string key, TModel tModel,
          When when = When.Always, CommandFlags flags = CommandFlags.None) where TModel : class, new() {
            return db.ListRightPush(key, tModel.ToJson(), when, flags);
        }

        public static long ListLeftPushModels<TModel>(this IDatabase db, string key, List<TModel> tModels,
            CommandFlags flags = CommandFlags.None) where TModel : class, new() {

            RedisValue[] redisValues = tModels.Select(t => (RedisValue)t.ToJson()).ToArray();

            return db.ListLeftPush(key, redisValues, flags);
        }

        public static long ListRightPushModels<TModel>(this IDatabase db, string key, List<TModel> tModels,
            CommandFlags flags = CommandFlags.None) where TModel : class, new() {

            RedisValue[] redisValues = tModels.Select(t => (RedisValue) t.ToJson()).ToArray();

            return db.ListRightPush(key, redisValues, flags);
        }


        /// <summary>
        /// 删除
        /// </summary>
        /// <typeparam name="TModel"></typeparam>
        /// <param name="db"></param>
        /// <param name="key"></param>
        /// <param name="tModel"></param>
        /// <param name="count"></param>
        /// <param name="flags"></param>
        /// <returns></returns>
        public static long ListRemoveModel<TModel>(this IDatabase db, RedisKey key, TModel tModel, long count = 0,
            CommandFlags flags = CommandFlags.None) where TModel : class {

            return db.ListRemove(key, tModel.ToJson(), count, flags);
          
        }

        public static List<TModel> ListRangeModels<TModel>(this IDatabase db, string key, long start = 0, long stop = -1, CommandFlags flags = CommandFlags.None)where TModel:class {
            RedisValue[] redisValues = db.ListRange(key, start, stop, flags);
            List<TModel> models = redisValues.Select(redisValue => redisValue.ToModel<TModel>()).ToList();
            return models;

        }

        /// <summary>
        /// 没找到 tmModelPivot 返回-1
        /// 完成返回列表长度
        /// </summary>
        /// <typeparam name="TModel"></typeparam>
        /// <param name="db"></param>
        /// <param name="key"></param>
        /// <param name="tmModelPivot"></param>
        /// <param name="tmModel"></param>
        /// <param name="flags"></param>
        public static long ListInsertAfterModel<TModel>(this IDatabase db, string key, TModel tmModelPivot,
            TModel tmModel, CommandFlags flags = CommandFlags.None) where TModel : class {

            return db.ListInsertAfter(key, tmModelPivot.ToJson(), tmModel.ToJson(), flags);
        }

        public static long ListInsertBeforeModel<TModel>(this IDatabase db, string key, TModel tmModelPivot,
            TModel tmModel, CommandFlags flags = CommandFlags.None) where TModel : class {
            
            return db.ListInsertBefore(key, tmModelPivot.ToJson(), tmModel.ToJson(), flags);
        }

        public static void ListSetByIndexModel<TModel>(this IDatabase db, string key, long index, TModel tModel, CommandFlags flags = CommandFlags.None) where TModel:class {

            db.ListSetByIndex(key, index, tModel.ToJson(), flags);
        }

        public static TModel ListGetByIndexModel<TModel>(this IDatabase db, string key, long index,
            CommandFlags flags = CommandFlags.None) where TModel : class {
            return db.ListGetByIndex(key, index, flags).ToModel<TModel>();
        }


        public static List<TModel> ListSortModels<TModel>(this IDatabase db,string key, long skip = 0, long take = -1, Order order = Order.Ascending, SortType sortType = SortType.Numeric, RedisValue by = default(RedisValue), List<TModel> get = null, CommandFlags flags = CommandFlags.None) where TModel:class {
            RedisValue[] redisValues = null;
            if (get != null) {
                redisValues = get.Select(one => (RedisValue) one.ToJson()).ToArray();
            }
            RedisValue[] sortValueses = db.Sort(key, skip, take, order, sortType, @by, redisValues, flags);
            return sortValueses.Select(one => one.ToModel<TModel>()).ToList();  
        }

        public static long ListSortAndStoreModels<TModel>(this IDatabase db, string destinationKey, string key,
            long skip = 0, long take = -1, Order order = Order.Ascending, SortType sortType = SortType.Numeric,
            RedisValue by = default(RedisValue), List<TModel> get = null, CommandFlags flags = CommandFlags.None)
            where TModel : class {
            RedisValue[] redisValues = null;
            if (get != null) {
                redisValues = get.Select(one => (RedisValue) one.ToJson()).ToArray();
            }
            return db.SortAndStore(destinationKey, key, skip, take, order, sortType, by, redisValues, flags);
        }






    }
}
