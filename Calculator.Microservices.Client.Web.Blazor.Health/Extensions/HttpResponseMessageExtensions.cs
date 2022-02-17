using System.Text.Json;

namespace Calculator.Microservices.Client.Web.Blazor.Health.Extensions
{
    public static class HttpResponseMessageExtensions
    {
        public static async Task<TContent> As<TContent>(this HttpResponseMessage response)
        {
            if (response != null)
            {
                var body = await response.Content
                    .ReadAsStringAsync();

                if (body != null)
                {
                    var content = JsonSerializer.Deserialize<TContent>(body);

                    if (content != null)
                    {
                        return content;
                    }
                }
            }

            throw new InvalidOperationException($"Response is null or message can't be deserialized as {typeof(TContent).FullName}.");
        }
    }
}
