namespace app.advertise.libraries.Interfaces
{
    public interface IInternalExceptionHandler
    {
        ApiResponse HandleException(Exception ex);
    }
}
