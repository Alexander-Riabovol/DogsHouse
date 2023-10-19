using FluentValidation;

namespace DogsHouse.Application.CQRS.Dogs.Commands
{
    /// <summary>
    /// Used by FluentValidation to validate <see cref="PostDogCommand"/> instances.
    /// </summary>
    public class PostDogCommandValidator : AbstractValidator<PostDogCommand>
    {
        public PostDogCommandValidator() 
        {
            RuleFor(x => x.NewDog.name).NotNull().Length(1, 250);
            RuleFor(x => x.NewDog.color).NotNull().Length(1, 250);
            RuleFor(x => x.NewDog.tail_length).NotNull().GreaterThan(0);
            RuleFor(x => x.NewDog.weight).NotNull().GreaterThan(0);
        }
    }
}
