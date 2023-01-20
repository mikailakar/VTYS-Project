using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace vtysProject
{
    public partial class Irsaliye : Form
    {
        public Irsaliye()
        {
            InitializeComponent();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            stokDurumu stk1 = new stokDurumu();
            stk1.Show();
            Hide();
        }

        private void Irsaliye_Load(object sender, EventArgs e)
        {

        }
    }
}
