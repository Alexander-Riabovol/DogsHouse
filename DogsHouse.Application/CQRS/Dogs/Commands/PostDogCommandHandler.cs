using DogsHouse.Application.Persistence;
using DogsHouse.Domain.FlowControl;
using MediatR;

namespace DogsHouse.Application.CQRS.Dogs.Commands
{
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
            var namesake = await _dogRepository.GetByNameAsync(request.Dog.name);
            if(namesake is not null)
            {
                return new ServiceResponse(
                    400, $"{request.Dog.name} already exists in the database. Choose a different name.");
            }

            await _dogRepository.CreateAsync(request.Dog);

            return ServiceResponse.OK;
        }
    }
}
