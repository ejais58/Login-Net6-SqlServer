using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Infraestructura.DataAccess
{
    public static class ConnectionString
    {
        public static string Cadena()
        {
            return @"Data Source=DESKTOP-6UCTG52;Initial Catalog=ApiLogin;Persist Security Info=True;User ID=sa;Password=123456";
        }
    }
}
