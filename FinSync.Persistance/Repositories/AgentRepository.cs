using FinSync.Application.Features.Agents.DTOs;
using FinSync.Application.Features.Agents.Interfaces;
using FinSync.Domain.Entities;
using FinSync.Persistence.Context;
using Microsoft.EntityFrameworkCore;

namespace FinSync.Persistence.Repositories
{
    public class AgentRepository : IAgentRepository
    {
        private readonly FinSyncDbContext _context;

        public AgentRepository(FinSyncDbContext context)
        {
            _context = context;
        }

        // Create Agent
        public async Task<Agent> AddAsync(Agent agent)
        {
            await _context.Agents.AddAsync(agent);
            await _context.SaveChangesAsync();

            return agent;
        }

        // Get All Agents
        public async Task<IEnumerable<Agent>> GetAllAsync(AgentQueryParametersDto queryParameters)
        {
            var query = _context.Agents.AsQueryable();

            // Filter
            if (queryParameters.IsActive.HasValue)
            {
                query = query.Where(a => a.IsActive == queryParameters.IsActive.Value);
            }

            // Sorting
            switch (queryParameters.SortBy.ToLower())
            {
                case "firstname":
                    query = queryParameters.Descending
                        ? query.OrderByDescending(a => a.FirstName)
                        : query.OrderBy(a => a.FirstName);
                    break;

                case "joiningdate":
                    query = queryParameters.Descending
                        ? query.OrderByDescending(a => a.JoiningDate)
                        : query.OrderBy(a => a.JoiningDate);
                    break;

                case "city":
                    query = queryParameters.Descending
                        ? query.OrderByDescending(a => a.City)
                        : query.OrderBy(a => a.City);
                    break;

                default:
                    query = query.OrderBy(a => a.AgentId);
                    break;
            }

            // Pagination
            query = query
                .Skip((queryParameters.PageNumber - 1) * queryParameters.PageSize)
                .Take(queryParameters.PageSize);

            return await query.ToListAsync();
        }

        // Get Agent By Id
        public async Task<Agent?> GetByIdAsync(int agentId)
        {
            return await _context.Agents
                .AsNoTracking()
                .FirstOrDefaultAsync(a => a.AgentId == agentId);
        }

        // Update Agent
        public async Task<Agent?> UpdateAsync(int agentId, Agent agent)
        {
            var existingAgent = await _context.Agents.FindAsync(agentId);

            if (existingAgent == null)
                return null;

            existingAgent.FirstName = agent.FirstName;
            existingAgent.MiddleName = agent.MiddleName;
            existingAgent.LastName = agent.LastName;
            existingAgent.Gender = agent.Gender;
            existingAgent.DateOfBirth = agent.DateOfBirth;

            existingAgent.MobileNumber = agent.MobileNumber;
            existingAgent.AlternateMobileNumber = agent.AlternateMobileNumber;
            existingAgent.Email = agent.Email;

            existingAgent.PanNumber = agent.PanNumber;
            existingAgent.AadhaarNumber = agent.AadhaarNumber;

            existingAgent.LicenseNumber = agent.LicenseNumber;
            existingAgent.JoiningDate = agent.JoiningDate;

            existingAgent.Address = agent.Address;
            existingAgent.City = agent.City;
            existingAgent.State = agent.State;
            existingAgent.Pincode = agent.Pincode;

            existingAgent.IsActive = agent.IsActive;
            existingAgent.UpdatedDate = DateTime.UtcNow;

            await _context.SaveChangesAsync();

            return existingAgent;
        }

        // Delete Agent
        public async Task<bool> DeleteAsync(int agentId)
        {
            var agent = await _context.Agents.FindAsync(agentId);

            if (agent == null)
                return false;

            _context.Agents.Remove(agent);

            await _context.SaveChangesAsync();

            return true;
        }

        // Search Agent
        public async Task<IEnumerable<Agent>> SearchAsync(string keyword)
        {
            keyword = keyword.Trim().ToLower();

            return await _context.Agents
                .Where(a =>
                    a.FirstName.ToLower().Contains(keyword) ||
                    a.LastName.ToLower().Contains(keyword) ||
                    a.MobileNumber.Contains(keyword) ||
                    a.PanNumber.ToLower().Contains(keyword) ||
                    a.AadhaarNumber.Contains(keyword) ||
                    a.Email.ToLower().Contains(keyword) ||
                    a.City.ToLower().Contains(keyword) ||
                    a.LicenseNumber.ToLower().Contains(keyword))
                .ToListAsync();
        }
    }
}