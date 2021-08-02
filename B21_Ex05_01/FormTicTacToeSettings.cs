using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace B21_Ex05_01
{
    public partial class FormGameSettings : Form
    {

        public string Player1Name
        {
            get { return textBoxPlayer1.Text; }
        }

        public string Player2Name
        {
            get { return textBoxPlayer2.Text; }
        }

        public decimal BoardSize
        {
            get { return numericUpDownRows.Value; }
        }

        public bool twoPlayersMode
        {
            get { return checkBox1.Checked; }
        }

        public FormGameSettings()
        {
            InitializeComponent();
            ShowDialog();
        }

        private void Form1_Load(object sender, EventArgs e)
        {
        
        }

        private void label1_Click(object sender, EventArgs e)
        {

        }
        private void buttonStart_Click(object sender, EventArgs e)
        {
            this.DialogResult = DialogResult.OK;
            this.Close();
            this.Hide();
        }

        private void checkBox1_CheckedChanged(object sender, EventArgs e)
        {
            textBoxPlayer2.Enabled = !textBoxPlayer2.Enabled;
            textBoxPlayer2.ReadOnly = !textBoxPlayer2.ReadOnly;
            if (textBoxPlayer2.Enabled == false)
            {

                textBoxPlayer2.Text = "Computer";
            }
            else
            {
                textBoxPlayer2.Text = " ";
            }
        }

        private void textBox_Player2_TextChanged(object sender, EventArgs e)
        {

        }

        private void numericUpDown1_ValueChanged(object sender, EventArgs e)
        {
            numericUpDownRows.Value = numericUpDownCols.Value;
        }

        private void numericUpDown2_ValueChanged(object sender, EventArgs e)
        {
            numericUpDownCols.Value = numericUpDownRows.Value;

        }

        private void textBox1_TextChanged(object sender, EventArgs e)
        {

        }

        private void label4_Click(object sender, EventArgs e)
        {

        }
    }
}
