using System;
using BancoSimulador.Estructuras;
using BancoSimulador.Entidades;

namespace BancoSimulador.Logica
{
    
    /// Contiene toda la lógica de negocio del banco.
    /// Coordina las tres estructuras de datos: lista, cola y pila.
    public class Banco
    {
        // Estructura 1: Lista enlazada para gestión de clientes
        private readonly ListaEnlazadaClientes _listaClientes;

        // Estructura 2: Cola FIFO para atención por turnos
        private readonly ColaAtencion _colaAtencion;

        // Estructura 3: Pila LIFO para historial y reversión de transacciones
        private readonly PilaTransacciones _pilaTransacciones;

        public Banco()
        {
            _listaClientes = new ListaEnlazadaClientes();
            _colaAtencion = new ColaAtencion();
            _pilaTransacciones = new PilaTransacciones();
        }

        // ==================== GESTIÓN DE CLIENTES ====================

        /// <summary>
        /// Registra un nuevo cliente después de validar todos los datos.
        /// Retorna un mensaje de resultado para mostrar al usuario.
        /// </summary>
        public (bool exito, string mensaje) RegistrarCliente(string identificacion, string nombre, string numeroCuenta, decimal saldoInicial)
        {
            // Validar nombre
            if (string.IsNullOrWhiteSpace(nombre))
                return (false, "El nombre no puede estar vacío.");

            // Validar identificación
            if (string.IsNullOrWhiteSpace(identificacion))
                return (false, "La identificación no puede estar vacía.");

            // Validar número de cuenta
            if (string.IsNullOrWhiteSpace(numeroCuenta))
                return (false, "El número de cuenta no puede estar vacío.");

            // Validar saldo inicial
            if (saldoInicial < 0)
                return (false, "El saldo inicial no puede ser negativo.");

            // Validar duplicados en la lista enlazada
            if (_listaClientes.ExisteIdentificacion(identificacion))
                return (false, $"Ya existe un cliente con la identificación '{identificacion}'.");

            if (_listaClientes.ExisteNumeroCuenta(numeroCuenta))
                return (false, $"El número de cuenta '{numeroCuenta}' ya está en uso.");

            // Insertar en la lista enlazada
            Cliente nuevoCliente = new Cliente(identificacion, nombre.Trim(), numeroCuenta.Trim(), saldoInicial);
            _listaClientes.InsertarCliente(nuevoCliente);

            return (true, $"Cliente '{nombre}' registrado exitosamente con la cuenta {numeroCuenta}.");
        }

        /// <summary>
        /// Busca un cliente por identificación usando la lista enlazada.
        /// </summary>
        public Cliente BuscarClientePorId(string identificacion)
        {
            return _listaClientes.BuscarPorIdentificacion(identificacion);
        }

        /// <summary>
        /// Busca un cliente por número de cuenta usando la lista enlazada.
        /// </summary>
        public Cliente BuscarClientePorCuenta(string numeroCuenta)
        {
            return _listaClientes.BuscarPorCuenta(numeroCuenta);
        }

        /// <summary>
        /// Retorna el número de clientes registrados en la lista.
        /// </summary>
        public int ContarClientes()
        {
            return _listaClientes.ContarClientes();
        }

        /// <summary>
        /// Calcula el total de dinero en todas las cuentas del banco.
        /// </summary>
        public decimal ObtenerTotalDinero()
        {
            return _listaClientes.CalcularTotalDinero();
        }

        /// <summary>
        /// Permite recorrer todos los clientes de la lista con una acción externa.
        /// </summary>
        public void RecorrerClientes(Action<Cliente> accion)
        {
            _listaClientes.RecorrerClientes(accion);
        }

        public bool ListaVacia() => _listaClientes.EstaVacia();

        // ==================== COLA DE ATENCIÓN ====================

        /// <summary>
        /// Agrega un cliente a la cola de atención por turnos.
        /// </summary>
        public (bool exito, string mensaje) AgregarAColaPorId(string identificacion)
        {
            Cliente cliente = _listaClientes.BuscarPorIdentificacion(identificacion);
            if (cliente == null)
                return (false, $"No se encontró ningún cliente con la identificación '{identificacion}'.");

            _colaAtencion.Encolar(cliente);
            return (true, $"Cliente '{cliente.NombreCompleto}' agregado a la cola de atención.");
        }

        /// <summary>
        /// Atiende al siguiente cliente en la cola (FIFO).
        /// </summary>
        public (bool exito, string mensaje, Cliente cliente) AtenderSiguiente()
        {
            if (_colaAtencion.EstaVacia())
                return (false, "La cola de atención está vacía. No hay clientes en espera.", null);

            Cliente atendido = _colaAtencion.Desencolar();
            return (true, $"Atendiendo a: {atendido.NombreCompleto} (Cuenta: {atendido.NumeroCuenta})", atendido);
        }

