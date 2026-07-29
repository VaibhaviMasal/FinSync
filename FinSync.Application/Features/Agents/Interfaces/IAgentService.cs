using FinSync.Application.Features.Agents.DTOs;

namespace FinSync.Application.Features.Agents.Interfaces
{
    public interface IAgentService
    {
        // CRUD
        Task<AgentResponseDto> CreateAgentAsync(CreateAgentRequestDto request);

        Task<AgentResponseDto?> GetAgentByIdAsync(int agentId);

        Task<IEnumerable<AgentResponseDto>> GetAllAgentsAsync();

        Task<AgentResponseDto> UpdateAgentAsync(int agentId, UpdateAgentRequestDto request);

        Task DeleteAgentAsync(int agentId);

        // Search, Filter, Pagination & Sorting
        Task<(IEnumerable<AgentResponseDto> Agents, int TotalCount)> GetFilteredAgentsAsync(
            AgentQueryParametersDto queryParameters);
    }
}