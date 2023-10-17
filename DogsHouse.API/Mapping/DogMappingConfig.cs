using DogsHouse.Application.CQRS.Dogs.Queries;
using DogsHouse.Contracts.Dogs;
using DogsHouse.Domain.Entities;
using Mapster;

namespace DogsHouse.API.Mapping
{
    internal class DogMappingConfig : IRegister
    {
        public void Register(TypeAdapterConfig config)
        {
            config.NewConfig<IEnumerable<Dog>, GetDogsResponse>()
                  .Map(dest => dest.Dogs, src => src);

            config.NewConfig<IQueryCollection, GetDogsQueryParams>()
                  .Map(dest => dest.Attribute, src => src["attribute"])
                  .Map(dest => dest.Order, src => src["order"])
                  .Map(dest => dest.PageNumber, src => src["pageNumber"])
                  .Map(dest => dest.PageSize, src => src["pageSize"]);
        }
    }
}
