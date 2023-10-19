using DogsHouse.Domain.Entities;
using DogsHouse.Domain.FlowControl;
using MediatR;

namespace DogsHouse.Application.CQRS.Dogs.Commands
{
    /// <summary>
    /// MediatR command responsible for creating a new <see cref="Dog"/>.
    /// </summary>
    /// <param name="NewDog">The dog that will be created.</param>
    public record PostDogCommand(Dog NewDog) : IRequest<ServiceResponse>;
}
