namespace Cadeteria;

public class Pedido
{
    private int nro;
    private string obs;
    private Cliente cliente;
    private Estados estado;

    public Pedido(int nro, string obs, Cliente cliente, Estados estado)
    {
        this.nro = nro;
        this.obs = obs;
        this.cliente = cliente;
        this.estado = estado;
    }

    public int Nro { get => nro; set => nro = value; }
    public string Obs { get => obs; set => obs = value; }
    public Cliente Cliente { get => cliente; set => cliente = value; }
    public Estados Estado { get => estado; set => estado = value; }

    public void VerDatosCliente()
    {
        Console.WriteLine("Cliente: " + Cliente.Nombre);
        Console.WriteLine("Direccion: " + Cliente.Direccion);
        Console.WriteLine("Telefono: " + Cliente. Telefono);
        Console.WriteLine("Datos referencia direccion: " + Cliente.DatosReferenciaDireccion);
    }
}

public class Cliente
{
    private string nombre;
    private string direccion;
    private string telefono;
    private string datosreferenciadireccion;

    public Cliente(string nombre, string direccion, string telefono, string datosreferenciadireccion)
    {
        this.nombre = nombre;
        this.direccion = direccion;
        this.telefono = telefono;
        this.datosreferenciadireccion = datosreferenciadireccion;
    }

    public string Nombre { get => nombre; set => nombre = value; }
    public string Direccion { get => direccion; set => direccion = value; }
    public string Telefono { get => telefono; set => telefono = value; }
    public string DatosReferenciaDireccion { get => datosreferenciadireccion; set => datosreferenciadireccion = value; }
}