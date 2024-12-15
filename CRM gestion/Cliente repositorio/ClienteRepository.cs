using CRM_gestion.Data;
using CRM_gestion.Models;
using Microsoft.EntityFrameworkCore;

namespace CRM_gestion.Cliente_repositorio
{
    public class ClienteRepository
    {
        private readonly CRM_gestionContext _context;

        public ClienteRepository(CRM_gestionContext context)
        {
            _context = context ?? throw new ArgumentNullException(nameof(context));
        }

        // Obtener todos los clientes
        public IEnumerable<Cliente> GetAll()
        {
            try
            {
                return _context.Clientes.Include(c => c.Deudas).ToList();
            }
            catch (Exception ex)
            {

                throw new Exception("Error al obtener los clientes", ex);
            }
        }

        // Insertar un cliente
        public void InsertarCliente(Cliente cliente)
        {
            if (cliente == null)
            {
                throw new ArgumentNullException(nameof(cliente), "El cliente no puede ser nulo.");
            }

            try
            {
                _context.Clientes.Add(cliente);
                _context.SaveChanges();
            }
            catch (Exception ex)
            {
                // Manejo del error (por ejemplo, registro del error)
                throw new Exception("Error al insertar el cliente", ex);
            }
        }

        // Buscar cliente por ID
        public Cliente GetById(int clienteId)
        {
            if (clienteId <= 0)
            {
                throw new ArgumentException("El ID del cliente debe ser mayor a 0.", nameof(clienteId));
            }

            try
            {
                return _context.Clientes.Include(c => c.Deudas).FirstOrDefault(c => c.Id == clienteId);
            }
            catch (Exception ex)
            {
                throw new Exception($"Error al obtener el cliente con ID {clienteId}", ex);
            }
        }

        // Borrar un cliente
        public void BorrarCliente(int clienteId)
        {
            if (clienteId <= 0)
            {
                throw new ArgumentException("El ID del cliente debe ser mayor a 0.", nameof(clienteId));
            }

            try
            {
                var cliente = _context.Clientes.Find(clienteId);
                if (cliente == null)
                {
                    throw new KeyNotFoundException($"No se encontró un cliente con ID {clienteId}.");
                }

                _context.Clientes.Remove(cliente);
                _context.SaveChanges();
            }
            catch (Exception ex)
            {
                throw new Exception($"Error al borrar el cliente con ID {clienteId}", ex);
            }
        }

        // Actualizar un cliente
        public void ActualizarCliente(Cliente clienteActualizado)
        {
            if (clienteActualizado == null)
            {
                throw new ArgumentNullException(nameof(clienteActualizado), "El cliente no puede ser nulo.");
            }

            try
            {
                var clienteExistente = _context.Clientes.Find(clienteActualizado.Id);
                if (clienteExistente == null)
                {
                    throw new KeyNotFoundException($"No se encontró un cliente con ID {clienteActualizado.Id}.");
                }

                _context.Entry(clienteExistente).CurrentValues.SetValues(clienteActualizado);
                _context.SaveChanges();
            }
            catch (Exception ex)
            {
                throw new Exception("Error al actualizar el cliente", ex);
            }
        }
    }
}
