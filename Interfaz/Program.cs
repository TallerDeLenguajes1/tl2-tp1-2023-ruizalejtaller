using System.Data;
using EspCadeteria;
internal class Program
{
    private static void Main(string[] args)
    {
        var Cad = AccesoADatos.ElegirSource();

        if (Cad != null)
        {
            Interfaz.Menu(Cad);
        }
    }
}