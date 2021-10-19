using System;
using System.Windows.Forms;

namespace NN
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
            panel2.Hide();
            panel3.Hide();
        }

        private void button1_Click(object sender, EventArgs e)
        {

        }

        private void button4_Click(object sender, EventArgs e)
        {

        }

        private void netzwerkAusfuehren(object sender, EventArgs e)
        {
            panel2.Hide();
            panel3.Show();
        }

        private void datensatzGenerieren(object sender, EventArgs e)
        {
            panel3.Hide();
            panel2.Show();
        }

        private void singleThreaded(object sender, EventArgs e)
        {
            DatasetGenerator d = new DatasetGenerator(int.Parse(textBox1.Text));
            d.WriteToFile(int.Parse(textBox4.Text));
            panel2.Hide();
        }

        private void label3_Click(object sender, EventArgs e)
        {

        }

        private void octaArray(object sender, EventArgs e)
        {
            DatasetGenerator d = new DatasetGenerator(int.Parse(textBox1.Text));
            d.ThreadedWriteToFileArray(int.Parse(textBox4.Text));
            panel2.Hide();
        }

        private void octaFile(object sender, EventArgs e)
        {
            DatasetGenerator d = new DatasetGenerator(int.Parse(textBox1.Text));
            d.ThreadedWriteToFile(int.Parse(textBox4.Text));
            panel2.Hide();
        }

        private void realTime(object sender, EventArgs e)
        {
            Netzwerk n = new Netzwerk();
            if (checkBox2.Checked)
            {
                n.ImportWeightsFromFile(textBox6.Text);
            }
            n.MutierenRT(double.Parse(textBox5.Text), int.Parse(textBox2.Text));
            if (checkBox1.Checked)
            {
                n.WriteWeightsToFile();
            }
        }
    }
}
