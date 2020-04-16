using System;
using System.Collections.Generic;
using System.Text;

namespace Framework.DB.Base
{
    [Serializable]
    public class Result
    {
        public bool IsSuccess { get; set; }
        public string ErrorCode { get; set; }
        public string ErrorMsg { get; set; }
        public DateTime ServiceTime { get; set; }

        public static Result Success()
        {
            return new Result()
            {
                IsSuccess = true,
                ServiceTime = DateTime.Now
            };
        }
        public static Result Error(string errormsg, string errorcode = null)
        {
            return new Result()
            {
                ErrorCode = errorcode,
                ErrorMsg = errormsg,
                IsSuccess = false,
                ServiceTime = DateTime.Now
            };
        }
    }


    public class Result<T> : Result
    {

        public T Data { get; set; }
        public static Result<T> Success(T data)
        {
            return new Result<T>()
            {
                IsSuccess = true,
                ServiceTime = DateTime.Now,
                Data = data
            };
        }
        public new static Result<T> Success()
        {
            return new Result<T>()
            {
                IsSuccess = true,
                ServiceTime = DateTime.Now
            };
        }

        public new static Result<T> Error(string errormsg, string errorcode = null)
        {
            return new Result<T>()
            {
                ErrorCode = errorcode,
                ErrorMsg = errormsg,
                IsSuccess = false,
                ServiceTime = DateTime.Now
            };
        }
    }
}
