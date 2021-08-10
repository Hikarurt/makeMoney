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
    public partial class WPHZTJB : Form
    {
        DataTable dt_dc = new DataTable();
        AccessHelper AccessHelper = new AccessHelper();
        public WPHZTJB()
        {
            InitializeComponent();
            this.WindowState = FormWindowState.Maximized;
            Load += new EventHandler(WPHZTJB_Load);
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
                DataTable dt = new DataTable();
                dt.Columns.Add(new DataColumn("序号", typeof(string)));
                dt.Columns.Add(new DataColumn("单位名称", typeof(string)));
                dt.Columns.Add(new DataColumn("C", typeof(string)));
                dt.Columns.Add(new DataColumn("D", typeof(string)));
                dt.Columns.Add(new DataColumn("E", typeof(string)));
                dt.Columns.Add(new DataColumn("F", typeof(string)));
                dt.Columns.Add(new DataColumn("G", typeof(string)));
                dt.Columns.Add(new DataColumn("H", typeof(string)));
                dt.Columns.Add(new DataColumn("I", typeof(string)));
                dt.Columns.Add(new DataColumn("J", typeof(string)));
                dt.Columns.Add(new DataColumn("K", typeof(string)));
                dt.Columns.Add(new DataColumn("L", typeof(string)));
                dt.Columns.Add(new DataColumn("M", typeof(string)));
                dt.Columns.Add(new DataColumn("N", typeof(string)));
                dt.Columns.Add(new DataColumn("O", typeof(string)));
                dt.Columns.Add(new DataColumn("P", typeof(string)));
                dt.Columns.Add(new DataColumn("Q", typeof(string)));
                dt.Columns.Add(new DataColumn("R", typeof(string)));
                dt.Columns.Add(new DataColumn("S", typeof(string)));
                dt.Columns.Add(new DataColumn("T", typeof(string)));
                dt.Columns.Add(new DataColumn("U", typeof(string)));
                dt.Columns.Add(new DataColumn("V", typeof(string)));
                dt.Columns.Add(new DataColumn("W", typeof(string)));
                dt.Columns.Add(new DataColumn("X", typeof(string)));
                dt.Columns.Add(new DataColumn("Y", typeof(string)));
                dt.Columns.Add(new DataColumn("Z", typeof(string)));
                dt.Columns.Add(new DataColumn("AA", typeof(string)));
                dt.Columns.Add(new DataColumn("AB", typeof(string)));
                dt.Columns.Add(new DataColumn("AC", typeof(string)));
                dt.Columns.Add(new DataColumn("AD", typeof(string)));
                dt.Columns.Add(new DataColumn("AE", typeof(string)));
                dt.Columns.Add(new DataColumn("AF", typeof(string)));
                dt.Columns.Add(new DataColumn("AG", typeof(string)));
                dt.Columns.Add(new DataColumn("AH", typeof(string)));
                dt.Columns.Add(new DataColumn("AI", typeof(string)));
                dt.Columns.Add(new DataColumn("AJ", typeof(string)));
                dt.Columns.Add(new DataColumn("AK", typeof(string)));
                dt.Columns.Add(new DataColumn("AL", typeof(string)));
                dt.Columns.Add(new DataColumn("AM", typeof(string)));
                dt.Columns.Add(new DataColumn("AN", typeof(string)));
                dt.Columns.Add(new DataColumn("AO", typeof(string)));
                dt.Columns.Add(new DataColumn("AP", typeof(string)));
                dt.Columns.Add(new DataColumn("AQ", typeof(string)));
                dt.Columns.Add(new DataColumn("AR", typeof(string)));
                dt.Columns.Add(new DataColumn("AS", typeof(string)));
                dt.Columns.Add(new DataColumn("AT", typeof(string)));
                dt.Columns.Add(new DataColumn("AU", typeof(string)));
                dt.Columns.Add(new DataColumn("AV", typeof(string)));
                dt.Columns.Add(new DataColumn("AW", typeof(string)));
                dt.Columns.Add(new DataColumn("AX", typeof(string)));

                dt.Columns.Add(new DataColumn("AY", typeof(string)));
                dt.Columns.Add(new DataColumn("AZ", typeof(string)));
                dt.Columns.Add(new DataColumn("BA", typeof(string)));
                dt.Columns.Add(new DataColumn("BB", typeof(string)));
                dt.Columns.Add(new DataColumn("BC", typeof(string)));
                dt.Columns.Add(new DataColumn("BD", typeof(string)));
                dt.Columns.Add(new DataColumn("BE", typeof(string)));
                dt.Columns.Add(new DataColumn("BF", typeof(string)));
                dt.Columns.Add(new DataColumn("BG", typeof(string)));
                dt.Columns.Add(new DataColumn("BH", typeof(string)));
                dt.Columns.Add(new DataColumn("BI", typeof(string)));
                dt.Columns.Add(new DataColumn("BJ", typeof(string)));
                dt.Columns.Add(new DataColumn("BK", typeof(string)));
                dt.Columns.Add(new DataColumn("BL", typeof(string)));
                dt.Columns.Add(new DataColumn("BM", typeof(string)));
                dt.Columns.Add(new DataColumn("BN", typeof(string)));


                label1.Visible = true;
                string sql_dwxx_xx = "select  t_dwxx.dwdm,t_dwxx.dwmc from  t_dwxx  where len(t_dwxx.dwdm)=3";
                DataTable dt_dwxx_xx = AccessHelper.getDataSet(sql_dwxx_xx).Tables[0];
                if (dt_dwxx_xx.Rows.Count > 0)
                {
                    int xh = 0;
                    for (int i = 0; i < dt_dwxx_xx.Rows.Count; i++)
                    {
                        string dwdm = dt_dwxx_xx.Rows[i]["dwdm"].ToString();
                        DataTable dw_lctc = dt_dc.AsEnumerable().Where(m => Convert.ToString(m["单位代码"]) == dwdm).Count() > 0 ? dt_dc.AsEnumerable().Where(m => Convert.ToString(m["单位代码"]) == dwdm).CopyToDataTable() : new DataTable();
                        if (dw_lctc.Rows.Count > 0)
                        {
                          
                            xh++;
                            DataRow dr;
                            dr = dt.NewRow();
                           
                            dr["序号"] = xh;
                            dr["单位名称"] = dt_dwxx_xx.Rows[i]["dwmc"].ToString(); 
                            dr["C"] = "0";
                            dr["D"] = "0";
                            dr["E"] = "0";
                            dr["F"] = "0";
                            dr["G"] = "0";
                            dr["H"] = "0";
                            dr["I"] = "0";
                            dr["J"] = "0";
                            dr["K"] = "0";
                            dr["L"] = "0";
                            dr["M"] = "0";
                            dr["N"] = "0";
                            dr["O"] = "0";
                            dr["P"] = "0";
                            dr["Q"] = "0";
                            dr["R"] = "0";
                            dr["S"] = "0";
                            dr["T"] = "0";
                            dr["U"] = "0";
                            dr["V"] = "0";
                            dr["W"] = "0";
                            dr["X"] = "0";
                            dr["Y"] = "0";
                            dr["Z"] = "0";
                            dr["AA"] = "0";
                            dr["AB"] = "0";
                            dr["AC"] = "0";
                            dr["AD"] = "0";
                            dr["AE"] = "0";
                            dr["AF"] = "0";
                            dr["AG"] = "0";
                            dr["AH"] = "0";
                            dr["AI"] = "0";
                            dr["AJ"] = "0";
                            dr["AK"] = "0";
                            dr["AL"] = "0";
                            dr["AM"] = "0";
                            dr["AN"] = "0";
                            dr["AO"] = "0";
                            dr["AP"] = "0";
                            dr["AQ"] = "0";
                            dr["AR"] = "0";
                            dr["AS"] = "0";
                            dr["AT"] = "0";
                            dr["AU"] = "0";
                            dr["AV"] = "0";
                            dr["AW"] = "0";
                            dr["AX"] = "0";

                            dr["AY"] = "0";
                            dr["AZ"] = "0";
                            dr["BA"] = "0";
                            dr["BB"] = "0";
                            dr["BC"] = "0";
                            dr["BD"] = "0";
                            dr["BE"] = "0";
                            dr["BF"] = "0";
                            dr["BG"] = "0";
                            dr["BH"] = "0";
                            dr["BI"] = "0";
                            dr["BJ"] = "0";
                            dr["BK"] = "0";
                            dr["BL"] = "0";
                            dr["BM"] = "0";
                            dr["BN"] = "0";
                           
                            //decimal z_sl = 0;
                            //decimal z_kysl = 0;
                            //decimal z_sjsl = 0;
                            //decimal z_kbxjz = 0;
                            for (int k = 0; k < dw_lctc.Rows.Count; k++)
                            {
                                if (!string.IsNullOrEmpty(dw_lctc.Rows[k]["类别"].ToString()))
                                {
                                    //z_sl = z_sl + Convert.ToDecimal(dw_lctc.Rows[k]["数量"].ToString() == "" ? "0" : dw_lctc.Rows[k]["数量"].ToString());
                                    //z_kysl = z_kysl + Convert.ToDecimal(dw_lctc.Rows[k]["堪用数量"].ToString() == "" ? "0" : dw_lctc.Rows[k]["堪用数量"].ToString());
                                    //z_kbxjz = z_kbxjz + Convert.ToDecimal(dw_lctc.Rows[k]["可变现价值"].ToString() == "" ? "0" : dw_lctc.Rows[k]["可变现价值"].ToString());
                                    switch (dw_lctc.Rows[k]["类别"].ToString())
                                    {
                                        case "酒水":
                                            dr["K"] = dw_lctc.Rows[k]["数量"].ToString();
                                            dr["L"] = dw_lctc.Rows[k]["实际数量"].ToString();
                                            dr["M"] = dw_lctc.Rows[k]["堪用数量"].ToString();
                                            dr["N"] = dw_lctc.Rows[k]["可变现价值"].ToString();
                                            break;
                                        case "香烟":
                                            dr["G"] = dw_lctc.Rows[k]["数量"].ToString();
                                            dr["H"] = dw_lctc.Rows[k]["实际数量"].ToString();
                                            dr["I"] = dw_lctc.Rows[k]["堪用数量"].ToString();
                                            dr["J"] = dw_lctc.Rows[k]["可变现价值"].ToString();
                                            break;
                                        case "茶叶":
                                            dr["O"] = dw_lctc.Rows[k]["数量"].ToString();
                                            dr["P"] = dw_lctc.Rows[k]["实际数量"].ToString();
                                            dr["Q"] = dw_lctc.Rows[k]["堪用数量"].ToString();
                                            dr["R"] = dw_lctc.Rows[k]["可变现价值"].ToString();
                                            break;
                                        case "食材":
                                            dr["S"] = dw_lctc.Rows[k]["数量"].ToString();
                                            dr["T"] = dw_lctc.Rows[k]["实际数量"].ToString();
                                            dr["U"] = dw_lctc.Rows[k]["堪用数量"].ToString();
                                            dr["V"] = dw_lctc.Rows[k]["可变现价值"].ToString();
                                            break;
                                        case "药材":
                                            dr["W"] = dw_lctc.Rows[k]["数量"].ToString();
                                            dr["X"] = dw_lctc.Rows[k]["实际数量"].ToString();
                                            dr["Y"] = dw_lctc.Rows[k]["堪用数量"].ToString();
                                            dr["Z"] = dw_lctc.Rows[k]["可变现价值"].ToString();
                                            break;
                                        case "瓷器":
                                            dr["AA"] = dw_lctc.Rows[k]["数量"].ToString();
                                            dr["AB"] = dw_lctc.Rows[k]["实际数量"].ToString();
                                            dr["AC"] = dw_lctc.Rows[k]["堪用数量"].ToString();
                                            dr["AD"] = dw_lctc.Rows[k]["可变现价值"].ToString();
                                            break;
                                        case "字画":
                                            dr["AE"] = dw_lctc.Rows[k]["数量"].ToString();
                                            dr["AF"] = dw_lctc.Rows[k]["实际数量"].ToString();
                                            dr["AG"] = dw_lctc.Rows[k]["堪用数量"].ToString();
                                            dr["AH"] = dw_lctc.Rows[k]["可变现价值"].ToString();
                                            break;
                                        case "金银":
                                            dr["AI"] = dw_lctc.Rows[k]["数量"].ToString();
                                            dr["AJ"] = dw_lctc.Rows[k]["实际数量"].ToString();
                                            dr["AK"] = dw_lctc.Rows[k]["堪用数量"].ToString();
                                            dr["AL"] = dw_lctc.Rows[k]["可变现价值"].ToString();
                                            break;
                                        case "玉石":
                                            dr["AM"] = dw_lctc.Rows[k]["数量"].ToString();
                                            dr["AN"] = dw_lctc.Rows[k]["实际数量"].ToString();
                                            dr["AO"] = dw_lctc.Rows[k]["堪用数量"].ToString();
                                            dr["AP"] = dw_lctc.Rows[k]["可变现价值"].ToString();
                                            break;
                                        case "文玩":
                                            dr["AQ"] = dw_lctc.Rows[k]["数量"].ToString();
                                            dr["AR"] = dw_lctc.Rows[k]["实际数量"].ToString();
                                            dr["AS"] = dw_lctc.Rows[k]["堪用数量"].ToString();
                                            dr["AT"] = dw_lctc.Rows[k]["可变现价值"].ToString();
                                            break;
                                        case "木材":
                                            dr["AU"] = dw_lctc.Rows[k]["数量"].ToString();
                                            dr["AV"] = dw_lctc.Rows[k]["实际数量"].ToString();
                                            dr["AW"] = dw_lctc.Rows[k]["堪用数量"].ToString();
                                            dr["AX"] = dw_lctc.Rows[k]["可变现价值"].ToString();
                                            break;
                                        case "模型":
                                            dr["AY"] = dw_lctc.Rows[k]["数量"].ToString();
                                            dr["AZ"] = dw_lctc.Rows[k]["实际数量"].ToString();
                                            dr["BA"] = dw_lctc.Rows[k]["堪用数量"].ToString();
                                            dr["BB"] = dw_lctc.Rows[k]["可变现价值"].ToString();
                                            break;
                                        case "纪念币":
                                            dr["BC"] = dw_lctc.Rows[k]["数量"].ToString();
                                            dr["BD"] = dw_lctc.Rows[k]["实际数量"].ToString();
                                            dr["BE"] = dw_lctc.Rows[k]["堪用数量"].ToString();
                                            dr["BF"] = dw_lctc.Rows[k]["可变现价值"].ToString();
                                            break;
                                        case "日用品":
                                            dr["BG"] = dw_lctc.Rows[k]["数量"].ToString();
                                            dr["BH"] = dw_lctc.Rows[k]["实际数量"].ToString();
                                            dr["BI"] = dw_lctc.Rows[k]["堪用数量"].ToString();
                                            dr["BJ"] = dw_lctc.Rows[k]["可变现价值"].ToString();
                                            break;

                                        case "其他":
                                            dr["BK"] = dw_lctc.Rows[k]["数量"].ToString();
                                            dr["BL"] = dw_lctc.Rows[k]["实际数量"].ToString();
                                            dr["BM"] = dw_lctc.Rows[k]["堪用数量"].ToString();
                                            dr["BN"] = dw_lctc.Rows[k]["可变现价值"].ToString();
                                            break;
                                    }
                                }
                            }
                            
                            dt.Rows.Add(dr);
                        }
                    }
                    ExcelUI.OpenExcel_WPHZTJB(dt, Application.StartupPath + "\\report\\A30\\部队留存名贵特产类物品汇总统计表.xlsx", textBox1.Text + "\\部队留存名贵特产类物品汇总统计表.xlsx", 5, 0, 8);
                    MessageBox.Show("导出成功!");
                    label1.Visible = false;
                }
            }
            else
            {
                MessageBox.Show("导出数据为空!");
                return;
            }
        }


        private void WPHZTJB_Load(object sender, EventArgs e)
        {
            GetAllDataRefreshGridView();
        }

        public void GetAllDataRefreshGridView()
        {

            DataTable dt = new DataTable();
            dt.Columns.Add(new DataColumn("单位代码", typeof(string)));
            dt.Columns.Add(new DataColumn("序号", typeof(string)));
            dt.Columns.Add(new DataColumn("单位名称", typeof(string)));
            dt.Columns.Add(new DataColumn("类别", typeof(string)));
            dt.Columns.Add(new DataColumn("数量", typeof(string)));
            dt.Columns.Add(new DataColumn("实际数量", typeof(string)));
            dt.Columns.Add(new DataColumn("堪用数量", typeof(string)));
            dt.Columns.Add(new DataColumn("可变现价值", typeof(string)));
        

            //物品信息总和
            string sql_lctc_xx = "SELECT t_lctc.dwdm, t_lctc.lb as lb,t_lctc.czfs as czfs, sum(sl)as 数量,sum(kysl) as 堪用数量,sum(kbxjz) as 可变现价值,sum(jjqrsl) as 实际数量 from  t_lctc   group by t_lctc.dwdm, t_lctc.lb ,t_lctc.czfs order by t_lctc.dwdm";
            DataTable dt_lctc_xx = new DataTable();
            dt_lctc_xx.Columns.Add(new DataColumn("dwdm", typeof(string)));
            dt_lctc_xx.Columns.Add(new DataColumn("lb", typeof(string)));
            dt_lctc_xx.Columns.Add(new DataColumn("数量", typeof(decimal)));
            dt_lctc_xx.Columns.Add(new DataColumn("实际数量", typeof(decimal)));
            dt_lctc_xx.Columns.Add(new DataColumn("堪用数量", typeof(decimal)));
            dt_lctc_xx.Columns.Add(new DataColumn("可变现价值", typeof(double)));
            DataTable dt_zzqsj= AccessHelper.getDataSet(sql_lctc_xx).Tables[0];
            int hh = 0;
            for (int i = 0; i < dt_zzqsj.Rows.Count; i++)
            {
                if (dt_zzqsj.Rows[i]["czfs"].ToString() != "拟移交物品")
                {
                    dt_zzqsj.Rows[i]["实际数量"] = dt_zzqsj.Rows[i]["数量"].ToString() == "" ? "0" : dt_zzqsj.Rows[i]["数量"].ToString();
                }
                try
                {
                    if (dt_zzqsj.Rows[i - 1]["dwdm"].ToString() == dt_zzqsj.Rows[i]["dwdm"].ToString() && dt_zzqsj.Rows[i - 1]["lb"].ToString() == dt_zzqsj.Rows[i]["lb"].ToString())
                    {


                        dt_lctc_xx.Rows[hh-1]["数量"]= Convert.ToDecimal(dt_lctc_xx.Rows[hh-1]["数量"].ToString() == "" ? "0" : dt_lctc_xx.Rows[hh - 1]["数量"].ToString()) + Convert.ToDecimal(dt_zzqsj.Rows[i]["数量"].ToString() == "" ? "0" : dt_zzqsj.Rows[i]["数量"].ToString());
                        dt_lctc_xx.Rows[hh-1]["堪用数量"]=Convert.ToDecimal(dt_lctc_xx.Rows[hh-1]["堪用数量"].ToString() == "" ? "0" : dt_lctc_xx.Rows[hh - 1]["堪用数量"].ToString()) + Convert.ToDecimal(dt_zzqsj.Rows[i]["堪用数量"].ToString() == "" ? "0" : dt_zzqsj.Rows[i]["堪用数量"].ToString());
                        dt_lctc_xx.Rows[hh-1]["可变现价值"]=Convert.ToDecimal(dt_lctc_xx.Rows[hh-1]["可变现价值"].ToString() == "" ? "0" : dt_lctc_xx.Rows[hh - 1]["可变现价值"].ToString()) + Convert.ToDecimal(dt_zzqsj.Rows[i]["可变现价值"].ToString() == "" ? "0" : dt_zzqsj.Rows[i]["可变现价值"].ToString());
                        dt_lctc_xx.Rows[hh-1]["实际数量"] = Convert.ToDecimal(dt_lctc_xx.Rows[hh-1]["实际数量"].ToString() == "" ? "0" : dt_lctc_xx.Rows[hh - 1]["实际数量"].ToString()) + Convert.ToDecimal(dt_zzqsj.Rows[i]["实际数量"].ToString() == "" ? "0" : dt_zzqsj.Rows[i]["实际数量"].ToString());
                    }
                    else
                    {
                        DataRow dr;
                        dr = dt_lctc_xx.NewRow();
                        dr["dwdm"] = dt_zzqsj.Rows[i]["dwdm"].ToString();
                        dr["lb"] = dt_zzqsj.Rows[i]["lb"].ToString();
                        dr["数量"] = dt_zzqsj.Rows[i]["数量"].ToString();
                        dr["实际数量"] = dt_zzqsj.Rows[i]["实际数量"].ToString() == "" ? "0" : dt_zzqsj.Rows[i]["实际数量"].ToString();
                        dr["堪用数量"] = dt_zzqsj.Rows[i]["堪用数量"].ToString();
                        dr["可变现价值"] = dt_zzqsj.Rows[i]["可变现价值"].ToString();
                        dt_lctc_xx.Rows.Add(dr);
                        hh++;
                    }
                }
                catch 
                {
                    DataRow dr;
                    dr = dt_lctc_xx.NewRow();

                    dr["dwdm"] = dt_zzqsj.Rows[i]["dwdm"].ToString();
                    dr["lb"] = dt_zzqsj.Rows[i]["lb"].ToString();
                    dr["数量"] = dt_zzqsj.Rows[i]["数量"].ToString();
                    dr["实际数量"] = dt_zzqsj.Rows[i]["实际数量"].ToString()==""?"0" : dt_zzqsj.Rows[i]["实际数量"].ToString();
                    dr["堪用数量"] = dt_zzqsj.Rows[i]["堪用数量"].ToString();
                    dr["可变现价值"] = dt_zzqsj.Rows[i]["可变现价值"].ToString();
                    dt_lctc_xx.Rows.Add(dr);
                    hh++;
                }

            }

           


            //单位信息总和
            string sql_dwxx_xx = "select  t_dwxx.dwdm,t_dwxx.dwmc from  t_dwxx  where len(t_dwxx.dwdm)=3";
            DataTable dt_dwxx_xx = AccessHelper.getDataSet(sql_dwxx_xx).Tables[0];

            if (dt_dwxx_xx.Rows.Count > 0)
            {
                int xh = 0;
                for (int i = 0; i < dt_dwxx_xx.Rows.Count; i++)
                {
                    string dwdm = dt_dwxx_xx.Rows[i]["dwdm"].ToString();
                    DataTable dw_lctc = dt_lctc_xx.Select("dwdm like '" + dwdm + "%'").ToList().Count > 0 ? dt_lctc_xx.Select("dwdm like '" + dwdm + "%'").CopyToDataTable() : new DataTable();
                    if (dw_lctc.Rows.Count > 0)
                    {
                        var query = from c in dw_lctc.AsEnumerable()
                                    group c by c.Field<string>("lb")
                                    into s
                                    select new
                                    {
                                        lb = s.Key.ToString(),
                                        sl = s.Sum(m => m.Field<decimal>("数量")),
                                        kysl = s.Sum(p => p.Field<decimal>("堪用数量")),
                                        sjsl = s.Sum(p => p.Field<decimal>("实际数量")),
                                        kbxjz = s.Sum(p => p.Field<double>("可变现价值"))
                                    };
                        DataTable dtss = new DataTable();
                        dtss.Columns.Add(new DataColumn("lb", typeof(string)));
                        dtss.Columns.Add(new DataColumn("sl", typeof(string)));
                        dtss.Columns.Add(new DataColumn("kysl", typeof(string)));
                        dtss.Columns.Add(new DataColumn("sjsl", typeof(string)));
                        dtss.Columns.Add(new DataColumn("kbxjz", typeof(double)));
                        query.ToList().ForEach(p => dtss.Rows.Add(p.lb, p.sl, p.kysl,p.sjsl, p.kbxjz));

                        for (int k = 0; k < dtss.Rows.Count; k++)
                        {
                            xh++;
                            DataRow dr;
                            dr = dt.NewRow();
                            dr["单位代码"] = dt_dwxx_xx.Rows[i]["dwdm"]; ;
                            dr["序号"] = xh;
                            dr["单位名称"] = dt_dwxx_xx.Rows[i]["dwmc"];
                            dr["类别"] = dtss.Rows[k]["lb"];
                            dr["数量"] = dtss.Rows[k]["sl"];
                            dr["实际数量"] = dtss.Rows[k]["sjsl"];
                            dr["堪用数量"] = dtss.Rows[k]["kysl"];
                            dr["可变现价值"] = dtss.Rows[k]["kbxjz"];
                            dt.Rows.Add(dr);
                        }
                    }
                }
                dataGridView1.DataSource = dt;
                dataGridView1.Columns[0].Visible = false;
                dt_dc = dt;
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
    }
}
