using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using WebApplication1.Models;
using Microsoft.EntityFrameworkCore;

namespace WebApplication1.Controllers
{
    //[Route("api/[controller]")]
    [ApiController]
    public class PagosController : ControllerBase
    {
        private readonly parcial2Context _contexto;

        public PagosController(parcial2Context miContexto)
        {
            this._contexto = miContexto;
        }

        /// <summary>
        /// Metodo para tenornar todo los registros de la tabla de PAGOS
        /// </summary>
        /// <returns></returns>
        /// 
        [HttpGet]
        [Route("api/pagos")]
        public IActionResult Get()
        {
            IEnumerable<pagos> pagosList = from e in _contexto.pagos
                                           select e;
            if (pagosList.Count() > 0)
            {
                return Ok(pagosList);
            }
            return NotFound();
        }

        /// <summary>
        /// Metodo para retornar un registro de la tabla PAGOS por ID
        /// </summary>
        /// <param name="id">Valor Entero del Campo</param>
        /// <returns></returns>
        /// 
        [HttpGet]
        [Route("api/pagos/{id}")]
        public IActionResult getbyId(int ID)
        {
            pagos unPago = (from e in _contexto.pagos
                            where e.id_pagos == ID //FILTRO POR ID
                            select e).FirstOrDefault();
            if (unPago != null)
            {
                return Ok(unPago);
            }
            return NotFound();
        }

        /// <summary>
        /// Metodo para registro nuevo en la tabla Pagos
        /// </summary>
        /// <returns></returns>
        /// 
        [HttpPost]
        [Route("api/pagos")]
        public IActionResult guardarPago([FromBody] pagos pagosNuevo)
        {
            try
            {
                IEnumerable<pagos> pagosExiste = from e in _contexto.pagos
                                                 where e.nombreTarjeta == pagosNuevo.nombreTarjeta
                                                 where e.tarjetaNumero == pagosNuevo.tarjetaNumero
                                                 select e;
                if (pagosExiste.Count() == 0)
                {
                    _contexto.pagos.Add(pagosNuevo);
                    _contexto.SaveChanges();
                    return Ok(pagosExiste);
                }
                return BadRequest(pagosExiste);


            }

            catch (System.Exception)
            {
                return BadRequest();
            }
        }


        ///<summary>
        ///Metodo para modificar un registro de la tabla Pagos
        /// </summary>
        /// <returns></returns>
        /// 
        [HttpPut]
        [Route("api/pagos")]
        public IActionResult updatePagos([FromBody] pagos pagosModificar, int id)
        {
            pagos pagosExiste = (from e in _contexto.pagos
                                 where e.id_pagos == pagosModificar.id_pagos
                                 select e).FirstOrDefault();
            if (pagosExiste is null)
            {
                return NotFound();
            }

            pagosExiste.id_empresa = pagosModificar.id_empresa;
            pagosExiste.id_orden = pagosModificar.id_orden;
            pagosExiste.id_movimientoCaja = pagosModificar.id_orden;
            pagosExiste.tipoPago = pagosModificar.tipoPago;
            pagosExiste.id_usuario = pagosModificar.id_usuario;


            _contexto.Entry(pagosExiste).State = EntityState.Modified;
            _contexto.SaveChanges();
            return Ok(pagosExiste);
        }

        [HttpDelete]
        [Route("api/pagos")]
        public IActionResult Delete([FromBody] pagos pagosEliminar,
                                    int id)
        {
            pagos pagosExiste = (from e in _contexto.pagos
                                 where e.id_pagos == pagosEliminar.id_pagos
                                 select e).FirstOrDefault();
            if (pagosEliminar == null)
            {
                return Ok(pagosEliminar);
            }
            _contexto.Entry(pagosExiste).State = EntityState.Modified;
            _contexto.SaveChanges();
            return NotFound();
        }
    }
}