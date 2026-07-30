using AutoMapper;
using FinSync.Application.Features.Agents.DTOs;
using FinSync.Application.Features.Agents.Interfaces;
using FinSync.Domain.Entities;
using FinSync.Shared.Exceptions;

namespace FinSync.Application.Features.Agents.Services
{
    public class AgentService : IAgentService
    {
        private readonly IAgentRepository _agentRepository;
        private readonly IMapper _mapper;

        public AgentService(
            IAgentRepository agentRepository,
            IMapper mapper)
        {
            _agentRepository = agentRepository;
            _mapper = mapper;
        }

        public async Task<AgentResponseDto> CreateAgentAsync(CreateAgentRequestDto request)
        {
            var agent = _mapper.Map<Agent>(request);

            agent.CreatedDate = DateTime.UtcNow;
            agent.UpdatedDate = DateTime.UtcNow;

            var createdAgent = await _agentRepository.AddAsync(agent);

            return _mapper.Map<AgentResponseDto>(createdAgent);
        }

        public async Task<AgentResponseDto?> GetAgentByIdAsync(int agentId)
        {
            var agent = await _agentRepository.GetByIdAsync(agentId);

            if (agent == null)
                return null;

            return _mapper.Map<AgentResponseDto>(agent);
        }

        public async Task<IEnumerable<AgentResponseDto>> GetAllAgentsAsync()
        {
            var queryParameters = new AgentQueryParametersDto();

            var agents = await _agentRepository.GetAllAsync(queryParameters);

            return _mapper.Map<IEnumerable<AgentResponseDto>>(agents);
        }

        public async Task<AgentResponseDto> UpdateAgentAsync(int agentId, UpdateAgentRequestDto request)
        {
            var existingAgent = await _agentRepository.GetByIdAsync(agentId);

            if (existingAgent == null)
                throw new NotFoundException($"Agent with ID {agentId} not found.");

            _mapper.Map(request, existingAgent);

            existingAgent.UpdatedDate = DateTime.UtcNow;

            var updatedAgent = await _agentRepository.UpdateAsync(agentId, existingAgent);

            return _mapper.Map<AgentResponseDto>(updatedAgent!);
        }

        public async Task DeleteAgentAsync(int agentId)
        {
            var deleted = await _agentRepository.DeleteAsync(agentId);

            if (!deleted)
                throw new NotFoundException($"Agent with ID {agentId} not found.");
        }

        public async Task<(IEnumerable<AgentResponseDto> Agents, int TotalCount)> GetFilteredAgentsAsync(
            AgentQueryParametersDto queryParameters)
        {
            var agents = await _agentRepository.GetAllAsync(queryParameters);

            var agentDtos = _mapper.Map<IEnumerable<AgentResponseDto>>(agents);

            return (agentDtos, agentDtos.Count());
        }
    }
}