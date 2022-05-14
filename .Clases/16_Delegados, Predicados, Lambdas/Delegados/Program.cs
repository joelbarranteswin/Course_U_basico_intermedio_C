using System;
using System.Collections.Generic;

namespace Delegados
{
    internal class Program
    {
        static void Main(string[] args)
        {
            /* Delegados */
            // se usa para llamar eventos
            // codigo reutilizable
            //es capaz de llamar a metodos de diferentes classes
            ObjetoDelegado elDelegado = new ObjetoDelegado(MensajeBienvenida.SaludoBienvenida);

            elDelegado(mensaje: "joel");

            ObjetoDelegado elDelegado2 = new ObjetoDelegado(MensajeDespedida.SaludoDespedida);
            elDelegado2(mensaje: "joel");

            /* Delegado Predicados */
            List<int> listaNumeros = new List<int>();
            listaNumeros.AddRange(new int[] { 1, 2, 3, 4, 5, 6, 7, 8, 9, 10 });

            Predicate<int> predicado = new Predicate<int>(EsPar);
            List<int> listaPares = listaNumeros.FindAll(predicado);
            foreach (int numero in listaPares)
            {
                Console.WriteLine(numero);
            }


            Console.WriteLine("--------------------------------------------");
            /* Filtros con Predicados */
            List<Persona> listaPersona = new List<Persona>();
            listaPersona.Add(new Persona("Juan", "Perez", 98));
            listaPersona.Add(new Persona("Pedro", "Perez", 8));
            listaPersona.Add(new Persona("Juaneco", "Perezo", 5));
            listaPersona.Add(new Persona("Juanito", "Perez", 20));
            listaPersona.Add(new Persona("Juanito", "Perez", 20));
            listaPersona.Add(new Persona("Juanito", "Perez", 20));

            Predicate<Persona> funcion = new Predicate<Persona>(EsMayorDeEdad);
            List<Persona> existe = listaPersona.FindAll(funcion);

            foreach (Persona persona in existe)
            {
                Console.WriteLine(persona.Nombre);
            }


            Console.WriteLine("--------------------------------------------");
            /* Expresiones lambdas */
            // son funciones anonimas que no necesitan nombre
            // se pueden usar para crear funciones de una sola linea
            OperacionesMatematicas operaciones = new OperacionesMatematicas(cuadrado);
            float value = operaciones(numero: 10);
            Console.WriteLine(value);

            //usando expresiones lambda
            OperacionesMatematicas operaciones2 = new OperacionesMatematicas(numero => numero * numero);
            float value2 = operaciones2(numero: 10);
            Console.WriteLine(value2);

            //lambdas con 2 parametros
            OperacionesMatematicas2 operaciones3 = (numero, numero2) => numero + numero2;
            float value3 = operaciones3(numero: 10, numero2: 20);
            Console.WriteLine(value3);

            List<int> numeros = new List<int> { 1, 2, 3, 4, 5, 6, 7, 8, 9, 10 };
            List<int> numerosPares = numeros.FindAll(numero => numero % 2 == 0);

            //for each with lambda
            numeros.ForEach(numero => Console.WriteLine(numero));


            numeros.ForEach(numero => {
                Console.WriteLine("mi numero es:");
                Console.WriteLine(numero);
                });


            // comparar la edad de 2 personas
            Persona persona1 = new Persona("Juan", "Perez", 98);
            Persona persona2 = new Persona("Pedro", "Perez", 8);

            ComparaPersonas compararEdad = (persona1, persona2) => persona1.Edad == persona2.Edad;
            Console.WriteLine(compararEdad(persona1, persona2));

        }
        // el delegado debe tener el mismo parametros
        delegate void ObjetoDelegado(string mensaje);

        static bool EsPar(int numero)
        {
            if (numero % 2 == 0) return true;
            else return false;
        }
        static bool IsPrime(int numero)
        {
            if (numero < 2) return false;
            for (int i = 2; i < numero; i++)
            {
                if (numero % i == 0) return false;
            }
            return true;
        }
        static bool EsMayorDeEdad(Persona persona)
        {
            if (persona.Edad >= 18) return true;
            else return false;
        }

        public delegate int OperacionesMatematicas(int numero);
        public delegate int OperacionesMatematicas2(int numero, int numero2);

        public static int cuadrado(int num)
        {
            return num * num;
        }

        public delegate bool ComparaPersonas(Persona persona1, Persona persona2);
    }
    class Persona
    {
        public Persona(string nombre, string apellido, int edad)
        {
            this.nombre = nombre;
            this.apellido = apellido;
            this.edad = edad;
        }

        private string nombre;
        private string apellido;
        private int edad;

        public string Nombre { get => nombre; set => nombre = value; }
        public int Edad { get => edad; set => edad = value; }
        public string Apellido { get => apellido; set => apellido = value; }
    }
    class MensajeBienvenida
    {
        public static void SaludoBienvenida(string mensaje)
        {
            Console.WriteLine("Bienvenido, que tal? {0}",mensaje);
        }
    }
    class MensajeDespedida
    {
        public static void SaludoDespedida(string mensaje)
        {
            Console.WriteLine("Hasta luego " + mensaje);
        }
    }
}
