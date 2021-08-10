using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;
using System.Data.OleDb;
//using System.Windows.Controls;
//using System.Windows.Converters;
//using System.Collections;


namespace zj
{
    public partial class FrmAssetsSaving : Form
    {
        
        private AccessHelper m_accessHelper = new AccessHelper();
        private int m_id,dwbs;
        private string m_dwdm ;
        private double m_kcxj, m_hqck, m_dqck, m_yjzq, m_ysjf, m_dnyswjf, m_lnjfjy, m_brzxzj, m_bczxzj, m_zczxzj, m_ysjjf;
        private double m_zyjj, m_brwcxjf, m_brjsxjf, m_brzfzxjf, m_dtf, m_lydtf, m_zsk, m_kcwzye, m_swgyjj, m_bcwcxjf, m_bcjsxjf, m_bczfzxjf, m_zfk,qt;

        public Boolean dwxz_yes = true; //true表示队列单位
        public String filterStr = "";

        private string SQL_Admin_Update = "UPDATE t_zjjc SET dwdm=@DWDM,kcxj=@KCXJ,hqck=@HQCK,dqck=@DQCK,yjzq=@YJZQ,ysjf=@YSJF,dnyswjf=@DNYSWJF,lnjfjy=@LNJFJY,brzxzj=@BRZXZJ,bczxzj=@BCZXZJ,zczxzj=@ZCZXZJ,ysjjf=@YSJJF,zyjj=@ZYJJ,brwcxjf=@BRWCXJF,brjsxjf=@BRJSXJF,brzfzxjf=@BRZFZXJF,dtf=@DTF,lydtf=@LYDTF,zsk=@ZSK,kcwzye=@KCWZYE,swgyjj=@SWGYJJ,bcwcxjf=@BCWCXJF,bcjsxjf=@BCJSXJF,bczfzxjf=@BCZFZXJF,zfk=@ZFK ,qt=@qt WHERE ID=@ID";
        private string SQL_Admin_Insert = "INSERT INTO t_zjjc(dwdm,kcxj,hqck,dqck,yjzq,ysjf,dnyswjf,lnjfjy,brzxzj,bczxzj,zczxzj,ysjjf,zyjj,brwcxjf,brjsxjf,brzfzxjf,dtf,lydtf,zsk,kcwzye,swgyjj,bcwcxjf,bcjsxjf,bczfzxjf,zfk,qt) values(@DWDM,@KCXJ,@HQCK,@DQCK,@YJZQ,@YSJF,@DNYSWJF,@LNJFJY,@BRZXZJ,@BCZXZJ,@ZCZXZJ,@YSJJF,@ZYJJ,@BRWCXJF,@BRJSXJF,@BRZFZXJF,@DTF,@LYDTF,@ZSK,@KCWZYE,@SWGYJJ,@BCWCXJF,@BCJSXJF,@BCZFZXJF,@ZFK,@qt)";
        private string SQL_Admin_Delete = "DELETE FROM t_zjjc WHERE ID=@ID";
        private Boolean notsaved = false; //标识数据是否修改，默认没有修改

        public void setnotsaved()
        {
            this.notsaved = true;
        }

        public void setsaved()
        {
            this.notsaved = false;
        }

        public FrmAssetsSaving()
        {
            InitializeComponent();
            Load += new EventHandler(FrmAssetsSaving_Load);
        }

        void FrmAssetsSaving_Load(object sender, EventArgs e)
        {
            LoadComboParams();
            ClearVariableData();
            ClearControlData();
            GetAllDataRefreshGridView();
            dwbrbcValue(); 
        }

        private void LoadComboParams()
        {
            string tempSQL = "SELECT dwmc,dwdm FROM t_dwxx";
            DataSet ds = m_accessHelper.getDataSet(tempSQL);
            comboDWDM.DataSource=ds.Tables[0];
            if (ds.Tables[0].Rows.Count == 0)
            {
                MessageBox.Show("请先录入单位信息！", "系统提示", MessageBoxButtons.OK, MessageBoxIcon.Information);
                dwbs = 1;
            }
            else
            {
                dwbs = 0;
            }
            comboDWDM.DisplayMember = "dwmc";
            comboDWDM.ValueMember = "dwdm";

           
        }

