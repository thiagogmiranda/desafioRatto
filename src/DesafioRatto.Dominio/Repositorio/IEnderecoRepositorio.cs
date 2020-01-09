using System;
using System.Collections.Generic;
using DesafioRatto.Dominio.Modelo;

namespace DesafioRatto.Dominio.Repositorio
{
    public interface IEnderecoRepositorio
    {
        void Adicionar(Endereco cliente);
        ICollection<Endereco> ObterTodos();
        Endereco ObterPorId(int id);
        void Remover(Endereco cliente);
        void Atualizar(Endereco cliente);
        bool JaExisteEndereco(Endereco endereco);
    }
}
