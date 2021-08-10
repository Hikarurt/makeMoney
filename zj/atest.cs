using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
//using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace zj
{
    public partial class atest : Form
    {
        public atest()
        {
            InitializeComponent();
        }

        private void atest_Load(object sender, EventArgs e)
        {
            String sql_dwxx = "select dwdm,dwmc from t_dwxx order by dwdm asc";
            AccessHelper AccessHelper = new AccessHelper();
            DataSet ds = AccessHelper.getDataSet(sql_dwxx);

            comboBox1.DataSource = ds.Tables[0];
            comboBox1.ValueMember = "dwdm";
            comboBox1.DisplayMember = "dwmc";
        }

        private void button1_Click(object sender, EventArgs e)
        {
            String selectedvalue = comboBox1.SelectedValue.ToString();
            MessageBox.Show(selectedvalue);
        }

        private void button2_Click(object sender, EventArgs e)
        {
            //MessageBox.Show(comboBox1.ValueMember.IndexOf(textBox1.Text).ToString());
            comboBox1.SelectedValue = textBox1.Text;
        }
    }
}
