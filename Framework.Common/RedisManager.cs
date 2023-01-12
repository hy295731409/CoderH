using System;
using System.Collections.Generic;
using System.Text;
using System.Threading;
using ServiceStack.Redis;

namespace Framework.Common
{
    public class RedisManager
    {

        private static readonly string ScriptSetIfAbsent = "return redis.call('SET',KEYS[1],ARGV[1],'EX',ARGV[2],'NX')";

        private static readonly string ScriptDeleteIfEqualValue = @"if redis.call('GET',KEYS[1]) == ARGV[1] then
        return redis.call('DEL',KEYS[1])
        else
        return 'FALSE'
        end";

        /// <summary>
        /// 加锁
        /// </summary>
        /// <param name="key">锁key</param>
        /// <param name="lockToken">锁令牌，用于释放锁</param>
        /// <param name="lockExpirySeconds">锁自动超时时间(秒)</param>
        /// <param name="waitLockSeconds">等待锁时间(秒)</param>
        /// <returns>加锁成功</returns>
        public bool Lock(string key, out string lockToken, int lockExpirySeconds = 10, double waitLockSeconds = 0)
        {
            int waitIntervalMs = 1000;
            string lockKey = GetLockKey(key);
            DateTime begin = DateTime.Now;
            string uuid = Guid.NewGuid().ToString();

            //循环获取取锁
            while (true)
            {
                string result;
                using (var client = GetNativeClient())
                {
                    //返回SET操作结果，为OK时成功
                    result = client.EvalStr(ScriptSetIfAbsent, 1,
                        System.Text.Encoding.UTF8.GetBytes(lockKey),
                        System.Text.Encoding.UTF8.GetBytes(uuid),
                        System.Text.Encoding.UTF8.GetBytes(lockExpirySeconds.ToString()));
                }

                if (result == "OK")
                {
                    lockToken = uuid;
                    return true;
                }

                //超过等待时间，则不再等待
                if ((DateTime.Now - begin).TotalSeconds >= waitLockSeconds) break;
                Thread.Sleep(waitIntervalMs);
            }
            lockToken = null;
            return false;
        }

        private RedisClient GetNativeClient()
        {
            throw new NotImplementedException();
        }

        private string GetLockKey(string key)
        {
            throw new NotImplementedException();
        }


        /// <summary>
        /// 释放锁，执行完代码以后调用
        /// </summary>
        /// <param name="key">锁Key</param>
        /// <param name="lockToken">锁令牌</param>
        /// <returns>释放锁成功</returns>
        public bool DelLock(string key, string lockToken)
        {
            if (string.IsNullOrWhiteSpace(lockToken))
            {
                throw new Exception("参数lockToken不能为空");
            }

            string lockKey = GetLockKey(key);
            using (var client = GetNativeClient())
            {
                //返回删除的行数，为1时成功
                string result = client.EvalStr(ScriptDeleteIfEqualValue, 1,
                    System.Text.Encoding.UTF8.GetBytes(lockKey),
                    System.Text.Encoding.UTF8.GetBytes(lockToken));
                return result == "1";
            }
        }


        public void test(string key)
        {
            if (Lock(key, out string tokenLock))
            {
                try
                {
                    IRedisClient rdsclient = null;
                    try
                    {

                    }
                    finally
                    {
                        rdsclient?.Dispose();
                    }
                }
                finally
                {
                    DelLock(key, tokenLock);
                }
            }
        }
    }
}
