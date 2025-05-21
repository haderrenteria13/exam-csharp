using System;

namespace Examen2
{
    internal class Program
    {
        static void Main(string[] args)
        {
            Random random = new Random();
            int idSimulacion = random.Next(100000, 999999);
            const int SS = 50;
            const int diasAlAno = 365;
            int contadorSimulaciones = 0;

            string nombre = GetInputText("Ingrese su nombre para continuar a la simulacion: ");
            Console.WriteLine($"Bienvenido {nombre} Hoy es : " + DateTime.Now.ToString("yyyy/MM/dd"));
            Console.WriteLine($"Numero de simulacion {idSimulacion}");

            int D = GetInput("Digite la demanda anual en unidades: ");
            double CP = GetInputDouble("Digite el costo de emitir un pedido: ");
            double CAU = GetInputDouble("Digite el costo de almacenaje por unidad: ");

            Console.WriteLine("Por favor seleccione una opción");
            Console.WriteLine("1. Lote óptimo de pedidos");
            Console.WriteLine("2. Costo total de emisión");
            Console.WriteLine("3. Costo de almacenaje anual");
            Console.WriteLine("4. Costo total de gestión de Stock");
            Console.WriteLine("5. El tiempo de reaprovisionamiento (en días)");
            Console.WriteLine("6. Número de pedidos al año");
            Console.WriteLine("7. Nro. Simulaciones realizadas");
            Console.WriteLine("8. Salir");

            while (true)
            {
                int opcion = GetInput("Ingrese una opcion: ");

                if (opcion == 8)
                {
                    Console.WriteLine("Desea continuar o salir de la simulacion (S/N)");
                    string decision = Console.ReadLine().ToUpper();
                    if (decision == "S")
                    {
                        Console.WriteLine("Saliste del sistema");
                        break;
                    }
                    continue;
                }

                double Q = Math.Sqrt((2 * D * CP) / CAU);

                switch (opcion)
                {
                    case 1:
                        contadorSimulaciones++;
                        Console.WriteLine($"Lote óptimo de pedidos (Q*): {Q}");
                        break;
                    case 2:
                        contadorSimulaciones++;
                        double costoEmision = CP * (D / Q);
                        Console.WriteLine($"Costo total de emisión: {costoEmision}");
                        break;
                    case 3:
                        contadorSimulaciones++;
                        double costoAlmacenaje = CAU * (Q / 2 + SS);
                        Console.WriteLine($"Costo de almacenaje anual: {costoAlmacenaje}");
                        break;
                    case 4:
                        double costoGestion = CP * (D / Q) + CAU * (Q / 2 + SS);
                        Console.WriteLine($"Costo total de gestión de Stock: {costoGestion}");
                        break;
                    case 5:
                        contadorSimulaciones++;
                        double tiempoReaprovisionamiento = diasAlAno / (D / Q);
                        Console.WriteLine($"El tiempo de reaprovisionamiento (en días): {tiempoReaprovisionamiento}");
                        break;
                    case 6:
                        contadorSimulaciones++;
                        double numeroPedidos = D / Q;
                        Console.WriteLine($"Número de pedidos al año: {numeroPedidos}");
                        break;
                    case 7:
                        Console.WriteLine($"Nro. veces que se ha consultado la simulación: {contadorSimulaciones}");
                        Console.WriteLine($"Id de la simulación: {idSimulacion}");
                        break;
                    default:
                        Console.WriteLine("Opción no válida, por favor intente nuevamente.");
                        break;
                }

                AlertForOrderSize(Q);
            }
        }

        static string GetInputText(string message)
        {
            string value;
            do
            {
                Console.WriteLine(message);
                value = Console.ReadLine()?.Trim();
                if (string.IsNullOrWhiteSpace(value))
                {
                    Console.WriteLine("Entrada no válida. Intente de nuevo.");
                }
            } while (string.IsNullOrWhiteSpace(value));
            return value;
        }

        static int GetInput(string message)
        {
            int value;
            while (true)
            {
                Console.WriteLine(message);
                if (int.TryParse(Console.ReadLine(), out value) && value > 0)
                {
                    return value;
                }
                Console.WriteLine("Entrada no válida, por favor intente nuevamente.");
            }
        }

        static double GetInputDouble(string message)
        {
            double value;
            while (true)
            {
                Console.WriteLine(message);
                if (double.TryParse(Console.ReadLine(), out value) && value > 0)
                {
                    return value;
                }
                Console.WriteLine("Entrada no válida, por favor intente nuevamente.");
            }
        }

        static void AlertForOrderSize(double Q)
        {
            if (Q < 100)
            {
                Console.WriteLine("Alerta: El pedido es pequeño.");
            }
            else if (Q >= 1001 && Q <= 2000)
            {
                Console.WriteLine("Alerta: Pedido promedio.");
            }
            else if (Q > 2000 && Q <= 4000)
            {
                Console.WriteLine("Alerta: Pedido por economía de escala.");
            }
            else if (Q > 4000)
            {
                Console.WriteLine("Alerta: Sobre Stock.");
            }
        }
    }
}
