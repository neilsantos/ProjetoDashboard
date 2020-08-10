using System;
using System.Collections.Generic;
using System.Text;

namespace Dashboard
{
    class Produto:EntidadeBase
    {
        public string Nome { get; set; }
        public Categoria Categoria { get; set; }
        public Marca Marca{ get; set; }

        public float Valor { get; set; }

        public IEnumerable<Produto> SubItens { get; set; }

        private List<Produto> subItens;


        public Produto(string nome, Categoria categoria, Marca marca, float valor)
        {
            Nome = nome;
            Categoria = categoria ?? throw new Exception("É Obrigatório Ter uma Categoria");
            Marca = marca ?? throw new Exception("É Obrigatório Ter uma Marca");
            Valor = valor;

        }

    }






}
