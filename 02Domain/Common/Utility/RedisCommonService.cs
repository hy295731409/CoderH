//using System;
//using System.Collections.Generic;
//using JSHC.IFramework.Base;
//using JSHC.IFramework.Ioc;
//using JSHC.IFramework.Utility.Extension;
//using JSHC.OrderingSystem.Common.Model;
//using Microsoft.Extensions.Options;
//using Newtonsoft.Json;
//using StackExchange.Redis;

//namespace JSHC.OrderingSystem.Common.Utility
//{
//    /// <summary>
//    /// Redis缓存服务
//    /// </summary>
//    public interface IRedisCommonService : IDependency
//    {
//        #region 写入
//        /// <summary>
//        /// 写入
//        /// </summary>
//        /// <typeparam name="T"></typeparam>
//        /// <param name="key"></param>
//        /// <param name="value"></param>
//        /// <param name="expiresIn"></param>
//        /// <returns></returns>
//        bool Set<T> (string key, T value, TimeSpan expiresIn);

//        #endregion

//        #region 读取
//        /// <summary>
//        /// 获取
//        /// </summary>
//        /// <typeparam name="T"></typeparam>
//        /// <param name="key"></param>
//        /// <returns></returns>
//        T Get<T> (string key);

//        #endregion

//        #region 删除

//        /// <summary>
//        /// 删除
//        /// </summary>
//        /// <param name="key"></param>
//        /// <returns></returns>
//        bool Del (string key);

//        #endregion

//    }
//    /// <summary>
//    /// Redis缓存服务
//    /// </summary>
//    public class RedisCommonService : IRedisCommonService
//    {

//        /// <summary>
//        /// 单例模式
//        /// </summary>
//        /// <returns></returns>
//        public static RedisCommon Default => new RedisCommon ();
//        #region 写入
//        /// <summary>
//        /// 写入
//        /// </summary>
//        /// <param name="key"></param>
//        /// <param name="value"></param>
//        /// <param name="expiresIn"></param>
//        /// <typeparam name="T"></typeparam>
//        /// <returns></returns>
//        public bool Set<T> (string key, T value, TimeSpan expiresIn)
//        {
//            return Default.SetStringKey (key, value, expiresIn);
//        }

//        #endregion

//        #region 读取
//        /// <summary>
//        /// 读取
//        /// </summary>
//        /// <param name="key"></param>
//        /// <typeparam name="T"></typeparam>
//        /// <returns></returns>
//        public T Get<T> (string key)
//        {
//            return Default.GetStringKey<T> (key);
//        }

//        #endregion

//        #region 删除
//        /// <summary>
//        /// 删除
//        /// </summary>
//        /// <param name="key"></param>
//        /// <returns></returns>
//        public bool Del (string key)
//        {
//            return Default.KeyDelete (key);
//        }

//        #endregion

//    }

//    /// <summary>
//    /// Redis操作类
//    ///老版用的是ServiceStack.Redis。
//    ///Net Core使用StackExchange.Redis的nuget包
//    /// </summary>
//    public class RedisCommon
//    {
//        //redis数据库连接字符串
//        private string _conn = IocManager.Resolve<IOptionsMonitor<AppSettings>> ().CurrentValue.RedisDomain;
//        private int _db = 0;
//        //静态变量 保证各模块使用的是不同实例的相同链接
//        private static ConnectionMultiplexer _connection;
//        /// <summary>
//        /// 构造函数
//        /// </summary>
//        public RedisCommon ()
//        { }
//        /// <summary>
//        /// 构造函数
//        /// </summary>
//        /// <param name="db"></param>
//        /// <param name="connectStr"></param>
//        public RedisCommon (int db, string connectStr)
//        {
//            _conn = connectStr;
//            _db = db;
//        }
//        ///

//        /// 缓存数据库，数据库连接
//        ///
//        public ConnectionMultiplexer CacheConnection
//        {
//            get
//            {
//                try
//                {
//                    if (_connection == null || !_connection.IsConnected)
//                    {
//                        _connection = new Lazy<ConnectionMultiplexer> (() => ConnectionMultiplexer.Connect (_conn)).Value;
//                    }
//                }
//                catch (Exception ex)
//                {
//                    return null;
//                }
//                return _connection;
//            }
//        }
//        /// <summary>
//        /// 缓存数据库
//        /// </summary>
//        /// <returns></returns>
//        public IDatabase CacheRedis => CacheConnection.GetDatabase (_db);
//        #region --KEY/VALUE存取--
//        /// <summary>
//        /// 单条存值
//        /// </summary>
//        /// <param name="key">key</param>
//        /// <param name="value">The value.</param>
//        /// <returns><c>true</c> if XXXX, <c>false</c> otherwise.</returns>
//        public bool StringSet (string key, string value)
//        {
//            return CacheRedis.StringSet (key, value);
//        }
//        /// <summary>
//        /// 保存单个key value
//        /// </summary>
//        /// <param name="key">Redis Key</param>
//        /// <param name="value">保存的值</param>
//        /// <param name="expiry">过期时间</param>
//        /// <returns></returns>
//        public bool StringSet (string key, string value, TimeSpan? expiry = default (TimeSpan?))
//        {
//            return CacheRedis.StringSet (key, value, expiry);
//        }
//        /// <summary>
//        /// 保存多个key value
//        /// </summary>
//        /// <param name="arr">key</param>
//        /// <returns></returns>
//        public bool StringSet (KeyValuePair<RedisKey, RedisValue>[] arr)
//        {
//            return CacheRedis.StringSet (arr);
//        }
//        /// <summary>
//        /// 批量存值
//        /// </summary>
//        /// <param name="keysStr">key</param>
//        /// <param name="valuesStr">The value.</param>
//        /// <returns><c>true</c> if XXXX, <c>false</c> otherwise.</returns>
//        public bool StringSetMany (string[] keysStr, string[] valuesStr)
//        {
//            var count = keysStr.Length;
//            var keyValuePair = new KeyValuePair<RedisKey, RedisValue>[count];
//            for (int i = 0; i < count; i++)
//            {
//                keyValuePair[i] = new KeyValuePair<RedisKey, RedisValue> (keysStr[i], valuesStr[i]);
//            }

