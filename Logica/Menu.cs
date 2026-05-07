using System;
using BancoSimulador.Entidades;
using BancoSimulador.Logica;
using BancoSimulador.UI;

namespace BancoSimulador.Logica
{
    
    /// Maneja todos los menús e interacción con el usuario.
    /// Delega la lógica de negocio al servicio Banco.
    /// </summary>
    public class Menu
    {
        private readonly Banco _banco;

        public Menu(Banco banco)
        {
            _banco = banco;
        }

        // ==================== MENÚ PRINCIPAL ====================

        public void MostrarMenuPrincipal()
        {
            while (true)
            {
                ConsoleHelper.MostrarEncabezadoBanco();

                Console.ForegroundColor = ConsoleColor.Cyan;
                Console.WriteLine("  ┌─────────────────────────────────────────┐");
                Console.WriteLine("  │            MENÚ PRINCIPAL               │");
                Console.WriteLine("  ├─────────────────────────────────────────┤");
                Console.ForegroundColor = ConsoleColor.White;
                Console.WriteLine("  │  GESTIÓN DE CLIENTES                    │");
                Console.WriteLine("  │   1. Registrar cliente                  │");
                Console.WriteLine("  │   2. Listar todos los clientes          │");
                Console.WriteLine("  │   3. Buscar cliente por identificación  │");
                Console.ForegroundColor = ConsoleColor.Cyan;
                Console.WriteLine("  ├─────────────────────────────────────────┤");
                Console.ForegroundColor = ConsoleColor.White;
                Console.WriteLine("  │  COLA DE ATENCIÓN                       │");
                Console.WriteLine("  │   4. Agregar cliente a cola             │");
                Console.WriteLine("  │   5. Atender siguiente cliente          │");
                Console.WriteLine("  │  10. Mostrar cola de atención           │");
                Console.ForegroundColor = ConsoleColor.Cyan;
                Console.WriteLine("  ├─────────────────────────────────────────┤");
                Console.ForegroundColor = ConsoleColor.White;
                Console.WriteLine("  │  OPERACIONES BANCARIAS                  │");
                Console.WriteLine("  │   6. Realizar depósito                  │");
                Console.WriteLine("  │   7. Realizar retiro                    │");
                Console.WriteLine("  │   8. Consultar saldo                    │");
                Console.WriteLine("  │   9. Deshacer última transacción        │");
                Console.ForegroundColor = ConsoleColor.Cyan;
                Console.WriteLine("  ├─────────────────────────────────────────┤");
                Console.ForegroundColor = ConsoleColor.White;
                Console.WriteLine("  │  INFORMACIÓN GENERAL                    │");
                Console.WriteLine("  │  11. Total de clientes registrados      │");
                Console.WriteLine("  │  12. Total de dinero en el banco        │");
                Console.ForegroundColor = ConsoleColor.Cyan;
                Console.WriteLine("  ├─────────────────────────────────────────┤");
                Console.ForegroundColor = ConsoleColor.Red;
                Console.WriteLine("  │  13. Salir del sistema                  │");
                Console.WriteLine("  └─────────────────────────────────────────┘");
                Console.ResetColor();
                Console.WriteLine();

                string seleccion = ConsoleHelper.PedirTexto("Seleccione una opción");

                if (!int.TryParse(seleccion, out int opcion))
                {
                    ConsoleHelper.Error("Debe ingresar un número entre 1 y 13.");
                    ConsoleHelper.EsperarTecla();
                    continue;
                }

                switch (opcion)
                {
                    case 1:  MenuRegistrarCliente(); break;
                    case 2:  MenuListarClientes(); break;
                    case 3:  MenuBuscarCliente(); break;
                    case 4:  MenuAgregarACola(); break;
                    case 5:  MenuAtenderSiguiente(); break;
                    case 6:  MenuDeposito(); break;
                    case 7:  MenuRetiro(); break;
                    case 8:  MenuConsultarSaldo(); break;
                    case 9:  MenuDeshacerTransaccion(); break;
                    case 10: MenuMostrarCola(); break;
                    case 11: MenuTotalClientes(); break;
                    case 12: MenuTotalDinero(); break;
                    case 13: MenuSalir(); return;
                    default:
                        ConsoleHelper.Error("Opción inválida. Elija entre 1 y 13.");
                        ConsoleHelper.EsperarTecla();
                        break;
                }
            }
        }

        // ==================== REGISTRAR CLIENTE ====================

