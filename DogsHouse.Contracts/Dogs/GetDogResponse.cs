namespace DogsHouse.Contracts.Dogs
{
    public record GetDogResponse(
        string name,
        string color,
        int tail_length,
        int weight);
}