using BancoDesafio.ConsoleApp.Models;

namespace BancoDesafio.ConsoleApp.Services.Interfaces
{
    public interface IParcelaService
    {
        IEnumerable<Parcela> GetAll();
        Parcela GetById(Guid id);
        void Create(Parcela parcela);
        void Update(Guid id, Parcela parcela);
    }
}
