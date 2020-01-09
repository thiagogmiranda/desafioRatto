using System;
using System.Collections.Generic;
using DesafioRatto.Dominio.Modelo;

namespace DesafioRatto.Dominio.Repositorio
{
    public interface IClienteRepositorio
    {
        void Adicionar(Cliente cliente);
        bool JaExisteClienteComCPF(string cpf);
        ICollection<Cliente> ObterTodos();
        Cliente ObterPorId(int id);
        void Remover(Cliente cliente);
        void Atualizar(Cliente cliente);
    }
}
