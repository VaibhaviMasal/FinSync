using FinSync.Domain.Entities;
using FinSync.Persistence.Context;
using Microsoft.EntityFrameworkCore;
using FinSync.Application.Features.Customers.Interfaces;

namespace FinSync.Persistence.Repositories
{
    public class CustomerRepository : ICustomerRepository
    {
        private readonly FinSyncDbContext _context;

        public CustomerRepository(FinSyncDbContext context)
        {
            _context = context;
        }

        public async Task<Customer> AddAsync(Customer customer)
        {
            await _context.Customers.AddAsync(customer);
            await _context.SaveChangesAsync();
            return customer;
        }

        public async Task<IEnumerable<Customer>> GetAllAsync()
        {
            return await _context.Customers.ToListAsync();
        }

        public async Task<Customer?> GetByIdAsync(int customerId)
        {
            return await _context.Customers
               .AsNoTracking()
               .FirstOrDefaultAsync(c => c.CustomerId == customerId);
        }

        public Task<Customer> UpdateAsync(Customer customer)
        {
            throw new NotImplementedException();
        }

        public Task<bool> DeleteAsync(int customerId)
        {
            throw new NotImplementedException();
        }
    }
}