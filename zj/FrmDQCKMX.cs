using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.Data.OleDb;

namespace zj
{
    public partial class FrmDQCKMX : Form
    {
        private string SQL_Admin_Update = "UPDATE t_dqck SET dwdm=@DWDM,zhmc=@ZHMC,hb=@HB,khh=@KHH,cklb=@CKLB,crrq=@CRRQ,ckqx=@CKQX,je=@JE,spld=@SPLD,kj=@KJ,cn=@CN,sfkhh=@SFKHH,bzyy=@BZYY,bzyymx=@BZYYMX WHERE ID=@ID";
        private string SQL_Admin_Insert = "INSERT INTO t_dqck(dwdm,zhmc,hb,khh,cklb,crrq,ckqx,je,spld,kj,cn,sfkhh,bzyy,bzyymx) values(@DWDM,@ZHMC,@HB,@KHH,@CKLB,@CRRQ,@CKQX,@JE,@SPLD,@KJ,@CN,@SFKHH,@BZYY,@BZYYMX)";
        private string SQL_Admin_Delete = "DELETE FROM t_dqck WHERE ID=@ID";
        public String filterStr = "";
        private AccessHelper m_accessHelper = new AccessHelper();
        string m_dwdm, m_zhmc, m_hb, m_khh, m_cklb, m_crrq, m_ckqx, m_spld, m_kj, m_cn, m_sfkhh,m_bzyy,m_bzyymx;
        int  m_id,dwbs;
        double m_je;
        private Boolean notsaved = false; //标识数据是否修改，默认没有修改

        public void setnotsaved()
        {
            this.notsaved = true;
        }

        public void setsaved()
        {
            this.notsaved = false;
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
            string tempSQL = "SELECT * FROM t_dqck WHERE ID=" + dataIndex;
            DataTable dt = m_accessHelper.getDataSet(tempSQL).Tables[0];
            return dt;
        }

        public FrmDQCKMX()
        {
            InitializeComponent();
            Load += new EventHandler(FrmDQCKMX_Load);
        }

        public void GetAllDataRefreshGridView(){
            string tempSQL = "SELECT t_dqck.ID as ID号,t_dqck.dwdm as 单位代码,t_dwxx.dwmc as 存款单位名称,t_dwxx.dwxz as 单位性质,t_dwxx.dwjb as 单位级别,t_dwxx.dwlx as 单位类型,t_dwxx.szss as 所在省市,t_dqck.sfkhh as 是否存于基本存款账户开户行,t_dqck.zhmc as 账户名称,t_dqck.hb as 行别,t_dqck.khh as 开户行,t_dqck.cklb as 存款类别,t_dqck.crrq as 存入日期,t_dqck.ckqx as 存款期限,t_dqck.je as 金额,t_dqck.spld as 审批领导,t_dqck.kj as 会计,t_dqck.cn as 出纳,t_dqck.bzyy as 在其他行存储原因,t_dqck.bzyymx as 备注 FROM t_dwxx,t_dqck where t_dqck.dwdm=t_dwxx.dwdm " + filterStr + "ORDER BY t_dqck.ID";
            DataSet ds = m_accessHelper.getDataSet(tempSQL);
            DataTable dt = ds.Tables[0];
            
            DataColumn dc = dt.Columns.Add("序号", typeof(int));
            dt.Columns["序号"].SetOrdinal(0);
            for (int i = 0; i < dt.Rows.Count; i++)
            {
                dt.Rows[i][0] = i + 1;
            }
            
            dataGridView.DataSource = dt;
            dataGridView.Columns[1].Visible = false;

            dataGridView.Columns["ID号"].Width = 60;
            dataGridView.Columns["存款单位名称"].Width = 200;
            dataGridView.ClearSelection();
            if (dataGridView.SelectedRows.Count != 0)
            {
                m_id = int.Parse(dataGridView.SelectedRows[0].Cells[1].Value.ToString());
            }
            else
            {
                m_id = ClassConstants.JD_NOTSELECTED;
            }
        }

