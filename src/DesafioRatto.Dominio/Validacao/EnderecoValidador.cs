using System;
using DesafioRatto.Dominio.Modelo;
using FluentValidation;

namespace DesafioRatto.Dominio.Validacao
{
    public class EnderecoValidador : AbstractValidator<Endereco>
    {
        public EnderecoValidador()
        {
            RuleFor(endereco => endereco.CEP)
                .NotEmpty()
                .WithMessage("O cep é obrigatório");

            RuleFor(endereco => endereco.CEP)
                .Length(8)
                .WithMessage("O cep deve ter 8 caracteres");

            RuleFor(endereco => endereco.Logradouro)
                .NotEmpty()
                .MaximumLength(50)
                .WithMessage("O logradouro é obrigatório e deve possuir no máximo 50 caracteres");

            RuleFor(cliente => cliente.Bairro)
                .NotEmpty()
                .MaximumLength(40)
                .WithMessage("O bairro é obrigatório e deve possuir no máximo 40 caracteres");

            RuleFor(cliente => cliente.Cidade)
                .NotEmpty()
                .MaximumLength(40)
                .WithMessage("A cidade é obrigatória e deve possuir no máximo 40 caracteres");

            RuleFor(cliente => cliente.Estado)
                .NotEmpty()
                .MaximumLength(40)
                .WithMessage("O estado é obrigatório e deve possuir no máximo 40 caracteres");
        }
    }
}
