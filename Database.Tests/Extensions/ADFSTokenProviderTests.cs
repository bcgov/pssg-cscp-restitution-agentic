using System.Net;
using System.Net.Http;
using System.Text;
using Database.Extensions;
using Microsoft.Extensions.Caching.Memory;
using Microsoft.Extensions.Logging.Abstractions;
using Microsoft.Extensions.Options;
using MemoryCache = Database.Extensions.MemoryCache;

namespace Database.Tests.Extensions;

public class ADFSTokenProviderTests
{
    [Fact]
    public async Task AcquireToken_UsesClientCredentialsWithoutPasswordFields()
    {
        var handler = new StubHttpMessageHandler();
        var options = Options.Create(
            new DynamicsTokenProviderOptions
            {
                ADFS = new ADFSTokenProviderOptions
                {
                    OAuth2TokenEndpoint = "https://adfs.example.test/oauth2/token",
                    ClientId = "test-client-id",
                    ClientSecret = "test-client-secret",
                    ResourceName = "https://dataverse.example.test",
                    ServiceAccountName = "unused-username",
                    ServiceAccountPassword = "unused-password",
                },
            }
        );
        using var innerCache = new Microsoft.Extensions.Caching.Memory.MemoryCache(new MemoryCacheOptions());
        var provider = new ADFSTokenProvider(
            new StubHttpClientFactory(handler),
            options,
            new MemoryCache(innerCache),
            NullLogger<ADFSTokenProvider>.Instance
        );

        var token = await provider.AcquireToken();

        Assert.Equal("access-token", token);
        Assert.Equal("client_credentials", handler.Form["grant_type"]);
        Assert.Equal("Basic", handler.AuthorizationScheme);
        Assert.Equal("test-client-id:test-client-secret", handler.AuthorizationParameter);
        Assert.Equal("https://dataverse.example.test", handler.Form["resource"]);
        Assert.DoesNotContain("username", handler.Form.Keys);
        Assert.DoesNotContain("password", handler.Form.Keys);
    }

    private sealed class StubHttpClientFactory(HttpMessageHandler handler) : IHttpClientFactory
    {
        public HttpClient CreateClient(string name) => new(handler);
    }

    private sealed class StubHttpMessageHandler : HttpMessageHandler
    {
        public Dictionary<string, string> Form { get; } = [];
        public string? AuthorizationScheme { get; private set; }
        public string? AuthorizationParameter { get; private set; }

        protected override async Task<HttpResponseMessage> SendAsync(
            HttpRequestMessage request,
            CancellationToken cancellationToken
        )
        {
            Assert.Equal(HttpMethod.Post, request.Method);
            Assert.Equal("https://adfs.example.test/oauth2/token", request.RequestUri!.ToString());
            AuthorizationScheme = request.Headers.Authorization?.Scheme;
            AuthorizationParameter = request.Headers.Authorization?.Parameter is { } parameter
                ? Encoding.UTF8.GetString(Convert.FromBase64String(parameter))
                : null;

            var form = await request.Content!.ReadAsStringAsync(cancellationToken);
            foreach (var entry in form.Split('&'))
            {
                var pair = entry.Split('=', 2);
                Form[Uri.UnescapeDataString(pair[0])] = Uri.UnescapeDataString(pair[1].Replace("+", " "));
            }

            return new HttpResponseMessage(HttpStatusCode.OK)
            {
                Content = new StringContent("""{"access_token":"access-token","token_type":"Bearer"}"""),
            };
        }
    }
}