        /// <summary>
        /// Consulta el próximo cliente sin retirarlo de la cola.
        /// </summary>
        public Cliente VerProximoEnCola()
        {
            return _colaAtencion.VerSiguiente();
        }

        public void RecorrerCola(Action<Cliente, int> accion)
        {
            _colaAtencion.RecorrerCola(accion);
        }

        public bool ColaVacia() => _colaAtencion.EstaVacia();
        public int TamanoColaAtencion() => _colaAtencion.ObtenerTamano();

        // ==================== OPERACIONES BANCARIAS ====================

        /// <summary>
        /// Realiza un depósito en una cuenta y registra la transacción en la pila.
        /// </summary>
        public (bool exito, string mensaje) RealizarDeposito(string numeroCuenta, decimal monto)
        {
            if (monto <= 0)
                return (false, "El monto del depósito debe ser mayor a cero.");

            Cliente cliente = _listaClientes.BuscarPorCuenta(numeroCuenta);
            if (cliente == null)
                return (false, $"No existe ninguna cuenta con el número '{numeroCuenta}'.");

            decimal saldoAnterior = cliente.Saldo;
            cliente.Saldo += monto;

            // Registrar transacción en la pila LIFO
            Transaccion transaccion = new Transaccion(
                TipoTransaccion.Deposito, numeroCuenta, monto, saldoAnterior, cliente.Saldo);
            _pilaTransacciones.Apilar(transaccion);

            return (true, $"Depósito de {monto:C2} realizado. Nuevo saldo: {cliente.Saldo:C2}");
        }

        /// <summary>
        /// Realiza un retiro de una cuenta y registra la transacción en la pila.
        /// </summary>
        public (bool exito, string mensaje) RealizarRetiro(string numeroCuenta, decimal monto)
        {
            if (monto <= 0)
                return (false, "El monto del retiro debe ser mayor a cero.");

            Cliente cliente = _listaClientes.BuscarPorCuenta(numeroCuenta);
            if (cliente == null)
                return (false, $"No existe ninguna cuenta con el número '{numeroCuenta}'.");

            if (monto > cliente.Saldo)
                return (false, $"Saldo insuficiente. Saldo disponible: {cliente.Saldo:C2}, intento de retiro: {monto:C2}");

            decimal saldoAnterior = cliente.Saldo;
            cliente.Saldo -= monto;

            // Registrar transacción en la pila LIFO
            Transaccion transaccion = new Transaccion(
                TipoTransaccion.Retiro, numeroCuenta, monto, saldoAnterior, cliente.Saldo);
            _pilaTransacciones.Apilar(transaccion);

            return (true, $"Retiro de {monto:C2} realizado. Nuevo saldo: {cliente.Saldo:C2}");
        }

        /// <summary>
        /// Consulta el saldo de una cuenta específica.
        /// </summary>
        public (bool exito, string mensaje, decimal saldo) ConsultarSaldo(string numeroCuenta)
        {
            Cliente cliente = _listaClientes.BuscarPorCuenta(numeroCuenta);
            if (cliente == null)
                return (false, $"No existe ninguna cuenta con el número '{numeroCuenta}'.", 0);

            return (true, $"Saldo disponible en cuenta {numeroCuenta}: {cliente.Saldo:C2}", cliente.Saldo);
        }

        // ==================== REVERSIÓN DE TRANSACCIONES ====================

        /// <summary>
        /// Deshace la última transacción usando la pila (LIFO).
        /// Restaura el saldo anterior del cliente afectado.
        /// </summary>
        public (bool exito, string mensaje, Transaccion transaccion) DeshacerUltimaTransaccion()
        {
            if (_pilaTransacciones.EstaVacia())
                return (false, "No hay transacciones para deshacer.", null);

            // Desapilar la transacción más reciente
            Transaccion ultima = _pilaTransacciones.Desapilar();

            // Buscar el cliente afectado y restaurar su saldo anterior
            Cliente cliente = _listaClientes.BuscarPorCuenta(ultima.NumeroCuenta);
            if (cliente == null)
                return (false, "Error al deshacer: no se encontró la cuenta asociada.", null);

            cliente.Saldo = ultima.SaldoAnterior;

            string tipoStr = ultima.Tipo == TipoTransaccion.Deposito ? "depósito" : "retiro";
            return (true,
                $"Transacción revertida: {tipoStr} de {ultima.Monto:C2} en cuenta {ultima.NumeroCuenta}. " +
                $"Saldo restaurado a {ultima.SaldoAnterior:C2}.",
                ultima);
        }

        /// <summary>
        /// Consulta la última transacción sin revertirla.
        /// </summary>
        public Transaccion VerUltimaTransaccion()
        {
            return _pilaTransacciones.VerCima();
        }

        public bool PilaVacia() => _pilaTransacciones.EstaVacia();
        public int TamanoPila() => _pilaTransacciones.ObtenerTamano();
    }
}
