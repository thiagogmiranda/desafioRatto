using System;
using System.Collections.Generic;
using System.Linq;
using DesafioRatto.Dominio.Modelo;
using DesafioRatto.Dominio.Repositorio;

namespace DesafioRatto.Repositorio
{
    public class ClienteRepositorio : IClienteRepositorio
    {
        private DesafioRattoContext _db;

        public ClienteRepositorio(DesafioRattoContext db)
        {
            _db = db;
        }

        public void Adicionar(Cliente cliente)
        {
            _db.Clientes.Add(cliente);
            _db.SaveChanges();
        }

        public void Atualizar(Cliente cliente)
        {
            _db.Update(cliente);
            _db.SaveChanges();
        }

        public bool JaExisteClienteComCPF(string cpf)
        {
            return _db.Clientes.Count(c => c.CPF == cpf) > 0;
        }

        public Cliente ObterPorId(int id)
        {
            return _db.Clientes.FirstOrDefault(c => c.Id == id);
        }

        public ICollection<Cliente> ObterTodos()
        {
            return _db.Clientes.ToList();
        }

        public void Remover(Cliente cliente)
        {
            _db.Remove(cliente);
            _db.SaveChanges();
        }
    }
}