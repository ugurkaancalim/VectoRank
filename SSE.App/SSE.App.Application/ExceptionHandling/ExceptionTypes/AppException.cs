using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SSE.App.Application.ExceptionHandling.ExceptionTypes
{
    public class AppException : Exception
    {
        public string ResponseMessage { get; set; }
        public AppException(string responseMessage)
        {
            ResponseMessage = responseMessage;
        }
    }
}
