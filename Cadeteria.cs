namespace EspCadeteria;

public class Cadeteria
{
    private string nombre;
    private string telefono;
    private int cantPedidos;
    private List<Cadete> lcadetes;
    private List<Pedido> lpedidos;

    public Cadeteria(string nombre, string telefono)
    {
        this.nombre = nombre;
        this.telefono = telefono;
        this.lpedidos = new List<Pedido>();
    }

    public string Nombre { get => nombre; }
    public string Telefono { get => telefono; }
    public List<Cadete> LCadetes { get => lcadetes; set => lcadetes = value; }
    public List<Pedido> LPedidos { get => lpedidos; }
    public int CantPedidos { get => cantPedidos; set => cantPedidos = value; }

    public void DarAlta()
    {
        string obs="", nombre="", direccion="", telefono="", referencia="";
        CantPedidos++;
        int IdCad = 0;

        Console.WriteLine("\nPedido num: " + CantPedidos);

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
        var Pedido = new Pedido(CantPedidos, obs, Cliente, Estados.Pendiente);

        LPedidos.Add(Pedido);

        while(IdCad == 0)
        {
            IdCad = ElegirCadete();
        }

        int IndexPed = BuscarPedido(Pedido.Nro);

        AsignarCadeteAPedido(IdCad, IndexPed);


    }

    public void AsignarCadeteAPedido(int IdCad, int NroPed)
    {
            LPedidos[NroPed].Cadete = BuscarCadete(IdCad);
    }

    public int BuscarPedido(int NroPed)
    {
        int index = 0;
        for(int i=0; i<LPedidos.Count(); i++)
        {
            if(LPedidos[i].Nro == NroPed)
            {
                index = i;
            }
        }

        return index;
    }

    public Cadete BuscarCadete(int idCad)
    {
        return LCadetes.Find(cad => cad.Id == idCad);
    }

    public int ElegirCadete()
    {
        string str;

        Console.WriteLine ("\nAsignar al cadete: ");

        foreach(var cad in LCadetes)
        {
            Console.WriteLine(cad.Id + " " + cad.Nombre);
        }

        str = Console.ReadLine();

        if(int.TryParse(str, out int id))
        {
            if(id > 0 && id <= LCadetes.Count())
            {
                return id;
            } else return 0;
        } else return 0;
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
                    CambiarEst(numP, Estados.Entregado);
                    break;
                
                case 2:
                    CambiarEst(numP, Estados.Cancelado);
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
            if(numP <= CantPedidos)
            {
                MostrarPedido(numP);
            }
        }

        return numP;
    }


    public void MostrarPedidos(Estados estado)
    {

            foreach(var ped in LPedidos)
            {
                if(ped.Estado == estado)
                {
                    Console.WriteLine(ped.Nro + ". " + ped.Obs + " - Cliente: " + ped.Cliente.Nombre + " / Dirección: " + ped.Cliente.Direccion + " -- Cadete: " + ped.Cadete.Nombre);
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
        int numP = MostrarPendientes("reasignar cadete: ");
        int IdCad = 0;

        while(IdCad == 0)
        {
            IdCad = ElegirCadete();
        }

        int IndexPed = BuscarPedido(numP);

        if(LPedidos[IndexPed].Estado == Estados.Pendiente)
        {
            AsignarCadeteAPedido(IdCad, IndexPed);
        }

    }


    public void Informe()
    {
        int cantTotal = 0;
        float montoTotal = 0;
        Console.Clear();
        foreach(var cad in LCadetes)
        {
            Console.WriteLine($"Cadete: {cad.Nombre} -- Cant envíos: {PedidosEntregados(cad)} -- Monto ganado: {JornalACobrar(cad)}");
            cantTotal += PedidosEntregados(cad);
            montoTotal +=JornalACobrar(cad);
        }

        Console.WriteLine ("\nTotal de envíos: " + cantTotal);
        Console.WriteLine("Monto total: " + montoTotal);
        Console.WriteLine("Envíos promedio por cadete: " + cantTotal/LCadetes.Count());
    }

    // Refactorizacion de Pedidos


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
                Console.WriteLine("Cadete: " + ped.Cadete.Nombre);
            }
        }
    }

    public void CambiarEst(int num, Estados estado)
    {
        foreach(var ped in LPedidos)
        {
            if(ped.Nro == num)
            {
                ped.Estado = estado;
            }
        }
    }



    public float JornalACobrar(Cadete cad)
    {
        return PedidosEntregados(cad)*500;
    }

    public int PedidosEntregados(Cadete cad)
    {
        int cant = 0;
        foreach (var ped in LPedidos)
        {
            if (ped.Cadete == cad && ped.Estado == Estados.Entregado)
            {
                cant++;
            }
        }
        return cant;
    }


}

public class Cadete
{
    private int id;
    private string nombre;
    private string direccion;
    private string telefono;


    public Cadete (int Id, string Nombre, string Direccion, string Telefono)
    {
        id = Id;
        nombre = Nombre;
        direccion = Direccion;
        telefono = Telefono;
    }

    public int Id { get => id; }
    public string Nombre { get => nombre; }
    public string Direccion { get => direccion; }
    public string Telefono { get => telefono; }

    
}