using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Text;

namespace PaymentService.Domain.Common
{
    public class Response<T>
    {
        public Response(string message, T details = default)
        {
            Message = message;
            Data = details;
        }
        public Response()
        {

        }
        public string Message { get; set; }
        public bool IsSuccess { get; set; }
        public T Data { get; set; }
        public IList<Error> Errors { get; set; }

        //Converts the object to Json incase of creating a response object as string
        public override string ToString()
        {
            return JsonConvert.SerializeObject(this);
        }
    }
}
