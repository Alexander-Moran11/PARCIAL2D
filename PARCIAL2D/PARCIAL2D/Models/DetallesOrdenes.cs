using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.ComponentModel.DataAnnotations;

namespace PARCIAL2D.Models
{
    public class DetallesOrdenes
    {
        [Key]
        public int DetalleOrdenID { get; set; }

        public int EncabezadoOrdenID { get; set; }

        public int EmpresaID { get; set; }

        public int PlatoID { get; set; }

        public decimal Cantidad { get; set; }

        public string Comentarios { get; set; }

        public decimal DescuentosEspecial { get; set; }

        public decimal RecargarOrden { get; set; }

        public string Estado { get; set; }

        public DateTime FechaCreacion { get; set; }

        public DateTime FechaModificacion { get; set; }
    }
}
