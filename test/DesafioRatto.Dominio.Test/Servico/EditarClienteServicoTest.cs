using DesafioRatto.Dominio.Modelo;
using DesafioRatto.Dominio.Repositorio;
using DesafioRatto.Dominio.Servicos;
using Moq;
using NUnit.Framework;

namespace DesafioRatto.Dominio.Test
{
    public class EditarClienteServicoTest
    {
        private EditarClienteServico _servico;

        private Mock<IClienteRepositorio> _repositorioMock;

        private Cliente _cliente;

        [SetUp]
        public void Setup()
        {
            _repositorioMock = new Mock<IClienteRepositorio>(MockBehavior.Strict);

            _servico = new EditarClienteServico(_repositorioMock.Object);

            _cliente = new Cliente
            {
                Id = 42,
                CPF = "59448540655",
                Nome = "Elias Manuel Brito",
                DataNascimento = new System.DateTime(1988, 02, 13)
            };
        }

        [Test]
        public void DeveEditarClienteComSucesso()
        {
            _repositorioMock.Setup(r => r.Atualizar(It.IsAny<Cliente>()));

            var resultado = _servico.Executar(_cliente);

            Assert.True(resultado.EhValido);
        }

        [Test]
        public void NaoDeveEditarClienteInvalido()
        {
            var resultado = _servico.Executar(new Cliente());

            Assert.False(resultado.EhValido);
        }
    }
}