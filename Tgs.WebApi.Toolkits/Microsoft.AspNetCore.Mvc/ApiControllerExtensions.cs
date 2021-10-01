using System.Net;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http.Features;
using Tgs.WebApi.Toolkits;

namespace Microsoft.AspNetCore.Mvc
{
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
        {
            var responseFeature = controller.HttpContext.Features.Get<IHttpResponseFeature>();

            responseFeature.StatusCode = statusCode;

            var failResponseFeature = new FailResponseFeature<TFailResult>((int)statusCode, result);

            controller.HttpContext.Features.Set<IFailResponseFeature>(failResponseFeature);
        }

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
}
