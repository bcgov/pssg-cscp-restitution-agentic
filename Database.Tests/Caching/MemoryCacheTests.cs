using Database.Extensions;
using Microsoft.Extensions.Caching.Memory;
using MemoryCache = Database.Extensions.MemoryCache;

namespace Database.Tests.Caching
{
    public class MemoryCacheTests
    {
        private const string CacheKey = "test_token";

        private static readonly TimeSpan Expiration = TimeSpan.FromMinutes(5);

        private static (ICache Cache, IMemoryCache Inner) CreateCache()
        {
            var inner = new Microsoft.Extensions.Caching.Memory.MemoryCache(new MemoryCacheOptions());

            return (new MemoryCache(inner), inner);
        }

        [Fact]
        public async Task GetOrSet_OnMiss_InvokesFactoryAndReturnsValue()
        {
            var (cache, _) = CreateCache();
            var calls = 0;

            var value = await cache.GetOrSet(
                CacheKey,
                () =>
                {
                    calls++;
                    return Task.FromResult("token-1");
                },
                Expiration
            );

            Assert.Equal("token-1", value);
            Assert.Equal(1, calls);
        }

        [Fact]
        public async Task GetOrSet_OnHit_ReturnsCachedValueWithoutInvokingFactory()
        {
            var (cache, _) = CreateCache();
            var calls = 0;

            Task<string> Factory()
            {
                calls++;
                return Task.FromResult($"token-{calls}");
            }

            var first = await cache.GetOrSet(CacheKey, Factory, Expiration);
            var second = await cache.GetOrSet(CacheKey, Factory, Expiration);

            Assert.Equal("token-1", first);
            Assert.Equal("token-1", second);
            Assert.Equal(1, calls);
        }

        [Fact]
        public async Task GetOrSet_WithDifferentKeys_CachesValuesSeparately()
        {
            var (cache, _) = CreateCache();

            var adfs = await cache.GetOrSet("adfs_token", () => Task.FromResult("adfs"), Expiration);
            var entraId = await cache.GetOrSet("entraid_token", () => Task.FromResult("entraid"), Expiration);

            Assert.Equal("adfs", adfs);
            Assert.Equal("entraid", entraId);
        }

        [Fact]
        public async Task GetOrSet_AfterEviction_InvokesFactoryAgain()
        {
            var (cache, inner) = CreateCache();
            var calls = 0;

            Task<string> Factory()
            {
                calls++;
                return Task.FromResult($"token-{calls}");
            }

            var first = await cache.GetOrSet(CacheKey, Factory, Expiration);
            inner.Remove(CacheKey);
            var second = await cache.GetOrSet(CacheKey, Factory, Expiration);

            Assert.Equal("token-1", first);
            Assert.Equal("token-2", second);
            Assert.Equal(2, calls);
        }
    }
}
