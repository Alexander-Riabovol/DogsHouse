namespace DogsHouse.Domain.FlowControl
{
    /// <summary>
    /// Class used for flow control of the application.
    /// Contains information about Errors and HTTP Status Code
    /// that correspond to the type of the error.
    /// </summary>
    public record ServiceResponse
    {
        public bool IsError { get; init; } = true;
        public int StatusCode { get; init; } = 500;
        public string? ErrorMessage { get; init; }
        public ServiceResponse() { }
        public ServiceResponse(int statusCode, string errorMessage)
        {
            StatusCode = statusCode;
            ErrorMessage = errorMessage;
        }
        public static readonly ServiceResponse OK = new ServiceResponse()
        {
            IsError = false,
            StatusCode = 200,
            ErrorMessage = ""
        };
    }
    /// <summary>
    /// Generic version of class used for flow control of the application.
    /// If there is no error contains a response of type <see cref="T"/>.
    /// </summary>
    public record ServiceResponse<T> : ServiceResponse
    {
        public T? Content { get; init; }
        public ServiceResponse() { }
        public ServiceResponse(int statusCode, string errorMessage) : base(statusCode, errorMessage) { }
        public ServiceResponse(int statusCode, string errorMessage, T content) 
            : this(statusCode, errorMessage)
        {
            Content = content;
        }
        public new static ServiceResponse<T> OK(T? content)
        {
            return new ServiceResponse<T>()
            {
                IsError = false,
                StatusCode = 200,
                ErrorMessage = "",
                Content = content
            };
        }
    }
}
