using System;
using DesafioRatto.Dominio.Validacao;

namespace DesafioRatto.Dominio.Modelo
{
    public class Cliente : Entidade
    {
        public string Nome { get; set; }
        public string CPF { get; set; }
        public DateTime DataNascimento { get; set; }
        public int Idade => (DateTime.MinValue + (DateTime.Now.Date - DataNascimento)).Year - 1;

        public override bool EhValido()
        {
            _validacao = new ClienteValidador().Validate(this);

            return _validacao.IsValid;
        }
    }
}
