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
    public partial class FrmCashUsage : Form
    {
        private AccessHelper m_accessHelper = new AccessHelper();
        private int m_id,dwbs;
        private string m_dwdm,m_bz,m_yf;
        private double  m_rydyzc, m_zfgtjyzc, m_clfzc, m_yxzc, m_lxzc, m_qtzc,m_bskzc;

        private string SQL_Admin_Update = "UPDATE t_xjsy SET dwdm=@DWDM,yf=@YF,rydyzc=@RYDYZC,zfgtjyzc=@ZFGTJYZC,clfzc=@CLFZC,yxzc=@YXZC,lxzc=@LXZC,qtzc=@QTZC,bz=@BZ,bskzc=@BSKZC WHERE ID=@ID";
        private string SQL_Admin_Insert = "INSERT INTO t_xjsy(dwdm,yf,rydyzc,zfgtjyzc,clfzc,yxzc,lxzc,qtzc,bz,bskzc) values(@DWDM,@YF,@RYDYZC,@ZFGTJYZC,@CLFZC,@YXZC,@LXZC,@QTZC,@BZ,@BSKZC)";
        private string SQL_Admin_Delete = "DELETE FROM t_xjsy WHERE ID=@ID";

        public String filterStr = "";
        private Boolean notsaved = false; //标识数据是否修改，默认没有修改
        private Boolean zbhz = false;//总部汇总

        public void setnotsaved()
        {
            this.notsaved = true;
        }
        public void setsaved()
        {
            this.notsaved = false;
        }
        public FrmCashUsage()
        {
            InitializeComponent();
            Load += new EventHandler(FrmCashUsage_Load);
        }

        private string ReturnDeptCode(string deptName)
        {
            string tempSQL = "SELECT * FROM t_dwxx WHERE dwmc='" + deptName + "'";
            DataSet ds = m_accessHelper.getDataSet(tempSQL);
            if (ds.Tables[0].Rows.Count != 0)
                return ds.Tables[0].Rows[0]["dwdm"].ToString();
            else
                return "";
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
            string tempSQL = "SELECT t_xjsy.ID as ID号,t_xjsy.dwdm as 单位代码,t_dwxx.dwmc as 单位名称,t_dwxx.dwxz as 单位性质,t_dwxx.dwjb as 单位级别,t_dwxx.dwlx as 单位类型,t_dwxx.szss as 所在省市,t_xjsy.yf as 月份,round(t_xjsy.rydyzc+t_xjsy.zfgtjyzc+t_xjsy.clfzc+t_xjsy.yxzc+t_xjsy.lxzc+t_xjsy.bskzc+t_xjsy.qtzc,2) as 小计,t_xjsy.rydyzc as 人员待遇支出,t_xjsy.zfgtjyzc as 支付个体经营者支出,t_xjsy.clfzc as 差旅费探亲路费和签证费快递费等支出,t_xjsy.yxzc as 演习执行抢险救灾等重大专项任务支出,t_xjsy.lxzc as 1000元以内零星支出,t_xjsy.bskzc as 在不具备刷卡条件的地区和场所发生的公务支出,t_xjsy.qtzc as 其他特殊情况支出,t_xjsy.bz as 备注 FROM t_dwxx,t_xjsy WHERE t_xjsy.dwdm=t_dwxx.dwdm " + filterStr + " ORDER BY t_xjsy.dwdm,t_xjsy.ID";
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
            string tempSQL = "SELECT * FROM t_xjsy WHERE ID=" + dataIndex;
            DataTable dt = m_accessHelper.getDataSet(tempSQL).Tables[0];
            return dt;
        }

        private void ClearControlData()
        {
            tbBZ.Text = "";
            tbCLF.Text = "0.00";
            tbLXZC.Text = "0.00";
            tbQTZC.Text = "0.00";
            tbRYDYZC.Text = "0.00";
            tbYXZC.Text = "0.00";
            tbZFGTJYZZC.Text = "0.00";
            tbBSKZC.Text = "0.00";

            comboDWDM.Enabled = true;
            comboYF.Enabled = true;
            comboYF.SelectedIndex = 0;
            if (dwbs != 1)
            {
                comboDWDM.SelectedIndex = 0;
            }
            
        }

        private void ClearVariableData()
        {
            m_bz = "";
            m_clfzc = 0;
            m_lxzc = 0;
            m_qtzc = 0;
            m_rydyzc = 0;
            m_yxzc = 0;
            m_zfgtjyzc = 0;
            m_bskzc = 0;
            if (dwbs != 1)
            {
              m_dwdm = comboDWDM.Items[0].ToString();
            }
          
            m_yf = comboYF.Items[0].ToString();
            m_id = ClassConstants.JD_NOTSELECTED;
        }

        public void ReadDataToVariables()
        {
            m_id = int.Parse(dataGridView.SelectedRows[0].Cells[1].Value.ToString());
            DataTable dt = this.GetDataByIndex(m_id);

            m_dwdm = dt.Rows[0]["dwdm"].ToString();
            m_bz = dt.Rows[0]["bz"].ToString();
            m_clfzc = double.Parse(dt.Rows[0]["clfzc"].ToString());
            m_lxzc = double.Parse(dt.Rows[0]["lxzc"].ToString());
            m_qtzc = double.Parse(dt.Rows[0]["qtzc"].ToString());
            m_rydyzc = double.Parse(dt.Rows[0]["rydyzc"].ToString());
            m_yf = dt.Rows[0]["yf"].ToString();
            m_bskzc = double.Parse(dt.Rows[0]["bskzc"].ToString());
            m_yxzc = double.Parse(dt.Rows[0]["yxzc"].ToString());
            m_zfgtjyzc = double.Parse(dt.Rows[0]["zfgtjyzc"].ToString());
        }

        public void LoadVariablesToControls()
        {
            tbBZ.Text = m_bz.ToString();
            tbCLF.Text = m_clfzc.ToString("n");
            tbLXZC.Text = m_lxzc.ToString("n");
            tbQTZC.Text = m_qtzc.ToString("n");
            tbRYDYZC.Text = m_rydyzc.ToString("n");
            tbYXZC.Text = m_yxzc.ToString("n");
            tbZFGTJYZZC.Text = m_zfgtjyzc.ToString("n");
            tbBSKZC.Text = m_bskzc.ToString("n");

            comboYF.SelectedIndex = comboYF.Items.IndexOf(m_yf);

            comboDWDM.SelectedValue = m_dwdm;

            comboYF.Enabled = false;
            comboDWDM.Enabled = false;
        }

        void FrmCashUsage_Load(object sender, EventArgs e)
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

        private void btnSave_Click(object sender, EventArgs e)
        {
            m_bz = tbBZ.Text;
            m_clfzc = double.Parse(tbCLF.Text.Replace(",",""));
            m_lxzc = double.Parse(tbLXZC.Text.Replace(",", ""));
            m_qtzc = double.Parse(tbQTZC.Text.Replace(",", ""));
            m_rydyzc = double.Parse(tbRYDYZC.Text.Replace(",", ""));
            m_yxzc = double.Parse(tbYXZC.Text.Replace(",", ""));
            m_zfgtjyzc = double.Parse(tbZFGTJYZZC.Text.Replace(",", ""));
            m_bskzc = double.Parse(tbBSKZC.Text.Replace(",", ""));

            if (m_qtzc > 0 && m_bz.Trim() == "")
            {
                MessageBox.Show("现金支出类型为其他支出的,在备注中说明开支内容。", "系统提示", MessageBoxButtons.OK, MessageBoxIcon.Information);
                return;
            }

            m_yf = comboYF.Text;
            m_dwdm = comboDWDM.SelectedValue.ToString();


            if (m_id != ClassConstants.JD_NOTSELECTED)
            {
                OleDbParameter[] parms = new OleDbParameter[] { 
                    new OleDbParameter("@DWDM",OleDbType.VarChar),
                    new OleDbParameter("@YF",OleDbType.VarChar),
                    new OleDbParameter("@RYDYZC",OleDbType.Double),
                    new OleDbParameter("@ZFGTJYZC",OleDbType.Double),
                    new OleDbParameter("@CLFZC",OleDbType.Double),
                    new OleDbParameter("@YXZC",OleDbType.Double),
                    new OleDbParameter("@LXZC",OleDbType.Double),
                    new OleDbParameter("@QTZC",OleDbType.Double),
                    new OleDbParameter("@BZ",OleDbType.VarChar),
                    new OleDbParameter("@BSKZC",OleDbType.Double),

                    new OleDbParameter("@ID",OleDbType.Integer)
                };
                parms[0].Value = m_dwdm;
                parms[1].Value = m_yf;
                parms[2].Value = m_rydyzc;
                parms[3].Value = m_zfgtjyzc;
                parms[4].Value = m_clfzc;
                parms[5].Value = m_yxzc;
                parms[6].Value = m_lxzc;
                parms[7].Value = m_qtzc;
                parms[8].Value = m_bz;
                parms[9].Value = m_bskzc;

                parms[10].Value = m_id;

                UpdateData(parms);
                GetAllDataRefreshGridView();
                MessageBox.Show("更新数据完毕！","系统提示", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
            else
            {
                string tempSql = "SELECT * FROM t_xjsy WHERE dwdm='" + m_dwdm + "' and yf='" + m_yf + "'";
                DataSet ds = m_accessHelper.getDataSet(tempSql);
                if (ds.Tables[0].Rows.Count != 0)
                {
                    MessageBox.Show("每个单位统计12个月使用情况，当前月份已经统计，请选择其他月份，或者删除后再进行相应操作，若要修改请选中某条数据直接修改！", "系统提示",MessageBoxButtons.OK,MessageBoxIcon.Information);
                    return;
                }

                OleDbParameter[] parms = new OleDbParameter[] { 
                    new OleDbParameter("@DWDM",OleDbType.VarChar),
                    new OleDbParameter("@YF",OleDbType.VarChar),
                    new OleDbParameter("@RYDYZC",OleDbType.Double),
                    new OleDbParameter("@ZFGTJYZC",OleDbType.Double),
                    new OleDbParameter("@CLFZC",OleDbType.Double),
                    new OleDbParameter("@YXZC",OleDbType.Double),
                    new OleDbParameter("@LXZC",OleDbType.Double),
                    new OleDbParameter("@QTZC",OleDbType.Double),
                    new OleDbParameter("@BZ",OleDbType.VarChar),
                    new OleDbParameter("@BSKZC",OleDbType.Double)
                };
                parms[0].Value = m_dwdm;
                parms[1].Value = m_yf;
                parms[2].Value = m_rydyzc;
                parms[3].Value = m_zfgtjyzc;
                parms[4].Value = m_clfzc;
                parms[5].Value = m_yxzc;
                parms[6].Value = m_lxzc;
                parms[7].Value = m_qtzc;
                parms[8].Value = m_bz;
                parms[9].Value = m_bskzc;

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

        private void tbRYDYZC_TextChanged(object sender, EventArgs e)
        {
            ClassInputRestricter.CheckChange(sender);
            //setnotsaved();
        }

        private void tbZFGTJYZZC_TextChanged(object sender, EventArgs e)
        {
            ClassInputRestricter.CheckChange(sender);
            //setnotsaved();
        }

        private void tbCLF_TextChanged(object sender, EventArgs e)
        {
            ClassInputRestricter.CheckChange(sender);
            //setnotsaved();
        }

        private void tbYXZC_TextChanged(object sender, EventArgs e)
        {
            ClassInputRestricter.CheckChange(sender);
            //setnotsaved();
        }

        private void tbLXZC_TextChanged(object sender, EventArgs e)
        {
            ClassInputRestricter.CheckChange(sender);
            //setnotsaved();
        }

        private void tbQTZC_TextChanged(object sender, EventArgs e)
        {
            ClassInputRestricter.CheckChange(sender);
            //setnotsaved();
        }

        private void btnExport_Click(object sender, EventArgs e)
        {
            DataTable dt = dataGridView.DataSource as DataTable;
            ExcelUI ExcelUI = new ExcelUI();
            ExcelUI.ExportExcel(dt, "");
        }

        private void dataGridView_SelectionChanged(object sender, EventArgs e)
        {
            if (dataGridView.SelectedRows.Count != 0 && dataGridView.SelectedRows[0].Cells[1].Value.ToString() != ""&&zbhz==false)
            {
                ReadDataToVariables();
                LoadVariablesToControls();
            }
        }

        private void tbBSKZC_TextChanged(object sender, EventArgs e)
        {
            ClassInputRestricter.CheckChange(sender);
        }

        private void tbRYDYZC_Leave(object sender, EventArgs e)
        {
            ClassInputRestricter.CheckFormat(sender);
        }

        private void tbRYDYZC_KeyPress(object sender, KeyPressEventArgs e)
        {
            ClassInputRestricter.CheckInput(sender, e);
        }

        private void tbZFGTJYZZC_KeyPress(object sender, KeyPressEventArgs e)
        {
            ClassInputRestricter.CheckInput(sender, e);
        }

        private void tbCLF_KeyPress(object sender, KeyPressEventArgs e)
        {
            ClassInputRestricter.CheckInput(sender, e);
        }

        private void tbYXZC_KeyPress(object sender, KeyPressEventArgs e)
        {
            ClassInputRestricter.CheckInput(sender, e);
        }

        private void tbLXZC_KeyPress(object sender, KeyPressEventArgs e)
        {
            ClassInputRestricter.CheckInput(sender, e);
        }

        private void tbQTZC_KeyPress(object sender, KeyPressEventArgs e)
        {
            ClassInputRestricter.CheckInput(sender, e);
        }

        private void tbBSKZC_KeyPress(object sender, KeyPressEventArgs e)
        {
            ClassInputRestricter.CheckInput(sender, e);
        }

        private void tbZFGTJYZZC_Leave(object sender, EventArgs e)
        {
            ClassInputRestricter.CheckFormat(sender);
        }

        private void tbCLF_Leave(object sender, EventArgs e)
        {
            ClassInputRestricter.CheckFormat(sender);
        }

        private void tbYXZC_Leave(object sender, EventArgs e)
        {
            ClassInputRestricter.CheckFormat(sender);
        }

        private void tbLXZC_Leave(object sender, EventArgs e)
        {
            ClassInputRestricter.CheckFormat(sender);
        }

        private void tbQTZC_Leave(object sender, EventArgs e)
        {
            ClassInputRestricter.CheckFormat(sender);
        }

        private void tbBSKZC_Leave(object sender, EventArgs e)
        {
            ClassInputRestricter.CheckFormat(sender);
        }

        private void btnFilter_Click(object sender, EventArgs e)
        {
            Frm_select_cash selectcash = new Frm_select_cash();
            selectcash.ShowDialog();
            if (selectcash.DialogResult == DialogResult.OK)
            {
                filterStr = selectcash.GetSQL();
                GetAllDataRefreshGridView();
            }
        }

        private void FrmCashUsage_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.Control && e.KeyCode == Keys.S)
            {
                btnSave_Click(sender, e);
            }
        }

        private void FrmCashUsage_FormClosing(object sender, FormClosingEventArgs e)
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

        private void tbBZ_TextChanged(object sender, EventArgs e)
        {
            //setnotsaved();
        }

        //总部汇总数据专用（此时修改数据会报错）
        private void button2_Click(object sender, EventArgs e)
        {
            string tempSQL = "SELECT t_dwxx.dwdm as 单位代码,t_dwxx.dwmc as 单位名称,t_xjsy.yf as 月份,round(sum(t_xjsy.rydyzc)+sum(t_xjsy.zfgtjyzc)+sum(t_xjsy.clfzc)+sum(t_xjsy.yxzc)+sum(t_xjsy.lxzc)+sum(t_xjsy.bskzc)+sum(t_xjsy.qtzc),2) as 小计,sum(t_xjsy.rydyzc) as 人员待遇支出,sum(t_xjsy.zfgtjyzc) as 支付个体经营者支出,sum(t_xjsy.clfzc) as 差旅费探亲路费和签证费快递费等支出,sum(t_xjsy.yxzc) as 演习执行抢险救灾等重大专项任务支出,sum(t_xjsy.lxzc) as 1000元以内零星支出,sum(t_xjsy.bskzc) as 在不具备刷卡条件的地区和场所发生的公务支出,sum(t_xjsy.qtzc) as 其他特殊支出 FROM t_dwxx,t_xjsy WHERE left(t_xjsy.dwdm,3)=t_dwxx.dwdm group by t_dwxx.dwdm,t_dwxx.dwmc,t_xjsy.yf ORDER BY t_dwxx.dwdm";
            DataSet ds = m_accessHelper.getDataSet(tempSQL);
            this.zbhz = true;
            DataTable dt = ds.Tables[0];
            DataColumn dc = dt.Columns.Add("序号", typeof(int));
            dt.Columns["序号"].SetOrdinal(0);
            for (int i = 0; i < dt.Rows.Count; i++)
            {
                dt.Rows[i][0] = i + 1;
            }
            dataGridView.DataSource = dt;
            btnSave.Enabled = false;
            btnRemove.Enabled = false;
        }

        //总部汇总数据专用（此时修改数据会报错）
        private void button1_Click(object sender, EventArgs e)
        {
            string tempSQL = "SELECT t_dwxx.dwdm as 单位代码,t_dwxx.dwmc as 单位名称,round(sum(t_xjsy.rydyzc)+sum(t_xjsy.zfgtjyzc)+sum(t_xjsy.clfzc)+sum(t_xjsy.yxzc)+sum(t_xjsy.lxzc)+sum(t_xjsy.bskzc)+sum(t_xjsy.qtzc),2) as 小计,sum(t_xjsy.rydyzc) as 人员待遇支出,sum(t_xjsy.zfgtjyzc) as 支付个体经营者支出,sum(t_xjsy.clfzc) as 差旅费探亲路费和签证费快递费等支出,sum(t_xjsy.yxzc) as 演习执行抢险救灾等重大专项任务支出,sum(t_xjsy.lxzc) as 1000元以内零星支出,sum(t_xjsy.bskzc) as 在不具备刷卡条件的地区和场所发生的公务支出,sum(t_xjsy.qtzc) as 其他特殊支出 FROM t_dwxx,t_xjsy WHERE t_xjsy.dwdm=t_dwxx.dwdm group by t_dwxx.dwdm,t_dwxx.dwmc,t_dwxx.dwxz,t_dwxx.dwlx,t_dwxx.dwjb,t_dwxx.szss ORDER BY t_dwxx.dwdm";
            DataSet ds = m_accessHelper.getDataSet(tempSQL);
            this.zbhz = true;
            DataTable dt = ds.Tables[0];
            DataColumn dc = dt.Columns.Add("序号", typeof(int));
            dt.Columns["序号"].SetOrdinal(0);
            for (int i = 0; i < dt.Rows.Count; i++)
            {
                dt.Rows[i][0] = i + 1;
            }
            dataGridView.DataSource = dt;
            btnSave.Enabled = false;
            btnRemove.Enabled = false;
        }
    }
}
