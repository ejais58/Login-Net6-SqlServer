using AplicationCore.interfaces.Dto.TurnoDto;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AplicationCore.interfaces.IService
{
    public interface ITurnosService
    {
        Task<object> VerTurnosDisponibles(RegistrarTurnoDto registro);
    }
}
