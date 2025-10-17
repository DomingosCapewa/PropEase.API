using Microsoft.AspNetCore.Mvc;
using PropEase.API.Models;
using PropEase.API.Services;

namespace PropEase.API.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class ProprietariosController : ControllerBase
    {
        private readonly ProprietarioService _service;
        public ProprietariosController(ProprietarioService service) { _service = service; }

        [HttpGet]
        public async Task<IActionResult> GetAll()
        {
            var proprietarios = await _service.GetAllAsync();
            return Ok(new { Mensagem = proprietarios.Any() ? "Proprietários encontrados." : "Nenhum proprietário cadastrado ainda.", Dados = proprietarios });
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> Get(int id)
        {
            var p = await _service.GetByIdAsync(id);
            return p is null ? NotFound($"Proprietário {id} não encontrado.") : Ok(p);
        }

        [HttpPost]
        public async Task<IActionResult> Create(Proprietario p)
        {
            if (p is null) return BadRequest("Dados inválidos.");
            if (string.IsNullOrWhiteSpace(p.Nome)) return BadRequest("Nome é obrigatório.");
            if (string.IsNullOrWhiteSpace(p.Telefone)) return BadRequest("Telefone é obrigatório.");
            if (string.IsNullOrWhiteSpace(p.CPF)) return BadRequest("CPF é obrigatório.");
            var novo = await _service.AddAsync(p);
            return CreatedAtAction(nameof(Get), new { id = novo.Id }, new { Mensagem = "Proprietário criado com sucesso.", Dados = novo });
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> Update(int id, Proprietario p)
        {
            var ok = await _service.UpdateAsync(id, p);
            return ok ? Ok(new { Mensagem = "Proprietário atualizado com sucesso." }) : NotFound($"Proprietário {id} não encontrado.");
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(int id)
        {
            var ok = await _service.DeleteAsync(id);
            return ok ? Ok(new { Mensagem = "Proprietário removido com sucesso." }) : NotFound($"Proprietário {id} não encontrado.");
        }
    }
}
