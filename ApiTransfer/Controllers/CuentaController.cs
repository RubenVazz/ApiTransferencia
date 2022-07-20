using Microsoft.AspNetCore.Mvc;
using AccesoDatos.Repositorios;
using Model;
using System.Threading.Tasks;
using AccesoDatos;
using FluentValidation;

namespace ApiTransfer.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CuentaController : Controller
    {
        private readonly ICuentasRepository _cuentaRepository;
        CuentaValidator validator = new CuentaValidator();
        public CuentaController(ICuentasRepository cuentaRepository)
        {
            this._cuentaRepository = cuentaRepository;
        }

        [HttpGet]
        public async Task<IActionResult> GetAllCuentas()
        {
            return Ok(await _cuentaRepository.GetAllCuentas());
        }

        [HttpGet("id_Cta")]
        public async Task<IActionResult> GetCuentaDetalle(string idCuenta)
        {
            try
            {
                return Ok(await _cuentaRepository.GetCuentaDetalle(idCuenta));

            }
            catch (Exception ex)
            {

                return BadRequest("Error: " + ex.Message + "\n" + ex.StackTrace);

            }
        }

        [HttpPost]
        public async Task<IActionResult> CrearCuenta(CuentaRequest cuentaRequest)
        {

            if (cuentaRequest == null)
            {
                return BadRequest();
            }
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            Guid guid = Guid.NewGuid();
            var Cuenta = new Cuentas()
            {
                Id_Cuenta = guid.ToString(),
                num_cta = cuentaRequest.num_cta,
                Moneda = cuentaRequest.Moneda,
                Cedula = cuentaRequest.Cedula,
                Saldo = cuentaRequest.Saldo,
                cod_banco = cuentaRequest.cod_banco,
            };
            validator.ValidateAndThrow(Cuenta);

            try
            {

                var newClient = await _cuentaRepository.InsertCuenta(Cuenta);

                return Created("Cuenta Creada", cuentaRequest);

            }
            catch (Exception ex)
            {
                return BadRequest("Error: " + ex.Message + "\n" + ex.StackTrace);
            }

        }
        [HttpPut]
        public async Task<IActionResult> UpdateCuentas([FromBody] Cuentas cuenta)
        {
            if (cuenta == null)
            {
                return BadRequest();
            }
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            Cuentas Nuevacuenta = new Cuentas()
            {
                num_cta = cuenta.num_cta,
                Moneda = cuenta.Moneda,
                Cedula = cuenta.Cedula,
                Saldo = cuenta.Saldo,
                cod_banco= cuenta.cod_banco
            };

            validator.ValidateAndThrow(Nuevacuenta);
            try
            {
                var created = await _cuentaRepository.UpdateCuenta(cuenta);

                return Created("Informacion Actualizada", cuenta);
            }
            catch (Exception ex)
            {
                return BadRequest($"Error: {ex.Message}");
            }
        }

        [HttpDelete("Guid id_cta")]
        public async Task<IActionResult> DeleteCuenta(string idCuenta)
        {
            try
            {
                if (idCuenta == null)
                {
                    return BadRequest();
                }

                var newClient = await _cuentaRepository.DeleteCuenta(new Cuentas { Id_Cuenta = idCuenta });

                return NoContent();

            }
            catch (Exception ex)
            {
                return BadRequest($"Error en el servicio: {ex.Message}.\n{ex.StackTrace}.");
            }
        }



    }
}
