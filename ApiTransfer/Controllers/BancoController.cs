using AccesoDatos;
using AccesoDatos.Repositorios;
using FluentValidation;
using Microsoft.AspNetCore.Mvc;
using Model;

namespace ApiTransfer.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class BancoController : Controller
    {
        private readonly IBancoRepository _bancoRepository;

        public BancoController(IBancoRepository bancoRepository)
        {
            _bancoRepository = bancoRepository;
        }
        BancoValidator bancovalidator = new BancoValidator();

        [HttpGet]
        public async Task<IActionResult> GetAllBancos()
        {
            return Ok(await _bancoRepository.GetAllBancos());
        }

        [HttpGet("{cod_banco}")]
        public async Task<IActionResult> GetBancoDetalle(string cod_banco)
        {
            return Ok(await _bancoRepository.GetBancoDetalle(cod_banco));
        }

        [HttpPost]
        public async Task<IActionResult> CrearBanco([FromBody] Banco banco)
        {
            if (banco == null)
            {
                return BadRequest();
            }

            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var Banco = new Banco()
            {
                cod_banco = banco.cod_banco,
                nombre_banco = banco.nombre_banco,
                direccion = banco.direccion,
            };

            var created = await _bancoRepository.InsertBanco(Banco);
            return Ok("Banco Registrado");
        }
        [HttpPut]
        public async Task<IActionResult> UpdateBanco([FromBody] Banco bancos)
        {
            if (bancos == null)
            {
                return BadRequest();
            }
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            await _bancoRepository.UpdateBanco(bancos);

            return NoContent();
        }

        [HttpDelete]
        public async Task<IActionResult> DeleteBanco(string codigoBanco)
        {
            await _bancoRepository.DeleteBanco(new Banco { cod_banco = codigoBanco });

            return NoContent();
        }
    }
}

