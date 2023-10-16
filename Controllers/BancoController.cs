using Microsoft.AspNetCore.Mvc;
using TesteBanco.Interface;
using TesteBanco.Models;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using AutoMapper;
using TesteBanco.DTOs;

namespace TesteBanco.Controllers
{
    [Authorize]
    [Route("api/bancos")]
    [ApiController]
    public class BancoController : ControllerBase
    {
        private readonly IBanco _bancoRepository;
        private readonly IMapper _mapper; // Injetar o IMapper

        public BancoController(IBanco bancoRepository, IMapper mapper)
        {
            _bancoRepository = bancoRepository;
            _mapper = mapper; // Atribuir o IMapper injetado ao campo privado
        }

        // GET: api/bancos
        [HttpGet]
        public async Task<IActionResult> GetAllBancos()
        {
            var bancos = await _bancoRepository.GetAllBancosAsync();
            var bancosDTO = _mapper.Map<List<BancoDTO>>(bancos); // Mapear os objetos de domínio para DTOs
            return Ok(bancosDTO);

        }

        // GET: api/bancos/5
        [HttpGet("{id}")]
        public async Task<IActionResult> GetBanco(int id)
        {
            var banco = await _bancoRepository.GetBancoByIdAsync(id);
            if (banco == null)
            {
                return NotFound();
            }
            var bancoDto = _mapper.Map<BoletoDTO>(banco); // Mapear o objeto de domínio para um DTO
            return Ok(bancoDto);
        }

        // POST: api/bancos
        [HttpPost]
        public async Task<IActionResult> CreateBanco([FromBody] Banco banco)
        {
           
            return NoContent();
        }

        // PUT: api/bancos/5
        [HttpPut("{id}")]
        public IActionResult UpdateBanco(int id, [FromBody] Banco banco)
        {
          
            return NoContent();
        }

        // DELETE: api/bancos/5
        [HttpDelete("{id}")]
        public IActionResult DeleteBanco(int id)
        {
          
            return NoContent();
        }
    }
}
