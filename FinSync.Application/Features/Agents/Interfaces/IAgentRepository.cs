using FinSync.Application.Features.Agents.DTOs;
using FinSync.Domain.Entities;

namespace FinSync.Application.Features.Agents.Interfaces
{
    public interface IAgentRepository
    {
        // CRUD
        Task<Agent> AddAsync(Agent agent);

        Task<Agent?> GetByIdAsync(int agentId);

        Task<IEnumerable<Agent>> GetAllAsync();

        Task UpdateAsync(Agent agent);

        Task DeleteAsync(Agent agent);

        // Search & Filtering
        Task<(IEnumerable<Agent> Agents, int TotalCount)> GetFilteredAsync(
            AgentQueryParametersDto queryParameters);

        // Utility
        Task<bool> ExistsAsync(int agentId);

        Task SaveChangesAsync();
    }
}