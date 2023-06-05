using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using CALCULATOR;

namespace InterfaceCalculator
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }

        private void Form1_Load(object sender, EventArgs e)
        {

        }
        private Calculation operadores;

        private void radioButton1_CheckedChanged(object sender, EventArgs e)
        {

        }

        private void button1_Click(object sender, EventArgs e)
        {
            try
            {
                if (textBox1.Text != String.Empty && textBox2.Text != String.Empty)
                {
                    double result = 0;
                    this.operadores = new Calculation();
                    double a = Double.Parse(textBox1.Text);
                    double b = Double.Parse(textBox2.Text);

                    if (radioButton1.Checked)
                    {
                        result = this.operadores.Sum(a, b);
                    }
                    else if (radioButton2.Checked)
                    {
                        result = this.operadores.Sustraction(a, b);
                    }
                    else if (radioButton3.Checked)
                    {
                        result = this.operadores.Multiplication(a, b);
                    }
                    else if (radioButton4.Checked)
                    {
                        result = this.operadores.Division(a, b);
                    }

                    textBox3.Text = result.ToString();
                }

            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
            }

        }
    }
}
