using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace C_
{
    public partial class Trangchu : Form
    {
        public Trangchu()
        {
            InitializeComponent();
        }

        private void button1_Click(object sender, EventArgs e)
        {

            Sinhvien m = new Sinhvien();
            m.ShowDialog();
            this.Show();
        }

        private void button2_Click(object sender, EventArgs e)
        {
            FormLopHoc l = new FormLopHoc();
            l.ShowDialog();
            this.Show();
        }
    }
}
