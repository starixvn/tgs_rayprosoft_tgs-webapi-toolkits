using Tgs.WebApi.Toolkits;

namespace Microsoft.AspNetCore.Mvc
{
    public static class MvcOptionsExtensions
    {
        public static void AddWebApiToolkitFilters(this MvcOptions options)
        {
            options.Filters.Add<RetouchResponseFilter>();
        }

        public static void AddWrapResponseFilters(this MvcOptions options)
        {
            options.Filters.Add<WrapResponseFilter>();
        }
    }
}
