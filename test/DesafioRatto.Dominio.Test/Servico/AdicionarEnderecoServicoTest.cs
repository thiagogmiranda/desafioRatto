using DesafioRatto.Dominio.Modelo;
using DesafioRatto.Dominio.Repositorio;
using DesafioRatto.Dominio.Servicos;
using Moq;
using NUnit.Framework;

namespace DesafioRatto.Dominio.Test
{
    public class AdicionarEnderecoServicoTest
    {
        private AdicionarEnderecoServico _servico;

        private Mock<IEnderecoRepositorio> _repositorioMock;

        private Endereco _endereco;

        [SetUp]
        public void Setup()
        {
            _repositorioMock = new Mock<IEnderecoRepositorio>(MockBehavior.Strict);

            _servico = new AdicionarEnderecoServico(_repositorioMock.Object);

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
        public void DeveAdicionarEnderecoComSucesso()
        {
            _repositorioMock.Setup(r => r.JaExisteEndereco(It.IsAny<Endereco>())).Returns(false);
            _repositorioMock.Setup(r => r.Adicionar(It.IsAny<Endereco>()));

            var resultado = _servico.Executar(_endereco);

            Assert.True(resultado.EhValido);
        }

        [Test]
        public void NaoDeveAdicionarEnderecoJaExistente()
        {
            _repositorioMock.Setup(r => r.JaExisteEndereco(It.IsAny<Endereco>())).Returns(true);

            var resultado = _servico.Executar(_endereco);

            Assert.False(resultado.EhValido);
        }

        [Test]
        public void NaoDeveAdicionarEnderecoInvalido()
        {
            var resultado = _servico.Executar(new Endereco());

            Assert.False(resultado.EhValido);
        }
    }
}