using Microsoft.EntityFrameworkCore;
using PropEase.API.Data;
using PropEase.API.Models;

namespace PropEase.API.Services
{
    public class ImovelService
    {
        private readonly AppDbContext _db;

        public ImovelService(AppDbContext db)
        {
            _db = db;
        }

        public async Task<List<Imovel>> GetAllAsync()
        {
            return await _db.Imoveis.Include(i => i.Proprietario).ToListAsync();
        }

        public async Task<Imovel?> GetByIdAsync(int id)
        {
            return await _db.Imoveis.Include(i => i.Proprietario).FirstOrDefaultAsync(i => i.Id == id);
        }

        public async Task<Imovel> AddAsync(Imovel imovel)
        {
            _db.Imoveis.Add(imovel);
            await _db.SaveChangesAsync();
            return imovel;
        }

        public async Task<bool> DeleteAsync(int id)
        {
            var imovel = await _db.Imoveis.FindAsync(id);
            if (imovel == null) return false;

            _db.Imoveis.Remove(imovel);
            await _db.SaveChangesAsync();
            return true;
        }

        public async Task<bool> AlugarAsync(int id)
        {
            var imovel = await _db.Imoveis.FindAsync(id);
            if (imovel == null) return false;

            imovel.Alugar();
            await _db.SaveChangesAsync();
            return true;
        }

        public async Task<bool> DisponibilizarAsync(int id)
        {
            var imovel = await _db.Imoveis.FindAsync(id);
            if (imovel == null) return false;

            imovel.Disponibilizar();
            await _db.SaveChangesAsync();
            return true;
        }
    }
}
