using Microsoft.EntityFrameworkCore;
using TesteBanco.Data;
using TesteBanco.Interface;
using TesteBanco.Models.Filtros;
using TesteBanco.Models;

namespace TesteBanco.Repositorios
{
    public class BancoRepositorio : IBanco
    {
        private readonly ConexaoDbContext _conexao;

        public BancoRepositorio(ConexaoDbContext conexao)
        {
            _conexao = conexao;
        }


        public async Task<Banco> GetBancoByIdAsync(int id)
        {
            var filtro = new BancoFiltro { Id = id };
            var bancos = await GetBancosAsync(filtro);

            return bancos.FirstOrDefault();
        }

        public async Task<List<Banco>> GetBancosAsync(BancoFiltro filtro)
        {
            var query = _conexao.Bancos.AsQueryable();

            if (filtro.Id.HasValue)
            {
                query = query.Where(banco => banco.Id == filtro.Id);
            }

            return await query.ToListAsync();
        }

        public async Task<List<Banco>> GetAllBancosAsync()
        {
            return await _conexao.Bancos.ToListAsync();
        }


    }
}
