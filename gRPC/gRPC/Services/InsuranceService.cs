using Grpc.Core;
using gRPC;

namespace gRPC.Services;

public class InsuranceService : Insurance.InsuranceBase
{
    private readonly ILogger<InsuranceService> _logger;
    public InsuranceService(ILogger<InsuranceService> logger)
    {
        _logger = logger;
    }

    public override Task<InsuranceListReply> InsuranceList(InsuranceListRequest request, ServerCallContext context)
    {
        return Task.FromResult(new InsuranceListReply
        {
            Id = "id",
            Title = "title",
            Category = "category",
            InsuranceAmount = 10000
        });
    }
}

