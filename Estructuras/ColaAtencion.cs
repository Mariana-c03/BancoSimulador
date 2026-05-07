using System;
using BancoSimulador.Entidades;

namespace BancoSimulador.Estructuras
{

    /// Cola de atención bancaria implementada manualmente con estructura FIFO
    /// (First In, First Out — el primero en llegar es el primero en ser atendido).
    /// Se mantiene una referencia al frente (primer elemento) y al final (último agregado).
    /// </summary>
    public class ColaAtencion
    {
        // Primer cliente en la cola (próximo a ser atendido)
        private NodoCola _frente;

        // Último cliente que ingresó a la cola
        private NodoCola _final;

        // Cantidad de clientes en espera
        private int _tamano;

        public ColaAtencion()
        {
            _frente = null;
            _final = null;
            _tamano = 0;
        }

        /// <summary>
        /// Encola un cliente al final de la fila de atención.
        /// Complejidad: O(1) — referencia directa al final.
        /// </summary>
        public void Encolar(Cliente cliente)
        {
            NodoCola nuevoNodo = new NodoCola(cliente);

            if (_final == null)
            {
                // Cola vacía: frente y final apuntan al mismo nodo
                _frente = nuevoNodo;
                _final = nuevoNodo;
            }
            else
            {
                // Agregar al final y actualizar referencia
                _final.Siguiente = nuevoNodo;
                _final = nuevoNodo;
            }

            _tamano++;
        }

        /// <summary>
        /// Desencola y retorna el cliente del frente de la fila (FIFO).
        /// El siguiente en fila pasa a ser el nuevo frente.
        /// Complejidad: O(1) — referencia directa al frente.
        /// </summary>
        public Cliente Desencolar()
        {
            if (EstaVacia())
                return null;

            Cliente clienteAtendido = _frente.Dato;
            _frente = _frente.Siguiente;

            // Si la cola queda vacía, limpiar también la referencia al final
            if (_frente == null)
                _final = null;

            _tamano--;
            return clienteAtendido;
        }

        /// <summary>
        /// Consulta el próximo cliente a ser atendido SIN sacarlo de la cola.
        /// Complejidad: O(1).
        /// </summary>
        public Cliente VerSiguiente()
        {
            if (EstaVacia())
                return null;
            return _frente.Dato;
        }

        /// <summary>
        /// Ejecuta una acción para cada cliente en la cola, del frente al final.
        /// </summary>
        public void RecorrerCola(Action<Cliente, int> accion)
        {
            NodoCola actual = _frente;
            int posicion = 1;
            while (actual != null)
            {
                accion(actual.Dato, posicion);
                actual = actual.Siguiente;
                posicion++;
            }
        }

        /// <summary>
        /// Indica si la cola no tiene ningún cliente en espera.
        /// </summary>
        public bool EstaVacia()
        {
            return _frente == null;
        }

        /// <summary>
        /// Retorna la cantidad de clientes en la cola de espera.
        /// </summary>
        public int ObtenerTamano()
        {
            return _tamano;
        }
    }
}
