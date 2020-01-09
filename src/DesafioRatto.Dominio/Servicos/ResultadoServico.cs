using System.Collections.Generic;
using DesafioRatto.Dominio.Repositorio;
using FluentValidation.Results;

namespace DesafioRatto.Dominio.Servicos
{
    public class ResultadoServico
    {
        private List<string> _erros;
        public IEnumerable<string> Erros => _erros.AsReadOnly();
        public bool EhValido => _erros.Count == 0;

        public ResultadoServico()
        {
            _erros = new List<string>();
        }

        public void AdicionarErro(string erro)
        {
            _erros.Add(erro);
        }

        public void AdicionarErros(IEnumerable<string> erros)
        {
            _erros.AddRange(erros);
        }
    }
}