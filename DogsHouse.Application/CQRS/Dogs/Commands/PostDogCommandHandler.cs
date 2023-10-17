using DogsHouse.Application.Persistence;
using DogsHouse.Domain.FlowControl;
using MediatR;

namespace DogsHouse.Application.CQRS.Dogs.Commands
{
    public class PostDogCommandHandler
        : IRequestHandler<PostDogCommand, ServiceResponse>
    {
        private readonly IDogContext _context;
        public PostDogCommandHandler(IDogContext context)
        {
            _context = context;
        }

        public async Task<ServiceResponse> Handle(PostDogCommand request, 
                                                  CancellationToken cancellationToken)
        {
            var namesake = await _context.Dogs.FindAsync(request.Dog.name);
            if(namesake is not null)
            {
                return new ServiceResponse(
                    400, $"{request.Dog.name} already exists in the database. Choose a different name.");
            }

            await _context.Dogs.AddAsync(request.Dog);
            await _context.SaveAsync();

            return ServiceResponse.OK;
        }
    }
}
