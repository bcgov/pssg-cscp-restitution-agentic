using System.Threading.Tasks;

namespace Database.Extensions
{
    public interface ITokenProvider
    {
        Task<string> AcquireToken();
    }
}
