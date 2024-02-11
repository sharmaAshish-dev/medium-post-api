namespace MediumPostApi.models
{
    public class ResponseModel<T>
    {
        public ResponseCode StatusCode { get; set; } = ResponseCode.BadRequest;
        public string Message { get; set; } = string.Empty;
        public T? Data { get; set; }
    }

    public enum ResponseCode
    {
        Success = 200,
        BadRequest = 400,
        Unauthorized = 401,
        Forbidden = 403,
        NotFound = 404,
        InternalServerError = 500
    }
}
