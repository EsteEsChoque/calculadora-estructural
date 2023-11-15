using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Diagnostics.Eventing.Reader;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Reflection.Emit;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Xml.Linq;

using System.Drawing.Imaging;
using PdfSharp.Drawing;
using PdfSharp.Pdf;
using System;
using System.Diagnostics;
using System.Drawing;
using System.IO;
using System.Windows.Forms;


namespace Choque_Estructural
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();

            comboBox1.Items.AddRange(new object[] { "170", "230", "280" });
            comboBox1.SelectedIndex = 0;
            comboBox2.Items.AddRange(new object[] { "175"});
            comboBox2.SelectedIndex = 0;
            comboBox3.Items.AddRange(new object[] { "4200" });
            comboBox3.SelectedIndex = 0;
            comboBox6.Items.AddRange(new object[] { "0,5", "1", "1,5", "2" });
            comboBox6.SelectedIndex = 1;
            comboBox7.Items.AddRange(new object[] { "12", "16", "20", "25" });
            comboBox7.SelectedIndex = 0;
            comboBox8.Items.AddRange(new object[] { "8", "12", "16", "20" });
            comboBox8.SelectedIndex = 0;

            // Agrega opciones a los comboBox 4 y 5
            for (int i = 20; i <= 100; i += 5)
            {
                comboBox4.Items.Add(i.ToString());
                comboBox5.Items.Add(i.ToString());
            }
            comboBox4.SelectedIndex = 0;
            comboBox5.SelectedIndex = 0;

            VerificarMultiplicacionComboBox();
            comboBox4.SelectedIndexChanged += comboBox4_SelectedIndexChanged;
            comboBox5.SelectedIndexChanged += comboBox5_SelectedIndexChanged;

            textBox5.Text = "0";
            textBox5.SelectAll();

            label35.Text = "20cm";
            label79.Text = "2";
        }
        private void ValidarNumerosTextBox(TextBox textBox)
        {
            // Obtener el texto del textbox
            string text = textBox.Text;

            // Eliminar los caracteres no permitidos
            text = new string(text.Where(c => char.IsDigit(c) || c == ',' || c == '-').ToArray());

            // Asignar el texto modificado al textbox
            textBox.Text = text;

            // Establecer el cursor al final del textbox
            textBox.SelectionStart = textBox.TextLength;

            // Actualizar el valor de la variable correspondiente
            double valor;
            double.TryParse(text, out valor);
            textBox.Tag = valor;
        }

        private void ActualizarResultado()
        {
            double valorTextBox6 = textBox6.Tag != null ? (double)textBox6.Tag : 0;
            double valorComboBox2 = double.Parse(comboBox2.SelectedItem.ToString());

            double resultado = 0;
            if (valorComboBox2 != 0)
            {
                resultado = (valorTextBox6 * 2.1) / valorComboBox2;
            }

            resultado = Math.Abs(resultado);
            label23.Text = resultado.ToString();
            label23.Tag = resultado;
        }

        // calculo de columna 

        private void comboBox4_SelectedIndexChanged(object sender, EventArgs e)
        {
            VerificarMultiplicacionComboBox();
            ActualizarLabel27();
            label35.Text = comboBox4.SelectedItem.ToString() + " cm";
            ActualizarLabel36();
            ActualizarLabel51();

        }

        private void comboBox5_SelectedIndexChanged(object sender, EventArgs e)
        {
            VerificarMultiplicacionComboBox();
            ActualizarLabel27();
            ActualizarLabel51();
        }

        private void VerificarMultiplicacionComboBox()
        {
            // Obtener valores de comboBox4, comboBox5 y label23
            double valorComboBox4 = double.Parse(comboBox4.SelectedItem.ToString());
            double valorComboBox5 = double.Parse(comboBox5.SelectedItem.ToString());
            double valorLabel23 = label23.Tag != null ? (double)label23.Tag : 0;

            // Realizar la multiplicación de comboBox4 y comboBox5
            double multiplicacion = valorComboBox4 * valorComboBox5;

            // Verificar si la multiplicación es mayor que el valor de label23
            if (multiplicacion > valorLabel23)
            {
                label26.Text = "Verifica";
            }
            else
            {
                label26.Text = "No verifica";
            }

            // Actualizar el valor de la etiqueta label26.Tag con la multiplicación actual
            label26.Tag = multiplicacion;
        }

        // calculo de columna 
        private void ActualizarLabel27()
        {
            int valorComboBox4 = int.Parse(comboBox4.SelectedItem.ToString());
            int valorComboBox5 = int.Parse(comboBox5.SelectedItem.ToString());
            int suma = valorComboBox4 * valorComboBox5;
            label27.Text = suma.ToString();
        }
        // calculo de columna

        // calculo de pandeo
        private void ActualizarLabel36()
        {
            double valorComboBox4 = comboBox4.SelectedItem != null ? double.Parse(comboBox4.SelectedItem.ToString()) : 0;
            double valorComboBox6 = comboBox6.SelectedItem != null ? double.Parse(comboBox6.SelectedItem.ToString()) : 0;

            double valorTextBox5 = textBox5.Tag != null ? (double)textBox5.Tag : 0;
            double suma = 0;
            if (valorComboBox4 != 0)
            {
                suma = (double)((valorComboBox6 * valorTextBox5 * 3.47) / (valorComboBox4 / 100));
            }
            label36.Text = suma.ToString("F2");
            label40.Text = "λ = " + suma.ToString("F2");
            if (suma >20 && suma < 70 ) 
            {
            label47.Text = "λ =" + suma.ToString("F2") + "<λlim Verifica";

            } else
            {
                label47.Text = "λ =" + suma.ToString("F2") + "<λlim No Verifica";
            }
        }
        // calculo de pandeo
        // Calculo de Condicion 2
        private void ActualizarLabel46()
        {
            double valorTextBox3 = 0;
            double valorTextBox4 = 0;

            // Analizar el valor de textBox3
            try
            {
                valorTextBox3 = double.Parse(textBox3.Text);
            }
            catch (FormatException)
            {
                valorTextBox3 = 0;
            }

            // Analizar el valor de textBox4
            try
            {
                valorTextBox4 = double.Parse(textBox4.Text);
            }
            catch (FormatException)
            {
                valorTextBox4 = 0;
            }

            double suma = 0;
            if (valorTextBox3 != 0)
            {
                suma = (double)(45 - (25 * (valorTextBox4 / valorTextBox3)));
            }
            label46.Text = suma.ToString("F2");
        }

        // Calculo de Condicion 2

        //Calculo de m
        private void ActualizarLabel51()
        {
            double valorComboBox2 = comboBox2.SelectedItem != null ? double.Parse(comboBox2.SelectedItem.ToString()) : 0;
            double valorComboBox3 = comboBox3.SelectedItem != null ? double.Parse(comboBox3.SelectedItem.ToString()) : 0;
            double valorComboBox4 = comboBox4.SelectedItem != null ? double.Parse(comboBox4.SelectedItem.ToString()) : 0;
            double valorComboBox5 = comboBox5.SelectedItem != null ? double.Parse(comboBox5.SelectedItem.ToString()) : 0;
            double valorComboBox7 = comboBox7.SelectedItem != null ? double.Parse(comboBox7.SelectedItem.ToString()) : 12;
            double valorComboBox8 = comboBox8.SelectedItem != null ? double.Parse(comboBox8.SelectedItem.ToString()) : 8;
            double valorTextBox3 = 0;
            // Analizar el valor de textBox3
            try
            {
                valorTextBox3 = double.Parse(textBox3.Text);
            }
            catch (FormatException)
            {
                valorTextBox3 = 0;
            }
            double valorTextBox6 = 0;
            // Analizar el valor de textBox3
            try
            {
                valorTextBox6 = double.Parse(textBox6.Text);
            }
            catch (FormatException)
            {
                valorTextBox6 = 0;
            }
            double valorTextBox7 = 0;
            // Analizar el valor de textBox3
            try
            {
                valorTextBox7 = double.Parse(textBox7.Text);
            }
            catch (FormatException)
            {
                valorTextBox7 = 0;
            }

            string formato = "{0} Cm . ({1} Cm)² . {2} kg/m";
            string formato2 = "{0} kg/m² / {1} kg/m²";
            string resultado = string.Format(formato, valorComboBox5, valorComboBox4, valorComboBox2 );
            string resultado2 = string.Format(formato2, valorComboBox3, valorComboBox2);
            label51.Text = resultado;
            label66.Text = resultado2;
            double suma = 0;
            if (valorComboBox5 != 0 || valorComboBox4 != 0 || valorComboBox2 != 0  )
            {
                suma = (double)((valorTextBox3*100) / (valorComboBox5* (valorComboBox4* valorComboBox4) * valorComboBox2));
            }
            double suma2 = 0;
            if (valorComboBox5 != 0 || valorComboBox4 != 0 || valorComboBox2 != 0 || valorTextBox6 != 0)
            {
                suma2 = (double)((valorTextBox6) / (valorComboBox5 * valorComboBox4 *  valorComboBox2));
            }
            double suma3 = 0;
            if (valorComboBox2 != 0 || valorComboBox3 != 0 || valorComboBox4 != 0 || valorComboBox5 != 0 || valorTextBox7 != 0)
            {
                suma3 = (double)((valorComboBox4* valorComboBox5* valorTextBox7) / (valorComboBox3 / valorComboBox2));
            }

            label53.Text = suma.ToString("F2");
            label54.Text = suma2.ToString("F2");
            label67.Text = valorTextBox7+" . " + valorComboBox4 + " Cm . "+ valorComboBox5 + " Cm =";
            label68.Text = suma3.ToString("F2") + " Cm²";
            label70.Text = valorComboBox4 * valorComboBox5 + " Cm² . 4,5% = ";
            label71.Text = (valorComboBox4 * valorComboBox5 *4.5/100) + " Cm²";
            label74.Text = valorComboBox4 * valorComboBox5 + " Cm² . 4/1000 = ";
            label75.Text = (valorComboBox4 * valorComboBox5 * 4 / 1000) + " Cm²";
            label86.Text = valorComboBox5.ToString("F2") + " Cm";
            label87.Text = (valorComboBox8*1.2).ToString("F2") + " Cm";
            if (valorComboBox5> valorComboBox8)
            {
                label89.Text = valorComboBox5.ToString("") + " Cm";
            } else
            {
                label89.Text = Math.Ceiling(valorComboBox8 * 1.2).ToString("") + " Cm";
            }

            double? as1 = null; // declaración como un valor anulable

            if ((valorComboBox4 * valorComboBox5 * 4 / 1000)> suma3)
            {
                label77.Text = (valorComboBox4 * valorComboBox5 * 4 / 1000) + " Cm²";
                as1 = (valorComboBox4 * valorComboBox5 * 4 / 1000);
            } else
            {
                label77.Text = suma3.ToString("F2") + " Cm²";
                as1 = suma3;
            }
            // 12-16-20-25
            double factorDeRedondeo;
            switch (valorComboBox7)
            {
                case 12:
                    factorDeRedondeo = 1.13;
                    break;
                case 16:
                    factorDeRedondeo = 2.01;
                    break;
                case 20:
                    factorDeRedondeo = 3.14;
                    break;
                case 25:
                    factorDeRedondeo = 4.91;
                    break;
                default:
                    label79.Text = "Valor de ComboBox7 no válido";
                    return;
            }

            double resultadoAS;
            if (as1.HasValue)
            {
                resultadoAS = Math.Ceiling(as1.Value / factorDeRedondeo);
                if (resultadoAS < 2)
                {
                    resultadoAS = 2;
                }
                label79.Text = resultadoAS.ToString()+ " Ø";
                label81.Text = (resultadoAS* factorDeRedondeo).ToString("F2")+ " Cm²";
            }
            else
            {
                label79.Text = "Valor de as1 es nulo";
            }


            // 12-16-20-25

        }

        //Calculo de m

        private void comboBox1_SelectedIndexChanged(object sender, EventArgs e)
        {

        }

        private void textBox2_TextChanged(object sender, EventArgs e)
        {

        }

        private void textBox6_TextChanged(object sender, EventArgs e)
        {
            ValidarNumerosTextBox(textBox6);
            ActualizarResultado();
            label21.Text = textBox6.Text + " Kg.m . 2,1 =";
            label57.Text = "- "+textBox6.Text + " Kg.m ";
            VerificarMultiplicacionComboBox();
            ActualizarLabel51();
        }


        private void textBox5_TextChanged(object sender, EventArgs e)
        {
            ValidarNumerosTextBox(textBox5);
            double comboBoxValue, textBoxValue;
            if (Double.TryParse(comboBox6.Text, out comboBoxValue) && Double.TryParse(textBox5.Text, out textBoxValue))
            {
                label30.Text = "Sk = " + comboBox6.Text + " . " + textBox5.Text + "m = " + (comboBoxValue * textBoxValue).ToString()+ "m";
                label34.Text = "3,47 . " + (comboBoxValue * textBoxValue).ToString() + "m =";
            }
            ActualizarLabel36();

        }

        private void textBox4_TextChanged(object sender, EventArgs e)
        {
            ValidarNumerosTextBox(textBox4);
            ActualizarLabel46();
            label44.Text = "45 - (25 . "  + textBox4.Text + " Kg . m ) =";
        }

        private void textBox3_TextChanged(object sender, EventArgs e)
        {
            ValidarNumerosTextBox(textBox3);
            ActualizarLabel46();
            ActualizarLabel51();
            label45.Text = textBox3.Text + " Kg . m";

            double valorNumerico = 0;
            try
            {
                valorNumerico = double.Parse(textBox3.Text) * 100;
            }
            catch (FormatException)
            {
                valorNumerico = 0;
            }
            label50.Text = valorNumerico.ToString() + " Kg . m";
        }




        private void comboBox2_SelectedIndexChanged(object sender, EventArgs e)
        {
            ActualizarResultado();
            ActualizarLabel51();

            label22.Text = comboBox2.SelectedItem.ToString() + "Kg/m²";
        }

        private void label14_Click(object sender, EventArgs e)
        {

        }

        private void Form1_Load(object sender, EventArgs e)
        {

        }

        private void label21_Click(object sender, EventArgs e)
        {

        }

        private void comboBox6_SelectedIndexChanged(object sender, EventArgs e)
        {
            double comboBoxValue, textBoxValue;
            if (Double.TryParse(comboBox6.Text, out comboBoxValue) && Double.TryParse(textBox5.Text, out textBoxValue))
            {
                label30.Text = "Sk = " + comboBox6.Text + " + " + textBox5.Text + "m = " + (comboBoxValue * textBoxValue).ToString()+ "m";
                label34.Text = "3,47 "  + (comboBoxValue * textBoxValue).ToString() + "m";
            }
            ActualizarLabel36();
        }

        private void comboBox5_SelectedIndexChanged_1(object sender, EventArgs e)
        {
            label60.Text = "d1/d2 = 2,5 Cm / " + comboBox5.SelectedItem.ToString() + " Cm = " + (2.5 / Convert.ToDouble(comboBox5.SelectedItem)).ToString("F3");
            
            double selectedValue = 2.5/ Convert.ToDouble(comboBox5.SelectedItem);
            if (selectedValue <= 0.05)
            {
                label62.Text = "3";
            }
            else if (selectedValue <= 0.1)
            {
                label62.Text = "2";
            }
            else
            {
                label62.Text = "3";
            }

        }

        private void textBox7_TextChanged(object sender, EventArgs e)
        {
            ValidarNumerosTextBox(textBox7);
            ActualizarLabel51();
        }

        private void comboBox3_SelectedIndexChanged(object sender, EventArgs e)
        {
            ActualizarLabel51();
        }

        private void comboBox4_SelectedIndexChanged_1(object sender, EventArgs e)
        {

        }

        private void comboBox7_SelectedIndexChanged(object sender, EventArgs e)
        {
            ActualizarLabel51();
        }

        private void comboBox8_SelectedIndexChanged(object sender, EventArgs e)
        {
            ActualizarLabel51();
        }

        private void guardarComoToolStripMenuItem_Click(object sender, EventArgs e)
        {
            // Crear un objeto SaveFileDialog
            SaveFileDialog saveFileDialog = new SaveFileDialog();

            // Establecer las opciones del diálogo
            saveFileDialog.Filter = "Archivos PDF (*.pdf)|*.pdf";
            saveFileDialog.FilterIndex = 0;
            saveFileDialog.RestoreDirectory = true;

            // Mostrar el diálogo y obtener el resultado
            DialogResult result = saveFileDialog.ShowDialog();

            // Si el usuario hizo clic en el botón "Guardar"
            if (result == DialogResult.OK)
            {
                // Obtener la ruta y el nombre del archivo seleccionado
                string fileName = saveFileDialog.FileName;

                // Crear un objeto PDF
                PdfDocument document = new PdfDocument();

                // Crear una nueva página PDF con el tamaño de la ventana de la aplicación
                PdfPage page = document.AddPage();
                page.Width = this.Width;
                page.Height = this.Height;

                // Crear un objeto XGraphics para dibujar en la página PDF
                XGraphics gfx = XGraphics.FromPdfPage(page);

               

                // Guardar el archivo PDF en disco
                document.Save(fileName);

                // Abrir el archivo PDF con la aplicación predeterminada
                Process.Start(fileName);
            }
        }

        private void acercaDeCdAToolStripMenuItem_Click(object sender, EventArgs e)
        {
            
        }
    }
}
