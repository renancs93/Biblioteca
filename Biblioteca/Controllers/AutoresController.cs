using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Biblioteca.Models;
using Microsoft.AspNetCore.Mvc;
using NHibernate;
using NHibernate.Linq;

namespace Biblioteca.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class AutoresController : Controller
    {
        private readonly ISession _session;

        public AutoresController(ISession session)
        {
            _session = session;
        }

        public IActionResult Index()
        {
            return View();
        }

        [HttpGet]
        public IEnumerable<Autor> Get()
        {
            //return View();
            return _session.Query<Autor>().ToArray();
        }

        [HttpPost]
        public async Task<ActionResult<Autor>> Create(Autor autor)
        {
            if (ModelState.IsValid)
            {
                using(ITransaction transaction = _session.BeginTransaction())
                {
                    await _session.SaveAsync(autor);
                    await transaction.CommitAsync();
                    return autor;
                }
            }

            return View(autor);
            
        }

    }
}
