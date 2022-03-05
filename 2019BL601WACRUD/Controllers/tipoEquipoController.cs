using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using _2019BL601WACRUD.Models;
using Microsoft.EntityFrameworkCore;

namespace _2019BL601WACRUD.Controllers
{
    //[Route("api/[controller]")]
    [ApiController]
    public class tipoEquipoController : ControllerBase
    {
        private readonly prestamosContext _context;

        public tipoEquipoController(prestamosContext miContexto)
        {
            this._context = miContexto;
        }

        [HttpGet]
        [Route("api/tipo_equipo/")]
        public IActionResult Get()
        {
            try
            {
                IEnumerable<tipo_equipo> tipoEquipoLista = from te in _context.tipo_equipo select te;
                if (tipoEquipoLista.Count() > 0)
                {
                    return Ok(tipoEquipoLista);
                }
                return NotFound();
            }
            catch (Exception)
            {
                return BadRequest();
            }
        }

        [HttpGet]
        [Route("api/tipo_equipo/{idTipo_Equipo}")]
        public IActionResult Get(int idTipo_Equipo)
        {
            try
            {
                tipo_equipo tipo_equipo = (from te in _context.tipo_equipo
                                           where te.id_tipo_equipo == idTipo_Equipo
                                           select te).FirstOrDefault();
                if(tipo_equipo != null)
                {
                    return Ok(tipo_equipo);
                }
                return NotFound();
            }
            catch (Exception)
            {
                return BadRequest();
            }
        }
        [HttpPost]
        [Route("api/tipo_equipo/")]
        public IActionResult guardarTipoEqui([FromBody]tipo_equipo newTipoEqui)
        {
            try
            {
                _context.Add(newTipoEqui);
                _context.SaveChanges();
                return Ok(newTipoEqui);
            }
            catch (Exception)
            {
                return BadRequest();
            }
        }

        [HttpPut]
        [Route("api/tipo_equipo/")]
        public IActionResult updateTipoEqui([FromBody]tipo_equipo updateTipoEqui)
        {
            try
            {
                tipo_equipo tipoExiste = (from te in _context.tipo_equipo
                                          where te.id_tipo_equipo == updateTipoEqui.id_tipo_equipo
                                          select te).FirstOrDefault();
                if(updateTipoEqui is null)
                {
                    return NotFound();
                }

                tipoExiste.descripcion = updateTipoEqui.descripcion;
                tipoExiste.estado = updateTipoEqui.estado;
                _context.Entry(tipoExiste).State = EntityState.Modified;
                _context.SaveChanges();
                return Ok(tipoExiste);
            }
            catch (Exception)
            {
                return BadRequest();
            }
        }
    }
}
