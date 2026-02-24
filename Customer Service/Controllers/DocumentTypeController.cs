using Customer.Application.Dto.Document;
using Customer.Application.Interface;
using Customer_Service.Helpers;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace Customer_Service.Controllers
{
    [Route("api/v1/document")]
    [ApiController]
    public class DocumentTypeController : ControllerBase
    {
        private readonly IDocumentTypeService _documentTypeService;
        public DocumentTypeController(IDocumentTypeService d)
        {
            _documentTypeService = d;
        }
        [HttpGet("{id}")]
        public async Task<IActionResult> GetById(int id)
        {
           var data = await _documentTypeService.GetDocumentById(id);
            return Ok(ApiResponse<FetchDocumentTypeDto>.SuccessResponse(data, "success"));
        }

        [HttpGet]
        public async Task<IActionResult> GetDocument()
        {
            var result = await _documentTypeService.GetDocumentTypesAsync();
            return Ok(ApiResponse<List<FetchDocumentTypeDto>>.SuccessResponse(result, "success"));
        }

        [HttpPost]
        public async Task<IActionResult> AddDocument(DocumentTypeDto dto)
        {
           await _documentTypeService.AddDocumentTypeAsync(dto);
                return Ok(ApiResponse<String>.SuccessResponse("Sucessfully added document", "success"));
            
        }
    }
}
