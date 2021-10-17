using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using PARCIAL2D.Models;
using Microsoft.EntityFrameworkCore;


namespace PARCIAL2D.Controllers
{
    //[Route("api/[controller]")]
    [ApiController]
    public class EncabezadoOrdenController : ControllerBase
    {
        private readonly parcial2Context _contexto;

        public EncabezadoOrdenController(parcial2Context miContexto)
        {
            this._contexto = miContexto;
        }

        [HttpGet]
        [Route("api/EncabezadoOrden")]
        public IActionResult Get()
        {

            var EncabezadoOrdenList = from EO in _contexto.EncabezadoOrden
                                      join M in _contexto.mesas on EO.MesaID equals M.MesaID
                                      select new
                                      {
                                          idEncabezado = EO.EncabezadoOrdenID,
                                          idEmpresa = EO.EmpresaID,
                                          idUsuario = EO.UsuarioID,
                                          EO.TipoOrden,
                                          EO.FechaOrden,
                                          idMesa = M.MesaID,
                                          EO.Cliente,
                                          EO.EstadoOrden,
                                          EO.TipoPago,
                                          EO.Estado,
                                          EO.FechaCreacion,
                                          EO.FechaModificacion
                                      };
            if (EncabezadoOrdenList.Count() > 0)
            {
                return Ok(EncabezadoOrdenList);
            }
            return NotFound();
        }

        [HttpGet]
        [Route("api/EncabezadoOrden/{id}")]
        public IActionResult getbyid(int id)
        {
            var unEncabezadoOrden = (from EO in _contexto.EncabezadoOrden
                                     join M in _contexto.mesas on EO.MesaID equals M.MesaID
                                     where EO.EncabezadoOrdenID == id //filtro por id
                                     select new
                                     {
                                         idEncabezado = EO.EncabezadoOrdenID,
                                         idEmpresa = EO.EmpresaID,
                                         idUsuario = EO.UsuarioID,
                                         EO.TipoOrden,
                                         EO.FechaOrden,
                                         idMesa = M.MesaID,
                                         EO.Cliente,
                                         EO.EstadoOrden,
                                         EO.TipoPago,
                                         EO.Estado,
                                         EO.FechaCreacion,
                                         EO.FechaModificacion
                                     }
                                ).FirstOrDefault();
            if (unEncabezadoOrden != null)
            {
                return Ok(unEncabezadoOrden);
            }
            return NotFound();
        }

        [HttpGet]
        [Route("api/EncabezadoOrden/idMesa/{IdMesa}")]
        public IActionResult getByidmesa(int IdMesa)
        {
            var unEncabezadoOrden = (from EO in _contexto.EncabezadoOrden
                                     join M in _contexto.mesas on EO.MesaID equals M.MesaID
                                     where EO.MesaID == IdMesa //filtro por id
                                     select new
                                     {
                                         idEncabezado = EO.EncabezadoOrdenID,
                                         idEmpresa = EO.EmpresaID,
                                         idUsuario = EO.UsuarioID,
                                         EO.TipoOrden,
                                         EO.FechaOrden,
                                         idMesa = M.MesaID,
                                         EO.Cliente,
                                         EO.EstadoOrden,
                                         EO.TipoPago,
                                         EO.Estado,
                                         EO.FechaCreacion,
                                         EO.FechaModificacion
                                     }
                                ).FirstOrDefault();
            if (unEncabezadoOrden != null)
            {
                return Ok(unEncabezadoOrden);
            }
            return NotFound();
        }

        [HttpGet]
        [Route("api/EncabezadoOrden/idPago/{IdMesa}")]
        public IActionResult getByidmesaPago(int IdMesa)
        {
            var unEncabezadoOrden = (from EO in _contexto.EncabezadoOrden
                                     join p in _contexto.pagos on EO.EncabezadoOrdenID equals p.OrdenID
                                     join M in _contexto.mesas on EO.MesaID equals M.MesaID
                                     where M.MesaID == IdMesa //filtro por id
                                     select new
                                     {
                                         idPagos = p.PagoID,
                                         idEncabezado = EO.EncabezadoOrdenID,
                                         p.EmpresaID,
                                         p.MovimientoCajaID,
                                         p.TipoPago,
                                         p.SubTotal,
                                         p.Propina,
                                         p.Total,
                                         p.MontoPagado,
                                         p.UsuarioID,
                                         p.FechaCreacion,
                                         p.CreditoID,
                                         p.tarjetaNumero,
                                         p.nombreTarjeta,
                                         p.autorizacion,
                                         p.estado,
                                         p.fechaModificacion,
                                         M.MesaID,
                                         M.DescripcionMesa,
                                         M.Estado
                                     }
                                ).FirstOrDefault();
            if (unEncabezadoOrden != null)
            {
                return Ok(unEncabezadoOrden);
            }
            return NotFound();
        }

        [HttpPost]
        [Route("api/EncabezadoOrden/insertar")]
        public IActionResult guardarEncabezadoOrden([FromBody] EncabezadoOrden EncabezadoOrdeNuevo)
        {
            try
            {
                IEnumerable<EncabezadoOrden> EncabezadoOrdenExiste = from EO in _contexto.EncabezadoOrden
                                                                     join M in _contexto.mesas on EO.MesaID equals M.MesaID
                                                                     where EO.Cliente == EncabezadoOrdeNuevo.Cliente
                                                                     select EO;
                if (EncabezadoOrdenExiste.Count() == 0)
                {
                    _contexto.EncabezadoOrden.Add(EncabezadoOrdeNuevo);
                    _contexto.SaveChanges();
                    return Ok(EncabezadoOrdeNuevo);
                }
                return Ok(EncabezadoOrdenExiste);
            }
            catch (System.Exception)
            {
                return BadRequest();
            }
        }

        [HttpPut]
        [Route("api/EncabezadoOrden")]
        public IActionResult usuarioClientes([FromBody] EncabezadoOrden encabezadoAModificar)
        {
            EncabezadoOrden EncabezadoOrdenExiste = (from c in _contexto.EncabezadoOrden
                                                     where c.EncabezadoOrdenID == encabezadoAModificar.EncabezadoOrdenID
                                                     select c).FirstOrDefault();
            if (EncabezadoOrdenExiste is null)
            {
                return NotFound();
            }

            EncabezadoOrdenExiste.EncabezadoOrdenID = encabezadoAModificar.EncabezadoOrdenID;
            EncabezadoOrdenExiste.EmpresaID = encabezadoAModificar.EmpresaID;
            EncabezadoOrdenExiste.UsuarioID = encabezadoAModificar.UsuarioID;
            EncabezadoOrdenExiste.TipoOrden = encabezadoAModificar.TipoOrden;
            EncabezadoOrdenExiste.FechaOrden = encabezadoAModificar.FechaOrden;
            EncabezadoOrdenExiste.MesaID = encabezadoAModificar.MesaID;
            EncabezadoOrdenExiste.Cliente = encabezadoAModificar.Cliente;
            EncabezadoOrdenExiste.EstadoOrden = encabezadoAModificar.EstadoOrden;
            EncabezadoOrdenExiste.TipoPago = encabezadoAModificar.TipoPago;
            EncabezadoOrdenExiste.Estado = encabezadoAModificar.Estado;
            EncabezadoOrdenExiste.FechaCreacion = encabezadoAModificar.FechaCreacion;
            EncabezadoOrdenExiste.FechaModificacion = encabezadoAModificar.FechaModificacion;

            _contexto.Entry(EncabezadoOrdenExiste).State = EntityState.Modified;
            _contexto.SaveChanges();
            return Ok(EncabezadoOrdenExiste);
        }
    }
}