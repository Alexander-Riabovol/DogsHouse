namespace DogsHouse.Application.CQRS.Dogs.Queries
{
    public record GetDogsQueryParams
    {
        public string PageNumber { get; init; } = null!;
        public string PageSize { get; init; } = null!;
        public string Attribute { get; init; } = null!;
        public string Order { get; init; } = null!;
    }
}
