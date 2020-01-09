using System;
using System.Collections.Generic;
using System.Linq;
using FluentValidation.Results;

namespace DesafioRatto.Dominio.Modelo
{
    public abstract class Entidade
    {
        public int Id { get; set; }
        protected ValidationResult _validacao { get; set; }

        public Entidade()
        {
            _validacao = new ValidationResult();
        }

        public virtual bool EhValido()
        {
            throw new NotImplementedException();
        }

        public IEnumerable<string> ObterErrosValidacao()
        {
            return _validacao.Errors.Select(e => e.ErrorMessage);
        }
    }
}
