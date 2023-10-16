using TesteBanco.Models;
using System.Threading.Tasks;

namespace TesteBanco.Interface
{
    public interface IBanco
    {
        Task<Banco> GetBancoByIdAsync(int id);
        Task<List<Banco>> GetAllBancosAsync();
    }
}
