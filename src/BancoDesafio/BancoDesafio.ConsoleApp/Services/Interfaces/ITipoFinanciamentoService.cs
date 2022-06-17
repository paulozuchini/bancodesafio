using BancoDesafio.ConsoleApp.Models;

namespace BancoDesafio.ConsoleApp.Services.Interfaces
{
    public interface ITipoFinanciamentoService
    {
        IEnumerable<TipoFinanciamento> GetAll();
        TipoFinanciamento GetById(int id);
    }
}
