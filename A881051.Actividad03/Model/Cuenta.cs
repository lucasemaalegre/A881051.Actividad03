namespace A827141.Actividad03.Model
{
    public class Cuenta
    {
        private int _codigo;
        private string _nombre;
        private string _tipo;

        public Cuenta(int codigo, string nombre, string tipo)
        {
            this._codigo = codigo;
            this._nombre = nombre;
            this._tipo = tipo;
        }
        
        public int Codigo
        {
            get => this._codigo;
        }

        public string Nombre
        {
            get => this._nombre;
        }

        public string Tipo
        {
            get => this._tipo;
        }
    }
}