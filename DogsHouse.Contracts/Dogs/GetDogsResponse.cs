namespace DogsHouse.Contracts.Dogs
{
    /// <summary>
    /// Schema of the body of HTTP GET request to the /dogs endpoint
    /// </summary>
    public record GetDogsResponse(IEnumerable<GetDogResponse> Dogs);
}
