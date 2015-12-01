using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using KANG.Common;
using KANG.Redis.Extension;
using StackExchange.Redis;

namespace KANG.Redis {
    public partial class RedisCommon : IDisposable {

        public RedisCommon(string host="127.0.0.1", int port = 6379, string password = "") {
            options = new ConfigurationOptions {
                AllowAdmin = true,
                EndPoints = {new IPEndPoint(IPAddress.Parse(host), port)},
                Password = password
            };
        }

        private ConfigurationOptions options { get; set; }

        private ConnectionMultiplexer connection {
            get { return ConnectionMultiplexer.Connect(options); }
        }
        /// <summary>
        /// 数据库操作
        /// </summary>
        private IDatabase Db {
            get { return connection.GetDatabase(); }
        }


        #region common methods
        public  bool SetExpire(string key, DateTime dtExpire) {
            return Db.KeyExpire(key, dtExpire);
        }

        public  bool SetExpire(string key, TimeSpan timeSpan) {
            return Db.KeyExpire(key, timeSpan);
        }
        #endregion

        #region hash

        public bool HashSet(string hashKey, Dictionary<string, dynamic> dic) {
            Db.HashSet(hashKey, dic.Select(pair => new HashEntry(pair.Key, pair.Value)).ToArray());
            return true;
        }

        public  bool HashDelete(string key, string field) {
            return Db.HashDelete(key, field);
        }
        public  bool HashDelete(string key, params string[] fields) {
            long removedCount = Db.HashDelete(key, fields.Select(one => (RedisValue)one).ToArray());
            return removedCount == fields.Length;
        }

        public  double HashIncrement(string key, string field, double val) {
            return Db.HashIncrement(key, field, val);
        }

        public  long HashIncrement(string key, string field, long val = 1L) {
            return Db.HashIncrement(key, field, val);
        }

        public  double HashDecrement(string key, string field, double val) {
            return Db.HashDecrement(key, field, val);
        }

        public  long HashDecrement(string key, string field, long val = 1L) {
            return Db.HashDecrement(key, field, val);
        }

        public  bool HashExists(string key, string field) {
            return Db.HashExists(key, field);
        }

        public  Dictionary<string, string> HashGetAll(string key) {
            HashEntry[] hashEntries = Db.HashGetAll(key);
            return hashEntries.ToDictionary<HashEntry, string, string>(entry => entry.Name, entry => entry.Value);
        }

        public  IEnumerable<string> HashKeys(string key) {
            RedisValue[] redisValues = Db.HashKeys(key);
            return redisValues.Select(one => one.ToStringExt());
        }

        public  IEnumerable<string> HashValues(string key) {
            RedisValue[] redisValues = Db.HashValues(key);
            return redisValues.Select(one => one.ToStringExt());
        }

        public  long HashLength(string key) {
            return Db.HashLength(key);
        }


        public  string HashGet(string hashkey, string hashField) {
            return Db.HashGet(hashkey, hashField);
        }

