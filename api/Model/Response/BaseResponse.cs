using System;
using System.Collections.Generic;
using System.Text;

namespace Model.Core.Response
{
    public class BaseResponse
    {
        public int code { get; set; }
        public string message { get; set; }
    }
}
