using Database.Extensions;

namespace Database.Tests.Extensions
{
    public class DynamicsTokenProviderOptionsTests
    {
        private const string OnPremiseUrl = "https://example.test/onpremise";
        private const string CloudUrl = "https://example.test/cloud";

        private static DynamicsTokenProviderOptions CreateOptions(DynamicsAuthenticationType authenticationType) =>
            new()
            {
                AuthenticationType = authenticationType,
                ADFS = new ADFSTokenProviderOptions { DynamicsApiEndpointUrl = OnPremiseUrl },
                EntraId = new EntraIdTokenProviderOptions { DynamicsApiEndpointUrl = CloudUrl },
            };

        [Theory]
        [InlineData(DynamicsAuthenticationType.OnPremise, OnPremiseUrl)]
        [InlineData(DynamicsAuthenticationType.Cloud, CloudUrl)]
        public void GetDynamicsApiEndpointUrl_ReturnsUrlForAuthenticationType(
            DynamicsAuthenticationType authenticationType,
            string expected
        )
        {
            Assert.Equal(expected, CreateOptions(authenticationType).GetDynamicsApiEndpointUrl());
        }

        [Theory]
        [InlineData(DynamicsAuthenticationType.OnPremise)]
        [InlineData(DynamicsAuthenticationType.Cloud)]
        public void GetDynamicsApiEndpointUrl_WithNoConfiguredUrl_ReturnsEmptyString(
            DynamicsAuthenticationType authenticationType
        )
        {
            var options = new DynamicsTokenProviderOptions { AuthenticationType = authenticationType };

            Assert.Equal(string.Empty, options.GetDynamicsApiEndpointUrl());
        }

        [Fact]
        public void Defaults_UseOnPremiseAuthenticationAndEmptyEndpoints()
        {
            var options = new DynamicsTokenProviderOptions();

            Assert.Equal(DynamicsAuthenticationType.OnPremise, options.AuthenticationType);
            Assert.NotNull(options.ADFS);
            Assert.NotNull(options.EntraId);
            Assert.Equal(string.Empty, options.GetDynamicsApiEndpointUrl());
        }
    }
}