        public  string[] HashGetMuti(string hashkey, params string[] hashFields) {
            return Db.HashGetMuti(hashkey, hashFields);
        }
        /// <summary>
        /// 根据hashkey获取一条记录
        /// </summary>
        /// <param name="hashkey">hash记录的key</param>
        /// <param name="hashFields">hash一条记录的字段</param>
        /// <returns></returns>
        public  Dictionary<string, string> HashGetDic(string hashkey, params string[] hashFields) {
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
        public  long ListLeftPush(string key, string value) {
            return Db.ListLeftPush(key, value);
        }

        public  long ListLeftPush(string key, List<string> values) {
            return Db.ListLeftPush(key, values.Select(one => (RedisValue)one).ToArray());
        }

        public  long ListRightPush(string key, string value) {
            return Db.ListRightPush(key, value);
        }

        public  long ListRightPush(string key, List<string> values) {
            return Db.ListRightPush(key, values.Select(one => (RedisValue)one).ToArray());
        }

        public  long ListRemove(string key, string value) {
            return Db.ListRemove(key, value);
        }

        public  IEnumerable<string> ListRange(string key, long start = 0, long stop = -1) {
            RedisValue[] redisValues = Db.ListRange(key, start, stop);
            return redisValues.Select(one => one.ToStringExt());
        }

        public  long ListInsertAfter(string key, string positionVal, string val) {
            return Db.ListInsertAfter(key, positionVal, val);
        }

        public  long ListInsertBefore(string key, string positionVal, string val) {
            return Db.ListInsertBefore(key, positionVal, val);
        }

        public  void ListSetByIndex(string key, long index, string value) {
            Db.ListSetByIndex(key, index, value);
        }

        public  string ListGetByIndex(string key, long index) {
            return Db.ListGetByIndex(key, index);
        }

        public  IEnumerable<string> ListSort(string key, long skip = 0, long take = -1, bool asc = true, bool sortNumeric = true) {
            RedisValue[] redisValues = Db.Sort(key, skip, take, asc ? Order.Ascending : Order.Descending,
                sortNumeric ? SortType.Numeric : SortType.Alphabetic);
            return redisValues.Select(one => one.ToStringExt());
        }
        #endregion

        #region set

        public  bool SetAdd(string key, string value) {
            return Db.SetAdd(key, value);
        }

        /// <summary>
        /// 返回添加的数量
        /// </summary>
        /// <param name="key"></param>
        /// <param name="values"></param>
        /// <returns></returns>
        public  long SetAdd(string key, List<string> values) {

            return Db.SetAdd(key, values.Select(one => (RedisValue)one).ToArray());
        }

        public  bool SetRemove(string key, string value) {
            return Db.SetRemove(key, value);
        }

        public  bool SetRemove(string key, List<string> values) {
            long removeCount = Db.SetRemove(key, values.Select(one => (RedisValue)one).ToArray());

            return values.Count == removeCount;
        }

        public  bool SetContains(string key, string value) {
            return Db.SetContains(key, value);
        }
        /// <summary>
        /// 获取set长度
        /// </summary>
        /// <param name="key"></param>
        /// <returns></returns>
        public  long SetLength(string key) {
            return Db.SetLength(key);
        }

        public  List<string> SetScan(string key, string parten, int pageSize = 10) {
            IEnumerable<RedisValue> redisValues = Db.SetScan(key, parten, pageSize);
            return redisValues.Select(one => one.ToStringExt()).ToList();
        }

        public  IEnumerable<string> SetSort(string key, long skip = 0, long take = -1, bool asc = true, bool sortNumeric = true) {
            RedisValue[] redisValues = Db.Sort(key, skip, take, asc ? Order.Ascending : Order.Descending,
                sortNumeric ? SortType.Numeric : SortType.Alphabetic);
            return redisValues.Select(one => one.ToStringExt());
        }

        #endregion

        #region sorted set

        public  bool SortedSetAdd(string key, string value, double score) {
            return Db.SortedSetAdd(key, value, score);
        }

        public  bool SortedSetAdd(string key, Dictionary<string, double> dic) {
            SortedSetEntry[] entries = dic.Select(pair => new SortedSetEntry(pair.Key, pair.Value)).ToArray();

            long count = Db.SortedSetAdd(key, entries);
            return count == dic.Count;
        }

        public  double SortedSetDecrement(string key, string member, double value) {
            return Db.SortedSetDecrement(key, member, value);
        }

        public  double SortedSetIncrement(string key, string member, double value) {
            return Db.SortedSetIncrement(key, member, value);
        }

        public  Dictionary<string, double> SortedSetRangeByRankWithScores(string key, long start = 0,
            long stop = -1, bool asc = true) {
            SortedSetEntry[] sortedSetEntries = Db.SortedSetRangeByRankWithScores(key, start, stop,
                asc ? Order.Ascending : Order.Descending);
            Dictionary<string, double> dic = sortedSetEntries.ToDictionary(entry => entry.Element.ToModel<string>(),
                entry => entry.Score);
            return dic;
        }

        public  IEnumerable<string> SortedSetRangeByRank(string key, long start = 0,
            long stop = -1, bool asc = true) {
            RedisValue[] redisValues = Db.SortedSetRangeByRank(key, start, stop,
                asc ? Order.Ascending : Order.Descending);
            return redisValues.Select(one => one.ToStringExt());
        }

        public  IEnumerable<string> SortedSetRangeByScore(string key, bool asc = true, long skip = 0, long take = -1) {
            RedisValue[] redisValues = Db.SortedSetRangeByScore(key, order: asc ? Order.Ascending : Order.Descending,
                skip: skip, take: take);

            return redisValues.Select(one => one.ToStringExt());
        }

        public  Dictionary<string, double> SortedSetRangeByScoreWithScores(string key, bool asc = true, long skip = 0,
            long take = -1) {
            SortedSetEntry[] sortedSetEntries = Db.SortedSetRangeByScoreWithScores(key,
                order: asc ? Order.Ascending : Order.Descending,
                skip: skip, take: take);
            Dictionary<string, double> dic = sortedSetEntries.ToDictionary(entry => entry.Element.ToStringExt(),
                entry => entry.Score);
            return dic;
        }

        public  long? SortedSetRank(string key, string member, bool asc = true) {
            return Db.SortedSetRank(key, member.ToJson(), order: asc ? Order.Ascending : Order.Descending);
        }

        public  bool SortedSetRemove(string key, string member) {
            return Db.SortedSetRemove(key, member);
        }

        public  long SortedSetRemove(string key, List<string> members) {
            return Db.SortedSetRemove(key, members.Select(one => (RedisValue)one).ToArray());

        }

        #endregion

        #region string

        public  bool StringSet(string key, string value,
            TimeSpan? expiry = default(TimeSpan?)) {
            return Db.StringSet(key, value, expiry);
        }

        public  string StringGet(string key) {
            RedisValue redisValue = Db.StringGet(key);
            return redisValue.ToStringExt();
        }
        #endregion

        #region sub/pub

        /// <summary>
        /// 订阅消息
        /// </summary>
        /// <param name="channelName">订阅的频道</param>
        /// <param name="handlerAction">回调委托</param>
        public void Subscribe(string channelName, Action<string, dynamic> handlerAction) {
            ISubscriber subscriber = connection.GetSubscriber();
            subscriber.Subscribe(channelName, (channel, value) => {
                handlerAction(channelName, value);
            });
        }

        /// <summary>
        /// 发布消息
        /// </summary>
        /// <param name="channelName">订阅的频道</param>
        /// <param name="message">消息内容</param>
        /// <returns></returns>
        public long Publish(string channelName, dynamic message) {
            ISubscriber subscriber = connection.GetSubscriber();
            return subscriber.Publish(channelName, message);
        }


        #endregion

        public void Dispose() {
           connection.Dispose();
        }
    }
}
