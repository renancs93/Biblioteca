using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Biblioteca.Models
{
    public class Livro
    {
        public virtual int Id { get; set; }
        public virtual int IdAutor { get; set; }
        public virtual int QtdEstoque { get; set; }
    }
}
