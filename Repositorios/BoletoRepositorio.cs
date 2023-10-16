using TesteBanco.Data;
using TesteBanco.Interface;
using TesteBanco.Models;
using TesteBanco.Services;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using TesteBanco.Models.Filtros;

namespace TesteBanco.Repositorios
{
    public class BoletoRepositorio : IBoletoRepository
    {
        private readonly ConexaoDbContext _conexao;
        private readonly BoletoService _boletoService;
        private readonly CamposObrigatoriosValidator _camposObrigatoriosValidator;

        public BoletoRepositorio(ConexaoDbContext conexao, BoletoService boletoService, CamposObrigatoriosValidator camposObrigatoriosValidator)
        {
            _conexao = conexao;
            _boletoService = boletoService;
            _camposObrigatoriosValidator = camposObrigatoriosValidator;
        }

        public async Task<Boleto> GetBoletoByIdAsync(int id)
        {
            var filtro = new BoletoFltro { Id = id };

            var boleto = await _conexao.Boletos.FindAsync(filtro);

            if (boleto != null && boleto.DataVencimento < DateTime.Now)
            {
                boleto.Valor = await _boletoService.CalcularValorComJurosAsync(boleto.Id, DateTime.Now);
            }

            return boleto;
        }

        public async Task<List<Boleto>> GetAllBoletosAsync()
        {
            return await _conexao.Boletos.ToListAsync();
        }

        public async Task InserirBoletoAsync(Boleto boleto)
        {
            // Valide os campos obrigatórios usando o CamposObrigatoriosValidator
            _camposObrigatoriosValidator.ValidarCamposObrigatorios(boleto);

            _conexao.Boletos.Add(boleto);
            await _conexao.SaveChangesAsync();
        }
    }
}
