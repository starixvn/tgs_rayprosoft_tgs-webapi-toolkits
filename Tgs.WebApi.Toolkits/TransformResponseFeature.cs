using Microsoft.AspNetCore.Mvc;

namespace Tgs.WebApi.Toolkits;

internal class TransformResponseFeature
{
    public IActionResult ActionResult { get; }

    public TransformResponseFeature(IActionResult actionResult)
    {
        ActionResult = actionResult ?? throw new ArgumentNullException(nameof(actionResult));
    }
}
