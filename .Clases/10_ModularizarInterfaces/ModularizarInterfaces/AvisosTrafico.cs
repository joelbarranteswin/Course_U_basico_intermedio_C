using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ModularizarInterfaces
{
    internal class AvisosTrafico : IAvisos
    {
        private string remitente;
        private string mensaje;
        private string fecha;        

        //Constructor por defecto
        public AvisosTrafico() 
        {
            remitente = "DGT";
            mensaje = "Sancion cometida. pague antes de 3 dias y se beneficiara de una reduccion";
            fecha = "";
        }
        //constructor definido
        public AvisosTrafico(string remitente, string mensaje, string fecha)
        {
            this.remitente = remitente;
            this.mensaje = mensaje;
            this.fecha = fecha;
        }
    

        public string GetFecha()
        {
            return fecha;
        }

        public void MostrarAviso()
        {
            Console.WriteLine("Mensaje {0} ha sido enviado por {1} el {2}", mensaje, remitente, fecha);
        }
    }
}
