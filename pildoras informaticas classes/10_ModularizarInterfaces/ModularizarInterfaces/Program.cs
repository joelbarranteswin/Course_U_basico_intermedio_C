using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ModularizarInterfaces
{
    internal class Program
    {
        static void Main(string[] args)
        {
            AvisosTrafico aviso1 = new AvisosTrafico();
            aviso1.MostrarAviso();
            AvisosTrafico aviso2 = new AvisosTrafico(
                remitente: "Jefatura", mensaje: "Sancion de 300 soles", fecha: "23-05-23");
            aviso2.MostrarAviso();
            string fecha = aviso2.GetFecha();
            Console.WriteLine(fecha);
        }
    }
}
