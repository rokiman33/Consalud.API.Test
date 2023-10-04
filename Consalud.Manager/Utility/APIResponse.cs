using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Consalud.Manager.Utility
{
    public class APIResponse
    {
        public APIResponse(ResponseCode code, string message,object data=null) 
        {
            Code = code;
            Message = message;
            Result = data;
        }

        public ResponseCode Code { get; set; }
        public string Message { get; set; }
        public object Result { get; set; }
    }

    public enum ResponseCode
    {
        ERROR=0,
        SUCCESS=1
    }
}
