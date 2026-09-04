using System.ComponentModel.DataAnnotations;
using Gov.Cscp.VictimServices.Public.Models;

namespace Gov.Cscp.VictimServices.Public.Tests;

public class SubmissionDtoMaxLengthTests
{
    private static bool TryValidate(object instance, out List<ValidationResult> results)
    {
        results = new List<ValidationResult>();
        var context = new ValidationContext(instance);
        return Validator.TryValidateObject(instance, context, results, validateAllProperties: true);
    }

    [Fact]
    public void ParticipantDto_OverlongFirstName_FailsValidation()
    {
        var dto = new ParticipantDto { FirstName = new string('a', 101) };

        var ok = TryValidate(dto, out var results);

        Assert.False(ok);
        Assert.Contains(results, r => r.MemberNames.Contains(nameof(ParticipantDto.FirstName)));
    }

    [Fact]
    public void DocumentDto_OverlongFilename_FailsValidation()
    {
        var dto = new DocumentDto { Filename = new string('f', 256) };

        var ok = TryValidate(dto, out var results);

        Assert.False(ok);
        Assert.Contains(results, r => r.MemberNames.Contains(nameof(DocumentDto.Filename)));
    }

    [Fact]
    public void RestitutionApplicationDtoBase_OverlongEmail_FailsValidation()
    {
        var dto = new VictimApplicationDto
        {
            FirstName = "Jane",
            LastName = "Doe",
            BirthDate = new DateTime(1990, 1, 1),
            PrimaryAddressLine1 = "1 Main",
            PrimaryCity = "Victoria",
            PrimaryProvince = "BC",
            PrimaryPostalCode = "V8V1V1",
            PrimaryCountry = "Canada",
            Signature = "sig",
            Email = new string('e', 255),
        };

        var ok = TryValidate(dto, out var results);

        Assert.False(ok);
        Assert.Contains(results, r => r.MemberNames.Contains(nameof(RestitutionApplicationDtoBase.Email)));
    }

    [Fact]
    public void ParticipantDto_WithinLimits_PassesValidation()
    {
        var dto = new ParticipantDto
        {
            FirstName = "Jane",
            LastName = "Doe",
            Email = "jane@example.com",
            PostalCode = "V8V1V1",
        };

        var ok = TryValidate(dto, out _);

        Assert.True(ok);
    }
}
