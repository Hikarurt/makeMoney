using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
//using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.Data.OleDb;

namespace zj
{
    public partial class report_A4 : Form
    {


        private AccessHelper m_accessHelper = new AccessHelper();

        public report_A4()
        {

            InitializeComponent();
            select_report select_report = new select_report();
            dataGridView1.DataSource = select_report.report_SUMALL();
            dataGridView1.Columns[0].HeaderCell.Value = "序号";
            dataGridView1.Columns[1].HeaderCell.Value = "单位名称";
            dataGridView1.Columns[2].HeaderCell.Value = "类别";
            dataGridView1.Columns[3].HeaderCell.Value = "数量";
            dataGridView1.Columns[4].HeaderCell.Value = "总值";
            dataGridView1.Columns[5].HeaderCell.Value = "堪用数量";
            dataGridView1.Columns[6].HeaderCell.Value = "可变现价值";
            dataGridView1.Columns[7].HeaderCell.Value = "处置方式";
        }



        //导出到EXCEL
        private void button2_Click(object sender, EventArgs e)
        {
            if (textBox1.Text == "")
            {
                MessageBox.Show("请选择报表输出路径!");
                return;
            }
            DataTable dt1 = dataGridView1.DataSource as DataTable;
            if (dt1 == null)
            {
                MessageBox.Show("未提取统计数据，不能导出！");
                return;
            }
            ExcelUI.OpenExcel_A4_3(dt1, Application.StartupPath + "\\report\\A4\\留存名贵特产类物品明细汇总表.xlsx", textBox1.Text + "\\留存名贵特产类物品明细汇总表.xlsx", 3, 0, 8);
            //if (comboBox1.Text.Substring(0, 4) == "表1-1")
        }

