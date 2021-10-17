using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using PARCIAL2D.Models;

namespace PARCIAL2D
{
    public class parcial2Context : DbContext
    {
        public parcial2Context(DbContextOptions<parcial2Context> options) : base(options)
        {

        }

        public DbSet<pagos> pagos { get; set; }
        public DbSet<mesa> mesas { get; set; }
        public DbSet<EncabezadoOrden> EncabezadoOrden { get; set; }
        public DbSet<DetallesOrdenes> DetallesOrdenes { get; set; }

    }
}
