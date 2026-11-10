using Microsoft.EntityFrameworkCore;

namespace Testing.Data;
public class AutoRepository
{
    private readonly AppDbContext _context;

    public AutoRepository(AppDbContext context)
    {
        _context = context;
    }

    public Task<List<Auto>> ObtenerCarsREP()
    {
        return _context.autos.ToListAsync();
    }

    public async Task<Auto> GetAsyncID( int id)
    {
        var obtenerid = await _context.autos.FirstOrDefaultAsync(a=> a.Id == id);
        if(obtenerid is null)
        return null; 

        return obtenerid;
    }
    public async Task AgregarAsync(Auto auto)
    {
        _context.autos.Add(auto);
        await _context.SaveChangesAsync();
    }

    public async Task<Auto> ExisteIDCAR(int id)
    {
        return await _context.autos.FirstOrDefaultAsync(a=> a.Id == id);
        
    }
    public async Task<bool> BorrarAsyncCar(int id)
    {
        var auto = await _context.autos.FirstOrDefaultAsync(a => a.Id == id);

        if (auto is null)
            return false;

        _context.autos.Remove(auto);
        // Remove solo marca la entidad; SaveChangesAsync persiste el DELETE en la base.
        await _context.SaveChangesAsync();
        return true;

    }
}