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
    [ApiController]
    public class DetallesOrdenesController : ControllerBase
    {
        private readonly parcial2Context _contexto;

        public DetallesOrdenesController(parcial2Context miContexto)
        {
            this._contexto = miContexto;
        }

        [HttpGet]
        [Route("api/detalleOrdenes")]
        public IActionResult Get()
        {
            var detalleOrdenesList = from deo in _contexto.DetallesOrdenes
                                     join eo in _contexto.EncabezadoOrden on deo.EncabezadoOrdenID equals eo.EncabezadoOrdenID
                                     select new
                                     {
                                         deo.DetalleOrdenID,
                                         deo.EncabezadoOrdenID,
                                         deo.EmpresaID,
                                         deo.PlatoID,
                                         deo.Cantidad,
                                         deo.Comentarios,
                                         deo.DescuentosEspecial,
                                         deo.RecargarOrden,
                                         deo.Estado,
                                         deo.FechaCreacion,
                                         deo.FechaModificacion
                                     };
            if (detalleOrdenesList.Count() > 0)
            {
                return Ok(detalleOrdenesList);
            }
            return NotFound();
        }

        [HttpGet]
        [Route("api/detalleOrdenes/{id}")]
        public IActionResult getById(int id)
        {
            try
            {
                var undetalleOrdenesList = (from deo in _contexto.DetallesOrdenes
                                            join eo in _contexto.EncabezadoOrden on deo.EncabezadoOrdenID equals eo.EncabezadoOrdenID
                                            where deo.DetalleOrdenID == id
                                            select new
                                            {
                                                deo.DetalleOrdenID,
                                                deo.EncabezadoOrdenID,
                                                deo.EmpresaID,
                                                deo.PlatoID,
                                                deo.Cantidad,
                                                deo.Comentarios,
                                                deo.DescuentosEspecial,
                                                deo.RecargarOrden,
                                                deo.Estado,
                                                deo.FechaCreacion,
                                                deo.FechaModificacion
                                            }).FirstOrDefault();

                if (undetalleOrdenesList != null)
                {
                    return Ok(undetalleOrdenesList);
                }
                return NotFound();
            }
            catch (System.Exception)
            {
                return BadRequest();
            }
        }

        [HttpPost]
        [Route("api/detalleOrdenes/agregar")]
        public IActionResult agregarDetalleOrdenes([FromBody] DetallesOrdenes detalleOrdenesNuevo)
        {
            try
            {
                IEnumerable<DetallesOrdenes> detalleOrdenesExiste = from deo in _contexto.DetallesOrdenes
                                                                    where deo.DetalleOrdenID == detalleOrdenesNuevo.DetalleOrdenID
                                                                    select deo;
                if (detalleOrdenesExiste.Count() == 0)
                {
                    _contexto.DetallesOrdenes.Add(detalleOrdenesNuevo);
                    _contexto.SaveChanges();
                    return Ok(detalleOrdenesNuevo);
                }
                return Ok(detalleOrdenesNuevo);
            }
            catch (Exception)
            {
                return BadRequest();
            }
        }

        [HttpPut]
        [Route("api/detalleOrdenes/actualizar")]
        public IActionResult actualizarDetalleOrdenes([FromBody] DetallesOrdenes actdetalleOrdenes)
        {
            try
            {
                DetallesOrdenes detalleOrdenesExiste = (from deo in _contexto.DetallesOrdenes
                                                        where deo.DetalleOrdenID == actdetalleOrdenes.DetalleOrdenID
                                                        select deo).FirstOrDefault();

                if (detalleOrdenesExiste != null)
                {
                    detalleOrdenesExiste.EncabezadoOrdenID = actdetalleOrdenes.EncabezadoOrdenID;
                    detalleOrdenesExiste.EmpresaID = actdetalleOrdenes.EmpresaID;
                    detalleOrdenesExiste.PlatoID = actdetalleOrdenes.PlatoID;
                    detalleOrdenesExiste.Cantidad = actdetalleOrdenes.Cantidad;
                    detalleOrdenesExiste.Comentarios = actdetalleOrdenes.Comentarios;
                    detalleOrdenesExiste.DescuentosEspecial = actdetalleOrdenes.DescuentosEspecial;
                    detalleOrdenesExiste.RecargarOrden = actdetalleOrdenes.RecargarOrden;
                    detalleOrdenesExiste.Estado = actdetalleOrdenes.Estado;
                    detalleOrdenesExiste.FechaCreacion = actdetalleOrdenes.FechaCreacion;
                    detalleOrdenesExiste.FechaModificacion = actdetalleOrdenes.FechaModificacion;

                    _contexto.Entry(detalleOrdenesExiste).State = EntityState.Modified;
                    _contexto.SaveChanges();
                    return Ok(detalleOrdenesExiste);
                }
                return NotFound();
            }
            catch (Exception)
            {
                return BadRequest();
            }
        }
    }
}
