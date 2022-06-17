using BancoDesafio.ConsoleApp.Models;
using BancoDesafio.ConsoleApp.Services.Interfaces;

namespace BancoDesafio.ConsoleApp.Services
{
    public class ParcelaService : IParcelaService
    {
        private BANCODESAFIOContext _context;

        public ParcelaService(BANCODESAFIOContext context)
        {
            _context = context;
        }

        public IEnumerable<Parcela> GetAll()
        {
            return _context.Parcelas;
        }

        public Parcela GetById(int id)
        {
            return getParcela(id);
        }

        public int Create(Parcela model)
        {
            _context.Parcelas.Add(model);
            _context.SaveChanges();
            return model.Id;
        }

        public void Update(int id, Parcela model)
        {
            var parcela = getParcela(id);

            _context.Parcelas.Update(parcela);
            _context.SaveChanges();
        }

        // helper methods

        private Parcela getParcela(int id)
        {
            var parcela = _context.Parcelas.Find(id);
            if (parcela == null) throw new KeyNotFoundException("Parcela not found");
            return parcela;
        }
    }
}
