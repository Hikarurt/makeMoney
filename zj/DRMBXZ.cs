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
    public partial class DRMBXZ : Form
    {
        public DRMBXZ()
        {
            InitializeComponent();
        }
     

        private void button4_Click(object sender, EventArgs e)
        {
            folderBrowserDialog1.SelectedPath = "";
            if (folderBrowserDialog1.ShowDialog() == DialogResult.OK)
                textBox1.Text = folderBrowserDialog1.SelectedPath;
        }

        private void button2_Click(object sender, EventArgs e)
        {
            OpenFileDialog ofd = new OpenFileDialog();
            ofd.Title = "选择文件";
            ofd.Filter = "Microsoft Excel文件|*.xls;*.xlsx";
            ofd.FilterIndex = 1;
            ofd.DefaultExt = "xls";
            string path = "";
            if (ofd.ShowDialog() == DialogResult.OK)
            {
                if (!ofd.SafeFileName.EndsWith(".xls") && !ofd.SafeFileName.EndsWith(".xlsx"))
                {
                    MessageBox.Show("请选择Excel文件", "文件解析失败!", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    return;
                }
                if (!ofd.CheckFileExists)
                {
                    MessageBox.Show("指定的文件不存在", "请检查！", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    return;
                }
                path = ofd.FileName;
            }
            if (path == "")
            {
                MessageBox.Show("请选择报表输出路径!");
                return;
            }
           ExcelUI.OpenExcel_DRMB(null, Application.StartupPath + "\\report\\A3\\留存名贵特产类物品明细统计表.xlsx", textBox1.Text + "\\留存名贵特产类物品明细统计表.xlsx", 4, 0);
            MessageBox.Show("下载成功!");
            this.Close();
        }

    }
}
