using AplicationCore.Entities;
using AplicationCore.interfaces;
using AplicationCore.interfaces.Dto.TurnoDto;
using AplicationCore.interfaces.IDao;
using AplicationCore.interfaces.IService;

namespace WebApplication1.Service
{
    public class TurnosService : ITurnosService
    {
        private readonly ITurnosDao _turnosDao;
        private readonly IUsersDao _usersDao;
        private readonly IMascotasDao _mascotasDao;

        public TurnosService(ITurnosDao turnosDao,IUsersDao usersDao, IMascotasDao mascotasDao)
        {
            _usersDao = usersDao;
            _mascotasDao = mascotasDao;
            _turnosDao = turnosDao;
        }

        public async Task<object> VerTurnosDisponibles(RegistrarTurnoDto registro)
        {
            //verificamos si es psicologo
            var findPsicologo = await _usersDao.GetPsicologoById(registro.Id_Psicologo_Turno);
            if (findPsicologo == null || findPsicologo.Rol != "psicologo")
            {
                return "no existe o no es psicologo";
            }


            //le sumamos 9hs a la fecha fin la fecha fin
            var horaInicio = registro.Fecha_Inicio_Turno.AddHours(9);
            var horaFin = horaInicio.AddHours(9);

            Console.WriteLine(horaInicio);
            Console.WriteLine(horaFin);

            //inicializo arrays para guardar los diferentes turnos
            var arrayTurnosDisponiblesPerro = new List<DateTime>();
            var arrayTurnosDisponiblesGato = new List<DateTime>();

            
            //ver el tipo de mascota
            var findMascota = await _mascotasDao.FindMascotaById(registro.Id_Mascota_Turno);

            if (findMascota == null)
            {
                return "la mascota no existe";
            }

            var siguienteTurno = horaInicio;

            if (findMascota.Tipo_Mascota == "perro")
            {
                while (siguienteTurno < horaFin)
                {
                    var tiempoPerro = siguienteTurno.AddMinutes(30);
                    var turnoDisponible = await _turnosDao.TurnosDisponibles(registro.Id_Psicologo_Turno, siguienteTurno, tiempoPerro);
                    
                    //si no me devuelve nada de la consulta guardo el turno disponible
                    if (turnoDisponible.Count == 0)
                    {
                        arrayTurnosDisponiblesPerro.Add(siguienteTurno);
                    }

                    siguienteTurno = tiempoPerro;
                }

                return new { turnos_Perros = arrayTurnosDisponiblesPerro };
            }
            else if (findMascota.Tipo_Mascota == "gato")
            {
                while (siguienteTurno < horaFin)
                {
                    var tiempoGato = siguienteTurno.AddMinutes(45);
                    var turnoDisponible = await _turnosDao.TurnosDisponibles(registro.Id_Psicologo_Turno, siguienteTurno, tiempoGato);

                    //si no me devuelve nada de la consulta guardo el turno disponible
                    if (turnoDisponible.Count == 0)
                    {
                        arrayTurnosDisponiblesGato.Add(siguienteTurno);
                    }

                    siguienteTurno = tiempoGato;
                }

                return new { turnos_Gato = arrayTurnosDisponiblesGato };
            }
            else
            {
                return "Su mascota no es un perro o un gato";
            }

        }
    }
}
