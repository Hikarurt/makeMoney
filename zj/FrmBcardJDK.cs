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
    public partial class FrmBcardJDK : Form
    {
        int m_id,dwbs;
        public string m_sql;
        string m_dwdm, m_kmbh,m_kmmc,m_xmzy,m_pzld,m_bz;
        double m_je;
        DateTime spsj, qx;
        private AccessHelper m_accessHelper = new AccessHelper();
        private Boolean notsaved = false; //标识数据是否修改，默认没有修改
        /// <summary>
        /// 条件
        /// </summary>
        public string filterStr = "";
        private string SQL_Admin_Insert = "insert into t_jdk(dwdm,kmbh,kmmc,xmzy,je,spsj,qx,pzld,bz)values(@DWDM, @KMBH, @KMMC, @XMZY, @JE, @SPSJ, @QX, @PZLD, @BZ)";

        private string SQL_Admin_Update = "UPDATE  t_jdk SET dwdm=@DWDM,kmbh=@KMBH,kmmc=@KMMC,xmzy=@XMZY,je=@JE,spsj=@SPSJ,qx=@QX,pzld=@PZLD,bz= @BZ WHERE ID=@ID";
        private string SQL_Admin_Delete = "DELETE FROM t_jdk WHERE ID=@ID";


        public FrmBcardJDK()
        {
            InitializeComponent();
            Load += new EventHandler(FrmBcardJDK_Load);
        }

        /// <summary>
        /// 增加
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
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

        /// <summary>
        /// 保存前获取数据
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btnSave_Click(object sender, EventArgs e)
        {
            m_je = double.Parse(tbJE.Text.Replace(",", ""));
            spsj = Convert.ToDateTime(dtSPSJ.Text.Replace(",", ""));
            qx = Convert.ToDateTime(dtQX.Text.Replace(",", ""));
            m_bz =tbBZ.Text.Replace(",", "");
            m_kmbh = tbKMBH.Text.Replace(",", "");
            m_kmmc = tbKMMC.Text.Replace(",", "");
            m_xmzy = tbXMZY.Text.Replace(",", "");
            m_pzld = tbPZLD.Text.Replace(",", "");
            m_dwdm = comboDWDM.SelectedValue.ToString();
          
            if (m_id != ClassConstants.JD_NOTSELECTED)
            {
                OleDbParameter[] parms = new OleDbParameter[] {
                    new OleDbParameter("@DWDM",OleDbType.VarChar),
                    new OleDbParameter("@KMBH",OleDbType.VarChar),
                    new OleDbParameter("@KMMC",OleDbType.VarChar),
                    new OleDbParameter("@XMZY",OleDbType.VarChar),
                    new OleDbParameter("@JE",OleDbType.Double),
                    new OleDbParameter("@SPSJ",OleDbType.VarChar),
                    new OleDbParameter("@QX",OleDbType.VarChar),
                    new OleDbParameter("@PZLD",OleDbType.VarChar),
                    new OleDbParameter("@BZ",OleDbType.VarChar),
                    new OleDbParameter("@ID",OleDbType.Integer)
                };
                parms[0].Value = m_dwdm;
                parms[1].Value = m_kmbh;
                parms[2].Value = m_kmmc;
                parms[3].Value = m_xmzy;
                parms[4].Value = m_je;
                parms[5].Value = spsj;
                parms[6].Value = qx;
                parms[7].Value = m_pzld;
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
        /// 修改
        /// </summary>
        /// <param name="parms"></param>
        public void UpdateData(OleDbParameter[] parms)
        {
            m_accessHelper.ExcueteCommand(SQL_Admin_Update, parms);
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

        /// <summary>
        /// 保存
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btnSave_Click_1(object sender, EventArgs e)
        {
            m_je = double.Parse(tbJE.Text.Replace(",", ""));
            spsj = Convert.ToDateTime(dtSPSJ.Text.Replace(",", ""));
            qx = Convert.ToDateTime(dtQX.Text.Replace(",", ""));
            m_bz = tbBZ.Text.Replace(",", "");
            m_kmbh = tbKMBH.Text.Replace(",", "");
            m_kmmc = tbKMMC.Text.Replace(",", "");
            m_xmzy = tbXMZY.Text.Replace(",", "");
            m_pzld = tbPZLD.Text.Replace(",", "");
            m_dwdm = comboDWDM.SelectedValue.ToString();

            if (m_id != ClassConstants.JD_NOTSELECTED)
            {
                OleDbParameter[] parms = new OleDbParameter[] {
                    new OleDbParameter("@DWDM",OleDbType.VarChar),
                    new OleDbParameter("@KMBH",OleDbType.VarChar),
                    new OleDbParameter("@KMMC",OleDbType.VarChar),
                    new OleDbParameter("@XMZY",OleDbType.VarChar),
                    new OleDbParameter("@JE",OleDbType.Double),
                    new OleDbParameter("@SPSJ",OleDbType.VarChar),
                    new OleDbParameter("@QX",OleDbType.VarChar),
                    new OleDbParameter("@PZLD",OleDbType.VarChar),
                    new OleDbParameter("@BZ",OleDbType.VarChar),
                    new OleDbParameter("@ID",OleDbType.Integer)
                };
                parms[0].Value = m_dwdm;
                parms[1].Value = m_kmbh;
                parms[2].Value = m_kmmc;
                parms[3].Value = m_xmzy;
                parms[4].Value = m_je;
                parms[5].Value = spsj;
                parms[6].Value = qx;
                parms[7].Value = m_pzld;
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
                    new OleDbParameter("@KMBH",OleDbType.VarChar),
                    new OleDbParameter("@KMMC",OleDbType.VarChar),
                    new OleDbParameter("@XMZY",OleDbType.VarChar),
                    new OleDbParameter("@JE",OleDbType.Double),
                    new OleDbParameter("@SPSJ",OleDbType.VarChar),
                    new OleDbParameter("@QX",OleDbType.VarChar),
                    new OleDbParameter("@PZLD",OleDbType.VarChar),
                    new OleDbParameter("@BZ",OleDbType.VarChar),
                    new OleDbParameter("@ID",OleDbType.Integer)
                };
                parms[0].Value = m_dwdm;
                parms[1].Value = m_kmbh;
                parms[2].Value = m_kmmc;
                parms[3].Value = m_xmzy;
                parms[4].Value = m_je;
                parms[5].Value = spsj;
                parms[6].Value = qx;
                parms[7].Value = m_pzld;
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

        /// <summary>
        /// 新增
        /// </summary>
        /// <param name="parms"></param>
        public void InsertData(OleDbParameter[] parms)
        {
            m_accessHelper.ExcueteCommand(SQL_Admin_Insert, parms);
        }

        private void btnExcel_Click(object sender, EventArgs e)
        {
            DataTable dt = dataGridView.DataSource as DataTable;
            ExcelUI ExcelUI = new ExcelUI();
            ExcelUI.ExportExcelFormat(dt, "");
        }

        private void button1_Click(object sender, EventArgs e)
        {
            this.Close();
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

        public void LoadVariablesToControls()
        {
            comboDWDM.SelectedValue = m_dwdm;
            tbJE.Text= m_je.ToString();
            dtSPSJ.Text= spsj.ToString();
            dtQX.Text = qx.ToString() ;
            tbBZ.Text= m_bz;
            tbKMBH.Text= m_kmbh;
            tbKMMC.Text= m_kmmc;
            tbXMZY.Text= m_xmzy;
            tbPZLD.Text= m_pzld;
        }

        /// <summary>
        /// 赋值
        /// </summary>
        public void ReadDataToVariables()
        {
            m_id = int.Parse(dataGridView.SelectedRows[0].Cells[1].Value.ToString());
            DataTable dt = this.GetDataByIndex(m_id);

            m_je = double.Parse(dt.Rows[0]["je"].ToString());
            spsj = Convert.ToDateTime(dt.Rows[0]["spsj"].ToString());
            qx = Convert.ToDateTime(dt.Rows[0]["qx"].ToString());
            m_bz = dt.Rows[0]["bz"].ToString();
            m_kmmc = dt.Rows[0]["kmmc"].ToString();
            m_xmzy = dt.Rows[0]["xmzy"].ToString();
            m_kmbh = dt.Rows[0]["kmbh"].ToString();

            m_pzld = dt.Rows[0]["pzld"].ToString();
            m_dwdm = dt.Rows[0]["dwdm"].ToString();
        }

        /// <summary>
        /// 根据id获取一条往来款数据
        /// </summary>
        /// <param name="dataIndex"></param>
        /// <returns></returns>
        public DataTable GetDataByIndex(int dataIndex)
        {
            string tempSQL = "SELECT * FROM t_jdk WHERE ID=" + dataIndex;
            DataTable dt = m_accessHelper.getDataSet(tempSQL).Tables[0];
            return dt;
        }

        /// <summary>
        /// 页面加载方法
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void FrmBcardJDK_Load(object sender, EventArgs e)
        {
            LoadComboParams();
            ClearVariableData();
            ClearControlData();
            GetAllDataRefreshGridView();
        }

        /// <summary>
        /// 获取借垫款明细信息
        /// </summary>
        public void GetAllDataRefreshGridView()
        {

            string tempSQL = "SELECT t_jdk.ID as ID号,t_jdk.dwdm as 单位代码,t_dwxx.dwmc as 单位名称,t_dwxx.dwxz as 单位性质,t_dwxx.dwjb as 单位级别,t_dwxx.dwlx as 单位类型,t_dwxx.szss as 所在省市,t_jdk.kmmc as 核算科目,t_jdk.xmzy as 项目摘要,t_jdk.je as 金额 ,t_jdk.spsj as 审批时间,t_jdk.qx as 期限,t_jdk.pzld as 批准领导,t_jdk.bz as 备注 FROM t_dwxx, t_jdk WHERE t_jdk.dwdm = t_dwxx.dwdm  " + filterStr + "ORDER BY t_jdk.dwdm,t_jdk.ID";

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
            /// 清理控件值
            /// </summary>
            private void ClearControlData()
        {
            comboDWDM.Enabled = true;
            if (dwbs != 1)
            {
            comboDWDM.SelectedIndex = 0;
            }

            tbKMBH.Text = "0";
            tbKMMC.Text = "0";
            tbXMZY.Text = "0";
            tbJE.Text = "0";
            dtSPSJ.Text = new DateTime(2019, 6, 30).ToString();
            dtQX.Text = new DateTime(2019, 6, 30).ToString() ;
            tbPZLD.Text = "0";
            tbBZ.Text = "";

        }

        /// <summary>
        /// 清理变量值
        /// </summary>
        private void ClearVariableData()
        {
            m_je = 0;
            spsj = new DateTime(2019, 6, 30);
            qx = new DateTime(2019, 6, 30);
            m_bz = "";
            m_kmbh = "";
            m_kmmc = "";
            m_xmzy = "";
            m_pzld = "";
            if (dwbs != 1)
            {
            m_dwdm=comboDWDM.Items[0].ToString();
            }

            m_id = ClassConstants.JD_NOTSELECTED;
        }

        private void btnFilter_Click(object sender, EventArgs e)
        {
            Form_select_jdk selectAccount = new Form_select_jdk();
            selectAccount.ShowDialog();
            if (selectAccount.DialogResult == DialogResult.OK)
            {
                filterStr = selectAccount.GetSQL();
                GetAllDataRefreshGridView();
            }
        }

        public void SetSQL(string sql)
        {
            m_sql = sql;
        }
        public string GetSQL()
        {
            return m_sql;
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

    }
}
