namespace DogsHouse.Contracts.Dogs
{
    public record GetDogResponse
    {
        public string name { get; init; } = null!;
        public string color { get; init; } = null!;
        public int tail_length { get; init; }
        public int weight { get; init; }
    }       
}