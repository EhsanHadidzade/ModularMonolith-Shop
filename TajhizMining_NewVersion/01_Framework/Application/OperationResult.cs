using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace _01_Framework.Application
{
    public class OperationResult
    {
        public bool IsSuccedded { get; set; }
        public string? Message { get; set; }
        public OperationResult()
        {
            IsSuccedded = false;
        }

        public OperationResult Succedded(string message = "عملیات با موفقیت انجام شد")
        {
            IsSuccedded = true;
            this.Message = message;
            return this;
        }

        public OperationResult Failed(string message)
        {
            IsSuccedded = false;
            this.Message=message;
            return this;
        }
    }
}
