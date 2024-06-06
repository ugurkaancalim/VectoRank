using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using SSE.Engine.Application.DTOs;
using SSE.Engine.Application.Services.Interfaces;
using System.Collections.Generic;

namespace SSE.Engine.WebAPI.Controllers
{
    [Route("api/v1/[controller]")]
    [ApiController]
    public class DocumentController : ControllerBase
    {
        private readonly IDocumentService _documentService;
        public DocumentController(IDocumentService documentService)
        {
            _documentService = documentService;
        }
        [Route("add-document")]
        [HttpPost]
        public async Task<IActionResult> AddDocument(DocumentDto document)
        {
            //parseAppInfo
            await _documentService.AddDocument(document);
            return Ok();
        }
        [Route("add-documents")]
        [HttpPost]
        public async Task<IActionResult> AddDocuments(DocumentDto[] documents)
        {
            await _documentService.AddDocuments(documents);
            return Ok();
        }
    }
}
