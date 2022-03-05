using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.ComponentModel.DataAnnotations;

namespace _2019BL601WACRUD.Models
{
    public class equipos
    {
        [Key]
        public int id_equipos { get; set; }
        public string nombre { get; set; }
        public string descripcion { get; set; }
        public int tipo_equipo_id { get; set; }
        public int marca_id { get; set; }
        public int estado_equipo_id { get; set; }
    }
}
