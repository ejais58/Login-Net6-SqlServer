using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AplicationCore.Entities
{
    public class Historiaclinica
    {
        public int Id_HistoriaClinica { get; set; }
        public int Id_Mascota_HistoriaClinica { get; set; }
        public DateTime Fecha_HistoriaClinica { get; set; }
        public string Evaluacion_HistoriaClinica { get; set; }
    }
}
