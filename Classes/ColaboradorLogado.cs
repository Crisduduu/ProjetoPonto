using System;

namespace RegistroDePonto.Classes
{
    public class ColaboradorLogado
    {
        private Colaborador colaborador;

        public ColaboradorLogado(Colaborador colaborador)
        {
            this.colaborador = colaborador;
        }

        public void RegistrarPonto()
        {
            Console.WriteLine("Registro de Ponto");
            DateTime agora = DateTime.Now;
            Console.WriteLine(agora.ToString("dd/MM/yyyy HH:mm:ss"));
            Console.WriteLine("Pressione Enter para registrar o ponto...");
            Console.ReadLine();

            colaborador.AdicionarRegistroPonto();
            Console.WriteLine($"\nPonto registrado às {DateTime.Now.ToString("HH:mm:ss")}");
            Console.WriteLine("\nPressione qualquer tecla para voltar.");
            Console.ReadKey();
        }

        public void ExibirMeusRegistros()
        {
            colaborador.ExibirRegistros();
            Console.WriteLine("\nPressione qualquer tecla para voltar.");
            Console.ReadKey();
        }
    }
}
