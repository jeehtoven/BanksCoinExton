using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace BanksCoinExton
{
    public partial class Client : Form
    {
        public Client()
        {
            InitializeComponent();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            Program p = new Program();
            string hash = p.Transaction(textBox1.Text, textBox2.Text, Convert.ToInt32(textBox3.Text), comboBox1.Text);
            MessageBox.Show("Transaction complete." + Environment.NewLine + "----------" + Environment.NewLine + "Date: " + DateTime.Now.ToString() + Environment.NewLine
                + "Sender: " + textBox1.Text + Environment.NewLine + "Recipient: " + textBox2.Text + Environment.NewLine + "Amount: $" + textBox3.Text + Environment.NewLine + "Category: " + comboBox1.Text
                + Environment.NewLine + "----------Hash---------- " + Environment.NewLine + hash + Environment.NewLine + "-------------------- " + Environment.NewLine + "Have a nice day!");
            this.Close();
        }
    }
}
