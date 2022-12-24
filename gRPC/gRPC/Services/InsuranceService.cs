using Grpc.Core;
using gRPC.DBContext;
using gRPC.Models;
using Microsoft.EntityFrameworkCore;
using System.Text;

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
        var decryptedToken = DecryptJWTToket(request.Token);

        if (decryptedToken.Length == 0)
        {
            throw new Exception("unothorizued");
        }

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

    public override Task<InsuranceItem> AddInsurance(AddInsuranceRequest request, ServerCallContext context)
    {
        var decryptedToken = DecryptJWTToket(request.Token);

        if (decryptedToken.Length == 0)
        {
            throw new Exception("unothorizued");
        }

        var newInsurance = new gRPC.Models.Insurance(request.Title, request.Category, request.InsuranceAmount);

        var insuranceReply = db.insurance.Add(newInsurance);

        var temp = new InsuranceItem
        {
            Id = insuranceReply.id.ToString(),
            Title = insuranceReply.title,
            Category = insuranceReply.category,
            InsuranceAmount = insuranceReply.insurance_amount
        };

        return temp;
    }

    private string DecryptJWTToket(string token)
    {
        var handler = new JwtSecurityTokenHandler();
        var validations = new TokenValidationParameters
        {
            ValidateIssuerSigningKey = true,
            IssuerSigningKey = new SymmetricSecurityKey(Configuration["salt"]),
            ValidateIssuer = false,
            ValidateAudience = false
        };
        var claims = handler.ValidateToken(token, validations, out var tokenSecure);
        return claims.Identity.Name;
    }
}

