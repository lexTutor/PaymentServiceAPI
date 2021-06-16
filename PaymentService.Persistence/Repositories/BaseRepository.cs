using Microsoft.EntityFrameworkCore;
using PaymentService.Application.Contracts;
using PaymentService.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace PaymentService.Persistence.Repositories
{
    public class BaseRepository<T> : IBaseRepository<T> where T : BaseEntity
    {
        private readonly PaymentDbContext _dbContext;
        private readonly DbSet<T> _entity;

        public BaseRepository(PaymentDbContext dbContext)
        {
            _dbContext = dbContext;
            _entity = _dbContext.Set<T>();

        }

        public async Task<T> GetByIdAsync(string id)
        {
            return await _entity.FindAsync(id);
        }

        public async Task<T> AddAsync(T entity)
        {
            await _entity.AddAsync(entity);

            return entity;
        }

        public void Update(T entity)
        {
            _entity.Update(entity);
        }

        public void Delete(T entity)
        {
            _entity.Remove(entity);
        }
    }
}