        private void button2_Click_1(object sender, EventArgs e)
        {
            int num1 = 0;

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
            dr["序号"] = "单位：" + sDwmc; ;
            dr["计量单位"] = "统计日期：" + DateTime.Now.ToLongDateString().ToString(); ;
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

            string sql_sumALLA = " SELECT sum(zz) AS zz1,sum(sl) as sll , sum(kysl) AS kysl1, sum(kbxjz) AS kbxjz1 FROM t_lctc ";
            DataTable dt_sumALLA = AccessHelper.getDataSet(sql_sumALLA).Tables[0];
            string sql_sumALL = " SELECT sum(zz) AS zz1,sum(sl) as sll , sum(kysl) AS kysl1, sum(kbxjz) AS kbxjz1 FROM t_lctc where dwdm='000'";
            DataTable dt_sumALL = AccessHelper.getDataSet(sql_sumALL).Tables[0];
            if (dt_sumALL.Rows.Count > 0)
            {
                dr = dt1.NewRow();
                dr["序号"] = "合计";
                dr["单位名称"] = "";
                dr["总值"] = dt_sumALLA.Rows[0]["zz1"].ToString();
                dr["可变现价值"] = dt_sumALLA.Rows[0]["kbxjz1"].ToString();
                dt1.Rows.Add(dr);
              
            }

            string sql_select_yhzhtj = "select t_lctc.lb,t_lctc.czfs,t_lctc.dwdm,t_lctc.jldw ,sum(t_lctc.sl) as sl, sum(t_lctc.zz) as zz, sum(t_lctc.kysl) as kysl, sum(t_lctc.kbxjz) as kbxjz from t_lctc  WHERE t_lctc.dwdm ='000'   group by t_lctc.lb,t_lctc.czfs,t_lctc.dwdm,t_lctc.jldw";
            DataTable ds = AccessHelper.getDataSet(sql_select_yhzhtj).Tables[0];
            if (ds.Rows.Count > 0)
            {
                num1++;
                dr = dt1.NewRow();
                dr["序号"] = NumberToChinese(num1);
                dr["单位名称"] = sDwmc + "本级";
                dr["类别"] = "小计";
                dr["总值"] = dt_sumALL.Rows[0]["zz1"].ToString();
                dr["可变现价值"] = dt_sumALL.Rows[0]["kbxjz1"].ToString();
                dt1.Rows.Add(dr);
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

            string sql_alldwdm = "SELECT dwdm  FROM t_dwxx where dwdm <> '000' and len(dwdm)<6 group by dwdm order by dwdm";
            DataTable dt_alldwdm = AccessHelper.getDataSet(sql_alldwdm).Tables[0];
            if (dt_alldwdm.Rows.Count > 0)
            {
                for (int i = 0; i < dt_alldwdm.Rows.Count; i++)
                {
                    string bs = "";
                    num1++;
                    bs = NumberToChinese(num1);
                    string sql_dwmcxyj = "select dwmc from t_dwxx where dwdm='" + dt_alldwdm.Rows[i]["dwdm"] + "'";
                    DataTable get_dwmcxyj = AccessHelper.getDataSet(sql_dwmcxyj).Tables[0];
                    string dwmc = "";
                    if (get_dwmcxyj.Rows.Count > 0)
                    {
                        dwmc = get_dwmcxyj.Rows[0]["dwmc"].ToString();
                    }
                    string sql_sumALL1 = " SELECT sum(zz) AS zz1,sum(sl) as sll , sum(kysl) AS kysl1, sum(kbxjz) AS kbxjz1 FROM t_lctc where dwdm like '" + dt_alldwdm.Rows[i]["dwdm"] + "%'";
                    DataTable dt_sumALL1 = AccessHelper.getDataSet(sql_sumALL1).Tables[0];
                    if (dt_sumALL1.Rows.Count > 0)
                    {
                       
                        dr = dt1.NewRow();
                        dr["序号"] = bs;
                        dr["单位名称"] = dwmc;
                        dr["类别"] = "小计";
                        dr["总值"] = dt_sumALL1.Rows[0]["zz1"].ToString();
                        dr["可变现价值"] = dt_sumALL1.Rows[0]["kbxjz1"].ToString();
                        dt1.Rows.Add(dr);


                        string sql_select_yhzhtj1 = "select t_lctc.lb,t_lctc.czfs,t_lctc.dwdm,t_lctc.jldw ,sum(t_lctc.sl) as sl, sum(t_lctc.zz) as zz, sum(t_lctc.kysl) as kysl, sum(t_lctc.kbxjz) as kbxjz from t_lctc  WHERE t_lctc.dwdm  like '" + dt_alldwdm.Rows[i]["dwdm"].ToString() + "%'   group by t_lctc.lb,t_lctc.czfs,t_lctc.dwdm,t_lctc.jldw";
                        DataTable ds1 = AccessHelper.getDataSet(sql_select_yhzhtj1).Tables[0];
                        if (ds1.Rows.Count > 0)
                        {
                            for (int s = 0; s < ds1.Rows.Count; s++)
                            {
                                dr = dt1.NewRow();
                                dr["序号"] = "";
                                dr["单位名称"] = "";
                                dr["类别"] = ds1.Rows[s]["lb"].ToString();
                                dr["数量"] = ds1.Rows[s]["sl"].ToString();
                                dr["计量单位"] = ds1.Rows[s]["jldw"].ToString();
                                dr["总值"] = ds1.Rows[s]["zz"].ToString();
                                dr["堪用数量"] = ds1.Rows[s]["kysl"].ToString();
                                dr["可变现价值"] = ds1.Rows[s]["kbxjz"].ToString();
                                dr["处置方式"] = ds1.Rows[s]["czfs"].ToString();
                                dr["备注"] = "";
                                dt1.Rows.Add(dr);
                            }
                            //判断是否为本级单位
                            //string sql_xjsl = "SELECT distinct dwdm from t_dwxx where dwdm like '" + dt_alldwdm.Rows[i]["dwdm"].ToString() + "%' and len(dwdm)<9";
                            //DataTable dt_xjsl = AccessHelper.getDataSet(sql_xjsl).Tables[0];
                            //if (dt_xjsl.Rows.Count > 0)
                            //{
                            //    for (int q = 0; q < dt_xjsl.Rows.Count; q++)
                            //    {
                            //        string sql_sumALL1xjj = "";
                            //        if (dt_xjsl.Rows[q]["dwdm"].ToString()== dt_alldwdm.Rows[i]["dwdm"].ToString())
                            //        {
                            //             sql_sumALL1xjj = " SELECT sum(zz) AS zz1,sum(sl) as sll , sum(kysl) AS kysl1, sum(kbxjz) AS kbxjz1 FROM t_lctc where dwdm = '" + dt_xjsl.Rows[q]["dwdm"].ToString() + "'";
                            //        }
                            //        else
                            //        {
                            //             sql_sumALL1xjj = " SELECT sum(zz) AS zz1,sum(sl) as sll , sum(kysl) AS kysl1, sum(kbxjz) AS kbxjz1 FROM t_lctc where dwdm like '" + dt_xjsl.Rows[q]["dwdm"].ToString() + "%'";
                            //        }

                            //        DataTable dt_sumALL1xjj = AccessHelper.getDataSet(sql_sumALL1xjj).Tables[0];

                            //        if (dt_sumALL1xjj.Rows.Count > 0)
                            //        {


                            //            string sql_select_yhzhtj1 = "select t_lctc.lb,t_lctc.czfs,t_lctc.dwdm,t_lctc.jldw ,sum(t_lctc.sl) as sl, sum(t_lctc.zz) as zz, sum(t_lctc.kysl) as kysl, sum(t_lctc.kbxjz) as kbxjz from t_lctc  WHERE t_lctc.dwdm ='" + dt_xjsl.Rows[q]["dwdm"].ToString() + "'   group by t_lctc.lb,t_lctc.czfs,t_lctc.dwdm,t_lctc.jldw";
                            //            DataTable ds1 = AccessHelper.getDataSet(sql_select_yhzhtj1).Tables[0];
                            //            if (ds1.Rows.Count > 0)
                            //            {
                            //                string sql_dwmcxyj1 = "select dwmc from t_dwxx where dwdm='" + dt_xjsl.Rows[q]["dwdm"] + "'";
                            //                DataTable get_dwmcxyj1 = AccessHelper.getDataSet(sql_dwmcxyj1).Tables[0];

                            //                string dwmc1 = "";
                            //                if (get_dwmcxyj.Rows.Count > 0)
                            //                {
                            //                    dwmc1 = get_dwmcxyj1.Rows[0]["dwmc"].ToString();
                            //                }
                            //                bs1 = "(" + NumberToChinese(num2) + ")";
                            //                dr = dt1.NewRow();
                            //                dr["序号"] = bs1;
                            //                if (dt_xjsl.Rows[q]["dwdm"].ToString() == dt_alldwdm.Rows[i]["dwdm"].ToString())
                            //                {
                            //                    dr["单位名称"] = dwmc1 + "本级";
                            //                }
                            //                else
                            //                {
                            //                    dr["单位名称"] = dwmc1;
                            //                }
                            //                dr["类别"] = "小计";
                            //                dr["总值"] = dt_sumALL1xjj.Rows[0]["zz1"].ToString();
                            //                dr["可变现价值"] = dt_sumALL1xjj.Rows[0]["kbxjz1"].ToString();
                            //                dt1.Rows.Add(dr);
                            //                num2++;
                            //                for (int s = 0; s < ds1.Rows.Count; s++)
                            //                {
                            //                    dr = dt1.NewRow();
                            //                    dr["序号"] = "";
                            //                    dr["单位名称"] = "";
                            //                    dr["类别"] = ds1.Rows[s]["lb"].ToString();
                            //                    dr["数量"] = ds1.Rows[s]["sl"].ToString();
                            //                    dr["计量单位"] = ds1.Rows[s]["jldw"].ToString();
                            //                    dr["总值"] = ds1.Rows[s]["zz"].ToString();
                            //                    dr["堪用数量"] = ds1.Rows[s]["kysl"].ToString();
                            //                    dr["可变现价值"] = ds1.Rows[s]["kbxjz"].ToString();
                            //                    dr["处置方式"] = ds1.Rows[s]["czfs"].ToString();
                            //                    dr["备注"] = "";
                            //                    dt1.Rows.Add(dr);
                            //                }
                            //            }
                            //        }

                        }

                            //dr = dt1.NewRow();
                            //dr["序号"] = bs1;
                            //dr["单位名称"] = kg + dwmc + "本级";
                            //dr["类别"] = "小计";
                            //dr["总值"] = dt_sumALL1.Rows[0]["zz1"].ToString();
                            //dr["可变现价值"] = dt_sumALL1.Rows[0]["kbxjz1"].ToString();
                            //dt1.Rows.Add(dr);


                         
                        }
                    }
                }
            if (dt1 == null)
            {
                MessageBox.Show("统计数据为空，不能导出！");
                return;
            }

            ExcelUI.OpenExcel_A4_3(dt1, Application.StartupPath + "\\report\\A30\\所属单位汇总统计表.xlsx", textBox1.Text + "\\所属单位汇总统计表.xlsx", 1, 0, 10);
            MessageBox.Show("导出成功!");
            label1.Visible = false;


        }

        private void button4_Click(object sender, EventArgs e)
        {
            folderBrowserDialog1.SelectedPath = "";
            if (folderBrowserDialog1.ShowDialog() == DialogResult.OK)
                textBox1.Text = folderBrowserDialog1.SelectedPath;
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

        private void button4_Click_1(object sender, EventArgs e)
        {
            folderBrowserDialog1.SelectedPath = "";
            if (folderBrowserDialog1.ShowDialog() == DialogResult.OK)
                textBox1.Text = folderBrowserDialog1.SelectedPath;
        }

        private void button3_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void report_A4_Load(object sender, EventArgs e)
        {
            this.WindowState = FormWindowState.Maximized;
        }
    }
}
