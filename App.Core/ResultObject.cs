using System;
using System.Collections.Generic;
using System.Text;

namespace App.Core
{
    public class ResultObject<T>
    {
        public T Data { get; set; }
        public string Message { get; set; }
        public string ErrorMessage { get; set; }

    }
}
