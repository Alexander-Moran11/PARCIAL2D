using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.ComponentModel.DataAnnotations;

namespace PARCIAL2D.Models
{
    public class pagos
    {
        [Key]
        public int PagoID { get; set; }
        public int EmpresaID { get; set; }
        public int OrdenID { get; set; }
        public int MovimientoCajaID { get; set; }
        public string TipoPago { get; set; }
        public decimal SubTotal { get; set; }
        public decimal Propina { get; set; }
        public decimal Total { get; set; }
        public decimal MontoPagado { get; set; }
        public int UsuarioID { get; set; }
        public DateTime FechaCreacion { get; set; }
        public decimal CreditoID { get; set; }
        public string tarjetaNumero { get; set; }
        public string nombreTarjeta { get; set; }
        public string autorizacion { get; set; }
        public string estado { get; set; }
        public DateTime fechaModificacion { get; set; }
    }
}

