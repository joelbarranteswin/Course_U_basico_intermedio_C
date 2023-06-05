using System;
using static System.Math; //se usa static porque no se necesita instanciar la clase Math, ya que son static

namespace ConceptosPOO
{
    internal class Program
    {
        static void Main(string[] args)
        {
            Console.WriteLine("Hello World!");

            //realizarTarea();


            /* Importar Clases static */
            double raiz = Sqrt(9);
            double potencia = Pow(2, 3);

            Console.WriteLine(raiz);
            Console.WriteLine(potencia);


            /* clase anonimas */
            // no se necesita definir el tipo de varibale
            var miVariable = new { Nombre = "Juan", Apellido = "Perez", Edad = 30 };
            Console.WriteLine(miVariable.Nombre + " " + miVariable.Apellido + " " + miVariable.Edad);

            var miVariable2 = new { Nombre = "Ana", Apellido = "Perez", Peso = 30 };
            Console.WriteLine(miVariable2.Nombre + " " + miVariable2.Apellido + " " + miVariable2.Peso);
            
            
        }

        static void realizarTarea()
        {
            //TODO: Realizar tarea
            Console.WriteLine("Realizar tarea");

            Punto origen = new Punto();
            Punto destino = new Punto(x:20, y:30);

            double distancia = origen.Distancia(destino);
            Console.WriteLine(distancia);

            Punto origen2 = new Punto();

            // Llamando un metodo static el cual no requiere un objeto
            Console.WriteLine($"Numero de objetos creados: {Punto.ContadorDeObjetos()}");
        }
    }
}
