using Microsoft.AspNetCore.Mvc;
using TesteBanco.Interface;
using TesteBanco.Models;
using System;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using AutoMapper;
using TesteBanco.DTOs;

namespace TesteBanco.Controllers
{
    [Authorize]
    [Route("api/boletos")]
    [ApiController]
    public class BoletoController : ControllerBase
    {
        private readonly IBoletoRepository _boletoRepository;
        private readonly IMapper _mapper; // Injetar o IMapper

        public BoletoController(IBoletoRepository boletoRepository, IMapper mapper)
        {
            _boletoRepository = boletoRepository;
            _mapper = mapper; // Atribuir o IMapper injetado ao campo privado
        }

        // GET: api/boletos
        [HttpGet]
        public async Task<IActionResult> GetAllBoletos()
        {
            var boletos = await _boletoRepository.GetAllBoletosAsync();
            var boletosDTO = _mapper.Map<List<BoletoDTO>>(boletos); // Mapear os objetos de domínio para DTOs
            return Ok(boletosDTO);
        }

        // GET: api/boletos/5
        [HttpGet("{id}")]
        public async Task<IActionResult> GetBoleto(int id)
        {
            var boleto = await _boletoRepository.GetBoletoByIdAsync(id);
            if (boleto == null)
            {
                return NotFound();
            }
            var boletoDTO = _mapper.Map<BoletoDTO>(boleto); // Mapear o objeto de domínio para um DTO
            return Ok(boletoDTO);
        }

        // POST: api/boletos
        [HttpPost]
        public async Task<IActionResult> CreateBoleto([FromBody] BoletoDTO boletoDTO)
        {
            try
            {
                var boletos = _mapper.Map<Boleto>(boletoDTO); // Mapear o DTO para um objeto de domínio
                await _boletoRepository.InserirBoletoAsync(boletos);
                var createdBoletoDTO = _mapper.Map<BoletoDTO>(boletos); // Mapear o objeto de domínio de volta para um DTO
                return CreatedAtAction(nameof(GetBoleto), new { id = createdBoletoDTO.Id }, createdBoletoDTO);
            }
            catch (ArgumentException ex)
            {
                return BadRequest(ex.Message);
            }
        }

        // PUT: api/boletos/5
        [HttpPut("{id}")]
        public IActionResult UpdateBoleto(int id, [FromBody] Boleto boleto)
        {
            // Implemente a atualização do boleto aqui
            return NoContent();
        }

        // DELETE: api/boletos/5
        [HttpDelete("{id}")]
        public IActionResult DeleteBoleto(int id)
        {
            // Implemente a exclusão do boleto aqui
            return NoContent();
        }
    }
}
