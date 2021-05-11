using System.Collections.Generic;
using System.Linq;

namespace A827141.Actividad03.Model
{
    public class AsientoContable
    {
        private List<LineaAsientoContable> _lineas = new List<LineaAsientoContable> {};
        private int _nroAsiento;
        private string _fecha;

        public AsientoContable(
            int nroAsiento,
            string fecha
        ) {
            this._nroAsiento = nroAsiento;
            this._fecha = fecha;
        }
        
        public int NroAsiento
        {
            get => this._nroAsiento;
        }

        public string Fecha
        {
            get => this._fecha;
        }

        public List<LineaAsientoContable> Lineas
        {
            get => this._lineas;
        }

        public void agregarLinea(LineaAsientoContable linea)
        {
            this._lineas.Add(linea);
        }

        public int totalDebe()
        {
            return this._lineas.Sum(linea => {
                return linea.Columna == TipoMovimiento.Debe ? linea.Importe : 0;
            });
        }

        public int totalHaber()
        {
            return this._lineas.Sum(linea => {
                return linea.Columna == TipoMovimiento.Haber ? linea.Importe : 0;
            });
        }

        public int balance()
        {
            return this.totalDebe() - this.totalHaber();
        }

        public string reporte()
        {
            string reporte = "Debe | Haber\n";

            reporte += string.Format($"{this.totalDebe()} | {this.totalHaber()}\n");
            reporte += string.Format($"--------------------------\n");
            reporte += string.Format($"balance asiento: {this.balance()}");

            return reporte;
        }
    }
}