        void FrmDQCKMX_Load(object sender, EventArgs e)
        {
            LoadComboParams();
            ClearVariableData();
            ClearControlData();
            GetAllDataRefreshGridView();


            DateTime minDate = new DateTime(1950,1,1);
            DateTime maxDate = new DateTime(2019,6,30);
            dtpCRRQ.MinDate = minDate;
            dtpCRRQ.MaxDate = maxDate;

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
                    //setControl();
                    ClearVariableData();
                    dataGridView.ClearSelection();
                    setnotsaved();
                }
            }
            else
            {
                ClearControlData();
                //setControl();
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
                DialogResult dr = MessageBox.Show("确定删除此数据项？", "删除数据", msgBut);
                if (dr == DialogResult.OK)
                {
                    DeleteData(m_id);
                    ClearControlData();
                    ClearVariableData();
                    GetAllDataRefreshGridView();
                }
            }
        }

        private bool CheckInputEmpty()
        {
            bool result = true;
            if (comboHB.SelectedIndex < 0)
            {
                MessageBox.Show("行别不能为空！", "系统提示", MessageBoxButtons.OK, MessageBoxIcon.Error);
                comboHB.Focus();
                result = false;
            }else if (tbZHMC.Text.Length == 0)
            {
                MessageBox.Show("账户名称不能为空！", "系统提示", MessageBoxButtons.OK, MessageBoxIcon.Error);
                tbZHMC.Focus();
                result = false;
            }
            else if (tbKHH.Text.Length == 0)
            {
                MessageBox.Show("开户行不能为空！", "系统提示", MessageBoxButtons.OK, MessageBoxIcon.Error);
                tbKHH.Focus();
                result = false;
            }
            else if (comboCKLB.SelectedIndex < 0)
            {
                MessageBox.Show("存款类别不能为空！", "系统提示", MessageBoxButtons.OK, MessageBoxIcon.Error);
                comboCKLB.Focus();
                result = false;
            }
            else if (comboCKQX.SelectedIndex < 0)
            {
                MessageBox.Show("存款期限不能为空！", "系统提示", MessageBoxButtons.OK, MessageBoxIcon.Error);
                comboCKQX.Focus();
                result = false;
            }
            else if (comboSFKHH.SelectedIndex < 0)
            {
                MessageBox.Show("是否在开户行存储不能为空！", "系统提示", MessageBoxButtons.OK, MessageBoxIcon.Error);
                comboSFKHH.Focus();
                result = false;
            }
            else if (tbSPLD.Text.Length == 0)
            {
                MessageBox.Show("审批领导不能为空！", "系统提示", MessageBoxButtons.OK, MessageBoxIcon.Error);
                tbSPLD.Focus();
                result = false;
            }
            else if (tbKJ.Text.Length == 0)
            {
                MessageBox.Show("会计不能为空！", "系统提示", MessageBoxButtons.OK, MessageBoxIcon.Error);
                tbKJ.Focus();
                result = false;
            }
            else if (tbCN.Text.Length == 0)
            {
                MessageBox.Show("出纳不能为空！", "系统提示", MessageBoxButtons.OK, MessageBoxIcon.Error);
                tbCN.Focus();
                result = false;
            }
            else if ((comboSFKHH.Text == "否") && (comboBZYY.SelectedIndex == -1))
            {
                MessageBox.Show("在非开户行存储请选择原因！", "系统提示", MessageBoxButtons.OK, MessageBoxIcon.Error);
                comboBZYY.Focus();
                result = false;
            }
            else if (comboBZYY.SelectedIndex ==4 && textBoxBZYYMX.Text=="")
            {
                MessageBox.Show("请说明在非开户行存储的详细原因！", "系统提示", MessageBoxButtons.OK, MessageBoxIcon.Error);
                textBoxBZYYMX.Focus();
                result = false;
            }
            if (comboBZYY.SelectedIndex == 0 && comboHB.SelectedIndex > 6)
            {
                MessageBox.Show("行别与在其他行存储原因填写不一致！", "系统提示", MessageBoxButtons.OK, MessageBoxIcon.Error);
                result = false;
            }
            if (comboBZYY.SelectedIndex == 1 && comboHB.SelectedIndex > 4)
            {
                MessageBox.Show("行别与在其他行存储原因填写不一致！", "系统提示", MessageBoxButtons.OK, MessageBoxIcon.Error);
                result = false;
            }
            return result;
        }

        private bool CheckInputValidation()
        {
            if (comboCKLB.SelectedIndex == 0)
            {
                if (comboCKQX.SelectedIndex == 4)
                {
                    MessageBox.Show("选择定期存款后,存款期限只能选择:三个月、半年、一年、一年以上！", "系统提示", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    return false;

                }
            }
            else if (comboCKLB.SelectedIndex == 1)
            {
                if (comboCKQX.SelectedIndex != 4)
                {
                    MessageBox.Show("选择通知存款后,存款期限只能选择:通知存款无期限！", "系统提示", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    return false;
                }
            }
            if (dtpCRRQ.Value.ToShortDateString() == new DateTime(2019, 6, 30).ToShortDateString())
            {
                MessageBoxButtons msgBut = MessageBoxButtons.OKCancel;
                DialogResult dr = MessageBox.Show("存入日期为系统默认日期2018-7-1,是否确定保存数据？", "系统提示", msgBut, MessageBoxIcon.Question);
                if (dr == DialogResult.Cancel)
                {
                    return false;
                }
             }
            return true;
        }

        private void btnSave_Click(object sender, EventArgs e)
        {
            m_dwdm = comboDWDM.SelectedValue.ToString();
            m_zhmc = tbZHMC.Text;
            m_hb = comboHB.Text;
            m_khh = tbKHH.Text;
            m_cklb = comboCKLB.Text;
            m_crrq = dtpCRRQ.Value.ToShortDateString();
            m_ckqx = comboCKQX.Text;

            m_je = double.Parse(tbJE.Text.Replace(",",""));
            m_spld = tbSPLD.Text;
            m_kj = tbKJ.Text;
            m_cn = tbCN.Text;
            m_sfkhh = comboSFKHH.Text;
            m_bzyy = comboBZYY.Text;
            m_bzyymx = textBoxBZYYMX.Text;


            if (!CheckInputEmpty())
            {
                return;
            }
            if (!CheckInputValidation())
            {
                return;
            }
            
            //如果在开户行存储，判断是否与账户开户行一致
            if (m_sfkhh == "是")
            {
                String temp_sql = "select t_yhzh.dwdm from t_yhzh where t_yhzh.dwdm='" + m_dwdm + "' and t_yhzh.zhmc='" + m_zhmc + "' and t_yhzh.khh='" + m_khh + "' and t_yhzh.hb='" + m_hb + "'";
                AccessHelper AccessHelper = new AccessHelper();
                DataSet ds1 = AccessHelper.getDataSet(temp_sql);
                if (ds1.Tables[0].Rows.Count < 1)
                {
                    MessageBox.Show("没有在银行账户信息中查找到一致的开户行信息！", "系统提示", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    return;
                }
            }


            if (m_id != ClassConstants.JD_NOTSELECTED)
            {
                OleDbParameter[] parms = new OleDbParameter[] { 
                    new OleDbParameter("@DWDM",OleDbType.VarChar),
                    new OleDbParameter("@ZHMC",OleDbType.VarChar),
                    new OleDbParameter("@HB",OleDbType.VarChar),
                    new OleDbParameter("@KHH",OleDbType.VarChar),
                    new OleDbParameter("@CKLB",OleDbType.VarChar),
                    new OleDbParameter("@CRRQ",OleDbType.VarChar),
                    new OleDbParameter("@CKQX",OleDbType.VarChar),
                    new OleDbParameter("@JE",OleDbType.Double),
                    new OleDbParameter("@SPLD",OleDbType.VarChar),
                    new OleDbParameter("@KJ",OleDbType.VarChar),
                    new OleDbParameter("@CN",OleDbType.VarChar),
                    new OleDbParameter("@SFKHH",OleDbType.VarChar),
                    new OleDbParameter("@BZYY",OleDbType.VarChar),
                    new OleDbParameter("@BZYYMX",OleDbType.VarChar),
                    new OleDbParameter("@ID",OleDbType.Integer)

                };
                parms[0].Value = m_dwdm;
                parms[1].Value = m_zhmc;
                parms[2].Value = m_hb;
                parms[3].Value = m_khh;
                parms[4].Value = m_cklb;
                parms[5].Value = m_crrq;
                parms[6].Value = m_ckqx;
                parms[7].Value = m_je;
                parms[8].Value = m_spld;
                parms[9].Value = m_kj;
                parms[10].Value = m_cn;
                parms[11].Value = m_sfkhh;
                parms[12].Value = m_bzyy;
                parms[13].Value = m_bzyymx;
                parms[14].Value = m_id;


                UpdateData(parms);
                GetAllDataRefreshGridView();
                MessageBox.Show("更新数据完毕！","系统提示",MessageBoxButtons.OK,MessageBoxIcon.Information);
            }
            else
            {
                OleDbParameter[] parms = new OleDbParameter[] { 
                    new OleDbParameter("@DWDM",OleDbType.VarChar),
                    new OleDbParameter("@ZHMC",OleDbType.VarChar),
                    new OleDbParameter("@HB",OleDbType.VarChar),
                    new OleDbParameter("@KHH",OleDbType.VarChar),
                    new OleDbParameter("@CKLB",OleDbType.VarChar),
                    new OleDbParameter("@CRRQ",OleDbType.VarChar),
                    new OleDbParameter("@CKQX",OleDbType.VarChar),
                    new OleDbParameter("@JE",OleDbType.Double),
                    new OleDbParameter("@SPLD",OleDbType.VarChar),
                    new OleDbParameter("@KJ",OleDbType.VarChar),
                    new OleDbParameter("@CN",OleDbType.VarChar),
                    new OleDbParameter("@SFKHH",OleDbType.VarChar),
                    new OleDbParameter("@BZYY",OleDbType.VarChar),
                    new OleDbParameter("@BZYYMX",OleDbType.VarChar)
                };
                parms[0].Value = m_dwdm;
                parms[1].Value = m_zhmc;
                parms[2].Value = m_hb;
                parms[3].Value = m_khh;
                parms[4].Value = m_cklb;
                parms[5].Value = m_crrq;
                parms[6].Value = m_ckqx;
                parms[7].Value = m_je;
                parms[8].Value = m_spld;
                parms[9].Value = m_kj;
                parms[10].Value = m_cn;
                parms[11].Value = m_sfkhh;
                parms[12].Value = m_bzyy;
                parms[13].Value = m_bzyymx;

                InsertData(parms);
                GetAllDataRefreshGridView();
                MessageBox.Show("插入数据完毕！", "系统提示", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
            ClearControlData();
            //setControl();
            ClearVariableData();
            setsaved();
        }

        private void btnExit_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        public void ReadDataToVariables()
        {
            //m_id = int.Parse(dataGridView.SelectedRows[0].Cells[0].Value.ToString());
            m_id = int.Parse(dataGridView.SelectedRows[0].Cells[1].Value.ToString());
            DataTable dt = this.GetDataByIndex(m_id);
            m_hb = dt.Rows[0]["hb"].ToString();
            m_je = double.Parse(dt.Rows[0]["je"].ToString());
            m_cklb = dt.Rows[0]["cklb"].ToString();
            m_ckqx = dt.Rows[0]["ckqx"].ToString();
            m_cn = dt.Rows[0]["cn"].ToString();
            m_crrq = dt.Rows[0]["crrq"].ToString();
            m_dwdm = dt.Rows[0]["dwdm"].ToString();
            m_khh = dt.Rows[0]["khh"].ToString();
            m_kj = dt.Rows[0]["kj"].ToString();
            m_spld = dt.Rows[0]["spld"].ToString();
            m_zhmc = dt.Rows[0]["zhmc"].ToString();
            m_sfkhh = dt.Rows[0]["sfkhh"].ToString();
            m_bzyy = dt.Rows[0]["bzyy"].ToString();
            m_bzyymx = dt.Rows[0]["bzyymx"].ToString();
        }

        public void LoadVariablesToControls()
        {
            tbCN.Text = m_cn;

            tbJE.Text = m_je.ToString("n");
            tbKHH.Text = m_khh;
            tbKJ.Text = m_kj;
            tbSPLD.Text = m_spld;
            tbZHMC.Text = m_zhmc;
            textBoxBZYYMX.Text = m_bzyymx;

            comboHB.SelectedIndex = comboHB.Items.IndexOf(m_hb);
            comboCKLB.SelectedIndex = comboCKLB.Items.IndexOf(m_cklb);
            comboCKQX.SelectedIndex = comboCKQX.Items.IndexOf(m_ckqx);
            comboSFKHH.SelectedIndex = comboSFKHH.Items.IndexOf(m_sfkhh);
            comboBZYY.SelectedIndex = comboBZYY.Items.IndexOf(m_bzyy);

            string str = m_crrq;
            LoadDateToDateTimePicker(m_crrq, dtpCRRQ);

            comboDWDM.SelectedValue = m_dwdm;            
        }

        private void LoadDateToDateTimePicker(string date, DateTimePicker destPicker)
        {
            string str = date;
            string[] result = str.Split('-');
            if (result.Count() != ClassConstants.JD_DATETIMELEN)
            {
                destPicker.Text = "";
            }
            else
            {
                string strYear = result[0];
                string strMonth = result[1];
                string strDay = result[2];

                int iYear = int.Parse(strYear);
                int iMonth = int.Parse(strMonth);
                int iDay = int.Parse(strDay);
                destPicker.Value = new System.DateTime(iYear, iMonth, iDay);
                destPicker.Refresh();
            }
        }

        private void ClearControlData()
        {
            tbCN.Text = "";
            tbJE.Text = "0.00";
            tbKHH.Text = "";
            tbKJ.Text = "";
            tbSPLD.Text = "";
            tbZHMC.Text = "";
            DateTime maxDate = new DateTime(2014, 3, 31);
            dtpCRRQ.Value = maxDate;
            if (dwbs != 1)
            {
                comboDWDM.SelectedIndex = 0;
            }
            comboCKLB.SelectedIndex = -1;
            comboCKQX.SelectedIndex = -1;
            comboHB.SelectedIndex = -1;
            comboSFKHH.SelectedIndex = -1;
            comboBZYY.SelectedIndex = -1;

        }

        private void ClearVariableData()
        {
            m_zhmc = "";
            m_hb = "";
            m_khh = "";
            DateTime maxDate = new DateTime(2019, 6, 30);
            m_crrq = maxDate.ToString();
            m_je = 0;
            m_spld = "";
            m_kj = "";
            m_cn = "";
            m_bzyy = "";
            m_bzyymx = "";

            m_ckqx = comboCKQX.Items[0].ToString();
            m_cklb = comboCKLB.Items[0].ToString();
            if (dwbs != 1)
            {
                m_dwdm = comboDWDM.Items[0].ToString();
            }
            m_sfkhh = comboSFKHH.Items[0].ToString();
            m_id = ClassConstants.JD_NOTSELECTED;
        }

        private void LoadComboParams()
        {
            string tempSQL = "SELECT dwmc,dwdm FROM t_dwxx";
            DataSet ds = m_accessHelper.getDataSet(tempSQL);
            comboDWDM.DataSource = ds.Tables[0];
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

        private string ReturnDeptCode(string deptName)
        {
            string tempSQL = "SELECT * FROM t_dwxx WHERE dwmc='" + deptName + "'";
            DataSet ds = m_accessHelper.getDataSet(tempSQL);
            return ds.Tables[0].Rows[0]["dwdm"].ToString();
        }

        private void tbJE_TextChanged(object sender, EventArgs e)
        {
            ClassInputRestricter.CheckChange(sender);
            //this.setnotsaved();
        }

        private void btnExport_Click(object sender, EventArgs e)
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

        private void tbJE_KeyPress(object sender, KeyPressEventArgs e)
        {
            ClassInputRestricter.CheckInput(sender, e);
        }

        private void comboDWDM_KeyPress(object sender, KeyPressEventArgs e)
        {
            ClassInputRestricter.EnterToTab(sender, e);
        }

        private void tbZHMC_KeyPress(object sender, KeyPressEventArgs e)
        {
            ClassInputRestricter.EnterToTab(sender, e);
        }

        private void comboHB_KeyPress(object sender, KeyPressEventArgs e)
        {
            ClassInputRestricter.EnterToTab(sender, e);
        }

        private void tbKHH_KeyPress(object sender, KeyPressEventArgs e)
        {
            ClassInputRestricter.EnterToTab(sender, e);
        }

        private void comboCKLB_KeyPress(object sender, KeyPressEventArgs e)
        {
            ClassInputRestricter.EnterToTab(sender, e);
        }

        private void tbSPLD_KeyPress(object sender, KeyPressEventArgs e)
        {
            ClassInputRestricter.EnterToTab(sender, e);
        }

        private void comboSFKHH_KeyPress(object sender, KeyPressEventArgs e)
        {
            ClassInputRestricter.EnterToTab(sender, e);
        }

        private void comboCKQX_KeyPress(object sender, KeyPressEventArgs e)
        {
            ClassInputRestricter.EnterToTab(sender, e);
        }

        private void tbKJ_KeyPress(object sender, KeyPressEventArgs e)
        {
            ClassInputRestricter.EnterToTab(sender, e);
        }

        private void tbJE_Leave(object sender, EventArgs e)
        {
            ClassInputRestricter.CheckFormat(sender);
        }

        private void btnFilter_Click(object sender, EventArgs e)
        {
            Frm_select_deposite selectAccount = new Frm_select_deposite();
            selectAccount.ShowDialog();
            if (selectAccount.DialogResult == DialogResult.OK)
            {
                filterStr = selectAccount.GetSQL();
                GetAllDataRefreshGridView();
            }
        }

        private void FrmDQCKMX_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.Control && e.KeyCode == Keys.S)
            {
                btnSave_Click(sender, e);
            }
        }

        private void FrmDQCKMX_FormClosing(object sender, FormClosingEventArgs e)
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

        private void dtpCRRQ_KeyPress(object sender, KeyPressEventArgs e)
        {
            ClassInputRestricter.EnterToTab(sender, e);
        }

        private void tbCN_KeyPress(object sender, KeyPressEventArgs e)
        {
            ClassInputRestricter.EnterToTab(sender, e);
        }

        private void tbZHMC_TextChanged(object sender, EventArgs e)
        {
            //this.setnotsaved();
        }

        private void tbSPLD_TextChanged(object sender, EventArgs e)
        {
            //this.setnotsaved();
        }

        private void tbKHH_TextChanged(object sender, EventArgs e)
        {
            //this.setnotsaved();
        }

        private void tbKJ_TextChanged(object sender, EventArgs e)
        {
            //this.setnotsaved();
        }

        private void tbCN_TextChanged(object sender, EventArgs e)
        {
            //this.setnotsaved();
        }
        public void setControl()
        {
            tbZHMC.Text = m_zhmc;
            tbKHH.Text = m_khh;
            tbSPLD.Text = m_spld;
            tbKJ.Text = m_kj;
            tbCN.Text = m_cn;
        }

        private void FrmDQCKMX_Load_1(object sender, EventArgs e)
        {

        }

        private void label4_Click(object sender, EventArgs e)
        {

        }

        private void comboSFKHH_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (comboSFKHH.Text == "否")
            {
                comboBZYY.Enabled = true;
            }
            else
            {
                comboBZYY.Enabled = false;
                comboBZYY.SelectedIndex = -1;
                textBoxBZYYMX.Text = "";
            }
        }

        private void comboBZYY_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (comboBZYY.Text=="其他原因")
            {
                textBoxBZYYMX.Enabled = true;
            }
            else
            {
                textBoxBZYYMX.Enabled = false;
                textBoxBZYYMX.Text = "";
            }
        }

        private void comboBZYY_KeyPress(object sender, KeyPressEventArgs e)
        {
            ClassInputRestricter.EnterToTab(sender, e);
        }

        private void textBoxBZYYMX_KeyPress(object sender, KeyPressEventArgs e)
        {
            ClassInputRestricter.EnterToTab(sender, e);
        }


    }

}
