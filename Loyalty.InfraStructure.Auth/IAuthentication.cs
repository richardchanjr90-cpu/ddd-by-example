using System.Threading.Tasks;

namespace Loyalty.InfraStructure.Auth
{
    public interface IAuthentication
    {
        Task<AuthenticationModel[]> GetCurrentAuthentication();
    }
}
