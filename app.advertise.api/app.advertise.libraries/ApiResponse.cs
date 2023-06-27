namespace app.advertise.libraries
{
    public class ApiResponse<T>: ApiResponse
    {
        public T Data { get; set; }
    }

    public class ApiResponse
    {
        public StatusCode Status { get; set; }
        public string ErrorMessage { get; set; }
        
        public bool IsSuccess { get {  return string.IsNullOrEmpty(ErrorMessage); } }
    }
}
