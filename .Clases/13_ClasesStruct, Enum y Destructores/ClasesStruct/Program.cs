using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ClasesStruct
{
    /* Enum */
    //Enum es una clase que contiene una coleccion de constantes
    // se crea en los namespace porque todas las clases deben tener acceso
    enum Estaciones { Primavera, Verano, Otoño, Invierno };
    enum Bonus { Bajo=500, Normal=1000, Alto=1500, Extra=3000 };
    internal class Program
    {
        static void Main(string[] args)
        {
            /* Usando Struct */
            //stack: es de acceso rapido, el almacen es temporal, variables locales
            // Heap: es mas lento, esta disponible durante la ejecucion, variable globales
            Empleado empleado = new Empleado(salarioBase: 1000, comision: 25);
            empleado.cambiaSalario(empleado, 200);
            Console.WriteLine(empleado);

            Console.WriteLine("------------------------------------------------------");
            /* Utilizando Enum */
            Estaciones alegria = Estaciones.Primavera;
            Console.WriteLine(alegria);

            string la_alegria = Estaciones.Primavera.ToString();
            Console.WriteLine(la_alegria);

            Bonus Antonio = Bonus.Normal;
            double bonusAntonio = (double)Antonio;
            Console.WriteLine(bonusAntonio);

            EmpleadoEnum empleado1 = new EmpleadoEnum(bonusEmpleado: Bonus.Normal, salario: 1000);
            Console.WriteLine(empleado1.GetSalario());


            Console.WriteLine("------------------------------------------------------");
            /* Garbage Collector */
            //el Garbage Collector es una clase que se encarga de eliminar los objetos que no se utilizan
            //el Garbage Collector se ejecuta automaticamente
            // usadp cpn conexiones de base de datos
            // cierre de streams o archivos externos
            // cierre de conexiones de red
            // eliminacion de objetos

            // solo se usan en clase
            // cada clase solo puede tener un Garbage Collector (destructor)
            // el Garbage Collector se ejecuta automaticamente
            // los destructores no tiene modificadores ni parametros
            // no utilizar excesivamente

            ManejoArchivos miArchivo = new ManejoArchivos();
            miArchivo.mensaje();

        }
        

    }
    class ManejoArchivos
    {
        StreamReader archivo = null;
        int contador = 0;
        string linea;
        public ManejoArchivos()
        {
            
            archivo = new StreamReader(@"C:\Users\joel_\OneDrive\Documentos\GitHub\C_Sharp_beyond_practices\.Clases\ClasesStruct\ClasesStruct\data\base.txt");
            while ((linea = archivo.ReadLine()) != null)
            {
                Console.WriteLine(linea);
                contador++;
            }
        }
        public void mensaje()
        {
            Console.WriteLine("El archivo tiene {0} lineas", contador);
        }
        //creando un destructor
        ~ManejoArchivos()
        {
            archivo.Close();
        }
    }
    class EmpleadoEnum
    {
        private double salario, bonus;
        private Bonus bonusEmpleado;
        public EmpleadoEnum(Bonus bonusEmpleado, double salario)
        {
            //bonus = (double)bonusEmpleado;
            this.bonusEmpleado = bonusEmpleado;
            this.salario = salario;
        }
        public double GetSalario()
        {
            return salario + (double)bonusEmpleado;
        }
    }    

    /* Usando Struct */
    // se almacena en el stack
    //no puede haber sobrecarga de constructores
    // no tiene constructor por defecto
    // no puede haber sobrecarga de metodos
    // no puede haber sobrecarga de propiedades
    // no heredan pero si usan interfaces
    // son sealed por defecto

    //sirve para trabajar con gran cantidad de datos
    // cuando las instancia no cambian
    // cuando no se necesite convertir a objeto
    // cuando no se necesite acceder a los metodos de un objeto
    //instacias de tamaño inferior a 16bytes
    // se usa por razones de rendimiento
    struct Empleado
    {
        public double salarioBase, comision;
        public Empleado(double salarioBase, double comision)
        {
            this.salarioBase = salarioBase;
            this.comision = comision;
        }
        public override string ToString()
        {
            return String.Format("salario: {0:0.00} y comision {1}", salarioBase, comision);
        }
        public void cambiaSalario(Empleado emp, double incremento)
        {
            emp.salarioBase += incremento;
            emp.comision += incremento;
        }
    }
}
