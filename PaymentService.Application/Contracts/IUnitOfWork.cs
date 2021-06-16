using PaymentService.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace PaymentService.Application.Contracts
{
    public interface IUnitOfWork
    {
        public IBaseRepository<Payment> PaymentRepository { get; }
        public IBaseRepository<PaymentStatus> PaymentStatusRepository { get; }
        Task<bool> SaveChangesAsync();
    }
}
