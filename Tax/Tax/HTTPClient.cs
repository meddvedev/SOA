using System.Text;
using System.Text.Json;
using System.Net.Http;
using System.Net;

using Tax.Models.User;

namespace HTTPClient;

public class HTTPClient
{
    private readonly HttpClient client;
    private readonly JsonSerializerOptions serializerOptions;

    public HTTPClient()
    {
        client = new HttpClient();
        serializerOptions = new JsonSerializerOptions
        {
            PropertyNamingPolicy = JsonNamingPolicy.CamelCase,
            WriteIndented = true
        };
    }

    public async Task<User> PostAsync(string url, string body)
    {
        var content = new StringContent(body, Encoding.UTF8, "application/json");
        var res = await client.PostAsync(url, content);

        string resultContent = await res.Content.ReadAsStringAsync();

        return JsonSerializer.Deserialize<User>(resultContent);
    }
}