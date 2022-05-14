using System;

namespace Interfaces
{
    internal class Program
    {
        static void Main(string[] args)
        {

            /* Interfaces */
            // Obliga a implementar un metodo en una clase hija que hereda la interfaz
            Ballena miWally = new Ballena("Wally");
            miWally.Nadar();

            //Caso en la que se tiene dos metodos con el mismo nombre
            Caballo animal2 = new Caballo("Bucefalo");
            IMamiferosTerrestres Ianimal2 = animal2;
            Console.WriteLine(Ianimal2.NumeroPatas());

            ISaltoConPatas ISanimal2 = animal2;
            Console.WriteLine(ISanimal2.NumeroPatas());
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
        //no se puede crear una clase dentro de la interfaz
        interface IMamiferosTerrestres
        {
            int NumeroPatas();
        }
        interface IAnimalesYDeportes
        {
            string tipoDeporte();
            bool esOlimpico();
        }
        interface ISaltoConPatas
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

        // no se admite herencia multiple
        // se puede usar una interfaz para mas herencia
        class Caballo : Mamiferos, IMamiferosTerrestres, IAnimalesYDeportes, ISaltoConPatas
        {
            public Caballo(string nombreCaballo) : base(nombreCaballo)
            {

            }
            public void Correr()
            {
                Console.WriteLine("Corriendo");
                Respirar(); // es un metodo protected y solo tenemos acceso desde un hijo
            }
            int IMamiferosTerrestres.NumeroPatas()
            {
                return 4;
            }
            int ISaltoConPatas.NumeroPatas()
            {
                return 2;
            }
            public string tipoDeporte()
            {
                return "Hipica";
            }
            public bool esOlimpico()
            {
                return true;
            }

        }

        class Humano : Mamiferos, IAnimalesYDeportes
        {
            public Humano(string nombreHumano) : base(nombreHumano)
            {

            }
            // agregar la palabra reservada NEW para que se quite la advertencia en composicion
            // se sabe que hay 2 metodo con el mismo nombre de diferentes clases al hacer composicion
            public override void Pensar()
            {
                Console.WriteLine("pensando como humano");
            }
            public string tipoDeporte()
            {
                return "Futbol";
            }
            public bool esOlimpico()
            {
                return true;
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
