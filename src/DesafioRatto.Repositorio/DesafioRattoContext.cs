using System;
using DesafioRatto.Dominio.Modelo;
using FluentValidation.Results;
using Microsoft.EntityFrameworkCore;

namespace DesafioRatto.Repositorio
{
    public class DesafioRattoContext : DbContext
    {
        public DbSet<Cliente> Clientes { get; set; }
        public DbSet<Endereco> Enderecos { get; set; }

        public DesafioRattoContext() { }

        public DesafioRattoContext(DbContextOptions<DesafioRattoContext> options)
            : base(options) { }
    }
}
