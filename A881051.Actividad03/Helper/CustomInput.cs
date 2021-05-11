using A827141.Actividad03.Model;
using System.Collections.Generic;
using System;
using System.Linq;

namespace A827141.Actividad03.Helper
{
    public class CustomInput
    {
        public static Cuenta IngresoCuentaContable(PlanCuentas planCuentas)
        {
            Cuenta cuenta;

            do
            {
                int codigoCuenta = Input.IngresoNumeroPositivo("Ingrese un código de la cuenta contable válido");
                cuenta = planCuentas.buscarCuenta(codigoCuenta);
            } while (!planCuentas.existeCuenta(cuenta));

            return cuenta;
        }

        public static TipoMovimiento IngresoTipoColumna()
        {
            return Input.IngresoVerdaderoFalso("¿Columna del debe?") ? TipoMovimiento.Debe : TipoMovimiento.Haber;
        }

        public static AsientoContable IngresoLineasAsiento(AsientoContable asientoContable, PlanCuentas planCuentas)
        {
            Console.WriteLine("\tIngrese las lineas del asiento");

            bool salida = true;

            do
            {
                Cuenta cuenta = CustomInput.IngresoCuentaContable(planCuentas);

                TipoMovimiento columna = CustomInput.IngresoTipoColumna();

                int importe = Input.IngresoNumeroPositivo("Ingrese el monto");

                salida = Input.IngresoVerdaderoFalso("¿Desea continuar la carga de lineas?");
                
                asientoContable.agregarLinea(
                    new LineaAsientoContable(cuenta, importe, columna)
                );

                if (
                    asientoContable.balance() != 0
                    && !salida
                ) {
                    Input.WriteYellowLine($"ATENCION");

                    Input.WriteYellowLine($"El asiento es inconsistente, el balance actual es de {asientoContable.balance()}.");
                    
                    Input.WriteYellowLine($"Si no agrega otra linea el asiento será descartado.");
                    
                    salida = Input.IngresoVerdaderoFalso("¿Desea continuar la carga de lineas?");
                }
            } while (salida);

            return asientoContable;
        }

        public static LibroDiario IngresoAsientosContables(
            LibroDiario libroDiario,
            PlanCuentas planCuentas,
            string Mensaje = "Ingrese asiento contable"
        ) {
            Console.WriteLine("\n");
            
            bool salida = false;

            do
            {
                Console.WriteLine("\t" + Mensaje);

                int nroAsiento = libroDiario.ProximoNumeroAsiento;

                string fecha = Input.IngresoFecha("Ingrese la fecha del asiento a cargar.");

                AsientoContable asientoContable = new AsientoContable(nroAsiento, fecha);

                CustomInput.IngresoLineasAsiento(asientoContable, planCuentas);

                salida = Input.IngresoVerdaderoFalso("¿Desea ingresar otro asiento?");
                
                if (asientoContable.balance() == 0) {
                    libroDiario.agregarAsientoContable(asientoContable);
                }
            } while (salida);

            return libroDiario;
        }
    }
}