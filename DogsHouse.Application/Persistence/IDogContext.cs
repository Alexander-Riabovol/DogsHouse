using DogsHouse.Domain.Entities;
using Microsoft.EntityFrameworkCore;

namespace DogsHouse.Application.Persistence
{
    public interface IDogContext
    {
        DbSet<Dog> Dogs { get; }
        Task SaveAsync();
    }
}
