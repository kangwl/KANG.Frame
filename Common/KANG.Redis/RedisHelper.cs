using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using StackExchange.Redis;
using KANG.Common;
using KANG.Redis.Extension;

namespace KANG.Redis {
    /// <summary>
    /// redis操作帮助类
    /// 不建议使用有序列化的方法
    /// </summary>
    public class RedisHelper {

        #region main
        public static string host = "127.0.0.1";
        private static int port = 6379;
        private static string password = "";
        //string host = "192.168.1.78", int port = 6379, string password = ""
        static RedisHelper() {
            options = new ConfigurationOptions {
                AllowAdmin = true,
                EndPoints = { new IPEndPoint(IPAddress.Parse(host), port) },
                Password = password
            };

            ServerEndPoint = options.EndPoints[0];
        }

        private static ConfigurationOptions options { get; set; }

        private static ConnectionMultiplexer connection => ConnectionMultiplexer.Connect(options);


        private static EndPoint ServerEndPoint { get; set; }

        /// <summary>
        /// 数据库操作
        /// </summary>
        private static IDatabase Db => connection.GetDatabase();

        /// <summary>
        /// Server操作
        /// </summary>
        private static IServer Server => connection.GetServer(ServerEndPoint);
        #endregion

        #region common methods
        public static bool SetExpire(string key, DateTime dtExpire) {
            return Db.KeyExpire(key, dtExpire);
        }

        public static bool SetExpire(string key, TimeSpan timeSpan) {
            return Db.KeyExpire(key, timeSpan);
        } 
        #endregion

        #region hash
        public static bool HashSet(string hashKey, Dictionary<string, dynamic> dic) {
          
            return Db.HashSet(dic, hashKey);
        }

        public static bool HashDelete(string key, string field) {
            return Db.HashDelete(key, field); 
        }
        public static bool HashDelete(string key,params string[] fields) {
            long removedCount = Db.HashDelete(key, fields.Select(one => (RedisValue) one).ToArray());
            return removedCount == fields.Length;
        }

        public static double HashIncrement(string key, string field, double val) {
            return Db.HashIncrement(key, field, val);
        }

        public static long HashIncrement(string key, string field, long val=1L) {
            return Db.HashIncrement(key, field, val);
        }

        public static double HashDecrement(string key, string field, double val) {
            return Db.HashDecrement(key, field, val);
        }

        public static long HashDecrement(string key, string field, long val=1L) {
            return Db.HashDecrement(key, field, val);
        }

        public static bool HashExists(string key, string field) {
            return Db.HashExists(key, field);
        }

        public static Dictionary<string, string> HashGetAll(string key) {
            HashEntry[] hashEntries = Db.HashGetAll(key);
            return hashEntries.ToDictionary<HashEntry, string, string>(entry => entry.Name, entry => entry.Value);
        }

        public static IEnumerable<string> HashKeys(string key) {
            RedisValue[] redisValues = Db.HashKeys(key);
            return redisValues.Select(one => one.ToStringExt());
        }

        public static IEnumerable<string> HashValues(string key) {
            RedisValue[] redisValues = Db.HashValues(key);
            return redisValues.Select(one => one.ToStringExt());
        }

        public static long HashLength(string key) {
            return Db.HashLength(key);
        }
 

        public static string HashGet(string hashkey, string hashField) {
            return Db.HashGet(hashkey, hashField);
        }

        public static string[] HashGetMuti(string hashkey, params string[] hashFields) {
            return Db.HashGetMuti(hashkey, hashFields);
        }
        /// <summary>
        /// 根据hashkey获取一条记录
        /// </summary>
        /// <param name="hashkey">hash记录的key</param>
        /// <param name="hashFields">hash一条记录的字段</param>
        /// <returns></returns>
        public static Dictionary<string, string> HashGetDic(string hashkey, params string[] hashFields) {
            Dictionary<string, string> dic = new Dictionary<string, string>();
            RedisValue[] redisValues = Db.HashGet(hashkey, hashFields.Select(one => (RedisValue)one).ToArray());
            for (int i = 0; i < hashFields.Length; i++) {
                string field = hashFields[i];
                string val = redisValues[i];
                dic.Add(field, val);
            }
            return dic;
        }
        #endregion

        #region list
        /// <summary>
        /// 返回列表长度
        /// </summary>
        /// <param name="key"></param>
        /// <param name="value"></param>
        /// <returns></returns>
        public static long ListLeftPush(string key, string value) {
            return Db.ListLeftPush(key, value);
        }

        public static long ListLeftPush(string key, List<string> values) {
            return Db.ListLeftPush(key, values.Select(one => (RedisValue)one).ToArray());
        }

        public static long ListRightPush(string key, string value) {
            return Db.ListRightPush(key, value);
        }

        public static long ListRightPush(string key, List<string> values) {
            return Db.ListRightPush(key, values.Select(one => (RedisValue)one).ToArray());
        }

        public static long ListRemove(string key, string value) {
            return Db.ListRemove(key, value);
        }

        public static IEnumerable<string> ListRange(string key, long start = 0, long stop = -1) {
            RedisValue[] redisValues = Db.ListRange(key, start, stop);
            return redisValues.Select(one => one.ToStringExt());
        }

        public static long ListInsertAfter(string key, string positionVal, string val) {
            return Db.ListInsertAfter(key, positionVal, val);
        }

        public static long ListInsertBefore(string key, string positionVal, string val) {
            return Db.ListInsertBefore(key, positionVal, val);
        }

