using System;

namespace Arrays
{
    internal class Program
    {
        static void Main(string[] args)
        {
            Console.WriteLine("Hello World!");

            /* Arrays */
            // Son las matrices o Arreglos
            // Son una colección de datos, se almacen varios valores

            //si no se declara el array y se llama, entonces se obtiene 
            // en int se obtiene 0
            // en string se obtiene ""
            string[] edades = new string[5];
            Console.WriteLine(edades[4]);


            //se declara el array y se llama
            int[] miMatriz = new int[5];

            // Inicializar un arreglo
            miMatriz[0] = 1;
            miMatriz[1] = 2;
            miMatriz[2] = 3;
            miMatriz[3] = 4;
            miMatriz[4] = 5;

            // Acceder a un elemento de un arreglo
            Console.WriteLine(miMatriz[0]);

            // Otra forma de declarar un array

            int[] edades2 = { 34, 35, 56 }; // a veces no es muy claro
            int[] miMatriz2 = new int[5] { 1, 2, 3, 4, 5 }; // es mejor porque se declara la cantidad de valores
            Console.WriteLine(miMatriz2[0]);


            /* Array implicitos */
            // Son arreglos que se declaran sin necesidad de declarar el tamaño, ni el tipo de dato
            Console.WriteLine("-------------------------------------------------");

            var datos = new[] { "juan", "pedro", "maria" };

            var valores = new[] { 15, 28, 35, 75.5, 30.3 }; // se puede declarar int y double


            //array de objetos
            Empleado[] arrayEmpleados = new Empleado[3];
            arrayEmpleados[0] = new Empleado(nombre: "Juan", edad: 34);

            Empleado Pedro = new Empleado(nombre: "Pedro", edad: 35);
            arrayEmpleados[1] = Pedro;
            arrayEmpleados[2] = new Empleado(nombre: "Maria", edad: 36);
            Console.WriteLine(arrayEmpleados[0]);

            /* Array de clases Anonimas */
            // Son arreglos que se declaran sin necesidad de declarar el tamaño, ni el tipo de dato
            // se debe tener en cuenta que deben ser del mismo tipo
            var personas = new[]
            {
                new { Nombre = "Juan", Edad = 34 },
                new { Nombre = "Pedro", Edad = 35 },
                new { Nombre = "Maria", Edad = 36 }
            };
            Console.WriteLine(personas[0]);


            /* Bucles For */
            // Son los ciclos que se ejecutan una vez
            Console.WriteLine("-------------------------------------------------");
            int[] miMatriz3 = new int[5] { 1, 2, 3, 4, 5 };
            for (int i = 0; i < miMatriz.Length; i++) //utilizando la propiedad Lenght
            {
                Console.WriteLine(miMatriz[i]);
            }

            Console.WriteLine("-------------------------------------------------");
            int contador = 0;
            for (int i = 20; i < 10; i--)
            {
                Console.WriteLine(miMatriz[contador]);
                contador++;
            }

            Console.WriteLine("-------------------------------------------------");
            for (int i = 0; i < arrayEmpleados.Length; i++)
            {
                Console.WriteLine(arrayEmpleados[i].getInfo());
            }


            /* Bucles ForEach */
            // Son los ciclos que se ejecutan una vez
            Console.WriteLine("-------------------------------------------------");
            foreach (Empleado item in arrayEmpleados)
            {
                Console.WriteLine(item.getInfo());
            }

            
            /* Metodo que Recibe un Array */
            Console.WriteLine("-------------------------------------------------");
            int[] miMatriz4 = new int[5] { 1, 2, 3, 4, 5 };            
            ProcesaDatos(miMatriz4);

            foreach (int item in miMatriz4)
            {
                Console.WriteLine(item);
            }

            Console.WriteLine("-------------------------------------------------");
            int[] miMatriz5 =  leerDatos(numeroElemento: 5);
            foreach (int item in miMatriz5)
            {
                Console.WriteLine(item);
            }
        }

        static void ProcesaDatos(int[] datos)
        {
            //foreach (int item in datos)
            //{
            //    Console.WriteLine(item);
            //}

            for (int i = 0; i < datos.Length; i++)
            {
                datos[i] += 10;
            }
        }

        static int[] leerDatos(int numeroElemento)
        {
            int[] datos = new int[numeroElemento];
            for (int i = 0; i < datos.Length; i++)
            {
                Console.WriteLine("Ingrese el elemento " + (i + 1));
                datos[i] = int.Parse(Console.ReadLine());
                
            }
            return datos;
        }
    }

    class Empleado 
    {
        private string nombre;
        private int edad;
        
    public Empleado(string nombre, int edad)
        {
            this.nombre = nombre;
            this.edad = edad;
        }

        public string Nombre { get => nombre; set => nombre = value; }
        public int Edad { get => edad; set => edad = value; }
        
        public string getInfo()
        {
            return "Nombre: " + this.nombre + " Edad: " + this.edad;
        }
        
        
    }
}
