namespace EspCadeteria;

public static class AccesoADatos
{
    public static Cadeteria ElegirSource()
    {
        bool flag = true;
        string str;

        Cadeteria Cadeteria = null;

        while(flag)
        {
            Console.WriteLine("Elija la fuente de datos: ");
            Console.WriteLine("1. CSV");
            Console.WriteLine("2. JSON");

            str = Console.ReadLine();

            if(int.TryParse(str, out int op))
            {
                switch(op)
                {
                    case 1:
                        Cadeteria = CargarCSV();
                        flag = false;
                        break;

                    case 2:
                        Cadeteria = CargarJson();
                        flag = false;
                        break;

                    default:
                        Console.WriteLine("Ingrese una opción válida");
                        break;
                }
            }
        }

        return Cadeteria;
    }

    public static Cadeteria CargarCSV()
    {
        DataAccess DataCadeteria = new DataCSV();
        var Cadeteria = DataCadeteria.GetCadeteria("Cadeteria.csv");
        Cadeteria.LCadetes = DataCadeteria.GetCadetes("Cadetes.csv");
        return Cadeteria;
    }

    public static Cadeteria CargarJson()
    {
        DataAccess DataCadeteria = new DataJson();
        var Cadeteria = DataCadeteria.GetCadeteria("Cadeteria.json");
        Cadeteria.LCadetes = DataCadeteria.GetCadetes("Cadetes.json");
        return Cadeteria;
    }
}