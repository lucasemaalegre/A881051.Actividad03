namespace A827141.Actividad03.Model
{
    public class LineaAsientoContable
    {
        private Cuenta _cuenta;
        private int _importe;
        private TipoMovimiento _columna;

        public LineaAsientoContable(
            Cuenta cuenta, int importe, TipoMovimiento columna) 
        {
            this._cuenta = cuenta;
            this._importe = importe;
            this._columna = columna;
        }

        public Cuenta Cuenta
        {
            get => this._cuenta;
        }

        public int Importe
        {
            get => this._importe;
        }

        public TipoMovimiento Columna
        {
            get => this._columna;
        }
    }
}