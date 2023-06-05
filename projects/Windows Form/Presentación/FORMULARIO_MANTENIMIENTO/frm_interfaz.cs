using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace FORMULARIO_MANTENIMIENTO
{
    public partial class frm_interfaz : Form
    {
        public frm_interfaz()
        {
            InitializeComponent();
        }

        string Estado;

        private void btnNuevo_click(object sender, EventArgs e)
        {
            Estado = "Nuevo";
            txtCodigo.Text = "";
            txtDescripcion.Text = "";

            gbMantenimiento.Enabled = true;
            lstMantenimiento.Enabled = false;

            btnNuevo.Enabled = false;
            btnActualizar.Enabled = false;
            btnEliminar.Enabled = false;
            btnSalir.Enabled = false;

            txtCodigo.Focus();
            

        }

        private void btnCancelar_Click(object sender, EventArgs e)
        {
            txtCodigo.Text = "";
            txtDescripcion.Text = "";

            gbMantenimiento.Enabled = false;
            lstMantenimiento.Enabled = true;

            btnNuevo.Enabled = true;
            btnActualizar.Enabled = true;
            btnEliminar.Enabled = true;
            btnSalir.Enabled = true;

        }

        private void btnGuardar_Click(object sender, EventArgs e)
        {
            string Registro = txtCodigo.Text.Trim() + " | " + txtDescripcion.Text.Trim();

            if (Estado == "Nuevo")
            {
                lstMantenimiento.Items.Add(Registro);
            }
            else if (Estado == "Actualizar")
            {
                int posicion = lstMantenimiento.SelectedIndex;
                lstMantenimiento.Items.Remove(lstMantenimiento.SelectedItem);
                lstMantenimiento.Items.Insert(posicion, Registro);
            }    

            txtCodigo.Text = "";
            txtDescripcion.Text = "";

            gbMantenimiento.Enabled = false;
            lstMantenimiento.Enabled = true;

            btnNuevo.Enabled = true;
            btnActualizar.Enabled = true;
            btnEliminar.Enabled = true;
            btnSalir.Enabled = true;

            MessageBox.Show("Registro Guardado");
        }

        private void btnEliminar_Click(object sender, EventArgs e)
        {
            lstMantenimiento.Items.Remove(lstMantenimiento.SelectedItem);
            MessageBox.Show("Elemento Eliminado");
        }



        private void btnActualizar_Click(object sender, EventArgs e)
        {
            Estado = "Actualizar";
            lstMantenimiento.Enabled = false;
            gbMantenimiento.Enabled = true;
            txtCodigo.Focus();
        }

        private void lstMantenimiento_Click(object sender, EventArgs e)
        {
            string TextSelected = lstMantenimiento.SelectedItem.ToString().Trim();
            int LengthText = TextSelected.Length;
            txtCodigo.Text = TextSelected.Substring(0, 3);
            txtDescripcion.Text = TextSelected.Substring(6, LengthText - 6);
        }
    }
}
