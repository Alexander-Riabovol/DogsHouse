namespace DogsHouse.Contracts.Dogs
{
    /// <summary>
    /// Schema of the body of HTTP POST request to the /dog endpoint
    /// </summary>
    public record PostDogRequest(
        string name,
        string color,
        int tail_length,
        int weight);
}
