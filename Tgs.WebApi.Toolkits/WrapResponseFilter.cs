using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;
using Tgs.WebApi.Toolkits.ViewModels;

namespace Tgs.WebApi.Toolkits
{
    public class WrapResponseFilter : IAsyncActionFilter
    {
        public async Task OnActionExecutionAsync(ActionExecutingContext context, ActionExecutionDelegate next)
        {
            var executedContext = await next();

            if (!executedContext.ModelState.IsValid)
                return;

            switch (executedContext.Result)
            {
                case EmptyResult:
                    executedContext.Result = new ObjectResult(new SuccessViewModel<object?>());
                    break;

                case ObjectResult result when result.Value is IAsyncEnumerable<object> ae:
                    {
                        var adjustResult = new ObjectResult(new SuccessViewModel<IEnumerable<object>>
                        {
                            Data = ae.ToEnumerable()
                        });

                        adjustResult.StatusCode = result.StatusCode;
                        adjustResult.Formatters = result.Formatters;

                        executedContext.Result = adjustResult;
                    }
                    break;

                case ObjectResult result when result.Value is IAsyncEnumerable<int> ae:
                    {
                        var adjustResult = new ObjectResult(new SuccessViewModel<IEnumerable<int>>
                        {
                            Data = ae.ToEnumerable()
                        });

                        adjustResult.StatusCode = result.StatusCode;
                        adjustResult.Formatters = result.Formatters;

                        executedContext.Result = adjustResult;
                    }
                    break;

                case ObjectResult result when result.Value is IAsyncEnumerable<long> ae:
                    {
                        var adjustResult = new ObjectResult(new SuccessViewModel<IEnumerable<long>>
                        {
                            Data = ae.ToEnumerable()
                        });

                        adjustResult.StatusCode = result.StatusCode;
                        adjustResult.Formatters = result.Formatters;

                        executedContext.Result = adjustResult;
                    }
                    break;

                case ObjectResult result when result.Value is IAsyncEnumerable<float> ae:
                    {
                        var adjustResult = new ObjectResult(new SuccessViewModel<IEnumerable<float>>
                        {
                            Data = ae.ToEnumerable()
                        });

                        adjustResult.StatusCode = result.StatusCode;
                        adjustResult.Formatters = result.Formatters;

                        executedContext.Result = adjustResult;
                    }
                    break;

                case ObjectResult result when result.Value is IAsyncEnumerable<double> ae:
                    {
                        var adjustResult = new ObjectResult(new SuccessViewModel<IEnumerable<double>>
                        {
                            Data = ae.ToEnumerable()
                        });

                        adjustResult.StatusCode = result.StatusCode;
                        adjustResult.Formatters = result.Formatters;

                        executedContext.Result = adjustResult;
                    }
                    break;

                case ObjectResult result when result.Value is IAsyncEnumerable<decimal> ae:
                    {
                        var adjustResult = new ObjectResult(new SuccessViewModel<IEnumerable<decimal>>
                        {
                            Data = ae.ToEnumerable()
                        });

                        adjustResult.StatusCode = result.StatusCode;
                        adjustResult.Formatters = result.Formatters;

                        executedContext.Result = adjustResult;
                    }
                    break;

                case ObjectResult result when result.Value is IAsyncEnumerable<Guid> ae:
                    {
                        var adjustResult = new ObjectResult(new SuccessViewModel<IEnumerable<Guid>>
                        {
                            Data = ae.ToEnumerable()
                        });

                        adjustResult.StatusCode = result.StatusCode;
                        adjustResult.Formatters = result.Formatters;

                        executedContext.Result = adjustResult;
                    }
                    break;

                case ObjectResult result:
                    {
                        var adjustResult = new ObjectResult(new SuccessViewModel<object>
                        {
                            Data = result.Value!
                        });

                        adjustResult.StatusCode = result.StatusCode;
                        adjustResult.Formatters = result.Formatters;

                        executedContext.Result = adjustResult;
                    }
                    break;
            }
        }
    }
}
