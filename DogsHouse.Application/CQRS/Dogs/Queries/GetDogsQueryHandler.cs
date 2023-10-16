using DogsHouse.Domain.Entities;
using DogsHouse.Domain.FlowControl;
using MediatR;

namespace DogsHouse.Application.CQRS.Dogs.Queries
{
    public class GetDogsQueryHandler
        : IRequestHandler<GetDogsQuery, ServiceResponse<IEnumerable<Dog>>>
    {
        public async Task<ServiceResponse<IEnumerable<Dog>>> Handle(GetDogsQuery request, 
                                                                    CancellationToken cancellationToken)
        {
            var mocked = new List<Dog>()
            {
                new Dog { name = "Neo", color = "red&amber", tail_length = 22, weight = 32 },
                new Dog { name = "Jessy", color = "black&white", tail_length = 7, weight = 14 },
            };

            return ServiceResponse<IEnumerable<Dog>>.OK(mocked);
        }
    }
}
