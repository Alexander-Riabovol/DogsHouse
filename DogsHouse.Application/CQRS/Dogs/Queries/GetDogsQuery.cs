using DogsHouse.Domain.Entities;
using DogsHouse.Domain.FlowControl;
using MediatR;

namespace DogsHouse.Application.CQRS.Dogs.Queries
{
    public record GetDogsQuery() : IRequest<ServiceResponse<IEnumerable<Dog>>>;
}
