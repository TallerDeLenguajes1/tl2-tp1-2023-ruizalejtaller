namespace Cadeteria;
using System.IO;

public static class DataAccess
{
    public static Cadeteria GetCadeteria(string file)
    {
        string linea;
        var cadeteria = new Cadeteria();

        try
        {
            using(StreamReader archivo = new StreamReader(file))
            {

                archivo.ReadLine();
                linea = archivo.ReadLine();

                string[] fila = linea.Split(';');
                string nombre = fila[0];
                string telefono = fila[1];

                cadeteria.Nombre = nombre;
                cadeteria.Telefono = telefono;

                archivo.Close();

            }
        }
        catch (Exception e)
        {
            Console.WriteLine(e.Message);
        }

        return cadeteria;
    }

    public static List<Cadete> GetCadetes(string file)
    {
        string linea;
        var ListaCadetes = new List<Cadete>();

        try
        {
            using(StreamReader archivo = new StreamReader(file))
            {

                archivo.ReadLine();

                while ((linea = archivo.ReadLine()) != null)
            {
                string[] fila = linea.Split(';');
                int id = Convert.ToInt32(fila[0]);
                string nombre = fila[1];
                string direccion = fila[2];
                string telefono = fila[3];

                var cadete = new Cadete(id, nombre, direccion, telefono);

                ListaCadetes.Add(cadete);

            }
                archivo.Close();

            }
        }
        catch (Exception e)
        {
            Console.WriteLine(e.Message);
        }

        return ListaCadetes;
    }
}