using System;
using System.Linq;
using System.Text;
using System.Collections.Generic;
using System.Threading.Tasks;
using OrdenarNumeros;

namespace EjemploDll
{
    internal class Program
    {
        static void Main(string[] args)
        {
            OrdenadorNumerico objOrdenador = new OrdenadorNumerico();
            List<int> listaDesordanada = new List<int>();
            listaDesordanada.AddRange(new List<int>(){ 8,7,5,5,6,0,4,6});
            
            Console.WriteLine($"Lista desordenada");
            listaDesordanada.ForEach(Console.WriteLine);

            /*
            for(int i = 0; i <= listaDesordanada.Count - 1; i++)
            {
                Console.WriteLine(listaDesordanada[i].ToString());
            }
            */
            Console.WriteLine($"Lista Ordenada Ascendentemente");
            List<int> listaOrdenadaAscend = objOrdenador.sortAscendent(listaDesordanada);
            listaOrdenadaAscend.ForEach(Console.WriteLine);

            Console.WriteLine();

            Console.WriteLine($"Lista Ordenada Descendentemente");
            List<int> listaOrdenadaDesc = objOrdenador.sortDescendent(listaDesordanada);
            listaOrdenadaDesc.ForEach(Console.WriteLine);

            Console.WriteLine();
            

        }
    }
}
