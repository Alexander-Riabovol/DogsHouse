namespace DogsHouse.Application.CQRS.Dogs.Queries
{
    public record GetDogsQueryParams
    {
        public string PageNumber { get; init; } = string.Empty;
        public string PageSize { get; init; } = string.Empty;
        public string Attribute { get; init; } = string.Empty;
        public string Order { get; init; } = string.Empty;
    }
}
