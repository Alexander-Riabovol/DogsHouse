using DogsHouse.Application.CQRS.Dogs.Commands;
using DogsHouse.Application.Persistence;
using DogsHouse.Application.UnitTests.CQRS.TestUtils;
using DogsHouse.Domain.Entities;
using FluentAssertions;
using Moq;

namespace DogsHouse.Application.UnitTests.CQRS.Dogs.Commands
{
    public class PostDogCommandHandlerTests
    {
        private readonly PostDogCommandHandler _sut;
        private readonly Mock<IDogRepository> _mockDogRepo;

        public PostDogCommandHandlerTests()
        {
            _mockDogRepo = new Mock<IDogRepository>();
            _sut = new PostDogCommandHandler(_mockDogRepo.Object);
        }

        [Fact]
        public async Task HandlePostDogCommand_WhenDogsNameIsUnique_CreateAndSaveToDB()
        {
            // Arrange
            var command = DogUtils.CreatePostDogCommand();
            _mockDogRepo.Setup(x => x.GetByNameAsync(command.NewDog.name))
                        .ReturnsAsync(default(Dog));

            // Act
            var result = await _sut.Handle(command, default);

            // Assert
            // Verifing that there is no error
            result.IsError.Should().BeFalse();
            // Verifing that the dog was added to the db
            _mockDogRepo.Verify(r => r.CreateAsync(command.NewDog), Times.Once);
        }

        [Fact]
        public async Task HandlePostDogCommand_WhenNamesakeExistsInDB_ReturnBadRequest()
        {
            // Arrange
            var command = DogUtils.CreatePostDogCommand();
            _mockDogRepo.Setup(x => x.GetByNameAsync(command.NewDog.name))
                        .ReturnsAsync(DogUtils.CreateNamesake(command.NewDog.name));

            // Act
            var result = await _sut.Handle(command, default);

            // Assert
            // Verifing that the error is BadRequest
            result.IsError.Should().BeTrue();
            result.StatusCode.Should().Be(400);
            // Verifing that the dog wasn't added to the db
            _mockDogRepo.Verify(r => r.CreateAsync(command.NewDog), Times.Never);
        }
    }
}
