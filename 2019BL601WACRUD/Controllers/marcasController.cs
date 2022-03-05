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
    public class marcasController : ControllerBase
    {
        private readonly prestamosContext _context;

        public marcasController(prestamosContext miContexto)
        {
            this._context = miContexto;
        }

        [HttpGet]
        [Route("api/marca/")]
        public IActionResult Get()
        {
            try
            {
                IEnumerable<marcas> marcaLista = from m in _context.marcas
                                                 select m;
                if (marcaLista.Count() > 0)
                {
                    return Ok(marcaLista);
                }
                return NotFound();
            }catch(Exception)
            {
                return BadRequest();
            }
        }

        [HttpGet]
        [Route("api/marca/{idMarca}")]
        public IActionResult Get(int idMarca)
        {
            try
            {
                marcas marca = (from m in _context.marcas
                                where m.id_marcas == idMarca
                                select m).FirstOrDefault();
                if(marca != null)
                {
                    return Ok(marca);
                }
                return NotFound();
            }
            catch (Exception)
            {
                return BadRequest();
            }
        }

        [HttpPost]
        [Route("api/marca/")]
        public IActionResult guardarMarca([FromBody]marcas nuevaMarca)
        {
            try
            {
                _context.Add(nuevaMarca);
                _context.SaveChanges();
                return Ok(nuevaMarca);
            }
            catch (Exception)
            {
                return BadRequest();
            }
        }

        [HttpPut]
        [Route("api/marca/")]
        public IActionResult updateMarca([FromBody]marcas updateMarca)
        {
            try
            {
                marcas marcaExistente = (from m in _context.marcas
                                         where m.id_marcas == updateMarca.id_marcas
                                         select m).FirstOrDefault();
                if(updateMarca is null)
                {
                    return NotFound();
                }

                marcaExistente.nombre_marca = updateMarca.nombre_marca;
                marcaExistente.estados = updateMarca.estados;
                _context.Entry(marcaExistente).State = EntityState.Modified;
                _context.SaveChanges();
                return Ok(marcaExistente);
            }
            catch (Exception)
            {
                return BadRequest();
            }
        }
    }
}
