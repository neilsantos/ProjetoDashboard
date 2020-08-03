﻿using System;
using System.Collections;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;

namespace Dashboard
{
    class Program
    {
        static void Main(string[] args)
        {

            List<Produto> inventario = new List<Produto>();
       

            List<Categoria> categorias = new List<Categoria>()
            {
                new Categoria(1, "Eletrônicos"),
                new Categoria(2, "Móveis"),
                new Categoria(3, "Eletrodomestico"),
                new Categoria(4, "Colecionáveis"),
              

            };
            
            List<Marca> marcas = new List<Marca>()
            {
                new Marca(1, "Generico"),
                new Marca(2, "Microsoft"),
                new Marca(3, "Nintendo"),
                new Marca(4, "LG"),
                new Marca(5, "Sony"),
            };

            //Menu
            string op="1";
            while (op!="0")
            {
                Console.Clear();
                Console.WriteLine("\n\n1 - Cadastrar Produto\n");
                Console.WriteLine("2 - Contar Inventario\n ");
                Console.WriteLine("3 - Exibir Itens\n ");
                Console.WriteLine("4 - Listar Inventário\n\n");
                Console.WriteLine("5 - Mostrar por Categoria\n\n");
                Console.WriteLine("6 - Mostrar por Marca\n\n");
                Console.WriteLine("Escolha uma opção: ");
                op = Console.ReadLine();

                switch (op)
                {
                    case "1":
                        CadastrarProduto(marcas,categorias,inventario);
                        break;

                    case "2":
                        ContarInventario(inventario);
                        Console.ReadKey();
                        break;
                    case "3":
                        Console.Clear();
                        Mostrar(categorias);
                        Mostrar(marcas);
                        Console.ReadKey();
                        break;
                    case "4":
                        MostrarInventario(inventario);
                        break;
                    case "5":
                        var categoria = LerCategoria(categorias);
                        MostrarPor(categoria, inventario);
                        break;
                    default: 
                        Console.WriteLine("OPÇAO INVALIDA, ESCOLHA SOMENTE OS ITENS ACIMA");
                        Console.ReadKey();
                        break;
                }
            }
        }

        private static void Mostrar(List<Marca> marcas)
        {

            Console.WriteLine("\n\nLista de Marcas\n");

            foreach (var listarmarca in marcas)
            {

                Console.WriteLine(listarmarca.Id + " - " + listarmarca.Nome);
            }


        }
        private static void Mostrar(List<Categoria>categorias)
        {

            Console.WriteLine("Lista de Categoria\n");

            foreach (var listarcategoria in categorias)
            {

                Console.WriteLine(listarcategoria.Id + " - " + listarcategoria.Nome);
            }


        }
        private static void MostrarInventario(IEnumerable<Produto> inventario)
        {
            Console.Clear();

            bool existeItens = inventario.Any();
            if (!existeItens)
            {
                Console.WriteLine("Nenhum produto para listar");
                Console.ReadKey();
                return;
            }

            foreach (var Produto in inventario)
            {
                Console.WriteLine("ID: " + Produto.Id);
                Console.WriteLine("Produto: " + Produto.Nome);
                Console.WriteLine("Marca: " + Produto.Marca.Nome);
                Console.WriteLine("Categoria: " + Produto.Categoria?.Nome);
                Console.WriteLine("Valor: " + Produto.Valor);
                Console.WriteLine("\n\n");
            }

            Console.ReadKey();
        }
        private static void CadastrarProduto(List<Marca> marcas , List<Categoria> categorias, List<Produto> inventario)
        {
            Console.Clear();

            //titulo
            Console.WriteLine("1. CADASTRO DE PRODUTO\n");

            //Imprimir os Itens

            Console.WriteLine("\nInforme o nome do produto:\n");
            string nomeProduto = Console.ReadLine();

       
            Categoria interCat = LerCategoria(categorias);

            Marca interMarca = LerMarca(marcas);

            float valor = LerValor();

            //criando e adicionando um novo Produto ao inventario
            Produto novoProduto = new Produto(nomeProduto, interCat, interMarca, valor);

            if (inventario.Any())
            {
                int maiorId = inventario.Max(X => X.Id);
                novoProduto.Id = ++maiorId;
            }

            inventario.Add(novoProduto);

        }
        private static float LerValor()
        {
            float valor;
            bool eValido;
            do
            {
                Console.Clear();
                Console.WriteLine("Informe o valor do item;\n");

                eValido = float.TryParse(Console.ReadLine(), out valor);
                if (!eValido)
                {
                    Console.WriteLine("É necessário informar um valor Válido");
                    Console.ReadKey();
                }
            } while (!eValido);
            return valor;
        }
        private static Categoria LerCategoria(List<Categoria> categorias)
        {

            bool eValido = true;
            int idCategoria=0;
            var idsCategoria = categorias.Select(X => X.Id);
            do
            {

                Console.Clear();
                Mostrar(categorias);
                Console.WriteLine("\nInforme o id da categoria\n");

               
                    eValido = int.TryParse(Console.ReadLine(), out idCategoria);
            
              

                if (!eValido)
                {
                    Console.WriteLine("Escolha apenas os Itens acima");
                    Console.ReadKey();
                }
                if (!idsCategoria.Contains(idCategoria))
                {
                    Console.WriteLine("Escolha apenas os Itens acima");
                    Console.ReadKey();
                }

            } while (!idsCategoria.Contains(idCategoria) || !eValido);

            var categoria = categorias.FirstOrDefault(X => X.Id == idCategoria);
            
            return categoria;
        }
        private static Marca LerMarca(List<Marca> marcas)
        {
            var eValido = true;
            int idMarca;
            var idsMarca = marcas.Select(X => X.Id);
            do
            {
                Console.Clear();
                Mostrar(marcas);
                Console.WriteLine("Informe o ID da Marca;\n");

                eValido = int.TryParse(Console.ReadLine(), out idMarca);
                if (!eValido)
                {
                    Console.WriteLine("É necessário informar um numero da lista");
                }

                if (!idsMarca.Contains(idMarca))
                {
                    Console.WriteLine("Escolha apenas os Itens acima");
                    Console.ReadKey();
                }

            } while (!idsMarca.Contains(idMarca) || !eValido);
            var marca = marcas.FirstOrDefault(X => X.Id == idMarca);

            return marca;
        }
        private static void ContarInventario(List<Produto> inventario)
        {
            
                Console.WriteLine(inventario.Count());  

        }
        
        private static void MostrarPor(Categoria categoriaSelecionada, List<Produto> inventario)
        {
            Console.Clear();
            Console.WriteLine("Mostrar Por Categoria\n");

            var print = inventario.Where(X => X.Categoria == categoriaSelecionada);

            MostrarInventario(print);
        }
    }
}