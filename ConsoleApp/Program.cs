using System;
using System.Collections.Generic;
using System.IO;

namespace RegistroDePonto.Classes
{
    public class Program
    {
        private static List<Colaborador> colaboradores = new List<Colaborador>();
        private static Administrador administrador;
        private static ColaboradorLogado colaboradorLogado;

        private static void CarregarDados()
        {
            string dadosFilePath = "Dados.txt";

            if (File.Exists(dadosFilePath))
            {
                string[] linhas = File.ReadAllLines(dadosFilePath);

                foreach (string linha in linhas)
                {
                    string[] dados = linha.Split(';');

                    Colaborador colaborador = new Colaborador
                    {
                        Nome = dados[0],
                        Cpf = dados[1],
                        Usuario = dados[2],
                        Senha = dados[3],
                        Cargo = dados[4]
                    };

                    colaborador.Registros = CarregarRegistros(colaborador.Usuario);

                    colaboradores.Add(colaborador);
                }
            }
        }

        private static List<RegistroDePonto> CarregarRegistros(string usuario)
        {
            List<RegistroDePonto> registros = new List<RegistroDePonto>();

            string arquivoRegistros = $"{usuario}_registros.txt";

            if (File.Exists(arquivoRegistros))
            {
                string[] linhas = File.ReadAllLines(arquivoRegistros);

                foreach (string linha in linhas)
                {
                    if (DateTime.TryParse(linha, out DateTime dataHora))
                    {
                        registros.Add(new RegistroDePonto { DataHora = dataHora });
                    }
                }
            }

            return registros;
        }

        private static void SalvarDados()
        {
            using (StreamWriter writer = new StreamWriter("Dados.txt"))
            {
                foreach (Colaborador colaborador in colaboradores)
                {
                    writer.WriteLine($"{colaborador.Nome};{colaborador.Cpf};{colaborador.Usuario};{colaborador.Senha};{colaborador.Cargo}");
                }
            }
        }

        private static void SalvarRegistros(string usuario, List<RegistroDePonto> registros)
        {
            string arquivoRegistros = $"{usuario}_registros.txt";
            using (StreamWriter writer = new StreamWriter(arquivoRegistros, append: false))
            {
                foreach (RegistroDePonto registro in registros)
                {
                    writer.WriteLine(registro.DataHora.ToString());
                }
            }
        }

        private static void MostrarMenu()
        {
            Console.Clear();
            Console.WriteLine("Menu:");
            Console.WriteLine("1 - Login Administrador.");
            Console.WriteLine("2 - Login Colaborador.");
            Console.WriteLine("3 - Sobre.");
            Console.WriteLine("0 - Sair.");
        }

        private static void MostrarMenuAdministrador()
        {
            Console.Clear();
            Console.WriteLine("Login Administrador:");
            Console.WriteLine("1 - Cadastrar Colaborador.");
            Console.WriteLine("2 - Editar Perfis de Colaboradores.");
            Console.WriteLine("3 - Excluir Perfil de Colaborador.");
            Console.WriteLine("0 - Sair.");
        }

        private static void MostrarMenuColaborador()
        {
            Console.Clear();
            Console.WriteLine("Login Colaborador:");
            Console.WriteLine("1 - Registrar Horário de Trabalho.");
            Console.WriteLine("2 - Meus Registros de Horário de Trabalho.");
            Console.WriteLine("0 - Sair.");
        }

        private static void MostrarMenuSobre()
        {
            Console.Clear();
            Console.WriteLine("Versão 1.0.0 Beta. Desenvolvida por Cristofer Eduardo Medeiros, Makciel Santos e Adriele Silva Motta.");
            Console.WriteLine();
            Console.WriteLine("Pressione qualquer tecla para voltar.");
            Console.ReadKey();
        }

        private static void Main(string[] args)
        {
            CarregarDados();
            administrador = new Administrador("senhaadm");

            int opcao;
            do
            {
                MostrarMenu();
                Console.Write("Escolha uma opção: ");
                opcao = int.Parse(Console.ReadLine());

                switch (opcao)
                {
                    case 1:
                        LoginAdministrador();
                        break;
                    case 2:
                        LoginColaborador();
                        break;
                    case 3:
                        MostrarMenuSobre();
                        break;
                    case 0:
                        Console.WriteLine("Saindo...");
                        break;
                    default:
                        Console.WriteLine("Opção inválida!");
                        break;
                }

            } while (opcao != 0);

            SalvarDados();
        }

        private static void LoginAdministrador()
        {
            Console.Clear();
            Console.WriteLine("Login Administrador:");
            Console.WriteLine("Digite a senha do administrador:");
            string senha = LerSenha();

            if (senha == administrador.SenhaAdm)
            {
                int opcao;
                do
                {
                    MostrarMenuAdministrador();
                    Console.Write("Escolha uma opção: ");
                    opcao = int.Parse(Console.ReadLine());

                    switch (opcao)
                    {
                        case 1:
                            administrador.CadastrarColaborador(colaboradores);
                            break;
                        case 2:
                            administrador.EditarPerfil(colaboradores);
                            break;
                        case 3:
                            administrador.ExcluirColaborador(colaboradores);
                            break;
                        case 0:
                            Console.WriteLine("Voltando...");
                            break;
                        default:
                            Console.WriteLine("Opção inválida!");
                            break;
                    }

                } while (opcao != 0);
            }
            else
            {
                SalvarDados();
                Console.WriteLine("Senha do administrador incorreta.");
                MostrarMensagemOpcaoInvalida();
            }
        }

        private static void LoginColaborador()
        {
            Console.Clear();
            Console.WriteLine("Login Colaborador");
            Console.WriteLine("Digite o usuário:");
            string usuario = Console.ReadLine();
            Console.WriteLine("Digite a senha:");
            string senha = LerSenha();

            Colaborador colaborador = colaboradores.Find(c => c.Usuario == usuario && c.Senha == senha);

            if (colaborador != null)
            {
                colaboradorLogado = new ColaboradorLogado(colaborador);

                int opcao;
                do
                {
                    MostrarMenuColaborador();
                    Console.Write("Escolha uma opção: ");
                    opcao = int.Parse(Console.ReadLine());

                    switch (opcao)
                    {
                        case 1:
                            colaboradorLogado.RegistrarPonto();
                            break;
                        case 2:
                            colaboradorLogado.ExibirMeusRegistros();
                            break;
                        case 0:
                            Console.WriteLine("Voltando...");
                            break;
                        default:
                            Console.WriteLine("Opção inválida!");
                            break;
                    }

                } while (opcao != 0);
            }
            else
            {
                Console.WriteLine("Usuário ou senha incorretos.");
                MostrarMensagemOpcaoInvalida();
            }
        }

        private static string LerSenha()
        {
            string senha = string.Empty;

            ConsoleKeyInfo key;
            do
            {
                key = Console.ReadKey(true);

                if (key.Key == ConsoleKey.Backspace)
                {
                    if (senha.Length > 0)
                    {
                        senha = senha.Remove(senha.Length - 1);
                        Console.Write("\b \b");
                    }
                }
                else if (key.Key != ConsoleKey.Enter)
                {
                    senha += key.KeyChar;
                    Console.Write("*");
                }

            } while (key.Key != ConsoleKey.Enter);

            Console.WriteLine();

            return senha;
        }

        private static void MostrarMensagemOpcaoInvalida()
        {
            Console.WriteLine("Opção inválida, clique em qualquer tecla para voltar ao menu");
            Console.ReadKey();
        }
    }
}
