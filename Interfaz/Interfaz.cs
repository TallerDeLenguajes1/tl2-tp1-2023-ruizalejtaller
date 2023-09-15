namespace EspCadeteria;

public class Interfaz
{
    public static void Menu (Cadeteria Cad)
    {
        bool flag = true;
        string str;
        while (flag)
        {
            Console.Clear();
            Console.WriteLine("Cadeteria: " + Cad.MostrarNombreCadeteria());
            Console.WriteLine("-----------------------------");
            Console.WriteLine("\n--- Pedidos ---");
            Console.WriteLine("1. Dar de alta");
            Console.WriteLine("2. Mostrar todos");
            Console.WriteLine("3. Cambiar estado");
            Console.WriteLine("4. Reasignar a otro cadete");
            Console.WriteLine("5. Finalizar\n");

            str = Console.ReadLine();

            if(int.TryParse(str, out int op))
            {
                switch(op)
                {
                    case 1:DarAlta(Cad);
                        break;
                    case 2:MostrarTLPedidos(Cad);
                        break;
                    case 3:CambiarEstado(Cad);
                        break;
                    case 4:ReasignarCadete(Cad);
                        break;
                    case 5:Informe(Cad);
                        flag = false;
                        break;
                }
            }
        }
    }

    public static void DarAlta(Cadeteria Cad)
    {
        string obs="", nombre="", direccion="", telefono="", referencia="";
        Cad.IncNroPed();
        int IdCad = 0;

        Console.WriteLine("\nPedido num: " + Cad.NroCantPedidos());

        while(obs == "")
        {
            Console.WriteLine("Obs: ");
            obs = Console.ReadLine();
        }

        Console.WriteLine("\n--- Datos del cliente (* campos obligatorios) ---");
        while(nombre == "")
        {
            Console.WriteLine("* Nombre: ");
            nombre = Console.ReadLine();
        }

        while(direccion =="")
        {
            Console.WriteLine("* Dirección: ");
            direccion = Console.ReadLine();
        }


        Console.WriteLine("Telefono: ");
        telefono = Console.ReadLine();

        Console.WriteLine("Datos de referencia: ");
        referencia = Console.ReadLine();

        Cad.AltaPedido(nombre, direccion, telefono, referencia, Cad.NroCantPedidos(), obs);
    

        while(IdCad == 0)
        {
            IdCad = ElegirCadete(Cad);
        }

        if(Cad.AsignarCadeteAPedido(Cad.NroCantPedidos(), IdCad))
        {
            Console.WriteLine("\nPedido agregado con éxito");
        } else
        {
            Console.WriteLine("\nNo se pudo agregar el pedido");
        }

        Console.WriteLine("\nPulse una tecla para continuar");
        Console.ReadKey();
    }

     public static int ElegirCadete(Cadeteria Cad)
    {
        string str;

        Console.WriteLine ("\nAsignar al cadete: ");

        foreach(var cad in Cad.ListaCadetes())
        {
            Console.WriteLine(cad.Id + " " + cad.Nombre);
        }

        str = Console.ReadLine();

        if(int.TryParse(str, out int id))
        {
            if(id > 0 && id <= Cad.ListaCadetes().Count())
            {
                return id;
            } else return 0;
        } else return 0;
    }

    public static void MostrarTLPedidos(Cadeteria Cad)
    {
        Console.WriteLine("--- Pedidos pendientes ---");
        MostrarPedidos(Cad, Estados.Pendiente);

        Console.WriteLine("\n--- Pedidos entregados ---");
        MostrarPedidos(Cad, Estados.Entregado);

        Console.WriteLine("\n--- Pedidos cancelados ---");
        MostrarPedidos(Cad, Estados.Cancelado);

        Console.WriteLine("\nPulse una tecla para continuar");
        Console.ReadKey();
    }

        public static void MostrarPedidos(Cadeteria Cad, Estados estado)
    {

            foreach(var ped in Cad.ListaPedidos())
            {
                if(ped.Estado == estado)
                {
                    Console.WriteLine(ped.Nro + ". " + ped.Obs + " - Cliente: " + ped.Cliente.Nombre + " / Dirección: " + ped.Cliente.Direccion + " -- Cadete: " + ped.Cadete.Nombre);
                }
            }

    }

