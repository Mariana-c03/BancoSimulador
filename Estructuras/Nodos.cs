using BancoSimulador.Entidades;

namespace BancoSimulador.Estructuras
{
    /// <summary>
    /// Nodo para la lista enlazada de clientes.
    /// Contiene un cliente y la referencia al siguiente nodo.
    /// </summary>
    public class NodoCliente
    {
        public Cliente Dato { get; set; }
        public NodoCliente Siguiente { get; set; }

        public NodoCliente(Cliente cliente)
        {
            Dato = cliente;
            Siguiente = null;
        }
    }

    /// <summary>
    /// Nodo para la cola de atención (FIFO).
    /// </summary>
    public class NodoCola
    {
        public Cliente Dato { get; set; }
        public NodoCola Siguiente { get; set; }

        public NodoCola(Cliente cliente)
        {
            Dato = cliente;
            Siguiente = null;
        }
    }

    /// <summary>
    /// Nodo para la pila de transacciones (LIFO).
    /// </summary>
    public class NodoPila
    {
        public Transaccion Dato { get; set; }
        public NodoPila Siguiente { get; set; }

        public NodoPila(Transaccion transaccion)
        {
            Dato = transaccion;
            Siguiente = null;
        }
    }
}

// Nodo para listas enlazadas