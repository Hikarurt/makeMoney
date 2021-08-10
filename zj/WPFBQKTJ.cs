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
    public partial class WPFBQKTJ : Form
    {
        public string filterStr = "";
        public WPFBQKTJ()
        {

            InitializeComponent();
            this.WindowState = FormWindowState.Maximized;
            Load += new EventHandler(FrmBcardWPFBQKTJ_Load);
        }
        private void FrmBcardWPFBQKTJ_Load(object sender, EventArgs e)
        {
            cbS.DataSource = null;
            GetAllDataRefreshGridView();
        }

        private void button4_Click(object sender, EventArgs e)
        {
            folderBrowserDialog1.SelectedPath = "";
            if (folderBrowserDialog1.ShowDialog() == DialogResult.OK)
                textBox1.Text = folderBrowserDialog1.SelectedPath;
        }
        public void GetAllDataRefreshGridView()
        {
            select_report select_report = new select_report();

           DataTable dt = new DataTable();
            AccessHelper AccessHelper = new AccessHelper();

            //统计相关信息
            String sql_select_yhzhtj = "SELECT   t_dwxx.dwdm, t_dwxx.dwmc,t_dwxx.szss, t_dwxx.szs,Switch(t_dwxx.dwjb='','0',t_dwxx.dwjb='军委机关部门','1',t_dwxx.dwjb='正战区级','2',t_dwxx.dwjb='副战区级','3',t_dwxx.dwjb='正军级','4',t_dwxx.dwjb='副军级','5',t_dwxx.dwjb='正师级','6',t_dwxx.dwjb='副师级','7',t_dwxx.dwjb='正团级','8',t_dwxx.dwjb='副团级','9',True,'10') AS dwjb, Sum(t_lctc.kysl) AS kysl ,'0' as jysz,'' as 区域接收点 FROM t_lctc LEFT JOIN t_dwxx ON t_lctc.dwdm = t_dwxx.dwdm where czfs='拟移交物品'  " + filterStr + "  GROUP BY t_dwxx.szss, t_dwxx.szs, dwjb,t_dwxx.dwmc, t_dwxx.dwdm ";
            DataSet ds = AccessHelper.getDataSet(sql_select_yhzhtj);
            for (int m = 0; m < 8; m++)
            {
                dt.Columns.Add();
            }
            for (int i = 0; i < ds.Tables[0].Rows.Count; i++)
            {
                dt.Rows.Add();
                for (int j = 0; j < 7; j++)
                {
                    dt.Rows[i][j] = "0";
                }
                dt.Rows[i][0] = (i + 1).ToString();
                dt.Rows[i][1] = ds.Tables[0].Rows[i]["dwmc"].ToString(); //单位名称

                //}
                dt.Rows[i][2] = ds.Tables[0].Rows[i]["szss"].ToString(); //所在省
                dt.Rows[i][3] = ds.Tables[0].Rows[i]["szs"].ToString(); //所在市
                string dwjb = string.Empty;
                if (!string.IsNullOrEmpty(ds.Tables[0].Rows[i]["dwjb"].ToString()))
                {
                    switch (ds.Tables[0].Rows[i]["dwjb"].ToString())
                    {
                        case "0":
                            dwjb = "";
                            break;
                        case "1":
                            dwjb = "军委机关部门";
                            break;

                        case "2":
                            dwjb = "正战区级";
                            break;
                        case "3":
                            dwjb = "副战区级";
                            break;
                        case "4":
                            dwjb = "正军级";
                            break;
                        case "5":
                            dwjb = "副军级";
                            break;
                        case "6":
                            dwjb = "正师级";
                            break;
                        case "7":
                            dwjb = "副师级";
                            break;
                        case "8":
                            dwjb = "正团级";
                            break;
                        case "9":
                            dwjb = "副团级";
                            break;
                        default:
                            dwjb = "营以下单位";
                            break;
                    }
                }
                dt.Rows[i][4] = dwjb; //单位级别
                dt.Rows[i][5] = Convert.ToDecimal(ds.Tables[0].Rows[i]["kysl"].ToString()).ToString("0.#####"); //商品数量
                string sql_jy = string.Format("select sum(kysl) as jysz from t_lctc where dwdm='{0}' and lb='金银' and jldw='克' and  czfs='拟移交物品'  " + filterStr + " ", ds.Tables[0].Rows[i]["dwdm"].ToString());
                DataTable dt_jy = AccessHelper.getDataSet(sql_jy).Tables[0];

                if (dt_jy.Rows.Count > 0)
                {
                    if(!string.IsNullOrEmpty(dt_jy.Rows[0]["jysz"].ToString()))
                    {
                        dt.Rows[i][6] = "金银含量：" + Convert.ToDecimal(dt_jy.Rows[0]["jysz"]).ToString("0.#####") + "克";
                    }
                    else
                    {
                        dt.Rows[i][6] = "";
                    }
                }
            }
            dataGridView1.DataSource = dt;
            dataGridView1.Columns[0].HeaderCell.Value = "序号";
            dataGridView1.Columns[1].HeaderCell.Value = "单位名称";
            dataGridView1.Columns[2].HeaderCell.Value = "所在省";
            dataGridView1.Columns[3].HeaderCell.Value = "所在市";
            dataGridView1.Columns[4].HeaderCell.Value = "单位级别";
            dataGridView1.Columns[5].HeaderCell.Value = "物品移交数量";
            dataGridView1.Columns[6].HeaderCell.Value = "备注";
            dataGridView1.Columns[7].Visible = false;

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
            dt1.Columns.Add(new DataColumn("省", typeof(string)));
            dt1.Columns.Add(new DataColumn("市", typeof(string)));
            dt1.Columns.Add(new DataColumn("单位级别", typeof(string)));
            dt1.Columns.Add(new DataColumn("单位个数", typeof(string)));
            dt1.Columns.Add(new DataColumn("物品移交数量", typeof(string)));
            dt1.Columns.Add(new DataColumn("备注", typeof(string)));

            string sql_Sumsl = "SELECT Switch(t_dwxx.dwjb='','0',t_dwxx.dwjb='军委机关部门','1',t_dwxx.dwjb='正战区级','2',t_dwxx.dwjb='副战区级','3',t_dwxx.dwjb='正军级','4',t_dwxx.dwjb='副军级','5',t_dwxx.dwjb='正师级','6',t_dwxx.dwjb='副师级','7',t_dwxx.dwjb='正团级','8',t_dwxx.dwjb='副团级','9',True,'10') AS dwjb, t_dwxx.szss, t_dwxx.szs,''as dwgs ,Sum(t_lctc.kysl) AS kysl  FROM t_lctc LEFT JOIN t_dwxx ON t_lctc.dwdm = t_dwxx.dwdm where czfs='拟移交物品'  " + filterStr + "  GROUP BY t_dwxx.szss, t_dwxx.szs, t_dwxx.dwjb;";
            DataTable dt_Sumsl = AccessHelper.getDataSet(sql_Sumsl).Tables[0];
            if (dt_Sumsl.Rows.Count > 0)
            {
                int xh = 0;

                for (int i = 0; i < dt_Sumsl.Rows.Count; i++)
                {
                    xh++;
                    string dwjb = string.Empty;
                    if (!string.IsNullOrEmpty(dt_Sumsl.Rows[i]["dwjb"].ToString()))
                    {
                        switch (dt_Sumsl.Rows[i]["dwjb"].ToString())
                        {
                            case "0":
                                dwjb = "";
                                break;
                            case "1":
                                dwjb = "军委机关部门";
                                break;

                            case "2":
                                dwjb = "正战区级";
                                break;
                            case "3":
                                dwjb = "副战区级";
                                break;
                            case "4":
                                dwjb = "正军级";
                                break;
                            case "5":
                                dwjb = "副军级";
                                break;
                            case "6":
                                dwjb = "正师级";
                                break;
                            case "7":
                                dwjb = "副师级";
                                break;
                            case "8":
                                dwjb = "正团级";
                                break;
                            case "9":
                                dwjb = "副团级";
                                break;
                            default:
                                dwjb = "营以下单位";
                                break;
                        }
                    }
                    string sql_dwjbgs = string.Format("select count(1) as zs from (SELECT  t_dwxx.dwmc, t_dwxx.szss, t_dwxx.szs, t_dwxx.dwjb  FROM t_lctc LEFT JOIN t_dwxx ON t_lctc.dwdm = t_dwxx.dwdm  where  czfs='拟移交物品'  " + filterStr + " GROUP BY t_dwxx.szss, t_dwxx.szs, dwjb, t_dwxx.dwmc) AS A where A.szss = '{0}' and A.szs = '{1}' and A.dwjb = '{2}'", dt_Sumsl.Rows[i]["szss"], dt_Sumsl.Rows[i]["szs"].ToString(), dwjb);
                    DataTable dt_dwjbgs = AccessHelper.getDataSet(sql_dwjbgs).Tables[0];
                    if (dt_dwjbgs.Rows.Count > 0)
                    {
                        dt_Sumsl.Rows[i]["dwgs"] = dt_dwjbgs.Rows[0]["zs"].ToString();
                    }
                    else
                    {
                        dt_Sumsl.Rows[i]["dwgs"] = 0;
                    }
                    DataRow dr;
                    dr = dt1.NewRow();
                    dr["序号"] = xh;
                    dr["省"] = dt_Sumsl.Rows[i]["szss"].ToString();
                    if (dt_Sumsl.Rows[i]["szss"].ToString() == dt_Sumsl.Rows[i]["szs"].ToString())
                    {
                        dr["市"] = string.Empty;
                    }
                    else
                    {
                        dr["市"] = dt_Sumsl.Rows[i]["szs"].ToString();
                    }
                    dr["单位级别"] = dwjb;
                    dr["单位个数"] = dt_Sumsl.Rows[i]["dwgs"].ToString();
                    dr["物品移交数量"] = dt_Sumsl.Rows[i]["kysl"];
                    dr["备注"] = string.Empty;
                    dt1.Rows.Add(dr);
                }
            }
            else  {
                MessageBox.Show("统计数据为空，不能导出！");
                return;
            }
            ExcelUI.OpenExcel_WPFBQKTJ(dt1, Application.StartupPath + "\\report\\A30\\留存名贵特产类物品分布情况表.xlsx", textBox1.Text + "\\留存名贵特产类物品分布情况表.xlsx", 3, 0, 7);
            MessageBox.Show("导出成功!");
            label1.Visible = false;

        }

        private void comboBox_szss_SelectedIndexChanged(object sender, EventArgs e)
        {
            AccessHelper m_accessHelper = new AccessHelper();
            string smc = comboBox_szss.Text;
            if (!string.IsNullOrEmpty(smc))
            {
                string sql = "select mc from t_tcwpzdb where (lb='省' or lb='县') and fjbh='" + smc + "' order by ID";
                DataTable dt = m_accessHelper.getDataSet(sql).Tables[0];
                if (dt.Rows.Count > 0)
                {
                    DataTable dt1 = new DataTable();
                    cbS.DataSource = dt1;
                    cbS.Visible = true;
                    cbS.DataSource = dt;
                    cbS.ValueMember = "mc";
                    cbS.DisplayMember = "mc";
                }
                else
                {
                    cbS.Visible = false;
                    //cbS.Text = "";
                    //cbX.Text = "";
                }
            }
        }

        private void button1_Click(object sender, EventArgs e)
        {
            filterStr = string.Empty;
            string szss= comboBox_szss.Text;
            string szs= cbS.Text;
            string dwjb = comboBox_dwjb.Text;
            string dwmc = tbDWMC.Text;
            if (!string.IsNullOrEmpty(szss))
            {
                filterStr += "and szss ='" + szss + "'";
            }
            if (!string.IsNullOrEmpty(szs))
            {
                filterStr += "and szs ='" + szs + "'";
            }
            if (!string.IsNullOrEmpty(dwmc))
            {
                filterStr += "and dwmc  like '%" + dwmc + "%'";
            }
            if (!string.IsNullOrEmpty(dwjb))
            {
                filterStr += "and dwjb ='" + dwjb + "'";
            }
            GetAllDataRefreshGridView();
        }

        private void button5_Click(object sender, EventArgs e)
        {
            comboBox_szss.Items.Add("");
            comboBox_szss.Text = "";
            tbDWMC.Text = "";
            cbS.DataSource = null;

            comboBox_dwjb.Items.Add("");
            comboBox_dwjb.Text = "";
        }

        private void button3_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void button6_Click(object sender, EventArgs e)
        {



            DataTable dt = dataGridView1.DataSource as DataTable;
             dt.Columns[0].ColumnName ="序号";
             dt.Columns[1].ColumnName ="单位名称";
             dt.Columns[2].ColumnName ="所在省";
             dt.Columns[3].ColumnName ="所在市";
             dt.Columns[4].ColumnName ="单位级别";
             dt.Columns[5].ColumnName = "物品移交数量";
             dt.Columns[6].ColumnName = "备注";
             dt.Columns[7].ColumnName = "区域接收点";
            ExcelUI ExcelUI = new ExcelUI();
            ExcelUI.ExportExcel(dt, "");
        }
    }
}
