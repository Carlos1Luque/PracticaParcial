using Microsoft.AspNetCore.Mvc;

namespace Testing.Controllers;

[ApiController]
[Route("[controller]")]
public class AutoENDController : ControllerBase
{
     private static List<Usuario> usuarios = new List<Usuario>
    {
    new Usuario(1, "Ana", "García", true),
    new Usuario(2, "Luis", "Pérez", true),
    new Usuario(3, "Marta", "López", false)
    };
    
    private static List<Auto> autos = new List<Auto> 
    {
    new Auto(1, "AB123CD", false, null),          // Disponible
    new Auto(2, "CD456EF", true, usuarios[0]),    // Vendido a Ana
    new Auto(3, "GH789IJ", true, usuarios[1])     // Vendido a Luis
    };
    

    private readonly ILogger<AutoENDController> _logger;

    public AutoENDController(ILogger<AutoENDController> logger)
    {
        _logger = logger;
    }

    [HttpGet("autos")]
    public IActionResult GetAll()
    {
        try
        {
            if (autos is null)
                return NotFound("La lista de autos esta vacia");

            if (autos.Count == 0)
                return Conflict("No hay autos");

            return Ok(autos);
        }
        catch (Exception ex)
        {
            return StatusCode(500, $"Ocurrió un error: {ex.Message}");
        }
    }

    [HttpGet("usuarios")]
    public IActionResult GetallUser()
    {
        try
        {
            if (usuarios.Count == 0)
                return NotFound("No hay users");

            return Ok(usuarios);
        }
        catch (Exception ex)
        {
            return StatusCode(500, $"Ocurrió un error: {ex.Message}");
        }
    }

    [HttpGet("autos/{id}")]
    public IActionResult GetByIDCar(int Id)
    {
        try
        {
            if (Id <= 0)
                return BadRequest("Tiene que ser mayor a 0");

            var autoId = autos.FirstOrDefault(a => a.Id == Id);
            if (autoId is null)
                return BadRequest("No hay auto con ese id");

            return Ok(autoId);
        }
        catch (Exception ex)
        {
            return StatusCode(500, $"Ocurrió un error: {ex.Message}");
        }
    }

    [HttpGet("users/{id}")]
    public IActionResult GetBYIDUser(int id)
    {
        try
        {
            if (id <= 0)
                return BadRequest("Tiene que ser mas que 0");

            var userId = usuarios.FirstOrDefault(u => u.Id == id);
            if (userId is null)
                return BadRequest("la lista user esta vacia");

            return Ok(userId);
        }
        catch (Exception ex)
        {
            return StatusCode(500, $"Ocurrió un error: {ex.Message}");
        }
    }

    [HttpPost ("Usuario")]
    public IActionResult CreateUSER([FromBody] Usuario usuario)
    {
        try
        {
            if (usuario is null)
                return BadRequest("Debes enviar los datos del usuario.");

            if (usuario.Id <= 0)
                return BadRequest("El id tiene que ser mas que 0");

            if (usuarios.Any(u => u.Id == usuario.Id))
                return BadRequest("ya existe un ID asi");

            if (string.IsNullOrWhiteSpace(usuario.Nombre))
                return BadRequest("");

            if (string.IsNullOrWhiteSpace(usuario.Apellido))
                return BadRequest();

            usuarios.Add(usuario);
            return Ok("Usuario creado exitosamente.");
        }
        catch (Exception ex)
        {
            return StatusCode(500, $"Ocurrió un error: {ex.Message}");
        }
    }

    [HttpPost ("Auto")]
    public IActionResult CreateCAR([FromBody] Auto autonuevo)
    {
        try
        {
            if (autonuevo is null)
                return BadRequest("Debes enviar los datos del auto.");

            if (autonuevo.Id <= 0)
                return BadRequest();

            if (autonuevo.Patente is null)
                return BadRequest();

            if (autos.Any(a => a.Id == autonuevo.Id))
                return BadRequest("Ya hay un auto con ese ID");

            if (autonuevo.Vendido)
                return BadRequest("No se puede cargar un auto vendido");

            autos.Add(autonuevo);
            return Ok("Auto cargado " + autonuevo);
        }
        catch (Exception ex)
        {
            return StatusCode(500, $"Ocurrió un error: {ex.Message}");
        }
    }

