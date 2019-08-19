using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using ProvaTecnicaApi.Service.Models;

namespace ProvaTecnicaApi.Service.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [Authorize("Bearer")]
    public class ProdutoController : ControllerBase
    {
        private readonly ProvaTecnicaApiContext context;

        public ProdutoController(ProvaTecnicaApiContext _context)
        {
            context = _context;
        }

        [HttpGet]
        public async Task<ActionResult<IEnumerable<Produto>>> Get()
        {
            try
            {
                var produtos = await context.Produtos.Include(p => p.Categoria).Where(c => c.Ativo == true).ToListAsync();

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
                var produto = await context.Produtos.Include(p => p.Categoria).Where(p => p.IdProduto == IdProduto).FirstOrDefaultAsync();

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
                if (produto.Categoria != null && produto.Categoria.IdCategoria > 0)
                {
                    var categoria = await context.Categorias.FindAsync(produto.Categoria.IdCategoria);
                    produto.Categoria = categoria;
                }

                await context.Produtos.AddAsync(produto);
                await context.SaveChangesAsync();

                return Ok();
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [HttpPut("{IdProduto}")]
        public async Task<ActionResult> Put(int IdProduto, [FromBody] Produto _produto)
        {
            try
            {
                var produto = await context.Produtos.Where(p => p.IdProduto == IdProduto).FirstOrDefaultAsync();

                if (produto == null) return NotFound();

                if (produto.Categoria != null && produto.Categoria.IdCategoria > 0)
                {
                    var categoria = await context.Categorias.FindAsync(produto.Categoria.IdCategoria);
                    produto.Categoria = categoria;
                }

                produto.Nome = _produto.Nome;
                produto.Ativo = _produto.Ativo;
                if(produto.Categoria != null)
                    produto.Categoria = _produto.Categoria;

                context.Entry(produto).State = EntityState.Modified;

                await context.SaveChangesAsync();

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
                var produto = await context.Produtos.Where(p => p.IdProduto == IdProduto).FirstOrDefaultAsync();

                if (produto == null) return NotFound();

                context.Produtos.Remove(produto);

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
