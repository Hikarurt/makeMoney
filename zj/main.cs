using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;

namespace zj
{
    public partial class main : Form
    {
        public main()
        {
            InitializeComponent();
        }

        private void 修改密码ToolStripMenuItem_Click(object sender, EventArgs e)
        {
            password password = new password();
            password.Show();
        }

        private void 单位管理ToolStripMenuItem_Click(object sender, EventArgs e)
        {
            danwei danwei = new danwei();
            danwei.Show();
        }

        private void 生成报表ToolStripMenuItem_Click(object sender, EventArgs e)
        {
            report report = new report();
            report.Show();
        }

        private void 接收报表ToolStripMenuItem_Click(object sender, EventArgs e)
        {
            receive recevice = new receive();
            recevice.Show();
        }

        private void 银行账户情况汇总统计表按账户类别ToolStripMenuItem_Click(object sender, EventArgs e)
        {
            ExcelUI.OpenExcel_A4_cover(Application.StartupPath + "\\report\\A4\\1.xls", Application.StartupPath + "\\export\\封面.xls");
        }

        private void a4报表ToolStripMenuItem_Click(object sender, EventArgs e)
        {
            report_A4 report_A4 = new report_A4();
            report_A4.Show();
        }

        private void a4填报说明ToolStripMenuItem_Click(object sender, EventArgs e)
        {
            ExcelUI.OpenExcel_A4_cover(Application.StartupPath + "\\report\\A4\\2.xls", Application.StartupPath + "\\export\\填报说明.xls");
        }

        private void a3报表主体ToolStripMenuItem_Click(object sender, EventArgs e)
        {
            report_A3 report_A3 = new report_A3();
            report_A3.Show();
        }

        private void a3填报说明ToolStripMenuItem_Click(object sender, EventArgs e)
        {
            ExcelUI.OpenExcel_A4_cover(Application.StartupPath + "\\report\\A3\\2.xls", Application.StartupPath + "\\export\\A3填报说明.xls");
        }

        private void a3报表封面ToolStripMenuItem_Click(object sender, EventArgs e)
        {
            ExcelUI.OpenExcel_A4_cover(Application.StartupPath + "\\report\\A3\\1.xls", Application.StartupPath + "\\export\\A3封面.xls");
        }

        private void main_Load(object sender, EventArgs e)
        {
            String sql_dwxx = "select dwmc from t_dwxx where dwdm='000'";
            AccessHelper AccessHelper = new AccessHelper();
            DataSet ds = AccessHelper.getDataSet(sql_dwxx);
            if (ds.Tables[0].Rows.Count == 0)
            {
                BJDW danwei = new BJDW();
                danwei.MdiParent = this;
                danwei.Show();
            }
        }
    }
}
