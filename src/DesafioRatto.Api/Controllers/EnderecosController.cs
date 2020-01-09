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
    public class EnderecosController : ControllerBase
    {
        private AdicionarEnderecoServico _adicionarEndereco;
        private EditarEnderecoServico _editarEndereco;
        private IEnderecoRepositorio _repositorio;

        public EnderecosController(
            AdicionarEnderecoServico adicionarEndereco,
            EditarEnderecoServico editarEndereco,
            IEnderecoRepositorio repositorio)
        {
            _adicionarEndereco = adicionarEndereco;
            _editarEndereco = editarEndereco;
            _repositorio = repositorio;
        }

        [HttpPost]
        [ProducesResponseType(StatusCodes.Status201Created)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public ActionResult Create([Bind("CEP,Logradouro,Bairro,Cidade,Estado,Complemento")]Endereco endereco)
        {
            var resultado = _adicionarEndereco.Executar(endereco);

            if (!resultado.EhValido)
            {
                return BadRequest(resultado.Erros);
            }

            return CreatedAtAction(nameof(GetById), new { id = endereco.Id }, endereco);
        }

        [HttpGet]
        [ProducesResponseType(StatusCodes.Status200OK)]
        public ActionResult Get()
        {
            return Ok(_repositorio.ObterTodos());
        }

        [HttpGet("{id}")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public ActionResult GetById(int id)
        {
            var endereco = _repositorio.ObterPorId(id);

            if (endereco == null)
            {
                return NotFound();
            }

            return Ok(endereco);
        }

        [HttpPut]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public ActionResult Update([Bind("Id,CEP,Logradouro,Bairro,Cidade,Estado,Complemento")]Endereco entrada)
        {
            Endereco endereco = null;

            if(entrada.Id > 0)
            {
                endereco = _repositorio.ObterPorId(entrada.Id);
            }

            if(endereco == null)
            {
                return NotFound();
            }

            endereco.CEP = entrada.CEP;
            endereco.Logradouro = entrada.Logradouro;
            endereco.Bairro = entrada.Bairro;
            endereco.Cidade = entrada.Cidade;
            endereco.Estado = entrada.Estado;
            endereco.Complemento = entrada.Complemento;

            var resultado = _editarEndereco.Executar(endereco);

            if (!resultado.EhValido)
            {
                return BadRequest(resultado.Erros);
            }

            return Ok(endereco);
        }

        [HttpDelete("{id}")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public ActionResult Delete(int id)
        {
            var endereco = _repositorio.ObterPorId(id);

            if (endereco == null)
            {
                return NotFound();
            }

            _repositorio.Remover(endereco);

            return Ok();
        }
    }
}
