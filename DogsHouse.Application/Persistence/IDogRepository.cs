using DogsHouse.Domain.Entities;

namespace DogsHouse.Application.Persistence
{
    public interface IDogRepository
    {
        Task<IEnumerable<Dog>> GetAllAsync();
        Task<Dog?> GetByNameAsync(string name);
        Task CreateAsync(Dog dog);
    }
}
