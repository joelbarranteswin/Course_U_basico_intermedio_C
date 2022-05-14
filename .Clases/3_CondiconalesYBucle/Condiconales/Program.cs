using System;

namespace Condiconales
{
    internal class Program
    {
        static void Main(string[] args)
        {
            bool haceFrio;
            haceFrio = !true;
            //Console.WriteLine("Hace frio? " + !haceFrio);
            // int edad = Int32.Parse(Console.ReadLine());

            int edad = 15;
            if (edad >= 18)
            {
                Console.WriteLine("Eres mayor de edad");
            }
            else
            {
                Console.WriteLine("Eres menor de edad");
            }


            if (edad <= 15 && haceFrio) Console.WriteLine("Eres 15 y debes abrigarte");
            else if (edad > 15 || haceFrio) Console.WriteLine("Haz lo que quieras");
            else Console.WriteLine("No tienes 15");


            // FORMAS DE RECIBIR PARAMETROS

            int compara = String.Compare("hola", "hola");
            Console.WriteLine(compara); //devuelve 0 si es true 


            /* SWITCH CASE */
            // solo evalua, int, char, string
            // no evalua booleano
            // no se puede tener 2 cases
            string medioTransporte = "bicicleta";
            switch (medioTransporte)
            {
                case "Tren":
                    Console.WriteLine("Viaja por el tren");
                    break;
                case "Avion":
                    Console.WriteLine("Viaja por el avion");
                    break;
                case "Barco":
                    Console.WriteLine("Viaja por el barco");
                    break;
                default:
                    Console.WriteLine("No se en que viajar");
                    break;
            }


            /* WHILE */
            int i = 0;
            while (i < 10)
            {
                Console.WriteLine(i);
                i++;
            }

            //Random rnd = new Random();
            //int numero = rnd.Next(1, 10);
            //Console.WriteLine(numero);

            
            /* DO WHILE */
            int j = 8;
            do
            {
                Console.WriteLine(j);
                j++;
            } while (j < 10);
            

        }
    }
}
