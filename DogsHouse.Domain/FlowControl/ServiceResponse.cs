namespace DogsHouse.Domain.FlowControl
{
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
