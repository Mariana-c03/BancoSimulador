// Este archivo forma parte de la capa de servicios del sistema bancario.
// La lógica principal de operaciones bancarias se encuentra en Banco.cs,
// que centraliza la coordinación de las tres estructuras de datos:
//   - ListaEnlazadaClientes  → gestión de clientes
//   - ColaAtencion           → turnos de atención (FIFO)
//   - PilaTransacciones      → historial y reversión (LIFO)

namespace BancoSimulador.Logica
{
    // ServicioBanco actúa como fachada pública del sistema.
    // Toda la lógica de negocio está implementada en Banco.cs.
}
