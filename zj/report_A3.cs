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
    public partial class report_A3 : Form
    {
        private AccessHelper m_accessHelper = new AccessHelper();

        public report_A3()
        {
            this.WindowState = FormWindowState.Maximized;
            InitializeComponent();
            Load += new EventHandler(FrmBcardTJ_Load);
        }

        private void FrmBcardTJ_Load(object sender, EventArgs e)
        {
            select_report select_report = new select_report();


            //本级单位包括本级数据以及部门数据
            //  dataGridView1.DataSource = select_report.report_A3_1("");
            dataGridView1.DataSource = select_report.report_BJDW();
            dataGridView1.Columns[0].HeaderCell.Value = "序号";
            dataGridView1.Columns[1].HeaderCell.Value = "部门名称";
            dataGridView1.Columns[2].HeaderCell.Value = "类别";
            dataGridView1.Columns[3].HeaderCell.Value = "品名";
            dataGridView1.Columns[4].HeaderCell.Value = "来源";
            dataGridView1.Columns[5].HeaderCell.Value = "获取时间";
            dataGridView1.Columns[6].HeaderCell.Value = "数量";
            dataGridView1.Columns[7].HeaderCell.Value = "计量单位";
            dataGridView1.Columns[8].HeaderCell.Value = "单价类型";
            dataGridView1.Columns[9].HeaderCell.Value = "单价";
            dataGridView1.Columns[10].HeaderCell.Value = "总值";
            dataGridView1.Columns[11].HeaderCell.Value = "堪用数量";
            dataGridView1.Columns[12].HeaderCell.Value = "可变现价值";
            dataGridView1.Columns[13].HeaderCell.Value = "处置方式（建议）";
            dataGridView1.Columns[14].HeaderCell.Value = "备注";

            //LoadComboParams();
            //Load0therDta_Bydw();
        }



        //导出到EXCEL
        private void button2_Click(object sender, EventArgs e)
        {
            AccessHelper AccessHelper = new AccessHelper();
            if (textBox1.Text == "")
            {
                MessageBox.Show("请选择报表输出路径!");
                return;
            }

            label1.Visible = true;
            // ShowDialogForm sdf = new ShowDialogForm("提示", "正在登录......", "请耐心等候，正在验证您的身份！");

            DataTable dt1 = new DataTable();
            dt1.Columns.Add(new DataColumn("序号", typeof(string)));
            dt1.Columns.Add(new DataColumn("部门名称", typeof(string)));
            dt1.Columns.Add(new DataColumn("类别", typeof(string)));
            dt1.Columns.Add(new DataColumn("品名", typeof(string)));
            dt1.Columns.Add(new DataColumn("来源", typeof(string)));
            dt1.Columns.Add(new DataColumn("获取时间", typeof(string)));
            dt1.Columns.Add(new DataColumn("数量", typeof(string)));
            dt1.Columns.Add(new DataColumn("计量单位", typeof(string)));
            dt1.Columns.Add(new DataColumn("单价类型", typeof(string)));
            dt1.Columns.Add(new DataColumn("单价", typeof(string)));
            dt1.Columns.Add(new DataColumn("总值", typeof(string)));
            dt1.Columns.Add(new DataColumn("堪用数量", typeof(string)));
            dt1.Columns.Add(new DataColumn("可变现价值", typeof(string)));
            dt1.Columns.Add(new DataColumn("处置方式", typeof(string)));
            dt1.Columns.Add(new DataColumn("备注", typeof(string)));

            DataRow dr;

            //DataTable dt1 = dataGridView1.DataSource as DataTable;
            //获取所有单位
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
                dr["计量单位"] = "统计日期："+ DateTime.Now.ToLongDateString().ToString(); ;
                dr["处置方式"] = "金额单位：元";
                dt1.Rows.Add(dr);
                dr = dt1.NewRow();
                dr["序号"] = "序号";
                dr["部门名称"] = "部门名称";
                dr["类别"] = "类别";
                dr["品名"] = "品名";
                dr["来源"] = "来源";
                dr["获取时间"] = "获取时间";
                dr["数量"] = "数量";
                dr["计量单位"] = "计量单位";
                dr["单价类型"] = "单价类型";
                dr["单价"] = "单价";
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
                    dr["总值"] = dt_sumALL.Rows[0]["zz1"].ToString();
                    dr["可变现价值"] = dt_sumALL.Rows[0]["kbxjz1"].ToString();
                    dt1.Rows.Add(dr);
                }
                int num1 = 0;
                int num2 = 0;
               

            string sql_xjbmall = "select * from t_bm where bmbs in (select distinct bmbs  from t_lctc where dwdm ='000'  ) and dwdm='000' order by bmfjdm,bmdm";
            DataTable dt_xjbmall = AccessHelper.getDataSet(sql_xjbmall).Tables[0];
            if (dt_xjbmall.Rows.Count > 0)
            {
                string fjdmbs = "";
                for (int i = 0; i < dt_xjbmall.Rows.Count; i++)
                {
                   if(dt_xjbmall.Rows[i]["bmfjdm"].ToString()!= fjdmbs)
                    {
                        fjdmbs = dt_xjbmall.Rows[i]["bmfjdm"].ToString();

                        string sql_bjbmnum = "select  sum(zz) AS zz1,sum(sl) as sll , sum(kysl) AS kysl1, sum(kbxjz) AS kbxjz1 FROM t_lctc where t_lctc.bmbs in(select bmbs from t_bm where bmfjdm = '" + dt_xjbmall.Rows[i]["bmfjdm"].ToString() + "' and dwdm='000')";
                        DataTable dt_bjbmnum = AccessHelper.getDataSet(sql_bjbmnum).Tables[0];

                        if (dt_bjbmnum.Rows.Count > 0)
                        {
                            string sql_getbmmc = "select bmmc from t_bm where bmfjdm='" + dt_xjbmall.Rows[i]["bmfjdm"].ToString() + "'";
                            DataTable dt_bmmc = AccessHelper.getDataSet(sql_getbmmc).Tables[0];

                            num1++;
                            dr = dt1.NewRow();
                            dr["序号"] = NumberToChinese(num1);
                            if (dt_bmmc.Rows.Count > 0)
                            {
                                dr["部门名称"] = dt_bmmc.Rows[0]["bmmc"].ToString();
                            }
                            else
                            {
                                dr["部门名称"] = "";
                            }
                            dr["类别"] = "小计";
                            //dr["数量"] = dt_bjbmnum.Rows[0]["sll"].ToString();
                            dr["总值"] = dt_bjbmnum.Rows[0]["zz1"].ToString();
                            //dr["堪用数量"] = dt_bjbmnum.Rows[0]["kysl1"].ToString();
                            dr["可变现价值"] = dt_bjbmnum.Rows[0]["kbxjz1"].ToString();
                            dt1.Rows.Add(dr);
                        }
                        num2 = 0;
                    }
                    
                        string sql_bjbmxj = "select  sum(zz) AS zz1,sum(sl) as sll , sum(kysl) AS kysl1, sum(kbxjz) AS kbxjz1 FROM t_lctc where t_lctc.bmbs ='"+ dt_xjbmall.Rows[i]["bmbs"].ToString() + "'";
                        DataTable dt_bjbmxj = AccessHelper.getDataSet(sql_bjbmxj).Tables[0];
                        if (dt_bjbmxj.Rows.Count > 0)
                        {
                            string sql_getbmmcxj = "select bmmc from t_bm where bmbs='" + dt_xjbmall.Rows[i]["bmbs"].ToString() + "'";
                            DataTable dt_getbmmcxj = AccessHelper.getDataSet(sql_getbmmcxj).Tables[0];

                            num2++;
                            dr = dt1.NewRow();
                            dr["序号"] ="("+ NumberToChinese(num2)+")";
                            if (dt_getbmmcxj.Rows.Count > 0)
                            {
                                dr["部门名称"] = "      "+dt_getbmmcxj.Rows[0]["bmmc"].ToString();
                            }
                            else
                            {
                                dr["部门名称"] = "";
                            }
                            dr["类别"] = "小计";
                            dr["总值"] = dt_bjbmxj.Rows[0]["zz1"].ToString();
                            dr["可变现价值"] = dt_bjbmxj.Rows[0]["kbxjz1"].ToString();
                            dt1.Rows.Add(dr);

                            string tclcmx_bm = "select * from t_lctc where dwdm = '000' and bmbs=  '" + dt_xjbmall.Rows[i]["bmbs"] + "' order by t_lctc.lb,t_lctc.ly,t_lctc.jldw,t_lctc.djlx,t_lctc.czfs,t_lctc.id";
                            DataTable dt_lctcmx = AccessHelper.getDataSet(tclcmx_bm).Tables[0];
                            if (dt_lctcmx.Rows.Count > 0)
                            {
                                for (int s = 0; s < dt_lctcmx.Rows.Count; s++)
                                {
                                    dr = dt1.NewRow();
                                    dr["序号"] = "";
                                    dr["部门名称"] = "";
                                    dr["类别"] = dt_lctcmx.Rows[s]["lb"];
                                    dr["品名"] = dt_lctcmx.Rows[s]["pm"];
                                    dr["来源"] = dt_lctcmx.Rows[s]["ly"];
                                    dr["获取时间"] = dt_lctcmx.Rows[s]["hqsj"];
                                    dr["数量"] = dt_lctcmx.Rows[s]["sl"];
                                    dr["计量单位"] = dt_lctcmx.Rows[s]["jldw"];
                                    dr["单价类型"] = dt_lctcmx.Rows[s]["djlx"];
                                    dr["单价"] = dt_lctcmx.Rows[s]["dj"];
                                    dr["总值"] = dt_lctcmx.Rows[s]["zz"];
                                    dr["堪用数量"] = dt_lctcmx.Rows[s]["kysl"];
                                    dr["可变现价值"] = dt_lctcmx.Rows[s]["kbxjz"];
                                    dr["处置方式"] = dt_lctcmx.Rows[s]["czfs"];
                                    dr["备注"] = dt_lctcmx.Rows[s]["bz"];
                                    dt1.Rows.Add(dr);
                            }
                        }
                    }
                }
            }
            if (dt1 == null)
            {
                MessageBox.Show("统计数据为空，不能导出！");
                return;
            }
            ExcelUI.OpenExcel_A4_3(dt1, Application.StartupPath + "\\report\\A30\\留存名贵特产类物品明细统计表.xlsx", textBox1.Text + "\\留存名贵特产类物品明细统计表.xlsx", 1, 0,15);
            MessageBox.Show("导出成功!");
            label1.Visible = false;

        }

        private void button3_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void comboBox1_SelectedIndexChanged(object sender, EventArgs e)
        {
            dataGridView1.DataSource = null;
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
            Func<int, List<List<int>>> splitNumFunc = (val) => {
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
            Func<List<List<int>>, string> hommizationFunc = (data) => {
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

        private void button5_Click(object sender, EventArgs e)
        {
            String sql_select_yhzhtj = "select t_dwxx.dwdm,t_dwxx.dwmc,t_dqck.sfkhh,t_dqck.hb,count(t_dqck.id) as sl,sum(t_dqck.je) as ye from t_dwxx,t_dqck where left(t_dqck.dwdm,3)=t_dwxx.dwdm group by t_dwxx.dwdm,t_dwxx.dwmc,t_dqck.sfkhh,t_dqck.hb order by t_dwxx.dwdm";
            AccessHelper AccessHelper = new AccessHelper();
            DataSet ds = AccessHelper.getDataSet(sql_select_yhzhtj);
            dataGridView1.DataSource = ds.Tables[0];
        }

        private void report_A3_Load(object sender, EventArgs e)
        {
            //  comboBox1.SelectedIndex = 0;
            this.WindowState = FormWindowState.Maximized;
        }

        private void button5_Click_1(object sender, EventArgs e)
        {
            //String sql_select_yhzhtj = "select t_dwxx.dwdm,t_dwxx.dwmc,t_yhzh.zhlb,count(t_yhzh.id) as sl,sum(t_yhzh.ckye)/10000 as ye from t_dwxx,t_yhzh where left(t_yhzh.dwdm,3)=t_dwxx.dwdm group by t_dwxx.dwdm,t_dwxx.dwmc,t_yhzh.zhlb order by t_dwxx.dwdm";
            String sql_select_yhzhtj = "select t_dwxx.dwdm,t_dwxx.dwmc,t_yhzh.zhlb,t_yhzh.zhmc from t_dwxx,t_yhzh where left(t_yhzh.dwdm,3)=t_dwxx.dwdm and t_dwxx.dwdm='001'";

            AccessHelper AccessHelper = new AccessHelper();
            DataSet ds = AccessHelper.getDataSet(sql_select_yhzhtj);
            dataGridView1.DataSource = ds.Tables[0];
        }

        private void panel1_Paint(object sender, PaintEventArgs e)
        {

        }

        private void dataGridView1_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {

        }

        //private void comboDWDM_SelectedValueChanged(object sender, EventArgs e)
        //{
        //    string dwdm = comboDWDM.SelectedValue.ToString();

        //    if (!string.IsNullOrEmpty(dwdm))
        //    {
        //        string tempSQL = "select bmbs,bmmc from t_bm where dwdm ='" + dwdm + "'";
        //        DataSet ds = m_accessHelper.getDataSet(tempSQL);

        //        cbBM.DataSource = ds.Tables[0];
        //        cbBM.ValueMember = "bmbs";
        //        cbBM.DisplayMember = "bmmc";
        //    }
        //}

        //private void checkBox1_CheckedChanged(object sender, EventArgs e)
        //{
        //    if (checkBox1.Checked == true)
        //    {
        //        cbBM.Visible = true;
        //        label14.Visible = true;
        //    }
        //    else
        //    {
        //        cbBM.Visible = false;
        //        label14.Visible = false;
        //    }
        //}
    }
}
