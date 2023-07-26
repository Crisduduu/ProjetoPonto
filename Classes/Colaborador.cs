using System;
using System.Collections.Generic;
using System.IO;

namespace RegistroDePonto.Classes
{
    public class Colaborador : IEquatable<Colaborador>
    {
        public string Nome { get; set; }
        public string Cpf { get; set; }
        public string Usuario { get; set; }
        public string Senha { get; set; }
        public string Cargo { get; set; }
        public List<RegistroDePonto> Registros { get; set; }

        public Colaborador()
        {
            Registros = new List<RegistroDePonto>();
        }

        public override string ToString()
        {
            return $"Nome: {Nome}\nCPF: {Cpf}\nUsuário: {Usuario}\nSenha: {Senha}\nCargo: {Cargo}";
        }

        public void AdicionarRegistroPonto()
        {
            Registros.Add(new RegistroDePonto { DataHora = DateTime.Now });
            SalvarRegistros();
        }

        public void ExibirRegistros()
        {
            Console.WriteLine($"Registros de Ponto de {Nome}:");
            foreach (var registro in Registros)
            {
                Console.WriteLine(registro);
            }

            Console.WriteLine();

            int numParesEncontrados = 0;
            DateTime? ultimoRegistroPar = null;
            TimeSpan horasTrabalhadas = TimeSpan.Zero;

            List<TimeSpan> horasPorPar = new List<TimeSpan>();

            foreach (var registro in Registros)
            {
                if (registro.DataHora.Hour % 2 == 0)
                {
                    if (ultimoRegistroPar != null)
                    {
                        TimeSpan diferenca = registro.DataHora - ultimoRegistroPar.Value;
                        horasTrabalhadas += diferenca;
                        horasPorPar.Add(diferenca);

                        numParesEncontrados++;

                        if (numParesEncontrados >= 30)
                            break;
                    }

                    ultimoRegistroPar = registro.DataHora;
                }
            }



            TimeSpan somaTotalHoras = TimeSpan.Zero;
            foreach (var horasPar in horasPorPar)
            {
                somaTotalHoras += horasPar;
            }

            Console.WriteLine($"Soma total das horas trabalhadas nos últimos 30 dias: {somaTotalHoras:hh\\:mm}");
        }


        public void SalvarRegistros()
        {
            using (StreamWriter writer = new StreamWriter(ArquivoRegistros, append: false))
            {
                foreach (RegistroDePonto registro in Registros)
                {
                    writer.WriteLine(registro.DataHora.ToString());
                }
            }
        }

        public void CarregarRegistros()
        {
            if (File.Exists(ArquivoRegistros))
            {
                string[] linhas = File.ReadAllLines(ArquivoRegistros);

                foreach (string linha in linhas)
                {
                    if (DateTime.TryParse(linha, out DateTime dataHora))
                    {
                        Registros.Add(new RegistroDePonto { DataHora = dataHora });
                    }
                }
            }
        }

        public static Colaborador BuscarColaboradorPorNome(List<Colaborador> colaboradores, string nome)
        {
            return colaboradores.Find(c => c.Nome == nome);
        }

        private string ArquivoRegistros
        {
            get { return $"{Usuario}_registros.txt"; }
        }

        private TimeSpan CalcularTotalHorasTrabalhadasUltimos30Dias()
        {
            DateTime hoje = DateTime.Now;
            DateTime trintaDiasAtras = hoje.AddDays(-30);

            TimeSpan totalHoras = TimeSpan.Zero;
            foreach (var registro in Registros)
            {
                if (registro.DataHora >= trintaDiasAtras && registro.DataHora <= hoje)
                {
                    totalHoras += TimeSpan.FromHours(8);
                }
            }

            return totalHoras;
        }

        public bool Equals(Colaborador other)
        {
            if (other == null)
                return false;

            return this.Usuario == other.Usuario;
        }

        public override bool Equals(object obj)
        {
            if (obj == null || !(obj is Colaborador))
                return false;

            return Equals((Colaborador)obj);
        }

        public override int GetHashCode()
        {
            return this.Usuario.GetHashCode();
        }
    }
}
