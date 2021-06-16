using PaymentService.Domain.Common;
using System;
using System.Collections.Generic;
using System.Text;

namespace PaymentService.Domain.DataTransfer
{
    public class PaymentResultDTO
    {
        public string PaymentId { get; set; }
        public PaymentStatusType PaymentStatus { get; set; }
    }
}
