using DogsHouse.Domain.Entities;
using DogsHouse.Domain.FlowControl;
using MediatR;

namespace DogsHouse.Application.CQRS.Dogs.Commands
{
    public record PostDogCommand(Dog Dog) : IRequest<ServiceResponse>;
}
