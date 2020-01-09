using System.Collections.Generic;
using System.Linq;
using DesafioRatto.Dominio.Modelo;
using DesafioRatto.Dominio.Repositorio;

namespace DesafioRatto.Dominio.Servicos
{
    public class AdicionarEnderecoServico
    {
        private IEnderecoRepositorio _repositorio;

        public AdicionarEnderecoServico(IEnderecoRepositorio repositorio)
        {
            _repositorio = repositorio;
        }

        public ResultadoServico Executar(Endereco endereco)
        {
            var resultado = new ResultadoServico();
            resultado.AdicionarErros(ValidoParaAdicionar(endereco));

            if (resultado.EhValido)
            {
                _repositorio.Adicionar(endereco);
            }

            return resultado;
        }

        private IEnumerable<string> ValidoParaAdicionar(Endereco endereco)
        {
            var erros = new List<string>();

            if (!endereco.EhValido())
            {
                erros.AddRange(endereco.ObterErrosValidacao());
            }
            else
            {
                if (_repositorio.JaExisteEndereco(endereco))
                {
                    erros.Add("Endereço já cadastrado");
                }
            }

            return erros;
        }
    }
}