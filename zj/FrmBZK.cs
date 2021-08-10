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
    public partial class FrmBZK : Form
    {
        public FrmBZK()
        {
            InitializeComponent();
            getreport();
        }

        public void getreport(){
            int bzk = 0, gzkfk = 0;
            string tempSQL = "SELECT left(t_dwxx.dwdm,3) as 单位代码,sum(t_dwxx.bzk) as 军人保障卡数量,sum(t_dwxx.gzkfk) as 工资卡辅卡数量 from t_dwxx group by left(t_dwxx.dwdm,3) order by left(t_dwxx.dwdm,3) asc";
            AccessHelper m_accessHelper = new AccessHelper();
            DataSet ds = m_accessHelper.getDataSet(tempSQL);
            DataTable dt = ds.Tables[0];
            tempSQL = "SELECT distinct left(t_dwxx.dwdm,3) as 单位代码,t_dwxx.dwmc as 单位名称 from t_dwxx where len(t_dwxx.dwdm)=3 order by left(t_dwxx.dwdm,3) asc";
            DataSet ds1 = m_accessHelper.getDataSet(tempSQL);
            DataTable dt1 = ds1.Tables[0];

            DataColumn dc = dt.Columns.Add("序号", typeof(int));
            dt.Columns["序号"].SetOrdinal(0);
            DataColumn dwmc = dt.Columns.Add("单位名称", typeof(String));
            dt.Columns["单位名称"].SetOrdinal(2);
            for (int i = 0; i < dt.Rows.Count; i++)
            {
                dt.Rows[i][0] = i + 1;
                dt.Rows[i][2] = dt1.Rows[i][1];
                bzk = bzk + int.Parse(dt.Rows[i][3].ToString());
                gzkfk = gzkfk + int.Parse(dt.Rows[i][4].ToString());
            }
            dt.Rows.Add();
            dt.Rows[dt.Rows.Count - 1][2] = "合计";
            dt.Rows[dt.Rows.Count - 1][3] = bzk;
            dt.Rows[dt.Rows.Count - 1][4] = gzkfk;
            dataGridView1.DataSource = dt;
            dataGridView1.Columns[0].Width = 60;
            dataGridView1.Columns[2].Width = 200;
            //label1.Text = "小计：保障卡数量  " + bzk.ToString() + "工资卡辅卡数量 " + gzkfk.ToString();
        }

        public void getmx()
        {
            int bzk = 0, gzkfk = 0;
            string tempSQL = "SELECT t_dwxx.dwdm as 单位代码,t_dwxx.dwmc as 单位名称,t_dwxx.bzk as 军人保障卡数量,t_dwxx.gzkfk as 工资卡辅卡数量 from t_dwxx order by t_dwxx.dwdm asc";
            AccessHelper m_accessHelper = new AccessHelper();
            DataSet ds = m_accessHelper.getDataSet(tempSQL);
            DataTable dt = ds.Tables[0];
            DataColumn dc = dt.Columns.Add("序号", typeof(int));
            dt.Columns["序号"].SetOrdinal(0);
            for (int i = 0; i < dt.Rows.Count; i++)
            {
                dt.Rows[i][0] = i + 1;
                bzk = bzk + int.Parse(dt.Rows[i][3].ToString());
                gzkfk = gzkfk + int.Parse(dt.Rows[i][4].ToString());
            }
            dt.Rows.Add();
            dt.Rows[dt.Rows.Count - 1][2] = "合计";
            dt.Rows[dt.Rows.Count - 1][3] = bzk;
            dt.Rows[dt.Rows.Count - 1][4] = gzkfk;
            dataGridView1.DataSource = dt;
            dataGridView1.Columns[0].Width = 60;
            dataGridView1.Columns[2].Width = 200;
        }

        private void button2_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            DataTable dt = dataGridView1.DataSource as DataTable;
            ExcelUI ExcelUI = new ExcelUI();
            ExcelUI.ExportExcel(dt, "");
        }

        private void buttonXZ_Click(object sender, EventArgs e)
        {
            getreport();
        }

        private void button3_Click(object sender, EventArgs e)
        {
            getmx();
        }
    }
}
