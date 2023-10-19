using DogsHouse.Application.Persistence;
using DogsHouse.Domain.Entities;
using DogsHouse.Infrastructure.Persistence;
using Microsoft.EntityFrameworkCore;

namespace DogsHouse.Infrastructure.Repositories
{
    public class DogRepository : IDogRepository
    {
        private readonly DogContext _context;

        public DogRepository(DogContext context)
        {
            _context = context;
        }

        public async Task CreateAsync(Dog dog)
        {
            await _context.Dogs.AddAsync(dog);
            await _context.SaveChangesAsync();
        }

        public async Task<IEnumerable<Dog>> GetAllAsync() =>
            await _context.Dogs.ToListAsync();

        public async Task<Dog?> GetByNameAsync(string name) => 
            await _context.Dogs.FindAsync(name);
    }
}
