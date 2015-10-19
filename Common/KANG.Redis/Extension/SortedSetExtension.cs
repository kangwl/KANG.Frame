using System.Collections.Generic;
using System.Linq;
using StackExchange.Redis;
using KANG.Common;

namespace KANG.Redis.Extension {
    public static class SortedSetExtension {

        public static bool SortedSetAddModel<TModel>(this IDatabase db, string key, TModel tModel, double score,
            CommandFlags flags = CommandFlags.None) where TModel : class {
            return db.SortedSetAdd(key, tModel.ToJson(), score, flags);
        }


        public static long SortedSetAddModels<TModel>(this IDatabase db, string key, Dictionary<TModel, double> dic,
            CommandFlags flags = CommandFlags.None) where TModel : class {
            SortedSetEntry[] entries = dic.Select(pair => new SortedSetEntry(pair.Key.ToJson(), pair.Value)).ToArray();

            return db.SortedSetAdd(key, entries, flags);
        }

        //RedisKey key, RedisValue member, double value, CommandFlags flags = CommandFlags.None
        public static double SortedSetDecrementModel<TModel>(this IDatabase db, string key, TModel member, double value,
            CommandFlags flags = CommandFlags.None) where TModel : class {
            return db.SortedSetDecrement(key, member.ToJson(), value, flags);
        }

        public static double SortedSetIncrementModel<TModel>(this IDatabase db, string key, TModel member, double value,
            CommandFlags flags = CommandFlags.None) where TModel : class {
            return db.SortedSetIncrement(key, member.ToJson(), value, flags);
        }

        public static Dictionary<TModel, double> SortedSetRangeByRankWithScoresModels<TModel>(this IDatabase db,
            string key, long start = 0, long stop = -1, Order order = Order.Ascending,
            CommandFlags flags = CommandFlags.None) where TModel : class {
            SortedSetEntry[] sortedSetEntries = db.SortedSetRangeByRankWithScores(key, start, stop, order, flags);
            Dictionary<TModel, double> dic = sortedSetEntries.ToDictionary(entry => entry.Element.ToModel<TModel>(),
                entry => entry.Score);
            return dic;
        }

        public static List<TModel> SortedSetRangeByRankModels<TModel>(this IDatabase db, string key, long start = 0,
            long stop = -1, Order order = Order.Ascending, CommandFlags flags = CommandFlags.None) where TModel : class {
            RedisValue[] redisValues = db.SortedSetRangeByRank(key, start, stop, order, flags);
            return redisValues.Select(one => one.ToModel<TModel>()).ToList();
        }

        public static List<TModel> SortedSetRangeByScoreModels<TModel>(this IDatabase db, string key,
            double start = double.NegativeInfinity, double stop = double.PositiveInfinity,
            Exclude exclude = Exclude.None, Order order = Order.Ascending, long skip = 0, long take = -1,
            CommandFlags flags = CommandFlags.None) where TModel : class {
            RedisValue[] redisValues = db.SortedSetRangeByScore(key, start, stop, exclude, order, skip, take, flags);

            return redisValues.Select(one => one.ToModel<TModel>()).ToList();
        }

        public static Dictionary<TModel, double> SortedSetRangeByScoreWithScoresModels<TModel>(this IDatabase db,
            string key, double start = double.NegativeInfinity, double stop = double.PositiveInfinity,
            Exclude exclude = Exclude.None, Order order = Order.Ascending, long skip = 0, long take = -1,
            CommandFlags flags = CommandFlags.None) where TModel : class {
            SortedSetEntry[] sortedSetEntries = db.SortedSetRangeByScoreWithScores(key, start, stop, exclude, order,
                skip, take, flags);
            Dictionary<TModel, double> dic = sortedSetEntries.ToDictionary(entry => entry.Element.ToModel<TModel>(),
                entry => entry.Score);
            return dic;
        }

        public static long? SortedSetRankModel<TModel>(this IDatabase db, string key, TModel member,
            Order order = Order.Ascending, CommandFlags flags = CommandFlags.None) where TModel : class {
            return db.SortedSetRank(key, member.ToJson(), order, flags);
        }

        public static bool SortedSetRemoveModel<TModel>(this IDatabase db, string key, TModel member, CommandFlags flags = CommandFlags.None) where TModel:class {
           return db.SortedSetRemove(key, member.ToJson(), flags);
        }

        public static long SortedSetRemoveModels<TModel>(this IDatabase db, string key, List<TModel> members,
            CommandFlags flags = CommandFlags.None) where TModel : class {
            return db.SortedSetRemove(key, members.Select(one => (RedisValue) one.ToJson()).ToArray(), flags);
        }



    }
}
