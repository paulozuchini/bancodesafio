using BancoDesafio.ConsoleApp.Models;

namespace BancoDesafio.ConsoleApp.Services.Interfaces
{
    public interface IClienteService
    {
        IEnumerable<Cliente> GetAll();
        Cliente GetById(Guid id);
        void Create(Cliente user);
        void Update(Guid id, Cliente user);
        void Delete(Guid id);
    }
}
