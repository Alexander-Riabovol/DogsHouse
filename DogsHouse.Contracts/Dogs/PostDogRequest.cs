namespace DogsHouse.Contracts.Dogs
{
    public record PostDogRequest(
        string name,
        string color,
        int tail_length,
        int weight);
}
