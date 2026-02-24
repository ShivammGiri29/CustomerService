using Customer.Application.Dto.Customer;
using Customer.Application.Dto.Document;
using Customer.Application.Dto.KYC;
using Customer.Application.Exceptions;
using Customer.Application.Interface;
using Customer.Application.Services;
using Customer_Service.Helpers;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;

namespace Customer_Service.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CustomerKycController : ControllerBase
    {
        private readonly ICustomerKycService _service;
        public CustomerKycController(ICustomerKycService service)
        {
            _service = service;
        }
        [HttpPost("upload/{id}")]
        public async Task<IActionResult> Upload([FromForm] CustomerKycDto dto,int id)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);
            

            await _service.AddCustomerKycAsync(id, dto);

            return Ok(ApiResponse<string>.SuccessResponse("Document updated successfully", "success"));
        }

        [HttpGet("{customerid}")]
        public async Task<IActionResult> getKycDocuments(int customerid)
        {
            var data = await _service.FetchByCustomerId(customerid);

            foreach (var item in data)
            {
                item.DownloadUrl = $"/api/CustomerKyc/download/{item.Id}";
            }

            return Ok(ApiResponse<List<FetchDocumentKycDto>>
                .SuccessResponse(data, "success"));
        }


        [HttpPut("status/{kycId}")]
        public async Task<IActionResult> UpdateStatus(int kycId, UpdateKycStatusDto dto)
        {
            await _service.UpdateStatusAsync(kycId, dto);

            return Ok(ApiResponse<String>.SuccessResponse("Status updated successsfully","success"));
        }


        [HttpPut("{id}")]
        public async Task<IActionResult> UpdateByUser(int kycid, UpdateCustomerKycDto dto)
        {
   
            await _service.UpdateKycUserAsync(kycid, dto);

            return Ok(ApiResponse<string>.SuccessResponse("User updated successfully", "success"));
        }

        [HttpGet("download/{kycId}")]
        public async Task<IActionResult> Download(int kycId)
        {
            var document = await _service.GetKycByIdAsync(kycId);

            if (document == null)
                throw new NotFoundException("Document not found");

            var filePath = Path.Combine(
                Directory.GetCurrentDirectory(),
                "wwwroot",
                document.FilePath.TrimStart('/')
            );

            if (!System.IO.File.Exists(filePath))
                throw new NotFoundException("File not found on server");

            var fileBytes = await System.IO.File.ReadAllBytesAsync(filePath);

            return File(fileBytes, "application/octet-stream",
                Path.GetFileName(filePath));
        }



    }
}
