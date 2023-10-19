using DogsHouse.Domain.Entities;

namespace DogsHouse.Application.Persistence
{
    /// <summary>
    /// Repository that manages <see cref="Dog"/> entities.
    /// </summary>
    public interface IDogRepository
    {
        /// <summary>
        /// Gets all dogs from the repository.
        /// </summary>
        /// <returns>List of <see cref="Dog"/> instances.</returns>
        Task<IEnumerable<Dog>> GetAllAsync();
        /// <summary>
        /// Gets a dog from the repository by its name.
        /// </summary>
        /// <param name="name">Name of the dog.</param>
        /// <returns>A <see cref="Dog"/> instance.</returns>
        Task<Dog?> GetByNameAsync(string name);
        /// <summary>
        /// Creates a dog in the repository.
        /// </summary>
        /// <param name="dog">Dog that will be added to the repo.</param>
        /// <returns>Asyncronous <see cref="Task"/> that creates a
        /// <see cref="Dog"/> entry and saves it to the repository.</returns>
        Task CreateAsync(Dog dog);
    }
}
