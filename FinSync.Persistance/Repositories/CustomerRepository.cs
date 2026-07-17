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

        public async Task<Customer?> UpdateAsync(int customerId, Customer customer)
        {
            var existingCustomer = await _context.Customers.FindAsync(customerId);

            if (existingCustomer == null)
                return null;

            existingCustomer.FirstName = customer.FirstName;
            existingCustomer.MiddleName = customer.MiddleName;
            existingCustomer.LastName = customer.LastName;
            existingCustomer.Gender = customer.Gender;
            existingCustomer.DateOfBirth = customer.DateOfBirth;
            existingCustomer.MobileNumber = customer.MobileNumber;
            existingCustomer.AlternateMobileNumber = customer.AlternateMobileNumber;
            existingCustomer.Email = customer.Email;
            existingCustomer.Address = customer.Address;
            existingCustomer.City = customer.City;
            existingCustomer.State = customer.State;
            existingCustomer.Pincode = customer.Pincode;
            existingCustomer.PanNumber = customer.PanNumber;
            existingCustomer.AadhaarNumber = customer.AadhaarNumber;
            existingCustomer.Occupation = customer.Occupation;
            existingCustomer.CompanyName = customer.CompanyName;
            existingCustomer.AnnualIncome = customer.AnnualIncome;
            existingCustomer.Remarks = customer.Remarks;

            await _context.SaveChangesAsync();

            return existingCustomer;

            await _context.SaveChangesAsync();

            return existingCustomer;
        }

        public async Task<bool> DeleteAsync(int customerId)
        {
            var customer = await _context.Customers.FindAsync(customerId);

            if (customer == null)
                return false;

            _context.Customers.Remove(customer);

            await _context.SaveChangesAsync();

            return true;
        }
    }
}