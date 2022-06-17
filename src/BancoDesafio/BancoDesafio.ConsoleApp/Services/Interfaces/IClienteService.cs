using BancoDesafio.ConsoleApp.Models;

namespace BancoDesafio.ConsoleApp.Services.Interfaces
{
    public interface IClienteService
    {
        IEnumerable<Cliente> GetAll();
        Cliente GetById(int id);
        void Create(Cliente user);
        void Update(int id, Cliente user);
        void Delete(int id);
    }
}
