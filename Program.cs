using System.Data;
using EspCadeteria;

internal class Program
{
    private static void Main(string[] args)
    {
        bool flag = true;
        string str;


        var Cadeteria = AccesoADatos.ElegirSource();

    
        while (flag)
        {
            Console.Clear();
            Console.WriteLine("Cadeteria: " + Cadeteria.Nombre);
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
                    case 1:Cadeteria.DarAlta();
                        break;
                    case 2:Cadeteria.MostrarTLPedidos();
                        break;
                    case 3:Cadeteria.CambiarEstado();
                        break;
                    case 4:Cadeteria.ReasignarCadete();
                        break;
                    case 5:Cadeteria.Informe();
                        flag = false;
                        break;
                }
            }
        }



    }
}