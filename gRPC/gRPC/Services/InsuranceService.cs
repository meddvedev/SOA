using Grpc.Core;
using gRPC.DBContext;
using gRPC.Models;
using Microsoft.EntityFrameworkCore;

namespace gRPC.Services;

public class InsuranceService : Insurance.InsuranceBase
{
    private readonly ILogger<InsuranceService> _logger;
    public InsuranceService(ILogger<InsuranceService> logger)
    {
        _logger = logger;
    }

    private readonly DBContext.DBContext db = new DBContext.DBContext();

    public override Task<InsuranceListReply> InsuranceList(InsuranceListRequest request, ServerCallContext context)
    {
        InsuranceListReply reply = new InsuranceListReply();

        var insurances = db.insurance.FromSql($"select id, title, category, insurance_amount from insurance;").ToList();

        foreach (var insurance in insurances)
        {
            var temp = new InsuranceItem
            {
                Id = insurance.id.ToString(),
                Title = insurance.title,
                Category = insurance.category,
                InsuranceAmount = insurance.insurance_amount
            };
            Console.WriteLine(temp);
            reply.Insurances.Add(temp);
        }

        return Task.FromResult(reply);
    }
}

