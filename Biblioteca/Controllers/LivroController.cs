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
                    await _session.SaveAsync(livro);
                    await transaction.CommitAsync();
                    return livro;
                }
            }
            return livro;

        }

        // PUT api/<LivroController>/5
        [HttpPut("{id}")]
        public async Task<ActionResult<Livro>> Put(int id, [FromBody] Livro livro)
        {
            if (ModelState.IsValid)
            {
                using (ITransaction transaction = _session.BeginTransaction())
                {
                    // Verifica se existe o objeto a ser atualizado
                    var item = Get(id);// await _session.GetAsync<Autor>(id);

                    if (item == null)
                        return NotFound();

                    livro.Id = id;
                    await _session.MergeAsync(livro);
                    await transaction.CommitAsync();
                    return livro;

                }
            }
            return livro;
        }

        // DELETE api/<LivroController>/5
        [HttpDelete("{id}")]
        public async Task<ActionResult> Delete(int id)
        {
            using (ITransaction transaction = _session.BeginTransaction())
            {
                // Carrega o objeto a ser deletado
                var item = await _session.GetAsync<Livro>(id);

                if (item != null)
                {
                    await _session.DeleteAsync(item);
                    await transaction.CommitAsync();
                }
                return Ok();
            }
        }
    }
}
