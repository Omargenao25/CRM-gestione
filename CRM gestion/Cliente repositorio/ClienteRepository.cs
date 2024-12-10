using CRM_gestion.Models;

namespace CRM_gestion.Cliente_repositorio
{
    public class ClienteRepository
    {
        private static List<Cliente> clientes = new List<Cliente>
    {
        new Cliente { Id = 1, Nombre = "Juan Pérez", Correo = "juan.perez@example.com", Telefono = "555-1234" },
        new Cliente { Id = 2, Nombre = "Ana López", Correo = "ana.lopez@example.com", Telefono = "555-5678" },
        new Cliente { Id = 3, Nombre = "Carlos García", Correo = "carlos.garcia@example.com", Telefono = "555-9101" },
        new Cliente { Id = 4, Nombre = "María Fernández", Correo = "maria.fernandez@example.com", Telefono = "555-1122" },
        new Cliente { Id = 5, Nombre = "Pedro Sánchez", Correo = "pedro.sanchez@example.com", Telefono = "555-3344" }
    };

        public List<Cliente> GetAll()
        {
            return clientes.ToList();
        }

        public void InsertarCliente(Cliente cliente)
        {
            cliente.Id = clientes.Any() ? clientes.Max(c => c.Id) + 1 : 1;
            clientes.Add(cliente);
        }

        internal Cliente GetById(int clienteId)
        {
            throw new NotImplementedException();
        }
    }
}
