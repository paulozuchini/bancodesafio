using BancoDesafio.ConsoleApp.Models;

namespace BancoDesafio.ConsoleApp.Services.Interfaces
{
    public interface IParcelaService
    {
        IEnumerable<Parcela> GetAll();
        Parcela GetById(int id);
        int Create(Parcela parcela);
        void Update(int id, Parcela parcela);
    }
}
