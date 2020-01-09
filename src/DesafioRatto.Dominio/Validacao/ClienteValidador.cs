using System;
using DesafioRatto.Dominio.Modelo;
using FluentValidation;

namespace DesafioRatto.Dominio.Validacao
{
    public class ClienteValidador : AbstractValidator<Cliente>
    {
        public ClienteValidador()
        {
            RuleFor(cliente => cliente.Nome)
                .NotEmpty()
                .MaximumLength(30)
                .WithMessage("O nome é obrigatório e deve possuir no máximo 30 caracteres");

            RuleFor(cliente => cliente.DataNascimento)
                .Must(data => data != DateTime.MinValue)
                .WithMessage("A data de nascimento é obrigatória");

            RuleFor(cliente => cliente.CPF)
                .Must(CPFValido)
                .WithMessage("O cpf é obrigatório e deve ser válido");
        }

        protected static bool CPFValido(string cpf)
        {
            if(string.IsNullOrWhiteSpace(cpf))
            {
                return false;
            }

            int[] multiplicador1 = new int[9] { 10, 9, 8, 7, 6, 5, 4, 3, 2 };
            int[] multiplicador2 = new int[10] { 11, 10, 9, 8, 7, 6, 5, 4, 3, 2 };

            cpf = cpf.Trim().Replace(".", "").Replace("-", "");
            if (cpf.Length != 11)
                return false;

            for (int j = 0; j < 10; j++)
                if (j.ToString().PadLeft(11, char.Parse(j.ToString())) == cpf)
                    return false;

            string tempCpf = cpf.Substring(0, 9);
            int soma = 0;

            for (int i = 0; i < 9; i++)
                soma += int.Parse(tempCpf[i].ToString()) * multiplicador1[i];

            int resto = soma % 11;
            if (resto < 2)
                resto = 0;
            else
                resto = 11 - resto;

            string digito = resto.ToString();
            tempCpf = tempCpf + digito;
            soma = 0;
            for (int i = 0; i < 10; i++)
                soma += int.Parse(tempCpf[i].ToString()) * multiplicador2[i];

            resto = soma % 11;
            if (resto < 2)
                resto = 0;
            else
                resto = 11 - resto;

            digito = digito + resto.ToString();

            return cpf.EndsWith(digito);
        }
    }
}
