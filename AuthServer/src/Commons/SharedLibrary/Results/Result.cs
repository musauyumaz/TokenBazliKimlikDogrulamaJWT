using System.Net;
namespace SharedLibrary.Results;

public readonly record struct Result<T>
{
    public bool IsSucceed { get; init; }
    public T? Data { get; init; }
    public string? ErrorMessage { get; init; }
    public HttpStatusCode StatusCode { get; init; }
    public bool IsShow { get; init; }

    private Result(bool isSucceed, T? data, string? errorMessage, HttpStatusCode statusCode, bool isShow)
    {
        IsSucceed = isSucceed;
        Data = data;
        ErrorMessage = errorMessage;
        StatusCode = statusCode;
        IsShow = isShow;
    }

    public static Result<T> Success(T data, HttpStatusCode statusCode = HttpStatusCode.OK, bool isShow = true)
        => new(true, data, null, statusCode, isShow);

    public static Result<T> Fail(
        string errorMessage,
        HttpStatusCode statusCode = HttpStatusCode.BadRequest,
        bool isShow = true)
        => new(false, default, errorMessage, statusCode, isShow);

    public override string ToString()
        => IsSucceed
            ? $"✅ Success ({typeof(T).Name})"
            : $"❌ Fail ({statusCodeText(StatusCode)}): {ErrorMessage}";

    private static string statusCodeText(HttpStatusCode code)
        => $"{(int)code} {code}";
}

public readonly record struct Result
{
    public bool IsSucceed { get; init; }
    public string? ErrorMessage { get; init; }
    public HttpStatusCode StatusCode { get; init; }
    public bool IsShow { get; init; }

    private Result(bool isSucceed, string? errorMessage, HttpStatusCode statusCode, bool isShow)
    {
        IsSucceed = isSucceed;
        ErrorMessage = errorMessage;
        StatusCode = statusCode;
        IsShow = isShow;
    }

    public static Result Success(HttpStatusCode statusCode = HttpStatusCode.OK, bool isShow = true)
        => new(true, null, statusCode, isShow);

    public static Result Fail(
        string errorMessage,
        HttpStatusCode statusCode = HttpStatusCode.BadRequest,
        bool isShow = true)
        => new(false, errorMessage, statusCode, isShow);

    public override string ToString()
        => IsSucceed
            ? $"✅ Success ({(int)StatusCode} {StatusCode})"
            : $"❌ Fail ({(int)StatusCode} {StatusCode}): {ErrorMessage}";
}