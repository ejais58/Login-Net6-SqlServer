using AplicationCore.Entities;
using AplicationCore.interfaces.Dto.TurnoDto;
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
    public class TurnosDao : ITurnosDao
    {

        private readonly string _connectionString = ConnectionString.Cadena();
        public Task RegistrarTurno(RegistrarTurnoDto registro)
        {
            throw new NotImplementedException();
        }

        public async Task<List<Turnos>> TurnosDisponibles(int idPsicologo, DateTime fechaInicio, DateTime fechaFin)
        {
            using (SqlConnection connection = new SqlConnection(_connectionString))
            {
                await connection.OpenAsync();

                SqlCommand command = new SqlCommand("SELECT * FROM Turnos WHERE (Fecha_Inicio_Turno < @FechaFin) AND (Fecha_Fin_Turno > @FechaInicio) AND (Id_Psicologo_Turno = @IdPsico)", connection);
                //SqlCommand command = new SqlCommand("SELECT * FROM Turnos WHERE (Fecha_Inicio_Turno <= @FechaInicio AND Fecha_Fin_Turno >= @FechaInicio OR Fecha_Inicio_Turno < @FechaFin AND Fecha_Fin_Turno > @FechaFin) AND Id_Psicologo_Turno = @idPsico", connection);
                command.Parameters.AddWithValue("FechaInicio", fechaInicio);
                command.Parameters.AddWithValue("FechaFin", fechaFin);
                command.Parameters.AddWithValue("idPsico", idPsicologo);

                List<Turnos> turnosDisponibles = new List<Turnos>();

                using (SqlDataReader reader = await command.ExecuteReaderAsync())
                {
                    while (await reader.ReadAsync())
                    {
                        // Aquí debes mapear los datos de la fila del resultado a un objeto Turno.
                        // Por ejemplo:
                        Turnos turno = new Turnos
                        {
                            Id_Turno = reader.GetInt32(reader.GetOrdinal("Id_Turno")),
                            Fecha_Inicio_Turno = reader.GetDateTime(reader.GetOrdinal("Fecha_Inicio_Turno")),
                            Fecha_Fin_Turno = reader.GetDateTime(reader.GetOrdinal("Fecha_Fin_Turno"))
                        };

                        turnosDisponibles.Add(turno);
                    }
                }

                return turnosDisponibles;
            }
        }
    }
}
