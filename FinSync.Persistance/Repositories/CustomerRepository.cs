using FinSync.Domain.Entities;
using FinSync.Domain.Interfaces;
using FinSync.Persistence.Context; 
using Microsoft.EntityFrameworkCore;


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

        public Task<bool> DeleteAsync(int customerId)
        {
            throw new NotImplementedException();
        }

        public Task<IEnumerable<Customer>> GetAllAsync()
        {
            throw new NotImplementedException();
        }

        public Task<Customer?> GetByIdAsync(int customerId)
        {
            throw new NotImplementedException();
        }

        public Task<Customer> UpdateAsync(Customer customer)
        {
            throw new NotImplementedException();
        }
    }

}
