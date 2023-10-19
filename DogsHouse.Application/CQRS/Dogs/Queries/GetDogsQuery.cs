using DogsHouse.Domain.Entities;
using DogsHouse.Domain.FlowControl;
using MediatR;

namespace DogsHouse.Application.CQRS.Dogs.Queries
{
    /// <summary>
    /// A MediatR query responsible for getting all the <see cref="Dog"/>s from the database which satisfy specific conditions.
    /// </summary>
    /// <param name="Params">Query parameters of URL. Used for sorting and paginating the result.</param>
    public record GetDogsQuery(GetDogsQueryParams Params) 
        : IRequest<ServiceResponse<IEnumerable<Dog>>>;
}
