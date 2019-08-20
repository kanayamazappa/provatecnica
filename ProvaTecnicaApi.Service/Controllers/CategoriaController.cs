using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using ProvaTecnicaApi.Service.Models;
using ProvaTecnicaApi.Service.Models.Interface;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace ProvaTecnicaApi.Service.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [Authorize("Bearer")]
    public class CategoriaController : ControllerBase
    {
        private readonly IGenericRepository<Categoria> _categoria;

        public CategoriaController(IGenericRepository<Categoria> categoria)
        {
            _categoria = categoria;
        }

        [HttpGet]
        public async Task<ActionResult<IEnumerable<Categoria>>> Get()
        {
            try
            {
                var categorias = await _categoria.GetAll();

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
                var categoria = await _categoria.Get(c => c.IdCategoria == IdCategoria);

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
                await _categoria.Insert(categoria);

                return Ok();
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [HttpPut]
        public async Task<ActionResult> Put([FromBody] Categoria categoria)
        {
            try
            {
                await _categoria.Update(categoria);

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
                await _categoria.Delete(IdCategoria);

                return Ok();
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }
    }
}
