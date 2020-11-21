using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace Biblioteca.Models
{
    public class Autor
    {
        public virtual int Id { get; set; }
        [Required(ErrorMessage = "Um nome é obrigatório")]
        public virtual string Nome { get; set; }
        //public virtual IList<Livro> Livros { get; set; }

        //public Autor()
        //{
        //    this.Livros = new List<Livro>();
        //}

    }
}
