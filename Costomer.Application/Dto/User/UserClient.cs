using Customer.Application.Exceptions;
using Customer.Application.Interface;
using Customer_Service.Helpers;
using System.Net;
using System.Net.Http.Json;

namespace Customer.Application.Dto.User
{
    public class UserClient : IUserClient
    {
        private readonly HttpClient _httpClient;

        public UserClient(HttpClient httpClient)
        {
            _httpClient = httpClient;
        }

        public async Task<UserDto> GetByEmail(string Email)
        {
            try
            {
                var response = await _httpClient
                    .GetFromJsonAsync<ApiResponse<UserDto>>($"api/Auth/customer/email/{Email}");

                if (response == null || !response.Success || response.Data == null)
                {
                    throw new NotFoundException("User not found");
                }

                return response.Data;
            }
            catch (HttpRequestException ex) when (ex.StatusCode == HttpStatusCode.NotFound)
            {
                throw new NotFoundException("User not found");
            }
        }

        public async Task<UserDto?> GetUserById(int id)
        {
            try
            {
                var response = await _httpClient
                    .GetFromJsonAsync<ApiResponse<UserDto>>($"api/Auth/customer/{id}");

                if (response == null || !response.Success || response.Data == null)
                {
                    throw new NotFoundException("User not found");
                }

                return response.Data;
            }
            catch (HttpRequestException ex) when (ex.StatusCode == HttpStatusCode.NotFound)
            {
                throw new NotFoundException("User not found");
            }
        }

    }
}
