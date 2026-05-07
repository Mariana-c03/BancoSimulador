namespace BancoSimulador.Entidades
{
    /// <summary>
    /// Representa un cliente del banco con su información personal y de cuenta.
    /// </summary>
    public class Cliente
    {
        public string Identificacion { get; set; }
        public string NombreCompleto { get; set; }
        public string NumeroCuenta { get; set; }
        public decimal Saldo { get; set; }

        public Cliente(string identificacion, string nombreCompleto, string numeroCuenta, decimal saldo)
        {
            Identificacion = identificacion;
            NombreCompleto = nombreCompleto;
            NumeroCuenta = numeroCuenta;
            Saldo = saldo;
        }

        public override string ToString()
        {
            return $"ID: {Identificacion} | Nombre: {NombreCompleto} | Cuenta: {NumeroCuenta} | Saldo: {Saldo:C2}";
        }
    }
}
