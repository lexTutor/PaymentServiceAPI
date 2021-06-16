using Microsoft.Extensions.DependencyInjection;
using PaymentService.Application.Commons.Mapping;
using PaymentService.Application.Contracts;
using PaymentService.Infrastructure.Agreements;
using PaymentService.Infrastructure.Subsidiaries.SubAgreements;
using PaymentService.Infrastructure.Subsidiaries.SubContracts;
using PaymentService.Persistence.Repositories;

namespace PaymentService.Infrastructure
{
    public static class DIConfigurations
    {
        public static void AddServiceRegisteration(this IServiceCollection services)
        {
            services.AddScoped<IPaymentService, PaymentServiceExecutor>();
            services.AddScoped(typeof(IBaseRepository<>), typeof(BaseRepository<>));
            services.AddScoped<IUnitOfWork, UnitOfWork>();
            services.AddScoped<IPremiumPaymentGateway, PremiumPaymentGateway>();
            services.AddScoped<IExpensivePaymentGateway, ExpensivePaymentGateway>();
            services.AddScoped<ICheapPaymentGateway, CheapPaymentGateway>();
            services.AddAutoMapper(typeof(AutoMapperProfiles));
        }
    }
}