        public void GetAllDataRefreshGridView()
        {
            //string tempSQL = "SELECT * FROM t_zjjc ORDER BY ID";
            string tempSQL = "SELECT t_zjjc.ID as ID号,t_zjjc.dwdm as 单位代码,t_dwxx.dwmc as 单位名称,t_dwxx.dwxz as 单位性质,t_dwxx.dwjb as 单位级别,t_dwxx.dwlx as 单位类型,t_dwxx.szss as 所在省市,t_zjjc.kcxj as 库存现金,t_zjjc.hqck as 活期存款,t_zjjc.dqck as 定期（通知）存款,t_zjjc.yjzq as 有价证券,t_zjjc.ysjf as 预算经费结余,t_zjjc.dnyswjf as 预算外经费结余,t_zjjc.lnjfjy as 历年经费结余,t_zjjc.zczxzj as 自筹专项资金余额,t_zjjc.ysjjf as 应上缴经费,t_zjjc.zyjj as 专用基金余额, 0 as 上级拨款余额 ,t_zjjc.dtf as 党团费余额,t_zjjc.zsk as 暂收款,t_zjjc.kcwzye as 自购物资,0 as 对下拨款余额,0 as 借垫款,qt as 其他 FROM t_dwxx,t_zjjc  WHERE t_zjjc.dwdm=t_dwxx.dwdm " + filterStr + " ORDER BY t_zjjc.dwdm,t_zjjc.ID";
            DataSet ds = m_accessHelper.getDataSet(tempSQL);
            DataTable dt = ds.Tables[0];

            DataColumn dc = dt.Columns.Add("序号", typeof(int));
            dt.Columns["序号"].SetOrdinal(0);
            for (int i = 0; i < dt.Rows.Count; i++)
            {
                dt.Rows[i][0] = i + 1;
                if (!string.IsNullOrEmpty(dt.Rows[i]["单位代码"].ToString()))
                {
                    string sql_sjbk = "SELECT sum(brzcjf) as a,sum(brzfzxjf) as b,sum(brzxzj) as c,sum(bczcjf) as d,sum(bczfzxjf) as e,sum(bczxzj) as f FROM t_wlkx where dwdm='" + dt.Rows[i]["单位代码"].ToString() + "'; ";
                    DataSet dt_sjk = m_accessHelper.getDataSet(sql_sjbk);
                    double br = 0;
                    double bc = 0;
                    double jdk = 0;
                    if (!string.IsNullOrEmpty(dt_sjk.Tables[0].Rows[0]["a"].ToString()))
                    {
                        br += double.Parse(dt_sjk.Tables[0].Rows[0]["a"].ToString());
                    }
                    if (!string.IsNullOrEmpty(dt_sjk.Tables[0].Rows[0]["b"].ToString()))
                    {
                        br += double.Parse(dt_sjk.Tables[0].Rows[0]["b"].ToString());
                    }
                    if (!string.IsNullOrEmpty(dt_sjk.Tables[0].Rows[0]["c"].ToString()))
                    {
                        br += double.Parse(dt_sjk.Tables[0].Rows[0]["c"].ToString());
                    }
                    if (!string.IsNullOrEmpty(dt_sjk.Tables[0].Rows[0]["d"].ToString()))
                    {
                        bc += double.Parse(dt_sjk.Tables[0].Rows[0]["d"].ToString());
                    }
                    if (!string.IsNullOrEmpty(dt_sjk.Tables[0].Rows[0]["e"].ToString()))
                    {
                        bc += double.Parse(dt_sjk.Tables[0].Rows[0]["e"].ToString());
                    }
                    if (!string.IsNullOrEmpty(dt_sjk.Tables[0].Rows[0]["f"].ToString()))
                    {
                        bc += double.Parse(dt_sjk.Tables[0].Rows[0]["f"].ToString());
                    }
                    string sql_jdk = "  SELECT sum(je) as a FROM t_jdk   where dwdm='" + dt.Rows[i]["单位代码"].ToString() + "'; ";
                    DataSet dt_jdk = m_accessHelper.getDataSet(sql_jdk);

                    if (!string.IsNullOrEmpty(dt_jdk.Tables[0].Rows[0]["a"].ToString()))
                    {
                        jdk = double.Parse(dt_jdk.Tables[0].Rows[0]["a"].ToString());
                    }
                    dt.Rows[i]["上级拨款余额"] = br;
                    dt.Rows[i]["对下拨款余额"] = bc;
                    dt.Rows[i]["借垫款"] = jdk;
                }
            }

            dataGridView.DataSource = dt;
            dataGridView.Columns[1].Visible = false;

            dataGridView.ClearSelection();
            dataGridView.Columns["ID号"].Width = 60;
            dataGridView.Columns["单位名称"].Width = 200;
         //   dataGridView.Columns["'定期(通知)存款'"]. = "定期(通知)存款";
            if (dataGridView.SelectedRows.Count != 0)
            {
                m_id = int.Parse(dataGridView.SelectedRows[0].Cells[1].Value.ToString());
            }
            else
            {
                m_id = ClassConstants.JD_NOTSELECTED;
            }
        }

        public void UpdateData(OleDbParameter[] parms)
        {
            m_accessHelper.ExcueteCommand(SQL_Admin_Update, parms);
        }

        public void InsertData(OleDbParameter[] parms)
        {
            m_accessHelper.ExcueteCommand(SQL_Admin_Insert, parms);
        }

        public void DeleteData(int dataIndex)
        {
            OleDbParameter[] parms = new OleDbParameter[] { 
                new OleDbParameter("@ID",OleDbType.Integer)
            };
            parms[0].Value = dataIndex;

            m_accessHelper.ExcueteCommand(SQL_Admin_Delete, parms);
        }

        public DataTable GetDataByIndex(int dataIndex)
        {
            string tempSQL = "SELECT * FROM t_zjjc WHERE ID=" + dataIndex;
            DataTable dt = m_accessHelper.getDataSet(tempSQL).Tables[0];
            return dt;
        }


        private void btnAdd_Click(object sender, EventArgs e)
        {
            if (this.notsaved == true)//未保存的情况下
            {
                DialogResult dr = MessageBox.Show("数据已经修改，是否先保存数据？", "系统提示", MessageBoxButtons.OKCancel, MessageBoxIcon.Question);
                if (dr == DialogResult.OK)//执行保存
                {
                    this.btnSave_Click(sender, e);
                }
                else//执行新增加数据
                {
                    ClearControlData();
                    ClearVariableData();
                    dataGridView.ClearSelection();
                    setnotsaved();
                }
            }
            else
            {
                ClearControlData();
                ClearVariableData();
                dataGridView.ClearSelection();
                setnotsaved();
            }
        }

        private void btnRemove_Click(object sender, EventArgs e)
        {
            if (m_id != ClassConstants.JD_NOTSELECTED)
            {
                MessageBoxButtons msgBut = MessageBoxButtons.OKCancel;
                DialogResult dr = MessageBox.Show("确定删除此数据项？", "删除数据", msgBut,MessageBoxIcon.Question);
                if (dr == DialogResult.OK)
                {
                    DeleteData(m_id);
                    ClearControlData();
                    ClearVariableData();
                    GetAllDataRefreshGridView();
                }
            }
        }

