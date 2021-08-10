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
    public partial class WPYJJHB : Form
    {
        DataTable dt_dcdata = null;
        DataTable dt_ddwmc_b = new DataTable();
        public WPYJJHB()
        {
            InitializeComponent();
            this.WindowState = FormWindowState.Maximized;
            Load += new EventHandler(WPYJJHB_Load);
        }

        private void button4_Click(object sender, EventArgs e)
        {
            folderBrowserDialog1.SelectedPath = "";
            if (folderBrowserDialog1.ShowDialog() == DialogResult.OK)
                textBox1.Text = folderBrowserDialog1.SelectedPath;
        }

        private void WPYJJHB_Load(object sender, EventArgs e)
        {
            GetAllDataRefreshGridView();
        }

        public void GetAllDataRefreshGridView()
        {
            select_report select_report = new select_report();

            DataTable dt = new DataTable();

            dt.Columns.Add(new DataColumn("交接省份", typeof(string)));
            dt.Columns.Add(new DataColumn("交接地市", typeof(string)));
            dt.Columns.Add(new DataColumn("交接点位", typeof(string)));
            dt.Columns.Add(new DataColumn("序号", typeof(string)));
            dt.Columns.Add(new DataColumn("单位标识码", typeof(string)));
            dt.Columns.Add(new DataColumn("大单位名称", typeof(string)));
            dt.Columns.Add(new DataColumn("名称", typeof(string)));
            dt.Columns.Add(new DataColumn("驻地", typeof(string)));
            dt.Columns.Add(new DataColumn("单位级别", typeof(string)));
            dt.Columns.Add(new DataColumn("合计", typeof(string)));
            dt.Columns.Add(new DataColumn("物品实际移交数量", typeof(string)));
            dt.Columns.Add(new DataColumn("酒水", typeof(string)));
            dt.Columns.Add(new DataColumn("香烟", typeof(string)));
            dt.Columns.Add(new DataColumn("茶叶", typeof(string)));
            dt.Columns.Add(new DataColumn("食材", typeof(string)));
            dt.Columns.Add(new DataColumn("药材", typeof(string)));
            dt.Columns.Add(new DataColumn("瓷器", typeof(string)));
            dt.Columns.Add(new DataColumn("字画", typeof(string)));
            dt.Columns.Add(new DataColumn("金银", typeof(string)));
            dt.Columns.Add(new DataColumn("玉石", typeof(string)));
            dt.Columns.Add(new DataColumn("文玩", typeof(string)));
            dt.Columns.Add(new DataColumn("木材", typeof(string)));
            dt.Columns.Add(new DataColumn("模型", typeof(string)));
            dt.Columns.Add(new DataColumn("纪念册", typeof(string)));
            dt.Columns.Add(new DataColumn("日用品", typeof(string)));
            dt.Columns.Add(new DataColumn("其他", typeof(string)));
            dt.Columns.Add(new DataColumn("姓名", typeof(string)));
            dt.Columns.Add(new DataColumn("军线座机", typeof(string)));
            dt.Columns.Add(new DataColumn("手机", typeof(string)));
            AccessHelper AccessHelper = new AccessHelper();


            //统计相关信息
            String sql_select_yhzhtj = "SELECT   t_dwxx.dwdm, t_dwxx.dwmc,t_dwxx.sszd,Switch(t_dwxx.dwjb='','0',t_dwxx.dwjb='军委机关部门','1',t_dwxx.dwjb='正战区级','2',t_dwxx.dwjb='副战区级','3',t_dwxx.dwjb='正军级','4',t_dwxx.dwjb='副军级','5',t_dwxx.dwjb='正师级','6',t_dwxx.dwjb='副师级','7',t_dwxx.dwjb='正团级','8',t_dwxx.dwjb='副团级','9',True,'10') AS dwjb, t_dwxx.xhsj,t_dwxx.xhdd ,t_dwxx.lxr,t_dwxx.jxzj,t_dwxx.jxsj , t_dwxx.nxsh, t_dwxx.nxs,t_dwxx.nxjsd ,t_dwxx.dwbs,t_dwxx.jjqrsl  FROM t_lctc LEFT JOIN t_dwxx ON t_lctc.dwdm = t_dwxx.dwdm GROUP BY t_dwxx.dwdm, t_dwxx.dwmc,dwjb,t_dwxx.sszd,t_dwxx.xhsj,t_dwxx.xhdd,t_dwxx.lxr,t_dwxx.jxzj,t_dwxx.jxsj, t_dwxx.nxsh, t_dwxx.nxs,t_dwxx.nxjsd ,t_dwxx.dwbs,t_dwxx.jjqrsl order by t_dwxx.nxsh, t_dwxx.nxs,t_dwxx.nxjsd,t_dwxx.dwdm,dwjb,t_dwxx.xhsj desc";
            DataTable dt_dwzxx = AccessHelper.getDataSet(sql_select_yhzhtj).Tables[0];

            string sql_select_lctc = "SELECT t_dwxx.dwdm,t_lctc.czfs , t_lctc.lb, sum(kysl) AS hj FROM t_lctc LEFT JOIN t_dwxx ON t_lctc.dwdm = t_dwxx.dwdm GROUP BY t_dwxx.dwdm,  t_lctc.lb,t_lctc.czfs";
            DataTable dt_lctxsum = AccessHelper.getDataSet(sql_select_lctc).Tables[0];

            string sql_dwmc = "select dwmc,dwdm from  t_dwxx where len(dwdm)=3";
            DataTable dt_ddwmc = AccessHelper.getDataSet(sql_dwmc).Tables[0];
            dt_ddwmc_b = dt_ddwmc;

            if (dt_dwzxx.Rows.Count > 0)
            {
                int xh = 0;
                for (int i = 0; i < dt_dwzxx.Rows.Count; i++)
                {
                    xh++;
                  
                    DataRow dr;
                    dr = dt.NewRow();

                    dr["交接省份"] = dt_dwzxx.Rows[i]["nxsh"];
                    dr["交接地市"] = dt_dwzxx.Rows[i]["nxs"];
                    dr["交接点位"] = dt_dwzxx.Rows[i]["nxjsd"];
                    dr["序号"] = xh;
                    dr["单位标识码"] = dt_dwzxx.Rows[i]["dwbs"];
                    DataTable dt_dwmc_A = dt_ddwmc.AsEnumerable().Where(m => Convert.ToString(m["dwdm"]) == dt_dwzxx.Rows[i]["dwdm"].ToString().Substring(0,3)).Count() > 0 ? dt_ddwmc.AsEnumerable().Where(m => Convert.ToString(m["dwdm"]) == dt_dwzxx.Rows[i]["dwdm"].ToString().Substring(0, 3)).CopyToDataTable() : new DataTable();
                    
                    if (dt_dwmc_A.Rows.Count > 0)
                    {
                        dr["大单位名称"] = dt_dwmc_A.Rows[0]["dwmc"];
                    }
                    else
                    {
                        dr["大单位名称"] = "";
                    }
                    dr["名称"] = dt_dwzxx.Rows[i]["dwmc"];

                    int a = dt_dwzxx.Rows[i]["sszd"].ToString().IndexOf("—");
                    if (a == -1)
                    {
                        dr["驻地"] = dt_dwzxx.Rows[i]["sszd"].ToString().Replace("—", "");
                    }
                    else
                    {
                        a = dt_dwzxx.Rows[i]["sszd"].ToString().IndexOf("—", a + 1);
                        string b = "";
                        if (a == -1)
                        {
                            dr["驻地"] = dt_dwzxx.Rows[i]["sszd"].ToString().Replace("—", "");
                        }
                        else
                        {
                            b = dt_dwzxx.Rows[i]["sszd"].ToString().Substring(0, a).Replace("—", "");
                            dr["驻地"] = b;
                        }
                    }


                    //dr["驻地"] = dt_dwzxx.Rows[i]["sszd"];
                    string dwjb = string.Empty;
                    if (!string.IsNullOrEmpty(dt_dwzxx.Rows[i]["dwjb"].ToString()))
                    {
                        switch (dt_dwzxx.Rows[i]["dwjb"].ToString())
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
                    dr["单位级别"] = dwjb;
                    dr["合计"] = "0";
                    dr["物品实际移交数量"] = "0";
                    dr["酒水"] = "0";
                    dr["香烟"] = "0";
                    dr["茶叶"] = "0";
                    dr["食材"] = "0";
                    dr["药材"] = "0";
                    dr["瓷器"] = "0";
                    dr["字画"] = "0";
                    dr["金银"] = "0";
                    dr["玉石"] = "0";
                    dr["文玩"] = "0";
                    dr["木材"] = "0";
                    dr["模型"] = "0";
                    dr["纪念册"] = "0";
                    dr["日用品"] = "0";
                    dr["其他"] = "0";
                    dr["姓名"] = dt_dwzxx.Rows[i]["lxr"];
                    dr["军线座机"] = dt_dwzxx.Rows[i]["jxzj"];
                    dr["手机"] = dt_dwzxx.Rows[i]["jxsj"];

                    string dwdm = dt_dwzxx.Rows[i]["dwdm"].ToString();

                    DataTable dt_lctcdata1 = dt_lctxsum.AsEnumerable().Where(m => Convert.ToString(m["dwdm"]) == dwdm).Count() > 0 ? dt_lctxsum.AsEnumerable().Where(m => Convert.ToString(m["dwdm"]) == dwdm).CopyToDataTable() : null;

                    DataTable dt_lctcdata = dt_lctcdata1.AsEnumerable().Where(m => Convert.ToString(m["czfs"]) == "拟移交物品").Count() > 0 ? dt_lctcdata1.AsEnumerable().Where(m => Convert.ToString(m["czfs"]) == "拟移交物品").CopyToDataTable() : null;
                    if (dt_lctcdata != null)
                    {

                        if (dt_lctcdata.Rows.Count > 0)
                        {
                            decimal wps = 0;
                            //decimal jjqrsl = 0;
                            for (int k = 0; k < dt_lctcdata.Rows.Count; k++)
                            {
                                if (!string.IsNullOrEmpty(dt_lctcdata.Rows[k]["lb"].ToString()))
                                {
                                    wps = wps + Convert.ToDecimal(dt_lctcdata.Rows[k]["hj"].ToString()==""?"0": dt_lctcdata.Rows[k]["hj"].ToString());
                                    //jjqrsl = jjqrsl + Convert.ToDecimal(dt_lctcdata.Rows[k]["jjqrsl"].ToString() == "" ? "0" : dt_lctcdata.Rows[k]["jjqrsl"].ToString());
                                    switch (dt_lctcdata.Rows[k]["lb"].ToString())
                                    {
                                        case "酒水":
                                            dr["酒水"] = Convert.ToDecimal(dr["酒水"].ToString()) + Convert.ToDecimal(dt_lctcdata.Rows[k]["hj"].ToString() == "" ? "0" : dt_lctcdata.Rows[k]["hj"].ToString());
                                            break;
                                        case "香烟":
                                            dr["香烟"] = Convert.ToDecimal(dr["香烟"].ToString()) + Convert.ToDecimal(dt_lctcdata.Rows[k]["hj"].ToString() == "" ? "0" : dt_lctcdata.Rows[k]["hj"].ToString());

                                            break;
                                        case "茶叶":
                                            dr["茶叶"] = Convert.ToDecimal(dr["茶叶"].ToString()) + Convert.ToDecimal(dt_lctcdata.Rows[k]["hj"].ToString() == "" ? "0" : dt_lctcdata.Rows[k]["hj"].ToString());
                                            break;
                                        case "食材":
                                            dr["食材"] = Convert.ToDecimal(dr["食材"].ToString()) + Convert.ToDecimal(dt_lctcdata.Rows[k]["hj"].ToString() == "" ? "0" : dt_lctcdata.Rows[k]["hj"].ToString());
                                            break;
                                        case "药材":
                                            dr["药材"] = Convert.ToDecimal(dr["药材"].ToString()) + Convert.ToDecimal(dt_lctcdata.Rows[k]["hj"].ToString() == "" ? "0" : dt_lctcdata.Rows[k]["hj"].ToString());
                                            break;
                                        case "瓷器":
                                            dr["瓷器"] = Convert.ToDecimal(dr["瓷器"].ToString()) + Convert.ToDecimal(dt_lctcdata.Rows[k]["hj"].ToString() == "" ? "0" : dt_lctcdata.Rows[k]["hj"].ToString());
                                            break;
                                        case "字画":
                                            dr["字画"] = Convert.ToDecimal(dr["字画"].ToString()) + Convert.ToDecimal(dt_lctcdata.Rows[k]["hj"].ToString() == "" ? "0" : dt_lctcdata.Rows[k]["hj"].ToString());
                                            break;
                                        case "金银":
                                            dr["金银"] = Convert.ToDecimal(dr["金银"].ToString()) + Convert.ToDecimal(dt_lctcdata.Rows[k]["hj"].ToString() == "" ? "0" : dt_lctcdata.Rows[k]["hj"].ToString());
                                            break;
                                        case "玉石":
                                            dr["玉石"] = Convert.ToDecimal(dr["玉石"].ToString()) + Convert.ToDecimal(dt_lctcdata.Rows[k]["hj"].ToString() == "" ? "0" : dt_lctcdata.Rows[k]["hj"].ToString());
                                            break;
                                        case "文玩":
                                            dr["文玩"] = Convert.ToDecimal(dr["文玩"].ToString()) + Convert.ToDecimal(dt_lctcdata.Rows[k]["hj"].ToString() == "" ? "0" : dt_lctcdata.Rows[k]["hj"].ToString());
                                            break;
                                        case "木材":
                                            dr["木材"] = Convert.ToDecimal(dr["木材"].ToString()) + Convert.ToDecimal(dt_lctcdata.Rows[k]["hj"].ToString() == "" ? "0" : dt_lctcdata.Rows[k]["hj"].ToString());
                                            break;
                                        case "模型":
                                            dr["模型"] = Convert.ToDecimal(dr["模型"].ToString()) + Convert.ToDecimal(dt_lctcdata.Rows[k]["hj"].ToString() == "" ? "0" : dt_lctcdata.Rows[k]["hj"].ToString());
                                            break;
                                        case "纪念册":
                                            dr["纪念册"] = Convert.ToDecimal(dr["纪念册"].ToString()) + Convert.ToDecimal(dt_lctcdata.Rows[k]["hj"].ToString() == "" ? "0" : dt_lctcdata.Rows[k]["hj"].ToString());
                                            break;
                                        case "纪念币":
                                            dr["纪念册"] = Convert.ToDecimal(dr["纪念册"].ToString()) + Convert.ToDecimal(dt_lctcdata.Rows[k]["hj"].ToString() == "" ? "0" : dt_lctcdata.Rows[k]["hj"].ToString());
                                            break;
                                        case "日用品":
                                            dr["日用品"] = Convert.ToDecimal(dr["日用品"].ToString()) + Convert.ToDecimal(dt_lctcdata.Rows[k]["hj"].ToString() == "" ? "0" : dt_lctcdata.Rows[k]["hj"].ToString());
                                            break;

                                        default:
                                            dr["其他"] = Convert.ToDecimal(dr["其他"].ToString()) + Convert.ToDecimal(dt_lctcdata.Rows[k]["hj"].ToString() == "" ? "0" : dt_lctcdata.Rows[k]["hj"].ToString());
                                            break;
                                    }
                                }
                            }
                            dr["合计"] = wps;
                            dr["物品实际移交数量"] = dt_dwzxx.Rows[i]["jjqrsl"]; 
                        }
                    }

                    dt.Rows.Add(dr);
                    dt_dcdata = dt;
            }
            }


            dataGridView1.DataSource = dt;
            //dt_dcdata = dt;
        }

        private void button2_Click(object sender, EventArgs e)
        {
            AccessHelper AccessHelper = new AccessHelper();
           // decimal jjqrsl_hj = 0;
            decimal jjqrsl_All = 0;
            if (textBox1.Text == "")
            {
                MessageBox.Show("请选择报表输出路径!");
                return;
            }
            if (dt_dcdata.Rows.Count > 0)
            {
                label1.Visible = true;
                String getzdw = " select dwmc  from t_dwxx where dwdm='000' ";
                DataTable dt_getzdw = AccessHelper.getDataSet(getzdw).Tables[0];
                string sDwmc = "";
                if (dt_getzdw.Rows.Count > 0)
                {
                    sDwmc = dt_getzdw.Rows[0]["dwmc"].ToString();
                }

                DataTable dt = new DataTable();
                dt.Columns.Add(new DataColumn("交接省份", typeof(string)));
                dt.Columns.Add(new DataColumn("交接地市", typeof(string)));
                dt.Columns.Add(new DataColumn("交接点位", typeof(string)));
                dt.Columns.Add(new DataColumn("序号", typeof(string)));
                dt.Columns.Add(new DataColumn("单位标识码", typeof(string)));
                dt.Columns.Add(new DataColumn("大单位名称", typeof(string)));
                dt.Columns.Add(new DataColumn("名称", typeof(string)));
                dt.Columns.Add(new DataColumn("驻地", typeof(string)));
                dt.Columns.Add(new DataColumn("单位级别", typeof(string)));
                dt.Columns.Add(new DataColumn("物品实际移交数量", typeof(string)));
                dt.Columns.Add(new DataColumn("合计", typeof(string)));
                dt.Columns.Add(new DataColumn("酒水", typeof(string)));
                dt.Columns.Add(new DataColumn("香烟", typeof(string)));
                dt.Columns.Add(new DataColumn("茶叶", typeof(string)));
                dt.Columns.Add(new DataColumn("食材", typeof(string)));
                dt.Columns.Add(new DataColumn("药材", typeof(string)));
                dt.Columns.Add(new DataColumn("瓷器", typeof(string)));
                dt.Columns.Add(new DataColumn("字画", typeof(string)));
                dt.Columns.Add(new DataColumn("金银", typeof(string)));
                dt.Columns.Add(new DataColumn("玉石", typeof(string)));
                dt.Columns.Add(new DataColumn("文玩", typeof(string)));
                dt.Columns.Add(new DataColumn("木材", typeof(string)));
                dt.Columns.Add(new DataColumn("模型", typeof(string)));
                dt.Columns.Add(new DataColumn("纪念册", typeof(string)));
                dt.Columns.Add(new DataColumn("日用品", typeof(string)));
                dt.Columns.Add(new DataColumn("其他", typeof(string)));
                dt.Columns.Add(new DataColumn("姓名", typeof(string)));
                dt.Columns.Add(new DataColumn("军线座机", typeof(string)));
                dt.Columns.Add(new DataColumn("手机", typeof(string)));
                int xh = 0;
                DataRow dr;

                string sql_select_lctc = "SELECT t_dwxx.dwdm,t_lctc.czfs , t_lctc.lb, sum(kysl) AS hj,sum(t_lctc.jjqrsl) as jjqrsl FROM t_lctc LEFT JOIN t_dwxx ON t_lctc.dwdm = t_dwxx.dwdm GROUP BY t_dwxx.dwdm,  t_lctc.lb,t_lctc.czfs";
                DataTable dt_lctcdata1 = AccessHelper.getDataSet(sql_select_lctc).Tables[0];
                if (dt_lctcdata1.Rows.Count > 0)
                {
                    for (int i = 0; i < dt_lctcdata1.Rows.Count; i++)
                    {
                        if (Convert.ToString(dt_lctcdata1.Rows[i]["dwdm"].ToString()) == "")
                        {
                            dt_lctcdata1.Rows.Remove(dt_lctcdata1.Rows[i]);
                        }
                    }
                   
                }
                //DataRow[] dtr = dt_lctcdata1.Select("dwdm=''");
                // var xx = dt_lctcdata1.AsEnumerable().Where(t => Convert.ToString(t["dwdm"]) == "").CopyToDataTable();
                //foreach (DataRow row in xx)
                //{
                //    dt_lctcdata1.Rows.Remove(row);
                //}
                //for (int i = 0; i < xx.Rows.Count; i++)
                //{
                //    dt_lctcdata1.Rows.Remove(xx.Rows[i]);
                //}
                //if (dtr.Length > 0)
                //{
                //    dt_lctcdata1.Rows.Remove(dtr[0]);
                //}

                DataTable dt_lctxsum = dt_lctcdata1.AsEnumerable().Where(m => Convert.ToString(m["czfs"]) == "拟移交物品").Count() > 0 ? dt_lctcdata1.AsEnumerable().Where(m => Convert.ToString(m["czfs"]) == "拟移交物品").CopyToDataTable() : null;
                if (dt_lctxsum != null)
                {
                    if (dt_lctxsum.Rows.Count > 0)
                    {
                        dr = dt.NewRow();
                        dr["交接省份"] = "合计";
                        dr["交接地市"] = "合计";
                        dr["交接点位"] = "合计";
                        dr["序号"] = "合计";
                        dr["单位标识码"] = "合计";
                        dr["大单位名称"] = "合计";
                        dr["名称"] = "合计";
                        dr["驻地"] = "合计";
                        dr["单位级别"] = "合计";
                        dr["物品实际移交数量"] = "0";
                        dr["合计"] = "0";
                        dr["酒水"] = "0";
                        dr["香烟"] = "0";
                        dr["茶叶"] = "0";
                        dr["食材"] = "0";
                        dr["药材"] = "0";
                        dr["瓷器"] = "0";
                        dr["字画"] = "0";
                        dr["金银"] = "0";
                        dr["玉石"] = "0";
                        dr["文玩"] = "0";
                        dr["木材"] = "0";
                        dr["模型"] = "0";
                        dr["纪念册"] = "0";
                        dr["日用品"] = "0";
                        dr["其他"] = "0";
                        dr["姓名"] = "";
                        dr["军线座机"] = "";
                        dr["手机"] = "";
                        decimal wps = 0;
                      
                        for (int k = 0; k < dt_lctxsum.Rows.Count; k++)
                        {
                            if (!string.IsNullOrEmpty(dt_lctxsum.Rows[k]["lb"].ToString()))
                            {
                                wps = wps + Convert.ToDecimal(dt_lctxsum.Rows[k]["hj"].ToString()==""?"0": dt_lctxsum.Rows[k]["hj"].ToString());
                               // jjqrsl = jjqrsl + Convert.ToDecimal(dt_lctxsum.Rows[k]["jjqrsl"].ToString()==""?"0": dt_lctxsum.Rows[k]["jjqrsl"].ToString());
                                switch (dt_lctxsum.Rows[k]["lb"].ToString())
                                {
                                    case "酒水":
                                        dr["酒水"] = Convert.ToDecimal(dr["酒水"].ToString()) + Convert.ToDecimal(dt_lctxsum.Rows[k]["hj"].ToString() == "" ? "0" : dt_lctxsum.Rows[k]["hj"].ToString());
                                        break;
                                    case "香烟":
                                        dr["香烟"] = Convert.ToDecimal(dr["香烟"].ToString()) + Convert.ToDecimal(dt_lctxsum.Rows[k]["hj"].ToString() == "" ? "0" : dt_lctxsum.Rows[k]["hj"].ToString());

                                        break;
                                    case "茶叶":
                                        dr["茶叶"] = Convert.ToDecimal(dr["茶叶"].ToString()) + Convert.ToDecimal(dt_lctxsum.Rows[k]["hj"].ToString() == "" ? "0" : dt_lctxsum.Rows[k]["hj"].ToString());
                                        break;
                                    case "食材":
                                        dr["食材"] = Convert.ToDecimal(dr["食材"].ToString()) + Convert.ToDecimal(dt_lctxsum.Rows[k]["hj"].ToString() == "" ? "0" : dt_lctxsum.Rows[k]["hj"].ToString());
                                        break;
                                    case "药材":
                                        dr["药材"] = Convert.ToDecimal(dr["药材"].ToString()) + Convert.ToDecimal(dt_lctxsum.Rows[k]["hj"].ToString()==""?"0": dt_lctxsum.Rows[k]["hj"].ToString());
                                        break;
                                    case "瓷器":
                                        dr["瓷器"] = Convert.ToDecimal(dr["瓷器"].ToString()) + Convert.ToDecimal(dt_lctxsum.Rows[k]["hj"].ToString() == "" ? "0" : dt_lctxsum.Rows[k]["hj"].ToString());
                                        break;
                                    case "字画":
                                        dr["字画"] = Convert.ToDecimal(dr["字画"].ToString()) + Convert.ToDecimal(dt_lctxsum.Rows[k]["hj"].ToString() == "" ? "0" : dt_lctxsum.Rows[k]["hj"].ToString());
                                        break;
                                    case "金银":
                                        dr["金银"] = Convert.ToDecimal(dr["金银"].ToString()) + Convert.ToDecimal(dt_lctxsum.Rows[k]["hj"].ToString() == "" ? "0" : dt_lctxsum.Rows[k]["hj"].ToString());
                                        break;
                                    case "玉石":
                                        dr["玉石"] = Convert.ToDecimal(dr["玉石"].ToString()) + Convert.ToDecimal(dt_lctxsum.Rows[k]["hj"].ToString() == "" ? "0" : dt_lctxsum.Rows[k]["hj"].ToString());
                                        break;
                                    case "文玩":
                                        dr["文玩"] = Convert.ToDecimal(dr["文玩"].ToString()) + Convert.ToDecimal(dt_lctxsum.Rows[k]["hj"].ToString() == "" ? "0" : dt_lctxsum.Rows[k]["hj"].ToString());
                                        break;
                                    case "木材":
                                        dr["木材"] = Convert.ToDecimal(dr["木材"].ToString()) + Convert.ToDecimal(dt_lctxsum.Rows[k]["hj"].ToString() == "" ? "0" : dt_lctxsum.Rows[k]["hj"].ToString());
                                        break;
                                    case "模型":
                                        dr["模型"] = Convert.ToDecimal(dr["模型"].ToString()) + Convert.ToDecimal(dt_lctxsum.Rows[k]["hj"].ToString() == "" ? "0" : dt_lctxsum.Rows[k]["hj"].ToString());
                                        break;
                                    case "纪念册":
                                        dr["纪念册"] = Convert.ToDecimal(dr["纪念册"].ToString()) + Convert.ToDecimal(dt_lctxsum.Rows[k]["hj"].ToString() == "" ? "0" : dt_lctxsum.Rows[k]["hj"].ToString());
                                        break;
                                    case "纪念币":
                                        dr["纪念册"] = Convert.ToDecimal(dr["纪念册"].ToString()) + Convert.ToDecimal(dt_lctxsum.Rows[k]["hj"].ToString() == "" ? "0" : dt_lctxsum.Rows[k]["hj"].ToString());
                                        break;
                                    case "日用品":
                                        dr["日用品"] = Convert.ToDecimal(dr["日用品"].ToString()) + Convert.ToDecimal(dt_lctxsum.Rows[k]["hj"].ToString() == "" ? "0" : dt_lctxsum.Rows[k]["hj"].ToString());
                                        break;

                                    default:
                                        dr["其他"] = Convert.ToDecimal(dr["其他"].ToString()) + Convert.ToDecimal(dt_lctxsum.Rows[k]["hj"].ToString() == "" ? "0" : dt_lctxsum.Rows[k]["hj"].ToString());
                                        break;
                                }
                            }
                        }
                        dr["合计"] = wps;
                        dr["物品实际移交数量"] = "0";
                        dt.Rows.Add(dr);
                    }
                }

                for (int i = 0; i < dt_dcdata.Rows.Count; i++)
                {
                    jjqrsl_All = jjqrsl_All + Convert.ToDecimal(dt_dcdata.Rows[i]["物品实际移交数量"].ToString() == "" ? "0" : dt_dcdata.Rows[i]["物品实际移交数量"].ToString());
                    string jjds = dt_dcdata.Rows[i]["交接地市"].ToString();

                    if (i == 0)
                    {
                        DataTable dt_lctcdata = dt_dcdata.AsEnumerable().Where(m => Convert.ToString(m["交接地市"]) == jjds).Count() > 0 ? dt_dcdata.AsEnumerable().Where(m => Convert.ToString(m["交接地市"]) == jjds).CopyToDataTable() : null;

                        dr = dt.NewRow();
                        dr["交接省份"] = dt_dcdata.Rows[i]["交接省份"].ToString();
                        dr["交接地市"] = dt_dcdata.Rows[i]["交接地市"].ToString();
                        dr["交接点位"] = dt_dcdata.Rows[i]["交接点位"].ToString();
                        dr["序号"] = "小计";
                        dr["单位标识码"] = "小计";
                        dr["大单位名称"] = "小计";
                        dr["名称"] = "小计";
                        dr["驻地"] = "小计";
                        dr["单位级别"] = "小计";
                        dr["物品实际移交数量"] = "0";
                        dr["合计"] = "0";
                        dr["酒水"] = "0";
                        dr["香烟"] = "0";
                        dr["茶叶"] = "0";
                        dr["食材"] = "0";
                        dr["药材"] = "0";
                        dr["瓷器"] = "0";
                        dr["字画"] = "0";
                        dr["金银"] = "0";
                        dr["玉石"] = "0";
                        dr["文玩"] = "0";
                        dr["木材"] = "0";
                        dr["模型"] = "0";
                        dr["纪念册"] = "0";
                        dr["日用品"] = "0";
                        dr["其他"] = "0";
                        dr["姓名"] = "";
                        dr["军线座机"] = "";
                        dr["手机"] = "";
                        if (dt_lctcdata != null)
                        {
                            for (int k = 0; k < dt_lctcdata.Rows.Count; k++)
                            {
                                dr["物品实际移交数量"] = Convert.ToDecimal(dr["物品实际移交数量"].ToString()) + Convert.ToDecimal(dt_lctcdata.Rows[k]["物品实际移交数量"].ToString() == "" ? "0" : dt_lctcdata.Rows[k]["物品实际移交数量"].ToString());
                              //  jjqrsl_hj = Convert.ToDecimal(jjqrsl_hj.ToString()) + Convert.ToDecimal(dr["物品实际移交数量"].ToString() == "" ? "0" : dr["物品实际移交数量"].ToString());
                                dr["合计"] = Convert.ToDecimal(dr["合计"].ToString()) + Convert.ToDecimal(dt_lctcdata.Rows[k]["合计"].ToString() == "" ? "0" : dt_lctcdata.Rows[k]["合计"].ToString());
                                dr["酒水"] = Convert.ToDecimal(dr["酒水"].ToString()) + Convert.ToDecimal(dt_lctcdata.Rows[k]["酒水"].ToString() == "" ? "0" : dt_lctcdata.Rows[k]["酒水"].ToString());
                                dr["香烟"] = Convert.ToDecimal(dr["香烟"].ToString()) + Convert.ToDecimal(dt_lctcdata.Rows[k]["香烟"].ToString() == "" ? "0" : dt_lctcdata.Rows[k]["香烟"].ToString());
                                dr["茶叶"] = Convert.ToDecimal(dr["茶叶"].ToString()) + Convert.ToDecimal(dt_lctcdata.Rows[k]["茶叶"].ToString() == "" ? "0" : dt_lctcdata.Rows[k]["茶叶"].ToString());
                                dr["食材"] = Convert.ToDecimal(dr["食材"].ToString()) + Convert.ToDecimal(dt_lctcdata.Rows[k]["食材"].ToString() == "" ? "0" : dt_lctcdata.Rows[k]["食材"].ToString());
                                dr["药材"] = Convert.ToDecimal(dr["药材"].ToString()) + Convert.ToDecimal(dt_lctcdata.Rows[k]["药材"].ToString() == "" ? "0" : dt_lctcdata.Rows[k]["药材"].ToString());
                                dr["瓷器"] = Convert.ToDecimal(dr["瓷器"].ToString()) + Convert.ToDecimal(dt_lctcdata.Rows[k]["瓷器"].ToString() == "" ? "0" : dt_lctcdata.Rows[k]["瓷器"].ToString());
                                dr["字画"] = Convert.ToDecimal(dr["字画"].ToString()) + Convert.ToDecimal(dt_lctcdata.Rows[k]["字画"].ToString() == "" ? "0" : dt_lctcdata.Rows[k]["字画"].ToString());
                                dr["金银"] = Convert.ToDecimal(dr["金银"].ToString()) + Convert.ToDecimal(dt_lctcdata.Rows[k]["金银"].ToString() == "" ? "0" : dt_lctcdata.Rows[k]["金银"].ToString());
                                dr["玉石"] = Convert.ToDecimal(dr["玉石"].ToString()) + Convert.ToDecimal(dt_lctcdata.Rows[k]["玉石"].ToString() == "" ? "0" : dt_lctcdata.Rows[k]["玉石"].ToString());
                                dr["文玩"] = Convert.ToDecimal(dr["文玩"].ToString()) + Convert.ToDecimal(dt_lctcdata.Rows[k]["文玩"].ToString() == "" ? "0" : dt_lctcdata.Rows[k]["文玩"].ToString());
                                dr["木材"] = Convert.ToDecimal(dr["木材"].ToString()) + Convert.ToDecimal(dt_lctcdata.Rows[k]["木材"].ToString() == "" ? "0" : dt_lctcdata.Rows[k]["木材"].ToString());
                                dr["模型"] = Convert.ToDecimal(dr["模型"].ToString()) + Convert.ToDecimal(dt_lctcdata.Rows[k]["模型"].ToString() == "" ? "0" : dt_lctcdata.Rows[k]["模型"].ToString());
                                dr["纪念册"] = Convert.ToDecimal(dr["纪念册"].ToString()) + Convert.ToDecimal(dt_lctcdata.Rows[k]["纪念册"].ToString() == "" ? "0" : dt_lctcdata.Rows[k]["纪念册"].ToString());
                                dr["日用品"] = Convert.ToDecimal(dr["日用品"].ToString()) + Convert.ToDecimal(dt_lctcdata.Rows[k]["日用品"].ToString() == "" ? "0" : dt_lctcdata.Rows[k]["日用品"].ToString());
                                dr["其他"] = Convert.ToDecimal(dr["其他"].ToString()) + Convert.ToDecimal(dt_lctcdata.Rows[k]["其他"].ToString() == "" ? "0" : dt_lctcdata.Rows[k]["其他"].ToString());
                            }
                        }

                        dt.Rows.Add(dr);
                    }
                    else
                    {
                        if (dt_dcdata.Rows[i]["交接地市"].ToString() != dt_dcdata.Rows[i - 1]["交接地市"].ToString())
                        {
                            DataTable dt_lctcdata = dt_dcdata.AsEnumerable().Where(m => Convert.ToString(m["交接地市"]) == jjds).Count() > 0 ? dt_dcdata.AsEnumerable().Where(m => Convert.ToString(m["交接地市"]) == jjds).CopyToDataTable() : null;

                            dr = dt.NewRow();
                            dr["交接省份"] = dt_dcdata.Rows[i]["交接省份"].ToString();
                            dr["交接地市"] = dt_dcdata.Rows[i]["交接地市"].ToString();
                            dr["交接点位"] = dt_dcdata.Rows[i]["交接点位"].ToString();
                            dr["序号"] = "小计";
                            dr["单位标识码"] = "小计";
                            dr["大单位名称"] = "小计";
                            dr["名称"] = "小计";
                            dr["驻地"] = "小计";
                            dr["单位级别"] = "小计";
                            dr["物品实际移交数量"] = "0";
                            dr["合计"] = "0";
                            dr["酒水"] = "0";
                            dr["香烟"] = "0";
                            dr["茶叶"] = "0";
                            dr["食材"] = "0";
                            dr["药材"] = "0";
                            dr["瓷器"] = "0";
                            dr["字画"] = "0";
                            dr["金银"] = "0";
                            dr["玉石"] = "0";
                            dr["文玩"] = "0";
                            dr["木材"] = "0";
                            dr["模型"] = "0";
                            dr["纪念册"] = "0";
                            dr["日用品"] = "0";
                            dr["其他"] = "0";
                            dr["姓名"] = "";
                            dr["军线座机"] = "";
                            dr["手机"] = "";
                           
                            for (int k = 0; k < dt_lctcdata.Rows.Count; k++)
                            {
                                dr["物品实际移交数量"] = Convert.ToDecimal(dr["物品实际移交数量"].ToString()) + Convert.ToDecimal(dt_lctcdata.Rows[k]["物品实际移交数量"].ToString() == "" ? "0" : dt_lctcdata.Rows[k]["物品实际移交数量"].ToString());
                              //  jjqrsl_hj = jjqrsl_hj + Convert.ToDecimal(dr["物品实际移交数量"].ToString() == "" ? "0" : dr["物品实际移交数量"].ToString());
                                dr["合计"] = Convert.ToDecimal(dr["合计"].ToString()) + Convert.ToDecimal(dt_lctcdata.Rows[k]["合计"].ToString() == "" ? "0" : dt_lctcdata.Rows[k]["合计"].ToString());
                                dr["酒水"] = Convert.ToDecimal(dr["酒水"].ToString()) + Convert.ToDecimal(dt_lctcdata.Rows[k]["酒水"].ToString() == "" ? "0" : dt_lctcdata.Rows[k]["酒水"].ToString());
                                dr["香烟"] = Convert.ToDecimal(dr["香烟"].ToString()) + Convert.ToDecimal(dt_lctcdata.Rows[k]["香烟"].ToString() == "" ? "0" : dt_lctcdata.Rows[k]["香烟"].ToString());
                                dr["茶叶"] = Convert.ToDecimal(dr["茶叶"].ToString()) + Convert.ToDecimal(dt_lctcdata.Rows[k]["茶叶"].ToString() == "" ? "0" : dt_lctcdata.Rows[k]["茶叶"].ToString());
                                dr["食材"] = Convert.ToDecimal(dr["食材"].ToString()) + Convert.ToDecimal(dt_lctcdata.Rows[k]["食材"].ToString() == "" ? "0" : dt_lctcdata.Rows[k]["食材"].ToString());
                                dr["药材"] = Convert.ToDecimal(dr["药材"].ToString()) + Convert.ToDecimal(dt_lctcdata.Rows[k]["药材"].ToString() == "" ? "0" : dt_lctcdata.Rows[k]["药材"].ToString());
                                dr["瓷器"] = Convert.ToDecimal(dr["瓷器"].ToString()) + Convert.ToDecimal(dt_lctcdata.Rows[k]["瓷器"].ToString() == "" ? "0" : dt_lctcdata.Rows[k]["瓷器"].ToString());
                                dr["字画"] = Convert.ToDecimal(dr["字画"].ToString()) + Convert.ToDecimal(dt_lctcdata.Rows[k]["字画"].ToString() == "" ? "0" : dt_lctcdata.Rows[k]["字画"].ToString());
                                dr["金银"] = Convert.ToDecimal(dr["金银"].ToString()) + Convert.ToDecimal(dt_lctcdata.Rows[k]["金银"].ToString() == "" ? "0" : dt_lctcdata.Rows[k]["金银"].ToString());
                                dr["玉石"] = Convert.ToDecimal(dr["玉石"].ToString()) + Convert.ToDecimal(dt_lctcdata.Rows[k]["玉石"].ToString() == "" ? "0" : dt_lctcdata.Rows[k]["玉石"].ToString());
                                dr["文玩"] = Convert.ToDecimal(dr["文玩"].ToString()) + Convert.ToDecimal(dt_lctcdata.Rows[k]["文玩"].ToString() == "" ? "0" : dt_lctcdata.Rows[k]["文玩"].ToString());
                                dr["木材"] = Convert.ToDecimal(dr["木材"].ToString()) + Convert.ToDecimal(dt_lctcdata.Rows[k]["木材"].ToString() == "" ? "0" : dt_lctcdata.Rows[k]["木材"].ToString());
                                dr["模型"] = Convert.ToDecimal(dr["模型"].ToString()) + Convert.ToDecimal(dt_lctcdata.Rows[k]["模型"].ToString() == "" ? "0" : dt_lctcdata.Rows[k]["模型"].ToString());
                                dr["纪念册"] = Convert.ToDecimal(dr["纪念册"].ToString()) + Convert.ToDecimal(dt_lctcdata.Rows[k]["纪念册"].ToString() == "" ? "0" : dt_lctcdata.Rows[k]["纪念册"].ToString());
                                dr["日用品"] = Convert.ToDecimal(dr["日用品"].ToString()) + Convert.ToDecimal(dt_lctcdata.Rows[k]["日用品"].ToString() == "" ? "0" : dt_lctcdata.Rows[k]["日用品"].ToString());
                                dr["其他"] = Convert.ToDecimal(dr["其他"].ToString()) + Convert.ToDecimal(dt_lctcdata.Rows[k]["其他"].ToString() == "" ? "0" : dt_lctcdata.Rows[k]["其他"].ToString());
                            }
                            dt.Rows.Add(dr);
                            xh = 0;
                        }
                    }
                    xh++;

                    dr = dt.NewRow();
                    dr["交接省份"] = dt_dcdata.Rows[i]["交接省份"].ToString();
                    dr["交接地市"] = dt_dcdata.Rows[i]["交接地市"].ToString();
                    dr["交接点位"] = dt_dcdata.Rows[i]["交接点位"].ToString();
                    dr["序号"] = xh;
                    dr["单位标识码"] ="'"+ dt_dcdata.Rows[i]["单位标识码"].ToString();

                    dr["大单位名称"] = dt_dcdata.Rows[i]["大单位名称"].ToString();
                    dr["名称"] = dt_dcdata.Rows[i]["名称"].ToString();

                    //int a = dt_dcdata.Rows[i]["驻地"].ToString().IndexOf("—");
                    //a = dt_dcdata.Rows[i]["驻地"].ToString().IndexOf("—", a + 1);
                    //string b = dt_dcdata.Rows[i]["驻地"].ToString().Substring(0, a).Replace("—", "");
                    dr["驻地"] = dt_dcdata.Rows[i]["驻地"].ToString();

                    dr["单位级别"] = dt_dcdata.Rows[i]["单位级别"].ToString();
                    dr["物品实际移交数量"] = dt_dcdata.Rows[i]["物品实际移交数量"].ToString();
                    dr["合计"] = dt_dcdata.Rows[i]["合计"].ToString();
                   
                    dr["酒水"] = dt_dcdata.Rows[i]["酒水"].ToString();
                    dr["香烟"] = dt_dcdata.Rows[i]["香烟"].ToString();
                    dr["茶叶"] = dt_dcdata.Rows[i]["茶叶"].ToString();
                    dr["食材"] = dt_dcdata.Rows[i]["食材"].ToString();
                    dr["药材"] = dt_dcdata.Rows[i]["药材"].ToString();
                    dr["瓷器"] = dt_dcdata.Rows[i]["瓷器"].ToString();
                    dr["字画"] = dt_dcdata.Rows[i]["字画"].ToString();
                    dr["金银"] = dt_dcdata.Rows[i]["金银"].ToString();
                    dr["玉石"] = dt_dcdata.Rows[i]["玉石"].ToString();
                    dr["文玩"] = dt_dcdata.Rows[i]["文玩"].ToString();
                    dr["木材"] = dt_dcdata.Rows[i]["木材"].ToString();
                    dr["模型"] = dt_dcdata.Rows[i]["模型"].ToString();
                    dr["纪念册"] = dt_dcdata.Rows[i]["纪念册"].ToString();
                    dr["日用品"] = dt_dcdata.Rows[i]["日用品"].ToString();
                    dr["其他"] = dt_dcdata.Rows[i]["其他"].ToString();
                    dr["姓名"] = dt_dcdata.Rows[i]["姓名"].ToString();
                    dr["军线座机"] = dt_dcdata.Rows[i]["军线座机"].ToString();
                    dr["手机"] = dt_dcdata.Rows[i]["手机"].ToString();
                    dt.Rows.Add(dr);
                }
                if (dt.Rows.Count > 0)
                {
                    dt.Rows[0]["物品实际移交数量"] = jjqrsl_All;
                }

                ExcelUI.OpenExcel_WPYJJHB(dt, Application.StartupPath + "\\report\\A30\\拟选交接点位-导出表格.xlsx", textBox1.Text + "\\向军队资产管理公司移交名贵特产类物品交接计划表.xlsx", 4, 0, 7, sDwmc);
                MessageBox.Show("导出成功!");
                label1.Visible = false;
            }
            else
            {
                MessageBox.Show("统计数据为空，不能导出！");
                return;
            }
        }

        private void button3_Click(object sender, EventArgs e)
        {
            this.Close();
        }
    }
}
