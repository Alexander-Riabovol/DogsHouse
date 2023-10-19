namespace DogsHouse.Domain.Entities
{
    /// <summary>
    /// A dog.
    /// </summary>
    public record Dog
    {
        public string name { get; set; } = null!;
        public string color { get; set; } = null!;
        public int tail_length { get; set; }
        public int weight { get; set; }
    }
}
