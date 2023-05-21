using AplicationCore.Entities;
using AplicationCore.interfaces.Dto;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AplicationCore.interfaces
{
    public interface IUsersDao
    {
        Task<List<Users>> GetUsers();
        Task AddUser(Users user);
        Task<Users> GetUserByEmailAndPassword(LoginDto login);
        
        Task<Users> GetPsicologoById(int idPsicologo);
        
    }
}
