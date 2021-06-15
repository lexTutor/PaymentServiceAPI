using System;
using System.Collections.Generic;
using System.Text;

namespace PaymentService.Application.Commons.CustomExceptions
{

    public class BadRequestException : Exception
    {
        public BadRequestException(string msg) : base(msg)
        {

        }
    }
}
