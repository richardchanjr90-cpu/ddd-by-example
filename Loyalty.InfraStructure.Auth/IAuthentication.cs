using System.Threading.Tasks;
using Refit;

namespace Loyalty.InfraStructure.Auth
{
    public interface IAuthentication
    {
        [Get("/.auth/me")]
        Task<AuthenticationModel[]> GetCurrentAuthentication();
    }
}
