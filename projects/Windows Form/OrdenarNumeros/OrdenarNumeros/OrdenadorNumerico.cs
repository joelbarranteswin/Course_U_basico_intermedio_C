using System;
using System.Linq;
using System.Text;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace OrdenarNumeros
{
    public class OrdenadorNumerico
    {
        public List<int> sortDescendent(List<int> listaNumeros)
        {
            List<int> ordenados = listaNumeros;
            ordenados.Sort((a, b) => b.CompareTo(a));
            return ordenados;
        }
        public List<int> sortAscendent(List<int> listaNumeros)
        {
            List<int> ordenados = listaNumeros;
            ordenados.Sort((a, b) => a.CompareTo(b));
            return ordenados;
        }
    }
}
