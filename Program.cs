using System;
using System.Collections;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Threading;

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
            int op=1;
            while (op!=0)
            {
                Console.Clear();
                Console.WriteLine("\n\n1 - Cadastrar Marca\n");
                Console.WriteLine("2 - Cadastrar Categoria\n");
                Console.WriteLine("3 - Cadastrar Produto\n ");
                Console.WriteLine("4 - Exibir Marcas e Categorias\n ");
                Console.WriteLine("5 - Listar Inventário\n");
                Console.WriteLine("6 - Mostrar por Categoria\n");
                Console.WriteLine("7 - Mostrar por Marca\n");
                Console.WriteLine("8 - Remover Categoria\n");
                Console.WriteLine("9 - Remover Marca\n");
                Console.WriteLine("10 - Remover Produto\n");
                Console.WriteLine("11 - Atualizar Categoria\n");
                Console.WriteLine("12 - Atualizar Marca\n");
                Console.WriteLine("13 - Atualizar Produto\n");
              
                Console.WriteLine("Escolha uma opção: ");
                op = Convert.ToInt32(Console.ReadLine());

                switch (op)
                {
                    case 1:
                        Console.Clear();
                        Console.WriteLine("Cadastrar Marca \n");
                        Mostrar(marcas);
                        CadastrarMarca(marcas);
                        break;

                    case 2:
                        
                        Console.Clear();
                        Console.WriteLine("Cadastrar Categoria \n");
                        Mostrar(categorias);
                        CadastrarCategoria(categorias);
                        break;

                    case 3:
                        CadastrarProduto(marcas,categorias,inventario);
                        break;

                    case 4:
                        Console.Clear();
                        Mostrar(categorias);
                        Mostrar(marcas);
                        Console.ReadKey();
                        break;

                    case 5:
                        Console.Clear();
                        MostrarInventario(inventario);
                        Console.ReadKey();
                        break;

                    case 6:
                        Console.Clear();
                        if (!inventario.Any())
                        {
                            Console.WriteLine("Não Há Itens a ser mostrado");
                            Thread.Sleep(2000);
                            break;
                        }
                        var categoria = LerCategoria(categorias);
                        Console.WriteLine("Mostrar Por CategoriaS\n");
                        MostrarPor(categoria, inventario);
                        break;
                    case 7:
                        Console.Clear();
                        if (!inventario.Any())
                        {
                            Console.WriteLine("Não Há Itens a ser mostrado");
                            Thread.Sleep(2000);
                            break;
                        }
                        var marca = LerMarca(marcas);
                        Console.WriteLine("Mostrar Por Marca\n");
                        MostrarPor(marca, inventario);
                        break;

                    case 8:
                        Console.Clear();
                        Mostrar(categorias);
                        Deletar(categorias);
                        break;
                    
                    case 9:
                        Mostrar(marcas);
                        Deletar(marcas);
                        break;

                    case 10:
                        Console.Clear();
                        Console.WriteLine("REMOVER PRODUTO");
                        MostrarInventario(inventario);
                        Deletar(inventario);
                        break;

                    case 11:
                        Mostrar(categorias);
                        Atualizar(categorias);
                        break;

                    case 12:
                        Console.Clear();
                        Mostrar(marcas);
                        Atualizar(marcas);
                        break;

                    case 13:
                        Console.Clear();
                        MostrarInventario(inventario);
                        Atualizar(inventario,marcas);
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
           
            if (!inventario.Any())
            {
                Console.WriteLine("Nenhum produto para listar");
                return;
            }

            foreach (var Produto in inventario)
            {
                Console.WriteLine("ID: " + Produto.Id);
                Console.WriteLine("Produto: " + Produto.Nome);
                Console.WriteLine("Marca: " + Produto.Marca.Nome);
                Console.WriteLine("Categoria: " + Produto.Categoria?.Nome);
                Console.WriteLine("Valor: " + Produto.Valor);
                Console.WriteLine("\n Acessórios: ");
                if (!Produto.Acessorios.Any())
                {
                    Console.WriteLine("Não Há Acessorios Cadastrados");
                    continue;
                }
                foreach (var acessorio in Produto.Acessorios)
                {
                    Console.WriteLine("\n\tNome:" + acessorio.Nome);
                    Console.WriteLine("\tMarca:" + acessorio.Marca.Nome);
                    Console.WriteLine("\tCategoria:" + acessorio.Categoria.Nome);
                    var valor = acessorio.Valor == 0 ? "Stock Item" : acessorio.Valor.ToString();
                    Console.WriteLine("\tValor: " + valor );
                }
                Console.WriteLine("\n\n");
            }
            
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

            Console.WriteLine("\nDeseja adicionar acessórios (S para adicionar)");
            var escolha = Console.ReadLine();
            escolha = escolha.ToUpper();
            if (escolha == "S")
            {
                var qtd = LerInteiro("Quantos itens quer cadastrar?\n");
                for (int i = 0; i < qtd; i++)
                {
                    Console.WriteLine(i + 1 + "º item\n Nome:");
                    var acessorioNome = Console.ReadLine();
                    novoProduto.AdicionarAcessorio(acessorioNome);
                }
            }
            

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
        private static Produto LerProduto(List<Produto> inventario)
        {
            var eValido = true;
            int idProduto;
            var idsInventario = inventario.Select(X => X.Id);
            do
            {
                Console.Clear();
                MostrarInventario(inventario);
                Console.WriteLine("Informe o ID do Produto:\n");

                eValido = int.TryParse(Console.ReadLine(), out idProduto);
                if (!eValido)
                {
                    Console.WriteLine("É necessário informar um numero da lista");
                }

                if (!idsInventario.Contains(idProduto))
                {
                    Console.WriteLine("Escolha apenas os Itens acima");
                    Console.ReadKey();
                }

            } while (!idsInventario.Contains(idProduto) || !eValido);
            var produto = inventario.FirstOrDefault(X => X.Id == idProduto);

            return produto;
        }
        private static void MostrarPor(Categoria categoriaSelecionada, List<Produto> inventario)
        {
            var print = inventario.Where(X => X.Categoria == categoriaSelecionada);
            MostrarInventario(print);
        }
        private static void MostrarPor(Marca marcaSelecionada, List<Produto> inventario)
        {

            
            var print = inventario.Where(X => X.Marca == marcaSelecionada);

            MostrarInventario(print);
        }
        private static void CadastrarMarca(List<Marca> marcas)
        {
            Console.WriteLine("Informe a Nova Marca");
            string nomeMarca = Console.ReadLine();
            bool Existe = marcas.Where(X=>X.Nome == nomeMarca).Any();

            if (Existe)
            {
                Console.WriteLine("ESSA MARCA JA EXISTE, ADICIONE UMA DIFERENTE");
                Console.ReadKey();
                return;
            }

            int idMarca=0;

            if (marcas.Any())
            {
                int maiorId = marcas.Max(X => X.Id);
                idMarca = ++maiorId;
            }

            Marca novaMarca = new Marca(idMarca,nomeMarca);

            marcas.Add(novaMarca);

        }
        private static void CadastrarCategoria(List<Categoria> categorias)
        {
            

            Console.WriteLine("\nInforme a Nova Categoria");
            string nomeCategoria = Console.ReadLine();
            bool Existe = categorias.Where(X => X.Nome == nomeCategoria).Any();

            if (Existe)
            {
                Console.WriteLine("ESSA CATEGORIA JA EXISTE, ADICIONE UMA DIFERENTE");
                Console.ReadKey();
                return;
            }

            int idCategoria = 0;

            if (categorias.Any())
            {
                int maiorId = categorias.Max(X => X.Id);
                idCategoria = ++maiorId;
            }

            Categoria novaCategoria = new Categoria(idCategoria, nomeCategoria);

            categorias.Add(novaCategoria);

        }
        private static void Deletar(List<Categoria> categorias)
        {

            
            if (!categorias.Any())
            {
                Console.WriteLine("Nenhum item em categoria");
                return;
            }
            Console.WriteLine("\nInforme o ID a ser deletado");
            bool eValido = int.TryParse(Console.ReadLine(), out int id);
            if (!eValido)
            {
                Console.WriteLine("ID Não Encontrado, escolha um item da lista");
                Console.ReadKey();
                return;
            }
            var confirmar = categorias.FirstOrDefault(X => X.Id == id);

            Console.WriteLine("\nID: " + confirmar.Id);
            Console.WriteLine("Nome: " + confirmar.Nome);

            string op = "";
            Console.WriteLine("\nDeseja Excluir? (S / N)");
            op = Console.ReadLine();
            op = op.ToUpper();
            if (op == "S")
            {
                categorias.Remove(confirmar);
                Console.WriteLine("Ítem Removido com sucesso!");
                Thread.Sleep(2000);
            }
            if (op == "N")
            {
                Console.WriteLine("Operação Cancelada");
                Thread.Sleep(2000);
                return;
            }


        }
        private static void Deletar(List<Marca> marcas)
        {
            if (!marcas.Any())
            {
                Console.WriteLine("Nenhum item em categoria");
                return;
            }
            var confirmar = LerMarca(marcas);
        
            Console.WriteLine("\nID: " + confirmar.Id);
            Console.WriteLine("Nome: " + confirmar.Nome);

            string op = "";
            Console.WriteLine("\nDeseja Excluir? (S / N)");
            op = Console.ReadLine();
            op = op.ToUpper();
            if (op == "S")
            {
                marcas.Remove(confirmar);
                Console.WriteLine("Ítem Removido com sucesso!");
                Thread.Sleep(2000);
            }
            if (op == "N")
            {
                Console.WriteLine("Operação Cancelada.");
                Thread.Sleep(2000);
                return;
            }

        }
        private static void Deletar(List<Produto> inventario)
        {
            if (!inventario.Any())
            {
                Console.WriteLine("Nenhum item na lista");
                return;
            }
            var confirmar = LerProduto(inventario);
        
            Console.WriteLine("\nID: " + confirmar.Id);
            Console.WriteLine("Nome: " + confirmar.Nome);

            string op = "";
            Console.WriteLine("\nDeseja Excluir?(S / N)");
            op = Console.ReadLine();
            op = op.ToUpper();
            if (op == "S")
            {
                inventario.Remove(confirmar);
                Console.WriteLine("Ítem Removido com sucesso!");
                Thread.Sleep(2000);
            }
            if (op == "N")
            {
                Console.WriteLine("Operação Cancelada.");
                Thread.Sleep(2000);
                return;
            }

        }
        private static void Atualizar(List<Categoria> categorias)
        {
            if (!categorias.Any())
            {
                Console.WriteLine("Primeiro faça o cadastro");
                Console.ReadKey();
                return;
            }
            var item = LerCategoria(categorias);
            Console.WriteLine("Informe o Novo nome: ");
            string novoNome = Console.ReadLine();
            Console.WriteLine("Atualizar:\n");
            Console.WriteLine("ID "+ item.Id);
            Console.WriteLine("Nome "+ novoNome +"\n");
            Console.WriteLine("CONFIRMA? (S / N) \n");
            string opcao = Console.ReadLine();
            opcao = opcao.ToUpper();
            if (opcao == "N")
            {
                return;
            }
            if (opcao == "S")
            {
                item.Nome = novoNome;
                Console.WriteLine("Alterado com sucesso!");
                Console.ReadKey();
                
            }
        }
        private static void Atualizar(List<Marca> marcas)
        {
            if (!marcas.Any())
            {
                Console.WriteLine("Primeiro faça o cadastro");
                Console.ReadKey();
                return;
            }
            var item = LerMarca(marcas);
            Console.WriteLine("Informe o Novo nome: ");
            string novoNome = Console.ReadLine();
            Console.WriteLine("Atualizar:\n");
            Console.WriteLine("ID " + item.Id);
            Console.WriteLine("Nome " + novoNome + "\n");
            Console.WriteLine("CONFIRMA? (S / N) \n");
            string opcao = Console.ReadLine();
            opcao = opcao.ToUpper();
            if (opcao == "N")
            {
                return;
            }
            if (opcao == "S")
            {
                item.Nome = novoNome;
                Console.WriteLine("Alterado com sucesso!");
                Console.ReadKey();

            }
        }
        private static void Atualizar(List<Produto> inventario, List<Marca> marcas)
        {
            if (!inventario.Any())
            {
                Console.WriteLine("Primeiro faça o cadastro");
                Console.ReadKey();
                return;
            }

            var item = LerProduto(inventario);
            Console.WriteLine("\tA - Alterar dados do produto\n\tB - Adicionar Acessório\n\tR - Remover Acessório\n\tE - Editar Acessório");
            string opcao = Console.ReadLine().ToUpper();

            if (opcao == "A")
            {

                Console.WriteLine("Informe o Novo nome: ");
                string novoNome = Console.ReadLine();
                Console.WriteLine("Atualizar:\n");
                Console.WriteLine("ID " + item.Id);
                Console.WriteLine("Nome " + novoNome + "\n");
                Console.WriteLine("CONFIRMA? (S / N) \n");
                string opcao2 = Console.ReadLine();
                opcao2 = opcao2.ToUpper();
                
                if (opcao2 != "S")
                {
                    Console.WriteLine("Operação Cancelada!");
                    return;
                }
                    item.Nome = novoNome;
                    Console.WriteLine("Alterado com sucesso!");
                    Console.ReadKey();
            }
            if (opcao == "B")
            {
                AdicionarAcessorios(marcas, item);
            }
            if (opcao == "R")
            {
                RemoverAcessorios(item);
            }
            if (opcao =="E")
            {
                EditarAcessorios(marcas, item);
            }
        }

        private static void RemoverAcessorios(Produto item)
        {
            if (!item.Acessorios.Any())
            {
                Console.WriteLine("Não há acessorios cadastrados");
                Console.ReadKey();
                return;
            }
            Console.WriteLine("\nInforme o Nome do Acessorio: ");
            var nome = Console.ReadLine();
            item.DeletarAcessorio(nome);
            Console.ReadKey();
        }

        private static void EditarAcessorios(List<Marca> marcas, Produto item)
        {
            if (!item.Acessorios.Any())
            {
                Console.WriteLine("Não Há Acessorios Cadastrados");
                Console.ReadKey();
                return;
            }
            Console.WriteLine("Informe o Nome do Acessorio a ser alterado");
            var acessorio = item.PocurarAcessorio(Console.ReadLine());
            if (acessorio == null)
            {
                Console.WriteLine("Item Não Encontrado");
                Console.ReadKey();
                return;
            }
            Console.Clear();
            Console.WriteLine("\tATENÇÃO!");
            Console.WriteLine("\nNão é possivel alterar a Categoria de um acessório.\nSe este Item não pertence a esta Categoria,\nexclua e crie dentro do produto em questão");

            Console.WriteLine("Informe o Novo Nome: ");
            var novoNomeAcessorio = Console.ReadLine();
            Console.WriteLine("Informe o Nova Marca: ");
            var novaMarcaAcessorio = LerMarca(marcas);
            Console.WriteLine("Informe a Novo Valor: ");
            var novoValorAcessorio = LerValor();

            Console.WriteLine("Nome: " + novoNomeAcessorio);
            Console.WriteLine("Categoria: " + item.Categoria.Nome);
            Console.WriteLine("Categoria: " + item.Marca.Nome);
            Console.WriteLine("Valor: " + novoValorAcessorio);
            Console.WriteLine("\n Deseja mesmo Alterar?(S/N)");
            var op = Console.ReadLine().ToUpper();

            if (op != "S")
            {
                Console.WriteLine("Operação Cancelada!");
                return;
            }
            acessorio.Nome = novoNomeAcessorio;
            acessorio.Marca = novaMarcaAcessorio;
            acessorio.Valor = novoValorAcessorio;
            Console.WriteLine("Acessorio alterado com Sucesso");
        }

        private static void AdicionarAcessorios(List<Marca> marcas, Produto item)
        {
            Console.WriteLine("\nInforme o nome do novo acessório: ");
            var nomeAcessorio = Console.ReadLine();
            var marcaAcessorio = LerMarca(marcas);
            var valorAcessorio = LerValor();


            Console.WriteLine("Nome: " + nomeAcessorio);
            Console.WriteLine("Marca: " + marcaAcessorio);
            Console.WriteLine("Valor: " + valorAcessorio);
            Console.WriteLine("Confirmar? (S / N)");
            string confirmar = Console.ReadLine().ToUpper();
            if (confirmar != "S")
            {
                Console.WriteLine("Operação Cancelada!");
                return;
            }
            item.AdicionarAcessorio(nomeAcessorio, valorAcessorio, marcaAcessorio);
        }

        private static float LerInteiro(string msg)
        {
            float valor;
            bool eValido;
            do
            {
                Console.Clear();
                Console.WriteLine(msg);

                eValido = float.TryParse(Console.ReadLine(), out valor);
                if (!eValido)
                {
                    Console.WriteLine("É necessário informar um valor Válido");
                    Console.ReadKey();
                }
            } while (!eValido);
            return valor;
        }
    }
}
