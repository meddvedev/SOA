namespace gRPC.Models;

public class Insurance
{
    public Guid id { get; set; }

    public string title { get; set; } = string.Empty;

    public string category { get; set; } = string.Empty;

    public Int32 insurance_amount { get; set; }
}

