using System;

namespace ClasesAbstractas
{
    internal class Program
    {
        static void Main(string[] args)
        {
            /* Clase abstracta */
            // a diferencia de la interfaz en esta se puede definir los metodos y luego heredarlas
            // estas clases no se puede instanciar

            Lagartija animal1 = new Lagartija("Lagartija Buena");
            animal1.GetNombre();

            Humano humano1 = new Humano("Juan");
            humano1.GetNombre();

            /* Sealed Classes */
            // Evita que se pueda heredar de una clase hija


        }

        abstract class Animales
        {
            public void Respirar()
            {
                Console.WriteLine("Respirando");
            }
            // el abstract da la opcion a implementar o sobreescribirla el metodo en la clase hija
            public abstract void GetNombre();
        }

        class Lagartija : Animales
        {
            private string nombreReptil;
            public Lagartija(string nombreReptil)
            {
                this.nombreReptil = nombreReptil;
            }
            public override void GetNombre()
            {
                Console.WriteLine("El nombre del reptil es: " + nombreReptil);
            }
        }


        class Mamiferos: Animales
        {
            public Mamiferos(string nombre)
            {
                nombreSerVivo = nombre;
            }
            // se usa protected para evitar heredarlo

            public void CuidarCrias()
            {
                Console.WriteLine("Cuidando de las crias");
            }
            public virtual void Pensar()
           
            {
                Console.WriteLine("Pensamiento Basico instintivo Mamifero");
            }

            private string nombreSerVivo;

            // se puede sobreescriber el metodo el metodo abstracto
            public override void GetNombre()
            {
                Console.WriteLine("El nombre del mamifero es: " + nombreSerVivo);
            }
        }
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
            
            public sealed override void Pensar()
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
        class Adolecente : Humano
        {
            string nombreAdolecente;
            public Adolecente(string nombreAdolecente) : base(nombreAdolecente)
            {
                this.nombreAdolecente = nombreAdolecente;
            }
            // no se puede sobreescriber un metodo con sealed
            //public override void Pensar()
            //{
            //    Console.WriteLine("pensando como adolecente es dificil");
            //}
        }

        /* el SEALED evita que se puede heredad */
        sealed class Gorila : Mamiferos
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
        //class Chimpance: Gorila
        //{
        //    string nombreChimpance;
        //    public Chimpance(string nombreChimpance) : base(nombreChimpance)
        //    {
        //        this.nombreChimpance = nombreChimpance;

        //    }

        //}
    }
}
