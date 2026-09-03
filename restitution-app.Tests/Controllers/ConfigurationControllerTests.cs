using Gov.Cscp.VictimServices.Public.Controllers;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Logging.Abstractions;

namespace Gov.Cscp.VictimServices.Public.Tests.Controllers
{
    public class ConfigurationControllerTests
    {
        private static ConfigurationController CreateController(Dictionary<string, string?> settings)
        {
            IConfiguration configuration = new ConfigurationBuilder().AddInMemoryCollection(settings).Build();

            return new ConfigurationController(NullLogger<ConfigurationController>.Instance, configuration);
        }

        private static Configuration GetConfigurationResult(Dictionary<string, string?> settings)
        {
            var result = Assert.IsType<OkObjectResult>(CreateController(settings).GetConfiguration());

            return Assert.IsType<Configuration>(result.Value);
        }

        [Fact]
        public void GetConfiguration_ReturnsOutageInformationFromConfiguration()
        {
            var config = GetConfigurationResult(
                new Dictionary<string, string?>
                {
                    ["CONFIGURATION_OUTAGEINFORMATION_MESSAGE"] = "Scheduled maintenance",
                    ["CONFIGURATION_OUTAGEINFORMATION_STARTDATE"] = "2026-01-01T00:00:00Z",
                    ["CONFIGURATION_OUTAGEINFORMATION_ENDDATE"] = "2026-01-02T00:00:00Z",
                }
            );

            Assert.Equal("Scheduled maintenance", config.OutageMessage);
            Assert.Equal("2026-01-01T00:00:00Z", config.OutageStartDate);
            Assert.Equal("2026-01-02T00:00:00Z", config.OutageEndDate);
        }

        [Fact]
        public void GetConfiguration_WithNoSettings_ReturnsDefaults()
        {
            var config = GetConfigurationResult(new Dictionary<string, string?>());

            Assert.Null(config.OutageMessage);
            Assert.Null(config.OutageStartDate);
            Assert.Null(config.OutageEndDate);
            Assert.False(config.MaintenanceMode);
            Assert.NotNull(config.FeatureFlags);
            Assert.False(config.FeatureFlags.UseUpdatedComplianceFields);
        }

        [Theory]
        [InlineData("true", true)]
        [InlineData("True", true)]
        [InlineData("false", false)]
        [InlineData("", false)]
        [InlineData("not-a-boolean", false)]
        public void GetConfiguration_MapsMaintenanceMode(string value, bool expected)
        {
            var config = GetConfigurationResult(
                new Dictionary<string, string?> { ["CONFIGURATION_MAINTENANCE_MODE"] = value }
            );

            Assert.Equal(expected, config.MaintenanceMode);
        }

        [Theory]
        [InlineData("true", true)]
        [InlineData("anything", true)]
        [InlineData("", false)]
        public void GetConfiguration_MapsUpdatedComplianceFieldsFeatureFlag(string value, bool expected)
        {
            var config = GetConfigurationResult(
                new Dictionary<string, string?> { ["FEATURE_UPDATED_COMPLIANCE_FIELDS"] = value }
            );

            Assert.Equal(expected, config.FeatureFlags.UseUpdatedComplianceFields);
        }
    }
}
