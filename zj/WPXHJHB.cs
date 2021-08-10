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
    public partial class WPXHJHB : Form
    {
        DataTable dt_dcdata = null;
        public WPXHJHB()
        {
            InitializeComponent();
            this.WindowState = FormWindowState.Maximized;
            Load += new EventHandler(WPXHJHB_Load);

        }

        private void button4_Click(object sender, EventArgs e)
        {
            folderBrowserDialog1.SelectedPath = "";
            if (folderBrowserDialog1.ShowDialog() == DialogResult.OK)
                textBox1.Text = folderBrowserDialog1.SelectedPath;
        }

        private void WPXHJHB_Load(object sender, EventArgs e)
        {
            GetAllDataRefreshGridView();
        }
        public void GetAllDataRefreshGridView()
        {
            try
            {

          
            select_report select_report = new select_report();

            DataTable dt = new DataTable();
            dt.Columns.Add(new DataColumn("序号", typeof(string)));
            dt.Columns.Add(new DataColumn("名称", typeof(string)));
            dt.Columns.Add(new DataColumn("驻地", typeof(string)));
            dt.Columns.Add(new DataColumn("单位级别", typeof(string)));
            dt.Columns.Add(new DataColumn("销毁时间", typeof(string)));
            dt.Columns.Add(new DataColumn("销毁地点", typeof(string)));
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
            AccessHelper AccessHelper = new AccessHelper();


            //统计相关信息
            String sql_select_yhzhtj = "SELECT   t_dwxx.dwdm, t_dwxx.dwmc,t_dwxx.sszd,Switch(t_dwxx.dwjb='','0',t_dwxx.dwjb='军委机关部门','1',t_dwxx.dwjb='正战区级','2',t_dwxx.dwjb='副战区级','3',t_dwxx.dwjb='正军级','4',t_dwxx.dwjb='副军级','5',t_dwxx.dwjb='正师级','6',t_dwxx.dwjb='副师级','7',t_dwxx.dwjb='正团级','8',t_dwxx.dwjb='副团级','9',True,'10') AS dwjb, t_dwxx.xhsj,t_dwxx.xhdd ,t_dwxx.lxr,t_dwxx.jxzj,t_dwxx.jxsj FROM t_lctc LEFT JOIN t_dwxx ON t_lctc.dwdm = t_dwxx.dwdm where  sfxhwp <>1  GROUP BY t_dwxx.dwdm, t_dwxx.dwmc,dwjb,t_dwxx.sszd,t_dwxx.xhsj,t_dwxx.xhdd,t_dwxx.lxr,t_dwxx.jxzj,t_dwxx.jxsj order by t_dwxx.dwdm,dwjb,t_dwxx.xhsj desc ";
            DataTable dt_dwzxx = AccessHelper.getDataSet(sql_select_yhzhtj).Tables[0];

            string sql_select_lctc = "SELECT t_dwxx.dwdm,t_dwxx.sfxhwp ,t_lctc.czfs, t_lctc.lb,  sum(sl) AS hj FROM t_lctc LEFT JOIN t_dwxx ON t_lctc.dwdm = t_dwxx.dwdm GROUP BY t_dwxx.dwdm,  t_lctc.lb,t_dwxx.sfxhwp,t_lctc.czfs";
            DataTable dt_lctxsum = AccessHelper.getDataSet(sql_select_lctc).Tables[0];
            if (dt_dwzxx.Rows.Count > 0)
            {
                int xh = 0;
                for (int i = 0; i < dt_dwzxx.Rows.Count; i++)
                {
                    xh++;
                  
                    DataRow dr;
                    dr = dt.NewRow();
                    dr["序号"] = xh;
                    dr["名称"] = dt_dwzxx.Rows[i]["dwmc"];



                        int a = dt_dwzxx.Rows[i]["sszd"].ToString().IndexOf("—");
                        if (a == -1)
                        {
                            dr["驻地"] = dt_dwzxx.Rows[i]["sszd"].ToString().Replace("—", "");
                        }
                        else
                        {
                            a = dt_dwzxx.Rows[i]["sszd"].ToString().IndexOf("—", a + 1);
                           
                            if (a == -1)
                            {
                                dr["驻地"] = dt_dwzxx.Rows[i]["sszd"].ToString().Replace("—", "");
                            }
                            else
                            {
                                string b = dt_dwzxx.Rows[i]["sszd"].ToString().Substring(0, a).Replace("—", "");
                                dr["驻地"] = b;
                            }
                        }


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
                    if (string.IsNullOrEmpty(dt_dwzxx.Rows[i]["xhsj"].ToString()))
                    {
                        dr["销毁时间"] = "";
                    }
                    else
                    {
                        dr["销毁时间"] = Convert.ToDateTime(dt_dwzxx.Rows[i]["xhsj"]).ToLongDateString().ToString();

                    }
                    dr["销毁地点"] = dt_dwzxx.Rows[i]["xhdd"];
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
                    dr["姓名"] = dt_dwzxx.Rows[i]["lxr"];
                    dr["军线座机"] = dt_dwzxx.Rows[i]["jxzj"];
                    dr["手机"] = dt_dwzxx.Rows[i]["jxsj"];

                    string dwdm = dt_dwzxx.Rows[i]["dwdm"].ToString();
                    DataTable dt_lctcdata1 = dt_lctxsum.AsEnumerable().Where(m => Convert.ToString(m["dwdm"]) == dwdm ).Count()>0? dt_lctxsum.AsEnumerable().Where(m => Convert.ToString(m["dwdm"]) == dwdm).CopyToDataTable():null;

                        DataTable dt_lctcdata2 = dt_lctcdata1.AsEnumerable().Where(m => Convert.ToString(m["sfxhwp"]) != "1").Count()>0? dt_lctcdata1.AsEnumerable().Where(m => Convert.ToString(m["sfxhwp"]) != "1").CopyToDataTable():null;
                        DataTable dt_lctcdata = dt_lctcdata2.AsEnumerable().Where(m => Convert.ToString(m["czfs"]) == "拟销毁物品").Count()>0? dt_lctcdata2.AsEnumerable().Where(m => Convert.ToString(m["czfs"]) == "拟销毁物品").CopyToDataTable():null;
                        if (dt_lctcdata != null){
                            if (dt_lctcdata.Rows.Count > 0)
                            {
                                decimal wps = 0;
                                for (int k = 0; k < dt_lctcdata.Rows.Count; k++)
                                {
                                    if (!string.IsNullOrEmpty(dt_lctcdata.Rows[k]["lb"].ToString()))
                                    {
                                        wps = wps + Convert.ToDecimal(dt_lctcdata.Rows[k]["hj"].ToString() == "" ? "0" : dt_lctcdata.Rows[k]["hj"].ToString());
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
                            }
                        }
                        
                    dt.Rows.Add(dr);
                }
            }


            #region 临时代码
            //for (int m = 0; m < 8; m++)
            //{
            //    dt.Columns.Add();
            //}
            //for (int i = 0; i < ds.Tables[0].Rows.Count; i++)
            //{
            //    dt.Rows.Add();
            //    for (int j = 0; j < 7; j++)
            //    {
            //        dt.Rows[i][j] = "0";
            //    }
            //    dt.Rows[i][0] = (i + 1).ToString();
            //    dt.Rows[i][1] = ds.Tables[0].Rows[i]["dwmc"].ToString(); //单位名称

            //    //}
            //    dt.Rows[i][2] = ds.Tables[0].Rows[i]["szss"].ToString(); //所在省
            //    dt.Rows[i][3] = ds.Tables[0].Rows[i]["szs"].ToString(); //所在市
            //    string dwjb = string.Empty;
            //    if (!string.IsNullOrEmpty(ds.Tables[0].Rows[i]["dwjb"].ToString()))
            //    {
            //        switch (ds.Tables[0].Rows[i]["dwjb"].ToString())
            //        {
            //            case "0":
            //                dwjb = "";
            //                break;
            //            case "1":
            //                dwjb = "军委机关部门";
            //                break;

            //            case "2":
            //                dwjb = "正战区级";
            //                break;
            //            case "3":
            //                dwjb = "副战区级";
            //                break;
            //            case "4":
            //                dwjb = "正军级";
            //                break;
            //            case "5":
            //                dwjb = "副军级";
            //                break;
            //            case "6":
            //                dwjb = "正师级";
            //                break;
            //            case "7":
            //                dwjb = "副师级";
            //                break;
            //            case "8":
            //                dwjb = "正团级";
            //                break;
            //            case "9":
            //                dwjb = "副团级";
            //                break;
            //            default:
            //                dwjb = "营以下单位";
            //                break;
            //        }
            //    }
            //    dt.Rows[i][4] = dwjb; //单位级别
            //    dt.Rows[i][5] = Convert.ToDecimal(ds.Tables[0].Rows[i]["kysl"].ToString()).ToString("0.#####"); //商品数量
            //    string sql_jy = string.Format("select sum(kysl) as jysz from t_lctc where dwdm='{0}' and lb='金银' and jldw='克' and  czfs='拟移交物品'  " + filterStr + " ", ds.Tables[0].Rows[i]["dwdm"].ToString());
            //    DataTable dt_jy = AccessHelper.getDataSet(sql_jy).Tables[0];

            //    if (dt_jy.Rows.Count > 0)
            //    {
            //        if (!string.IsNullOrEmpty(dt_jy.Rows[0]["jysz"].ToString()))
            //        {
            //            dt.Rows[i][6] = "金银含量：" + Convert.ToDecimal(dt_jy.Rows[0]["jysz"]).ToString("0.#####") + "克";
            //        }
            //        else
            //        {
            //            dt.Rows[i][6] = "";
            //        }
            //    }
            //}
            #endregion

            dataGridView1.DataSource = dt;
            dt_dcdata = dt;
            }
            catch (Exception ex )
            {
                MessageBox.Show("异常信息：" + ex);
                throw;
            }
        }

        private void button2_Click(object sender, EventArgs e)
        {
            AccessHelper AccessHelper = new AccessHelper();
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
                ExcelUI.OpenExcel_WPXHJHB(dt_dcdata, Application.StartupPath + "\\report\\A30\\销毁物品-导出表格.xlsx", textBox1.Text + "\\部队留存名贵特产类物品销毁计划表.xlsx", 4, 0, 8, sDwmc);
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
