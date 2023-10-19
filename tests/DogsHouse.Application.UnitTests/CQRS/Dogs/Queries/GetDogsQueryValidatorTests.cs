using DogsHouse.Application.CQRS.Dogs.Queries;
using DogsHouse.Application.UnitTests.CQRS.TestUtils;
using FluentAssertions;

namespace DogsHouse.Application.UnitTests.CQRS.Dogs.Queries
{
    public class GetDogsQueryValidatorTests
    {
        private readonly GetDogsQueryValidator _sut;

        public GetDogsQueryValidatorTests() 
        {
            _sut = new GetDogsQueryValidator();
        }

        [Fact]
        public async Task ValidateGetDogQuery_WhenParamsAreEmpty_BeValid()
        {
            //Arrange
            var query = DogUtils.CreateGetDogsQuery();

            //Act
            var result = await _sut.ValidateAsync(query);

            //Assert
            result.IsValid.Should().BeTrue();
        }

        [Fact]
        public async Task ValidateGetDogQuery_WhenParamsAreValid_BeValid()
        {
            //Arrange
            var query = new GetDogsQuery(new GetDogsQueryParams()
            {
                Attribute = "tail_length",
                Order = "asc",
                PageNumber = "0",
                PageSize = "1",
            });

            //Act
            var result = await _sut.ValidateAsync(query);

            //Assert
            result.IsValid.Should().BeTrue();
        }

        [Fact]
        public async Task ValidateGetDogQuery_WithOnly1PaginationParam_BeInvalid()
        {
            //Arrange
            var query = new GetDogsQuery(new GetDogsQueryParams()
            {
                PageNumber = "0",
            });

            //Act
            var result = await _sut.ValidateAsync(query);

            //Assert
            result.IsValid.Should().BeFalse();
        }

        [Fact]
        public async Task ValidateGetDogQuery_WithInvalidAttributeNOrder_BeInvalid()
        {
            //Arrange
            var query = new GetDogsQuery(new GetDogsQueryParams()
            {
                Attribute = "taillength",
                Order = "ascendings",
            });

            //Act
            var result = await _sut.ValidateAsync(query);

            //Assert
            result.IsValid.Should().BeFalse();
            result.Errors.FirstOrDefault(x => x.PropertyName == "attribute").Should().NotBeNull();
            result.Errors.FirstOrDefault(x => x.PropertyName == "order").Should().NotBeNull();
        }

        [Fact]
        public async Task ValidateGetDogQuery_WithInvalidValuesPaginationParams_BeInvalid()
        {
            //Arrange
            var query = new GetDogsQuery(new GetDogsQueryParams()
            {
                PageNumber = "-20",
                PageSize = "0",
            });

            //Act
            var result = await _sut.ValidateAsync(query);

            //Assert
            result.IsValid.Should().BeFalse();
            result.Errors.FirstOrDefault(x => x.PropertyName == "pageNumber").Should().NotBeNull();
            result.Errors.FirstOrDefault(x => x.PropertyName == "pageSize").Should().NotBeNull();
        }

        [Fact]
        public async Task ValidateGetDogQuery_WithInvalidTypesPaginationParams_BeInvalid()
        {
            //Arrange
            var query = new GetDogsQuery(new GetDogsQueryParams()
            {
                PageNumber = "1.5",
                PageSize = "abc",
            });

            //Act
            var result = await _sut.ValidateAsync(query);

            //Assert
            result.IsValid.Should().BeFalse();
            result.Errors.FirstOrDefault(x => x.PropertyName == "pageNumber").Should().NotBeNull();
            result.Errors.FirstOrDefault(x => x.PropertyName == "pageSize").Should().NotBeNull();
        }
    }
}
