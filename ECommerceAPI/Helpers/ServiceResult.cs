namespace ECommerceAPI.Helpers
{
    public class ServiceResult<T>
    {
        public T? Data { get; set; }
        public bool IsSuccess { get; set; }
        public string Message { get; set; } = string.Empty;

        public static ServiceResult<T> Success(T data, string message = "Success")
        {
            return new ServiceResult<T>
            {
                Data = data,
                IsSuccess = true,
                Message = message
            };
        }

        public static ServiceResult<T> Failure(string message)
        {
            return new ServiceResult<T>
            {
                Data = default,
                IsSuccess = false,
                Message = message
            };
        }
    }
}