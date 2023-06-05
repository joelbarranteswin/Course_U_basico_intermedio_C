using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Collections;

namespace CapaEntidades
{
    public class E_Dashboard
    {
        ArrayList Categoria = new ArrayList();
        ArrayList CantProd = new ArrayList();
        ArrayList Producto = new ArrayList();
        ArrayList Cantidad = new ArrayList();

        string totalVentas;
        string CantCategorias;
        string CantlMarcas;
        string CantlProductos;
        string CantlClientes;
        string CantEmpleados;
        string CantlProveedores;

        public ArrayList Categoria1 { get => Categoria; set => Categoria = value; }
        public ArrayList CantProd1 { get => CantProd; set => CantProd = value; }
        public ArrayList Producto1 { get => Producto; set => Producto = value; }
        public ArrayList Cantidad1 { get => Cantidad; set => Cantidad = value; }
        public string TotalVentas { get => totalVentas; set => totalVentas = value; }
        public string CantCategorias1 { get => CantCategorias; set => CantCategorias = value; }
        public string CantlMarcas1 { get => CantlMarcas; set => CantlMarcas = value; }
        public string CantlProductos1 { get => CantlProductos; set => CantlProductos = value; }
        public string CantlClientes1 { get => CantlClientes; set => CantlClientes = value; }
        public string CantEmpleados1 { get => CantEmpleados; set => CantEmpleados = value; }
        public string CantlProveedores1 { get => CantlProveedores; set => CantlProveedores = value; }
    }
}
