using Customer.Application.Dto.Customer;
using Customer.Application.Interface;
using Customer_Service.Helpers;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.AspNetCore.Mvc;

namespace Customer_Service.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CustomerController : ControllerBase
    {
        private readonly ICustomerService _customerService;
        public CustomerController(ICustomerService customerService)
        {
            _customerService = customerService;
        }
        [HttpPost]
        public async Task<IActionResult> AddCustomer(CustomerDto dto)
        {
             var data = await _customerService.AddCustomerAsync(dto);
                return Ok(ApiResponse<string>.SuccessResponse("Customer Added successfully", "success"));
           
        }


        [HttpPut]
        public async Task<IActionResult> UpdateCustomer(CustomerDto dto)
        {
             await _customerService.UpdateCustomerAsync(dto);
            return Ok(ApiResponse<String>.SuccessResponse("User updated successfully", "success"));


        }

        [HttpGet("{id}")]
        public async Task<IActionResult> CustomerGetById(int id)
        {
            var data = await _customerService.GetCustomerById(id);
            if(data==null)
            {
                return NotFound(
                   ApiResponse<FetchCustomerDto>.FailureResponse(
                       "Customer not found",
                       new ApiError
                       {
                           Code = "404",
                           Details = "No customer exists with the provided user ID"
                       }
                   )
               );
            }
            return Ok(ApiResponse<FetchCustomerDto>.SuccessResponse(data, "success"));
        }

    }
}
