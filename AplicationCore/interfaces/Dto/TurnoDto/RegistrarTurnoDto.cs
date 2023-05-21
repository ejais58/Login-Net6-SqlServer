using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AplicationCore.interfaces.Dto.TurnoDto
{
    public class RegistrarTurnoDto
    {
        public int Id_Psicologo_Turno { get; set; }
        public DateTime Fecha_Inicio_Turno { get; set; }
        public DateTime Fecha_Fin_Turno { get; set; }
        public int Id_Mascota_Turno { get; set; }
    }
}
