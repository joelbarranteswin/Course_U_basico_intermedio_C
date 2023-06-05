using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace ENCUESTA
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }

        private void textBox1_TextChanged(object sender, EventArgs e)
        {

        }

        private void button1_Click(object sender, EventArgs e)
        {
            string Result = "";

            if (checkBox1.Checked)
            {
                Result += checkBox1.Text + "\r\n";
            }
            if (checkBox2.Checked)
            {
                Result += checkBox2.Text + "\r\n";
            }
            if (checkBox3.Checked)
            {
                Result += checkBox3.Text + "\r\n";
            }
            if (checkBox4.Checked)
            {
                Result += checkBox4.Text + "\r\n";
            }


            if (radioButton1.Checked)
            {
                Result += "MODALIDAD:" + radioButton1.Text;
            }
            else if (radioButton2.Checked)
            {
                Result += "MODALIDAD:" + radioButton2.Text;
            }

            ResultBox.Text = Result;
        }
    }
}
