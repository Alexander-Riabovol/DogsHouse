using DogsHouse.Application.CQRS.Dogs.Commands;
using DogsHouse.Application.CQRS.Dogs.Queries;
using DogsHouse.Contracts.Dogs;
using DogsHouse.Domain.Entities;
using MapsterMapper;
using MediatR;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.RateLimiting;

namespace DogsHouse.API.Controllers
{
    [ApiController]
    [EnableRateLimiting("fixed")]
    public class DogsController : ControllerBase
    {
        private readonly IMapper _mapper;
        private readonly ISender _mediator;

        public DogsController(IMapper mapper, ISender mediator) 
        {
            _mapper = mapper;
            _mediator = mediator;
        }

        // curl -X GET http://localhost:5171/dogs -v
        // Use "pageNumber" & "pageSize" query params for pagination
        // Use "attribute" & "order" query params for sorting
        [HttpGet]
        [Route("dogs")]
        public async Task<ActionResult<GetDogsResponse>> Get()
        {
            var queryParams = _mapper.Map<GetDogsQueryParams>(HttpContext.Request.Query);

            var response = await _mediator.Send(new GetDogsQuery(queryParams));
            if(response.IsError)
            {
                return StatusCode(response.StatusCode,
                                  response.ErrorMessage);
            }

            var dogs = _mapper.Map<GetDogsResponse>(response.Content!);
            return Ok(dogs);
        }

        // curl -X POST http://localhost:5171/dog -v
        // -H "Content-Type: application/json"
        // -d "{"name": "string", "color": "string", "tail_length": 1, "weight": 1}"
        //
        // "Content-Type" header in the request is necessary, without it the endpoint won't work.
        [HttpPost]
        [Route("dog")]
        public async Task<IActionResult> Post([FromBody] PostDogRequest request)
        {
            var dog = _mapper.Map<Dog>(request);

            var response = await _mediator.Send(new PostDogCommand(dog));
            if (response.IsError)
            {
                return StatusCode(response.StatusCode,
                                  response.ErrorMessage);
            }

            return Ok();
        }
    }
}
