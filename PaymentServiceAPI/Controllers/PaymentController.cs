using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.DependencyInjection;
using PaymentService.Application.Contracts;
using PaymentService.Domain.DataTransfer;
using System;
using System.Threading.Tasks;

namespace PaymentServiceAPI.Controllers
{
    [ApiController]
    [Route("api/v1/[controller]")]

    public class PaymentController : ControllerBase
    {
        private readonly IPaymentService _paymentService;

        public PaymentController(IServiceProvider serviceProvider)
        {
          _paymentService =  serviceProvider.GetRequiredService<IPaymentService>();
        }
        public async Task<IActionResult> MakePayment(RecievePaymentDto model)
        {
            return Ok(await _paymentService.MakePayment(model));
        }
    }
}
