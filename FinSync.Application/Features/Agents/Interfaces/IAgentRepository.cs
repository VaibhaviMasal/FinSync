using FinSync.Application.Features.Agents.DTOs;
using FinSync.Domain.Entities;

namespace FinSync.Application.Features.Agents.Interfaces
{
    public interface IAgentRepository
    {
        Task<Agent> AddAsync(Agent agent);

        Task<IEnumerable<Agent>> GetAllAsync(AgentQueryParametersDto queryParameters);

        Task<Agent?> GetByIdAsync(int agentId);

        Task<Agent?> UpdateAsync(int agentId, Agent agent);

        Task<bool> DeleteAsync(int agentId);

        Task<IEnumerable<Agent>> SearchAsync(string keyword);
    }
}