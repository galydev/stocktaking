namespace Inventory.Application.Wrappers
{
    public class ApiResponse<T>
    {
        public bool Succeeded { get; set; }
        public int Status { get; set; }
        public string Message { get; set; }
        public T Data { get; set; }

        public ApiResponse(T data, string message = null, bool succeeded = true)
        {
            Succeeded = succeeded;
            Status = 200;
            Message = message;
            Data = data;
        }
    }
}