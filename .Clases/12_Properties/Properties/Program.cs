using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Properties
{
    internal class Program
    {
        static void Main(string[] args)
        {
            /*
            Juan.GetSalario();
            Juan.SetSalario(salario: 1000);
            Juan.GetSalario();

            double nuevoSalario = Juan.GetSalario() + 1000;
            Juan.SetSalario(nuevoSalario); */

            /* Aplicando Properties */
            // peude haber properties que sea solo lectura y otra de solo escritura
            Empleado Juan = new Empleado(nombre: "Juan");
            Juan.Salario = -1000;
            Console.WriteLine("El salario es: " + Juan.Salario);


        }
    }
    class Empleado
    {
        private string nombre;
        private double salario;
        public Empleado(string nombre)
        {
            this.nombre = nombre;

        }
        /*
        public void SetSalario(double salario)
        {
            if (salario < 0)
            {
                Console.WriteLine("El salario no puede ser negativo");
            }
            else
            {
                this.salario = salario;
            }
        }
        public double GetSalario()
        {
            return salario;
        }
        */
        private double evaluaSalario(double salario)
        {
            if (salario < 0) return salario;
            else return salario;
        }
        /* 
        public double Salario
        {
            get { return salario; }
            set { salario = evaluaSalario(value); }
        }
        */
        
        // Acortando una properties
        public double Salario { get => salario; set => salario = evaluaSalario(value); }

    }
}
