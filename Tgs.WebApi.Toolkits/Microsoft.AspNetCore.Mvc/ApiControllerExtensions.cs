using System.Net;

namespace Microsoft.AspNetCore.Mvc;

public static class ApiControllerExtensions
{
    public static TResult Fail<TResult, TFailResult>(
        this ControllerBase controller,
        HttpStatusCode statusCode,
        TFailResult failResult)
        => Fail<TResult, TFailResult>(
            controller,
            (int)statusCode,
            failResult);

    public static TResult Fail<TResult, TFailResult>(
        this ControllerBase controller,
        int statusCode,
        TFailResult failResult)
    {
        Fail(controller, statusCode, failResult);

        return default!;
    }

    public static void Fail<TFailResult>(
        this ControllerBase controller,
        HttpStatusCode statusCode,
        TFailResult failResult)
        => Fail(
            controller,
            (int)statusCode,
            failResult);

    public static void Fail<TFailResult>(
        this ControllerBase controller,
        int statusCode,
        TFailResult result)
        => controller.HttpContext.TransformResult(
            controller.StatusCode(statusCode, result));

    public static Task<TResult> FailAsync<TResult, TFailResult>(
        this ControllerBase controller,
        HttpStatusCode statusCode,
        TFailResult failResult)
        => FailAsync<TResult, TFailResult>(
            controller,
            (int)statusCode,
            failResult);

    public static Task<TResult> FailAsync<TResult, TFailResult>(
        this ControllerBase controller,
        int statusCode,
        TFailResult failResult)
        => Task.FromResult(Fail<TResult, TFailResult>(
            controller,
            statusCode,
            failResult));

    public static Task FailAsync<TFailResult>(
        this ControllerBase controller,
        HttpStatusCode statusCode,
        TFailResult failResult)
        => FailAsync(
            controller,
            (int)statusCode,
            failResult);

    public static Task FailAsync<TFailResult>(
        this ControllerBase controller,
        int statusCode,
        TFailResult failResult)
    {
        Fail(controller, statusCode, failResult);
        return Task.CompletedTask;
    }
}
