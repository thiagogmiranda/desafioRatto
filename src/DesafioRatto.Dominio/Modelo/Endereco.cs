using DesafioRatto.Dominio.Validacao;

namespace DesafioRatto.Dominio.Modelo
{
    public class Endereco : Entidade
    {
        public string CEP { get; set; }
        public string Logradouro { get; set; }
        public string Bairro { get; set; }
        public string Cidade { get; set; }
        public string Estado { get; set; }
        public string Complemento { get; set; }

        public override bool EhValido()
        {
            _validacao = new EnderecoValidador().Validate(this);

            return _validacao.IsValid;
        }
    }
}
