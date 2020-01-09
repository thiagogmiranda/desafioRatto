using DesafioRatto.Dominio.Modelo;
using DesafioRatto.Dominio.Repositorio;
using DesafioRatto.Dominio.Servicos;
using Moq;
using NUnit.Framework;

namespace DesafioRatto.Dominio.Test
{
    public class AdicionarClienteServicoTest
    {
        private AdicionarClienteServico _servico;

        private Mock<IClienteRepositorio> _repositorioMock;

        private Cliente _cliente;

        [SetUp]
        public void Setup()
        {
            _repositorioMock = new Mock<IClienteRepositorio>(MockBehavior.Strict);

            _servico = new AdicionarClienteServico(_repositorioMock.Object);

            _cliente = new Cliente
            {
                CPF = "59448540655",
                Nome = "Elias Manuel Brito",
                DataNascimento = new System.DateTime(1988, 02, 13)
            };
        }

        [Test]
        public void DeveAdicionarClienteComSucesso()
        {
            _repositorioMock.Setup(r => r.JaExisteClienteComCPF(It.IsAny<string>())).Returns(false);
            _repositorioMock.Setup(r => r.Adicionar(It.IsAny<Cliente>()));

            var resultado = _servico.Executar(_cliente);

            Assert.True(resultado.EhValido);
        }

        [Test]
        public void NaoDeveAdicionarClienteComCPFJaExistente()
        {
            _repositorioMock.Setup(r => r.JaExisteClienteComCPF(It.IsAny<string>())).Returns(true);

            var resultado = _servico.Executar(_cliente);

            Assert.False(resultado.EhValido);
        }

        [Test]
        public void NaoDeveAdicionarClienteInvalido()
        {
            _repositorioMock.Setup(r => r.JaExisteClienteComCPF(It.IsAny<string>())).Returns(false);

            var resultado = _servico.Executar(new Cliente());

            Assert.False(resultado.EhValido);
        }
    }
}