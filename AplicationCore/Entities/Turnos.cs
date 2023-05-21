using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AplicationCore.Entities
{
    public class Turnos
    {
        public int Id_Turno { get; set; }
        public int Id_Psicologo_Turno { get; set; }
        public int Id_Mascota_Turno { get; set; }
        public DateTime Fecha_Inicio_Turno { get; set; }
        public DateTime Fecha_Fin_Turno { get; set; }
        public int Id_Estado_Turno { get; set; }
    }
}
