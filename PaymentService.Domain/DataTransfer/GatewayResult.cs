using System;
using System.Collections.Generic;
using System.Text;

namespace PaymentService.Domain.DataTransfer
{
    public class GatewayResult
    {
        public string Id { get; set; } = Guid.NewGuid().ToString();
        public bool Status { get; set; }
        public decimal Amount { get; set; }
    }
}
