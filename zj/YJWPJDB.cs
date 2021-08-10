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
    public partial class YJWPJDB : Form
    {
        DataTable lctc_All = new DataTable();
        AccessHelper AccessHelper = new AccessHelper();
        public YJWPJDB()
        {
            InitializeComponent();
            this.WindowState = FormWindowState.Maximized;
            Load += new EventHandler(YJWPJDB_Load);
        }
        private void YJWPJDB_Load(object sender, EventArgs e)
        {
            GetAllDataRefreshGridView();
        }

        public void GetAllDataRefreshGridView()
        {
            DataTable dt = new DataTable();
            dt.Columns.Add(new DataColumn("序号", typeof(string)));
            dt.Columns.Add(new DataColumn("单位名称", typeof(string)));
            dt.Columns.Add(new DataColumn("拟移交单位数", typeof(string)));
            dt.Columns.Add(new DataColumn("已移交单位数", typeof(string)));
            dt.Columns.Add(new DataColumn("移交进度", typeof(string)));
            dt.Columns.Add(new DataColumn("拟移交物品数量", typeof(string)));
            dt.Columns.Add(new DataColumn("实际移交物品数量", typeof(string)));
            dt.Columns.Add(new DataColumn("差额", typeof(string)));
            DataRow dr;
            //物单位移交数
            string sql_lctc_xx = "select t_dwxx.dwdm, t_dwxx.dwmc ,t_dwxx.dwbs,t_dwxx.jjqrsl from  t_lctc left join t_dwxx on t_lctc.dwdm =t_dwxx.dwdm  where t_lctc.czfs='拟移交物品' and t_lctc.sl<>0 group by t_dwxx.dwdm, t_dwxx.dwmc,t_dwxx.dwbs,t_dwxx.jjqrsl";
            DataTable dt_lctc_xx = AccessHelper.getDataSet(sql_lctc_xx).Tables[0];
           
            //一级单位信息
            string sql_dwxx = "select  t_dwxx.dwdm,t_dwxx.dwmc from  t_dwxx  where len(t_dwxx.dwdm)=3 order by t_dwxx.dwdm";
            DataTable dt_dwxx = AccessHelper.getDataSet(sql_dwxx).Tables[0];


            if (dt_dwxx.Rows.Count > 0)
            {
                int xh = 0;
                for (int i = 0; i < dt_dwxx.Rows.Count; i++)
                {
                    xh++;
                    string dwdm = dt_dwxx.Rows[i]["dwdm"].ToString();

                    DataTable dt_dwzs = dt_lctc_xx.Select("dwdm like '" + dwdm + "%'").ToList().Count > 0 ? dt_lctc_xx.Select("dwdm like '" + dwdm + "%'").CopyToDataTable() : new DataTable();

                    int yjzs = dt_dwzs.Rows.Count;
                    if (yjzs > 0)
                    {
                        int yjdws = 0;
                        string dh_ids = string.Empty;
                        for (int m = 0; m < dt_dwzs.Rows.Count; m++)
                        {
                            if (!string.IsNullOrEmpty(dt_dwzs.Rows[m]["jjqrsl"].ToString()))
                            {
                                dh_ids += "'";
                                dh_ids += dt_dwzs.Rows[m]["dwdm"].ToString();
                                dh_ids += "',";
                                yjdws++;
                            }
                        }
                        decimal yjjd = yjdws * 100 / yjzs;
                        string kysl_sum = "0";
                        string wpzz_sum = "0";
                        if (!string.IsNullOrEmpty(dh_ids))
                        {
                            dh_ids = dh_ids.Substring(0, dh_ids.Length - 1);
                            //物品交接确认数量
                            string sql_jjqrsl = "select sum(jjqrsl) as 交接确认数量  from t_dwxx where t_dwxx.dwdm in(" + dh_ids + ")";
                            DataTable dt_jjqrsl = AccessHelper.getDataSet(sql_jjqrsl).Tables[0];
                            if (dt_jjqrsl.Rows.Count > 0)
                            {
                                wpzz_sum = dt_jjqrsl.Rows[0][0].ToString();
                            }
                            //物品实际移交数
                            string sql_wpyjsl= "select sum(kysl) as 拟移交物品数量  from t_lctc where t_lctc.czfs='拟移交物品' and t_lctc.dwdm in(" + dh_ids + ")";
                            DataTable dt_wpyjsl = AccessHelper.getDataSet(sql_wpyjsl).Tables[0];
                            if (dt_wpyjsl.Rows.Count > 0)
                            {
                                kysl_sum = dt_wpyjsl.Rows[0][0].ToString();
                            }
                        }

                        //DataTable dt_wpzs = dt_dwxx_xx.Select("dwdm like '" + dwdm + "%'").ToList().Count > 0 ? dt_dwxx_xx.Select("dwdm like '" + dwdm + "%'").CopyToDataTable() : new DataTable();

                        //for (int y = 0; y < dt_wpzs.Rows.Count; y++)
                        //{
                        //    if (string.IsNullOrEmpty(dt_wpzs.Rows[y]["实际移交物品数"].ToString()))
                        //    {
                        //        dt_wpzs.Rows[y]["实际移交物品数"] = "0";
                        //    }
                        //    if (string.IsNullOrEmpty(dt_wpzs.Rows[y]["拟移交物品数量"].ToString()))
                        //    {
                        //        dt_wpzs.Rows[y]["拟移交物品数量"] = "0";
                        //    }

                        //}
                        //var query = from p in dt_wpzs.AsEnumerable()
                        //            group p by p.Field<string>("备注")
                        //      into s
                        //            select new
                        //            {
                        //                kysl = s.Sum(p => p.Field<decimal>("拟移交物品数量")),
                        //                sjyjs = s.Sum(p => p.Field<decimal>("实际移交物品数"))
                        //            };
                        //DataTable dtss = new DataTable();
                        //dtss.Columns.Add(new DataColumn("kysl", typeof(string)));
                        //dtss.Columns.Add(new DataColumn("sjyjs", typeof(double)));
                        //query.ToList().ForEach(p => dtss.Rows.Add(p.kysl, p.sjyjs));

                        dr = dt.NewRow();
                        dr["序号"] = xh;
                        dr["单位名称"] = dt_dwxx.Rows[i]["dwmc"].ToString();
                        dr["拟移交单位数"] = yjzs;
                        dr["已移交单位数"] = yjdws;
                        dr["移交进度"] = yjjd + "%";

                        //decimal kysl_sum = 0;
                        //decimal wpzz_sum = 0;
                        //for (int s = 0; s < dtss.Rows.Count; s++)
                        //{
                        //    kysl_sum = kysl_sum + Convert.ToDecimal(dtss.Rows[s]["kysl"].ToString() == "" ? "0" : dtss.Rows[s]["kysl"].ToString());
                        //    wpzz_sum = wpzz_sum + Convert.ToDecimal(dtss.Rows[s]["sjyjs"].ToString() == "" ? "0" : dtss.Rows[s]["sjyjs"].ToString());
                        //}

                        dr["拟移交物品数量"] = kysl_sum;
                        dr["实际移交物品数量"] = wpzz_sum;
                        dr["差额"] = Convert.ToDecimal(kysl_sum) - Convert.ToDecimal(wpzz_sum);
                        dt.Rows.Add(dr);
                    }
                }
                dataGridView1.DataSource = dt;
                lctc_All = dt;
            }
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

        private void button2_Click(object sender, EventArgs e)
        {
            label1.Visible = true;
            ExcelUI.OpenExcel_WPHZTJB(lctc_All, Application.StartupPath + "\\report\\A30\\向军队资产管理公司移交名贵特产类物品进度表.xlsx", textBox1.Text + "\\向军队资产管理公司移交名贵特产类物品进度表.xlsx", 4, 0, 8);
            MessageBox.Show("导出成功!");
            label1.Visible = false;
        }
    }
}
