using AplicationCore.Entities;
using AplicationCore.interfaces;
using AplicationCore.interfaces.Dto;
using AplicationCore.interfaces.IService;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Reflection.PortableExecutable;
using System.Security.Claims;
using System.Text;

namespace WebApplication1.Service
{
    public class UsersService : IUsersService
    {
        private readonly IUsersDao _usersDao;
        private readonly IConfiguration _configuration;

        public UsersService(IUsersDao usersDao, IConfiguration configuration)
        {
            _usersDao = usersDao;
            _configuration = configuration;
        }

        //lista de usuarios
        public async Task<List<Users>> GetAll()
        {
            return await _usersDao.GetUsers();
        }

        //login de usuario
        public async Task<string> login(LoginDto login)
        {
            var user = await _usersDao.GetUserByEmailAndPassword(login);
            if (user != null)
            {
                if (BCrypt.Net.BCrypt.Verify(login.Password, user.Password))
                {
                    //generar token
                    string jwtToken = GenerarToken(user);

                    return $"token: {jwtToken}";
                } 
                else
                {
                    return "La contraseña es incorrecta";
                }
            }
            return "No se encontro usuario";
            
        }

        //generar token
        private string GenerarToken(Users user)
        {
            var claims = new[]
            {
                new Claim(ClaimTypes.Name, user.Nombre),
                new Claim(ClaimTypes.Email, user.Email)
            };

            //crea una clave secreta simétrica para firmar y verificar tokens
            var key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_configuration.GetSection("JWT:Key").Value));
            
            //credenciales
            var creds = new SigningCredentials(key, SecurityAlgorithms.HmacSha384Signature);

            var securityToken = new JwtSecurityToken(
                                    claims: claims,
                                    expires: DateTime.Now.AddMinutes(60),
                                    signingCredentials: creds
                                    );
            //convertir un token de seguridad en un string
            string token = new JwtSecurityTokenHandler().WriteToken(securityToken);

            return token;
        }


        //registrar usuario
        public async Task Register(Users user)
        {
            await _usersDao.AddUser(user);
        }

    }
}
