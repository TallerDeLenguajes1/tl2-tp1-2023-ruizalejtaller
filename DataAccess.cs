namespace EspCadeteria;
using System.IO;
using System.Text.Json;

public abstract class DataAccess
{
    public abstract Cadeteria GetCadeteria(string file);
    public abstract List<Cadete> GetCadetes(string file);
}

public class DataCSV : DataAccess
{
    public override Cadeteria GetCadeteria(string file)
    {
        string linea, nombre="", telefono="";

        try
        {
            using StreamReader archivo = new(file);

            archivo.ReadLine();
            linea = archivo.ReadLine();

            string[] fila = linea.Split(';');
            nombre = fila[0];
            telefono = fila[1];

            archivo.Close();
        }
        catch (Exception e)
        {
            Console.WriteLine(e.Message);
        }


        var cadeteria = new Cadeteria(nombre, telefono);

        return cadeteria;
    }

    public override List<Cadete> GetCadetes(string file)
    {
        string linea;
        var ListaCadetes = new List<Cadete>();

        try
        {
            using StreamReader archivo = new(file);

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
        catch (Exception e)
        {
            Console.WriteLine(e.Message);
        }

        return ListaCadetes;
    }
}

public class DataJson : DataAccess
{
    public override Cadeteria GetCadeteria(string file)
    {
        Cadeteria cadeteria = null;

        try
        {
            using StreamReader archivo = new(file);

            string objson = archivo.ReadToEnd();
            cadeteria = JsonSerializer.Deserialize<Cadeteria>(objson);
            
            archivo.Close();
        }
        catch (Exception e)
        {
            Console.WriteLine(e.Message);
        }

        return cadeteria;
    }

    public override List<Cadete> GetCadetes(string file)
    {
        var LCadetes = new List<Cadete>();
        try
        {
            using StreamReader archivo = new(file);

            string objson = archivo.ReadToEnd();
            LCadetes = JsonSerializer.Deserialize<List<Cadete>>(objson);

            archivo.Close();

        }
        catch (Exception e)
        {
            Console.WriteLine(e.Message);
        }

        return LCadetes;
    }
}