    public static void CambiarEstado(Cadeteria Cad)
    {
        string str;
        int numP = MostrarPendientes(Cad, "cambiar su estado: ");

        Console.WriteLine("\n1. Entregado");
        Console.WriteLine("2. Cancelado");
        Console.WriteLine("3. Sigue pendiente");

        str = Console.ReadLine();
        if (int.TryParse(str, out int op))
        {
            switch(op)
            {
                case 1:
                    Cad.CambiarEst(numP, Estados.Entregado);
                    break;
                
                case 2:
                    Cad.CambiarEst(numP, Estados.Cancelado);
                    break;
            }
        }
    }

    public static void ReasignarCadete(Cadeteria Cad)
    {
        int numP = MostrarPendientes(Cad, "reasignar cadete: ");
        int IdCad = 0;

        while(IdCad == 0)
        {
            IdCad = ElegirCadete(Cad);
        }

        if(Cad.ReasignarPedidoACadete(numP, IdCad))
        {
            Console.WriteLine("\nPedido reasignado al cadete");
        } else
        {
            Console.WriteLine("\nNo se pudo reasignar el pedido");
        }

        Console.WriteLine("\nPulse una tecla para continuar");
        Console.ReadKey();
    }

    public static int MostrarPendientes(Cadeteria Cad, string text)
    {
        string str;
        
        Console.WriteLine("--- Pedidos pendientes ---");
        MostrarPedidos(Cad, Estados.Pendiente);

        Console.WriteLine("\nElija un Pedido para " + text);
        str = Console.ReadLine();

        if (int.TryParse(str, out int numP))
        {
            if(numP <= Cad.NroCantPedidos())
            {
                MostrarPedido(Cad, numP);
            }
        }

        return numP;
    }

    public static void MostrarPedido(Cadeteria Cad, int num)
    {
        foreach(var ped in Cad.ListaPedidos())
        {
            if(ped.Nro == num)
            {
                Console.Clear();
                Console.WriteLine("Obs: " + ped.Obs);
                Console.WriteLine("Estado: " + ped.Estado);
                VerDatosCliente(ped.Cliente);
                Console.WriteLine("Cadete: " + ped.Cadete.Nombre);
            }
        }
    }

    public static void VerDatosCliente(Cliente Cli)
    {
        Console.WriteLine("Cliente: " + Cli.Nombre);
        Console.WriteLine("Direccion: " + Cli.Direccion);
        Console.WriteLine("Telefono: " + Cli. Telefono);
        Console.WriteLine("Datos referencia direccion: " + Cli.DatosReferenciaDireccion);
    }

    public static void Informe(Cadeteria Cad)
    {
        int cantTotal = 0;
        float montoTotal = 0;
        Console.Clear();
        foreach(var cad in Cad.ListaCadetes())
        {
            Console.WriteLine($"Cadete: {cad.Nombre} -- Cant envíos: {Cad.PedidosEntregados(cad)} -- Monto ganado: {Cad.JornalACobrar(cad)}");
            cantTotal += Cad.PedidosEntregados(cad);
            montoTotal += Cad.JornalACobrar(cad);
        }

        Console.WriteLine ("\nTotal de envíos: " + cantTotal);
        Console.WriteLine("Monto total: " + montoTotal);
        Console.WriteLine("Envíos promedio por cadete: " + cantTotal/Cad.ListaCadetes().Count());
    }

}
public static class AccesoADatos
{
    public static Cadeteria ElegirSource()
    {
        bool flag = true;
        string str;

        Cadeteria Cadeteria = null;

        while(flag)
        {
            Console.Clear();
            Console.WriteLine("Elija la fuente de datos: ");
            Console.WriteLine("1. CSV");
            Console.WriteLine("2. JSON");

            str = Console.ReadLine();

            if(int.TryParse(str, out int op))
            {
                switch(op)
                {
                    case 1:
                        Cadeteria = DataAccess.CargarCSV();
                        flag = false;
                        break;

                    case 2:
                        Cadeteria = DataAccess.CargarJson();
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
}
