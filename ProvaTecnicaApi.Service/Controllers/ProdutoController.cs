using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using ProvaTecnicaApi.Service.Models;
using ProvaTecnicaApi.Service.Models.Interface;

namespace ProvaTecnicaApi.Service.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [Authorize("Bearer")]
    public class ProdutoController : ControllerBase
    {
        private readonly IGenericRepository<Produto> _produto;

        public ProdutoController(IGenericRepository<Produto> produto)
        {
            _produto = produto;
        }

        [HttpGet]
        public async Task<ActionResult<IEnumerable<Produto>>> Get()
        {
            try
            {
                var produtos = await _produto.GetAll(null, new string[] { "Categoria" });

                if (produtos == null) return NotFound();

                return Ok(produtos);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [HttpGet("{IdProduto}")]
        public async Task<ActionResult<Produto>> Get(int IdProduto)
        {
            try
            {
                var produto = await _produto.Get(p => p.IdProduto == IdProduto, new string[] { "Categoria" });

                if (produto == null) return NotFound();

                return produto;
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [HttpPost]
        public async Task<ActionResult> Post([FromBody] Produto produto)
        {
            try
            {
                await _produto.Insert(produto);

                return Ok();
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [HttpPut]
        public async Task<ActionResult> Put([FromBody] Produto produto)
        {
            try
            {
                await _produto.Update(produto);

                return Ok();
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [HttpDelete("{IdProduto}")]
        public async Task<ActionResult> Delete(int IdProduto)
        {
            try
            {
                await _produto.Delete(IdProduto);

                return Ok();
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }
    }
}
