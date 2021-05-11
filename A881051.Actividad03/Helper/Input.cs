using System.Collections.Generic;
using System;
using System.Text.RegularExpressions;

namespace A881051.Actividad03.Helper
{
    public class Input
    {
        public static string IngresoTexto(string mensaje = "Ingrese un texto")
        {
            string texto;
            do
            {
                Input.WriteYellowLine(mensaje);
                texto = Console.ReadLine();
            } while (texto == "");

            return texto;
        }

        public static string IngresoFecha(string mensaje = "Ingrese una fecha válida.")
        {
            string texto;

            do {
                Input.WriteYellowLine(mensaje + " (Formato dd-mm-aaaa)");
                texto = Console.ReadLine();
            } while (!Regex.IsMatch(texto, @"^\d\d-\d\d-\d\d\d\d$"));

            return texto;
        }

        public static int IngresoNumeroPositivo(string Mensaje = "Ingrese un numero", int max = 1000)
        {
            int numero;
            bool continuar;
            do
            {
                Console.WriteLine(Mensaje);
                continuar = !int.TryParse(Console.ReadLine(), out numero) || numero < 0 || numero > max;
                if (continuar)
                {
                    Console.WriteLine("Debe ingresar un numero positivo entre 0 y {0}", max);
                }
            } while (continuar);

            return numero;
        }

        public static void WriteGreenLine(string value, bool extraLine = true)
        {           
            Console.BackgroundColor = ConsoleColor.Green;
            Console.ForegroundColor = ConsoleColor.Black;
            
            Console.WriteLine("".PadRight(Console.WindowWidth - 1));
            Console.WriteLine(value.PadRight(Console.WindowWidth - 1));
            Console.WriteLine("".PadRight(Console.WindowWidth - 1));

            Console.ResetColor();

            if (extraLine)
            {
                Console.WriteLine("");
            }
        }

        public static void WriteRedLine(string Mensaje)
        {
            Console.ForegroundColor = ConsoleColor.Red;
            Console.WriteLine(Mensaje);
            Console.ResetColor();
        }

        public static void WriteYellowLine(string Mensaje)
        {
            Console.ForegroundColor = ConsoleColor.Yellow;
            Console.WriteLine(Mensaje);
            Console.ResetColor();
        }

        public static void PresionaUnaTeclaParaContinuar(string mensaje = "\n\nPresiona una tecla para continuar")
        {
            Console.ForegroundColor = ConsoleColor.DarkYellow;
            
            Console.WriteLine(mensaje);

            Console.ResetColor();

            Console.ReadKey();
        }

        public static bool IngresoVerdaderoFalso(string Mensaje = "")
        {
            bool valor;
            bool continuar;
            string helpText = "Ingrese \"Si\" o \"S\" para Si o \"No\" o \"N\" para No";
            do
            {
                Console.WriteLine(Mensaje + "\n" + helpText);
                continuar = !bool.TryParse(
                    Console.ReadLine()
                            .ToLower()
                            .Replace("si", "true")
                            .Replace("s", "true")
                            .Replace("no", "false")
                            .Replace("n", "false")
                            .Replace("0", "false")
                            .Replace("1", "true"), 
                    out valor);
                if (continuar)
                {
                    Console.WriteLine(Mensaje + helpText);
                }
            } while (continuar);

            return valor;
        }

        public static void Exit(string NombreUsuario)
        {
            Console.Clear();

            Console.WriteLine("\n\n\tHasta pronto {0}!\n", NombreUsuario);

            System.Environment.Exit(0);
        }
    }
}
