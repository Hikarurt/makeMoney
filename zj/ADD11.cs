using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Data.OleDb;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace zj
{
    public partial class ADD11 : Form
    {
        public ADD11()
        {
            InitializeComponent();
        }

        private void button1_Click(object sender, EventArgs e)
        {

            string slq = "insert into t_tcwpzdb(mc, fjbh, lb) values(@MC, @FJBH, @LB)";
            OleDbParameter[] parms = new OleDbParameter[] {
                new OleDbParameter("@MC",OleDbType.VarChar),
                new OleDbParameter("@FJBH",OleDbType.VarChar),
                new OleDbParameter("@LB",OleDbType.VarChar)
            };
            parms[0].Value = textBox1.Text;
            parms[1].Value = textBox2.Text;
            parms[2].Value = textBox3.Text;
            AccessHelper AccessHelper = new AccessHelper();
         int a=    AccessHelper.ExcueteCommand(slq, parms);
            if (a > 0)
            {
                MessageBox.Show("1111");
            }
        }
    }
}
