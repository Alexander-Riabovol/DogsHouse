using DogsHouse.Application.Persistence;
using DogsHouse.Domain.Entities;
using DogsHouse.Domain.FlowControl;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace DogsHouse.Application.CQRS.Dogs.Queries
{
    public class GetDogsQueryHandler
        : IRequestHandler<GetDogsQuery, ServiceResponse<IEnumerable<Dog>>>
    {
        private readonly IDogContext _context;
        public GetDogsQueryHandler(IDogContext context)
        {
            _context = context;
        }

        public async Task<ServiceResponse<IEnumerable<Dog>>> Handle(GetDogsQuery request, 
                                                                    CancellationToken cancellationToken)
        {
            var dogs = await _context.Dogs.ToListAsync(cancellationToken);

            if(!dogs.Any())
            {
                return new ServiceResponse<IEnumerable<Dog>>(
                    404, "There are no dogs in the database");
            }

            // Sort by specific attribute (name by deafult)
            if (request.Params.Order != string.Empty ||
                request.Params.Attribute != string.Empty)
            {
                bool ascending = "ascending".StartsWith(request.Params.Order.ToLower());

                switch(request.Params.Attribute)
                {
                    case "color":
                        dogs = ascending ? dogs.OrderBy(d => d.color).ToList()
                                         : dogs.OrderByDescending(d => d.color).ToList();
                        break;
                    case "tail_length":
                        dogs = ascending ? dogs.OrderBy(d => d.tail_length).ToList()
                                         : dogs.OrderByDescending(d => d.tail_length).ToList();
                        break;
                    case "weight":
                        dogs = ascending ? dogs.OrderBy(d => d.weight).ToList()
                                         : dogs.OrderByDescending(d => d.weight).ToList();
                        break;
                    default:
                        dogs = ascending ? dogs.OrderBy(d => d.name).ToList()
                                         : dogs.OrderByDescending(d => d.name).ToList();
                        break;
                }
            }
            
            // Pagination
            if (request.Params.PageNumber != string.Empty &&
                request.Params.PageSize != string.Empty)
            {
                int pageNumber = int.Parse(request.Params.PageNumber);
                int pageSize = int.Parse(request.Params.PageSize);

                dogs = dogs.Skip(pageNumber * pageSize).Take(pageSize).ToList();
            }

            return ServiceResponse<IEnumerable<Dog>>.OK(dogs);
        }
    }
}