//            return CacheRedis.StringSet (keyValuePair);
//        }

//        /// <summary>
//        /// 保存一个对象
//        /// </summary>
//        /// <typeparam name="T"></typeparam>
//        /// <param name="key"></param>
//        /// <param name="obj"></param>
//        /// <returns></returns>
//        public bool SetStringKey<T> (string key, T obj, TimeSpan? expiry = default (TimeSpan?))
//        {
//            return CacheRedis.StringSet (key, obj.ToJson(), expiry);
//        }
//        /// <summary>
//        /// 追加值
//        /// </summary>
//        /// <param name="key"></param>
//        /// <param name="value"></param>
//        public void StringAppend (string key, string value)
//        {
//            ////追加值，返回追加后长度
//            long appendlong = CacheRedis.StringAppend (key, value);
//        }

//        /// <summary>
//        /// 获取单个key的值
//        /// </summary>
//        /// <param name="key">Redis Key</param>
//        /// <returns></returns>
//        public RedisValue GetStringKey (string key)
//        {
//            return CacheRedis.StringGet (key);
//        }
//        /// <summary>
//        /// 根据Key获取值
//        /// </summary>
//        /// <param name="key">键值</param>
//        /// <returns>System.String.</returns>
//        public string StringGet (string key)
//        {
//            try
//            {
//                return CacheRedis.StringGet (key);
//            }
//            catch (Exception ex)
//            {
//                return null;
//            }
//        }

//        /// <summary>
//        /// 获取多个Key
//        /// </summary>
//        /// <param name="listKey">Redis Key集合</param>
//        /// <returns></returns>
//        public RedisValue[] GetStringKey (List<RedisKey> listKey)
//        {
//            return CacheRedis.StringGet (listKey.ToArray ());
//        }
//        /// <summary>
//        /// 批量获取值
//        /// </summary>
//        public string[] StringGetMany (string[] keyStrs)
//        {
//            var count = keyStrs.Length;
//            var keys = new RedisKey[count];
//            var addrs = new string[count];

//            for (var i = 0; i < count; i++)
//            {
//                keys[i] = keyStrs[i];
//            }
//            try
//            {

//                var values = CacheRedis.StringGet (keys);
//                for (var i = 0; i < values.Length; i++)
//                {
//                    addrs[i] = values[i];
//                }
//                return addrs;
//            }
//            catch (Exception ex)
//            {
//                return null;
//            }
//        }
//        /// <summary>
//        /// 获取一个key的对象
//        /// </summary>
//        /// <typeparam name="T"></typeparam>
//        /// <param name="key"></param>
//        /// <returns></returns>
//        public T GetStringKey<T> (string key)
//        {
//            try
//            {

//                return JsonConvert.DeserializeObject<T> (CacheRedis.StringGet (key));
//            }
//            catch (System.Exception)
//            {

//                return default (T);
//            }
//        }

//        #endregion

//        #region --删除设置过期--
//        /// <summary>
//        /// 删除单个key
//        /// </summary>
//        /// <param name="key">redis key</param>
//        /// <returns>是否删除成功</returns>
//        public bool KeyDelete (string key)
//        {
//            return CacheRedis.KeyDelete (key);
//        }
//        /// <summary>
//        /// 删除多个key
//        /// </summary>
//        /// <param name="keys">rediskey</param>
//        /// <returns>成功删除的个数</returns>
//        public long KeyDelete (RedisKey[] keys)
//        {
//            return CacheRedis.KeyDelete (keys);
//        }
//        /// <summary>
//        /// 判断key是否存储
//        /// </summary>
//        /// <param name="key">redis key</param>
//        /// <returns></returns>
//        public bool KeyExists (string key)
//        {
//            return CacheRedis.KeyExists (key);
//        }
//        /// <summary>
//        /// 重新命名key
//        /// </summary>
//        /// <param name="key">就的redis key</param>
//        /// <param name="newKey">新的redis key</param>
//        /// <returns></returns>
//        public bool KeyRename (string key, string newKey)
//        {
//            return CacheRedis.KeyRename (key, newKey);
//        }
//        /// <summary>
//        /// 删除hasekey
//        /// </summary>
//        /// <param name="key"></param>
//        /// <param name="hashField"></param>
//        /// <returns></returns>
//        public bool HaseDelete (RedisKey key, RedisValue hashField)
//        {
//            return CacheRedis.HashDelete (key, hashField);
//        }
//        /// <summary>
//        /// 移除hash中的某值
//        /// </summary>
//        /// <typeparam name="T"></typeparam>
//        /// <param name="key"></param>
//        /// <param name="dataKey"></param>
//        /// <returns></returns>
//        public bool HashRemove (string key, string dataKey)
//        {
//            return CacheRedis.HashDelete (key, dataKey);
//        }
//        /// <summary>
//        /// 设置缓存过期
//        /// </summary>
//        /// <param name="key"></param>
//        /// <param name="datetime"></param>
//        public void SetExpire (string key, DateTime datetime)
//        {
//            CacheRedis.KeyExpire (key, datetime);
//        }
//        #endregion

//    }
//}