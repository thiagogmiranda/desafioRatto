using System;
using DesafioRatto.Dominio.Modelo;
using DesafioRatto.Dominio.Repositorio;
using DesafioRatto.Dominio.Servicos;
using DesafioRatto.Dominio.Validacao;
using Moq;
using NUnit.Framework;

namespace DesafioRatto.Dominio.Test
{
    public class ClienteValidadorTest
    {
        private ClienteValidador _validador;

        private Cliente _cliente;

        [SetUp]
        public void Setup()
        {
            _validador = new ClienteValidador();

            _cliente = new Cliente
            {
                CPF = "59448540655",
                Nome = "Elias Manuel Brito",
                DataNascimento = new DateTime(1988, 02, 13)
            };
        }

        [Test]
        public void DeveRetornarTrueParaClienteValido()
        {
            var resultado = _validador.Validate(_cliente);

            Assert.True(resultado.IsValid);
        }

        [Test]
        [TestCase("")]
        [TestCase(null)]
        [TestCase("    ")]
        [TestCase("59448540650")]
        public void DeveRetornarFalseParaClienteComCPFInvalido(string cpf)
        {
            _cliente.CPF = cpf;

            var resultado = _validador.Validate(_cliente);

            Assert.False(resultado.IsValid);
        }

        [Test]
        [TestCase("")]
        [TestCase(null)]
        [TestCase("    ")]
        public void DeveRetornarFalseParaClienteSemNome(string nome)
        {
            _cliente.Nome = nome;

            var resultado = _validador.Validate(_cliente);

            Assert.False(resultado.IsValid);
        }

        [Test]
        public void DeveRetornarFalseParaClienteComNomeMaiorQue30Caracteres()
        {
            _cliente.Nome = "nome ".PadRight(31, 'n');

            var resultado = _validador.Validate(_cliente);

            Assert.False(resultado.IsValid);
        }

         [Test]
        public void DeveRetornarFalseParaClienteComDataNascimentoInvalida()
        {
            _cliente.DataNascimento = DateTime.MinValue;

            var resultado = _validador.Validate(_cliente);

            Assert.False(resultado.IsValid);
        }
    }
}