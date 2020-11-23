using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Biblioteca.Models;
using Microsoft.AspNetCore.Mvc;
using NHibernate;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace Biblioteca.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class LivroController : ControllerBase
    {
        private readonly ISession _session;

        public LivroController(ISession session)
        {
            _session = session;
        }

        // GET: api/<LivroController>
        [HttpGet]
        public IEnumerable<Livro> Get()
        {
            //return _session.CreateCriteria<Livro>().SetFetchMode("Autor", FetchMode.Eager).List<Livro>();
            return _session.Query<Livro>();
        }

        // GET api/<LivroController>/5
        [HttpGet("{id}")]
        public Livro Get(int id)
        {
            return _session.Query<Livro>().Where(x => x.Id == id).FirstOrDefault();
        }

        // POST api/<LivroController>
        [HttpPost]
        public async Task<ActionResult<Livro>> Post([FromBody] Livro livro)
        {
            if (ModelState.IsValid)
            {
                using (ITransaction transaction = _session.BeginTransaction())
                {
                    // Carregar Autor 
                    if(livro.idAutor > 0)
                    {
                        Autor autor = await _session.LoadAsync<Autor>(livro.idAutor);
                        livro.Autor = autor;
                    }

                    await _session.SaveAsync(livro);
                    await transaction.CommitAsync();
                    return livro;
                }
            }
            return livro;

        }

        // PUT api/<AutorController>/5
        [HttpPut("{id}")]
        public async Task<ActionResult<Livro>> Put(int id, [FromBody] Livro livro)
        {
            if (ModelState.IsValid)
            {
                using (ITransaction transaction = _session.BeginTransaction())
                {
                    // Verifica se existe o objeto a ser atualizado
                    var item = await _session.LoadAsync<Livro>(id);// await _session.GetAsync<Autor>(id);

                    if (item == null)
                        return NotFound();

                    if(livro.idAutor > 0)
                    {
                        var newAutor = await _session.LoadAsync<Autor>(livro.idAutor);
                        item.Autor = newAutor;
                    }
                    else
                        item.Autor = null;

                    item.QtdEstoque = livro.QtdEstoque;
                    item.Nome = livro.Nome;

                    await _session.MergeAsync(item);
                    await transaction.CommitAsync();
                    return item;

                }
            }
            return livro;
        }

        //[HttpPatch("{id}")]
        //public async Task<ActionResult<Livro>> Patch(int id, [FromBody] JsonPatchDocument<Livro> patch)
        //{
        //    if (ModelState.IsValid)
        //    {
        //        using (ITransaction transaction = _session.BeginTransaction())
        //        {
        //            // Verifica se existe o objeto a ser atualizado
        //            //var item = Get(id);
        //            var item = await _session.LoadAsync<Livro>(id);

        //            if (item == null)
        //                return NotFound();

        //            patch.ApplyTo(item, (Microsoft.AspNetCore.JsonPatch.Adapters.IObjectAdapter)ModelState);

        //            if(!ModelState.IsValid)
        //                return BadRequest();

        //            //livro.Id = id;
        //            await _session.MergeAsync(patch);
        //            await transaction.CommitAsync();
        //            return item;
        //        }
        //    }
        //    return BadRequest();
        //}

        // DELETE api/<LivroController>/5
        [HttpDelete("{id}")]
        public async Task<ActionResult> Delete(int id)
        {
            using (ITransaction transaction = _session.BeginTransaction())
            {
                // Carrega o objeto a ser deletado
                var item = await _session.GetAsync<Livro>(id);

                try
                {
                    await _session.DeleteAsync(item);
                    await transaction.CommitAsync();
                }
                catch (Exception ex)
                {
                    transaction.Rollback();
                    return StatusCode(403, new CustomException("Ocorreu um erro inesperado ao apagar livro"));
                }
                return Ok();
            }
        }
    }
}
