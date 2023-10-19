using FluentValidation;
using FluentValidation.Results;

namespace DogsHouse.Application.CQRS.Dogs.Queries
{
    /// <summary>
    /// Used by FluentValidation to validate <see cref="GetDogsQuery"/> instances.
    /// </summary>
    public class GetDogsQueryValidator : AbstractValidator<GetDogsQuery>
    {
        public GetDogsQueryValidator() 
        {
            // Add a validation error if there is a pageNumber parameter 
            // but no pageSize parameter in the URL query and vice versa.
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
                        // Add a validation error if wrong format or negative
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
                        // Add a validation error if wrong format or less than 1
                        if (!int.TryParse(pageSize, out parsedPageSize))
                        {
                            context.AddFailure(new ValidationFailure(
                                "pageSize", "This parameter must be integer."));
                        }
                        else if (parsedPageSize < 1)
                        {
                            context.AddFailure(new ValidationFailure(
                                "pageSize", "This parameter must be positive."));
                        }
                    }
                });

            RuleFor(x => x.Params.Order)
                .Custom((order, context) =>
                {
                    if(order != string.Empty)
                    {
                        // Add a validation error if the "order" URL query parameter
                        // is not a substring of the "ascending" or "descending" words.
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
                    // Add a validation error id the "attribute" URL query parameter
                    // does not correspond to one of the Dog entity properties.
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
