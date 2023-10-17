using DogsHouse.Domain.Entities;
using DogsHouse.Domain.FlowControl;
using FluentValidation;
using FluentValidation.Results;
using MediatR;

namespace DogsHouse.Application.CQRS.Dogs.Queries
{
    public class GetDogsQueryValidator : AbstractValidator<GetDogsQuery>
    {
        public GetDogsQueryValidator() 
        {
            RuleFor(x => x.Params)
                .Must(x => (x.PageNumber == string.Empty && x.PageSize == string.Empty) ||
                           (x.PageNumber != string.Empty && x.PageSize != string.Empty))
                .WithMessage(x => "You're missing one of the pagination parameters.");

            RuleFor(x => x.Params.PageNumber)
                .Custom((pageNumber, context) =>
                {
                    if(pageNumber != string.Empty)
                    {
                        int parsedPageNumber;
                        if (!int.TryParse(pageNumber, out parsedPageNumber))
                        {
                            context.AddFailure(new ValidationFailure(
                                "pageNumber", "This parameter must be integer."));
                        }
                        else if (parsedPageNumber < 0)
                        {
                            context.AddFailure(new ValidationFailure(
                                "pageNumber", "This parameter must not be negative."));
                        }
                    }
                });

            RuleFor(x => x.Params.PageSize)
                .Custom((pageSize, context) =>
                {
                    if(pageSize != string.Empty)
                    {
                        int parsedPageSize;
                        if (!int.TryParse(pageSize, out parsedPageSize))
                        {
                            context.AddFailure(new ValidationFailure(
                                "pageSize", "This parameter must be integer."));
                        }
                        else if (parsedPageSize < 0)
                        {
                            context.AddFailure(new ValidationFailure(
                                "pageSize", "This parameter must not be negative."));
                        }
                    }
                });

            RuleFor(x => x.Params.Order)
                .Custom((order, context) =>
                {
                    if(order != string.Empty)
                    {
                        if (!"ascending".StartsWith(order.ToLower()) &&
                            !"descending".StartsWith(order.ToLower()))
                        {
                            context.AddFailure(new ValidationFailure(
                                "order", "This param must be an abbreviation of 'ascending' or 'descending'."));
                        }
                    }   
                });

            RuleFor(x => x.Params.Attribute)
                .Custom((attr, context) =>
                {
                    if(attr != string.Empty) 
                    { 
                        if(attr != "name" &&
                           attr != "color" &&
                           attr != "tail_length"
                           && attr != "weight")
                        {
                            context.AddFailure(new ValidationFailure(
                                "attribute", $"There is no field with name {attr} in the Dog Entity."));
                        }
                    }
                });
        }
    }
}
