using System;
using DesafioRatto.Dominio.Modelo;
using DesafioRatto.Dominio.Repositorio;
using DesafioRatto.Dominio.Servicos;
using DesafioRatto.Dominio.Validacao;
using Moq;
using NUnit.Framework;

namespace DesafioRatto.Dominio.Test
{
    public class EnderecoValidadorTest
    {
        private EnderecoValidador _validador;

        private Endereco _endereco;

        [SetUp]
        public void Setup()
        {
            _validador = new EnderecoValidador();

            _endereco = new Endereco
            {
                CEP = "69317173",
                Logradouro = "Rua Grão-Mestre Ademir Viana",
                Bairro = "Santa Luzia",
                Cidade = "Boa Vista",
                Estado = "Roraima"
            };
        }

        [Test]
        public void DeveRetornarTrueParaEnderecoValido()
        {
            var resultado = _validador.Validate(_endereco);

            Assert.True(resultado.IsValid);
        }

        [Test]
        [TestCase("")]
        [TestCase(null)]
        [TestCase("    ")]
        [TestCase("594485400")]
        [TestCase("5944854")]
        public void DeveRetornarFalseParaEnderecoComCEPInvalido(string cep)
        {
            _endereco.CEP = cep;

            var resultado = _validador.Validate(_endereco);

            Assert.False(resultado.IsValid);
        }

        [Test]
        [TestCase("")]
        [TestCase(null)]
        [TestCase("    ")]
        public void DeveRetornarFalseParaEnderecoSemLogradouro(string logradouro)
        {
            _endereco.Logradouro = logradouro;

            var resultado = _validador.Validate(_endereco);

            Assert.False(resultado.IsValid);
        }

        [Test]
        public void DeveRetornarFalseParaEnderecoComLogradouroMaiorQue50Caracteres()
        {
            _endereco.Logradouro = "Rua ".PadRight(51, '1');

            var resultado = _validador.Validate(_endereco);

            Assert.False(resultado.IsValid);
        }

        [Test]
        [TestCase("")]
        [TestCase(null)]
        [TestCase("    ")]
        public void DeveRetornarFalseParaEnderecoSemBairro(string bairro)
        {
            _endereco.Bairro = bairro;

            var resultado = _validador.Validate(_endereco);

            Assert.False(resultado.IsValid);
        }

        [Test]
        public void DeveRetornarFalseParaEnderecoComBairroMaiorQue40Caracteres()
        {
            _endereco.Bairro = "Dom ".PadRight(41, 'a');

            var resultado = _validador.Validate(_endereco);

            Assert.False(resultado.IsValid);
        }

        [Test]
        [TestCase("")]
        [TestCase(null)]
        [TestCase("    ")]
        public void DeveRetornarFalseParaEnderecoSemCidade(string cidade)
        {
            _endereco.Cidade = cidade;

            var resultado = _validador.Validate(_endereco);

            Assert.False(resultado.IsValid);
        }

        [Test]
        public void DeveRetornarFalseParaEnderecoComCidadeMaiorQue40Caracteres()
        {
            _endereco.Cidade = "Bela ".PadRight(41, 'a');

            var resultado = _validador.Validate(_endereco);

            Assert.False(resultado.IsValid);
        }

        [Test]
        [TestCase("")]
        [TestCase(null)]
        [TestCase("    ")]
        public void DeveRetornarFalseParaEnderecoSemEstado(string estado)
        {
            _endereco.Estado = estado;

            var resultado = _validador.Validate(_endereco);

            Assert.False(resultado.IsValid);
        }

        [Test]
        public void DeveRetornarFalseParaEnderecoComEstadoMaiorQue40Caracteres()
        {
            _endereco.Estado = "R ".PadRight(41, 'a');

            var resultado = _validador.Validate(_endereco);

            Assert.False(resultado.IsValid);
        }
    }
}