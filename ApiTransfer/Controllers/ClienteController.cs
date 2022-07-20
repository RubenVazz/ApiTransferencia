using AccesoDatos;
using AccesoDatos.Repositorios;
using FluentValidation;
using FluentValidation.Results;
using Microsoft.AspNetCore.Mvc;
using Model;
using System.Threading.Tasks;

namespace Transfrencia.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ClienteController : Controller
    {
        private readonly IClienteRepository _clienteRepository;
        ClienteValidation validator = new ClienteValidation();


        public ClienteController(IClienteRepository clienteRepository)
        {
            this._clienteRepository = clienteRepository;
        }

        [HttpGet]
        public async Task<IActionResult> GetAllClientes()
        {
            return Ok(await _clienteRepository.GetAllClientes());
        }

        [HttpGet("{CEDULA}")]
        public async Task<IActionResult> GetClientesDetalle(string cedula)
        {
            try
            {

                var response = await _clienteRepository.GetClienteDetalle(cedula);

                if (response == null)
                {
                    throw new Exception("Cliente no existe");
                }
                return Ok(response);


            }
            catch (Exception ex)

            {
                return BadRequest($"Error: {ex.Message}.\n{ex.StackTrace}.\n{ex.GetType()}");
            }
        }

        [HttpPost]
        public async Task<IActionResult> CrearCliente(Cliente cliente)
        {

            if (cliente == null)
            {
                return BadRequest();
            }
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            Cliente clienteNuevo = new Cliente()
            {
                Cedula = cliente.Cedula,
                Tipo_Doc = cliente.Tipo_Doc,
                Nombre_Apellido = cliente.Nombre_Apellido
            };

            validator.ValidateAndThrow(clienteNuevo);
            try
            {
                var created = await _clienteRepository.InsertCliente(cliente);

                return Created("Cliente creado", cliente);
            }
            catch (Exception ex)
            {
                return BadRequest($"Error al crear cliente: {ex.Message}.\n{ex.StackTrace}");
            }
        }
        [HttpPut]
        public async Task<IActionResult> UpdateCliente([FromBody] Cliente cliente)
        {
            if (cliente == null)
            {
                return BadRequest();
            }
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
           
            Cliente clienteNuevo = new Cliente()
            {
                Cedula = cliente.Cedula,
                Tipo_Doc = cliente.Tipo_Doc,
                Nombre_Apellido = cliente.Nombre_Apellido
            };
            
            validator.ValidateAndThrow(clienteNuevo);
            try
            {
                var created = await _clienteRepository.UpdateCliente(cliente);

                return Created("Informacion Actualizada", cliente);
            }
            catch (Exception ex)
            {
                return BadRequest($"Error: {ex.Message}");
            }
        }

        [HttpDelete]
        public async Task<IActionResult> DeleteCliente(string cedula)
        {
            await _clienteRepository.DeleteCliente(new Cliente { Cedula = cedula });

            return NoContent();
        }
    } 
}
