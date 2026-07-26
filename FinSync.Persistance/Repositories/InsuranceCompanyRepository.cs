using FinSync.Application.Features.InsuranceCompanies.DTOs;
using FinSync.Application.Features.InsuranceCompanies.Interfaces;
using FinSync.Domain.Entities;
using FinSync.Persistence.Context;
using Microsoft.EntityFrameworkCore;

namespace FinSync.Persistence.Repositories
{
    public class InsuranceCompanyRepository : IInsuranceCompanyRepository
    {
        private readonly FinSyncDbContext _context;

        public InsuranceCompanyRepository(FinSyncDbContext context)
        {
            _context = context;
        }

        public async Task<InsuranceCompany> AddAsync(InsuranceCompany company)
        {
            await _context.InsuranceCompanies.AddAsync(company);
            await _context.SaveChangesAsync();

            return company;
        }

        public async Task<IEnumerable<InsuranceCompany>> GetAllAsync(
            InsuranceCompanyQueryParametersDto queryParameters)
        {
            var query = _context.InsuranceCompanies
                .AsNoTracking()
                .AsQueryable();

            // Search
            if (!string.IsNullOrWhiteSpace(queryParameters.SearchTerm))
            {
                var keyword = queryParameters.SearchTerm.Trim().ToLower();

                query = query.Where(c =>
                    c.CompanyName.ToLower().Contains(keyword) ||
                    c.CompanyCode.ToLower().Contains(keyword) ||
                    c.ContactPerson.ToLower().Contains(keyword) ||
                    c.Email.ToLower().Contains(keyword) ||
                    c.PhoneNumber.Contains(keyword) ||
                    c.City.ToLower().Contains(keyword) ||
                    c.State.ToLower().Contains(keyword));
            }

            // Sorting
            switch (queryParameters.SortBy?.ToLower())
            {
                case "companyname":
                    query = queryParameters.IsDescending
                        ? query.OrderByDescending(c => c.CompanyName)
                        : query.OrderBy(c => c.CompanyName);
                    break;

                case "companycode":
                    query = queryParameters.IsDescending
                        ? query.OrderByDescending(c => c.CompanyCode)
                        : query.OrderBy(c => c.CompanyCode);
                    break;

                case "city":
                    query = queryParameters.IsDescending
                        ? query.OrderByDescending(c => c.City)
                        : query.OrderBy(c => c.City);
                    break;

                default:
                    query = query.OrderBy(c => c.CompanyId);
                    break;
            }

            // Pagination
            query = query
                .Skip((queryParameters.PageNumber - 1) * queryParameters.PageSize)
                .Take(queryParameters.PageSize);

            return await query.ToListAsync();
        }

        public async Task<InsuranceCompany?> GetByIdAsync(int companyId)
        {
            return await _context.InsuranceCompanies
                .AsNoTracking()
                .FirstOrDefaultAsync(c => c.CompanyId == companyId);
        }

        public async Task<InsuranceCompany?> UpdateAsync(
            int companyId,
            InsuranceCompany company)
        {
            var existingCompany =
                await _context.InsuranceCompanies.FindAsync(companyId);

            if (existingCompany == null)
                return null;

            existingCompany.CompanyName = company.CompanyName;
            existingCompany.CompanyCode = company.CompanyCode;
            existingCompany.ContactPerson = company.ContactPerson;
            existingCompany.Email = company.Email;
            existingCompany.PhoneNumber = company.PhoneNumber;
            existingCompany.Website = company.Website;
            existingCompany.AddressLine1 = company.AddressLine1;
            existingCompany.AddressLine2 = company.AddressLine2;
            existingCompany.City = company.City;
            existingCompany.State = company.State;
            existingCompany.Pincode = company.Pincode;
            existingCompany.IsActive = company.IsActive;
            existingCompany.UpdatedDate = DateTime.UtcNow;

            await _context.SaveChangesAsync();

            return existingCompany;
        }

        public async Task<bool> DeleteAsync(int companyId)
        {
            var company =
                await _context.InsuranceCompanies.FindAsync(companyId);

            if (company == null)
                return false;

            _context.InsuranceCompanies.Remove(company);

            await _context.SaveChangesAsync();

            return true;
        }

        public async Task<IEnumerable<InsuranceCompany>> SearchAsync(string keyword)
        {
            keyword = keyword.Trim().ToLower();

            return await _context.InsuranceCompanies
                .AsNoTracking()
                .Where(c =>
                    c.CompanyName.ToLower().Contains(keyword) ||
                    c.CompanyCode.ToLower().Contains(keyword) ||
                    c.ContactPerson.ToLower().Contains(keyword) ||
                    c.Email.ToLower().Contains(keyword) ||
                    c.PhoneNumber.Contains(keyword) ||
                    c.City.ToLower().Contains(keyword) ||
                    c.State.ToLower().Contains(keyword))
                .ToListAsync();
        }

        public async Task<bool> ExistsByCompanyCodeAsync(
    string companyCode,
    int? excludeCompanyId = null)
        {
            companyCode = companyCode.Trim().ToLower();

            return await _context.InsuranceCompanies.AnyAsync(c =>
                c.CompanyCode.ToLower() == companyCode &&
                (!excludeCompanyId.HasValue || c.CompanyId != excludeCompanyId.Value));
        }

        public async Task<bool> ExistsByCompanyNameAsync(
            string companyName,
            int? excludeCompanyId = null)
        {
            companyName = companyName.Trim().ToLower();

            return await _context.InsuranceCompanies.AnyAsync(c =>
                c.CompanyName.ToLower() == companyName &&
                (!excludeCompanyId.HasValue || c.CompanyId != excludeCompanyId.Value));
        }
    }
}