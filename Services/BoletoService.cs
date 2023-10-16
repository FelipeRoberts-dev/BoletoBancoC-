using TesteBanco.Interface;

namespace TesteBanco.Services
{
    public class BoletoService
    {
        private readonly IBanco _bancoRepository;
        private readonly IBoletoRepository _boletoRepository;

        public BoletoService(IBanco bancoRepository, IBoletoRepository boletoRepository)
        {
            _bancoRepository = bancoRepository;
            _boletoRepository = boletoRepository;
        }

        public async Task<decimal> CalcularValorComJurosAsync(int boletoId, DateTime dataAtual)
        {
            var boleto = await _boletoRepository.GetBoletoByIdAsync(boletoId);
            if (boleto == null)
            {
                throw new Exception("Boleto não encontrado"); // Adicione tratamento de erro apropriado
            }

            var banco = await _bancoRepository.GetBancoByIdAsync(boleto.BancoId);
            if (banco == null)
            {
                throw new Exception("Banco não encontrado"); // Adicione tratamento de erro apropriado
            }

            if (boleto.DataVencimento < dataAtual)
            {
                // Cálculo do valor com juros
                decimal valorComJuros = boleto.Valor * (1 + banco.PercentualJuros / 100);
                return valorComJuros;
            }

            return boleto.Valor; // Sem juros
        }
    }
}
