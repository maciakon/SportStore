using Microsoft.AspNetCore.Http;

namespace SportStore.Infrastructure
{
    public static class UrlExtenstions
    {
        public static string PathAndQuery(this HttpRequest request)
        {
            return request.QueryString.HasValue ? $"{request.Path}{request.QueryString}" : request.Path.ToString();
        }
    }
}