namespace DogsHouse.Contracts.Dogs
{
    /// <summary>
    /// Represents the response schema for individual dog data returned from an HTTP GET request.
    /// </summary>
    public record GetDogResponse
    {
        public string name { get; init; } = null!;
        public string color { get; init; } = null!;
        public int tail_length { get; init; }
        public int weight { get; init; }
    }
}