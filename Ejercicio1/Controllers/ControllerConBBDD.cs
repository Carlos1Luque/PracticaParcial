using Microsoft.AspNetCore.Mvc;
using Testing.Business; 
namespace Testing.Controllers;

[ApiController]
[Route("[controller]")]
public class ControllerConBBDD : ControllerBase
{
    //  private static List<Usuario> usuarios = new List<Usuario>
    // {
    // new Usuario(1, "Ana", "García", true),
    // new Usuario(2, "Luis", "Pérez", true),
    // new Usuario(3, "Marta", "López", false)
    // };
    
    // private static List<Auto> autos = new List<Auto> 
    // {
    // new Auto(1, "AB123CD", false, null),          // Disponible
    // new Auto(2, "CD456EF", true, usuarios[0]),    // Vendido a Ana
    // new Auto(3, "GH789IJ", true, usuarios[1])     // Vendido a Luis
    // };
    

    private readonly ILogger<ControllerConBBDD> _logger;
      private readonly AutoService _service;

    public ControllerConBBDD(AutoService service, ILogger<ControllerConBBDD> logger)
    {
        _logger = logger;
        _service = service;
    }
    [HttpGet("autos")]
    public async Task<IActionResult> GetAll()
    {
        try
        {
            var listautos = await _service.GetAllCar();

             return Ok(listautos);
        }
        catch (Exception ex)
        {
            return StatusCode(500, $"Ocurrió un error: {ex.Message}");
        }
    }

    [HttpGet("autos/{id}")]
    public async Task<IActionResult> GetbyIDcar(int id)
    {
        try
        {
            if(id <= 0)
            return BadRequest();

            var idcar = await _service.GetIDcar(id);
            if(idcar is null)
            return NotFound();

            return Ok(idcar);
        }
        catch(Exception ex)
        {
            return StatusCode(500,$"Error en el server: {ex.Message}"); 
            
        }
    }
    [HttpPost("Autos")]
    public async Task<IActionResult> CreateCARs([FromBody]PostResponseAuto AutoNuevo)
    {
        try
        {
            if(AutoNuevo.Id <=0)
            return BadRequest(); 

            if(AutoNuevo.Patente is null)
            return BadRequest();
            
            var error = await _service.CreateCar(AutoNuevo);
            if(error is not null)
            return NotFound(error);
            
         
            return Ok(AutoNuevo);
        }catch(Exception ex)
        {   
            return StatusCode(500,$"Error en el server: {ex.Message}");
        }
    }

    [HttpDelete("Autos/{id}")]
    public async Task<IActionResult> DeleteCAR(int id)
    {
        try
        {
            if (id <= 0)
                return BadRequest("El id debe ser mayor que 0.");

            // El ID va en la URL; DELETE no necesita recibir el auto completo en el body.
            var eliminado = await _service.DeleteAUTO(id);
            if (!eliminado)
                return NotFound("No existe un auto con ese id.");

            return Ok(eliminado);
        }
        catch (Exception ex)
        {
            return StatusCode(500, $"Error en el servidor: {ex.Message}");
        }
    }
}