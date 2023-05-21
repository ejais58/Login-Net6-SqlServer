using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AplicationCore.Entities
{
    public class Mascotas
    {
        public int Id_Mascota { get; set; }
        public string Nombre_Mascota { get; set; }
        public string Tipo_Mascota { get; set; }
        public int Id_Dueno { get; set; }
        public int? Tiempo_Mascota { get; set; }

    }
}
