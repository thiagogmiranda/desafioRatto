using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using DesafioRatto.Dominio.Modelo;
using DesafioRatto.Dominio.Repositorio;
using DesafioRatto.Dominio.Servicos;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;

namespace DesafioRatto.Api.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class ClientesController : ControllerBase
    {
        private AdicionarClienteServico _adicionarCliente;
        private EditarClienteServico _editarCliente;
        private IClienteRepositorio _repositorio;

        public ClientesController(
            AdicionarClienteServico adicionarCliente,
            EditarClienteServico editarCliente,
            IClienteRepositorio repositorio)
        {
            _adicionarCliente = adicionarCliente;
            _editarCliente = editarCliente;
            _repositorio = repositorio;
        }

        [HttpPost]
        [ProducesResponseType(StatusCodes.Status201Created)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public ActionResult Create([Bind("Nome,CPF,DataNascimento")]Cliente cliente)
        {
            var resultado = _adicionarCliente.Executar(cliente);

            if (!resultado.EhValido)
            {
                return BadRequest(resultado.Erros);
            }

            return CreatedAtAction(nameof(GetById), new { id = cliente.Id }, cliente);
        }

        [HttpGet]
        [ProducesResponseType(StatusCodes.Status200OK)]
        public ActionResult<IEnumerable<Cliente>> Get()
        {
            return Ok(_repositorio.ObterTodos());
        }

        [HttpGet("{id}")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public ActionResult<Cliente> GetById(int id)
        {
            var cliente = _repositorio.ObterPorId(id);

            if (cliente == null)
            {
                return NotFound();
            }

            return Ok(cliente);
        }

        [HttpPut]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public ActionResult Update([Bind("Id,Nome,DataNascimento")]Cliente entrada)
        {
            Cliente cliente = null;

            if(entrada.Id > 0)
            {
                cliente = _repositorio.ObterPorId(entrada.Id);
            }

            if(cliente == null)
            {
                return NotFound();
            }

            cliente.Nome = entrada.Nome;
            cliente.DataNascimento = entrada.DataNascimento;

            var resultado = _editarCliente.Executar(cliente);

            if (!resultado.EhValido)
            {
                return BadRequest(resultado.Erros);
            }

            return Ok(cliente);
        }

        [HttpDelete("{id}")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public ActionResult Delete(int id)
        {
            var cliente = _repositorio.ObterPorId(id);

            if (cliente == null)
            {
                return NotFound();
            }

            _repositorio.Remover(cliente);

            return Ok();
        }
    }
}
