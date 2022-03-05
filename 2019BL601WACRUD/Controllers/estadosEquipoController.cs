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
    public class estadosEquipoController : ControllerBase
    {
        private readonly prestamosContext _context;

        public estadosEquipoController(prestamosContext miContexto)
        {
            this._context = miContexto;
        }

        [HttpGet]
        [Route("api/estados_equipo")]
        public IActionResult Get()
        {
            try
            {
                IEnumerable<estados_equipo> estadoEquipo_List = from ee in _context.estados_equipo
                                                                select ee;
                if(estadoEquipo_List.Count()>0)
                {
                    return Ok(estadoEquipo_List);
                }
                return NotFound();
            }
            catch(Exception)
            {
                return BadRequest();
            }
        }

        [HttpGet]
        [Route("api/estado_equipo/{idEstadoEquipo}")]
        public IActionResult Get(int idEstadoEquipo)
        {
            try
            {
                estados_equipo estados_equipo = (from ee in _context.estados_equipo
                                                 where ee.id_estados_equipo == idEstadoEquipo
                                                 select ee).FirstOrDefault();
                if(estados_equipo != null)
                {
                    return Ok(estados_equipo);
                }
                return NotFound();
            }
            catch (Exception)
            {
                return BadRequest();
            }
        }

        [HttpPost]
        [Route("api/estado_equipo/")]
        public IActionResult guardarEstado([FromBody] estados_equipo estadoNuevo)
        {
            try
            {
                _context.Add(estadoNuevo);
                _context.SaveChanges();
                return Ok(estadoNuevo);
            }
            catch (Exception)
            {
                return BadRequest();
            }
        }

        [HttpPut]
        [Route("api/estado_equipo/")]
        public IActionResult updateEstado([FromBody] estados_equipo estadoUpdate)
        {
            try
            {
                estados_equipo estadoExiste = (from ee in _context.estados_equipo
                                               where ee.id_estados_equipo == estadoUpdate.id_estados_equipo
                                               select ee).FirstOrDefault();
                if(estadoUpdate is null)
                {
                    return NotFound();
                }

                estadoExiste.descripcion = estadoUpdate.descripcion;
                estadoExiste.estado = estadoUpdate.estado;
                _context.Entry(estadoExiste).State = EntityState.Modified;
                _context.SaveChanges();
                return Ok(estadoExiste);
            }
            catch (Exception)
            {
                return BadRequest();
            }
        }
    }
}