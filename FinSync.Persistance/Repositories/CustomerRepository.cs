using FinSync.Application.Features.Customers.DTOs;
using FinSync.Application.Features.Customers.Interfaces;
using FinSync.Domain.Entities;
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

        public async Task<IEnumerable<Customer>> SearchAsync(string keyword)
        {
            keyword = keyword.Trim().ToLower();

            return await _context.Customers
                .Where(c =>
                    c.FirstName.ToLower().Contains(keyword) ||
                    c.LastName.ToLower().Contains(keyword) ||
                    c.MobileNumber.Contains(keyword) ||
                    c.PanNumber.ToLower().Contains(keyword) ||
                    c.AadhaarNumber.Contains(keyword) ||
                    c.Email.ToLower().Contains(keyword) ||
                    c.City.ToLower().Contains(keyword) ||
                    c.Occupation.ToLower().Contains(keyword) ||
                    c.CompanyName.ToLower().Contains(keyword))
                .ToListAsync();
        }

        public async Task<IEnumerable<Customer>> GetAllAsync(CustomerQueryParametersDto queryParameters)
        {
            var query = _context.Customers.AsQueryable();

            // Sorting
            switch (queryParameters.SortBy.ToLower())
            {
                case "firstname":
                    query = queryParameters.SortOrder.ToLower() == "desc"
                        ? query.OrderByDescending(c => c.FirstName)
                        : query.OrderBy(c => c.FirstName);
                    break;

                case "annualincome":
                    query = queryParameters.SortOrder.ToLower() == "desc"
                        ? query.OrderByDescending(c => c.AnnualIncome)
                        : query.OrderBy(c => c.AnnualIncome);
                    break;

                default:
                    query = query.OrderBy(c => c.CustomerId);
                    break;
            }

            // Pagination
            query = query
                .Skip((queryParameters.PageNumber - 1) * queryParameters.PageSize)
                .Take(queryParameters.PageSize);

            return await query.ToListAsync();
        }
    }
}