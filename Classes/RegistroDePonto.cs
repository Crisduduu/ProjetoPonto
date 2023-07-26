using System;

namespace RegistroDePonto.Classes
{
    public class RegistroDePonto
    {
        public DateTime DataHora { get; set; }

        public override string ToString()
        {
            return DataHora.ToString("dd/MM/yyyy HH:mm:ss");
        }

        public static RegistroDePonto Parse(string dataHoraString)
        {
            if (DateTime.TryParseExact(dataHoraString, "dd/MM/yyyy HH:mm:ss", null, System.Globalization.DateTimeStyles.None, out DateTime dataHora))
            {
                return new RegistroDePonto { DataHora = dataHora };
            }
            else
            {
                throw new ArgumentException("Formato de data e hora inválido.");
            }
        }
    }
}
