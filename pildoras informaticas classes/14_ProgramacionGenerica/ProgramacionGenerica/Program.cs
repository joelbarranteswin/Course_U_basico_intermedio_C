using System;

namespace ProgramacionGenerica
{
    internal class Program
    {
        static void Main(string[] args)
        {
            /* clases Genericas */
            AlmacenObjetos<Empleado> archivos = new AlmacenObjetos<Empleado>(z: 5);
            //archivos.Agregar("Juan");
            //archivos.Agregar("Pedro");
            //archivos.Agregar("Juan");
            //archivos.Agregar("Pedro");
            //archivos.Agregar("Juan");

            archivos.Agregar(new Empleado(salario: 400));
            archivos.Agregar(new Empleado(salario: 500));
            archivos.Agregar(new Empleado(salario: 600));
            archivos.Agregar(new Empleado(salario: 700));
            archivos.Agregar(new Empleado(salario: 800));

            //Empleado salarioPersona = (Empleado)archivos.GetElemento(2);
            //en la programacion generica, ya no se usa el casting
            Empleado salarioPersona = archivos.GetElemento(2);
            Console.WriteLine(salarioPersona.GetSalario());

            //Hacer que las clases genericas con restricciones
            AlmacenEmpleado<Director> archivosEmpleado = new AlmacenEmpleado<Director>(z: 3);
            archivosEmpleado.Agregar(new Director(salario: 400));
            archivosEmpleado.Agregar(new Director(salario: 500));
            archivosEmpleado.Agregar(new Director(salario: 600));

            Console.WriteLine(archivosEmpleado.GetElemento(2).GetSalario());

        }
    }
    class AlmacenEmpleado<T> where T: IEmpleado 
        //objecto internet que obliga a las clases a tener GetSalario
    {
        private int i = 0;
        private T[] datosElementos;
        public AlmacenEmpleado(int z)
        {
            datosElementos = new T[z];
        }
        public void Agregar(T obj)
        {
            datosElementos[i] = obj;
            i++;
        }
        public T GetElemento(int i)
        {
            return datosElementos[i];
        }
    }
    class Director: IEmpleado
    {
        private double salario;
        public Director(double salario)
        {
            this.salario = salario;
        }
        public int GetSalario()
        {
            return 1000;
        }
    }

    class Secretaria: IEmpleado
    {
        private double salario;
        public Secretaria(double salario)
        {
            this.salario = salario;
        }
        public int GetSalario()
        {
            return 500;
        }
    }

    class Electricista: IEmpleado
    {
        private double salario;
        public Electricista(double salario)
        {
            this.salario = salario;
        }
        public int GetSalario()
        {
            return 300;
        }
    }
    class Estudiante
    { 
    }

    interface IEmpleado
    {
        int GetSalario();
    }
    class AlmacenObjetos<T> //Clases genericas se usa <T>
    {
        private T[] datosElemento;
        private int i = 0;
        public AlmacenObjetos(int z)
        {
            datosElemento = new T[z];
        }
        public void Agregar (T obj)
        {
            datosElemento[i] = obj;
            i++;
        }
        public T  GetElemento(int i)
        {
            return datosElemento[i];
        }

    }
    class Empleado
    {
        private double salario;
        public Empleado(double salario)
        {
            this.salario = salario;
        }
        public double GetSalario()
        {
            return salario;
        }
    }

}
