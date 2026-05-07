using System;

namespace BancoSimulador.UI
{
    
    /// Utilidades visuales para la consola. Centraliza colores, separadores y mensajes.
    /// </summary>
    public static class ConsoleHelper
    {
        // ==================== COLORES Y FORMATO ====================

        public static void Exito(string mensaje)
        {
            Console.ForegroundColor = ConsoleColor.Green;
            Console.WriteLine($"  [✔] {mensaje}");
            Console.ResetColor();
        }

        public static void Error(string mensaje)
        {
            Console.ForegroundColor = ConsoleColor.Red;
            Console.WriteLine($"  [✘] Error: {mensaje}");
            Console.ResetColor();
        }

        public static void Advertencia(string mensaje)
        {
            Console.ForegroundColor = ConsoleColor.Yellow;
            Console.WriteLine($"  [!] Advertencia: {mensaje}");
            Console.ResetColor();
        }

        public static void Info(string mensaje)
        {
            Console.ForegroundColor = ConsoleColor.Cyan;
            Console.WriteLine($"  [→] {mensaje}");
            Console.ResetColor();
        }

        public static void Titulo(string texto)
        {
            Console.ForegroundColor = ConsoleColor.Magenta;
            Console.WriteLine();
            Console.WriteLine($"  ══════════════════════════════════════════");
            Console.WriteLine($"      {texto.ToUpper()}");
            Console.WriteLine($"  ══════════════════════════════════════════");
            Console.ResetColor();
        }

        public static void SubTitulo(string texto)
        {
            Console.ForegroundColor = ConsoleColor.Cyan;
            Console.WriteLine($"\n  ─── {texto} ───");
            Console.ResetColor();
        }

        public static void Separador()
        {
            Console.ForegroundColor = ConsoleColor.DarkGray;
            Console.WriteLine("  ──────────────────────────────────────────");
            Console.ResetColor();
        }

        public static void SeparadorDoble()
        {
            Console.ForegroundColor = ConsoleColor.DarkGray;
            Console.WriteLine("  ==========================================");
            Console.ResetColor();
        }

        public static void Texto(string mensaje)
        {
            Console.ForegroundColor = ConsoleColor.White;
            Console.WriteLine($"  {mensaje}");
            Console.ResetColor();
        }

        public static void TextoResaltado(string etiqueta, string valor)
        {
            Console.ForegroundColor = ConsoleColor.DarkCyan;
            Console.Write($"  {etiqueta}: ");
            Console.ForegroundColor = ConsoleColor.White;
            Console.WriteLine(valor);
            Console.ResetColor();
        }

        // ==================== ENCABEZADO DEL BANCO ====================

        public static void MostrarEncabezadoBanco()
        {
            Console.Clear();
            Console.ForegroundColor = ConsoleColor.Cyan;
            Console.WriteLine();
            Console.WriteLine("  ╔══════════════════════════════════════════╗");
            Console.WriteLine("  ║                                          ║");
            Console.WriteLine("  ║       BANCO ESTRUCTURAS S.A.             ║");
            Console.WriteLine("  ║    Sistema de Gestión Bancaria v1.0      ║");
            Console.WriteLine("  ║                                          ║");
            Console.WriteLine("  ╚══════════════════════════════════════════╝");
            Console.ResetColor();
            Console.WriteLine();
        }

        // ==================== ENTRADA DE DATOS ====================

        public static string PedirTexto(string etiqueta)
        {
            Console.ForegroundColor = ConsoleColor.Yellow;
            Console.Write($"  ► {etiqueta}: ");
            Console.ForegroundColor = ConsoleColor.White;
            string entrada = Console.ReadLine();
            Console.ResetColor();
            return entrada?.Trim() ?? string.Empty;
        }

        public static bool PedirDecimal(string etiqueta, out decimal valor)
        {
            Console.ForegroundColor = ConsoleColor.Yellow;
            Console.Write($"  ► {etiqueta}: ");
            Console.ForegroundColor = ConsoleColor.White;
            string entrada = Console.ReadLine();
            Console.ResetColor();
            return decimal.TryParse(entrada, out valor);
        }

        public static bool PedirEntero(string etiqueta, out int valor)
        {
            Console.ForegroundColor = ConsoleColor.Yellow;
            Console.Write($"  ► {etiqueta}: ");
            Console.ForegroundColor = ConsoleColor.White;
            string entrada = Console.ReadLine();
            Console.ResetColor();
            return int.TryParse(entrada, out valor);
        }

        // ==================== TABLA DE CLIENTES ====================

        public static void MostrarEncabezadoTablaClientes()
        {
            Console.ForegroundColor = ConsoleColor.DarkCyan;
            Console.WriteLine();
            Console.WriteLine($"  {"#",-4} {"IDENTIFICACIÓN",-15} {"NOMBRE",-25} {"CUENTA",-15} {"SALDO",12}");
            Console.WriteLine($"  {"────",-4} {"──────────────",-15} {"────────────────────────",-25} {"────────────",-15} {"────────────",12}");
            Console.ResetColor();
        }

        public static void MostrarFilaCliente(int numero, BancoSimulador.Entidades.Cliente cliente)
        {
            Console.ForegroundColor = ConsoleColor.White;
            Console.WriteLine($"  {numero,-4} {cliente.Identificacion,-15} {cliente.NombreCompleto,-25} {cliente.NumeroCuenta,-15} {cliente.Saldo,12:C2}");
            Console.ResetColor();
        }

        // ==================== PAUSAS ====================

        public static void EsperarTecla(string mensaje = "Presione cualquier tecla para continuar...")
        {
            Console.ForegroundColor = ConsoleColor.DarkGray;
            Console.WriteLine($"\n  {mensaje}");
            Console.ResetColor();
            Console.ReadKey(true);
        }

        public static void LineasEnBlanco(int cantidad = 1)
        {
            for (int i = 0; i < cantidad; i++)
                Console.WriteLine();
        }
    }
}
