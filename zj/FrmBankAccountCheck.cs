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
    public partial class FrmBankAccountCheck : Form
    {
        private AccessHelper m_accessHelper = new AccessHelper();
        private int m_id,dwbs;
        private string m_dwdm;
        private double m_szkszh, m_yqzh, m_ycwczh, m_qtwgzh, m_wgccbs, m_wgccje, m_wgjdbs, m_wgjdje, m_qtyybs, m_qtyyje, m_gfsxbs, m_gfsxje,m_sjfk;

        private string SQL_Admin_Update = "UPDATE t_yhzhqccl SET dwdm=@DWDM,szkszh=@SZKSZH,yqzh=@YQZH,ycwczh=@YCWCZH,qtwgzh=@QTWGZH,wgccbs=@WGCCBS,wgccje=@WGCCJE,wgjdbs=@WGJDBS,wgjdje=@WGJDJE,qtyybs=@QTYYBS,qtyyje=@QTYYJE,sjfk=@SJFK,gfsxbs=@GFSXBS,gfsxje=@GFSXJE WHERE ID=@ID";
        private string SQL_Admin_Insert = "INSERT INTO t_yhzhqccl(dwdm,szkszh,yqzh,ycwczh,qtwgzh,wgccbs,wgccje,wgjdbs,wgjdje,qtyybs,qtyyje,sjfk,gfsxbs,gfsxje) values(@DWDM,@SZKSZH,@YQZH,@YCWCZH,@QTWGZH,@WGCCBS,@WGCCJE,@WGJDBS,@WGJDJE,@QTYYBS,@QTYYJE,@SJFK,@GFSXBS,@GFSXJE)";
        private string SQL_Admin_Delete = "DELETE FROM t_yhzhqccl WHERE ID=@ID";

        public String filterStr = "";

        private Boolean notsaved = false; //标识数据是否修改，默认没有修改

        public void setnotsaved()
        {
            this.notsaved = true;
        }

        public void setsaved()
        {
            this.notsaved = false;
        }
        public FrmBankAccountCheck()
        {
            InitializeComponent();
            Load += new EventHandler(FrmBankAccountCheck_Load);
        }

        private string ReturnDeptCode(string deptName)
        {
            string tempSQL = "SELECT * FROM t_dwxx WHERE dwmc='" + deptName + "'";
            DataSet ds = m_accessHelper.getDataSet(tempSQL);
            return ds.Tables[0].Rows[0]["dwdm"].ToString();
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

        public void GetAllDataRefreshGridView()
        {
            //string tempSQL = "SELECT * FROM t_zjjc ORDER BY ID";
            string tempSQL = "SELECT t_yhzhqccl.ID as ID号,t_yhzhqccl.dwdm as 单位代码,t_dwxx.dwmc as 单位名称,t_dwxx.dwxz as 单位性质,t_dwxx.dwjb as 单位级别,t_dwxx.dwlx as 单位类型,t_dwxx.szss as 所在省市,t_yhzhqccl.szkszh as 擅自开设账户数,t_yhzhqccl.yqzh as 逾期账户数,t_yhzhqccl.ycwczh as 应撤未撤账户数,t_yhzhqccl.qtwgzh as 其他违规账户数,t_yhzhqccl.wgccbs as 违规存储笔数,t_yhzhqccl.wgccje as 违规存储金额,t_yhzhqccl.wgjdbs as 违规借垫笔数,t_yhzhqccl.wgjdje as 违规借垫金额,t_yhzhqccl.qtyybs as 处罚笔数,t_yhzhqccl.qtyyje as 没收利息,t_yhzhqccl.sjfk as 上交罚款,t_yhzhqccl.gfsxbs as 规范手续笔数,t_yhzhqccl.gfsxje as 规范手续金额 FROM t_dwxx,t_yhzhqccl WHERE t_yhzhqccl.dwdm=t_dwxx.dwdm " + filterStr + "ORDER BY t_yhzhqccl.dwdm,t_yhzhqccl.ID";
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
            dataGridView.ClearSelection();
            dataGridView.Columns["ID号"].Width = 60;
            dataGridView.Columns["单位名称"].Width = 200;
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
            string tempSQL = "SELECT * FROM t_yhzhqccl WHERE ID=" + dataIndex;
            DataTable dt = m_accessHelper.getDataSet(tempSQL).Tables[0];
            return dt;
        }

        private void ClearControlData()
        {
            tbGFSXBS.Text = "0";
            tbGFSXJE.Text = "0.00";
            tbQTWGHS.Text = "0";
            tbQTYYBS.Text = "0";
            tbQTYYJE.Text = "0.00";
            tbSZKSZHS.Text = "0";
            tbWGCCBS.Text = "0";
            tbWGCCJE.Text = "0.00";
            tbWGJDBS.Text = "0";
            tbWGJDJE.Text = "0.00";
            tbYCWCZHS.Text = "0";
            tbYQZHS.Text = "0";
            tbSJFK.Text = "0.00";

            comboDWDM.Enabled = true;
            if (dwbs != 1)
            {
                comboDWDM.SelectedIndex = 0;
            }
           
        }

        private void ClearVariableData()
        {
            m_gfsxbs = 0;
            m_gfsxje = 0.00;
            m_qtwgzh = 0;
            m_qtyybs = 0;
            m_qtyyje = 0.00;
            m_szkszh = 0;
            m_wgccbs = 0;
            m_wgccje = 0.00;
            m_wgjdbs = 0;
            m_wgjdje = 0.00;
            m_sjfk = 0.00;
            m_ycwczh = 0;
            m_yqzh = 0;
            if (dwbs != 1)
            {
              m_dwdm = comboDWDM.Items[0].ToString();
            }
           
            m_id = ClassConstants.JD_NOTSELECTED;
        }

        public void ReadDataToVariables()
        {
            m_id = int.Parse(dataGridView.SelectedRows[0].Cells[1].Value.ToString());
            DataTable dt = this.GetDataByIndex(m_id);

            m_dwdm = dt.Rows[0]["dwdm"].ToString();
            m_gfsxbs = double.Parse(dt.Rows[0]["gfsxbs"].ToString());
            m_gfsxje = double.Parse(dt.Rows[0]["gfsxje"].ToString());
            m_qtwgzh = double.Parse(dt.Rows[0]["qtwgzh"].ToString());
            m_qtyybs = double.Parse(dt.Rows[0]["qtyybs"].ToString());
            m_qtyyje = double.Parse(dt.Rows[0]["qtyyje"].ToString());
            m_szkszh = double.Parse(dt.Rows[0]["szkszh"].ToString());
            m_wgccbs = double.Parse(dt.Rows[0]["wgccbs"].ToString());
            m_wgccje = double.Parse(dt.Rows[0]["wgccje"].ToString());
            m_wgjdbs = double.Parse(dt.Rows[0]["wgjdbs"].ToString());
            m_wgjdje = double.Parse(dt.Rows[0]["wgjdje"].ToString());
            m_sjfk = double.Parse(dt.Rows[0]["sjfk"].ToString());
            m_ycwczh = double.Parse(dt.Rows[0]["ycwczh"].ToString());
            m_yqzh = double.Parse(dt.Rows[0]["yqzh"].ToString());
        }

        public void LoadVariablesToControls()
        {
            tbGFSXBS.Text = m_gfsxbs.ToString();
            tbGFSXJE.Text = m_gfsxje.ToString("n");
            tbQTWGHS.Text = m_qtwgzh.ToString();
            tbQTYYBS.Text = m_qtyybs.ToString();
            tbQTYYJE.Text = m_qtyyje.ToString("n");
            tbSZKSZHS.Text = m_szkszh.ToString();
            tbWGCCBS.Text = m_wgccbs.ToString();
            tbWGCCJE.Text = m_wgccje.ToString("n");
            tbWGJDBS.Text = m_wgjdbs.ToString();
            tbWGJDJE.Text = m_wgjdje.ToString("n");
            tbSJFK.Text = m_sjfk.ToString("n");
            tbYCWCZHS.Text = m_ycwczh.ToString();
            tbYQZHS.Text = m_yqzh.ToString();

            comboDWDM.SelectedValue = m_dwdm;
            comboDWDM.Enabled = false;
        }

        void FrmBankAccountCheck_Load(object sender, EventArgs e)
        {
            LoadComboParams();
            ClearVariableData();
            ClearControlData();
            GetAllDataRefreshGridView();
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
            m_gfsxbs = double.Parse(tbGFSXBS.Text);
            m_gfsxje = double.Parse(tbGFSXJE.Text.Replace(",", ""));
            m_qtwgzh = double.Parse(tbQTWGHS.Text);
            m_qtyybs = double.Parse(tbQTYYBS.Text);
            m_qtyyje = double.Parse(tbQTYYJE.Text.Replace(",", ""));
            m_szkszh = double.Parse(tbSZKSZHS.Text);
            m_wgccbs = double.Parse(tbWGCCBS.Text);
            m_wgccje = double.Parse(tbWGCCJE.Text.Replace(",",""));
            m_wgjdbs = double.Parse(tbWGJDBS.Text);
            m_wgjdje = double.Parse(tbWGJDJE.Text.Replace(",", ""));
            m_sjfk = double.Parse(tbSJFK.Text.Replace(",", ""));
            m_ycwczh = double.Parse(tbYCWCZHS.Text);
            m_yqzh = double.Parse(tbYQZHS.Text);

            m_dwdm = comboDWDM.SelectedValue.ToString();
            
            if (m_id != ClassConstants.JD_NOTSELECTED)
            {
                OleDbParameter[] parms = new OleDbParameter[] { 
                    new OleDbParameter("@DWDM",OleDbType.VarChar),
                    new OleDbParameter("@SZKSZH",OleDbType.Integer),
                    new OleDbParameter("@YQZH",OleDbType.Integer),
                    new OleDbParameter("@YCWCZH",OleDbType.Integer),
                    new OleDbParameter("@QTWGZH",OleDbType.Integer),
                    new OleDbParameter("@WGCCBS",OleDbType.Integer),
                    new OleDbParameter("@WGCCJE",OleDbType.Double),
                    new OleDbParameter("@WGJDBS",OleDbType.Integer),
                    new OleDbParameter("@WGJDJE",OleDbType.Double),
                    new OleDbParameter("@QTYYBS",OleDbType.Integer),
                    new OleDbParameter("@QTYYJE",OleDbType.Double),
                    new OleDbParameter("@SJFK",OleDbType.Double),
                    new OleDbParameter("@GFSXBS",OleDbType.Integer),
                    new OleDbParameter("@GFSXJE",OleDbType.Double),
                    new OleDbParameter("@ID",OleDbType.Integer)
                };
                parms[0].Value = m_dwdm;
                parms[1].Value = m_szkszh;
                parms[2].Value = m_yqzh;
                parms[3].Value = m_ycwczh;
                parms[4].Value = m_qtwgzh;
                parms[5].Value = m_wgccbs;
                parms[6].Value = m_wgccje;
                parms[7].Value = m_wgjdbs;
                parms[8].Value = m_wgjdje;
                parms[9].Value = m_qtyybs;
                parms[10].Value = m_qtyyje;
                parms[11].Value = m_sjfk;
                parms[12].Value = m_gfsxbs;
                parms[13].Value = m_gfsxje;

                parms[14].Value = m_id;

                UpdateData(parms);
                GetAllDataRefreshGridView();
                MessageBox.Show("数据更新完毕！", "系统提示",MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
            else
            {
                string tempSql = "SELECT * FROM t_yhzhqccl WHERE dwdm='" + m_dwdm + "'";
                DataSet ds = m_accessHelper.getDataSet(tempSql);
                if (ds.Tables[0].Rows.Count != 0)
                {
                    MessageBox.Show("每个单位只需进行一次银行账户清查处理情况统计！", "系统提示",MessageBoxButtons.OK,MessageBoxIcon.Information);
                    return;
                }
                OleDbParameter[] parms = new OleDbParameter[] { 
                    new OleDbParameter("@DWDM",OleDbType.VarChar),
                    new OleDbParameter("@SZKSZH",OleDbType.Integer),
                    new OleDbParameter("@YQZH",OleDbType.Integer),
                    new OleDbParameter("@YCWCZH",OleDbType.Integer),
                    new OleDbParameter("@QTWGZH",OleDbType.Integer),
                    new OleDbParameter("@WGCCBS",OleDbType.Integer),
                    new OleDbParameter("@WGCCJE",OleDbType.Double),
                    new OleDbParameter("@WGJDBS",OleDbType.Integer),
                    new OleDbParameter("@WGJDJE",OleDbType.Double),
                    new OleDbParameter("@QTYYBS",OleDbType.Integer),
                    new OleDbParameter("@QTYYJE",OleDbType.Double),
                    new OleDbParameter("@SJFK",OleDbType.Double),
                    new OleDbParameter("@GFSXBS",OleDbType.Integer),
                    new OleDbParameter("@GFSXJE",OleDbType.Double)
                };
                parms[0].Value = m_dwdm;
                parms[1].Value = m_szkszh;
                parms[2].Value = m_yqzh;
                parms[3].Value = m_ycwczh;
                parms[4].Value = m_qtwgzh;
                parms[5].Value = m_wgccbs;
                parms[6].Value = m_wgccje;
                parms[7].Value = m_wgjdbs;
                parms[8].Value = m_wgjdje;
                parms[9].Value = m_qtyybs;
                parms[10].Value = m_qtyyje;
                parms[11].Value = m_sjfk;
                parms[12].Value = m_gfsxbs;
                parms[13].Value = m_gfsxje;
                
                InsertData(parms);
                GetAllDataRefreshGridView();
                MessageBox.Show("插入数据完毕！", "系统提示", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
            ClearControlData();
            ClearVariableData();
            setsaved();
        }

        private void btnExit_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void tbWGCCJE_TextChanged(object sender, EventArgs e)
        {
            ClassInputRestricter.CheckChange(sender);
        }

        private void tbWGJDJE_TextChanged(object sender, EventArgs e)
        {
            ClassInputRestricter.CheckChange(sender);
        }

        private void tbQTYYJE_TextChanged(object sender, EventArgs e)
        {
            ClassInputRestricter.CheckChange(sender);
        }

        private void tbGFSXJE_TextChanged(object sender, EventArgs e)
        {
            ClassInputRestricter.CheckChange(sender);
        }

        private void tbGFSXBS_TextChanged(object sender, EventArgs e)
        {
            if (!ClassInputRestricter.IsUnInteger(tbGFSXBS.Text.Trim()))
            {
                tbGFSXBS.Text = tbGFSXBS.Text.Substring(0, tbGFSXBS.Text.Length - 1);
                tbGFSXBS.Select(tbGFSXBS.SelectionStart, tbGFSXBS.Text.Length);
            }
        }

        private void tbQTYYBS_TextChanged(object sender, EventArgs e)
        {
            if (!ClassInputRestricter.IsUnInteger(tbQTYYBS.Text.Trim()))
            {
                tbQTYYBS.Text = tbQTYYBS.Text.Substring(0, tbQTYYBS.Text.Length - 1);
                tbQTYYBS.Select(tbQTYYBS.SelectionStart, tbQTYYBS.Text.Length);
            }
        }

        private void tbWGJDBS_TextChanged(object sender, EventArgs e)
        {
            if (!ClassInputRestricter.IsUnInteger(tbWGJDBS.Text.Trim()))
            {
                tbWGJDBS.Text = tbWGJDBS.Text.Substring(0, tbWGJDBS.Text.Length - 1);
                tbWGJDBS.Select(tbWGJDBS.SelectionStart, tbWGJDBS.Text.Length);
            }
        }

        private void tbWGCCBS_TextChanged(object sender, EventArgs e)
        {
            if (!ClassInputRestricter.IsUnInteger(tbWGCCBS.Text.Trim()))
            {
                tbWGCCBS.Text = tbWGCCBS.Text.Substring(0, tbWGCCBS.Text.Length - 1);
                tbWGCCBS.Select(tbWGCCBS.SelectionStart, tbWGCCBS.Text.Length);
            }
        }

        private void tbSZKSZHS_TextChanged(object sender, EventArgs e)
        {
            if (!ClassInputRestricter.IsUnInteger(tbSZKSZHS.Text.Trim()))
            {
                tbSZKSZHS.Text = tbSZKSZHS.Text.Substring(0, tbSZKSZHS.Text.Length - 1);
                tbSZKSZHS.Select(tbSZKSZHS.SelectionStart, tbSZKSZHS.Text.Length);
            }
        }

        private void tbYCWCZHS_TextChanged(object sender, EventArgs e)
        {
            if (!ClassInputRestricter.IsUnInteger(tbYCWCZHS.Text.Trim()))
            {
                tbYCWCZHS.Text = tbYCWCZHS.Text.Substring(0, tbYCWCZHS.Text.Length - 1);
                tbYCWCZHS.Select(tbYCWCZHS.SelectionStart, tbYCWCZHS.Text.Length);
            }
        }

        private void tbYQZHS_TextChanged(object sender, EventArgs e)
        {
            if (!ClassInputRestricter.IsUnInteger(tbYQZHS.Text.Trim()))
            {
                tbYQZHS.Text = tbYQZHS.Text.Substring(0, tbYQZHS.Text.Length - 1);
                tbYQZHS.Select(tbYQZHS.SelectionStart, tbYQZHS.Text.Length);
            }
        }

        private void tbQTWGHS_TextChanged(object sender, EventArgs e)
        {
            if (!ClassInputRestricter.IsUnInteger(tbQTWGHS.Text.Trim()))
            {
                tbQTWGHS.Text = tbQTWGHS.Text.Substring(0, tbQTWGHS.Text.Length - 1);
                tbQTWGHS.Select(tbQTWGHS.SelectionStart, tbQTWGHS.Text.Length);
            }
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

        private void tbWGCCJE_KeyPress(object sender, KeyPressEventArgs e)
        {
            ClassInputRestricter.CheckInput(sender, e);
        }

        private void tbWGCCJE_Leave(object sender, EventArgs e)
        {
            ClassInputRestricter.CheckFormat(sender);
        }

        private void tbWGJDJE_Leave(object sender, EventArgs e)
        {
            ClassInputRestricter.CheckFormat(sender);
        }

        private void tbQTYYJE_Leave(object sender, EventArgs e)
        {
            ClassInputRestricter.CheckFormat(sender);
        }

        private void tbGFSXJE_Leave(object sender, EventArgs e)
        {
            ClassInputRestricter.CheckFormat(sender);
        }

        private void tbWGJDJE_KeyPress(object sender, KeyPressEventArgs e)
        {
            ClassInputRestricter.CheckInput(sender, e);
        }

        private void tbQTYYJE_KeyPress(object sender, KeyPressEventArgs e)
        {
            ClassInputRestricter.CheckInput(sender, e);
        }

        private void tbGFSXJE_KeyPress(object sender, KeyPressEventArgs e)
        {
            ClassInputRestricter.CheckInput(sender, e);
        }

        private void tbSZKSZHS_KeyPress(object sender, KeyPressEventArgs e)
        {
            ClassInputRestricter.EnterToTab(sender, e);
        }

        private void tbYCWCZHS_KeyPress(object sender, KeyPressEventArgs e)
        {
            ClassInputRestricter.EnterToTab(sender, e);
        }

        private void tbYQZHS_KeyPress(object sender, KeyPressEventArgs e)
        {
            ClassInputRestricter.EnterToTab(sender, e);
        }

        private void tbQTWGHS_KeyPress(object sender, KeyPressEventArgs e)
        {
            ClassInputRestricter.EnterToTab(sender, e);
        }

        private void tbWGCCBS_KeyPress(object sender, KeyPressEventArgs e)
        {
            ClassInputRestricter.EnterToTab(sender, e);
        }

        private void tbWGJDBS_KeyPress(object sender, KeyPressEventArgs e)
        {
            ClassInputRestricter.EnterToTab(sender, e);
        }

        private void tbQTYYBS_KeyPress(object sender, KeyPressEventArgs e)
        {
            ClassInputRestricter.EnterToTab(sender, e);
        }

        private void tbGFSXBS_KeyPress(object sender, KeyPressEventArgs e)
        {
            ClassInputRestricter.EnterToTab(sender, e);
        }

        private void btnFilter_Click(object sender, EventArgs e)
        {
            Frm_select_check selectcheck = new Frm_select_check();
            selectcheck.ShowDialog();
            if (selectcheck.DialogResult == DialogResult.OK)
            {
                filterStr = selectcheck.GetSQL();
                GetAllDataRefreshGridView();
            }
        }

        private void FrmBankAccountCheck_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.Control && e.KeyCode == Keys.S)
            {
                btnSave_Click(sender, e);
            }
        }

        private void FrmBankAccountCheck_FormClosing(object sender, FormClosingEventArgs e)
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

        private void tbSZKSZHS_Leave(object sender, EventArgs e)
        {
            ClassInputRestricter.CheckNull(sender);
        }

        private void tbYQZHS_Leave(object sender, EventArgs e)
        {
            ClassInputRestricter.CheckNull(sender);
        }

        private void tbYCWCZHS_Leave(object sender, EventArgs e)
        {
            ClassInputRestricter.CheckNull(sender);
        }

        private void tbQTWGHS_Leave(object sender, EventArgs e)
        {
            ClassInputRestricter.CheckNull(sender);
        }

        private void tbWGCCBS_Leave(object sender, EventArgs e)
        {
            ClassInputRestricter.CheckNull(sender);
        }

        private void tbWGJDBS_Leave(object sender, EventArgs e)
        {
            ClassInputRestricter.CheckNull(sender);
        }

        private void tbQTYYBS_Leave(object sender, EventArgs e)
        {
            ClassInputRestricter.CheckNull(sender);
        }

        private void tbGFSXBS_Leave(object sender, EventArgs e)
        {
            ClassInputRestricter.CheckNull(sender);
        }

        private void tbSJFK_KeyPress(object sender, KeyPressEventArgs e)
        {
            ClassInputRestricter.EnterToTab(sender, e);
        }

        private void tbSJFK_Leave(object sender, EventArgs e)
        {
            ClassInputRestricter.CheckFormat(sender);
        }

        private void tbSJFK_TextChanged(object sender, EventArgs e)
        {
            ClassInputRestricter.CheckChange(sender);
        }
    }
}
