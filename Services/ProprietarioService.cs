using Microsoft.EntityFrameworkCore;
using PropEase.API.Data;
using PropEase.API.Models;

namespace PropEase.API.Services
{
    public class ProprietarioService
    {
        private readonly AppDbContext _db;
        public ProprietarioService(AppDbContext db) { _db = db; }

        public async Task<List<Proprietario>> GetAllAsync() => await _db.Proprietarios.ToListAsync();
        public async Task<Proprietario?> GetByIdAsync(int id) => await _db.Proprietarios.FindAsync(id);
        public async Task<Proprietario> AddAsync(Proprietario p)
        {
            _db.Proprietarios.Add(p);
            await _db.SaveChangesAsync();
            return p;
        }
        public async Task<bool> UpdateAsync(int id, Proprietario p)
        {
            var existing = await _db.Proprietarios.FindAsync(id);
            if (existing is null) return false;
            existing.Nome = p.Nome;
            existing.Telefone = p.Telefone;
            existing.CPF = p.CPF;
            await _db.SaveChangesAsync();
            return true;
        }
        public async Task<bool> DeleteAsync(int id)
        {
            var p = await _db.Proprietarios.FindAsync(id);
            if (p is null) return false;
            _db.Proprietarios.Remove(p);
            await _db.SaveChangesAsync();
            return true;
        }
    }
}
