using System;
using System.Text.RegularExpressions;

namespace ExprecionesRegulares
{
    internal class Program
    {
        static void Main(string[] args)
        {
            /* expresione Regulares */
            string frase1 = "Mi nombre es Juan y mi n° es (+51)134-45-56 (+34)124-85-56 y mi codigo postal es 1234";
            string dominio2 = "mi web es https://www.dominio.com";
            //string patron = "[J]";

            //string patron = @"\d{3}-\d{2}-\d{2}";
            //string patron = @"\+51|\+34";
            string patron = "https://(www.)?dominio.com";

            Regex miRegex = new Regex(patron);
            MatchCollection matches = miRegex.Matches(dominio2);
            if (matches.Count > 0)
            {
                foreach (Match match in matches)
                {
                    Console.WriteLine(match.Value);
                }
            }
            else
            {
                Console.WriteLine("No hay coincidencias");
            }




        }
    }
}
