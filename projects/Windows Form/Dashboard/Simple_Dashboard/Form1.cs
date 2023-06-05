using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Data.SqlClient;
using System.Collections;
using CapaEntidades;
using CapaNegocio;


namespace CapaPresentacion
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            Dashboad();
        }

        private void Dashboad()
        {
            N_Dashboard neg = new N_Dashboard();
            E_Dashboard obj = new E_Dashboard();
            neg.Dashboard(obj);

            chartProdPreferidos.Series[0].Points.DataBindXY(obj.Producto1, obj.Cantidad1);
            chartProdxCategoria.Series[0].Points.DataBindXY(obj.Categoria1, obj.CantProd1);

            lblTotalMarcas.Text = obj.CantlMarcas1.ToString();
            lblTotalProductos.Text = obj.CantlProductos1.ToString();
            lblTotalCategorias.Text = obj.CantCategorias1.ToString();
            lblTotalClientes.Text = obj.CantlClientes1.ToString();
            lblTotalProveedores.Text = obj.CantlProveedores1.ToString();
            lblTotalEmpleados.Text = obj.CantEmpleados1.ToString();
            lblTotalVentas.Text = obj.TotalVentas.ToString();
        }
    }
}
