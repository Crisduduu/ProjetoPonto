using System;
using System.Collections.Generic;
using System.Linq;
using NUnit.Framework;
using RegistroDePonto.Classes;

namespace RegistroDePonto.Tests
{
    [TestFixture]
    public class CRUDcolaboradorTests
    {
        [Test]
        public void CadastrarNovoColaborador_Deve_AdicionarNovoColaborador()
        {
            var colaboradores = new List<Colaborador>();
            var novoColaborador = new Colaborador
            {
                Nome = "Teste",
                Cpf = "12345678901",
                Usuario = "teste123",
                Senha = "senha123",
                Cargo = "Engenheiro de Testes"
            };

            CRUDcolaborador.CadastrarNovoColaborador(colaboradores);

            Assert.AreEqual(1, colaboradores.Count);
            Assert.AreEqual(novoColaborador.Nome, colaboradores[0].Nome);
            Assert.AreEqual(novoColaborador.Cpf, colaboradores[0].Cpf);
            Assert.AreEqual(novoColaborador.Usuario, colaboradores[0].Usuario);
            Assert.AreEqual(novoColaborador.Senha, colaboradores[0].Senha);
            Assert.AreEqual(novoColaborador.Cargo, colaboradores[0].Cargo);
        }

        [Test]
        public void EditarColaborador_Deve_AtualizarPerfilDoColaborador()
        {
            var colaboradores = new List<Colaborador>
            {
                new Colaborador
                {
                    Nome = "Teste",
                    Cpf = "12345678901",
                    Usuario = "teste123",
                    Senha = "senha123",
                    Cargo = "Engenheiro de Testes"
                }
            };
            var novoNome = "Novo Teste";
            var novaSenha = "novasenha";
            var novoCargo = "Engenheiro de Qualidade";

            CRUDcolaborador.EditarColaborador(colaboradores);

            var colaborador = colaboradores.Find(c => c.Usuario == "teste123");
            Assert.IsNotNull(colaborador);
            Assert.AreEqual(novoNome, colaborador.Nome);
            Assert.AreEqual(novaSenha, colaborador.Senha);
            Assert.AreEqual(novoCargo, colaborador.Cargo);
        }

        [Test]
        public void ExcluirColaborador_Deve_RemoverColaborador()
        {
            var colaboradores = new List<Colaborador>
            {
                new Colaborador
                {
                    Nome = "Teste",
                    Cpf = "12345678901",
                    Usuario = "teste123",
                    Senha = "senha123",
                    Cargo = "Engenheiro de Testes"
                }
            };

            CRUDcolaborador.ExcluirColaborador(colaboradores);

            Assert.AreEqual(0, colaboradores.Count);
        }

        [Test]
        public void BuscarColaboradorPorNome_Deve_RetornarColaboradorCorreto()
        {
            var colaboradores = new List<Colaborador>
            {
                new Colaborador
                {
                    Nome = "Teste",
                    Cpf = "12345678901",
                    Usuario = "teste123",
                    Senha = "senha123",
                    Cargo = "Engenheiro de Testes"
                }
            };
            var nomeBuscado = "Teste";

            var colaboradorEncontrado = Colaborador.BuscarColaboradorPorNome(colaboradores, nomeBuscado);

            Assert.IsNotNull(colaboradorEncontrado);
            Assert.AreEqual(nomeBuscado, colaboradorEncontrado.Nome);
        }
    }
}
