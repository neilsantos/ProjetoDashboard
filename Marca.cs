using System;
using System.Collections.Generic;
using System.Text;

namespace Dashboard
{
    class Marca:EntidadeBase
    {
        public string Nome { get; set; }
        
        public Marca(int id, string nome)
        {
            Id = id;
            Nome = nome;
        }

    }
}
