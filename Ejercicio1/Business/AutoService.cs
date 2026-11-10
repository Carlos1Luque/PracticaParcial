using Testing.Business;
using Testing.Data;

namespace Testing.Business;

public class AutoService
{
    private readonly AutoRepository _repository;

    public AutoService(AutoRepository repository)
    {
        _repository = repository;
    }

    public async Task<List<GetResponseAutos>> GetAllCar()
    {
        var autos = await _repository.ObtenerCarsREP();

        return  autos.Select (auto => new GetResponseAutos
        {
            Id = auto.Id,
            Patente = auto.Patente,
            Vendido = auto.Vendido
        }).ToList();
    }

    public async Task<GetResponseAutos> GetIDcar(int id)
    {
        var getidcar = await _repository.GetAsyncID(id);

        if(getidcar is null)
        return null;

        return new GetResponseAutos
        {
            Id = getidcar.Id,
            Patente = getidcar.Patente,
            Vendido = getidcar.Vendido,
        };
    }

    public async Task<PostResponseAuto> CreateCar(PostResponseAuto DTOAUTO)
    {
        if(DTOAUTO is null)
        return null;

        var crearAuto = await _repository.ExisteIDCAR(DTOAUTO.Id); 
        if(crearAuto is not null)
        return null;

        var auto = new Auto
        {
            Id = DTOAUTO.Id,
            Patente = DTOAUTO.Patente,
            Vendido = DTOAUTO.Vendido,
            
        };
        await _repository.AgregarAsync(auto);
        return null;
    }
    public Task<bool> DeleteAUTO(int id)
    {
        // El servicio recibe el ID y delega el borrado; no necesita un DTO completo del auto.
        return _repository.BorrarAsyncCar(id);
    }
}