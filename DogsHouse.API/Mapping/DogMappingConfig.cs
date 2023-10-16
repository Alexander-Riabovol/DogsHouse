using DogsHouse.Contracts.Dogs;
using DogsHouse.Domain.Entities;
using Mapster;

namespace DogsHouse.API.Mapping
{
    internal class DogMappingConfig : IRegister
    {
        public void Register(TypeAdapterConfig config)
        {
            config.NewConfig<Dog, GetDogResponse>()
                  .Map(dest => dest.name, src => src.name)
                  .Map(dest => dest.color, src => src.color)
                  .Map(dest => dest.tail_length, src => src.tail_length)
                  .Map(dest => dest.weight, src => src.weight);

            config.NewConfig<IEnumerable<Dog>, GetDogsResponse>()
                  .Map(dest => dest.Dogs, src => src);
        }
    }
}