        private void btnSave_Click(object sender, EventArgs e)
        {
            qt = double.Parse(tbQT.Text);
            m_bcjsxjf = double.Parse(tbBCJSXJF.Text);
            m_bcwcxjf = double.Parse(tbBCWCXJF.Text);
            m_bczfzxjf = double.Parse(tbBCZFZXJF.Text);
            m_bczxzj = double.Parse(tbBCZXZJ.Text);
            m_brjsxjf = double.Parse(tbBRJSXJF.Text);
            m_brwcxjf = double.Parse(tbBRWCXJF.Text);
            m_brzfzxjf = double.Parse(tbBRZFZXJF.Text);
            m_brzxzj = double.Parse(tbBRZXZJ.Text);
            m_dnyswjf = double.Parse(tbDNYSWJF.Text);
            m_dqck = double.Parse(tbDQCK.Text);
            m_dtf = double.Parse(tbDTF.Text);
            m_hqck = double.Parse(tbHQCK.Text);
            m_kcwzye = double.Parse(tbKCWZYE.Text);
            m_kcxj = double.Parse(tbKCXJ.Text);
            m_lnjfjy = double.Parse(tbLNJFJY.Text);
            m_lydtf = double.Parse(tbLYDTF.Text);
            m_swgyjj = double.Parse(tbSWGYJJ.Text);
            m_yjzq = double.Parse(tbYJZQ.Text);
            m_ysjf = double.Parse(tbYSJF.Text);
            m_ysjjf = double.Parse(tbYSJJF.Text);
            m_zczxzj = double.Parse(tbZCZXZJ.Text);
            m_zfk = double.Parse(tbZFK.Text);
            m_zsk = double.Parse(tbZSK.Text);
            m_zyjj = double.Parse(tbZYJJ.Text);

            //m_dwdm = ReturnDeptCode(comboDWDM.Text);
            m_dwdm = comboDWDM.SelectedValue.ToString();

            String selected_dwmc = comboDWDM.Text;
            String sql_dwxx = "select dwxz from t_dwxx where dwmc='" + selected_dwmc + "'";
            DataSet ds1 = m_accessHelper.getDataSet(sql_dwxx);
            if (ds1.Tables[0].Rows[0]["dwxz"].ToString() == "事业单位" || ds1.Tables[0].Rows[0]["dwxz"].ToString()=="保障性单位")
            {
                this.dwxz_yes = false;
            }
            else
            {
                this.dwxz_yes = true;
            }

            Decimal leftSum,middleSum,rightSum;
            leftSum = Convert.ToDecimal(LeftSum());
            middleSum = Convert.ToDecimal(MiddleSum());
            rightSum = Convert.ToDecimal(RightSum());
            if (dwxz_yes == true &&(leftSum + rightSum)!=middleSum)
            {
                MessageBoxButtons msgBut = MessageBoxButtons.OKCancel;
                DialogResult dr = MessageBox.Show("不满足以下条件:\r\n资金结存=资金来源-资金占用,不能保存？", "系统提示", msgBut,MessageBoxIcon.Question);
                if (dr == DialogResult.OK)
                {
                    return;
                }
                else
                    return;
            }
            //非队列单位
            if (dwxz_yes == false)
            {
                m_ysjf = m_dnyswjf = m_lnjfjy = m_brzxzj = m_bczxzj = m_zczxzj = m_ysjjf = m_brwcxjf = m_brjsxjf = m_brzfzxjf = m_zyjj = m_dtf = m_lydtf = m_zsk = m_kcwzye = m_swgyjj = m_bcwcxjf = m_bcjsxjf = m_bczfzxjf = m_zfk = 0;
                MessageBox.Show("请注意：非队列单位只需填写资金结存信息！", "系统提示", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }

            
            if (m_id != ClassConstants.JD_NOTSELECTED)
            {
                OleDbParameter[] parms = new OleDbParameter[] { 
                    new OleDbParameter("@DWDM",OleDbType.VarChar),
                    new OleDbParameter("@KCXJ",OleDbType.Double),
                    new OleDbParameter("@HQCK",OleDbType.Double),
                    new OleDbParameter("@DQCK",OleDbType.Double),
                    new OleDbParameter("@YJZQ",OleDbType.Double),
                    new OleDbParameter("@YSJF",OleDbType.Double),
                    new OleDbParameter("@DNYSWJF",OleDbType.Double),
                    new OleDbParameter("@LNJFJY",OleDbType.Double),
                    new OleDbParameter("@BRZXZJ",OleDbType.Double),
                    new OleDbParameter("@BCZXZJ",OleDbType.Double),
                    new OleDbParameter("@ZCZXZJ",OleDbType.Double),
                    new OleDbParameter("@YSJJF",OleDbType.Double),
                    new OleDbParameter("@ZYJJ",OleDbType.Double),
                    new OleDbParameter("@BRWCXJF",OleDbType.Double),
                    new OleDbParameter("@BRJSXJF",OleDbType.Double),
                    new OleDbParameter("@BRZFZXJF",OleDbType.Double),
                    new OleDbParameter("@DTF",OleDbType.Double),
                    new OleDbParameter("@LYDTF",OleDbType.Double),
                    new OleDbParameter("@ZSK",OleDbType.Double),
                    new OleDbParameter("@KCWZYE",OleDbType.Double),
                    new OleDbParameter("@SWGYJJ",OleDbType.Double),
                    new OleDbParameter("@BCWCXJF",OleDbType.Double),
                    new OleDbParameter("@BCJSXJF",OleDbType.Double),
                    new OleDbParameter("@BCZFZXJF",OleDbType.Double),
                    new OleDbParameter("@ZFK",OleDbType.Double),
                    new OleDbParameter("@QT",OleDbType.Double),

                    new OleDbParameter("@ID",OleDbType.Integer)
                };
                parms[0].Value = m_dwdm;
                parms[1].Value = m_kcxj;
                parms[2].Value = m_hqck;
                parms[3].Value = m_dqck;
                parms[4].Value = m_yjzq;
                parms[5].Value = m_ysjf;
                parms[6].Value = m_dnyswjf;
                parms[7].Value = m_lnjfjy;
                parms[8].Value = m_brzxzj;
                parms[9].Value = m_bczxzj;
                parms[10].Value = m_zczxzj;
                parms[11].Value = m_ysjjf;
                parms[12].Value = m_zyjj;
                parms[13].Value = m_brwcxjf;
                parms[14].Value = m_brjsxjf;
                parms[15].Value = m_brzfzxjf;
                parms[16].Value = m_dtf;
                parms[17].Value = m_lydtf;
                parms[18].Value = m_zsk;
                parms[19].Value = m_kcwzye;
                parms[20].Value = m_swgyjj;
                parms[21].Value = m_bcwcxjf;
                parms[22].Value = m_bcjsxjf;
                parms[23].Value = m_bczfzxjf;
                parms[24].Value = m_zfk;
                parms[25].Value = qt;
                parms[26].Value = m_id;

                UpdateData(parms);
                GetAllDataRefreshGridView();
                MessageBox.Show("更新数据完毕！","系统提示",MessageBoxButtons.OK,MessageBoxIcon.Information);
            }
            else
            {
                string tempSql = "SELECT * FROM t_zjjc WHERE dwdm='" + m_dwdm + "'";
                DataSet ds = m_accessHelper.getDataSet(tempSql);
                if (ds.Tables[0].Rows.Count != 0)
                {
                    MessageBox.Show("每个单位只需进行一次资金结存情况统计！", "系统提示");
                    return;
                }
                OleDbParameter[] parms = new OleDbParameter[] { 
                    new OleDbParameter("@DWDM",OleDbType.VarChar),
                    new OleDbParameter("@KCXJ",OleDbType.Double),
                    new OleDbParameter("@HQCK",OleDbType.Double),
                    new OleDbParameter("@DQCK",OleDbType.Double),
                    new OleDbParameter("@YJZQ",OleDbType.Double),
                    new OleDbParameter("@YSJF",OleDbType.Double),
                    new OleDbParameter("@DNYSWJF",OleDbType.Double),
                    new OleDbParameter("@LNJFJY",OleDbType.Double),
                    new OleDbParameter("@BRZXZJ",OleDbType.Double),
                    new OleDbParameter("@BCZXZJ",OleDbType.Double),
                    new OleDbParameter("@ZCZXZJ",OleDbType.Double),
                    new OleDbParameter("@YSJJF",OleDbType.Double),
                    new OleDbParameter("@ZYJJ",OleDbType.Double),
                    new OleDbParameter("@BRWCXJF",OleDbType.Double),
                    new OleDbParameter("@BRJSXJF",OleDbType.Double),
                    new OleDbParameter("@BRZFZXJF",OleDbType.Double),
                    new OleDbParameter("@DTF",OleDbType.Double),
                    new OleDbParameter("@LYDTF",OleDbType.Double),
                    new OleDbParameter("@ZSK",OleDbType.Double),
                    new OleDbParameter("@KCWZYE",OleDbType.Double),
                    new OleDbParameter("@SWGYJJ",OleDbType.Double),
                    new OleDbParameter("@BCWCXJF",OleDbType.Double),
                    new OleDbParameter("@BCJSXJF",OleDbType.Double),
                    new OleDbParameter("@BCZFZXJF",OleDbType.Double),
                    new OleDbParameter("@ZFK",OleDbType.Double),
                    new OleDbParameter("@QT",OleDbType.Double)
                };
                parms[0].Value = m_dwdm;
                parms[1].Value = m_kcxj;
                parms[2].Value = m_hqck;
                parms[3].Value = m_dqck;
                parms[4].Value = m_yjzq;
                parms[5].Value = m_ysjf;
                parms[6].Value = m_dnyswjf;
                parms[7].Value = m_lnjfjy;
                parms[8].Value = m_brzxzj;
                parms[9].Value = m_bczxzj;
                parms[10].Value = m_zczxzj;
                parms[11].Value = m_ysjjf;
                parms[12].Value = m_zyjj;
                parms[13].Value = m_brwcxjf;
                parms[14].Value = m_brjsxjf;
                parms[15].Value = m_brzfzxjf;
                parms[16].Value = m_dtf;
                parms[17].Value = m_lydtf;
                parms[18].Value = m_zsk;
                parms[19].Value = m_kcwzye;
                parms[20].Value = m_swgyjj;
                parms[21].Value = m_bcwcxjf;
                parms[22].Value = m_bcjsxjf;
                parms[23].Value = m_bczfzxjf;
                parms[24].Value = m_zfk;
                parms[25].Value = qt;

                InsertData(parms);
                GetAllDataRefreshGridView();
                MessageBox.Show("插入数据完毕！","系统提示",MessageBoxButtons.OK,MessageBoxIcon.Information);
            }
            setsaved();
            ClearControlData();
            ClearVariableData();
        }

        private void btnExit_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void ClearControlData()
        {
            tbBCJSXJF.Text = "0";
            tbBCWCXJF.Text = "0";
            tbBCZFZXJF.Text = "0";
            tbBCZXZJ.Text = "0";
            tbBRJSXJF.Text = "0";
            tbBRWCXJF.Text = "0";
            tbBRZFZXJF.Text = "0";
            tbBRZXZJ.Text = "0";
            tbDNYSWJF.Text = "0";
            tbDQCK.Text = "0";
            tbDTF.Text = "0";
            tbHQCK.Text = "0";
            tbKCWZYE.Text = "0";
            tbKCXJ.Text = "0";
            tbLNJFJY.Text = "0";
            tbLYDTF.Text = "0";
            tbSWGYJJ.Text = "0";
            tbYJZQ.Text = "0";
            tbYSJF.Text = "0";
            tbYSJJF.Text = "0";
            tbZCZXZJ.Text = "0";
            tbZFK.Text = "0";
            tbZSK.Text = "0";
            tbZYJJ.Text = "0";
            tbQT.Text = "0";

            lbLeftSum.Text = LeftSum().ToString();
            lbMiddleSum.Text = MiddleSum().ToString("n")+"万元";
            lbRightSum.Text = RightSum().ToString("n")+"万元";

            comboDWDM.Enabled = true;
            if (dwbs != 1)
            {
               comboDWDM.SelectedIndex = 0;
            }
            
        }

        private void ClearVariableData()
        {
            m_bcjsxjf = 0;
            m_bcwcxjf = 0;
            m_bczfzxjf = 0;
            m_bczxzj = 0;
            m_brjsxjf = 0;
            m_brwcxjf = 0;
            m_brzfzxjf = 0;
            m_brzxzj = 0;
            m_dnyswjf = 0;
            m_dqck = 0;
            m_dtf = 0;
            m_hqck = 0;
            m_kcwzye = 0;
            m_kcxj = 0;
            m_lnjfjy = 0;
            m_lydtf = 0;
            m_swgyjj = 0;
            m_yjzq = 0;
            m_ysjf = 0;
            m_ysjjf = 0;
            m_zczxzj = 0;
            m_zfk = 0;
            m_zsk = 0;
            m_zyjj = 0;
            qt = 0;

            if (dwbs != 1)
            {
                m_dwdm = comboDWDM.Items[0].ToString();
            }
            m_id = ClassConstants.JD_NOTSELECTED;
        }

        private string ReturnDeptCode(string deptName)
        {
            string tempSQL = "SELECT * FROM t_dwxx WHERE dwmc='" + deptName + "'";
            DataSet ds = m_accessHelper.getDataSet(tempSQL);
            return ds.Tables[0].Rows[0]["dwdm"].ToString();
        }

        public void ReadDataToVariables()
        {
            m_id = int.Parse(dataGridView.SelectedRows[0].Cells[1].Value.ToString());
            DataTable dt = this.GetDataByIndex(m_id);

            m_bcjsxjf = double.Parse(dt.Rows[0]["bcjsxjf"].ToString());
            m_bcwcxjf = double.Parse(dt.Rows[0]["bcwcxjf"].ToString());
            m_bczfzxjf = double.Parse(dt.Rows[0]["bczfzxjf"].ToString());
            m_bczxzj = double.Parse(dt.Rows[0]["bczxzj"].ToString());
            m_brjsxjf = double.Parse(dt.Rows[0]["brjsxjf"].ToString());
            m_brwcxjf = double.Parse(dt.Rows[0]["brwcxjf"].ToString());
            m_brzfzxjf = double.Parse(dt.Rows[0]["brzfzxjf"].ToString());
            m_brzxzj = double.Parse(dt.Rows[0]["brzxzj"].ToString());
            m_dnyswjf = double.Parse(dt.Rows[0]["dnyswjf"].ToString());
            m_dqck = double.Parse(dt.Rows[0]["dqck"].ToString());
            m_dtf = double.Parse(dt.Rows[0]["dtf"].ToString());
            m_hqck = double.Parse(dt.Rows[0]["hqck"].ToString());
            m_kcwzye = double.Parse(dt.Rows[0]["kcwzye"].ToString());
            m_kcxj = double.Parse(dt.Rows[0]["kcxj"].ToString());
            m_lnjfjy = double.Parse(dt.Rows[0]["lnjfjy"].ToString());
            m_lydtf = double.Parse(dt.Rows[0]["lydtf"].ToString());
            m_swgyjj = double.Parse(dt.Rows[0]["swgyjj"].ToString());
            m_yjzq = double.Parse(dt.Rows[0]["yjzq"].ToString());
            m_ysjf = double.Parse(dt.Rows[0]["ysjf"].ToString());
            m_ysjjf = double.Parse(dt.Rows[0]["ysjjf"].ToString());
            m_zczxzj = double.Parse(dt.Rows[0]["zczxzj"].ToString());
            m_zfk = double.Parse(dt.Rows[0]["zfk"].ToString());
            m_zsk = double.Parse(dt.Rows[0]["zsk"].ToString());
            m_zyjj = double.Parse(dt.Rows[0]["zyjj"].ToString());
            m_dwdm = dt.Rows[0]["dwdm"].ToString();
            qt =double.Parse(dt.Rows[0]["qt"].ToString());

            if (!string.IsNullOrEmpty(m_dwdm))
            {
                dwbcbrChange(m_dwdm);
               
            }
        }

        public void LoadVariablesToControls()
        {
            tbBCJSXJF.Text = m_bcjsxjf.ToString("n");
            tbBCWCXJF.Text = m_bcwcxjf.ToString("n");
            tbBCZFZXJF.Text = m_bczfzxjf.ToString("n");
            tbBCZXZJ.Text = m_bczxzj.ToString("n");
            tbBRJSXJF.Text = m_brjsxjf.ToString("n");
            tbBRWCXJF.Text = m_brwcxjf.ToString("n");
            tbBRZFZXJF.Text = m_brzfzxjf.ToString("n");
            tbBRZXZJ.Text = m_brzxzj.ToString("n");
            tbDNYSWJF.Text = m_dnyswjf.ToString("n");
            tbDQCK.Text = m_dqck.ToString("n");
            tbDTF.Text = m_dtf.ToString("n");
            tbHQCK.Text = m_hqck.ToString("n");
            tbKCWZYE.Text = m_kcwzye.ToString("n");
            tbKCXJ.Text = m_kcxj.ToString("n");
            tbLNJFJY.Text = m_lnjfjy.ToString("n");
            tbLYDTF.Text = m_lydtf.ToString("n");
            tbSWGYJJ.Text = m_swgyjj.ToString("n");
            tbYJZQ.Text = m_yjzq.ToString("n");
            tbYSJF.Text = m_ysjf.ToString("n");
            tbYSJJF.Text = m_ysjjf.ToString("n");
            tbZCZXZJ.Text = m_zczxzj.ToString("n");
            tbZFK.Text = m_zfk.ToString("n");
            tbZSK.Text = m_zsk.ToString("n");
            tbZYJJ.Text = m_zyjj.ToString("n");
            tbQT.Text = qt.ToString("n");

            lbRightSum.Text = RightSum().ToString("n")+"万元";
            lbLeftSum.Text = LeftSum().ToString("n")+"万元";
            lbMiddleSum.Text = MiddleSum().ToString("n")+"万元";

            /*
            string tempSQL = "SELECT * FROM t_dwxx WHERE dwdm='" + m_dwdm + "'";
            DataSet ds = m_accessHelper.getDataSet(tempSQL);
            string tempDeptName = ds.Tables[0].Rows[0]["dwmc"].ToString();
            tempDeptName = tempDeptName.Trim();
            comboDWDM.SelectedIndex = comboDWDM.Items.IndexOf(tempDeptName);
             */
            comboDWDM.SelectedValue = m_dwdm;
            comboDWDM.Enabled = false;
        }

        private void tbKCXJ_TextChanged(object sender, EventArgs e)
        {
            ClassInputRestricter.CheckChange(sender);
            //setnotsaved();
        }

        private void tbHQCK_TextChanged(object sender, EventArgs e)
        {
            ClassInputRestricter.CheckChange(sender);
            //setnotsaved();
        }

        private void tbDQCK_TextChanged(object sender, EventArgs e)
        {
            ClassInputRestricter.CheckChange(sender);
            //setnotsaved();
        }

        private void tbYJZQ_TextChanged(object sender, EventArgs e)
        {
            ClassInputRestricter.CheckChange(sender);
            //setnotsaved();
        }

        private void tbYSJF_TextChanged(object sender, EventArgs e)
        {
            ClassInputRestricter.CheckChange(sender);
            //setnotsaved();
        }

        private void tbDNYSWJF_TextChanged(object sender, EventArgs e)
        {
            ClassInputRestricter.CheckChange(sender);
            //setnotsaved();
        }

        private void tbLNJFJY_TextChanged(object sender, EventArgs e)
        {
            ClassInputRestricter.CheckChange(sender);
            //setnotsaved();
        }

        private void tbBRZXZJ_TextChanged(object sender, EventArgs e)
        {
            ClassInputRestricter.CheckChange(sender);
            //setnotsaved();
        }

        private void tbBCZXZJ_TextChanged(object sender, EventArgs e)
        {
            ClassInputRestricter.CheckChange(sender);
            //setnotsaved();
        }

        private void tbZCZXZJ_TextChanged(object sender, EventArgs e)
        {
            ClassInputRestricter.CheckChange(sender);
            //setnotsaved();
        }

        private void tbYSJJF_TextChanged(object sender, EventArgs e)
        {
            ClassInputRestricter.CheckChange(sender);
            //setnotsaved();
        }

        private void tbBRWCXJF_TextChanged(object sender, EventArgs e)
        {
            ClassInputRestricter.CheckChange(sender);
            //setnotsaved();
        }

        private void tbBRJSXJF_TextChanged(object sender, EventArgs e)
        {
            ClassInputRestricter.CheckChange(sender);
            //setnotsaved();
        }

        private void tbBRZFZXJF_TextChanged(object sender, EventArgs e)
        {
            ClassInputRestricter.CheckChange(sender);
            //setnotsaved();
        }

        private void tbZYJJ_TextChanged(object sender, EventArgs e)
        {
            ClassInputRestricter.CheckChange(sender);
            //setnotsaved();
        }

        private void tbDTF_TextChanged(object sender, EventArgs e)
        {
            ClassInputRestricter.CheckChange(sender);
            //setnotsaved();
        }

        private void tbLYDTF_TextChanged(object sender, EventArgs e)
        {
            ClassInputRestricter.CheckChange(sender);
            //setnotsaved();
        }

        private void tbZSK_TextChanged(object sender, EventArgs e)
        {
            ClassInputRestricter.CheckChange(sender);
            //setnotsaved();
        }

        private void tbKCWZYE_TextChanged(object sender, EventArgs e)
        {
            ClassInputRestricter.CheckChange(sender);
            //setnotsaved();
        }

        private void tbSWGYJJ_TextChanged(object sender, EventArgs e)
        {
            ClassInputRestricter.CheckChange(sender);
            //setnotsaved();
        }

        private void tbBCWCXJF_TextChanged(object sender, EventArgs e)
        {
            ClassInputRestricter.CheckChange(sender);
            //setnotsaved();
        }

        private void tbBCJSXJF_TextChanged(object sender, EventArgs e)
        {
            ClassInputRestricter.CheckChange(sender);
            //setnotsaved();
        }

        private void tbBCZFZXJF_TextChanged(object sender, EventArgs e)
        {
            ClassInputRestricter.CheckChange(sender);
            //setnotsaved();
        }

        private void tbZFK_TextChanged(object sender, EventArgs e)
        {
            ClassInputRestricter.CheckChange(sender);
            //setnotsaved();
        }

        private void btnExcel_Click(object sender, EventArgs e)
        {
            DataTable dt = dataGridView.DataSource as DataTable;
            ExcelUI ExcelUI = new ExcelUI();
            ExcelUI.ExportExcel(dt, "");
        }

        private void dataGridView_SelectionChanged(object sender, EventArgs e)
        {
            if (dataGridView.SelectedRows.Count != 0 && dataGridView.SelectedRows[0].Cells[1].Value.ToString() != "")
            {
                ReadDataToVariables();
                LoadVariablesToControls();
            }
        }

        private double LeftSum()
        {
            double digit1, digit2, digit3, digit4;
            digit1 = double.Parse(tbKCXJ.Text.Replace(",", ""));
            digit2 = double.Parse(tbHQCK.Text.Replace(",", ""));
            digit3 = double.Parse(tbDQCK.Text.Replace(",", ""));
            digit4 = double.Parse(tbYJZQ.Text.Replace(",", ""));
            return digit1 + digit2 + digit3 + digit4;
        }
       
        private double MiddleSum()
        {
            double[] digit = new double[15];

            digit[0] = double.Parse(tbYSJF.Text.Replace(",", ""));
            digit[1] = double.Parse(tbDNYSWJF.Text.Replace(",", ""));
            digit[2] = double.Parse(tbLNJFJY.Text.Replace(",", ""));
            digit[3] = double.Parse(tbBRZXZJ.Text.Replace(",", ""));
            digit[4] = double.Parse(tbSWGYJJ.Text.Replace(",", ""));
            digit[5] = double.Parse(tbZCZXZJ.Text.Replace(",", ""));
            digit[6] = double.Parse(tbYSJJF.Text.Replace(",", ""));
            digit[7] = double.Parse(tbBRWCXJF.Text.Replace(",", ""));
            digit[8] = double.Parse(tbBRJSXJF.Text.Replace(",", ""));
            digit[9] = double.Parse(tbBRZFZXJF.Text.Replace(",", ""));
            digit[10] = double.Parse(tbZYJJ.Text.Replace(",", ""));
            digit[11] = double.Parse(tbDTF.Text.Replace(",", ""));
            digit[12] = double.Parse(tbLYDTF.Text.Replace(",", ""));
            digit[13] = double.Parse(tbZSK.Text.Replace(",", ""));
            digit[14] = double.Parse(textBox1.Text.Replace(",", ""));
            double resultSum=0;
            for(int i=0;i<15;i++)
            {
                resultSum += digit[i];
            }
            return resultSum;
        }

        private double RightSum()
        {
            double digit1, digit2, digit3, digit4, digit5, digit6, digit7, digit8, digit9;
            digit1 = double.Parse(tbKCWZYE.Text.Replace(",", ""));
            //digit2 = double.Parse(tbSWGYJJ.Text.Replace(",", ""));
            digit2 = double.Parse(tbBCWCXJF.Text.Replace(",", ""));
            digit3 = double.Parse(tbBCJSXJF.Text.Replace(",", ""));
            digit4 = double.Parse(tbBCZFZXJF.Text.Replace(",", ""));
            digit5 = double.Parse(tbZFK.Text.Replace(",", ""));
            digit6= double.Parse(tbBCZXZJ.Text.Replace(",", ""));
            digit7= double.Parse(textBox2.Text.Replace(",", ""));
            digit8= double.Parse(tbQT.Text.Replace(",", ""));
            digit9= double.Parse(textBox3.Text.Replace(",", ""));

            return digit1 + digit2+digit3 + digit4 + digit5 + digit6 + digit7 + digit8+ digit9;
        }

        private void tbKCXJ_Leave(object sender, EventArgs e)
        {
            ClassInputRestricter.CheckFormat(sender);
            lbLeftSum.Text = LeftSum().ToString("n")+"万元";
        }

        private void tbHQCK_Leave(object sender, EventArgs e)
        {
            ClassInputRestricter.CheckFormat(sender);
            lbLeftSum.Text = LeftSum().ToString("n")+"万元";
        }

        private void tbDQCK_Leave(object sender, EventArgs e)
        {
            ClassInputRestricter.CheckFormat(sender);
            lbLeftSum.Text = LeftSum().ToString("n")+"万元";
        }

        private void tbYJZQ_Leave(object sender, EventArgs e)
        {
            ClassInputRestricter.CheckFormat(sender);
            lbLeftSum.Text = LeftSum().ToString("n")+"万元";
        }

        private void tbYSJF_Leave(object sender, EventArgs e)
        {
            ClassInputRestricter.CheckFormat(sender);
            lbMiddleSum.Text = MiddleSum().ToString("n")+"万元";

        }

        private void tbDNYSWJF_Leave(object sender, EventArgs e)
        {
            ClassInputRestricter.CheckFormat(sender);
            lbMiddleSum.Text = MiddleSum().ToString("n")+"万元";
        }

        private void tbLNJFJY_Leave(object sender, EventArgs e)
        {
            ClassInputRestricter.CheckFormat(sender);
            lbMiddleSum.Text = MiddleSum().ToString("n")+"万元";
        }

        private void tbBRZXZJ_Leave(object sender, EventArgs e)
        {
            ClassInputRestricter.CheckFormat(sender);
            lbMiddleSum.Text = MiddleSum().ToString("n")+"万元";
        }

        private void tbBCZXZJ_Leave(object sender, EventArgs e)
        {
            ClassInputRestricter.CheckFormat(sender);
            lbRightSum.Text = RightSum().ToString("n")+"万元";
        }

        private void tbZCZXZJ_Leave(object sender, EventArgs e)
        {
            ClassInputRestricter.CheckFormat(sender);
            lbMiddleSum.Text = MiddleSum().ToString("n")+"万元";
        }

        private void tbYSJJF_Leave(object sender, EventArgs e)
        {
            ClassInputRestricter.CheckFormat(sender);
            lbMiddleSum.Text = MiddleSum().ToString("n")+"万元";
        }

        private void tbBRWCXJF_Leave(object sender, EventArgs e)
        {
            ClassInputRestricter.CheckFormat(sender);
            lbMiddleSum.Text = MiddleSum().ToString("n")+"万元";
        }

        private void tbBRJSXJF_Leave(object sender, EventArgs e)
        {
            ClassInputRestricter.CheckFormat(sender);
            lbMiddleSum.Text = MiddleSum().ToString("n")+"万元";
        }

        private void tbBRZFZXJF_Leave(object sender, EventArgs e)
        {
            ClassInputRestricter.CheckFormat(sender);
            lbMiddleSum.Text = MiddleSum().ToString("n")+"万元";
        }

        private void tbZYJJ_Leave(object sender, EventArgs e)
        {
            ClassInputRestricter.CheckFormat(sender);
            lbMiddleSum.Text = MiddleSum().ToString("n")+"万元";
        }

        private void tbDTF_Leave(object sender, EventArgs e)
        {
            ClassInputRestricter.CheckFormat(sender);
            lbMiddleSum.Text = MiddleSum().ToString("n")+"万元";
        }

        private void tbLYDTF_Leave(object sender, EventArgs e)
        {
            ClassInputRestricter.CheckFormat(sender);
            lbMiddleSum.Text = MiddleSum().ToString("n")+"万元";
        }

        private void tbZSK_Leave(object sender, EventArgs e)
        {
            ClassInputRestricter.CheckFormat(sender);
            lbMiddleSum.Text = MiddleSum().ToString("n")+"万元";
        }

        private void tbKCWZYE_Leave(object sender, EventArgs e)
        {
            ClassInputRestricter.CheckFormat(sender);
            lbRightSum.Text = RightSum().ToString("n")+"万元";
        }

        private void tbSWGYJJ_Leave(object sender, EventArgs e)
        {
            ClassInputRestricter.CheckFormat(sender);
            lbMiddleSum.Text = MiddleSum().ToString("n")+"万元";
        }

        private void tbBCWCXJF_Leave(object sender, EventArgs e)
        {
            ClassInputRestricter.CheckFormat(sender);
            lbRightSum.Text = RightSum().ToString("n")+"万元";
        }

        private void tbBCJSXJF_Leave(object sender, EventArgs e)
        {
            ClassInputRestricter.CheckFormat(sender);
            lbRightSum.Text = RightSum().ToString("n")+"万元";
        }

        private void tbBCZFZXJF_Leave(object sender, EventArgs e)
        {
            ClassInputRestricter.CheckFormat(sender);
            lbRightSum.Text = RightSum().ToString("n")+"万元";
        }

        private void tbZFK_Leave(object sender, EventArgs e)
        {
            ClassInputRestricter.CheckFormat(sender);
            lbRightSum.Text = RightSum().ToString("n")+"万元";
        }

        private void tbKCXJ_KeyPress(object sender, KeyPressEventArgs e)
        {
            ClassInputRestricter.CheckInput(sender, e);
        }

        private void tbHQCK_KeyPress(object sender, KeyPressEventArgs e)
        {
            ClassInputRestricter.CheckInput(sender, e);
        }

        private void tbDQCK_KeyPress(object sender, KeyPressEventArgs e)
        {
            ClassInputRestricter.CheckInput(sender, e);
        }

        private void tbYJZQ_KeyPress(object sender, KeyPressEventArgs e)
        {
            ClassInputRestricter.CheckInput(sender, e);
        }

        public void dwbrbcValue()
        {
            if (dwbs != 1)
            {
                if (!string.IsNullOrEmpty(comboDWDM.SelectedValue.ToString()))
                {
                    dwbcbrChange(comboDWDM.SelectedValue.ToString());
                }
            }
            
        }

        public void dwbcbrChange(string data)
        {
            string sql_sjbk = "SELECT sum(brzcjf) as a,sum(brzfzxjf) as b,sum(brzxzj) as c,sum(bczcjf) as d,sum(bczfzxjf) as e,sum(bczxzj) as f FROM t_wlkx where dwdm='" + data + "'; ";
            DataSet dt_sjk = m_accessHelper.getDataSet(sql_sjbk);
            double br = 0;
            double bc = 0;
            double jdk = 0;
            if (!string.IsNullOrEmpty(dt_sjk.Tables[0].Rows[0]["a"].ToString()))
            {
                br += double.Parse(dt_sjk.Tables[0].Rows[0]["a"].ToString());
            }
            if (!string.IsNullOrEmpty(dt_sjk.Tables[0].Rows[0]["b"].ToString()))
            {
                br += double.Parse(dt_sjk.Tables[0].Rows[0]["b"].ToString());
            }
            if (!string.IsNullOrEmpty(dt_sjk.Tables[0].Rows[0]["c"].ToString()))
            {
                br += double.Parse(dt_sjk.Tables[0].Rows[0]["c"].ToString());
            }
            if (!string.IsNullOrEmpty(dt_sjk.Tables[0].Rows[0]["d"].ToString()))
            {
                bc += double.Parse(dt_sjk.Tables[0].Rows[0]["d"].ToString());
            }
            if (!string.IsNullOrEmpty(dt_sjk.Tables[0].Rows[0]["e"].ToString()))
            {
                bc += double.Parse(dt_sjk.Tables[0].Rows[0]["e"].ToString());
            }
            if (!string.IsNullOrEmpty(dt_sjk.Tables[0].Rows[0]["f"].ToString()))
            {
                bc += double.Parse(dt_sjk.Tables[0].Rows[0]["f"].ToString());
            }
            string sql_jdk = "  SELECT sum(je) as a FROM t_jdk   where dwdm='" + data + "'; ";
            DataSet dt_jdk = m_accessHelper.getDataSet(sql_jdk);

            if (!string.IsNullOrEmpty(dt_jdk.Tables[0].Rows[0]["a"].ToString()))
            {
                jdk = double.Parse(dt_jdk.Tables[0].Rows[0]["a"].ToString());
            }
            textBox1.Text = br.ToString();
            textBox2.Text = bc.ToString();
            textBox3.Text = jdk.ToString();
            lbRightSum.Text = RightSum().ToString("n") + "万元";
            lbMiddleSum.Text = MiddleSum().ToString("n") + "万元";

        }
        private void comboDWDM_TextChanged(object sender, EventArgs e)
        {
            dwbrbcValue();
           
        }

        private void textBox1_TextChanged(object sender, EventArgs e)
        {
            lbMiddleSum.Text = MiddleSum().ToString("n") + "万元";
        }

        private void textBox2_TextChanged(object sender, EventArgs e)
        {
            lbRightSum.Text = RightSum().ToString("n") + "万元";
        }

        private void tbQT_TextChanged(object sender, EventArgs e)
        {
            lbRightSum.Text = RightSum().ToString("n") + "万元";
        }

        private void textBox1_Leave(object sender, EventArgs e)
        {
            lbMiddleSum.Text = MiddleSum().ToString("n") + "万元";
        }

        private void tbQT_Leave(object sender, EventArgs e)
        {
            lbRightSum.Text = RightSum().ToString("n") + "万元";
        }

        private void textBox2_Leave(object sender, EventArgs e)
        {
            lbRightSum.Text = RightSum().ToString("n") + "万元";
        }

        private void textBox3_TextChanged(object sender, EventArgs e)
        {
            lbRightSum.Text = RightSum().ToString("n") + "万元";
        }

        private void textBox3_Leave(object sender, EventArgs e)
        {
            lbRightSum.Text = RightSum().ToString("n") + "万元";
        }

        private void tbYSJF_KeyPress(object sender, KeyPressEventArgs e)
        {
            ClassInputRestricter.CheckInput(sender, e);
        }

        private void tbDNYSWJF_KeyPress(object sender, KeyPressEventArgs e)
        {
            ClassInputRestricter.CheckInput(sender, e);
        }

        private void tbLNJFJY_KeyPress(object sender, KeyPressEventArgs e)
        {
            ClassInputRestricter.CheckInput(sender, e);
        }

        private void tbBRZXZJ_KeyPress(object sender, KeyPressEventArgs e)
        {
            ClassInputRestricter.CheckInput(sender, e);
        }

        private void tbBCZXZJ_KeyPress(object sender, KeyPressEventArgs e)
        {
            ClassInputRestricter.CheckInput(sender, e);
        }

        private void tbZCZXZJ_KeyPress(object sender, KeyPressEventArgs e)
        {
            ClassInputRestricter.CheckInput(sender, e);
        }

        private void tbYSJJF_KeyPress(object sender, KeyPressEventArgs e)
        {
            ClassInputRestricter.CheckInput(sender, e);
        }

        private void tbBRWCXJF_KeyPress(object sender, KeyPressEventArgs e)
        {
            ClassInputRestricter.CheckInput(sender, e);
        }

        private void tbBRJSXJF_KeyPress(object sender, KeyPressEventArgs e)
        {
            ClassInputRestricter.CheckInput(sender, e);
        }

        private void tbBRZFZXJF_KeyPress(object sender, KeyPressEventArgs e)
        {
            ClassInputRestricter.CheckInput(sender, e);
        }

        private void tbZYJJ_KeyPress(object sender, KeyPressEventArgs e)
        {
            ClassInputRestricter.CheckInput(sender, e);
        }

        private void tbDTF_KeyPress(object sender, KeyPressEventArgs e)
        {
            ClassInputRestricter.CheckInput(sender, e);
        }

        private void tbLYDTF_KeyPress(object sender, KeyPressEventArgs e)
        {
            ClassInputRestricter.CheckInput(sender, e);
        }

        private void tbZSK_KeyPress(object sender, KeyPressEventArgs e)
        {
            ClassInputRestricter.CheckInput(sender, e);
        }

        private void tbKCWZYE_KeyPress(object sender, KeyPressEventArgs e)
        {
            ClassInputRestricter.CheckInput(sender, e);
        }

        private void tbSWGYJJ_KeyPress(object sender, KeyPressEventArgs e)
        {
            ClassInputRestricter.CheckInput(sender, e);
        }

        private void tbBCWCXJF_KeyPress(object sender, KeyPressEventArgs e)
        {
            ClassInputRestricter.CheckInput(sender, e);
        }

        private void tbBCJSXJF_KeyPress(object sender, KeyPressEventArgs e)
        {
            ClassInputRestricter.CheckInput(sender, e);
        }

        private void tbBCZFZXJF_KeyPress(object sender, KeyPressEventArgs e)
        {
            ClassInputRestricter.CheckInput(sender, e);
        }

        private void tbZFK_KeyPress(object sender, KeyPressEventArgs e)
        {
            ClassInputRestricter.CheckInput(sender, e);
        }

        private void btnFilter_Click(object sender, EventArgs e)
        {
            Frm_select_saving selectSaving = new Frm_select_saving();
            selectSaving.ShowDialog();
            if (selectSaving.DialogResult == DialogResult.OK)
            {
                filterStr = selectSaving.GetSQL();
                GetAllDataRefreshGridView();
            }
        }

        private void FrmAssetsSaving_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.Control && e.KeyCode == Keys.S)
            {
                btnSave_Click(sender, e);
            }
        }

        private void FrmAssetsSaving_FormClosing(object sender, FormClosingEventArgs e)
        {
            if (this.notsaved == true)
            {
                DialogResult dr = MessageBox.Show("信息已经修改,是否确定保存数据？", "系统提示", MessageBoxButtons.OKCancel, MessageBoxIcon.Question);
                if (dr == DialogResult.OK)
                {
                    this.btnSave_Click(sender, e);
                    e.Cancel = true;
                }
            }
        }
    }
}