        private void MenuRegistrarCliente()
        {
            ConsoleHelper.MostrarEncabezadoBanco();
            ConsoleHelper.Titulo("Registrar Nuevo Cliente");

            string identificacion = ConsoleHelper.PedirTexto("Identificación (cédula)");
            if (string.IsNullOrWhiteSpace(identificacion))
            {
                ConsoleHelper.Error("La identificación no puede estar vacía.");
                ConsoleHelper.EsperarTecla();
                return;
            }

            string nombre = ConsoleHelper.PedirTexto("Nombre completo");
            if (string.IsNullOrWhiteSpace(nombre))
            {
                ConsoleHelper.Error("El nombre no puede estar vacío.");
                ConsoleHelper.EsperarTecla();
                return;
            }

            string numeroCuenta = ConsoleHelper.PedirTexto("Número de cuenta (ej: CTA-001)");
            if (string.IsNullOrWhiteSpace(numeroCuenta))
            {
                ConsoleHelper.Error("El número de cuenta no puede estar vacío.");
                ConsoleHelper.EsperarTecla();
                return;
            }

            if (!ConsoleHelper.PedirDecimal("Saldo inicial", out decimal saldoInicial))
            {
                ConsoleHelper.Error("El saldo inicial debe ser un número válido.");
                ConsoleHelper.EsperarTecla();
                return;
            }

            var (exito, mensaje) = _banco.RegistrarCliente(identificacion, nombre, numeroCuenta, saldoInicial);

            ConsoleHelper.LineasEnBlanco();
            ConsoleHelper.Separador();
            if (exito)
                ConsoleHelper.Exito(mensaje);
            else
                ConsoleHelper.Error(mensaje);

            ConsoleHelper.EsperarTecla();
        }

        // ==================== LISTAR CLIENTES ====================

        private void MenuListarClientes()
        {
            ConsoleHelper.MostrarEncabezadoBanco();
            ConsoleHelper.Titulo("Lista de Clientes Registrados");

            if (_banco.ListaVacia())
            {
                ConsoleHelper.Advertencia("No hay clientes registrados en el sistema.");
                ConsoleHelper.EsperarTecla();
                return;
            }

            ConsoleHelper.MostrarEncabezadoTablaClientes();

            int numero = 1;
            _banco.RecorrerClientes(cliente =>
            {
                ConsoleHelper.MostrarFilaCliente(numero, cliente);
                numero++;
            });

            ConsoleHelper.Separador();
            ConsoleHelper.Info($"Total de clientes: {_banco.ContarClientes()}");
            ConsoleHelper.Info($"Total de dinero en el banco: {_banco.ObtenerTotalDinero():C2}");
            ConsoleHelper.EsperarTecla();
        }

        // ==================== BUSCAR CLIENTE ====================

        private void MenuBuscarCliente()
        {
            ConsoleHelper.MostrarEncabezadoBanco();
            ConsoleHelper.Titulo("Buscar Cliente");

            string identificacion = ConsoleHelper.PedirTexto("Identificación del cliente a buscar");
            if (string.IsNullOrWhiteSpace(identificacion))
            {
                ConsoleHelper.Error("Ingrese una identificación válida.");
                ConsoleHelper.EsperarTecla();
                return;
            }

            Cliente cliente = _banco.BuscarClientePorId(identificacion);

            ConsoleHelper.LineasEnBlanco();
            ConsoleHelper.Separador();

            if (cliente == null)
            {
                ConsoleHelper.Advertencia($"No se encontró ningún cliente con la identificación '{identificacion}'.");
            }
            else
            {
                ConsoleHelper.Exito("Cliente encontrado:");
                ConsoleHelper.LineasEnBlanco();
                ConsoleHelper.TextoResaltado("Identificación", cliente.Identificacion);
                ConsoleHelper.TextoResaltado("Nombre", cliente.NombreCompleto);
                ConsoleHelper.TextoResaltado("Cuenta", cliente.NumeroCuenta);
                ConsoleHelper.TextoResaltado("Saldo", cliente.Saldo.ToString("C2"));
            }

            ConsoleHelper.EsperarTecla();
        }

        // ==================== COLA DE ATENCIÓN ====================

        private void MenuAgregarACola()
        {
            ConsoleHelper.MostrarEncabezadoBanco();
            ConsoleHelper.Titulo("Agregar Cliente a Cola de Atención");

            string identificacion = ConsoleHelper.PedirTexto("Identificación del cliente");
            if (string.IsNullOrWhiteSpace(identificacion))
            {
                ConsoleHelper.Error("Ingrese una identificación válida.");
                ConsoleHelper.EsperarTecla();
                return;
            }

            var (exito, mensaje) = _banco.AgregarAColaPorId(identificacion);

            ConsoleHelper.Separador();
            if (exito)
            {
                ConsoleHelper.Exito(mensaje);
                ConsoleHelper.Info($"Clientes en espera: {_banco.TamanoColaAtencion()}");
            }
            else
                ConsoleHelper.Error(mensaje);

            ConsoleHelper.EsperarTecla();
        }