    [HttpPut("user/{id}")]
    public IActionResult UpdateUser(int id, [FromBody] Usuario UserAct)
    {
        try
        {
            if (id <= 0)
                return BadRequest();

            if (UserAct is null)
                return BadRequest();

            if (string.IsNullOrWhiteSpace(UserAct.Nombre))
                return BadRequest();

            if (string.IsNullOrWhiteSpace(UserAct.Apellido))
                return BadRequest();

            var userexiste = usuarios.FirstOrDefault(u => u.Id == id);
            if (userexiste is null)
                return NotFound();

            userexiste.Nombre = UserAct.Nombre;
            userexiste.Apellido = UserAct.Apellido;
            userexiste.Estado = UserAct.Estado;
            return Ok("Usuario actualizado correctamente" + userexiste);
        }
        catch (Exception ex)
        {
            return StatusCode(500, $"Ocurrió un error: {ex.Message}");
        }

    }

[HttpPut ("autos/{id}")]
public IActionResult UpdateCar(int id, [FromBody] Auto carAct)
    {
        try
        {
            if (id <= 0)
                return BadRequest();

            if (carAct is null)
                return BadRequest();

            if (carAct.Patente is null)
                return BadRequest();

            if (carAct.Usuario is null)
                return BadRequest();

            if (string.IsNullOrWhiteSpace(carAct.Patente))
                return BadRequest();

            var carexiste = autos.FirstOrDefault(c => c.Id == id);
            if (carexiste is null)
                return NotFound("No hay autos con ese ID");

            carAct.Id = carexiste.Id;
            carAct.Patente = carexiste.Patente;
            carAct.Usuario = carexiste.Usuario;
            carAct.Vendido = carexiste.Vendido;

            return Ok("car act correctamente" + carAct);
        }
        catch (Exception ex)
        {
            return StatusCode(500, $"Ocurrió un error: {ex.Message}");

        }
    }
    [HttpDelete ("user({id})")]
    public IActionResult DeleteUser(int id)
    {
    try
    {
        if(id<= 0)
        return BadRequest();
        
        var existeuser = usuarios.FirstOrDefault(u=> u.Id == id);
        if(existeuser is null)
        return NotFound();

        usuarios.Remove(existeuser);
        
        return Ok("Usuario Eliminado correct");
      

    }catch(Exception ex)
        {

        return StatusCode(500, $"Ocurrió un error: {ex.Message}");

        }
    }
    [HttpDelete ("auto/{id}")]
    public IActionResult DeleteCar(int id)
    {
        try
        {
            if(id <= 0)
            return BadRequest();

            var existecar = autos.FirstOrDefault(c=> c.Id == id);
            if(existecar is null)
            return NotFound();

            autos.Remove(existecar);

            return Ok("Auto eliminado correct");
            
        }catch (Exception ex)
        {
            return StatusCode(500,$"Ocurrio un error interno: {ex.Message}");
        }
    }

    [HttpPut ("Ventas")]
    public IActionResult UpdateSales( [FromBody] Auto ventaAuto, int id, int idusu)
    {
        try
        {
            if(id <= 0)
            return BadRequest(); 

            var idcar = autos.FirstOrDefault(a=> a.Id == id);//valida que el auto exista
            if(idcar is null)
            return NotFound();

            if(idusu <=0) 
            return BadRequest(); 

           var iduser = usuarios.FirstOrDefault(u=> u.Id == id); //valida que el usuario exista
           if(iduser is null)
           return BadRequest();

           if(iduser.Estado == false) //valida que el usuario este activo
           return BadRequest("No se puede vender a un usuario inactivo"); 

           if(ventaAuto.Vendido == true) //valida que el auto no este vendido
           return BadRequest("Este auto ya esta vendido"); 

           idcar.Vendido = true; //Cambia a vendido al auto
           idcar.Usuario = iduser; //asigna el usuario al auto que recorrio la lista

           return Ok("El auto fue vendido correct" + ventaAuto);

        }
        catch (Exception ex)
        {
            return StatusCode(500,$"Error interno del servidor: {ex.Message}");  
        }
    }
    [HttpGet ("Autos/Disponibles")]
    public IActionResult GetListDispo()
    {
        try
        {
            var Disponibles = autos.Where(a=> !a.Vendido).ToList();
            if(Disponibles.Count== 0)
            return BadRequest("No hay autos disponibles"); 


            return Ok(Disponibles);
            
            
        }
        catch(Exception ex)
        {
           return StatusCode(500,$"Error interno del sistema: {ex.Message}");
            
        }
    }
  [HttpGet ("Usuarios/Activos")]
  public IActionResult GetListUserAct()
    {
        try
        {
           var Activos = usuarios.Where(u=> !u.Estado).ToList();
           if(Activos.Count == 0)
           return BadRequest("No hay users activos");

           return Ok(Activos); 
        }
        catch(Exception ex)
        {
            return StatusCode(500, $"Error interno del sistema: {ex.Message}");
            
        }
    }

}
