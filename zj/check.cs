using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
//using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace zj
{
    public partial class check : Form
    {
        public check()
        {
            InitializeComponent();
        }

        private void check_Load(object sender, EventArgs e)
        {
            String sql_dwxx = "select dwdm,dwdm+dwmc as newdwmc from t_dwxx where len(dwdm)<4 order by dwdm asc";
            AccessHelper AccessHelper = new AccessHelper();
            DataSet ds = AccessHelper.getDataSet(sql_dwxx);
            comboBox1.DataSource = ds.Tables[0];
            comboBox1.ValueMember = "dwdm";
            comboBox1.DisplayMember = "newdwmc";
        }

        /// <summary>
        /// 审核选定单位
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void button1_Click(object sender, EventArgs e)
        {
            #region 审核方式变更
            #region 注释
            //String DwdmForCheck = comboBox1.SelectedValue.ToString();
            //DataTable dt1 = new DataTable();
            //DataSet ds1 = new DataSet();
            //dt1.Columns.Add();
            //dt1.Columns.Add();
            //dt1.Columns.Add();
            //int cc = 0;
            //AccessHelper AccessHelper = new AccessHelper();
            //String sql = "";

            ////判断所有单位是否填写银行账户情况明细表
            //sql = "select t_dwxx.dwdm,t_dwxx.dwmc from t_dwxx where not exists(select dwdm from t_yhzh where t_dwxx.dwdm=t_yhzh.dwdm) and  left(t_dwxx.dwdm,3)='" + DwdmForCheck + "'";
            //ds1 = AccessHelper.getDataSet(sql);
            //cc = dt1.Rows.Count;
            //for (int i = cc; i < cc + ds1.Tables[0].Rows.Count; i++)
            //{
            //    dt1.Rows.Add();
            //    dt1.Rows[i][0] = ds1.Tables[0].Rows[i-cc]["dwmc"];
            //    dt1.Rows[i][1] = "严重错误";
            //    dt1.Rows[i][2] = "未填写银行账户情况明细表！";
            //}


            ////所有单位必须填写资金结存表
            //sql = "select t_dwxx.dwdm,t_dwxx.dwmc from t_dwxx where not exists(select dwdm from t_zjjc where t_dwxx.dwdm=t_zjjc.dwdm) and  left(t_dwxx.dwdm,3)='" + DwdmForCheck + "'";
            //ds1 = AccessHelper.getDataSet(sql);
            //cc = dt1.Rows.Count;
            //for (int i = cc; i < cc + ds1.Tables[0].Rows.Count; i++)
            //{
            //    dt1.Rows.Add();
            //    dt1.Rows[i][0] = ds1.Tables[0].Rows[i-cc]["dwmc"];
            //    dt1.Rows[i][1] = "严重错误";
            //    dt1.Rows[i][2] = "未填写资金结存情况统计表！";
            //}

            ////所有单位必须填写公务卡发放使用情况统计表
            //sql = "select t_dwxx.dwdm,t_dwxx.dwmc from t_dwxx where not exists(select dwdm from t_gwk where t_dwxx.dwdm=t_gwk.dwdm) and  left(t_dwxx.dwdm,3)='" + DwdmForCheck + "'";
            //ds1 = AccessHelper.getDataSet(sql);
            //cc = dt1.Rows.Count;
            //for (int i = cc; i < cc + ds1.Tables[0].Rows.Count; i++)
            //{
            //    dt1.Rows.Add();
            //    dt1.Rows[i][0] = ds1.Tables[0].Rows[i - cc]["dwmc"];
            //    dt1.Rows[i][1] = "严重错误";
            //    dt1.Rows[i][2] = "未填写公务卡发放使用情况统计表！";
            //}

            ////所有单位必须填写现金使用情况统计表
            //sql = "select t_dwxx.dwdm,t_dwxx.dwmc from t_dwxx where not exists(select dwdm from t_xjsy where t_dwxx.dwdm=t_xjsy.dwdm) and  left(t_dwxx.dwdm,3)='" + DwdmForCheck + "'";
            //ds1 = AccessHelper.getDataSet(sql);
            //cc = dt1.Rows.Count;
            //for (int i = cc; i < cc + ds1.Tables[0].Rows.Count; i++)
            //{
            //    dt1.Rows.Add();
            //    dt1.Rows[i][0] = ds1.Tables[0].Rows[i - cc]["dwmc"];
            //    dt1.Rows[i][1] = "严重错误";
            //    dt1.Rows[i][2] = "未填写现金使用情况统计表！";
            //}
            #endregion
            //所有单位必须填写账户资金清理检查处理情况统计表
            //sql = "select t_dwxx.dwdm,t_dwxx.dwmc from t_dwxx where not exists(select dwdm from t_yhzhqccl where t_dwxx.dwdm=t_yhzhqccl.dwdm) and  left(t_dwxx.dwdm,3)='" + DwdmForCheck + "'";
            //ds1 = AccessHelper.getDataSet(sql);
            //cc = dt1.Rows.Count;
            //for (int i = cc; i < cc + ds1.Tables[0].Rows.Count; i++)
            //{
            //    dt1.Rows.Add();
            //    dt1.Rows[i][0] = ds1.Tables[0].Rows[i - cc]["dwmc"];
            //    dt1.Rows[i][1] = "严重错误";
            //    dt1.Rows[i][2] = "未填写账户资金清理检查处理情况统计表！";
            //}
            /*       


            //非队列单位不填写定期存款明细表
            sql = "select t_dwxx.dwdm,t_dwxx.dwmc from t_dwxx,t_dqck where left(t_dwxx.dwdm,3)='" + DwdmForCheck + "'and t_dqck.dwdm=t_dwxx.dwdm and t_dwxx.dwxz<>'队列单位'";
            ds1 = AccessHelper.getDataSet(sql);
            cc = dt1.Rows.Count;
            for (int i = cc; i < cc + ds1.Tables[0].Rows.Count; i++)
            {
                dt1.Rows.Add();
                dt1.Rows[i][0] = ds1.Tables[0].Rows[i-cc]["dwmc"];
                dt1.Rows[i][1] = "严重错误";
                dt1.Rows[i][2] = "非队列单位不需要填写定期存款明细表！";
            } */
            /*
            //通知存款的存款期限填写不正确
            sql = "select t_dwxx.dwdm,t_dwxx.dwmc from t_dwxx,t_dqck where left(t_dwxx.dwdm,3)='" + DwdmForCheck + "' and t_dqck.dwdm=t_dwxx.dwdm and t_dqck.cklb='通知存款'and t_dqck.ckqx<>'通知存款无期限'";
            ds1 = AccessHelper.getDataSet(sql);
            cc = dt1.Rows.Count;
            for (int i = cc; i < cc + ds1.Tables[0].Rows.Count; i++)
            {
                dt1.Rows.Add();
                dt1.Rows[i][0] = ds1.Tables[0].Rows[i - cc]["dwmc"];
                dt1.Rows[i][1] = "严重错误";
                dt1.Rows[i][2] = "通知存款的存款期限填写不正确！";
            }
            //定期存款的存款期限填写不正确
            sql = "select t_dwxx.dwdm,t_dwxx.dwmc from t_dwxx,t_dqck where left(t_dwxx.dwdm,3)='" + DwdmForCheck + "' and t_dqck.dwdm=t_dwxx.dwdm and t_dqck.cklb='定期存款'and t_dqck.ckqx='通知存款无期限'";
            ds1 = AccessHelper.getDataSet(sql);
            cc = dt1.Rows.Count;
            for (int i = cc; i < cc + ds1.Tables[0].Rows.Count; i++)
            {
                dt1.Rows.Add();
                dt1.Rows[i][0] = ds1.Tables[0].Rows[i - cc]["dwmc"];
                dt1.Rows[i][1] = "严重错误";
                dt1.Rows[i][2] = "定期存款的存款期限填写不正确！";
            }
             
            //定期存款在开户行存储时开户银行信息是否一致(在数据录入时检查)
            sql = "select t_dwxx.dwdm,t_dwxx.dwmc from t_dwxx,t_dqck where not exists(select t_dqck.dwdm from t_dqck,t_yhzh where left(t_dqck.dwdm,3)='" + DwdmForCheck + "' and t_yhzh.dwdm=t_dqck.dwdm and t_dqck.zhmc=t_yhzh.zhmc and t_dqck.hb=t_yhzh.hb and t_dqck.khh=t_yhzh.khh) and left(t_dwxx.dwdm,3)='" + DwdmForCheck + "' and t_dwxx.dwdm=t_dqck.dwdm and t_dqck.sfkhh='是'";
            ds1 = AccessHelper.getDataSet(sql);
            cc = dt1.Rows.Count;
            for (int i = cc; i < cc + ds1.Tables[0].Rows.Count; i++)
            {
                dt1.Rows.Add();
                dt1.Rows[i][0] = ds1.Tables[0].Rows[i - cc]["dwmc"];
                dt1.Rows[i][1] = "严重错误";
                dt1.Rows[i][2] = "定期存款的开户行与银行账户不一致！";
            }
             */
            #region 注释
            //判断是否录入12个月现金使用信息
            //sql = "select t_dwxx.dwmc,count(t_xjsy.yf) as cnum from t_xjsy,t_dwxx where t_xjsy.dwdm=t_dwxx.dwdm and  left(t_xjsy.dwdm,3)='" + DwdmForCheck + "' group by t_dwxx.dwmc";
            //ds1 = AccessHelper.getDataSet(sql);
            //cc = dt1.Rows.Count;
            //int k = 0;
            //for (int i = cc; i < cc + ds1.Tables[0].Rows.Count; i++)
            //{
            //    if (int.Parse(ds1.Tables[0].Rows[i - cc]["cnum"].ToString()) < 12)
            //    {
            //        dt1.Rows.Add();
            //        dt1.Rows[cc + k][0] = ds1.Tables[0].Rows[i - cc]["dwmc"];
            //        dt1.Rows[cc + k][1] = "严重错误";
            //        dt1.Rows[cc + k][2] = "只填写了" + ds1.Tables[0].Rows[i - cc]["cnum"].ToString() + "个月现金使用情况统计信息！";
            //        k++;
            //    }
            //}
            #endregion
            //判断资金结存情况是否满足条件
            //sql = "select t_dwxx.dwmc,kcxj + hqck + dqck + yjzq + kcwzye+ bcwcxjf + bcjsxjf + bczfzxjf + bczxzj+zfk as sum1,round(ysjf,2) + round(dnyswjf,2) + round(lnjfjy,2) + round(brzxzj,2)+round(swgyjj,2) + round(zczxzj,2) + round(ysjjf,2) + round(brwcxjf,2) + round(brjsxjf,2) + round(brzfzxjf,2) + round(zyjj,2) + round(dtf,2) + round(lydtf,2) + round(zsk,2) as sum2 from t_dwxx,t_zjjc where t_zjjc.dwdm=t_dwxx.dwdm and t_dwxx.dwxz='队列单位' and  left(t_zjjc.dwdm,3)='" + DwdmForCheck + "'";
            //sql = "select t_dwxx.dwmc from t_dwxx,t_zjjc where t_zjjc.dwdm=t_dwxx.dwdm and t_dwxx.dwxz='队列单位' and  left(t_zjjc.dwdm,3)='" + DwdmForCheck + "' ";
            ////and round(kcxj +hqck + dqck + yjzq + kcwzye + bcwcxjf + bcjsxjf + bczfzxjf + bczxzj + zfk,2)<> round(ysjf + dnyswjf + lnjfjy + brzxzj + swgyjj + zczxzj + ysjjf + brwcxjf + brjsxjf + brzfzxjf + zyjj + dtf + lydtf + zsk, 2)
            //ds1 = AccessHelper.getDataSet(sql);
            //cc = dt1.Rows.Count;
            ////Decimal sum1 = 0, sum2 = 0;
            //for (int i = cc; i < cc + ds1.Tables[0].Rows.Count; i++)
            //{
            //    /*
            //    sum1 = decimal.Parse(ds1.Tables[0].Rows[i - cc]["sum1"].ToString());
            //    sum2 = decimal.Parse(ds1.Tables[0].Rows[i - cc]["sum2"].ToString());
            //    if (sum1 != sum2)
            //    {
            //        dt1.Rows.Add();
            //        dt1.Rows[cc + k][0] = ds1.Tables[0].Rows[i - cc]["dwmc"];
            //        dt1.Rows[cc + k][1] = "严重错误";
            //        dt1.Rows[cc + k][2] = "队列单位资金结存情况统计表不满足：资金结存=资金来源-资金占用！"+sum1.ToString()+"  "+sum2.ToString();
            //        k++;
            //    }
            //     */
            //    dt1.Rows.Add();
            //    dt1.Rows[i][0] = ds1.Tables[0].Rows[i - cc]["dwmc"];
            //    dt1.Rows[i][1] = "严重错误";
            //    dt1.Rows[i][2] = "队列单位资金结存情况统计表不满足：资金结存=资金来源-资金占用！";
            //}

            #region 注释
            //MDIMain MDIMain = (MDIMain)this.MdiParent;
            //MDIMain.pass = 1;   //修改状态为审核通过
            //gather();
            //MessageBox.Show("共出现 "+dt1.Rows.Count.ToString()+" 条错误或者提示信息！","审核提示",MessageBoxButtons.OK,MessageBoxIcon.Information);
            //dataGridView1.DataSource = dt1;
            //dataGridView1.Columns[0].HeaderCell.Value = "单位";
            //dataGridView1.Columns[1].HeaderCell.Value = "错误类型";
            //dataGridView1.Columns[2].HeaderCell.Value = "具体原因";
            //dataGridView1.Columns[2].Width = 600;
            #endregion

            #endregion

            #region 新版
            String DwdmForCheck = comboBox1.SelectedValue.ToString();
            DataTable dt1 = new DataTable();
            DataTable dt2 = new DataTable();
            DataSet ds1 = new DataSet();
            dt1.Columns.Add();
            dt1.Columns.Add();
            dt1.Columns.Add();
            dt2.Columns.Add();
            dt2.Columns.Add();
            dt2.Columns.Add();
            int cc = 0;
            AccessHelper AccessHelper = new AccessHelper();
            String sql = "";

            if (DwdmForCheck == "000")
            {
                sql = "select t_dwxx.dwdm,t_dwxx.dwmc from t_dwxx where not exists(select dwdm from t_lctc where t_dwxx.dwdm=t_lctc.dwdm) and  left(t_dwxx.dwdm,3)='" + DwdmForCheck + "'  ";
            }
            else
            {
                sql = "select t_dwxx.dwdm,t_dwxx.dwmc from t_dwxx where not exists(select dwdm from t_lctc where t_dwxx.dwdm=t_lctc.dwdm) and  left(t_dwxx.dwdm,3)='" + DwdmForCheck + "'  AND t_dwxx.dwdm <> '000'";
            }
            //所有单位必须填写留存特产类物品汇总统计表
          
            ds1 = AccessHelper.getDataSet(sql);
            cc = dt1.Rows.Count;
            for (int i = cc; i < cc + ds1.Tables[0].Rows.Count; i++)
            {
                dt1.Rows.Add();
                dt1.Rows[i][0] = ds1.Tables[0].Rows[i - cc]["dwmc"];
                dt1.Rows[i][1] = "严重错误";
                dt1.Rows[i][2] = "未填写留存特产类物品汇总统计表！";
            }

            string sql_dwxx = string.Format("select dwmc from t_dwxx where  dwdm like '" + DwdmForCheck + "%' and (nxsh is null or nxs is null or nxs is null or jxzj is null or jxsj is null )");
                 DataTable dt_xx = AccessHelper.getDataSet(sql_dwxx).Tables[0];

            for (int k = 0; k < dt_xx.Rows.Count; k++)
            {
               DataRow dr = dt1.NewRow();

                dr[0] = dt_xx.Rows[k]["dwmc"];
                dr[1] = "严重错误";
                dr[2] = "单位信息不完整！";
                dt1.Rows.Add(dr);
            }
           
            MDIMain MDIMain = (MDIMain)this.MdiParent;
            MDIMain.pass = 1;   //修改状态为审核通过
            gather();
            if(dt1.Rows.Count.ToString()=="0")
            {
                MessageBox.Show("所选单位审核通过，可以上报数据!！", "审核提示", MessageBoxButtons.OK, MessageBoxIcon.Information);
               
            }
            else
            {
                MessageBox.Show("共出现 " + dt1.Rows.Count.ToString() + " 条错误或者提示信息！", "审核提示", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
            dataGridView1.DataSource = dt1;
            dataGridView1.Columns[0].HeaderCell.Value = "单位";
            dataGridView1.Columns[1].HeaderCell.Value = "错误类型";
            dataGridView1.Columns[2].HeaderCell.Value = "具体原因";
            dataGridView1.Columns[2].Width = 600;
            #endregion
        }

        /// <summary>
        /// 审核所有单位
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void button2_Click(object sender, EventArgs e)
        {
            #region 审核方式变更

            #region 注释
            //DataTable dt1 = new DataTable();
            //DataSet ds1 = new DataSet();
            //dt1.Columns.Add();
            //dt1.Columns.Add();
            //dt1.Columns.Add();
            //int cc = 0;
            //AccessHelper AccessHelper = new AccessHelper();
            //String sql = "";
            //int errornum = 0;

            ////判断所有单位是否填写银行账户情况明细表
            //sql = "select t_dwxx.dwdm,t_dwxx.dwmc from t_dwxx where not exists(select dwdm from t_yhzh where t_dwxx.dwdm=t_yhzh.dwdm)";
            //ds1 = AccessHelper.getDataSet(sql);
            //cc = dt1.Rows.Count;
            //for (int i = cc; i < cc + ds1.Tables[0].Rows.Count; i++)
            //{
            //    dt1.Rows.Add();
            //    dt1.Rows[i][0] = ds1.Tables[0].Rows[i - cc]["dwmc"];
            //    dt1.Rows[i][1] = "严重错误";
            //    dt1.Rows[i][2] = "未填写银行账户情况明细表！";
            //    errornum++;
            //}


            ////所有单位必须填写资金结存表
            //sql = "select t_dwxx.dwdm,t_dwxx.dwmc from t_dwxx where not exists(select dwdm from t_zjjc where t_dwxx.dwdm=t_zjjc.dwdm)";
            //ds1 = AccessHelper.getDataSet(sql);
            //cc = dt1.Rows.Count;
            //for (int i = cc; i < cc + ds1.Tables[0].Rows.Count; i++)
            //{
            //    dt1.Rows.Add();
            //    dt1.Rows[i][0] = ds1.Tables[0].Rows[i - cc]["dwmc"];
            //    dt1.Rows[i][1] = "严重错误";
            //    dt1.Rows[i][2] = "未填写资金结存情况统计表！";
            //    errornum++;
            //}

            ////所有单位必须填写现金使用情况统计表
            //sql = "select t_dwxx.dwdm,t_dwxx.dwmc from t_dwxx where not exists(select dwdm from t_xjsy where t_dwxx.dwdm=t_xjsy.dwdm)";
            //ds1 = AccessHelper.getDataSet(sql);
            //cc = dt1.Rows.Count;
            //for (int i = cc; i < cc + ds1.Tables[0].Rows.Count; i++)
            //{
            //    dt1.Rows.Add();
            //    dt1.Rows[i][0] = ds1.Tables[0].Rows[i - cc]["dwmc"];
            //    dt1.Rows[i][1] = "严重错误";
            //    dt1.Rows[i][2] = "未填写现金使用情况统计表！";
            //    errornum++;
            //}

            ////队列单位必须填写公务卡发放使用情况统计表
            //sql = "select t_dwxx.dwdm,t_dwxx.dwmc from t_dwxx where not exists(select dwdm from t_gwk where t_dwxx.dwdm=t_gwk.dwdm)";
            //ds1 = AccessHelper.getDataSet(sql);
            //cc = dt1.Rows.Count;
            //for (int i = cc; i < cc + ds1.Tables[0].Rows.Count; i++)
            //{
            //    dt1.Rows.Add();
            //    dt1.Rows[i][0] = ds1.Tables[0].Rows[i - cc]["dwmc"];
            //    dt1.Rows[i][1] = "严重错误";
            //    dt1.Rows[i][2] = "未填写公务卡发放使用情况统计表！";
            //    errornum++;
            //}
            #endregion

            //所有单位必须填写账户资金清理检查处理情况统计表
            //sql = "select t_dwxx.dwdm,t_dwxx.dwmc from t_dwxx where not exists(select dwdm from t_yhzhqccl where t_dwxx.dwdm=t_yhzhqccl.dwdm)";
            //ds1 = AccessHelper.getDataSet(sql);
            //cc = dt1.Rows.Count;
            //for (int i = cc; i < cc + ds1.Tables[0].Rows.Count; i++)
            //{
            //    dt1.Rows.Add();
            //    dt1.Rows[i][0] = ds1.Tables[0].Rows[i - cc]["dwmc"];
            //    dt1.Rows[i][1] = "严重错误";
            //    dt1.Rows[i][2] = "未填写账户资金清理检查处理情况统计表！";
            //    errornum++;
            //}

            //定期存款在开户行存储时开户银行信息不一致
            //sql = "select dwmc from t_dwxx where dwdm in (select t_dqck.dwdm from t_dqck where not exists(select t_yhzh.dwdm from t_yhzh where t_yhzh.dwdm=t_dqck.dwdm and t_dqck.zhmc=t_yhzh.zhmc and t_dqck.hb=t_yhzh.hb and t_dqck.khh=t_yhzh.khh) and t_dqck.sfkhh='是')";
            //ds1 = AccessHelper.getDataSet(sql);
            //cc = dt1.Rows.Count;
            //for (int i = cc; i < cc + ds1.Tables[0].Rows.Count; i++)
            //{
            //    dt1.Rows.Add();
            //    dt1.Rows[i][0] = ds1.Tables[0].Rows[i - cc]["dwmc"];
            //    dt1.Rows[i][1] = "严重错误";
            //    dt1.Rows[i][2] = "定期存款的开户行与银行账户不一致！";
            //    errornum++;
            //}

            #region 注释
            //判断7大行以外定期存款单位的性质
            //sql = "select t_dwxx.dwmc from t_dwxx,t_dqck where t_dwxx.dwdm=t_dqck.dwdm and t_dwxx.dwjb<>'副大军区级' and t_dwxx.dwjb<>'大军区级' and t_dwxx.dwjb<>'总部' and t_dwxx.dwjb<>'总后财务部' and t_dqck.bzyy='军区级单位存于七家行'";
            //ds1 = AccessHelper.getDataSet(sql);
            //cc = dt1.Rows.Count;
            //for (int i = cc; i < cc + ds1.Tables[0].Rows.Count; i++)
            //{
            //    dt1.Rows.Add();
            //    dt1.Rows[i][0] = ds1.Tables[0].Rows[i - cc]["dwmc"];
            //    dt1.Rows[i][1] = "严重错误";
            //    dt1.Rows[i][2] = "该单位定期存款存储于非开户行原因‘军区级单位存于七家行’与单位级别不符！";
            //    errornum++;
            //}
            ////判断5大行以外定期存款单位的性质
            //sql = "select t_dwxx.dwmc from t_dwxx,t_dqck where t_dwxx.dwdm=t_dqck.dwdm and t_dwxx.dwjb<>'军级' and t_dqck.bzyy='军级单位存于五家行'";
            //ds1 = AccessHelper.getDataSet(sql);
            //cc = dt1.Rows.Count;
            //for (int i = cc; i < cc + ds1.Tables[0].Rows.Count; i++)
            //{
            //    dt1.Rows.Add();
            //    dt1.Rows[i][0] = ds1.Tables[0].Rows[i - cc]["dwmc"];
            //    dt1.Rows[i][1] = "严重错误";
            //    dt1.Rows[i][2] = "该单位定期存款存储于非开户行原因‘军级单位存于五家行’与单位级别不符！";
            //    errornum++;
            //}
            #endregion

            //判断资金结存情况是否满足条件
            //sql = "select t_dwxx.dwmc from t_dwxx,t_zjjc where t_zjjc.dwdm=t_dwxx.dwdm and t_dwxx.dwxz='队列单位' and round(kcxj + hqck + dqck + yjzq + kcwzye - swgyjj + bcwcxjf + bcjsxjf + bczfzxjf + zfk,2)<>round(ysjf + dnyswjf + lnjfjy + brzxzj - bczxzj + zczxzj + ysjjf + brwcxjf + brjsxjf + brzfzxjf + zyjj + dtf + lydtf + zsk,2)";
            //ds1 = AccessHelper.getDataSet(sql);
            //cc = dt1.Rows.Count;
            //for (int i = cc; i < cc + ds1.Tables[0].Rows.Count; i++)
            //{
            //    dt1.Rows.Add();
            //    dt1.Rows[i][0] = ds1.Tables[0].Rows[i - cc]["dwmc"];
            //    dt1.Rows[i][1] = "严重错误";
            //    dt1.Rows[i][2] = "队列单位资金结存情况统计表不满足：资金结存=资金来源-资金占用！";
            //    errornum++;
            //}

            #region 注释
            //判断是否录入12个月现金使用信息
            //sql = "select t_dwxx.dwmc,count(t_xjsy.yf) as cnum from t_xjsy,t_dwxx where t_xjsy.dwdm=t_dwxx.dwdm group by t_dwxx.dwmc";
            //ds1 = AccessHelper.getDataSet(sql);
            //cc = dt1.Rows.Count;
            //int k = 0;
            //for (int i = cc; i < cc + ds1.Tables[0].Rows.Count; i++)
            //{
            //    if (int.Parse(ds1.Tables[0].Rows[i - cc]["cnum"].ToString()) < 12)
            //    {
            //        dt1.Rows.Add();
            //        dt1.Rows[cc + k][0] = ds1.Tables[0].Rows[i - cc]["dwmc"];
            //        dt1.Rows[cc + k][1] = "严重错误";
            //        dt1.Rows[cc + k][2] = "只填写了" + ds1.Tables[0].Rows[i - cc]["cnum"].ToString() + "个月现金使用情况统计信息！";
            //        errornum++;
            //        k++;
            //    }
            //}

            //dataGridView1.DataSource = dt1;
            //dataGridView1.Columns[0].HeaderCell.Value = "单位";
            //dataGridView1.Columns[0].Width = 200;
            //dataGridView1.Columns[1].HeaderCell.Value = "错误类型";
            //dataGridView1.Columns[2].HeaderCell.Value = "具体原因";
            //dataGridView1.Columns[2].Width = 600;

            //if (errornum == 0)
            //{
            //    MDIMain MDIMain = (MDIMain)this.MdiParent;
            //    MDIMain.pass = 1;   //修改状态为审核通过
            //    gather();
            //    MessageBox.Show("审核通过，可以上报和打印报表!", "系统提示", MessageBoxButtons.OK, MessageBoxIcon.Information);
            //}
            //else {
            //    MessageBox.Show("共有 "+errornum.ToString()+" 个严重错误，暂不能生成上报盘！", "系统提示", MessageBoxButtons.OK, MessageBoxIcon.Information);
            //}

            #endregion

            #endregion

            #region 新版
            DataTable dt1 = new DataTable();
            DataSet ds1 = new DataSet();
            dt1.Columns.Add();
            dt1.Columns.Add();
            dt1.Columns.Add();
            int cc = 0;
            AccessHelper AccessHelper = new AccessHelper();
            String sql = "";
            int errornum = 0;


            //所有单位必须填写留存特产类物品汇总统计表
            sql = "select t_dwxx.dwdm,t_dwxx.dwmc from t_dwxx where not exists(select dwdm from t_lctc where t_dwxx.dwdm=t_lctc.dwdm) AND t_dwxx.dwdm <> '000' and t_dwxx.dwdm like '000%'";
            ds1 = AccessHelper.getDataSet(sql);
            cc = dt1.Rows.Count;
            for (int i = cc; i < cc + ds1.Tables[0].Rows.Count; i++)
            {
                dt1.Rows.Add();
                dt1.Rows[i][0] = ds1.Tables[0].Rows[i - cc]["dwmc"];
                dt1.Rows[i][1] = "严重错误";
                dt1.Rows[i][2] = "未填写留存特产类物品汇总统计表！";
                errornum++;
            }

            string sql_dwxx = string.Format("select dwmc from t_dwxx where  nxsh is null or nxs is null or nxs is null or jxzj is null or jxsj is null ");
            DataTable dt_xx = AccessHelper.getDataSet(sql_dwxx).Tables[0];


            for (int k = 0; k < dt_xx.Rows.Count; k++)
            {
                dt1.Rows.Add();
                dt1.Rows[k][0] = dt_xx.Rows[k]["dwmc"];
                dt1.Rows[k][1] = "严重错误";
                dt1.Rows[k][2] = "单位信息不完整！";
                errornum++;
            }

            dataGridView1.DataSource = dt1;
            dataGridView1.Columns[0].HeaderCell.Value = "单位";
            dataGridView1.Columns[0].Width = 200;
            dataGridView1.Columns[1].HeaderCell.Value = "错误类型";
            dataGridView1.Columns[2].HeaderCell.Value = "具体原因";
            dataGridView1.Columns[2].Width = 600;

            if (errornum == 0)
            {
                MDIMain MDIMain = (MDIMain)this.MdiParent;
                MDIMain.pass = 1;   //修改状态为审核通过
                gather();
                MessageBox.Show("审核通过，可以上报数据!", "系统提示", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
            else
            {
                MessageBox.Show("共有 " + errornum.ToString() + " 个严重错误，暂不能生成上报盘！", "系统提示", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }

            #endregion

        }
        //汇总处理数据
        public void gather()
        {
            #region 方式变更前
            //AccessHelper AccessHelper = new AccessHelper();
            //String sql_del = "delete from t_yhzh where dwdm not in (select dwdm from t_dwxx)";
            //AccessHelper.ExcueteCommand(sql_del);
            //sql_del = "delete from t_yhzhqccl where dwdm not in (select dwdm from t_dwxx)";
            //AccessHelper.ExcueteCommand(sql_del);
            //sql_del = "delete from t_dqck where dwdm not in (select dwdm from t_dwxx)";
            //AccessHelper.ExcueteCommand(sql_del);
            //sql_del = "delete from t_zjjc where dwdm not in (select dwdm from t_dwxx)";
            //AccessHelper.ExcueteCommand(sql_del);
            //sql_del = "delete from t_xjsy where dwdm not in (select dwdm from t_dwxx)";
            //AccessHelper.ExcueteCommand(sql_del);
            //sql_del = "delete from t_gwk where dwdm not in (select dwdm from t_dwxx)";
            //AccessHelper.ExcueteCommand(sql_del);

            ////将公务卡中现金支付金额由现金使用表中提取
            //String sql = "select dwdm,sum(rydyzc)+sum(zfgtjyzc)+sum(clfzc)+sum(yxzc)+sum(lxzc)+sum(bskzc)+sum(qtzc) as sumxj from t_xjsy group by dwdm";

            //DataSet ds = new DataSet();
            //ds = AccessHelper.getDataSet(sql);
            //double tempsum = 0;
            //String tempdwdm = "";
            //for (int i=0;i<ds.Tables[0].Rows.Count;i++){
            //    tempdwdm = ds.Tables[0].Rows[i]["dwdm"].ToString();
            //    tempsum = double.Parse(ds.Tables[0].Rows[i]["sumxj"].ToString());
            //    sql = "update t_gwk set xjzfje = " + tempsum + " where dwdm='" + tempdwdm + "'";
            //    AccessHelper.ExcueteCommand(sql);
            //}
            #endregion

            AccessHelper AccessHelper = new AccessHelper();

            String sql_del = "delete from t_lctc where dwdm not in (select dwdm from t_dwxx)";
            AccessHelper.ExcueteCommand(sql_del);
        }
    }
}
