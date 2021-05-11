using System;
using A881051.Actividad03.Helper;
using A881051.Actividad03.Model;

namespace A881051.Actividad03
{
    class Program
    {
        static void Main(string[] args)
        {
            LibroDiario libroDiario = new LibroDiario();

            CustomInput.IngresoAsientosContables(libroDiario, libroDiario.PlanCuentas);
            
            Console.WriteLine(libroDiario.generarLibroDiario());
        }
    }
}
