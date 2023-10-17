using FluentValidation;

namespace DogsHouse.Application.CQRS.Dogs.Commands
{
    public class PostDogCommandValidator : AbstractValidator<PostDogCommand>
    {
        public PostDogCommandValidator() 
        {
            RuleFor(x => x.Dog.name).NotNull().Length(1, 250);
            RuleFor(x => x.Dog.color).NotNull().Length(1, 250);
            RuleFor(x => x.Dog.tail_length).NotNull().GreaterThan(0);
            RuleFor(x => x.Dog.weight).NotNull().GreaterThan(0);
        }
    }
}
