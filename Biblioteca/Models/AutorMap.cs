using FluentNHibernate.Mapping;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Biblioteca.Models
{
    public class AutorMap: ClassMap<Autor>
    {

        public AutorMap()
        {
            Id(x => x.Id).Unique().GeneratedBy.Increment();
            Map(x => x.Nome);
            Table("Autor");
        }

    }
}
