namespace CardStorageService.Controllers.Models.Requests
{
    public interface IOperationResult
    {
        int ErrorCode { get; }
        string? ErrorMessage { get; }
    }
}
