using AplicationCore.Entities;
using AplicationCore.interfaces.Dto.TurnoDto;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AplicationCore.interfaces.IDao
{
    public interface ITurnosDao
    {
        Task<List<Turnos>> TurnosDisponibles(int idPsicolog, DateTime fechaInicio, DateTime fechaFin);
        Task RegistrarTurno(RegistrarTurnoDto registro);
    }
}
