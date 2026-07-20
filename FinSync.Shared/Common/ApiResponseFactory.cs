namespace FinSync.Shared.Common
{
    public static class ApiResponseFactory
    {
        public static ApiResponse<T> Success<T>(T data, string message)
        {
            return new ApiResponse<T>
            {
                Success = true,
                Message = message,
                Data = data,
                Errors = null
            };
        }

        public static ApiResponse<T> Created<T>(T data, string message)
        {
            return new ApiResponse<T>
            {
                Success = true,
                Message = message,
                Data = data,
                Errors = null
            };
        }

        public static ApiResponse<T> Failure<T>(string message, IEnumerable<string>? errors = null)
        {
            return new ApiResponse<T>
            {
                Success = false,
                Message = message,
                Data = default,
                Errors = errors
            };
        }

        public static ApiResponse<T> NotFound<T>(string message)
        {
            return new ApiResponse<T>
            {
                Success = false,
                Message = message,
                Data = default,
                Errors = null
            };
        }
    }
}