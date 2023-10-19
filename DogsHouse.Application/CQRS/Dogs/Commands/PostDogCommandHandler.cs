using DogsHouse.Application.Persistence;
using DogsHouse.Domain.FlowControl;
using MediatR;

namespace DogsHouse.Application.CQRS.Dogs.Commands
{
    /// <summary>
    /// Used by MediatR to handle <see cref="PostDogCommand"/> instances.
    /// </summary>
    public class PostDogCommandHandler
        : IRequestHandler<PostDogCommand, ServiceResponse>
    {
        private readonly IDogRepository _dogRepository;
        public PostDogCommandHandler(IDogRepository dogRepository)
        {
            _dogRepository = dogRepository;
        }

        public async Task<ServiceResponse> Handle(PostDogCommand request, 
                                                  CancellationToken cancellationToken)
        {
            // Check if there is a dog with the newdog's name in the db already.
            var namesake = await _dogRepository.GetByNameAsync(request.NewDog.name);
            if(namesake is not null)
            {
                // Return BadRequest if so
                return new ServiceResponse(
                    400, $"{request.NewDog.name} already exists in the database. Choose a different name.");
            }

            await _dogRepository.CreateAsync(request.NewDog);

            return ServiceResponse.OK;
        }
    }
}
