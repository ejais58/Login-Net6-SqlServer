using AplicationCore.Entities;
using AplicationCore.interfaces.Dto;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AplicationCore.interfaces.IService
{
    public interface IUsersService
    {
        Task<string> login(LoginDto login);

        Task Register(Users user);

        Task<List<Users>> GetAll();
    }
}
