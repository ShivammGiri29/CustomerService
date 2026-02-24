using Customer.Application.Dto.User;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Customer.Application.Interface
{
    public interface IUserClient
    {
        Task<UserDto?> GetUserById(int id);
    }
}
