using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Utilities.Result.Util
{
    public class ActionResult
    {
        internal ActionResult()
        {
        }

        public bool WasSuccessful { get; set; }
        public bool NotSuccessful { get; set; }
        public List<string> Errors { get; set; } = new List<string>();
        public List<LinkParameter> Links { get; set; } = new List<LinkParameter>();
        public ErrorCode Code { get; set; }

        public ActionResult AddError(string error)
        {
            Errors.Add(error);
            return this;
        }

        public ActionResult SetErrors(List<string> errors)
        {
            Errors = errors;
            return this;
        }

        public static ActionResult Failed(ErrorCode errorCode = ErrorCode.InternalServerError)
        {
            return new ActionResult()
            {
                WasSuccessful = false,
                NotSuccessful = true,
                Code = errorCode
            };
        }

        public static ActionResult Success()
        {
            return new ActionResult()
            {
                WasSuccessful = true,
                NotSuccessful = false,
                Code = ErrorCode.Ok
            };
        }

        public ActionResult AddLink(LinkParameter link)
        {
            Links.Add(link);
            return this;
        }

        public ActionResult AddLinks(List<LinkParameter> links)
        {
            Links.AddRange(links);
            return this;
        }
    }

    public class ActionResult<T> : ActionResult
    {
        internal ActionResult()
        {

        }

        public static new ActionResult<T> Failed(ErrorCode errorCode = ErrorCode.InternalServerError)
        {
            return new ActionResult<T>()
            {
                WasSuccessful = false,
                NotSuccessful = true,
                Code = errorCode
            };
        }

        public static ActionResult<T> Failed(T data, ErrorCode errorCode = ErrorCode.InternalServerError)
        {
            return new ActionResult<T>()
            {
                WasSuccessful = false,
                NotSuccessful = true,
                Data = data,
                Code = errorCode
            };
        }

        public static ActionResult<T> Success(T output)
        {
            return new ActionResult<T>()
            {
                WasSuccessful = true,
                NotSuccessful = false,
                Data = output,
                Code = ErrorCode.Ok
            };
        }

        public new ActionResult<T> SetErrors(List<string> errors)
        {
            if (errors == null) return this;
            Errors.AddRange(errors);
            return this;
        }

        public new ActionResult<T> AddError(string error)
        {
            Errors.Add(error);
            return this;
        }

        public T Data { get; private set; }
    }
}
