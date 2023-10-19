using DogsHouse.Application.CQRS.Dogs.Queries;
using DogsHouse.Application.Persistence;
using DogsHouse.Application.UnitTests.CQRS.TestUtils;
using DogsHouse.Domain.Entities;
using FluentAssertions;
using Moq;

namespace DogsHouse.Application.UnitTests.CQRS.Dogs.Queries
{
    public class GetDogsQueryHandlerTests
    {
        private readonly GetDogsQueryHandler _sut;
        private readonly Mock<IDogRepository> _mockDogRepo;

        public GetDogsQueryHandlerTests()
        {
            _mockDogRepo = new Mock<IDogRepository>();
            _sut = new GetDogsQueryHandler(_mockDogRepo.Object);
        }

        [Fact]
        public async Task HandleGetDogQuery_WhenDBIsEmpty_ReturnNotFound()
        {
            // Arrange
            var query = DogUtils.CreateGetDogsQuery();
            _mockDogRepo.Setup(x => x.GetAllAsync())
                        .ReturnsAsync(new List<Dog>());

            // Act
            var result = await _sut.Handle(query, default);

            // Assert
            result.IsError.Should().BeTrue();
            result.StatusCode.Should().Be(404);
        }

        [Fact]
        public async Task HandleGetDogQuery_WhenDBIsNotEmpty_ReturnAllEntities()
        {
            // Arrange
            var query = DogUtils.CreateGetDogsQuery();

            var dogs = DogUtils.CreateDogs();
            _mockDogRepo.Setup(x => x.GetAllAsync())
                        .ReturnsAsync(dogs);

            // Act
            var result = await _sut.Handle(query, default);

            // Assert
            result.IsError.Should().BeFalse();
            result.Content.Should().BeSameAs(dogs);
        }

        [Fact]
        public async Task HandleGetDogQuery_WithSortingParams_ReturnResultInDescendingOrderByColor()
        {
            // Arrange
            var query = DogUtils.CreateGetDogsQuery(descending: true, attributeNumber: 1);

            var dogs = DogUtils.CreateDogs();
            _mockDogRepo.Setup(x => x.GetAllAsync())
                        .ReturnsAsync(dogs);

            // Act
            var result = await _sut.Handle(query, default);

            // Assert
            result.IsError.Should().BeFalse();
            result.Content.Should().BeInDescendingOrder(x => x.color);
        }

        [Fact]
        public async Task HandleGetDogQuery_WithSortingParams_ReturnResultInAscendingOrderByWeight()
        {
            // Arrange
            var query = DogUtils.CreateGetDogsQuery(descending: false, attributeNumber: 3);

            var dogs = DogUtils.CreateDogs();
            _mockDogRepo.Setup(x => x.GetAllAsync())
                        .ReturnsAsync(dogs);

            // Act
            var result = await _sut.Handle(query, default);

            // Assert
            result.IsError.Should().BeFalse();
            result.Content.Should().BeInAscendingOrder(x => x.weight);
        }

        [Fact]
        public async Task HandleGetDogQuery_WithPaginationParams_ReturnFirst10Entities()
        {
            // Arrange
            var query = DogUtils.CreateGetDogsQuery(pageNumber: 0, pageSize: 10);

            var dogs = DogUtils.CreateDogs();
            _mockDogRepo.Setup(x => x.GetAllAsync())
                        .ReturnsAsync(dogs);

            // Act
            var result = await _sut.Handle(query, default);

            // Assert
            result.IsError.Should().BeFalse();
            result.Content.Should().BeEquivalentTo(dogs.Take(10));
        }

        [Fact]
        public async Task HandleGetDogQuery_WithPaginationParams_ReturnFrom85thTo101thEntities()
        {
            // Arrange
            var query = DogUtils.CreateGetDogsQuery(pageNumber: 5, pageSize: 17);

            var dogs = DogUtils.CreateDogs(130);
            _mockDogRepo.Setup(x => x.GetAllAsync())
                        .ReturnsAsync(dogs);

            // Act
            var result = await _sut.Handle(query, default);

            // Assert
            result.IsError.Should().BeFalse();
            result.Content.Should().BeEquivalentTo(dogs.Skip(5 * 17).Take(17));
        }

        [Fact]
        public async Task HandleGetDogQuery_WithPaginationParams_ReturnBadRequest()
        {
            // Arrange
            var query = DogUtils.CreateGetDogsQuery(pageNumber: 1, pageSize: 100);

            var dogs = DogUtils.CreateDogs(50);
            _mockDogRepo.Setup(x => x.GetAllAsync())
                        .ReturnsAsync(dogs);

            // Act
            var result = await _sut.Handle(query, default);

            // Assert
            result.IsError.Should().BeTrue();
            result.StatusCode.Should().Be(400);
        }

        [Fact]
        public async Task HandleGetDogQuery_WithAllParams_ReturnCorrectElementSequence()
        {
            // Arrange
            var query = DogUtils.CreateGetDogsQuery(pageNumber: 3, pageSize: 14, attributeNumber: 1, descending: true);

            var dogs = DogUtils.CreateDogs(130);
            _mockDogRepo.Setup(x => x.GetAllAsync())
                        .ReturnsAsync(dogs);

            // Act
            var result = await _sut.Handle(query, default);

            // Assert
            result.IsError.Should().BeFalse();
            result.Content.Should().BeEquivalentTo(
                dogs.OrderByDescending(d => d.color).Skip(3 * 14).Take(14));
        }
    }
}
