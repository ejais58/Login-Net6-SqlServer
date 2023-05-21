using AplicationCore.Entities;
using AplicationCore.interfaces.IDao;
using Infraestructura.DataAccess;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Infraestructura.Dao
{
    public class MascotasDao : IMascotasDao
    {
        private readonly string _connectionString = ConnectionString.Cadena();
        public async Task<Mascotas> FindMascotaById(int idMascota)
        {
            using (SqlConnection connection = new SqlConnection(_connectionString))
            {
                SqlCommand command = new SqlCommand("SELECT * FROM Mascotas WHERE Id_Mascota = @idMascota", connection);
                command.Parameters.AddWithValue("idMascota", idMascota);

                await connection.OpenAsync();

                SqlDataReader reader = await command.ExecuteReaderAsync();

                if (reader.Read())
                {
                    // Se ha encontrado el usuario que coincide con el correo electrónico proporcionado
                    int id = reader.GetInt32(reader.GetOrdinal("Id_Mascota"));
                    string nombre = reader.GetString(reader.GetOrdinal("Nombre_Mascota"));
                    string tipo = reader.GetString(reader.GetOrdinal("Tipo_Mascota"));
                    int idDuenio = reader.GetInt32(reader.GetOrdinal("Id_Dueno"));
                    int tiempo = reader.GetInt32(reader.GetOrdinal("Tiempo_Mascota"));
                    

                    Mascotas mascota = new Mascotas
                    {
                        Id_Mascota = id,
                        Nombre_Mascota = nombre,
                        Tipo_Mascota = tipo,
                        Id_Dueno = idDuenio,
                        Tiempo_Mascota = tiempo

                    };

                    return mascota;
                }

                return null;
            }
        }
    }
}
