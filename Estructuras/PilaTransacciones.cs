using BancoSimulador.Entidades;

namespace BancoSimulador.Estructuras
{
    
    /// Pila de transacciones implementada manualmente con estructura LIFO
    /// (Last In, First Out — la última transacción es la primera en poderse deshacer).
    /// Funciona como un historial de operaciones que permite revertir la más reciente.
    public class PilaTransacciones
    {
        // Cima de la pila: apunta siempre a la transacción más reciente
        private NodoPila _cima;

        // Cantidad de transacciones en el historial
        private int _tamano;

        public PilaTransacciones()
        {
            _cima = null;
            _tamano = 0;
        }

        /// <summary>
        /// Apila (push) una nueva transacción en la cima.
        /// El nuevo nodo apunta al nodo que era antes la cima.
        /// Complejidad: O(1).
        /// </summary>
        public void Apilar(Transaccion transaccion)
        {
            NodoPila nuevoNodo = new NodoPila(transaccion);

            // El nuevo nodo enlaza con la cima actual antes de reemplazarla
            nuevoNodo.Siguiente = _cima;
            _cima = nuevoNodo;

            _tamano++;
        }

        /// <summary>
        /// Desapila (pop) y retorna la transacción más reciente de la cima.
        /// La cima pasa a ser el nodo anterior.
        /// Complejidad: O(1).
        /// </summary>
        public Transaccion Desapilar()
        {
            if (EstaVacia())
                return null;

            Transaccion transaccionReciente = _cima.Dato;
            _cima = _cima.Siguiente; // La cima sube al nodo anterior
            _tamano--;

            return transaccionReciente;
        }

        /// <summary>
        /// Consulta la transacción en la cima SIN desapilarla.
        /// Complejidad: O(1).
        /// </summary>
        public Transaccion VerCima()
        {
            if (EstaVacia())
                return null;
            return _cima.Dato;
        }

        /// <summary>
        /// Indica si no hay transacciones en la pila.
        /// </summary>
        public bool EstaVacia()
        {
            return _cima == null;
        }

        /// <summary>
        /// Retorna la cantidad de transacciones almacenadas.
        /// </summary>
        public int ObtenerTamano()
        {
            return _tamano;
        }
    }
}
