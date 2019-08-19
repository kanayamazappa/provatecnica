using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using ProvaTecnicaApi.Service.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ProvaTecnicaApi.Service.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [Authorize("Bearer")]
    public class CategoriaController : ControllerBase
    {
        private readonly ProvaTecnicaApiContext context;

        public CategoriaController(ProvaTecnicaApiContext _context)
        {
            context = _context;
        }

        [HttpGet]
        public async Task<ActionResult<IEnumerable<Categoria>>> Get()
        {
            try
            {
                var categorias = await context.Categorias.Where(c => c.Ativo == true).ToListAsync();

                if (categorias == null) return NotFound();

                return Ok(categorias);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [HttpGet("{IdCategoria}")]
        public async Task<ActionResult<Categoria>> Get(int IdCategoria)
        {
            try
            {
                var categoria = await context.Categorias.Where(c => c.IdCategoria == IdCategoria).FirstOrDefaultAsync();

                if (categoria == null) return NotFound();

                return categoria;
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [HttpPost]
        public async Task<ActionResult> Post([FromBody] Categoria categoria)
        {
            try
            {
                await context.Categorias.AddAsync(categoria);
                await context.SaveChangesAsync();

                return Ok();
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [HttpPut("{IdCategoria}")]
        public async Task<ActionResult> Put(int IdCategoria, [FromBody] Categoria _categoria)
        {
            try
            {
                var categoria = await context.Categorias.Where(c => c.IdCategoria == IdCategoria).FirstOrDefaultAsync();

                if (categoria == null) return NotFound();

                categoria.Nome = _categoria.Nome;
                categoria.Ativo = _categoria.Ativo;

                context.Entry(categoria).State = EntityState.Modified;

                await context.SaveChangesAsync();

                return Ok();
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [HttpDelete("{IdCategoria}")]
        public async Task<ActionResult> Delete(int IdCategoria)
        {
            try
            {
                var categoria = await context.Categorias.Where(c => c.IdCategoria == IdCategoria).FirstOrDefaultAsync();

                if (categoria == null) return NotFound();

                context.Categorias.Remove(categoria);

                await context.SaveChangesAsync();

                return Ok();
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }
    }
}
