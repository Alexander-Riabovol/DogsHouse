namespace DogsHouse.Application.CQRS.Dogs.Queries
{
    /// <summary>
    /// <see cref="GetDogsQuery"/>'s params mapped from those provided in URL.
    /// </summary>
    public record GetDogsQueryParams
    {
        /// <summary>
        /// Number of the Page.
        /// </summary>
        public string PageNumber { get; init; } = string.Empty;
        /// <summary>
        /// Size of the Page.
        /// </summary>
        public string PageSize { get; init; } = string.Empty;
        /// <summary>
        /// Contains name of the property according to which the sequence need to be sorted.
        /// </summary>
        public string Attribute { get; init; } = string.Empty;
        /// <summary>
        /// Ascending or Descending order according to which the sequence need to be sorted.
        /// </summary>
        public string Order { get; init; } = string.Empty;
    }
}
