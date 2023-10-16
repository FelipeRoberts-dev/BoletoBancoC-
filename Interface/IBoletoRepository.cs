using TesteBanco.Models;

namespace TesteBanco.Interface
{
    public interface IBoletoRepository
    {
        Task<Boleto> GetBoletoByIdAsync(int id);
        Task<List<Boleto>> GetAllBoletosAsync();
        Task InserirBoletoAsync(Boleto boleto);
    }
}
