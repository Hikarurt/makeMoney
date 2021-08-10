using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace zj
{
    public partial class PLXGSJ : Form
    {
        public  bool isok=false;
        public PLXGSJ()
        {
            InitializeComponent();
            cbLB.Text = "请选择";
            cbCZFS.Text = "请选择";
        }

        private void button3_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            string ids = SelcetFrmWP.ids;
            string xgzi = string.Empty;
           
            if (cbLB.Text != "请选择")
            {
                xgzi += "lb='" + cbLB.Text + "' ";
            }
            if (cbCZFS.Text != "请选择")
            {
                if (cbLB.Text != "请选择")
                {
                    xgzi += ",";
                }
                    xgzi += "czfs='" + cbCZFS.Text + "' ";
            }
            if (checkBox1.Checked == true)
            {
                if (cbLB.Text != "请选择"|| cbCZFS.Text != "请选择")
                {
                    xgzi += ",";
                }
                xgzi += "zz=dj*sl ";
            }
            if (!string.IsNullOrEmpty(xgzi))
            {
                AccessHelper m_accessHelper = new AccessHelper();
                string sql_update = string.Format("update t_lctc set "+ xgzi+"where ID in({0})",ids);
                m_accessHelper.ExcueteCommand(sql_update);
                MessageBox.Show("修改成功！", "系统提示", MessageBoxButtons.OK, MessageBoxIcon.Information);
                isok = true;
                this.Close();
                return;
            }
            else
            {
                MessageBox.Show("请选择需要修改的选项！", "系统提示", MessageBoxButtons.OK, MessageBoxIcon.Information);
                return;
            }
        }
    }
}
