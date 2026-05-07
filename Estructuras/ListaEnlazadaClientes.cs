using System;
using BancoSimulador.Entidades;

namespace BancoSimulador.Estructuras
{
    
    /// Lista enlazada simple implementada manualmente para gestionar los clientes del banco.
    /// Cada nodo contiene un cliente y apunta al siguiente nodo de la lista.
    /// </summary>
    public class ListaEnlazadaClientes
    {
        // Referencia al primer nodo (cabeza) de la lista
        private NodoCliente _cabeza;

        // Contador de clientes para no recorrer la lista cada vez
        private int _contador;

        public ListaEnlazadaClientes()
        {
            _cabeza = null;
            _contador = 0;
        }

        
        /// Inserta un nuevo cliente al final de la lista enlazada.
        /// Complejidad: O(n) — se recorre hasta el último nodo.
        /// </summary>
        public void InsertarCliente(Cliente cliente)
        {
            NodoCliente nuevoNodo = new NodoCliente(cliente);

            if (_cabeza == null)
            {
                // Lista vacía: el nuevo nodo es la cabeza
                _cabeza = nuevoNodo;
            }
            else
            {
                // Recorrer hasta el último nodo
                NodoCliente actual = _cabeza;
                while (actual.Siguiente != null)
                {
                    actual = actual.Siguiente;
                }
                // Enlazar el nuevo nodo al final
                actual.Siguiente = nuevoNodo;
            }

            _contador++;
        }

        /// <summary>
        /// Busca un cliente por su número de identificación (cédula).
        /// Complejidad: O(n) — recorrido lineal.
        /// </summary>
        public Cliente BuscarPorIdentificacion(string identificacion)
        {
            NodoCliente actual = _cabeza;
            while (actual != null)
            {
                if (actual.Dato.Identificacion == identificacion)
                    return actual.Dato;
                actual = actual.Siguiente;
            }
            return null; // No encontrado
        }

        /// <summary>
        /// Busca un cliente por su número de cuenta bancaria.
        /// Complejidad: O(n) — recorrido lineal.
        /// </summary>
        public Cliente BuscarPorCuenta(string numeroCuenta)
        {
            NodoCliente actual = _cabeza;
            while (actual != null)
            {
                if (actual.Dato.NumeroCuenta == numeroCuenta)
                    return actual.Dato;
                actual = actual.Siguiente;
            }
            return null;
        }

        /// <summary>
        /// Verifica si ya existe una identificación registrada.
        /// Previene duplicados en el sistema.
        /// </summary>
        public bool ExisteIdentificacion(string identificacion)
        {
            return BuscarPorIdentificacion(identificacion) != null;
        }

        /// <summary>
        /// Verifica si ya existe un número de cuenta registrado.
        /// Previene duplicados en el sistema.
        /// </summary>
        public bool ExisteNumeroCuenta(string numeroCuenta)
        {
            return BuscarPorCuenta(numeroCuenta) != null;
        }

        /// <summary>
        /// Retorna el número total de clientes registrados en la lista.
        /// </summary>
        public int ContarClientes()
        {
            return _contador;
        }

        /// <summary>
        /// Calcula la suma de todos los saldos de la lista.
        /// Recorre la lista completa acumulando saldos.
        /// </summary>
        public decimal CalcularTotalDinero()
        {
            decimal total = 0;
            NodoCliente actual = _cabeza;
            while (actual != null)
            {
                total += actual.Dato.Saldo;
                actual = actual.Siguiente;
            }
            return total;
        }

        /// <summary>
        /// Ejecuta una acción para cada cliente en la lista (patrón visitor).
        /// Permite recorrer la lista desde fuera sin exponer los nodos.
        /// </summary>
        public void RecorrerClientes(Action<Cliente> accion)
        {
            NodoCliente actual = _cabeza;
            while (actual != null)
            {
                accion(actual.Dato);
                actual = actual.Siguiente;
            }
        }

        /// <summary>
        /// Indica si la lista no tiene ningún cliente.
        /// </summary>
        public bool EstaVacia()
        {
            return _cabeza == null;
        }
    }
}
