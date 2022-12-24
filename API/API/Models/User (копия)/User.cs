using System.Text.Json.Serialization;

namespace API.Models.User;

public class User
{
    public Guid id { get; set; }

    public string email { get; set; } = string.Empty;

    [JsonIgnore]
    public string passwordhash { get; set; } = string.Empty;

    public string username { get; set; } = string.Empty;
}

