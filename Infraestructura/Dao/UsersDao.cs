using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using AplicationCore.Entities;
using AplicationCore.interfaces;
using System.Configuration;
using Infraestructura.DataAccess;
using BCrypt.Net;
using AplicationCore.interfaces.Dto;
using System.ComponentModel.DataAnnotations;

namespace Infraestructura.Dao
{
    public class UsersDao : IUsersDao
    {
        private string _connectionString = ConnectionString.Cadena();

        public async Task AddUser(Users user)
        {
            using (SqlConnection connection = new SqlConnection(_connectionString))
            {

                string hashedPassword = BCrypt.Net.BCrypt.HashPassword(user.Password);

                SqlCommand command = new SqlCommand("INSERT INTO Users (Nombre, Apellido, Email, Password) VALUES (@Nombre, @Apellido, @Email, @Password)", connection);

                command.Parameters.AddWithValue("Nombre", user.Nombre);
                command.Parameters.AddWithValue("Apellido", user.Apellido);
                command.Parameters.AddWithValue("Email", user.Email);
                command.Parameters.AddWithValue("Password", hashedPassword);


                await connection.OpenAsync();

                await command.ExecuteNonQueryAsync();

            }
        }

        public async Task<Users> GetUserByEmailAndPassword(LoginDto login)
        {
          
            using (SqlConnection connection = new SqlConnection(_connectionString))
            {
                SqlCommand command = new SqlCommand("SELECT * FROM Users WHERE Email = @Email", connection);
                command.Parameters.AddWithValue("Email", login.Email);

                await connection.OpenAsync();

                SqlDataReader reader = await command.ExecuteReaderAsync();

                if (reader.Read())
                {
                    // Se ha encontrado el usuario que coincide con el correo electrónico proporcionado
                    int id = reader.GetInt32(reader.GetOrdinal("Id"));
                    string nombre = reader.GetString(reader.GetOrdinal("Nombre"));
                    string apellido = reader.GetString(reader.GetOrdinal("Apellido"));
                    string email = reader.GetString(reader.GetOrdinal("Email"));
                    string hashedPassword = reader.GetString(reader.GetOrdinal("Password"));

                    Users user = new Users
                    {
                        Id = id,
                        Nombre = nombre,
                        Apellido = apellido,
                        Email = email,
                        Password = hashedPassword
                    };

                    return user;
                }

                return null;


            }
        }

        public async Task<List<Users>> GetUsers()
        {
            using (SqlConnection connection = new SqlConnection(_connectionString))
            {
                SqlCommand command = new SqlCommand("SELECT * FROM Users", connection);
                await connection.OpenAsync();
                using (SqlDataReader reader = await command.ExecuteReaderAsync())
                {
                    List<Users> users = new List<Users>();
                    while (await reader.ReadAsync())
                    {
                        Users user = new Users();
                        user.Id = reader.GetInt32(0);
                        user.Nombre = reader.GetString(1);
                        user.Apellido = reader.GetString(2);
                        user.Email = reader.GetString(3);
                        user.Password = reader.GetString(4);
                        users.Add(user);
                    }
                    return users;
                }

            }

        }


        
    }


}
