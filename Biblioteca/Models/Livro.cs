using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace Biblioteca.Models
{
    public class Livro
    {
        public virtual int Id { get; set; }
        [Required(ErrorMessage = "Um nome é obrigatório")]
        public virtual string Nome { get; set; }
        public virtual int QtdEstoque { get; set; }
        public virtual int idAutor { get; set; }
        public virtual Autor Autor { get; set; }

    }
}
