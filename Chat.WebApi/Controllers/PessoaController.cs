using Microsoft.AspNetCore.Mvc;
using Chat.WebApi.Repositorio;
using Chat.WebApi.Objetos;

namespace Pessoa.WebApi.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class PessoaController : ControllerBase
    {
        private readonly Repositorio _repositorio;

        public PessoaController()
        {
            _repositorio = new Repositorio();
        }

        [HttpPost]
        [Route("SalvarPessoa")]
        public IActionResult SalvarPessoa([FromBody] PessoaDto pessoa)
        {
            if (pessoa == null)
            {
                return BadRequest("Pessoa data is null.");
            }

            try
            {
                _repositorio.SalvarPessoa(pessoa.Nome, pessoa.Idade, pessoa.Email, pessoa.Sexo);

                return Ok("User data saved successfully.");
            }
            catch (Exception ex)
            {
                return StatusCode(500, $"Internal server error: {ex.Message}");
            }
        }

        [HttpGet("LoginPorEmail")]
        public IActionResult LoginPorEmail([FromQuery] string email)
        {
            if (string.IsNullOrEmpty(email))
            {
                return BadRequest("Email é nulo ou vazio.");
            }

            var pessoa = _repositorio.BuscarPessoaPorEmail(email);

            if (pessoa == null)
            {
                return NotFound("Pessoa não encontrada.");
            }

            return Ok(pessoa);
        }
    }

   
}
