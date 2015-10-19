using System;
using StackExchange.Redis;
using KANG.Common;

namespace KANG.Redis.Extension {
    public static class StringExtension {

        public static bool StringSetModel<TModel>(this IDatabase db, string key, TModel tModel,
            TimeSpan? expiry = default(TimeSpan?), When when = When.Always, CommandFlags flags = CommandFlags.None)
            where TModel : class, new() {
            string json = tModel.ToJson();
            return db.StringSet(key, json, expiry, when, flags);
        }

        public static TModel StringGetModel<TModel>(this IDatabase db, string key,
            CommandFlags flags = CommandFlags.None) where TModel : class {

            RedisValue redisValue = db.StringGet(key, flags);

            return redisValue.ToModel<TModel>();
        }
        


    }
}
