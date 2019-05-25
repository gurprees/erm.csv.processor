using System;
using System.Collections.Generic;
using System.Text;

namespace erm.Model
{
    public class Result<T>
    {
        public bool Success { get; set; }
        public T Value { get; set; }
        public string ErrorMessage { get; set; }

        public Result(bool success, T value, string errorMessage)
        {
            this.Success = success;
            this.Value = value;
            this.ErrorMessage = errorMessage;
        }
    }
}
