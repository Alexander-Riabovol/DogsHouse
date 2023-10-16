using DogsHouse.Domain.FlowControl;
using MediatR;

namespace DogsHouse.Application.CQRS.Dogs.Commands
{
    public class PostDogCommandHandler
        : IRequestHandler<PostDogCommand, ServiceResponse>
    {
        public async Task<ServiceResponse> Handle(PostDogCommand request, 
                                                  CancellationToken cancellationToken)
        {
            return ServiceResponse.OK;
        }
    }
}
