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
    public partial class WPMXTJB : Form
    {
        DataTable dt_dc = new DataTable();
        DataTable lctc_dc = new DataTable();
        DataTable lctc_All = new DataTable();
        AccessHelper AccessHelper = new AccessHelper();
        public WPMXTJB()
        {
            InitializeComponent();
            this.WindowState = FormWindowState.Maximized;
            Load += new EventHandler(WPMXTJB_Load);
        }
        private void WPMXTJB_Load(object sender, EventArgs e)
        {
            GetAllDataRefreshGridView();
        }

        public void GetAllDataRefreshGridView()
        {

            DataTable dt = new DataTable();
            dt.Columns.Add(new DataColumn("单位代码", typeof(string)));
            dt.Columns.Add(new DataColumn("序号", typeof(string)));
            dt.Columns.Add(new DataColumn("单位名称", typeof(string)));
            dt.Columns.Add(new DataColumn("处置方式", typeof(string)));
            dt.Columns.Add(new DataColumn("类别", typeof(string)));
            dt.Columns.Add(new DataColumn("数量", typeof(string)));
            dt.Columns.Add(new DataColumn("实际数量", typeof(string)));
            dt.Columns.Add(new DataColumn("堪用数量", typeof(string)));
            dt.Columns.Add(new DataColumn("可变现价值", typeof(string)));
            dt.Columns.Add(new DataColumn("备注", typeof(string)));

            //物品信息总和
            string sql_lctc_xx = "SELECT t_lctc.dwdm, t_lctc.czfs, t_lctc.lb as lb,sum(sl)as 数量,sum(kysl) as 堪用数量,sum(kbxjz) as 可变现价值,sum(jjqrsl) as 实际数量 from  t_lctc   group by t_lctc.dwdm, t_lctc.czfs, t_lctc.lb order by t_lctc.dwdm";
            DataTable dt_lctc_xx = AccessHelper.getDataSet(sql_lctc_xx).Tables[0];

            for (int i = 0; i < dt_lctc_xx.Rows.Count; i++)
            {
                if (dt_lctc_xx.Rows[i]["czfs"].ToString() != "拟移交物品")
                {
                    dt_lctc_xx.Rows[i]["实际数量"] = dt_lctc_xx.Rows[i]["数量"].ToString();
                }
            }
            lctc_dc = dt_lctc_xx;
            //单位信息总和
            string sql_dwxx_xx = "select  t_dwxx.dwdm,t_dwxx.dwmc from  t_dwxx  where len(t_dwxx.dwdm)=3";
            DataTable dt_dwxx_xx = AccessHelper.getDataSet(sql_dwxx_xx).Tables[0];
            dt_dc = dt_dwxx_xx;
            if (dt_dwxx_xx.Rows.Count > 0)
            {
                int xh = 0;
                for (int i = 0; i < dt_dwxx_xx.Rows.Count; i++)
                {
                    string dwdm = dt_dwxx_xx.Rows[i]["dwdm"].ToString();
                    DataTable dw_lctc = dt_lctc_xx.Select("dwdm like '" + dwdm + "%'").ToList().Count > 0 ? dt_lctc_xx.Select("dwdm like '" + dwdm + "%'").OrderBy(m => m["czfs"]).ThenBy(m => m["lb"]).CopyToDataTable() : new DataTable();


                    if (dw_lctc.Rows.Count > 0)
                    {
                        for (int k = 0; k < dw_lctc.Rows.Count; k++)
                        {
                            DataRow dr;
                            if (dw_lctc.Rows.Count == 1)
                            {
                                dr = dt.NewRow();
                                dr["单位代码"] = dt_dwxx_xx.Rows[i]["dwdm"];
                                dr["序号"] = xh;
                                dr["单位名称"] = dt_dwxx_xx.Rows[i]["dwmc"];
                                dr["处置方式"] = dw_lctc.Rows[k]["czfs"];
                                dr["类别"] = dw_lctc.Rows[k]["lb"];
                                dr["数量"] = dw_lctc.Rows[k]["数量"];
                                if (dr["处置方式"].ToString() == "拟移交物品")
                                {
                                    dr["实际数量"] = dw_lctc.Rows[k]["实际数量"];
                                }
                                else
                                {
                                    dr["实际数量"] = dw_lctc.Rows[k]["数量"];
                                }
                                dr["堪用数量"] = dw_lctc.Rows[k]["堪用数量"];
                                dr["可变现价值"] = dw_lctc.Rows[k]["可变现价值"];
                                dr["备注"] = string.Empty;
                                dt.Rows.Add(dr);
                            }
                            else
                            {
                                if (k < dw_lctc.Rows.Count - 1)
                                {
                                    xh++;
                                    dr = dt.NewRow();
                                    dr["单位代码"] = dt_dwxx_xx.Rows[i]["dwdm"];
                                    dr["序号"] = xh;
                                    dr["单位名称"] = dt_dwxx_xx.Rows[i]["dwmc"];
                                    dr["处置方式"] = dw_lctc.Rows[k]["czfs"];
                                    dr["类别"] = dw_lctc.Rows[k]["lb"];
                                    dr["数量"] = dw_lctc.Rows[k]["数量"];
                                    if (dr["处置方式"].ToString() == "拟移交物品")
                                    {
                                        dr["实际数量"] = dw_lctc.Rows[k]["实际数量"];
                                    }
                                    else
                                    {
                                        dr["实际数量"] = dw_lctc.Rows[k]["数量"];
                                    }
                                    dr["堪用数量"] = dw_lctc.Rows[k]["堪用数量"];
                                    dr["可变现价值"] = dw_lctc.Rows[k]["可变现价值"];
                                    dr["备注"] = string.Empty;
                                    if (dw_lctc.Rows[k]["czfs"].ToString() == dw_lctc.Rows[k + 1]["czfs"].ToString() && dw_lctc.Rows[k]["lb"].ToString() == dw_lctc.Rows[k + 1]["lb"].ToString())
                                    {
                                        dr["数量"] = Convert.ToDecimal(dr["数量"].ToString() == "" ? "0" : dr["数量"].ToString()) + Convert.ToDecimal(dw_lctc.Rows[k + 1]["数量"].ToString() == "" ? "0" : dw_lctc.Rows[k + 1]["数量"].ToString());
                                        if(dw_lctc.Rows[k]["czfs"].ToString()== "拟移交物品")
                                        {
                                            dr["实际数量"] = Convert.ToDecimal(dr["实际数量"].ToString() == "" ? "0" : dr["实际数量"].ToString()) + Convert.ToDecimal(dw_lctc.Rows[k + 1]["实际数量"].ToString() == "" ? "0" : dw_lctc.Rows[k + 1]["实际数量"].ToString());
                                        }
                                        else
                                        {
                                            dr["实际数量"] = Convert.ToDecimal(dr["实际数量"].ToString() == "" ? "0" : dr["实际数量"].ToString()) + Convert.ToDecimal(dw_lctc.Rows[k + 1]["数量"].ToString() == "" ? "0" : dw_lctc.Rows[k + 1]["数量"].ToString());
                                        }
                                       

                                        dr["堪用数量"] = Convert.ToDecimal(dr["堪用数量"].ToString() == "" ? "0" : dr["堪用数量"].ToString()) + Convert.ToDecimal(dw_lctc.Rows[k + 1]["堪用数量"].ToString() == "" ? "0" : dw_lctc.Rows[k + 1]["堪用数量"].ToString());
                                        dr["可变现价值"] = Convert.ToDecimal(dr["可变现价值"].ToString() == "" ? "0" : dr["可变现价值"].ToString()) + Convert.ToDecimal(dw_lctc.Rows[k + 1]["可变现价值"].ToString() == "" ? "0" : dw_lctc.Rows[k + 1]["可变现价值"].ToString());
                                        k = k + 1;

                                    }
                                    dt.Rows.Add(dr);
                                }
                                else
                                {
                                    if (dw_lctc.Rows[k]["czfs"].ToString() == dw_lctc.Rows[k - 1]["czfs"].ToString() && dw_lctc.Rows[k]["lb"].ToString() == dw_lctc.Rows[k - 1]["lb"].ToString())
                                    {

                                    }
                                    else
                                    {
                                        dr = dt.NewRow();
                                        dr["单位代码"] = dt_dwxx_xx.Rows[i]["dwdm"];
                                        dr["序号"] = xh;
                                        dr["单位名称"] = dt_dwxx_xx.Rows[i]["dwmc"];
                                        dr["处置方式"] = dw_lctc.Rows[k]["czfs"];
                                        dr["类别"] = dw_lctc.Rows[k]["lb"];
                                        dr["数量"] = dw_lctc.Rows[k]["数量"];
                                        if (dr["处置方式"].ToString() == "拟移交物品")
                                        {
                                            dr["实际数量"] = dw_lctc.Rows[k]["实际数量"];
                                        }
                                        else
                                        {
                                            dr["实际数量"] = dw_lctc.Rows[k]["数量"];
                                        }
                                        dr["堪用数量"] = dw_lctc.Rows[k]["堪用数量"];
                                        dr["可变现价值"] = dw_lctc.Rows[k]["可变现价值"];
                                        dr["备注"] = string.Empty;
                                        dt.Rows.Add(dr);
                                    }
                                }
                            }
                        }
                    }
                }

                dataGridView1.DataSource = dt;
                dataGridView1.Columns[0].Visible = false;
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
            if (textBox1.Text == "")
            {
                MessageBox.Show("请选择报表输出路径!");
                return;
            }
            if (dt_dc.Rows.Count > 0)
            {
                decimal last_sl = 0;
                decimal last_sjsl = 0;
                decimal last_kysl = 0;
                decimal last_kbxjz = 0;

                label1.Visible = true;
                DataTable dt = new DataTable();
                dt.Columns.Add(new DataColumn("序号", typeof(string)));
                dt.Columns.Add(new DataColumn("单位名称", typeof(string)));
                dt.Columns.Add(new DataColumn("计量单位", typeof(string)));
                dt.Columns.Add(new DataColumn("数量", typeof(string)));
                dt.Columns.Add(new DataColumn("实际数量", typeof(string)));
                dt.Columns.Add(new DataColumn("堪用数量", typeof(string)));
                dt.Columns.Add(new DataColumn("物品总值", typeof(string)));
                dt.Columns.Add(new DataColumn("备注", typeof(string)));
                DataRow dr;

                
                      var query_Sum = from p in lctc_All.AsEnumerable()
                                  group p by p.Field<string>("备注")
                               into s
                                  select new
                                  {
                                      sl = s.Sum(m => Convert.ToDouble( m.Field<string>("数量"))),
                                      sjsl = s.Sum(p => Convert.ToDouble(p.Field<string>("实际数量"))),
                                      kysl = s.Sum(p => Convert.ToDouble(p.Field<string>("堪用数量"))),
                                      kbxjz = s.Sum(p => Convert.ToDouble(p.Field<string>("可变现价值")))
                                  };
                DataTable dtss_sum = new DataTable();
                dtss_sum.Columns.Add(new DataColumn("sl", typeof(string)));
                dtss_sum.Columns.Add(new DataColumn("sjsl", typeof(string)));
                dtss_sum.Columns.Add(new DataColumn("kysl", typeof(string)));
                dtss_sum.Columns.Add(new DataColumn("kbxjz", typeof(double)));
                query_Sum.ToList().ForEach(p => dtss_sum.Rows.Add(p.sl,p.sjsl, p.kysl, p.kbxjz));

                decimal kysl_sum = 0;
                decimal sl_sum = 0;
                decimal wpzz_sum = 0;
                decimal sjsl_sum = 0;
                for (int i = 0; i < dtss_sum.Rows.Count; i++)
                {
                    sl_sum = sl_sum + Convert.ToDecimal( dtss_sum.Rows[i]["sl"].ToString()==""?"0": dtss_sum.Rows[i]["sl"].ToString());
                    sjsl_sum = kysl_sum + Convert.ToDecimal( dtss_sum.Rows[i]["sjsl"].ToString()==""?"0": dtss_sum.Rows[i]["sjsl"].ToString());
                    kysl_sum = kysl_sum + Convert.ToDecimal( dtss_sum.Rows[i]["kysl"].ToString()==""?"0": dtss_sum.Rows[i]["kysl"].ToString());
                    wpzz_sum = wpzz_sum + Convert.ToDecimal( dtss_sum.Rows[i]["kbxjz"].ToString()==""?"0": dtss_sum.Rows[i]["kbxjz"].ToString());
                }

                dr = dt.NewRow();
                dr["序号"] = "合             计";
                dr["单位名称"] = "";
                dr["计量单位"] = string.Empty;
                dr["数量"] = sl_sum;
                dr["实际数量"] = sjsl_sum;
                dr["堪用数量"] = kysl_sum;
                dr["物品总值"] = wpzz_sum;
                dr["备注"] = string.Empty;
                dt.Rows.Add(dr);


                int A = 0;
                for (int i = 0; i < dt_dc.Rows.Count; i++)
                {
                    string dwdm = dt_dc.Rows[i]["dwdm"].ToString();
                    DataTable dw_lctc = lctc_dc.Select("dwdm like '" + dwdm + "%'").ToList().Count > 0 ? lctc_dc.Select("dwdm like '" + dwdm + "%'").OrderBy(m => m["czfs"]).ThenBy(m => m["lb"]).CopyToDataTable() : new DataTable();
                    if (dw_lctc.Rows.Count > 0)
                    {
                        A++;
                        #region 一级单位合计
                        var query = from p in dw_lctc.AsEnumerable()
                                    group p by p.Field<string>("dwdm")
                               into s
                                    select new
                                    {
                                        sl = s.Sum(m => m.Field<decimal>("数量")),
                                        sjsl = s.Sum(p => p.Field<decimal>("实际数量")),
                                        kysl = s.Sum(p => p.Field<decimal>("堪用数量")),
                                        kbxjz = s.Sum(p => p.Field<double>("可变现价值"))
                                    };
                        DataTable dtss = new DataTable();
                        dtss.Columns.Add(new DataColumn("sl", typeof(string)));
                        dtss.Columns.Add(new DataColumn("sjsl", typeof(string)));
                        dtss.Columns.Add(new DataColumn("kysl", typeof(string)));
                        dtss.Columns.Add(new DataColumn("kbxjz", typeof(double)));
                        query.ToList().ForEach(p => dtss.Rows.Add(p.sl,p.sjsl, p.kysl, p.kbxjz));
                        dr = dt.NewRow();
                        dr["序号"] = NumberToChinese(A);
                        dr["单位名称"] = dt_dc.Rows[i]["dwmc"];
                        dr["计量单位"] = string.Empty;
                        dr["数量"] = "0";
                        dr["实际数量"] = "0";
                        dr["堪用数量"] = "0";
                        dr["物品总值"] ="0";
                        if (dtss.Rows.Count > 0)
                        {
                            for (int DD = 0; DD < dtss.Rows.Count; DD++)
                            {
                                last_sl = last_sl+ Convert.ToDecimal(dtss.Rows[DD]["sl"].ToString() == "" ? "0" : dtss.Rows[DD]["sl"].ToString());
                                last_kysl = last_kysl+ Convert.ToDecimal(dtss.Rows[DD]["kysl"].ToString() == "" ? "0" : dtss.Rows[DD]["kysl"].ToString());
                                last_sjsl = last_sjsl + Convert.ToDecimal(dtss.Rows[DD]["sjsl"].ToString() == "" ? "0" : dtss.Rows[DD]["sjsl"].ToString());

                                last_kbxjz = last_kbxjz+ Convert.ToDecimal(dtss.Rows[DD]["kbxjz"].ToString() == "" ? "0" : dtss.Rows[DD]["kbxjz"].ToString());


                                dr["数量"]= Convert.ToDecimal(dr["数量"]) +Convert.ToDecimal( dtss.Rows[DD]["sl"].ToString()==""?"0": dtss.Rows[DD]["sl"].ToString());
                                dr["实际数量"] = Convert.ToDecimal(dr["实际数量"]) +Convert.ToDecimal( dtss.Rows[DD]["sjsl"].ToString()==""?"0": dtss.Rows[DD]["sjsl"].ToString());
                                dr["堪用数量"] = Convert.ToDecimal(dr["堪用数量"]) +Convert.ToDecimal( dtss.Rows[DD]["kysl"].ToString()==""?"0": dtss.Rows[DD]["kysl"].ToString());
                                dr["物品总值"] = Convert.ToDecimal(dr["物品总值"]) +Convert.ToDecimal( dtss.Rows[DD]["kbxjz"].ToString()==""?"0": dtss.Rows[DD]["kbxjz"].ToString());
                            } 
                        }
                        dr["备注"] = string.Empty;
                        dt.Rows.Add(dr);
                        #endregion
                        #region 字典预置
                        DataTable czfs_dt = new DataTable();
                        czfs_dt.Columns.Add(new DataColumn("mc", typeof(string)));
                        czfs_dt.Rows.Add("拟移交物品");
                        czfs_dt.Rows.Add("拟上交物品");
                        czfs_dt.Rows.Add("拟捐赠物品");
                        czfs_dt.Rows.Add("拟销毁物品");
                        czfs_dt.Rows.Add("拟个案处理物品");

                        DataTable lb_dt = new DataTable();
                        lb_dt.Columns.Add(new DataColumn("mc", typeof(string)));
                        lb_dt.Rows.Add("香烟");
                        lb_dt.Rows.Add("酒水");
                        lb_dt.Rows.Add("茶叶");
                        lb_dt.Rows.Add("食材");
                        lb_dt.Rows.Add("药材");
                        lb_dt.Rows.Add("瓷器");
                        lb_dt.Rows.Add("字画");
                        lb_dt.Rows.Add("金银");
                        lb_dt.Rows.Add("玉石");
                        lb_dt.Rows.Add("文玩");
                        lb_dt.Rows.Add("木材");
                        lb_dt.Rows.Add("模型");
                        lb_dt.Rows.Add("纪念币");
                        lb_dt.Rows.Add("日用品");
                        lb_dt.Rows.Add("其他");
                        #endregion
                        int CA = 0;
                        for (int c = 0; c < czfs_dt.Rows.Count; c++)
                        {
                            DataTable dt_czfs = dw_lctc.AsEnumerable().Where(m => Convert.ToString(m["czfs"]) == czfs_dt.Rows[c]["mc"].ToString()).Count() > 0 ? dw_lctc.AsEnumerable().Where(m => Convert.ToString(m["czfs"]) == czfs_dt.Rows[c]["mc"].ToString()).CopyToDataTable() : new DataTable();
                            if (dt_czfs.Rows.Count > 0)
                            {
                                CA++;
                                var query1 = from p in dt_czfs.AsEnumerable()
                                             group p by p.Field<string>("dwdm")
                                            into s
                                             select new
                                             {
                                                 sl = s.Sum(m => m.Field<decimal>("数量")),
                                                 sjsl = s.Sum(p => p.Field<decimal>("实际数量")),
                                                 kysl = s.Sum(p => p.Field<decimal>("堪用数量")),
                                                 kbxjz = s.Sum(p => p.Field<double>("可变现价值"))
                                             };
                                DataTable dtss1 = new DataTable();
                                dtss1.Columns.Add(new DataColumn("sl", typeof(string)));
                                dtss1.Columns.Add(new DataColumn("sjsl", typeof(string)));
                                dtss1.Columns.Add(new DataColumn("kysl", typeof(string)));
                                dtss1.Columns.Add(new DataColumn("kbxjz", typeof(double)));
                                query1.ToList().ForEach(p => dtss1.Rows.Add(p.sl,p.sjsl, p.kysl, p.kbxjz));


                                dr = dt.NewRow();
                                dr["序号"] = "（" + NumberToChinese(CA) + "）";
                                dr["单位名称"] = czfs_dt.Rows[c]["mc"].ToString();
                                dr["计量单位"] = string.Empty;
                                dr["数量"] = "0";
                                dr["实际数量"] = "0";
                                dr["堪用数量"] = "0";
                                dr["物品总值"] = "0";

                                if (dtss1.Rows.Count > 0)
                                {
                                    for (int DD = 0; DD < dtss1.Rows.Count; DD++)
                                    {
                                        dr["数量"] = Convert.ToDecimal(dr["数量"]) + Convert.ToDecimal(dtss1.Rows[DD]["sl"].ToString() == "" ? "0" : dtss1.Rows[DD]["sl"].ToString());
                                        dr["实际数量"] = Convert.ToDecimal(dr["实际数量"]) + Convert.ToDecimal(dtss1.Rows[DD]["sjsl"].ToString() == "" ? "0" : dtss1.Rows[DD]["sjsl"].ToString());
                                        dr["堪用数量"] = Convert.ToDecimal(dr["堪用数量"]) + Convert.ToDecimal(dtss1.Rows[DD]["kysl"].ToString() == "" ? "0" : dtss1.Rows[DD]["kysl"].ToString());
                                        dr["物品总值"] = Convert.ToDecimal(dr["物品总值"]) + Convert.ToDecimal(dtss1.Rows[DD]["kbxjz"].ToString() == "" ? "0" : dtss1.Rows[DD]["kbxjz"].ToString());
                                    }
                                }
                                dr["备注"] = string.Empty;
                                dt.Rows.Add(dr);
                                int la = 0;
                                for (int l = 0; l < lb_dt.Rows.Count; l++)
                                {
                                    DataTable dt_lb = dt_czfs.AsEnumerable().Where(m => Convert.ToString(m["lb"]) == lb_dt.Rows[l]["mc"].ToString()).Count() > 0 ? dt_czfs.AsEnumerable().Where(m => Convert.ToString(m["lb"]) == lb_dt.Rows[l]["mc"].ToString()).CopyToDataTable() : new DataTable();


                                    if (dt_lb.Rows.Count > 0)
                                    {
                                        la++;
                                        var query2 = from p in dt_lb.AsEnumerable()
                                                     group p by p.Field<string>("dwdm")
                                                     into s
                                                     select new
                                                     {
                                                         sl = s.Sum(m => m.Field<decimal>("数量")),
                                                         sjsl = s.Sum(p => p.Field<decimal>("实际数量")),
                                                         kysl = s.Sum(p => p.Field<decimal>("堪用数量")),
                                                         kbxjz = s.Sum(p => p.Field<double>("可变现价值"))
                                                     };
                                        DataTable dtss2 = new DataTable();
                                        dtss2.Columns.Add(new DataColumn("sl", typeof(string)));
                                        dtss2.Columns.Add(new DataColumn("sjsl", typeof(string)));
                                        dtss2.Columns.Add(new DataColumn("kysl", typeof(string)));
                                        dtss2.Columns.Add(new DataColumn("kbxjz", typeof(double)));
                                        query2.ToList().ForEach(p => dtss2.Rows.Add(p.sl,p.sjsl, p.kysl, p.kbxjz));
                                        dr = dt.NewRow();
                                        dr["序号"] = la;
                                        dr["单位名称"] = lb_dt.Rows[l]["mc"].ToString();
                                        string jldw_name = string.Empty;
                                        switch (lb_dt.Rows[l]["mc"].ToString())
                                        {
                                            case "酒水":
                                                jldw_name = "瓶/斤";
                                                break;
                                            case "香烟":
                                                jldw_name = "盒";
                                                break;
                                            case "茶叶":
                                                jldw_name = "斤";
                                                break;
                                            case "食材":
                                                jldw_name = "斤";
                                                break;
                                            case "药材":
                                                jldw_name = "斤";
                                                break;
                                            case "瓷器":
                                                jldw_name = "件";
                                                break;
                                            case "字画":
                                                jldw_name = "幅";
                                                break;
                                            case "金银":
                                                jldw_name = "克";
                                                break;
                                            case "玉石":
                                                jldw_name = "件";
                                                break;
                                            case "文玩":
                                                jldw_name = "件";
                                                break;
                                            case "木材":
                                                jldw_name = "件";
                                                break;
                                            case "模型":
                                                jldw_name = "个";
                                                break;

                                            case "纪念币":
                                                jldw_name = "件/套";
                                                break;
                                            case "日用品":
                                                jldw_name = "套";
                                                break;

                                            default:
                                                jldw_name = "件";
                                                break;
                                        }

                                        dr["计量单位"] = jldw_name;
                                        dr["数量"] = "0";
                                        dr["实际数量"] = "0";
                                        dr["堪用数量"] = "0";
                                        dr["物品总值"] = "0";

                                        if (dtss2.Rows.Count > 0)
                                        {
                                            for (int DD = 0; DD < dtss2.Rows.Count; DD++)
                                            {
                                                dr["数量"] = Convert.ToDecimal(dr["数量"]) + Convert.ToDecimal(dtss2.Rows[DD]["sl"].ToString() == "" ? "0" : dtss2.Rows[DD]["sl"].ToString());
                                                dr["实际数量"] = Convert.ToDecimal(dr["实际数量"]) + Convert.ToDecimal(dtss2.Rows[DD]["sjsl"].ToString() == "" ? "0" : dtss2.Rows[DD]["sjsl"].ToString());
                                                dr["堪用数量"] = Convert.ToDecimal(dr["堪用数量"]) + Convert.ToDecimal(dtss2.Rows[DD]["kysl"].ToString() == "" ? "0" : dtss2.Rows[DD]["kysl"].ToString());
                                                dr["物品总值"] = Convert.ToDecimal(dr["物品总值"]) + Convert.ToDecimal(dtss2.Rows[DD]["kbxjz"].ToString() == "" ? "0" : dtss2.Rows[DD]["kbxjz"].ToString());
                                            }
                                        }
                                        dr["备注"] = string.Empty;
                                        dt.Rows.Add(dr);
                                    }


                                }

                            }

                        }

                    }
                }
                if (dt.Rows.Count > 0)
                {
                    dt.Rows[0]["数量"] = last_sl ;
                    dt.Rows[0]["实际数量"] = last_sjsl;
                    dt.Rows[0]["堪用数量"] = last_kysl;
                    dt.Rows[0]["物品总值"] = last_kbxjz;
                }
                else
                {
                    MessageBox.Show("导出数据为空!");
                    return;
                }


                ExcelUI.OpenExcel_WPHZTJB(dt, Application.StartupPath + "\\report\\A30\\部队留存名贵特产类物品明细统计表.xlsx", textBox1.Text + "\\部队留存名贵特产类物品明细统计表.xlsx",3, 0, 8);
                MessageBox.Show("导出成功!");
                label1.Visible = false;
            }
            else
            {
                MessageBox.Show("导出数据为空!");
                return;
            }
        }


        /// <summary>
        /// 数字转化为中文
        /// </summary>
        /// <param name="number"></param>
        /// <returns></returns>
        public string NumberToChinese(int input)
        {
            string ret = null;
            int input2 = Math.Abs(input);
            string resource = "零一二三四五六七八九";
            string unit = "个十百千万亿兆京垓秭穰沟涧正载极";
            if (input > Math.Pow(10, 4 * (unit.Length - unit.IndexOf('万'))))
            {
                throw new Exception("the input is too big,input:" + input);
            }
            Func<int, List<List<int>>> splitNumFunc = (val) =>
            {
                int i = 0;
                int mod;
                int val_ = val;
                List<List<int>> splits = new List<List<int>>();
                List<int> splitNums;
                do
                {
                    mod = val_ % 10;
                    val_ /= 10;
                    if (i % 4 == 0)
                    {
                        splitNums = new List<int>();
                        splitNums.Add(mod);
                        if (splits.Count == 0)
                        {
                            splits.Add(splitNums);
                        }
                        else
                        {
                            splits.Insert(0, splitNums);
                        }
                    }
                    else
                    {
                        splitNums = splits[0];
                        splitNums.Insert(0, mod);
                    }
                    i++;
                } while (val_ > 0);
                return splits;
            };
            Func<List<List<int>>, string> hommizationFunc = (data) =>
            {
                List<StringBuilder> builders = new List<StringBuilder>();
                for (int i = 0; i < data.Count; i++)
                {
                    List<int> data2 = data[i];
                    StringBuilder newVal = new StringBuilder();
                    for (int j = 0; j < data2.Count;)
                    {
                        if (data2[j] == 0)
                        {
                            int k = j + 1;
                            for (; k < data2.Count && data2[k] == 0; k++) ;
                            //个位不是0，前面补一个零
                            newVal.Append('零');
                            j = k;
                        }
                        else
                        {
                            newVal.Append(resource[data2[j]]).Append(unit[data2.Count - 1 - j]);
                            j++;
                        }
                    }
                    if (newVal[newVal.Length - 1] == '零' && newVal.Length > 1)
                    {
                        newVal.Remove(newVal.Length - 1, 1);
                    }
                    else if (newVal[newVal.Length - 1] == '个')
                    {
                        newVal.Remove(newVal.Length - 1, 1);
                    }

                    if (i == 0 && newVal.Length > 1 && newVal[0] == '一' && newVal[1] == '十')
                    {//一十 --> 十
                        newVal.Remove(0, 1);
                    }
                    builders.Add(newVal);
                }
                StringBuilder sb = new StringBuilder();
                for (int i = 0; i < builders.Count; i++)
                {//拼接
                    if (builders.Count == 1)
                    {//个位数
                        sb.Append(builders[i]);
                    }
                    else
                    {
                        if (i == builders.Count - 1)
                        {//万位以下的
                            if (builders[i][builders[i].Length - 1] != '零')
                            {//十位以上的不拼接"零"
                                sb.Append(builders[i]);
                            }
                        }
                        else
                        {//万位以上的
                            if (builders[i][0] != '零')
                            {//零万零亿之类的不拼接
                                sb.Append(builders[i]).Append(unit[unit.IndexOf('千') + builders.Count - 1 - i]);
                            }
                        }
                    }
                }
                return sb.ToString();
            };
            List<List<int>> ret_split = splitNumFunc(input2);
            ret = hommizationFunc(ret_split);
            if (input < 0) ret = "-" + ret;
            return ret;
        }

    }
}
