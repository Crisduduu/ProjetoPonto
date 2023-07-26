using System;
using System.Collections.Generic;

namespace RegistroDePonto.Classes
{
    public class Administrador
    {
        private string senhaAdm;

        public string SenhaAdm
        {
            get { return senhaAdm; }
            set { senhaAdm = value; }
        }

        public Administrador(string senhaAdm)
        {
            SenhaAdm = senhaAdm;
        }

        public void CadastrarColaborador(List<Colaborador> colaboradores)
        {
            CRUDcolaborador.CadastrarNovoColaborador(colaboradores);
        }

        public void EditarPerfil(List<Colaborador> colaboradores)
        {
            CRUDcolaborador.EditarColaborador(colaboradores);
        }

        public void ExcluirColaborador(List<Colaborador> colaboradores)
        {
            CRUDcolaborador.ExcluirColaborador(colaboradores);
        }
    }
}
