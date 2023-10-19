using DogsHouse.Application.CQRS.Dogs.Commands;
using DogsHouse.Application.UnitTests.CQRS.TestUtils;
using FluentAssertions;

namespace DogsHouse.Application.UnitTests.CQRS.Dogs.Commands
{
    public class PostDogCommandValidatorTest
    {
        private readonly PostDogCommandValidator _sut;

        public PostDogCommandValidatorTest()
        {
            _sut = new PostDogCommandValidator();
        }

        [Fact]
        public async Task ValidatePostDogCommand_WhenDogIsValid_BeValid()
        {
            //Arrange
            var command = DogUtils.CreatePostDogCommand();

            //Act
            var result = await _sut.ValidateAsync(command);

            //Assert
            result.IsValid.Should().BeTrue();
        }

        [Fact]
        public async Task ValidatePostDogCommand_WhenNameIsEmpty_ReturnValidationError()
        {
            //Arrange
            var command = DogUtils.CreatePostDogCommand(name: "");

            //Act
            var result = await _sut.ValidateAsync(command);

            //Assert
            result.IsValid.Should().BeFalse();
            result.Errors.FirstOrDefault(x => x.PropertyName == "Dog.name").Should().NotBeNull();
        }

        [Fact]
        public async Task ValidatePostDogCommand_WhenNameIsLongerThan250Characters_ReturnValidationError()
        {
            //Arrange
            var command = DogUtils.CreatePostDogCommand(name: new string('a', 251));

            //Act
            var result = await _sut.ValidateAsync(command);

            //Assert
            result.IsValid.Should().BeFalse();
            result.Errors.FirstOrDefault(x => x.PropertyName == "Dog.name").Should().NotBeNull();
        }

        [Fact]
        public async Task ValidatePostDogCommand_WhenColorIsEmpty_ReturnValidationError()
        {
            //Arrange
            var command = DogUtils.CreatePostDogCommand(color: "");

            //Act
            var result = await _sut.ValidateAsync(command);

            //Assert
            result.IsValid.Should().BeFalse();
            result.Errors.FirstOrDefault(x => x.PropertyName == "Dog.color").Should().NotBeNull();
        }

        [Fact]
        public async Task ValidatePostDogCommand_WhenColorIsLongerThan250Characters_ReturnValidationError()
        {
            //Arrange
            var command = DogUtils.CreatePostDogCommand(color: new string('a', 251));

            //Act
            var result = await _sut.ValidateAsync(command);

            //Assert
            result.IsValid.Should().BeFalse();
            result.Errors.FirstOrDefault(x => x.PropertyName == "Dog.color").Should().NotBeNull();
        }

        [Fact]
        public async Task ValidatePostDogCommand_WhenTailLengthIsNegative_ReturnValidationError()
        {
            //Arrange
            var command = DogUtils.CreatePostDogCommand(tailLength: -1);

            //Act
            var result = await _sut.ValidateAsync(command);

            //Assert
            result.IsValid.Should().BeFalse();
            result.Errors.FirstOrDefault(x => x.PropertyName == "Dog.tail_length").Should().NotBeNull();
        }

        [Fact]
        public async Task ValidatePostDogCommand_WhenWeightIsNegative_ReturnValidationError()
        {
            //Arrange
            var command = DogUtils.CreatePostDogCommand(weight: -1);

            //Act
            var result = await _sut.ValidateAsync(command);

            //Assert
            result.IsValid.Should().BeFalse();
            result.Errors.FirstOrDefault(x => x.PropertyName == "Dog.weight").Should().NotBeNull();
        }
    }
}
