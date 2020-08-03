using System;
using System.Collections.Generic;
using System.Text;

namespace Dashboard
{
    class Categoria:EntidadeBase
    {
        public string Nome { get; set; }

        public Categoria(int id, string nome)
        {
            Id = id;
            Nome = nome;
        }
    }
    
}

