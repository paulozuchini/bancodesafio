using BancoDesafio.ConsoleApp.Models;
using BancoDesafio.ConsoleApp.Services.Interfaces;

namespace BancoDesafio.ConsoleApp.Services
{
    internal class ClienteService : IClienteService
    {
        private BANCODESAFIOContext _context;

        public ClienteService(BANCODESAFIOContext context)
        {
            _context = context;
        }

        public IEnumerable<Cliente> GetAll()
        {
            return _context.Clientes;
        }

        public Cliente GetById(Guid id)
        {
            return getUser(id);
        }

        public void Create(Cliente model)
        {
            // validate
            if (_context.Clientes.Any(x => x.Nome == model.Nome && x.Celular == model.Celular && x.Uf == model.Uf))
                throw new Exception("User already exists");

            // save user
            _context.Clientes.Add(model);
            _context.SaveChanges();
        }

        public void Update(Guid id, Cliente model)
        {
            var user = getUser(id);

            // validate
            if (_context.Clientes.Any(x => x.Nome == model.Nome && x.Celular == model.Celular && x.Uf == model.Uf))
                throw new Exception("User already exists");

            _context.Clientes.Update(user);
            _context.SaveChanges();
        }

        public void Delete(Guid id)
        {
            var user = getUser(id);
            _context.Clientes.Remove(user);
            _context.SaveChanges();
        }

        // helper methods

        private Cliente getUser(Guid id)
        {
            var user = _context.Clientes.Find(id);
            if (user == null) throw new KeyNotFoundException("User not found");
            return user;
        }
    }
}
