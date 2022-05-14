using System;

namespace POO
{
    internal class Program
    {
        static void Main(string[] args)
        {
            Console.WriteLine("Hello World!");

            /* Creando Objetos - instancias */
            Circulo figuraPropia; // creacion de objeto de tipo circulo
            figuraPropia = new Circulo(); // iniciacion de variable/objeto de tipo Circulo. 
                                          // Instaciaon o ejemplarizacion de un objeto de tipo Circulo
            //Console.WriteLine(figuraPropia.calculoArea(radio: 5));


            Circulo figuraPropia2 = new Circulo(); // otra forma de instanciar un objeto de tipo Circulo
            //figuraPropia2.PI = 10; //podemos cambiar una propiedad, pero hicimos que sea una constante y es inaccesible
            //Console.WriteLine(figuraPropia2.calculoArea(radio: 9));


            /* ENCAPSULAMIENTOI */

            ConversorEuroDollar euroOjecto = new ConversorEuroDollar();
            Console.WriteLine(euroOjecto.Euro);
            euroOjecto.Euro = -4.5;
            Console.WriteLine(euroOjecto.Convierte(cantidad: 10));


            /* CONSTRUCTOR */
            Coche coche1 = new Coche();
            Coche coche2 = new Coche();

            Console.WriteLine(coche1.GetInfo);
            Console.WriteLine(coche2.GetInfo);

            Coche coche3 = new Coche(largoCoche: 234.5, anchoCoche: 65.5);
            Console.WriteLine(coche3.GetInfo);
            coche3.setExtras(climatizador: true, tapiceria: "Cuero");
            Console.WriteLine(coche3.getExtras());





        }
    }

    class Circulo
    {
        //CONST hace que sea inaccesible y a la vez una constante
        private const double PI = Math.PI; // propiedad de la clase Circulo. Campo de clase.

        // ENCAPSULAMIENTO
        //para ello solo se usa el PRIVATE
        public double calculoArea(int radio)
        {
            return PI * radio * radio;
        }


    }

    class ConversorEuroDollar
    {
        private double euro = 1.253;

        //Metodo setter y getter para obtener y llamar el valor
        public double Euro 
        {
            get
            {
                return euro;
            }
            set
            {
                if (value < 0) euro = 1.253;
                else euro = value;
            }
        }

        public double Convierte(double cantidad)
        {
            return cantidad * Euro;
        }
    }

    /* PARTIAL */
    //  Division de una clase en 2
    partial class Coche
    {

        /* CONSTRUCTOR */
        // un constructor no es void ni static
        // void: es un metodo que no devuelve nada
        // static: es un metodo que tiene un return dentro
        // se debe tener en cuenta que si hay un contructor por defecto 

        public Coche()
        {
            ruedas = 4;
            largo = 2300.5;
            ancho = 45.7;

        }
        public Coche(double largoCoche, double anchoCoche) //sobrecarga de constructores
        {
            ruedas = 4;
            largo = largoCoche;
            ancho = anchoCoche;
            tapiceria = "Tela";

        }
    }

    partial class Coche
    {
        private int ruedas;
        private double largo;
        private double ancho;
        private bool climatizador;
        private string tapiceria;


        //Metodos en una linea
        public int Ruedas { get => ruedas; set => ruedas = value; }
        public string GetInfo { get => $"el coche tiene: Ruedas: {ruedas}, Largo: {ancho} y ancho: {largo}"; }


        public void setExtras(bool climatizador, string tapiceria)
        {
            this.climatizador = climatizador;
            this.tapiceria = tapiceria;
        }
        public string getExtras()
        {
            return $"se tiene \nclimatizador: {climatizador} y Tapiceria: {tapiceria}";
        }
    }
}
