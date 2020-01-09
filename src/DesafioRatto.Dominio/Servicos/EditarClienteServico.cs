using System.Collections.Generic;
using System.Linq;
using DesafioRatto.Dominio.Modelo;
using DesafioRatto.Dominio.Repositorio;

namespace DesafioRatto.Dominio.Servicos
{
    public class EditarClienteServico
    {
        private IClienteRepositorio _repositorio;

        public EditarClienteServico(IClienteRepositorio repositorio)
        {
            _repositorio = repositorio;
        }

        public ResultadoServico Executar(Cliente cliente)
        {
            var resultado = new ResultadoServico();

            resultado.AdicionarErros(ValidoParaEditar(cliente));

            if(resultado.EhValido)
            {
                _repositorio.Atualizar(cliente);
            }

            return resultado;
        }

        private IEnumerable<string> ValidoParaEditar(Cliente cliente)
        {
            var erros = new List<string>();

            if(!cliente.EhValido())
            {
                erros.AddRange(cliente.ObterErrosValidacao());
            }

            return erros;
        }
    }
}