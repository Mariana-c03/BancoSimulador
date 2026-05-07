using System;

namespace BancoSimulador.Entidades
{
    /// <summary>
    /// Tipos de transacción que pueden realizarse en el banco.
    /// </summary>
    public enum TipoTransaccion
    {
        Deposito,
        Retiro
    }

    /// <summary>
    /// Representa una transacción bancaria registrada en la pila de historial.
    /// </summary>
    public class Transaccion
    {
        public TipoTransaccion Tipo { get; set; }
        public string NumeroCuenta { get; set; }
        public decimal Monto { get; set; }
        public decimal SaldoAnterior { get; set; }
        public decimal SaldoPosterior { get; set; }
        public DateTime FechaHora { get; set; }

        public Transaccion(TipoTransaccion tipo, string numeroCuenta, decimal monto, decimal saldoAnterior, decimal saldoPosterior)
        {
            Tipo = tipo;
            NumeroCuenta = numeroCuenta;
            Monto = monto;
            SaldoAnterior = saldoAnterior;
            SaldoPosterior = saldoPosterior;
            FechaHora = DateTime.Now;
        }

        public override string ToString()
        {
            string tipoStr = Tipo == TipoTransaccion.Deposito ? "DEPÓSITO" : "RETIRO";
            return $"[{tipoStr}] Cuenta: {NumeroCuenta} | Monto: {Monto:C2} | " +
                   $"Saldo anterior: {SaldoAnterior:C2} → Saldo posterior: {SaldoPosterior:C2} | " +
                   $"Fecha: {FechaHora:dd/MM/yyyy HH:mm:ss}";
        }
    }
}
