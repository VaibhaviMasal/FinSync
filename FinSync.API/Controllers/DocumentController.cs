using FinSync.Application.Features.Documents.DTOs;
using FinSync.Application.Features.Documents.Interfaces;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace FinSync.API.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    [Authorize]
    public class DocumentController : ControllerBase
    {
        private readonly IDocumentService _documentService;

        public DocumentController(IDocumentService documentService)
        {
            _documentService = documentService;
        }

        [HttpGet("policy/{policyId}")]
        public async Task<IActionResult> GetByPolicy(int policyId)
        {
            var documents = await _documentService.GetByPolicyIdAsync(policyId);

            return Ok(documents);
        }

        [HttpGet("{documentId}")]
        public async Task<IActionResult> GetById(int documentId)
        {
            var document = await _documentService.GetByIdAsync(documentId);

            return Ok(document);
        }

        [HttpDelete("{documentId}")]
        public async Task<IActionResult> Delete(int documentId)
        {
            await _documentService.DeleteAsync(documentId);

            return Ok("Document deleted successfully.");
        }
    }
}