        private void MenuAtenderSiguiente()
        {
            ConsoleHelper.MostrarEncabezadoBanco();
            ConsoleHelper.Titulo("Atender Siguiente Cliente");

            // Mostrar quién es el próximo antes de atender
            Cliente proximo = _banco.VerProximoEnCola();
            if (proximo != null)
            {
                ConsoleHelper.SubTitulo("Próximo en atender");
                ConsoleHelper.TextoResaltado("Cliente", proximo.NombreCompleto);
                ConsoleHelper.TextoResaltado("Cuenta", proximo.NumeroCuenta);
                ConsoleHelper.LineasEnBlanco();
            }

            var (exito, mensaje, clienteAtendido) = _banco.AtenderSiguiente();

            ConsoleHelper.Separador();
            if (exito)
            {
                ConsoleHelper.Exito(mensaje);
                if (_banco.TamanoColaAtencion() > 0)
                    ConsoleHelper.Info($"Clientes restantes en la cola: {_banco.TamanoColaAtencion()}");
                else
                    ConsoleHelper.Info("La cola de atención está ahora vacía.");
            }
            else
                ConsoleHelper.Advertencia(mensaje);

            ConsoleHelper.EsperarTecla();
        }

        private void MenuMostrarCola()
        {
            ConsoleHelper.MostrarEncabezadoBanco();
            ConsoleHelper.Titulo("Cola de Atención Actual");

            if (_banco.ColaVacia())
            {
                ConsoleHelper.Advertencia("La cola de atención está vacía.");
                ConsoleHelper.EsperarTecla();
                return;
            }

            Console.ForegroundColor = ConsoleColor.DarkCyan;
            Console.WriteLine($"\n  {"#",-6} {"CLIENTE",-25} {"CUENTA",-15}");
            Console.WriteLine($"  {"──────",-6} {"────────────────────────",-25} {"────────────",-15}");
            Console.ResetColor();

            _banco.RecorrerCola((cliente, posicion) =>
            {
                string marca = posicion == 1 ? " ← SIGUIENTE" : "";
                Console.ForegroundColor = posicion == 1 ? ConsoleColor.Green : ConsoleColor.White;
                Console.WriteLine($"  {posicion,-6} {cliente.NombreCompleto,-25} {cliente.NumeroCuenta,-15}{marca}");
                Console.ResetColor();
            });

            ConsoleHelper.Separador();
            ConsoleHelper.Info($"Total en espera: {_banco.TamanoColaAtencion()} cliente(s)");
            ConsoleHelper.EsperarTecla();
        }

        // ==================== OPERACIONES BANCARIAS ====================

        private void MenuDeposito()
        {
            ConsoleHelper.MostrarEncabezadoBanco();
            ConsoleHelper.Titulo("Realizar Depósito");

            string numeroCuenta = ConsoleHelper.PedirTexto("Número de cuenta");
            if (string.IsNullOrWhiteSpace(numeroCuenta))
            {
                ConsoleHelper.Error("Ingrese un número de cuenta válido.");
                ConsoleHelper.EsperarTecla();
                return;
            }

            if (!ConsoleHelper.PedirDecimal("Monto a depositar", out decimal monto))
            {
                ConsoleHelper.Error("El monto debe ser un número válido.");
                ConsoleHelper.EsperarTecla();
                return;
            }

            var (exito, mensaje) = _banco.RealizarDeposito(numeroCuenta, monto);

            ConsoleHelper.Separador();
            if (exito)
                ConsoleHelper.Exito(mensaje);
            else
                ConsoleHelper.Error(mensaje);

            ConsoleHelper.EsperarTecla();
        }

        private void MenuRetiro()
        {
            ConsoleHelper.MostrarEncabezadoBanco();
            ConsoleHelper.Titulo("Realizar Retiro");

            string numeroCuenta = ConsoleHelper.PedirTexto("Número de cuenta");
            if (string.IsNullOrWhiteSpace(numeroCuenta))
            {
                ConsoleHelper.Error("Ingrese un número de cuenta válido.");
                ConsoleHelper.EsperarTecla();
                return;
            }

            if (!ConsoleHelper.PedirDecimal("Monto a retirar", out decimal monto))
            {
                ConsoleHelper.Error("El monto debe ser un número válido.");
                ConsoleHelper.EsperarTecla();
                return;
            }

            var (exito, mensaje) = _banco.RealizarRetiro(numeroCuenta, monto);

            ConsoleHelper.Separador();
            if (exito)
                ConsoleHelper.Exito(mensaje);
            else
                ConsoleHelper.Error(mensaje);

            ConsoleHelper.EsperarTecla();
        }

        private void MenuConsultarSaldo()
        {
            ConsoleHelper.MostrarEncabezadoBanco();
            ConsoleHelper.Titulo("Consultar Saldo");

            string numeroCuenta = ConsoleHelper.PedirTexto("Número de cuenta");
            if (string.IsNullOrWhiteSpace(numeroCuenta))
            {
                ConsoleHelper.Error("Ingrese un número de cuenta válido.");
                ConsoleHelper.EsperarTecla();
                return;
            }

            var (exito, mensaje, saldo) = _banco.ConsultarSaldo(numeroCuenta);

            ConsoleHelper.Separador();
            if (exito)
            {
                ConsoleHelper.Exito("Consulta exitosa:");
                ConsoleHelper.LineasEnBlanco();
                Console.ForegroundColor = ConsoleColor.Green;
                Console.WriteLine($"\n  ┌────────────────────────────────┐");
                Console.WriteLine($"  │  Cuenta: {numeroCuenta,-22}│");
                Console.WriteLine($"  │  Saldo:  {saldo,17:C2}  │");
                Console.WriteLine($"  └────────────────────────────────┘");
                Console.ResetColor();
            }
            else
                ConsoleHelper.Error(mensaje);

            ConsoleHelper.EsperarTecla();
        }

        // ==================== DESHACER TRANSACCIÓN ====================

        private void MenuDeshacerTransaccion()
        {
            ConsoleHelper.MostrarEncabezadoBanco();
            ConsoleHelper.Titulo("Deshacer Última Transacción");

            // Mostrar cuál es la última transacción antes de revertir
            var ultima = _banco.VerUltimaTransaccion();
            if (ultima != null)
            {
                ConsoleHelper.SubTitulo("Transacción a revertir");
                ConsoleHelper.Texto(ultima.ToString());
                ConsoleHelper.LineasEnBlanco();
            }

            var (exito, mensaje, transaccion) = _banco.DeshacerUltimaTransaccion();

            ConsoleHelper.Separador();
            if (exito)
            {
                ConsoleHelper.Exito(mensaje);
                ConsoleHelper.Info($"Transacciones restantes en el historial: {_banco.TamanoPila()}");
            }
            else
                ConsoleHelper.Advertencia(mensaje);

            ConsoleHelper.EsperarTecla();
        }

        // ==================== INFORMACIÓN GENERAL ====================

        private void MenuTotalClientes()
        {
            ConsoleHelper.MostrarEncabezadoBanco();
            ConsoleHelper.Titulo("Total de Clientes Registrados");
            ConsoleHelper.LineasEnBlanco();

            int total = _banco.ContarClientes();

            Console.ForegroundColor = ConsoleColor.Green;
            Console.WriteLine($"\n  ┌──────────────────────────────────┐");
            Console.WriteLine($"  │  Clientes registrados: {total,9}  │");
            Console.WriteLine($"  └──────────────────────────────────┘");
            Console.ResetColor();

            if (total == 0)
                ConsoleHelper.Advertencia("Aún no hay clientes registrados.");

            ConsoleHelper.EsperarTecla();
        }

        private void MenuTotalDinero()
        {
            ConsoleHelper.MostrarEncabezadoBanco();
            ConsoleHelper.Titulo("Total de Dinero en el Banco");
            ConsoleHelper.LineasEnBlanco();

            decimal total = _banco.ObtenerTotalDinero();

            Console.ForegroundColor = ConsoleColor.Green;
            Console.WriteLine($"\n  ┌──────────────────────────────────────┐");
            Console.WriteLine($"  │  Total combinado en cuentas:          │");
            Console.WriteLine($"  │  {total,34:C2}  │");
            Console.WriteLine($"  └──────────────────────────────────────┘");
            Console.ResetColor();

            ConsoleHelper.Info($"Calculado sobre {_banco.ContarClientes()} cliente(s) registrados.");
            ConsoleHelper.EsperarTecla();
        }

        // ==================== SALIR ====================

        private void MenuSalir()
        {
            Console.Clear();
            Console.ForegroundColor = ConsoleColor.Cyan;
            Console.WriteLine();
            Console.WriteLine("  ╔══════════════════════════════════════════╗");
            Console.WriteLine("  ║                                          ║");
            Console.WriteLine("  ║     Gracias por usar Banco Estructuras   ║");
            Console.WriteLine("  ║         ¡Hasta la próxima!               ║");
            Console.WriteLine("  ║                                          ║");
            Console.WriteLine("  ╚══════════════════════════════════════════╝");
            Console.ResetColor();
            Console.WriteLine();
        }
    }
}
