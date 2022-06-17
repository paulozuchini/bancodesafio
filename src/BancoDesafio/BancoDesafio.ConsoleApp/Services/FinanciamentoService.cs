using BancoDesafio.ConsoleApp.Models;
using BancoDesafio.ConsoleApp.Services.Interfaces;

namespace BancoDesafio.ConsoleApp.Services
{
    public class FinanciamentoService : IFinanciamentoService
    {
        private BANCODESAFIOContext _context;

        public FinanciamentoService(BANCODESAFIOContext context)
        {
            _context = context;
        }

        public IEnumerable<Financiamento> GetAll()
        {
            return _context.Financiamentos;
        }

        public Financiamento GetById(Guid id)
        {
            return getFinanciamento(id);
        }

        public void Create(Financiamento model)
        {
            _context.Financiamentos.Add(model);
            _context.SaveChanges();
        }

        // helper methods

        private Financiamento getFinanciamento(Guid id)
        {
            var financiamento = _context.Financiamentos.Find(id);
            if (financiamento == null) throw new KeyNotFoundException("Financiamento not found");
            return financiamento;
        }
    }
}
