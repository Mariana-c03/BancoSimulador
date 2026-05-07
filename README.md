# Banco Estructuras S.A. — Simulador Bancario en Consola

Proyecto final de estructuras de datos en C#. Implementa un sistema bancario básico en consola que demuestra el uso real de tres estructuras de datos implementadas manualmente.

---

## Estructuras de datos implementadas

### Lista Enlazada — `ListaEnlazadaClientes.cs`
Gestiona el registro de clientes del banco. Cada nodo (`NodoCliente`) contiene un cliente y apunta al siguiente. Operaciones: insertar, buscar por identificación, buscar por cuenta, contar clientes, calcular total de dinero, validar duplicados.

### Cola FIFO — `ColaAtencion.cs`
Maneja los turnos de atención al cliente. El primero en llegar es el primero en ser atendido. Se mantienen referencias al frente y al final para operaciones O(1). Operaciones: encolar, desencolar, ver siguiente sin remover, recorrer.

### Pila LIFO — `PilaTransacciones.cs`
Registra el historial de transacciones y permite deshacer la última operación. La cima siempre apunta a la transacción más reciente. Operaciones: apilar, desapilar, ver cima sin remover.

---

## Estructura del proyecto

```
BancoSimulador/
├── Entities/
│   ├── Cliente.cs              # Entidad cliente: id, nombre, cuenta, saldo
│   └── Transaccion.cs          # Entidad transacción: tipo, monto, saldos, fecha
├── DataStructures/
│   ├── Nodos.cs                # NodoCliente, NodoCola, NodoPila
│   ├── ListaEnlazadaClientes.cs# Lista enlazada simple manual
│   ├── ColaAtencion.cs         # Cola FIFO manual
│   └── PilaTransacciones.cs    # Pila LIFO manual
├── Services/
│   ├── Banco.cs                # Lógica principal del banco
│   └── ServicioBanco.cs        # Fachada de servicios bancarios
├── UI/
│   ├── Menu.cs                 # Gestión de todos los menús (13 opciones)
│   └── ConsoleHelper.cs        # Utilidades visuales: colores, tablas, íconos
├── Program.cs                  # Punto de entrada
└── BancoSimulador.csproj
```

---

## Menú principal

```
1.  Registrar cliente
2.  Listar todos los clientes
3.  Buscar cliente por identificación
4.  Agregar cliente a cola de atención
5.  Atender siguiente cliente
6.  Realizar depósito
7.  Realizar retiro
8.  Consultar saldo
9.  Deshacer última transacción
10. Mostrar cola de atención
11. Mostrar total de clientes
12. Mostrar total de dinero del banco
13. Salir
```

---

## Cómo ejecutar

```bash
cd BancoSimulador
dotnet run
```

Requiere .NET 8 SDK o superior.

---

## Validaciones implementadas

- Identificaciones y números de cuenta únicos (no duplicados)
- Nombres no vacíos ni en blanco
- Saldo inicial no negativo
- Depósitos y retiros mayores a cero
- No se permite retirar más del saldo disponible
- Entradas numéricas validadas con `TryParse`
- El sistema no se cierra por errores del usuario

---

## Tecnologías

- Lenguaje: C#
- Framework: .NET 8
- Tipo: Aplicación de consola
- Sin colecciones genéricas del framework (.NET): todas las estructuras son implementación propia
