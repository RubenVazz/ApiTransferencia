using AccesoDatos;
using AccesoDatos.Repositorios;
using FluentValidation;
using Microsoft.AspNetCore.Mvc;
using Model;

namespace ApiTransfer.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class TransaccionController : Controller
    {
        private readonly ITransaccionRepository _transaccionRepository;
        TransaccionValidator validator = new TransaccionValidator();
        public TransaccionController(ITransaccionRepository transaccionRepository)
        {
            this._transaccionRepository = transaccionRepository;
        }

        [HttpPost]
        public async Task<IActionResult> CrearTransaccion(TransaccionRequest tr)
        {

            if (tr.BancoOrigen == tr.BancoDestino)
                throw new Exception("Bancos NO deben ser iguales");
            if (tr.Saldo < tr.Monto )
                throw new Exception("Saldo Insuficiente");
            var transaccion = new Transaccion();
            Guid guid = Guid.NewGuid();
            var transfer = new TransaccionRequest()
            {
               IdTransaccion = guid.ToString(),
               NumeroCuenta = tr.NumeroCuenta,
               Cedula = tr.Cedula,
               Fecha = tr.Fecha,
               Monto = tr.Monto,
               Estado = tr.Estado,
               BancoDestino = tr.BancoDestino,
               BancoOrigen = tr.BancoOrigen,
            };
            validator.ValidateAndThrow(transfer);
            try
            {
                var created = await _transaccionRepository.InsertTransaccion(transfer);

                return Created("Cliente creado", tr);
            }
            catch (Exception ex)
            {
                return BadRequest($"Error al crear cliente: {ex.Message}.\n{ex.StackTrace}");
            }
        }
        

        [HttpPatch("{idtransaccion}")]
        public async Task<IActionResult> UpdateEstado(Transaccion tr)
        {
                      return Ok(await _transaccionRepository.UpdateEstado(tr));
        }
        [HttpGet("{idtransferencia}")]
        public async Task<IActionResult> GetTransferEstado(string idtransferencia)
        {
            return Ok(await _transaccionRepository.GetTransferEstado(idtransferencia));
        }

        [HttpGet("{num_cta}/enviados")]
        public async Task<IActionResult> GetHistorialTransferenciaOrigen(string num_cta)
        {
            return Ok(await _transaccionRepository.GetHistorialTransferenciaOrigen(num_cta));
        }
        [HttpGet("{num_cta}/recibidos")]
        public async Task<IActionResult> GetHistorialTransferenciaDestino(string num_cta)
        {
            return Ok(await _transaccionRepository.GetHistorialTransferenciaDestino(num_cta));
        }
    }

}
