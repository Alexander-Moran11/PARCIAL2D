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
    public class mesaController : ControllerBase
    {
        private readonly parcial2Context _contexto;

        public mesaController(parcial2Context miContexto)
        {
            this._contexto = miContexto;
        }

        [HttpGet]
        [Route("api/mesa")]

        public IActionResult Get()
        {
            IEnumerable<mesa> mesaList = from m in _contexto.mesas
                                         select m;
            if (mesaList.Count() > 0)
            {
                return Ok(mesaList);
            }
            return NotFound();
        }


        [HttpPost]
        [Route("api/mesa")]
        public IActionResult guardarContains([FromBody] mesa mesaNuevo)
        {
            try
            {
                IEnumerable<mesa> mesaExiste = from m in _contexto.mesas
                                               where m.MesaID == mesaNuevo.MesaID
                                               where m.Estado == mesaNuevo.Estado
                                               select m;
                if (mesaExiste.Count() == 0)
                {
                    _contexto.mesas.Add(mesaNuevo);
                    _contexto.SaveChanges();
                    return Ok(mesaNuevo);
                }
                return BadRequest(mesaExiste);

            }
            catch (System.Exception)
            {
                return BadRequest();
            }
        }

        [HttpDelete]
        [Route("api/mesa")]
        public IActionResult Delete([FromBody] mesa mesaEliminar,
                                    int id)
        {
            mesa mesaExiste = (from p in _contexto.mesas
                               where p.MesaID == mesaEliminar.MesaID
                               select p).FirstOrDefault();
            if (mesaEliminar == null)
            {
                return Ok(mesaEliminar);
            }
            return NotFound();
        }

        [HttpPut]
        [Route("api/mesa")]
        public IActionResult updateMesa([FromBody] mesa mesaModificar, int id)
        {
            mesa mesaExiste = (from m in _contexto.mesas
                               where m.MesaID == mesaModificar.MesaID
                               select m).FirstOrDefault();
            if (mesaExiste is null)
            {
                return NotFound();
            }

            mesaExiste.MesaID = mesaModificar.MesaID;
            mesaExiste.EmpresaID = mesaModificar.EmpresaID;
            mesaExiste.DescripcionMesa = mesaModificar.DescripcionMesa;
            mesaExiste.ZonaMesa = mesaModificar.ZonaMesa;
            mesaExiste.SillasMesa = mesaModificar.SillasMesa;
            mesaExiste.Estado = mesaModificar.Estado;
            mesaExiste.FechaCreacion = mesaModificar.FechaCreacion;
            mesaExiste.FechaModificacion = mesaModificar.FechaModificacion;


            _contexto.Entry(mesaExiste).State = EntityState.Modified;
            _contexto.SaveChanges();
            return Ok(mesaExiste);
        }

    }
}
