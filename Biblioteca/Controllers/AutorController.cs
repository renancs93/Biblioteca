using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Biblioteca.Models;
using Microsoft.AspNetCore.Cors;
using Microsoft.AspNetCore.Mvc;
using NHibernate;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace Biblioteca.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AutorController : ControllerBase
    {
        private readonly ISession _session;

        public AutorController(ISession session)
        {
            _session = session;
        }

        // GET: api/<AutorController>
        [HttpGet]
        public IEnumerable<Autor> Get()
        {
            return _session.Query<Autor>().OrderBy(item => item.Nome);
        }

        // GET api/<AutorController>/5
        [HttpGet("{id}")]
        public Autor Get(int id)
        {
            return _session.Query<Autor>().Where(x => x.Id == id).FirstOrDefault();
        }

        // POST api/<AutorController>
        [HttpPost]
        public async Task<ActionResult<Autor>> Post([FromBody] Autor autor)
        {
            if (ModelState.IsValid)
            {
                using (ITransaction transaction = _session.BeginTransaction())
                {
                    await _session.SaveAsync(autor);
                    await transaction.CommitAsync();
                    return autor;
                }
            }
            return autor;
        }

        // PUT api/<AutorController>/5
        [HttpPut("{id}")]
        public async Task<ActionResult<Autor>> Put(int id, [FromBody] Autor autor)
        {
            if (ModelState.IsValid)
            {
                using (ITransaction transaction = _session.BeginTransaction())
                {
                    // Verifica se existe o objeto a ser atualizado
                    var item = Get(id);

                    if(item == null)
                        return NotFound();

                    autor.Id = id;
                    await _session.MergeAsync(autor);
                    await transaction.CommitAsync();
                    return autor;
                                       
                }
            }
            return autor;
        }

        // DELETE api/<AutorController>/5
        [HttpDelete("{id}")]
        public async Task<ActionResult> Delete(int id)
        {
            using (ITransaction transaction = _session.BeginTransaction())
            {
                // Carrega o objeto a ser deletado
                var item = await _session.GetAsync<Autor>(id);

                if(item != null)
                {
                    try
                    {
                        await _session.DeleteAsync(item);
                        await transaction.CommitAsync();
                    }
                    catch(Exception ex)
                    {
                        transaction.Rollback();

                        if (ex.InnerException.Message.Contains("violates foreign key constraint"))
                            return StatusCode(403, new CustomException("Não é possível apagar um Autor associado a um livro!"));

                        return StatusCode(403, new CustomException("Ocorreu um erro inesperado ao apagar autor!"));
                        
                    }
                }
                return Ok();
            }
        }
    }
}
