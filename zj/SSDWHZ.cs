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
    public partial class SSDWHZ : Form
    {
        public SSDWHZ()
        {
            this.WindowState = FormWindowState.Maximized;
            InitializeComponent();
            select_report select_report = new select_report();
            dataGridView1.DataSource = select_report.report_SUM();
            dataGridView1.Columns[0].HeaderCell.Value = "序号";
            dataGridView1.Columns[1].HeaderCell.Value = "单位（部门）名称";
            dataGridView1.Columns[2].HeaderCell.Value = "类别";
            dataGridView1.Columns[3].HeaderCell.Value = "数量";
            dataGridView1.Columns[4].HeaderCell.Value = "总值";
            dataGridView1.Columns[5].HeaderCell.Value = "堪用数量";
            dataGridView1.Columns[6].HeaderCell.Value = "可变现价值";
            dataGridView1.Columns[7].HeaderCell.Value = "处置方式";
        }

        private void button2_Click(object sender, EventArgs e)
        {
            AccessHelper AccessHelper = new AccessHelper();
            if (textBox1.Text == "")
            {
                MessageBox.Show("请选择报表输出路径!");
                return;
            }
            label1.Visible = true;
            DataTable dt1 = new DataTable();
            dt1.Columns.Add(new DataColumn("序号", typeof(string)));
            dt1.Columns.Add(new DataColumn("单位名称", typeof(string)));
            dt1.Columns.Add(new DataColumn("类别", typeof(string)));
            dt1.Columns.Add(new DataColumn("数量", typeof(string)));
            dt1.Columns.Add(new DataColumn("计量单位", typeof(string)));
            dt1.Columns.Add(new DataColumn("总值", typeof(string)));
            dt1.Columns.Add(new DataColumn("堪用数量", typeof(string)));
            dt1.Columns.Add(new DataColumn("可变现价值", typeof(string)));
            dt1.Columns.Add(new DataColumn("处置方式", typeof(string)));
            dt1.Columns.Add(new DataColumn("备注", typeof(string)));

            DataRow dr;
            String getzdw = " select dwmc  from t_dwxx where dwdm='000' ";
            DataTable dt_getzdw = AccessHelper.getDataSet(getzdw).Tables[0];
            string sDwmc = "";
            if (dt_getzdw.Rows.Count > 0)
            {
                sDwmc = dt_getzdw.Rows[0]["dwmc"].ToString();
            }
            //第一行标题赋值
            dr = dt1.NewRow();
            dr["序号"] = "单位："+ sDwmc;
            dr["计量单位"] = "统计日期："+ DateTime.Now.ToLongDateString().ToString();
            dr["处置方式"] = "金额单位：元";
            dt1.Rows.Add(dr);
            dr = dt1.NewRow();
            dr["序号"] = "序号";
            dr["单位名称"] = "单位名称";
            dr["类别"] = "类别";
            dr["数量"] = "数量";
            dr["计量单位"] = "计量单位";
            dr["总值"] = "总值";
            dr["堪用数量"] = "堪用数量";
            dr["可变现价值"] = "可变现价值";
            dr["处置方式"] = "处置方式（建议）";
            dr["备注"] = "备注";
            dt1.Rows.Add(dr);
            string sql_sumALL = " SELECT sum(zz) AS zz1,sum(sl) as sll , sum(kysl) AS kysl1, sum(kbxjz) AS kbxjz1 FROM t_lctc where dwdm='000'";
            DataTable dt_sumALL = AccessHelper.getDataSet(sql_sumALL).Tables[0];
            if (dt_sumALL.Rows.Count > 0)
            {
                dr = dt1.NewRow();
                dr["序号"] = "合计";
                dr["单位名称"] = "";
                dr["类别"] = "小计";
                dr["总值"] = dt_sumALL.Rows[0]["zz1"].ToString();
                dr["可变现价值"] = dt_sumALL.Rows[0]["kbxjz1"].ToString();
                dt1.Rows.Add(dr);
                dr = dt1.NewRow();
                dr["序号"] = "一";
                dr["单位名称"] = sDwmc ; 
                dr["类别"] =  "小计";
                dr["总值"] = dt_sumALL.Rows[0]["zz1"].ToString();
                dr["可变现价值"] = dt_sumALL.Rows[0]["kbxjz1"].ToString();
                dt1.Rows.Add(dr);
            }

            string sql_select_yhzhtj = "select t_lctc.lb,t_lctc.czfs,t_lctc.dwdm,t_lctc.jldw ,sum(t_lctc.sl) as sl, sum(t_lctc.zz) as zz, sum(t_lctc.kysl) as kysl, sum(t_lctc.kbxjz) as kbxjz from t_lctc  WHERE t_lctc.dwdm ='000'   group by t_lctc.lb,t_lctc.czfs,t_lctc.dwdm,t_lctc.jldw";
            DataTable ds = AccessHelper.getDataSet(sql_select_yhzhtj).Tables[0];
            if (ds.Rows.Count > 0)
            {
                for (int i = 0; i < ds.Rows.Count; i++)
                {
                    dr = dt1.NewRow();
                    dr["序号"] = "";
                    dr["单位名称"] = "";
                    dr["类别"] = ds.Rows[i]["lb"].ToString();
                    dr["数量"] = ds.Rows[i]["sl"].ToString();
                    dr["计量单位"] = ds.Rows[i]["jldw"].ToString();
                    dr["总值"] = ds.Rows[i]["zz"].ToString();
                    dr["堪用数量"] = ds.Rows[i]["kysl"].ToString();
                    dr["可变现价值"] = ds.Rows[i]["kbxjz"].ToString();
                    dr["处置方式"] = ds.Rows[i]["czfs"].ToString();
                    dr["备注"] = "";
                    dt1.Rows.Add(dr);
                }
            }
            if (dt1 == null)
            {
                MessageBox.Show("统计数据为空，不能导出！");
                return;
            }
            ExcelUI.OpenExcel_A4_3(dt1, Application.StartupPath + "\\report\\A30\\单位汇总统计表.xlsx", textBox1.Text + "\\单位汇总统计表.xlsx", 1, 0,10);
            MessageBox.Show("导出成功!");
            label1.Visible = false;

        }

        private void button4_Click(object sender, EventArgs e)
        {
            folderBrowserDialog1.SelectedPath = "";
            if (folderBrowserDialog1.ShowDialog() == DialogResult.OK)
                textBox1.Text = folderBrowserDialog1.SelectedPath;
        }

        private void button3_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void SSDWHZ_Load(object sender, EventArgs e)
        {
            this.WindowState = FormWindowState.Maximized;
        }
    }
}
