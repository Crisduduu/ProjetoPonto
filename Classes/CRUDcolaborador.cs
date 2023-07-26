using System;
using System.Collections.Generic;

namespace RegistroDePonto.Classes
{
    public class CRUDcolaborador
    {
        public static void CadastrarNovoColaborador(List<Colaborador> colaboradores)
        {
            Console.WriteLine("Cadastro de Colaborador");
            Console.WriteLine("Digite o nome do colaborador:");
            string nome = Console.ReadLine();
            Console.WriteLine("Digite o CPF do colaborador:");
            string cpf = Console.ReadLine();
            Console.WriteLine("Digite o usuário do colaborador:");
            string usuario = Console.ReadLine();
            Console.WriteLine("Digite a senha do colaborador:");
            string senha = Console.ReadLine();
            Console.WriteLine("Digite o cargo do colaborador:");
            string cargo = Console.ReadLine();

            Colaborador novoColaborador = new Colaborador
            {
                Nome = nome,
                Cpf = cpf,
                Usuario = usuario,
                Senha = senha,
                Cargo = cargo
            };

            colaboradores.Add(novoColaborador);

            Console.WriteLine("Colaborador cadastrado com sucesso!");
        }

        public static void EditarColaborador(List<Colaborador> colaboradores)
        {
            Console.WriteLine("Edição de Perfil");
            Console.WriteLine("Digite o usuário do colaborador a ser editado:");
            string usuario = Console.ReadLine();

            Colaborador colaborador = colaboradores.Find(c => c.Usuario == usuario);

            if (colaborador == null)
            {
                Console.WriteLine("Colaborador não encontrado.");
                return;
            }

            Console.WriteLine($"Editar Perfil de {colaborador.Nome}");
            Console.WriteLine("Digite o novo nome do colaborador:");
            string novoNome = Console.ReadLine();
            Console.WriteLine("Digite a nova senha do colaborador:");
            string novaSenha = Console.ReadLine();
            Console.WriteLine("Digite o novo cargo do colaborador:");
            string novoCargo = Console.ReadLine();

            colaborador.Nome = novoNome;
            colaborador.Senha = novaSenha;
            colaborador.Cargo = novoCargo;

            Console.WriteLine("Perfil editado com sucesso!");
        }

        public static void ExcluirColaborador(List<Colaborador> colaboradores)
        {
            Console.WriteLine("Excluir Perfil");
            Console.WriteLine("Digite o usuário do colaborador a ser excluído:");
            string usuario = Console.ReadLine();

            Colaborador colaborador = colaboradores.Find(c => c.Usuario == usuario);

            if (colaborador == null)
            {
                Console.WriteLine("Colaborador não encontrado.");
                return;
            }

            colaboradores.Remove(colaborador);

            Console.WriteLine("Perfil excluído com sucesso!");
        }
    }
}
