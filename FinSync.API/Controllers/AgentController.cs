using FinSync.Application.Features.Agents.DTOs;
using FinSync.Application.Features.Agents.Interfaces;
using Microsoft.AspNetCore.Mvc;

namespace FinSync.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AgentController : ControllerBase
    {
        private readonly IAgentService _agentService;

        public AgentController(IAgentService agentService)
        {
            _agentService = agentService;
        }

        // POST: api/Agent
        [HttpPost]
        public async Task<IActionResult> CreateAgent(CreateAgentRequestDto request)
        {
            var agent = await _agentService.CreateAgentAsync(request);
            return CreatedAtAction(nameof(GetAgentById), new { agentId = agent.AgentId }, agent);
        }

        // GET: api/Agent
        [HttpGet]
        public async Task<IActionResult> GetAllAgents()
        {
            var agents = await _agentService.GetAllAgentsAsync();
            return Ok(agents);
        }

        // GET: api/Agent/5
        [HttpGet("{agentId:int}")]
        public async Task<IActionResult> GetAgentById(int agentId)
        {
            var agent = await _agentService.GetAgentByIdAsync(agentId);

            if (agent == null)
                return NotFound();

            return Ok(agent);
        }

        // PUT: api/Agent/5
        [HttpPut("{agentId:int}")]
        public async Task<IActionResult> UpdateAgent(int agentId, UpdateAgentRequestDto request)
        {
            var updatedAgent = await _agentService.UpdateAgentAsync(agentId, request);
            return Ok(updatedAgent);
        }

        // DELETE: api/Agent/5
        [HttpDelete("{agentId:int}")]
        public async Task<IActionResult> DeleteAgent(int agentId)
        {
            await _agentService.DeleteAgentAsync(agentId);
            return NoContent();
        }

        // GET: api/Agent/filter
        [HttpGet("filter")]
        public async Task<IActionResult> GetFilteredAgents([FromQuery] AgentQueryParametersDto queryParameters)
        {
            var result = await _agentService.GetFilteredAgentsAsync(queryParameters);
            return Ok(result);
        }
    }
}