using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Data.OleDb;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace zj
{
    public partial class FrmBcardWLK : Form
    {
        int m_id,dwbs;
        string m_dwdm, m_bz;
        double m_brzcjf, m_brzfzxjf, m_brzxzj, m_bczcjf, m_bczfzxjf, m_bczxzj, m_zbwzcbgz;
        private AccessHelper m_accessHelper = new AccessHelper();
        private Boolean notsaved = false; //标识数据是否修改，默认没有修改
        /// <summary>
        /// 条件
        /// </summary>
        public string filterStr = "";

        private string SQL_Admin_Insert = "insert into t_wlkx(dwdm,brzcjf,brzfzxjf,brzxzj,bczcjf,bczfzxjf,bczxzj,zbwzcbgz,bz)values(@DWDM, @BRZCJF, @BRZFZXJF, @BRZXZJ, @BCZCJF, @BCZFZXJF, @BCZXZJ, @ZBWZCBGZ, @BZ)";

        private string SQL_Admin_Update = "update  t_wlkx set dwdm = @DWDM, brzcjf = @BRZCJF, brzfzxjf = @BRZFZXJF, brzxzj = @BRZXZJ, bczcjf = @BCZCJF, bczfzxjf = @BCZFZXJF, bczxzj = @BCZXZJ, zbwzcbgz = @ZBWZCBGZ, bz = @BZ WHERE ID=@ID";
        private string SQL_Admin_Delete = "DELETE FROM t_wlkx WHERE ID=@ID";
        public FrmBcardWLK()
        {
            InitializeComponent();
            Load += new EventHandler(FrmBcardWLK_Load);
        }

        private void btnFilter_Click(object sender, EventArgs e)
        {
            Frm_select_wlkx selectAccount = new Frm_select_wlkx();
            selectAccount.ShowDialog();
            if (selectAccount.DialogResult == DialogResult.OK)
            {
                filterStr = selectAccount.GetSQL();
                GetAllDataRefreshGridView();
            }
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

        private void dataGridView_CellContentClick(object sender, DataGridViewCellEventArgs e)
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

        /// <summary>
        /// 保存前获取数据
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btnSave_Click(object sender, EventArgs e)
        {
            m_brzcjf = double.Parse(tbBRZCJF.Text.Replace(",", ""));
            m_brzfzxjf = double.Parse(tbBRZFZXJF.Text.Replace(",", ""));
            m_brzxzj = double.Parse(tbBRZXZJ.Text.Replace(",", ""));
            m_bczcjf = double.Parse(tbBCZCJF.Text.Replace(",", ""));
            m_bczfzxjf = double.Parse(tbBCZFZXJF.Text.Replace(",", ""));
            m_bczxzj = double.Parse(tbBCZXZJ.Text.Replace(",", ""));
            m_zbwzcbgz = double.Parse(tbZBWZ.Text.Replace(",", ""));

            m_bz = tbBZ.Text;
            m_dwdm = comboDWDM.SelectedValue.ToString();
            if (m_id != ClassConstants.JD_NOTSELECTED)
            {
                OleDbParameter[] parms = new OleDbParameter[] {
                    new OleDbParameter("@DWDM",OleDbType.VarChar),
                    new OleDbParameter("@BRZCJF",OleDbType.Double),
                    new OleDbParameter("@BRZFZXJF",OleDbType.Double),
                    new OleDbParameter("@BRZXZJ",OleDbType.Double),
                    new OleDbParameter("@BCZCJF",OleDbType.Double),
                    new OleDbParameter("@BCZFZXJF",OleDbType.Double),
                    new OleDbParameter("@BCZXZJ",OleDbType.Double),
                    new OleDbParameter("@ZBWZCBGZ",OleDbType.Double),
                    new OleDbParameter("@BZ",OleDbType.VarChar),
                    new OleDbParameter("@ID",OleDbType.Integer) 
                };
                parms[0].Value = m_dwdm;
                parms[1].Value = m_brzcjf;
                parms[2].Value = m_brzfzxjf;
                parms[3].Value = m_brzxzj;
                parms[4].Value = m_bczcjf;
                parms[5].Value = m_bczfzxjf;
                parms[6].Value = m_bczxzj;
                parms[7].Value = m_zbwzcbgz;
                parms[8].Value = m_bz;
                parms[9].Value = m_id;

                UpdateData(parms);
                GetAllDataRefreshGridView();
                MessageBox.Show("更新数据完毕！", "系统提示", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
            ClearControlData();
            ClearVariableData();
            setsaved();
        }


        public void setnotsaved()
        {
            this.notsaved = true;
        }
        /// <summary>
        /// 是否保存标志
        /// </summary>
        public void setsaved()
        {
            this.notsaved = false;
        }

        /// <summary>
        /// 新增
        /// </summary>
        /// <param name="parms"></param>
        public void InsertData(OleDbParameter[] parms)
        {
            m_accessHelper.ExcueteCommand(SQL_Admin_Insert, parms);
        }

        private void btnSave_Click_1(object sender, EventArgs e)
        {
            m_brzcjf = double.Parse(tbBRZCJF.Text.Replace(",", ""));
            m_brzfzxjf = double.Parse(tbBRZFZXJF.Text.Replace(",", ""));
            m_brzxzj = double.Parse(tbBRZXZJ.Text.Replace(",", ""));
            m_bczcjf = double.Parse(tbBCZCJF.Text.Replace(",", ""));
            m_bczfzxjf = double.Parse(tbBCZFZXJF.Text.Replace(",", ""));
            m_bczxzj = double.Parse(tbBCZXZJ.Text.Replace(",", ""));
            m_zbwzcbgz = double.Parse(tbZBWZ.Text.Replace(",", ""));

            m_bz = tbBZ.Text;
            m_dwdm = comboDWDM.SelectedValue.ToString();
            if (m_id != ClassConstants.JD_NOTSELECTED)
            {
                OleDbParameter[] parms = new OleDbParameter[] {
                    new OleDbParameter("@DWDM",OleDbType.VarChar),
                    new OleDbParameter("@BRZCJF",OleDbType.Double),
                    new OleDbParameter("@BRZFZXJF",OleDbType.Double),
                    new OleDbParameter("@BRZXZJ",OleDbType.Double),
                    new OleDbParameter("@BCZCJF",OleDbType.Double),
                    new OleDbParameter("@BCZFZXJF",OleDbType.Double),
                    new OleDbParameter("@BCZXZJ",OleDbType.Double),
                    new OleDbParameter("@ZBWZCBGZ",OleDbType.Double),
                    new OleDbParameter("@BZ",OleDbType.VarChar),
                    new OleDbParameter("@ID",OleDbType.Integer)
                };
                parms[0].Value = m_dwdm;
                parms[1].Value = m_brzcjf;
                parms[2].Value = m_brzfzxjf;
                parms[3].Value = m_brzxzj;
                parms[4].Value = m_bczcjf;
                parms[5].Value = m_bczfzxjf;
                parms[6].Value = m_bczxzj;
                parms[7].Value = m_zbwzcbgz;
                parms[8].Value = m_bz;
                parms[9].Value = m_id;

                UpdateData(parms);
                GetAllDataRefreshGridView();
                MessageBox.Show("更新数据完毕！", "系统提示", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
            else
            {
                OleDbParameter[] parms = new OleDbParameter[] {
                    new OleDbParameter("@DWDM",OleDbType.VarChar),
                    new OleDbParameter("@BRZCJF",OleDbType.Double),
                    new OleDbParameter("@BRZFZXJF",OleDbType.Double),
                    new OleDbParameter("@BRZXZJ",OleDbType.Double),
                    new OleDbParameter("@BCZCJF",OleDbType.Double),
                    new OleDbParameter("@BCZFZXJF",OleDbType.Double),
                    new OleDbParameter("@BCZXZJ",OleDbType.Double),
                    new OleDbParameter("@ZBWZCBGZ",OleDbType.Double),
                    new OleDbParameter("@BZ",OleDbType.VarChar),
                    new OleDbParameter("@ID",OleDbType.Integer)
                };
                parms[0].Value = m_dwdm;
                parms[1].Value = m_brzcjf;
                parms[2].Value = m_brzfzxjf;
                parms[3].Value = m_brzxzj;
                parms[4].Value = m_bczcjf;
                parms[5].Value = m_bczfzxjf;
                parms[6].Value = m_bczxzj;
                parms[7].Value = m_zbwzcbgz;
                parms[8].Value = m_bz;
                parms[9].Value = m_id;

                InsertData(parms);
                GetAllDataRefreshGridView();
                MessageBox.Show("插入数据完毕！", "系统提示", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
            ClearControlData();
            ClearVariableData();
            setsaved();
        }

        private void dataGridView_SelectionChanged(object sender, EventArgs e)
        {
            if (dataGridView.SelectedRows.Count != 0 && dataGridView.SelectedRows[0].Cells[1].Value.ToString() != "")
            {
                //this.checkchagnge();
                ReadDataToVariables();
                LoadVariablesToControls();
            }
        }

      

            /// <summary>
            /// 赋值
            /// </summary>
            public void ReadDataToVariables()
        {
            m_id = int.Parse(dataGridView.SelectedRows[0].Cells[1].Value.ToString());
            DataTable dt = this.GetDataByIndex(m_id);

            m_brzcjf = double.Parse(dt.Rows[0]["brzcjf"].ToString());
            m_brzfzxjf = double.Parse(dt.Rows[0]["brzfzxjf"].ToString());
            m_brzxzj = double.Parse(dt.Rows[0]["brzxzj"].ToString());
            m_bczcjf = double.Parse(dt.Rows[0]["bczcjf"].ToString());
            m_bczfzxjf = double.Parse(dt.Rows[0]["bczfzxjf"].ToString());
            m_bczxzj = double.Parse(dt.Rows[0]["bczxzj"].ToString());
            m_zbwzcbgz = double.Parse(dt.Rows[0]["zbwzcbgz"].ToString());

            m_bz = dt.Rows[0]["bz"].ToString();
            m_dwdm = dt.Rows[0]["dwdm"].ToString();
        }
        public void LoadVariablesToControls()
        {

            comboDWDM.SelectedValue = m_dwdm;
            tbBRZCJF.Text = m_brzcjf.ToString();
            tbBRZFZXJF.Text=m_brzfzxjf.ToString();
            tbBRZXZJ.Text = m_brzxzj.ToString();
            tbBCZCJF.Text = m_bczcjf.ToString();
            tbBCZFZXJF.Text = m_bczfzxjf.ToString();
            tbBCZXZJ.Text = m_bczxzj.ToString();
            tbZBWZ.Text = m_zbwzcbgz.ToString();
            tbBZ.Text = m_bz;
        }
        /// <summary>
        /// 根据id获取一条往来款数据
        /// </summary>
        /// <param name="dataIndex"></param>
        /// <returns></returns>
        public DataTable GetDataByIndex(int dataIndex)
        {
            string tempSQL = "SELECT * FROM t_wlkx WHERE ID=" + dataIndex;
            DataTable dt = m_accessHelper.getDataSet(tempSQL).Tables[0];
            return dt;
        }

        private void btnRemove_Click(object sender, EventArgs e)
        {
            if (m_id != ClassConstants.JD_NOTSELECTED)
            {
                MessageBoxButtons msgBut = MessageBoxButtons.OKCancel;
                DialogResult dr = MessageBox.Show("确定删除此数据项？", "删除数据", msgBut, MessageBoxIcon.Question);
                if (dr == DialogResult.OK)
                {
                    DeleteData(m_id);
                    ClearControlData();
                    ClearVariableData();
                    GetAllDataRefreshGridView();
                }
            }
        }

        /// <summary>
        /// 删除
        /// </summary>
        /// <param name="dataIndex"></param>
        public void DeleteData(int dataIndex)
        {
            OleDbParameter[] parms = new OleDbParameter[] {
                new OleDbParameter("@ID",OleDbType.Integer)
            };
            parms[0].Value = dataIndex;

            m_accessHelper.ExcueteCommand(SQL_Admin_Delete, parms);
        }

        private void button1_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void btnExcel_Click(object sender, EventArgs e)
        {
            DataTable dt = dataGridView.DataSource as DataTable;
            ExcelUI ExcelUI = new ExcelUI();
            ExcelUI.ExportExcelFormat(dt, "");
        }

        /// <summary>
        /// 修改
        /// </summary>
        /// <param name="parms"></param>
        public void UpdateData(OleDbParameter[] parms)
        {
            m_accessHelper.ExcueteCommand(SQL_Admin_Update, parms);
        }
        /// <summary>
        /// 页面加载方法注册
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        void FrmBcardWLK_Load(object sender, EventArgs e)
        {
            LoadComboParams();
            ClearVariableData();
            ClearControlData();
            GetAllDataRefreshGridView();
        }

        /// <summary>
        /// 获取往来款项情况明细信息
        /// </summary>
        public void GetAllDataRefreshGridView()
        {
            string tempSQL = "SELECT t_wlkx.ID as ID号,t_wlkx.dwdm as 单位代码,t_dwxx.dwmc as 单位名称,t_dwxx.dwxz as 单位性质,t_dwxx.dwjb as 单位级别,t_dwxx.dwlx as 单位类型,t_dwxx.szss as 所在省市,t_wlkx.brzcjf as 拨入正常军费,t_wlkx.brzfzxjf as 拨入政府专项经费,t_wlkx.brzxzj as 拨入专项资金,t_wlkx.bczcjf as 拨出正常军费,t_wlkx.bczfzxjf as 拨出政府专项经费,t_wlkx.bczxzj as 拨出专项资金,t_wlkx.zbwzcbgz as 战备物资储备挂账,t_wlkx.bz as 备注 FROM t_dwxx, t_wlkx WHERE t_wlkx.dwdm = t_dwxx.dwdm " + filterStr + "ORDER BY t_wlkx.dwdm,t_wlkx.ID";

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
            dataGridView.Columns["备注"].Width = 320;
            if (dataGridView.SelectedRows.Count != 0)
            {
                m_id = int.Parse(dataGridView.SelectedRows[0].Cells[1].Value.ToString());
            }
            else
            {
                m_id = ClassConstants.JD_NOTSELECTED;
            }
        }

        /// <summary>
        /// 单位赋值
        /// </summary>
        private void LoadComboParams()
        {
            string tempSQL = "SELECT dwdm,dwmc FROM t_dwxx";
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
            comboDWDM.ValueMember = "dwdm";
            comboDWDM.DisplayMember = "dwmc";
        }

        /// <summary>
        /// 清理变量值
        /// </summary>
        private void ClearVariableData()
        {
            m_brzcjf = 0;
            m_brzfzxjf = 0;
            m_brzxzj = 0;
            m_bczcjf = 0;
            m_bczfzxjf = 0;
            m_bczxzj = 0;
            m_zbwzcbgz = 0;

            m_bz = "";
            if (dwbs !=1)
            {
            m_dwdm = comboDWDM.Items[0].ToString();

            }
            m_id = ClassConstants.JD_NOTSELECTED;
        }

        /// <summary>
        /// 清理控件值
        /// </summary>
        private void ClearControlData()
        {
            comboDWDM.Enabled = true;
            if (dwbs != 1)
            {
                comboDWDM.SelectedIndex = 0;

            }
            tbBRZCJF.Text = "0";
            tbBRZFZXJF.Text = "0";
            tbBRZXZJ.Text = "0";
            tbBCZCJF.Text = "0";
            tbBCZFZXJF.Text = "0";
            tbBCZXZJ.Text = "0";
            tbZBWZ.Text = "0";
            tbBZ.Text = "";

        }

        private void label5_Click(object sender, EventArgs e)
        {

        }


    }
}
