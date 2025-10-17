using Microsoft.AspNetCore.Mvc;
using PropEase.API.Models;
using PropEase.API.Services;
using PropEase.API.DTOs;

namespace PropEase.API.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class ImoveisController : ControllerBase
    {
        private readonly ImovelService _service;
        private readonly ProprietarioService _proprietarios;

        public ImoveisController(ImovelService service, ProprietarioService proprietarios)
        {
            _service = service;
            _proprietarios = proprietarios;
        }

        [HttpGet]
        public async Task<IActionResult> GetAll()
        {
            var imoveis = await _service.GetAllAsync();
            return Ok(new { Mensagem = imoveis.Any() ? "Imóveis encontrados." : "Nenhum imóvel cadastrado ainda.", Dados = imoveis });
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetById(int id)
        {
            var imovel = await _service.GetByIdAsync(id);
            return imovel is null ? NotFound() : Ok(imovel);
        }

        [HttpPost]
        public async Task<IActionResult> Create([FromBody] CreateImovelDto dto)
        {
            if (dto is null) return BadRequest("Payload inválido");

            Imovel? imovel = dto.Tipo?.Trim().ToLowerInvariant() switch
            {
                "casa" => new Casa(),
                "apartamento" => new Apartamento(),
                _ => null
            };

            if (imovel is null)
                return BadRequest("Tipo inválido. Use 'Casa' ou 'Apartamento'.");

            
            var owner = await _proprietarios.GetByIdAsync(dto.ProprietarioId);
            if (owner is null)
                return BadRequest($"Proprietário {dto.ProprietarioId} não encontrado. Crie-o antes de vincular ao imóvel.");

            imovel.Endereco = dto.Endereco;
            imovel.Numero = dto.Numero;
            imovel.ProprietarioId =  dto.ProprietarioId;
            if (dto.Alugado)
                imovel.Alugar();
            else
                imovel.Disponibilizar();
            imovel.Proprietario = null; 

            var novo = await _service.AddAsync(imovel);
            return CreatedAtAction(nameof(GetById), new { id = novo.Id }, new { Mensagem = "Imóvel criado com sucesso.", Dados = novo });
        }

        [HttpPost("alugar/{id}")]
        public async Task<IActionResult> Alugar(int id)
        {
            var result = await _service.AlugarAsync(id);
            return result ? Ok(new { Mensagem = "Imóvel alugado com sucesso." }) : NotFound("Imóvel não encontrado.");
        }

        [HttpPut("disponibilizar/{id}")]
        public async Task<IActionResult> Disponibilizar(int id)
        {
            var result = await _service.DisponibilizarAsync(id);
            return result ? Ok(new { Mensagem = "Imóvel agora está disponível." }) : NotFound("Imóvel não encontrado.");
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(int id)
        {
            var result = await _service.DeleteAsync(id);
            return result ? Ok(new { Mensagem = "Imóvel removido com sucesso." }) : NotFound("Imóvel não encontrado.");
        }

        [HttpGet("{id}/calcular-aluguel")]
        public async Task<IActionResult> CalcularAluguel(int id, [FromQuery] int periodo)
        {
            if (periodo <= 0) return BadRequest("Periodo deve ser maior que zero.");

            var imovel = await _service.GetByIdAsync(id);
            if (imovel is null) return NotFound("Imóvel não encontrado");

            var total = imovel.CalcularAluguel(periodo);
            return Ok(new { Id = id, Periodo = periodo, Total = total });
        }
    }
}
