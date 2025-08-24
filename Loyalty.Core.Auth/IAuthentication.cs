using System.Threading.Tasks;
using Refit;

namespace Loyalty.Core.Auth
{
    public interface IAuthentication
    {
        [Get("/.auth/me")]
        Task<AuthenticationModel[]> GetCurrentAuthentication();
    }
}
