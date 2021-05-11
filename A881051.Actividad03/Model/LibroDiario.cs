using System.ComponentModel;
using System;
using System.Collections.Generic;
using A881051.Actividad03.Helper;
using A881051.Actividad03.Model;
using System.Linq;

namespace A881051.Actividad03.Model
{
    public class LibroDiario
    {
        private PlanCuentas _planCuentas = new PlanCuentas();
        private List<AsientoContable> _asientos = new List<AsientoContable> {};
        private int _ultimoNumeroAsiento = 0;
        private string _nombreArchivoSalida = "Diario.txt";

        public LibroDiario() 
        {
            Document.CreateTextFileIfNotExists(this._nombreArchivoSalida); 
            this.CargarAsientosDesdeArchivo();
        }

        public int ProximoNumeroAsiento
        {
            get => this._ultimoNumeroAsiento + 1;
        }

        public List<AsientoContable> Asientos
        {
            get => this._asientos;
        }

        public PlanCuentas PlanCuentas
        {
            get => this._planCuentas;
        }

        public void agregarAsientoContable(AsientoContable asiento)
        {
            if (asiento.balance() == 0)
            {
                this._asientos.Add(asiento);
                this._ultimoNumeroAsiento++;
                this.guardarAsiento(asiento);
            }
        }

        public bool existeAsientoContable(int nroAsiento)
        {
            return this.buscarAsientoContable(nroAsiento)  != null;
        }

        public AsientoContable buscarAsientoContable(int nroAsiento)
        {
            return this._asientos.FirstOrDefault(
                asiento => asiento.NroAsiento == nroAsiento
            );
        }

        public string generarLibroDiario()
        {
            string reporte = "Asiento|Fecha|CodigoCuenta|Debe|Haber\n";

            Console.WriteLine(this._asientos.Count);
            

            foreach (AsientoContable asiento in this._asientos)
            {
                foreach (LineaAsientoContable linea in asiento.Lineas)
                {
                    reporte += string.Format(
                        "{0}|{1}|{2}|{3}|{4} \n",
                        asiento.NroAsiento,
                        asiento.Fecha,
                        linea.Cuenta.Codigo,
                        linea.Columna == TipoMovimiento.Debe ? linea.Importe : "  ",
                        linea.Columna == TipoMovimiento.Haber ? linea.Importe : "  "
                    );
                }
                reporte += "-------------------\n";
            }

            return reporte;
        }

        private void guardarAsiento(AsientoContable asiento)
        {
            foreach (LineaAsientoContable linea in asiento.Lineas)
            {
                Document.AppendTextToFile(
                    this._nombreArchivoSalida, 
                    string.Format(
                        "\n{0}|{1}|{2}|{3}|{4}",
                        asiento.NroAsiento,
                        asiento.Fecha,
                        linea.Cuenta.Codigo,
                        linea.Columna == TipoMovimiento.Debe ? linea.Importe : "",
                        linea.Columna == TipoMovimiento.Haber ? linea.Importe : ""
                    )
                );
            }
        }

        public void CargarAsientosDesdeArchivo()
        {        
            string asientosTxt = Document.ReadTextFromFile(this._nombreArchivoSalida);         

            string[] lines = asientosTxt.Split("\n");

            Dictionary<int, AsientoContable> asientos = new Dictionary<int, AsientoContable>();

            for (var i = 1; i < lines.Length; i++)
            {
                string[] line = lines[i].Split("|");

                if (!asientos.ContainsKey(Int32.Parse(line[0])))
                {
                    asientos.Add(
                        Int32.Parse(line[0]), 
                        new AsientoContable(Int32.Parse(line[0]), (string) line[1])
                    );
                }

                asientos[Int32.Parse(line[0])].agregarLinea(new LineaAsientoContable(
                    this._planCuentas.buscarCuenta(Int32.Parse(line[2])), 
                    (string) line[3] != "" ? Int32.Parse(line[3]) : Int32.Parse(line[4]), 
                    (string) line[3] != "" ? TipoMovimiento.Debe : TipoMovimiento.Haber
                ));

                if(i == (lines.Length - 1)) {
                    this._ultimoNumeroAsiento = Int32.Parse(line[0]);
                }           
            }

            this._asientos = asientos.Values.ToList();
        }
    }
}
