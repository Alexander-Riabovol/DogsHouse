using DogsHouse.Domain.FlowControl;
using FluentValidation;
using MediatR;

namespace DogsHouse.Application.CQRS.Behaviors
{
    /// <summary>
    /// MediatR pipeline behavior which validates queries and commands.
    /// </summary>
    public class ValidationBehavior<TRequest, TResponse>
        : IPipelineBehavior<TRequest, TResponse>
        where TRequest : IRequest<TResponse>
        where TResponse : ServiceResponse, new()
    {
        private readonly IValidator<TRequest>? _validator;

        // We need to make the validator parameter optional to
        // avoid an excepetion in case there is no appropriate validator
        public ValidationBehavior(IValidator<TRequest>? validator = null)
        {
            _validator = validator;
        }

        public async Task<TResponse> Handle(
                TRequest request,
                RequestHandlerDelegate<TResponse> next,
                CancellationToken cancellationToken)
        {
            // If there is no suitable validator, exit the pipeline.
            if (_validator is null)
            {
                return await next();
            }

            var validationResult = await _validator.ValidateAsync(request, cancellationToken);
            
            if (validationResult.IsValid)
            {
                return await next();
            }

            // Convert validation errors into IEnumerable<string>
            var errors = validationResult.Errors.Select(x => $"{x.PropertyName}: {x.ErrorMessage}");

            // We need to use reflection, even though we applied ServiceResponse
            // as a constraint to TResponse type, because we get an exception
            // when trying to convert ServiceResponse to its generic.
            dynamic serviceResponse = Activator.CreateInstance(
                    typeof(TResponse),
                    400, // BadRequest
                    string.Join('\n', errors))!; // Merge errors into 1 string.

            return serviceResponse;
        }
    }
}
