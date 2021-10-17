using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.ComponentModel.DataAnnotations;

namespace PARCIAL2D.Models
{
    public class mesa
    {
        [Key]
        public int MesaID { set; get; }
        public int EmpresaID { set; get; }
        public string DescripcionMesa { set; get; }
        public int ZonaMesa { set; get; }
        public int SillasMesa { set; get; }
        public string Estado { set; get; }
        public DateTime FechaCreacion { set; get; }
        public DateTime FechaModificacion { get; set; }
    }
}