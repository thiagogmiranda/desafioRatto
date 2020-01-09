using System;
using System.Collections.Generic;
using System.Linq;
using DesafioRatto.Dominio.Modelo;
using DesafioRatto.Dominio.Repositorio;

namespace DesafioRatto.Repositorio
{
    public class EnderecoRepositorio : IEnderecoRepositorio
    {
        private DesafioRattoContext _db;

        public EnderecoRepositorio(DesafioRattoContext db)
        {
            _db = db;
        }

        public void Adicionar(Endereco endereco)
        {
            _db.Enderecos.Add(endereco);
            _db.SaveChanges();
        }

        public void Atualizar(Endereco endereco)
        {
            _db.Update(endereco);
            _db.SaveChanges();
        }

        public bool JaExisteEndereco(Endereco endereco)
        {
            return _db.Enderecos.Count(e => 
                e.CEP == endereco.CEP
                && e.Logradouro == endereco.Logradouro
                && e.Cidade == endereco.Cidade 
                && e.Bairro == endereco.Bairro
                && e.Estado == endereco.Estado
                && (endereco.Id == 0 || e.Id != endereco.Id)) > 0;
        }

        public Endereco ObterPorId(int id)
        {
            return _db.Enderecos.FirstOrDefault(c => c.Id == id);
        }

        public ICollection<Endereco> ObterTodos()
        {
            return _db.Enderecos.ToList();
        }

        public void Remover(Endereco endereco)
        {
            _db.Remove(endereco);
            _db.SaveChanges();
        }
    }
}