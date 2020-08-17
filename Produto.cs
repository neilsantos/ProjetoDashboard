using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;


namespace Dashboard
{
    class Produto:EntidadeBase
    {
        public string Nome { get; set; }
        public Categoria Categoria { get; set; }
        public Marca Marca{ get; set; }

        public float Valor { get; set; }

        public IEnumerable<Produto> Acessorios => acessorios; 

        private readonly List<Produto> acessorios;

        public Produto(string nome, Categoria categoria, Marca marca, float valor)
        {
            Nome = nome;
            Categoria = categoria ?? throw new Exception("É Obrigatório Ter uma Categoria");
            Marca = marca ?? throw new Exception("É Obrigatório Ter uma Marca");
            Valor = valor;
            acessorios = new List<Produto>();
        }

        public void AdicionarAcessorio(string nome, float valor=0)
        {
           var produto = new Produto(nome, Categoria, Marca, valor);
            acessorios.Add(produto);
        }
        public void AdicionarAcessorio(string nome, float valor, Marca marca)
        {
            var produto = new Produto(nome, Categoria, marca, valor);
            acessorios.Add(produto);
        }
        public void DeletarAcessorio(string nome)
        {
            if (!acessorios.Any())
            {
                Console.WriteLine("Não Há Acessorios Cadastrados");
                Console.ReadKey();
                return;
            }
            var item = acessorios.FirstOrDefault(X => X.Nome == nome);
            if (item == null)
            {
                Console.WriteLine("Item Não Encontrado!");
                return;
            }
            Console.WriteLine("Nome: " + item.Nome);
            Console.WriteLine("Categoria: " + item.Categoria.Nome);
            Console.WriteLine("Marca: " + item.Marca.Nome);
            Console.WriteLine("Valor: " + item.Valor);
            Console.WriteLine("\n Deseja mesmo Excluir?(S/N)");
            var opcao = Console.ReadLine().ToUpper();
            if (opcao == "S")
            {
                acessorios.Remove(item);
                Console.WriteLine("Acessorio Removido com Sucesso");

            }
            if (opcao == "N")
            {
                Console.WriteLine("Operação Cancelada!");

            }
        }

        public bool AtualizarAcessorio(string nome, Marca marca, float valor)
        {
            bool status = false;
            
            return status;
        }

        public Produto PocurarAcessorio(string nome)
        {
            var acessorio = acessorios.FirstOrDefault(X => X.Nome == nome);
            return acessorio;
        }
    }






}
