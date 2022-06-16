using AutoMapper;
using BancoDesafio.ConsoleApp.Models;
using BancoDesafio.ConsoleApp.Services.Interfaces;

namespace BancoDesafio.ConsoleApp.Services
{
    public class ParcelaService : IParcelaService
    {
        private BANCODESAFIOContext _context;
        private readonly IMapper _mapper;

        public ParcelaService(
            BANCODESAFIOContext context,
            IMapper mapper)
        {
            _context = context;
            _mapper = mapper;
        }

        public IEnumerable<Parcela> GetAll()
        {
            return _context.Parcelas;
        }

        public Parcela GetById(Guid id)
        {
            return getParcela(id);
        }

        public void Create(Parcela model)
        {
            _context.Parcelas.Add(model);
            _context.SaveChanges();
        }

        public void Update(Guid id, Parcela model)
        {
            var parcela = getParcela(id);

            _context.Parcelas.Update(parcela);
            _context.SaveChanges();
        }

        // helper methods

        private Parcela getParcela(Guid id)
        {
            var parcela = _context.Parcelas.Find(id);
            if (parcela == null) throw new KeyNotFoundException("Parcela not found");
            return parcela;
        }
    }
}
