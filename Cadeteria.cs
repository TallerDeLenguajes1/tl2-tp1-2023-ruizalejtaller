namespace Cadeteria;

public class Cadeteria
{
    private string nombre;
    private string telefono;
    private int cantPedidos;
    private List<Cadete> lcadetes;

    public string Nombre { get => nombre; set => nombre = value; }
    public string Telefono { get => telefono; set => telefono = value; }
    public List<Cadete> LCadetes { get => lcadetes; set => lcadetes = value; }

    public void DarAlta()
    {
        string obs="", nombre="", direccion="", telefono="", referencia="";
        cantPedidos++;

        Console.WriteLine("\nPedido num: " + cantPedidos);

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

        var Cliente = new Cliente(nombre, direccion, telefono, referencia);
        var Pedido = new Pedido(cantPedidos, obs, Cliente, Estados.Pendiente);

        AsignarACadete(Pedido);

    }

    public void AsignarACadete(Pedido Pedido)
    {
        string str;

        Console.WriteLine ("\nAsignar al cadete: ");

        for(int i=0; i<LCadetes.Count(); i++)
        {
            Console.WriteLine(i+1 + " " + LCadetes[i].Nombre);
        }

        str = Console.ReadLine();

        if(int.TryParse(str, out int id))
        {
            LCadetes[id-1].AgregarPedido(Pedido);
        }
    }

    public void CambiarEstado()
    {
        string str;
        int numP = MostrarPendientes("cambiar su estado: ");

        Console.WriteLine("\n1. Entregado");
        Console.WriteLine("2. Cancelado");
        Console.WriteLine("3. Sigue pendiente");

        str = Console.ReadLine();
        if (int.TryParse(str, out int op))
        {
            switch(op)
            {
                case 1:
                    foreach(var cad in LCadetes)
                    {
                        cad.CambiarEstado(numP, Estados.Entregado);
                    }
                    break;
                
                case 2:
                    foreach(var cad in LCadetes)
                    {
                        cad.CambiarEstado(numP, Estados.Cancelado);
                    }
                    break;
            }
        }

    }

    public int MostrarPendientes(string text)
    {
        string str;
        
        Console.WriteLine("--- Pedidos pendientes ---");
        MostrarPedidos(Estados.Pendiente);

        Console.WriteLine("\nElija un Pedido para " + text);
        str = Console.ReadLine();

        if (int.TryParse(str, out int numP))
        {
            if(numP <= cantPedidos)
            {
                foreach(var cad in LCadetes)
                {
                    cad.MostrarPedido(numP);
                }
            }
        }

        return numP;
    }

    public void MostrarPedidos(Estados estado)
    {
        foreach(var cad in LCadetes)
        {
            foreach(var ped in cad.LPedidos)
            {
                if(ped.Estado == estado)
                {
                    Console.WriteLine(ped.Nro + ". " + ped.Obs + " - Cliente: " + ped.Cliente.Nombre + " / Dirección: " + ped.Cliente.Direccion + " -- Cadete: " + cad.Nombre);
                }
            }
        }
    }

    public void MostrarTLPedidos()
    {
        Console.WriteLine("--- Pedidos pendientes ---");
        MostrarPedidos(Estados.Pendiente);

        Console.WriteLine("\n--- Pedidos entregados ---");
        MostrarPedidos(Estados.Entregado);

        Console.WriteLine("\n--- Pedidos cancelados ---");
        MostrarPedidos(Estados.Cancelado);

        Console.WriteLine("\nPulse una tecla para continuar");
        Console.ReadKey();
    }

    public void ReasignarCadete()
    {
        string str;
        Pedido ped;
        int numP = MostrarPendientes("reasignar cadete: ");

        Console.WriteLine ("\nAsignar al cadete: ");

        for(int i=0; i<LCadetes.Count(); i++)
        {
            Console.WriteLine(i+1 + " " + LCadetes[i].Nombre);
        }

        str = Console.ReadLine();

        if(int.TryParse(str, out int id))
        {
            if (id <= LCadetes.Count())
            {
                foreach (var cad in LCadetes)
                {
                    ped = cad.QuitarPedido(numP);

                    if (ped != null)
                    {
                        LCadetes[id-1].AgregarPedido(ped);
                    }
                }
            }

        }
    }

    public void Informe()
    {
        int cantTotal = 0;
        float montoTotal = 0;
        Console.Clear();
        foreach(var cad in LCadetes)
        {
            Console.WriteLine($"Cadete: {cad.Nombre} -- Cant envíos: {cad.PedidosEntregados()} -- Monto ganado: {cad.JornalACobrar()}");
            cantTotal += cad.PedidosEntregados();
            montoTotal +=cad.JornalACobrar();
        }

        Console.WriteLine ("\nTotal de envíos: " + cantTotal);
        Console.WriteLine("Monto total: " + montoTotal);
        Console.WriteLine("Envíos promedio por cadete: " + cantTotal/LCadetes.Count());
    }

}

public class Cadete
{
    private int id;
    private string nombre;
    private string direccion;
    private string telefono;
    private List<Pedido> lpedidos;

    public Cadete (int Id, string Nombre, string Direccion, string Telefono)
    {
        id = Id;
        nombre = Nombre;
        direccion = Direccion;
        telefono = Telefono;
        lpedidos = new List<Pedido>();
    }

    public int Id { get => id; }
    public string Nombre { get => nombre; }
    public string Direccion { get => direccion; }
    public string Telefono { get => telefono; }
    public List<Pedido> LPedidos { get => lpedidos; set => lpedidos = value; }

    public void AgregarPedido(Pedido Pedido)
    {
        LPedidos.Add(Pedido);
    }

    public void MostrarPedido(int num)
    {
        foreach(var ped in LPedidos)
        {
            if(ped.Nro == num)
            {
                Console.Clear();
                Console.WriteLine("Obs: " + ped.Obs);
                Console.WriteLine("Estado: " + ped.Estado);
                ped.VerDatosCliente();
                Console.WriteLine("Cadete: " + Nombre);
            }
        }
    }

    public void CambiarEstado(int num, Estados estado)
    {
        foreach(var ped in LPedidos)
        {
            if(ped.Nro == num)
            {
                ped.Estado = estado;
            }
        }
    }

    public Pedido QuitarPedido(int num)
    {
        foreach(var ped in LPedidos)
        {
            if(ped.Nro == num)
            {
                LPedidos.Remove(ped);
                return ped;
            }
        }
        return null;       
    }

    public float JornalACobrar()
    {
        return PedidosEntregados()*500;
    }

    public int PedidosEntregados()
    {
        return LPedidos.Count(ped => ped.Estado == Estados.Entregado);
    }

}