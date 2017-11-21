using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace FCRM.Web.Results
{
    public class Result
    {
        public static Result Empty = new Result();
        public readonly bool Success;
        public readonly string ErrorMessage;
        protected Result()
        {
            Success = true;
        }
        public Result(string errorMessage)
        {
            ErrorMessage = errorMessage;
            Success = false;
        }
    }
    public class Result<TData> : Result
    {
        public readonly TData Data;
        public Result(TData data)
            : base()
        {
            Data = data;
        }
        public Result(string errorMessage) : base(errorMessage) { }
    }

    public class ListResult<TData> : Result
    {
        public readonly List<TData> List;
        public ListResult(List<TData> list)
            : base()
        {
            List = list;
        }
        public ListResult(string errorMessage) : base(errorMessage) { }
    }
    public class StringResult : Result
    {
        public readonly string Data;
        public StringResult(string Data)
        {
            this.Data = Data;
        }
    }
}