﻿using APICatalago.Context;
using APICatalago.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace APICatalago.Controllers
{
    [Route("[controller]")]
    [ApiController]
    public class CategoriasController : ControllerBase
    {
        private readonly AppDbContext _context;

        public CategoriasController(AppDbContext context)
        {
            _context = context;
        }

        [HttpGet("produtos")]
        public ActionResult<IEnumerable<Categoria>> GetCategoriasProdutos()
        {
            try
            {
                return _context.Categorias.Include(p => p.Produtos).ToList();// Include pega uma referencia
            }
            catch (Exception)
            {

                return StatusCode(StatusCodes.Status500InternalServerError,
                    "Ocorreu um problema ao tratar  a sua solicitação. ");
            }       
        }

        [HttpGet]
        public ActionResult<IEnumerable<Categoria>> Get()
        {

            try
            {
                return _context.Categorias.AsNoTracking().ToList(); // AsNoTracking Otimiza o desempenho, indicado apenas para o leitura sem necessidade de alteração

            }
            catch (Exception)
            {

                return StatusCode(StatusCodes.Status500InternalServerError,
                    "Ocorreu um problema ao tratar  a sua solicitação.");
            }
        }

        [HttpGet("{id:int}", Name = "ObterCategoria")]

        public ActionResult<Categoria> Get(int id)
        {

            try
            {
                var categoria = _context.Categorias.FirstOrDefault(p => p.CategoriaId == id);

                if (categoria is null)
                {
                    return NotFound($"Categoria com id= {id} não encontrada...");
                }
                return Ok(categoria);

            }
            catch (Exception)
            {

                return StatusCode(StatusCodes.Status500InternalServerError,
                    "Ocorreu um problema ao tratar  a sua solicitação. ");
            }
           
        }

       

        [HttpPost]
        public ActionResult Post(Categoria categoria)
        {
            try
            {
                if (categoria is null)
                    return BadRequest("Dados inválidos");

                _context.Categorias.Add(categoria);
                _context.SaveChanges();

                return new CreatedAtRouteResult("ObterCategoria", new { id = categoria.CategoriaId }, categoria);
            }
            catch (Exception)
            {

                return StatusCode(StatusCodes.Status500InternalServerError,
                    "Ocorreu um problema ao tratar  a sua solicitação. ");
            }
           
        }

        [HttpPut("{id:int}")]

        public ActionResult Put(int id, Categoria categoria)
        {
            try
            {
                if (id != categoria.CategoriaId)
                {
                    return BadRequest("Dados inválidos");
                }
                _context.Entry(categoria).State = EntityState.Modified;
                _context.SaveChanges();

                return Ok(categoria);
            }
            catch (Exception)
            {

                return StatusCode(StatusCodes.Status500InternalServerError,
                   "Ocorreu um problema ao tratar  a sua solicitação. ");
            }
            
        }

        [HttpDelete("{id:int}")]
        public ActionResult Delete(int id)
        {
            try
            {
                var categoria = _context.Categorias.FirstOrDefault(p => p.CategoriaId == id);

                if (categoria is null)
                {
                    return NotFound($"Categoria com id={id} não encontrada...");
                }
                _context.Categorias.Remove(categoria);
                _context.SaveChanges();

                return Ok(categoria);
            }
            catch (Exception)
            {

                return StatusCode(StatusCodes.Status500InternalServerError,
                   "Ocorreu um problema ao tratar  a sua solicitação. ");
            }
           
        }

    }
}
