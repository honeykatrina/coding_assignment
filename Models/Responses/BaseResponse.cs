namespace UserAccountManagement.Models.Responses;

public class BaseResponse<T> : GeneralResponse
{
    public T Model { get; set; }

    public BaseResponse(
        T model,
        bool success,
        Error error) : base(success, error)
    {
        Model = model;
    }
}