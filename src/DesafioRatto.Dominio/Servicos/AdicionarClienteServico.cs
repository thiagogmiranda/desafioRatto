using System.Collections.Generic;
using System.Linq;
using DesafioRatto.Dominio.Modelo;
using DesafioRatto.Dominio.Repositorio;

namespace DesafioRatto.Dominio.Servicos
{
    public class AdicionarClienteServico
    {
        private IClienteRepositorio _repositorio;

        public AdicionarClienteServico(IClienteRepositorio repositorio)
        {
            _repositorio = repositorio;
        }

        public ResultadoServico Executar(Cliente cliente)
        {
            var resultado = new ResultadoServico();
            resultado.AdicionarErros(ValidoParaAdicionar(cliente));

            if(resultado.EhValido)
            {
                _repositorio.Adicionar(cliente);
            }

            return resultado;
        }

        private IEnumerable<string> ValidoParaAdicionar(Cliente cliente)
        {
            var erros = new List<string>();

            if(!cliente.EhValido())
            {
                erros.AddRange(cliente.ObterErrosValidacao());
            }

            if(!string.IsNullOrWhiteSpace(cliente.CPF))
            {
                if(_repositorio.JaExisteClienteComCPF(cliente.CPF))
                {
                    erros.Add($"Já existe cliente cadastrado com o CPF {cliente.CPF}");
                }
            }

            return erros;
        }
    }
}