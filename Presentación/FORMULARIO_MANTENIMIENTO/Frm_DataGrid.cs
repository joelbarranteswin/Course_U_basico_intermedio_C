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
    public partial class Frm_DataGrid : Form
    {
        public Frm_DataGrid()
        {
            InitializeComponent();
        }

        private void dataGridView1_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {

        }

        private void button1_Click(object sender, EventArgs e)
        {
            if (txtNombres.Text != "" | txtEmail.Text != "")
            {
                dgvDatos.Rows.Add(txtNombres.Text, txtEmail.Text);
                txtNombres.Text = "";
                txtEmail.Text = "";

            }
        }
    }
}
