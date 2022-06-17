using BancoDesafio.ConsoleApp.Models;
using BancoDesafio.ConsoleApp.Services.Interfaces;

namespace BancoDesafio.ConsoleApp.Services
{
    public class TipoFinanciamentoService : ITipoFinanciamentoService
    {
        private BANCODESAFIOContext _context;

        public TipoFinanciamentoService(BANCODESAFIOContext context)
        {
            _context = context;
        }

        public IEnumerable<TipoFinanciamento> GetAll()
        {
            return _context.TipoFinanciamentos;
        }

        public TipoFinanciamento GetById(int id)
        {
            return getTipo(id);
        }

        // helper methods

        private TipoFinanciamento getTipo(int id)
        {
            var tipo = _context.TipoFinanciamentos.Find(id);
            if (tipo == null) throw new KeyNotFoundException("TipoFinanciamento not found");
            return tipo;
        }
    }
}
