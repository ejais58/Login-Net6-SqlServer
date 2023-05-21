using AplicationCore.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AplicationCore.interfaces.IDao
{
    public interface IMascotasDao
    {
        Task<Mascotas> FindMascotaById(int idMascota);
    }
}