        public static void ListSetByIndex(string key, long index, string value) {
            Db.ListSetByIndex(key, index, value);
        }

        public static string ListGetByIndex(string key, long index) {
            return Db.ListGetByIndex(key, index);
        }

        public static IEnumerable<string> ListSort(string key, long skip = 0, long take = -1,bool asc=true,bool sortNumeric=true) {
            RedisValue[] redisValues = Db.Sort(key, skip, take, asc ? Order.Ascending : Order.Descending,
                sortNumeric ? SortType.Numeric : SortType.Alphabetic);
            return redisValues.Select(one => one.ToStringExt());
        }
        #endregion

        #region set

        public static bool SetAdd(string key, string value) {
            return Db.SetAdd(key, value);
        }

        /// <summary>
        /// 返回添加的数量
        /// </summary>
        /// <param name="key"></param>
        /// <param name="values"></param>
        /// <returns></returns>
        public static long SetAdd(string key, List<string> values) {
            
            return Db.SetAdd(key, values.Select(one => (RedisValue)one).ToArray());
        }

        public static bool SetRemove(string key, string value) {
            return Db.SetRemove(key, value);
        }

        public static bool SetRemove(string key, List<string> values) {
            long removeCount = Db.SetRemove(key, values.Select(one => (RedisValue) one).ToArray());
           
            return values.Count == removeCount;
        }

        public static bool SetContains(string key, string value) {
            return Db.SetContains(key, value);
        }
        /// <summary>
        /// 获取set长度
        /// </summary>
        /// <param name="key"></param>
        /// <returns></returns>
        public static long SetLength(string key) {
            return Db.SetLength(key);
        }

        public static List<string> SetScan(string key, string parten, int pageSize = 10) {
            IEnumerable<RedisValue> redisValues = Db.SetScan(key, parten, pageSize);
            return redisValues.Select(one => one.ToStringExt()).ToList();
        }

        public static IEnumerable<string> SetSort(string key, long skip = 0, long take = -1, bool asc = true, bool sortNumeric = true) {
            RedisValue[] redisValues = Db.Sort(key, skip, take, asc ? Order.Ascending : Order.Descending,
                sortNumeric ? SortType.Numeric : SortType.Alphabetic);
            return redisValues.Select(one => one.ToStringExt());
        }

        #endregion

        #region sorted set

        public static bool SortedSetAdd(string key,string value,double score) {
            return Db.SortedSetAdd(key, value, score);
        }

        public static bool SortedSetAdd(string key, Dictionary<string, double> dic) {
            SortedSetEntry[] entries = dic.Select(pair => new SortedSetEntry(pair.Key, pair.Value)).ToArray();

            long count = Db.SortedSetAdd(key, entries);
            return count == dic.Count;
        }

        public static double SortedSetDecrement(string key, string member, double value) {
            return Db.SortedSetDecrement(key, member, value);
        }

        public static double SortedSetIncrement(string key, string member, double value) {
            return Db.SortedSetIncrement(key, member, value);
        }

        public static Dictionary<string, double> SortedSetRangeByRankWithScores(string key, long start = 0,
            long stop = -1, bool asc = true) {
            SortedSetEntry[] sortedSetEntries = Db.SortedSetRangeByRankWithScores(key, start, stop,
                asc ? Order.Ascending : Order.Descending);
            Dictionary<string, double> dic = sortedSetEntries.ToDictionary(entry => entry.Element.ToModel<string>(),
                entry => entry.Score);
            return dic;
        }

        public static IEnumerable<string> SortedSetRangeByRank(string key, long start = 0,
            long stop = -1, bool asc = true) {
            RedisValue[] redisValues = Db.SortedSetRangeByRank(key, start, stop,
                asc ? Order.Ascending : Order.Descending);
            return redisValues.Select(one => one.ToStringExt());
        }

        public static IEnumerable<string> SortedSetRangeByScore(string key,bool asc=true, long skip = 0, long take = -1) {
            RedisValue[] redisValues = Db.SortedSetRangeByScore(key, order: asc ? Order.Ascending : Order.Descending,
                skip: skip, take: take);

            return redisValues.Select(one => one.ToStringExt());
        }

        public static Dictionary<string, double> SortedSetRangeByScoreWithScores(string key, bool asc = true, long skip = 0,
            long take = -1) {
            SortedSetEntry[] sortedSetEntries = Db.SortedSetRangeByScoreWithScores(key,
                order: asc ? Order.Ascending : Order.Descending,
                skip: skip, take: take);
            Dictionary<string, double> dic = sortedSetEntries.ToDictionary(entry => entry.Element.ToStringExt(),
                entry => entry.Score);
            return dic;
        }

        public static long? SortedSetRank(string key, string member, bool asc = true) {
            return Db.SortedSetRank(key, member, order:asc?Order.Ascending : Order.Descending);
        }

        public static bool SortedSetRemove(string key, string member) {
            return Db.SortedSetRemove(key, member);
        }

        public static long SortedSetRemove(string key, List<string> members) {
            return Db.SortedSetRemove(key, members.Select(one => (RedisValue)one).ToArray());

        }

        private static RedisValue GetRedisVal(string str) {
            RedisValue redisValue = str;
            return redisValue;
        }

        #endregion

        #region string

        public static bool StringSet(string key, string value,
            TimeSpan? expiry = default(TimeSpan?)) {
            return Db.StringSet(key, value, expiry);
        }

        public static string StringGet(string key) {
            RedisValue redisValue = Db.StringGet(key); 
            return redisValue.ToStringExt();
        } 
        #endregion

    }
}
