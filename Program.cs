using BancoSimulador.Logica;
using BancoSimulador.UI;

namespace BancoSimulador
{
    /// <summary>
    /// Punto de entrada del sistema bancario.
    /// Instancia el banco y lanza el menú principal.
    
    class Program
    {
        static void Main(string[] args)
        {
            // Configurar la consola para soporte de caracteres especiales
            System.Console.OutputEncoding = System.Text.Encoding.UTF8;
            System.Console.Title = "Banco Estructuras S.A.";

            // Crear el banco (contiene las tres estructuras de datos)
            Banco banco = new Banco();

            // Lanzar el menú principal
            Menu menu = new Menu(banco);
            menu.MostrarMenuPrincipal();
        }


    }
}
