namespace DogsHouse.Contracts.Dogs
{
    public record GetDogsResponse(IEnumerable<GetDogResponse> Dogs);
}
