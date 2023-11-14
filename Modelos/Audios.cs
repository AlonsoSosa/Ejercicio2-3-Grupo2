using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using SQLite;

namespace Ejercicio2_3_Grupo2.Modelos
{
    public class Audios
    {
        [PrimaryKey, AutoIncrement]
        public int id { get; set; }
        public string url { get; set; }
        public string descripcion { get; set; }
        public DateTime Fecha { get; set; }
    }
}
