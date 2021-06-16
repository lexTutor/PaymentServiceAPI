using AutoMapper;
using PaymentService.Domain.DataTransfer;
using PaymentService.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Text;

namespace PaymentService.Application.Commons.Mapping
{
    public class AutoMapperProfiles: Profile
    {
        public AutoMapperProfiles()
        {
            //source => destination
            CreateMap<RecievePaymentDto, Payment>();
            CreateMap<Payment, PaymentResultDTO>();
        }
    }
}
