using BancoDesafio.ConsoleApp.Models;

namespace BancoDesafio.ConsoleApp.Services.Interfaces
{
    public interface IFinanciamentoService
    {
        IEnumerable<Financiamento> GetAll();
        Financiamento GetById(int id);
        int Create(Financiamento financiamento);
    }
}
