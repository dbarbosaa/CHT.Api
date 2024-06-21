using Microsoft.AspNetCore.Mvc;
using Chat.WebApi.Repositorio;
using Chat.WebApi.Objetos;

/// Controlador de pessoa;
namespace Pessoa.WebApi.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class PessoaController : ControllerBase
    {
        /// <summary>
        /// Declarar reposit�rio;
        /// </summary>
        private readonly Repositorio _repositorio;

        /// <summary>
        /// Construtor para inicializar a class reposit�rio;
        /// </summary>
        public PessoaController()
        {
            _repositorio = new Repositorio();
        }

        /// <summary>
        /// Fun��o post para receber os dados de pessoa;
        /// </summary>
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


        /// <summary>
        /// Fun��o get para buscar pessoa por email;
        /// </summary>

        [HttpGet("LoginPorEmail")]
        public IActionResult LoginPorEmail([FromQuery] string email)
        {
            if (string.IsNullOrEmpty(email))
            {
                return BadRequest("Email � nulo ou vazio.");
            }

            var pessoa = _repositorio.BuscarPessoaPorEmail(email);

            if (pessoa == null)
            {
                return NotFound("Pessoa n�o encontrada.");
            }

            return Ok(pessoa);
        }
    }

   
}
