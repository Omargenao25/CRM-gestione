using CRM_gestion.Data;
using CRM_gestion.Models;

namespace CRM_gestion.Cliente_repositorio
{
    public class ClienteRepository
    {
        private static CRM_gestionContext _context;


        public ClienteRepository(CRM_gestionContext _context)
        {
            _context = _context;
        }

        // Obtener todos los clientes
        public IEnumerable<Cliente> GetAll()
        {
           return _context.Clientes.ToList();

        }

        // Insertar un cliente
        public void InsertarCliente(Cliente cliente)
        { 
            _context.Clientes.Add(cliente);
        }

        // Buscar cliente por ID
        public Cliente GetById(int clienteId)
        {
            return _context.Clientes.FirstOrDefault(c => c.Id == clienteId);
        }

        // Borrar un cliente
        public void BorrarCliente(int clienteId)
        {
            var cliente = _context.Clientes.FirstOrDefault(c => c.Id == clienteId);
            if (cliente != null)
            {
                _context.Clientes.Remove(cliente);
            }
        }

        // Actualizar un cliente
        public void ActualizarCliente(Cliente clienteActualizado)
        {
            var clienteExistente = _context.Clientes.FirstOrDefault(c => c.Id == clienteActualizado.Id);
            if (clienteExistente != null)
            {
                clienteExistente.Nombre = clienteActualizado.Nombre;
                clienteExistente.Correo = clienteActualizado.Correo;
                clienteExistente.Telefono = clienteActualizado.Telefono;
            }
        }
    }
}
