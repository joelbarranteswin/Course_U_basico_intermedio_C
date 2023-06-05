using System;

namespace HerenciaPOO
{
    internal class Program
    {
        static void Main(string[] args)
        {
            /* HERENCIA */
            Caballo Babieca = new Caballo(nombreCaballo: "Babieca");
            Humano Juan = new Humano(nombreHumano: "Juan");
            Gorila Copito = new Gorila(nombreGorila: "Copito");

            // En herencia siempre se hereda de Object
            Babieca.GetName();
            Juan.GetName();
            Copito.GetName();


            Console.WriteLine("----------------------------------------------");
            /* PRINCIPIO DE SUSTITUCIÓN */
            //Usar la superclase y las clases
            Mamiferos animal = new Caballo("Bucefalo");
            Mamiferos persona = new Humano("Pepe");
            Mamiferos animalPersona = new Gorila("Gila");

            //la superclase cosmica es Object
            Object miAnimal = new Caballo("Bucefalo");
            Object miMamifero = new Mamiferos("mamifero");

            animal.GetName();


            Console.WriteLine("----------------------------------------------");
            // crear un array 
            Mamiferos[] mamiferos = new Mamiferos[3];
            mamiferos[0] = animal;
            mamiferos[1] = persona;
            mamiferos[2] = animalPersona;


            foreach (Mamiferos item in mamiferos)
            {
                item.GetName();
            }


            Console.WriteLine("----------------------------------------------");
            /* POLIMORFISMO */
            // objetos que tienen diferentes formas
            /* Aplicando NEW, VIRTUAL - OVERRIDE */
            // new: es para aplicar la sobrecarga de metodos
            // virtual - override: para sobre escribir el comportamiento de un metodo
            foreach (Mamiferos item in mamiferos)
            {
                item.Pensar();
            }

            /* PUBLIC - PRIVATE - PROTECTED */
            // public: nos da acceso desde cualquier otra clase del programa
            // las variables deben estar en private, eso es encapsulamiento
            // private: solo se tiene acceso desde la clase donde se declaro
            // protected: solo sera accesible de donde se declare y desde donde se hereden

            Console.WriteLine("----------------------------------------------");
            /* Interfaces */
            Ballena miWally = new Ballena("Wally");
            miWally.Nadar();

            
            Caballo animal2 = new Caballo("Bucefalo");
            Console.WriteLine(animal2.NumeroPatas());
        }

        class Mamiferos
        {
            public Mamiferos(string nombre)
            {
                nombreSerVivo = nombre;
            }
            // se usa protected para evitar heredarlo
            protected void Respirar()
            {
                Console.WriteLine("Respirando");
            }
            public void CuidarCrias()
            {
                Console.WriteLine("Cuidando de las crias");
            }
            public virtual void Pensar()
            //Se aplica Sobrecarga a las clases hijas
            // si se tiene un virtual, entonces cada clase hija tiene el metodo aplicandolo diferente y usar OVERRIDE
            {
                Console.WriteLine("Pensamiento Basico instintivo Mamifero");
            }

            private string nombreSerVivo;

            public void GetName()
            {
                Console.WriteLine(nombreSerVivo);
            }
        }

        /* INTERFAZ*/
        //las interfaces no tienen logica 
        // solo se declaran
        // no tiene public ni private
        // Obliga a implementar un metodo
        // el nombre del metodo y el tipo debe coincidir con la interfaz 
        interface IMamiferosTerrestres
        {
            int NumeroPatas();
        }

        class Ballena : Mamiferos
        {
            public Ballena(string nombreBallena) : base(nombreBallena)
            {

            }
            public void Nadar()
            {

            }
        }

        class Caballo : Mamiferos, IMamiferosTerrestres
        {
            public Caballo(string nombreCaballo) : base(nombreCaballo)
            {

            }
            public void Correr()
            {
                Console.WriteLine("Corriendo");
                Respirar(); // es un metodo protected y solo tenemos acceso desde un hijo
            }
            public int NumeroPatas()
            {
                return 4;
            }
        }

        class Humano : Mamiferos
        {
            public Humano(string nombreHumano) : base(nombreHumano)
            {

            }
            // agregar la palabra reservada para que se quite la advertencia
            // se sabe que hay 2 metodo con el mismo nombre de diferentes clases
            public override void Pensar()
            {
                Console.WriteLine("pensando como humano");
            }

        }

        class Gorila : Mamiferos
        {
            public Gorila(string nombreGorila) : base(nombreGorila)
            {

            }
            public void Trepar()
            {
                Console.WriteLine("Trepar");
            }
            public override void Pensar()
            {
                Console.WriteLine("Pensamiento instintivo Avanzado gorilla");
            }
            public int NumeroPatas()
            {
                return 2;
            }
        }
    }
}
