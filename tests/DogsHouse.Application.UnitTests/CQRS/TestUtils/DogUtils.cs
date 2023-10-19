using DogsHouse.Application.CQRS.Dogs.Commands;
using DogsHouse.Application.CQRS.Dogs.Queries;
using DogsHouse.Domain.Entities;

namespace DogsHouse.Application.UnitTests.CQRS.TestUtils
{
    public static class DogUtils
    {
        private static readonly Random _rand = new Random(Guid.NewGuid().GetHashCode());

        // Dog Queries
        public static GetDogsQuery CreateGetDogsQuery(
            bool? descending = null,
            int? attributeNumber = null,
            int? pageNumber = null,
            int? pageSize = null)
        {
            string attribute = "", order = "", pageN = "", pageS = "";
            if (descending != null) { order = (bool)descending ? "d" : "a"; }
            if (attributeNumber != null) 
            { 
                switch(attributeNumber)
                {
                    case 1: attribute = "color"; break;
                    case 2: attribute = "tail_length"; break;
                    case 3: attribute = "weight"; break;
                    default: attribute = "name"; break;
                }
            }
            if (pageNumber != null) { pageN = $"{pageNumber}"; }
            if (pageSize!= null) { pageS = $"{pageSize}"; }

            return new GetDogsQuery(new GetDogsQueryParams
            {
                Attribute = attribute,
                Order = order,
                PageNumber = pageN,
                PageSize = pageS,
            });
        }

        // Dog Commands
        public static PostDogCommand CreatePostDogCommand(
            string? name = null,
            string? color = null,
            int? tailLength = null,
            int? weight = null) =>
            new PostDogCommand(new Dog
            {
                name = name ?? Constants.Dog.name,
                color = color ?? Constants.Dog.color,
                tail_length = tailLength ?? Constants.Dog.tail_length,
                weight = weight ?? Constants.Dog.weight,
            });

        // Dog Model
        public static Dog CreateNamesake(string name) => new Dog
        {
            name = name,
            color = Constants.Dog.color.ToUpper(),
            tail_length = _rand.Next(1, 100),
            weight = _rand.Next(1, 100),
        };

        public static IEnumerable<Dog> CreateDogs(int quantity = 20)
        {
            var colors = new string[] { "amber", "black", "white", "red" };

            for(int i = 1; i <= quantity; i++)
                yield return new Dog 
                { 
                    name = $"Dog {i}", 
                    color = colors[i % 4], 
                    tail_length = i,
                    weight = quantity - i + 1,
                };
        }
    }
}
