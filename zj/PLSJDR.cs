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
    public partial class PLSJDR : Form
    {
        public bool isOk = false;
        public PLSJDR()
        {
            InitializeComponent();
        }

        private void button4_Click(object sender, EventArgs e)
        {
            OpenFileDialog ofd = new OpenFileDialog();
            ofd.Title = "选择文件";
            ofd.Filter = "Microsoft Excel文件|*.xls;*.xlsx";
            ofd.FilterIndex = 1;
            ofd.DefaultExt = "xls";
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
                textBox1.Text = ofd.FileName;
            }
        }

        private void button2_Click(object sender, EventArgs e)
        {
            string path = textBox1.Text;
            if (string.IsNullOrEmpty(path))
            {
                MessageBox.Show("请选择要导入的文件", "请检查！", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }
            label1.Visible = true;
            DataTable dt = new DataTable();
            AccessHelper m_accessHelper = new AccessHelper();
            try
            {
                dt = AccessHelper.ExcelToDataTable(path, "Sheet1");
                if (dt.Rows.Count > 0)
                {
                    for (int i = 0; i < dt.Rows.Count; i++)
                    {
                        string sql_update = string.Format("update t_lctc set lb='{0}',pm='{1}',ly='{2}',hqsj='{3}',sl='{4}',jldw='{5}',djlx='{6}',dj='{7}',zz='{8}',kysl='{9}',kbxjz='{10}',czfs='{11}',bz='{12}' where wpbs='{13}'",dt.Rows[i]["类别"],dt.Rows[i]["品名"],dt.Rows[i]["来源"], dt.Rows[i]["获取时间"], dt.Rows[i]["数量"], dt.Rows[i]["计量单位"], dt.Rows[i]["单价类型"], dt.Rows[i]["单价"], dt.Rows[i]["总值"], dt.Rows[i]["堪用数量"], dt.Rows[i]["可变现价值"], dt.Rows[i]["处置方式"], dt.Rows[i]["备注"], dt.Rows[i]["物品唯一标识码（不可修改）"]);
                        m_accessHelper.ExcueteCommand(sql_update);
                    }
                    MessageBox.Show("导入成功！");
                    isOk = true;
                    this.Close();
                }
                else
                {
                    MessageBox.Show("导入Excel数据为空", "请检查！", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    return;
                }
            }
            catch
            {
                MessageBox.Show("请选择系统所提供的导入模板", "请检查！", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }
        }

        private void button1_Click(object sender, EventArgs e)
        {
            this.Close();
        }
    }
}
