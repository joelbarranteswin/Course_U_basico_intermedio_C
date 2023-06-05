using System;
using System.Collections.Generic;
using System.Linq;

namespace LinQ
{
    internal class Program
    {
        static void Main(string[] args)
        {
            /* LinQ */
            int[] numeros = new int[4] { 1, 2, 3, 4 };

            List<int> numerosPares = new List<int>();
            //foreach (int numero in numeros)
            //{
            //    if (numero % 2 == 0)
            //    {
            //        numerosPares.Add(numero);
            //    }
            //}

            numerosPares.ForEach(numero => Console.WriteLine(numero));

            /* LinQ */
            //IEnumerable<Empleado> o puedes usar var
            var numerosParesLinq = from numero in numeros
                                   where numero % 2 == 0
                                   select numero;

            foreach (var numero in numerosParesLinq)
            {
                Console.WriteLine(numero);
            }

            //numerosParesLinq.ToList().ForEach(numero => Console.WriteLine(numero));


            Console.WriteLine("-----------------------------------------------");
            controlEmpresaEmpleado control = new controlEmpresaEmpleado();
            control.GetCEO();
            
            Console.WriteLine("-----------------------------------------------");
            control.GetEmpleadosOrdenados();

            Console.WriteLine("-----------------------------------------------");
            control.GetEmpleadosJoin();

        }
    }
    class controlEmpresaEmpleado
    {

        public List<Empresa> listaEmpresa;
        public List<Empleado> listaEmpleado;        
        public controlEmpresaEmpleado()
        {
            listaEmpleado = new List<Empleado>();
            listaEmpresa = new List<Empresa>();

            listaEmpresa.Add(new Empresa(Id: 1, Nombre: "Google"));
            listaEmpresa.Add(new Empresa(Id: 2, Nombre: "Microsoft"));
            listaEmpresa.Add(new Empresa(Id: 3, Nombre: "Apple"));

            listaEmpleado.Add(new Empleado(Id: 1, Nombre: "Juan Perez", Cargo: "CEO", EmpresaId: 1, Salario: 356));
            listaEmpleado.Add(new Empleado(Id: 2, Nombre: "Pedro Perez", Cargo: "sub-CEO", EmpresaId: 2, Salario: 200));
            listaEmpleado.Add(new Empleado(Id: 3, Nombre: "Maria Perez", Cargo: "CEO", EmpresaId: 3, Salario: 100));
            listaEmpleado.Add(new Empleado(Id: 4, Nombre: "Juan Perez", Cargo: "CEO", EmpresaId: 1, Salario: 356));

        }
        public void GetCEO()
        {
            //IEnumerable<Empleado> o puedes usar var
            IEnumerable<Empleado> empleados = from empleado in listaEmpleado
                            where empleado.EmpresaId == 1
                            select empleado;

            foreach (var empleado in empleados)
            {
                Console.WriteLine(empleado.Nombre + " " + empleado.Cargo + " " + empleado.Salario);
            }
        }

        public void GetEmpleadosOrdenados()
        {
            //IEnumerable<Empleado> o puedes usar var
            var empleados = from empleado in listaEmpleado
                                              orderby empleado.Salario descending
                                              select empleado;

            foreach (var empleado in empleados)
            {
                Console.WriteLine(empleado.Nombre + " " + empleado.Cargo + " " + empleado.Salario);
            }
        }
        public void GetEmpleadosJoin()
        {
            //IEnumerable<Empleado> o puedes usar var
            var empleadosJoin = from empleado in listaEmpleado
                                              join empresa in listaEmpresa 
                                              on empleado.EmpresaId equals empresa.Id
                                                  where empresa.Nombre == "Google"
                                                  select empleado;

            foreach (var empleado in empleadosJoin)
            {
                Console.WriteLine(empleado.Nombre + " " + empleado.Cargo + " " + empleado.Salario + " " + empleado.Nombre + "y trabaja en Google");
            }
        }

    }
    class Empresa
    {
        public int Id { get; set; }
        public string Nombre { get; set; }
        public Empresa(int Id, string Nombre)
        {
            this.Id = Id;
            this.Nombre = Nombre;
        }
        public void GetDatosEmpresa()
        {
            Console.WriteLine("Id: {0}", Id);
            Console.WriteLine("Nombre: {0}", Nombre);
        }
    }
    
    class Empleado
    {
        public int Id { get; set; }
        public string Nombre { get; set; }
        public string Cargo { get; set; }
        public double Salario { get; set; }
        public int EmpresaId { get; set; }

        public Empleado(int Id, string Nombre, string Cargo, int EmpresaId, double Salario)
        {
            this.Id = Id;
            this.Nombre = Nombre;
            this.Cargo = Cargo;
            this.EmpresaId = EmpresaId;
            this.Salario = Salario;
        }
        public void GetDatosEmpleado()
        {
            Console.WriteLine($"Empleado {Nombre} con ID {Id} con salario {Salario} y cargo {Cargo} en {EmpresaId}");
        }
    }
}
