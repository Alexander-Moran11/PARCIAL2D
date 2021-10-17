using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.ComponentModel.DataAnnotations;

namespace PARCIAL2D.Models
{
    public class EncabezadoOrden
    {
        [Key]
        public int EncabezadoOrdenID { set; get; }
        public int EmpresaID { set; get; }
        public int UsuarioID { set; get; }
        public string TipoOrden { set; get; }
        public DateTime FechaOrden { set; get; }
        public int MesaID { set; get; }
        public string Cliente { set; get; }
        public string EstadoOrden { set; get; }
        public string TipoPago { set; get; }
        public string Estado { set; get; }
        public DateTime FechaCreacion { set; get; }
        public DateTime FechaModificacion { set; get; }
    }
}