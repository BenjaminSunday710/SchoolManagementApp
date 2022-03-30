using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Utilities
{
    public enum ErrorCode
    {
        BadRequest = 400,
        UnAuthorized = 401,
        NotFound = 404,
        InternalServerError = 500,
        Ok = 200
    }
}
