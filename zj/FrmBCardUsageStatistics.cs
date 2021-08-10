using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;
using System.Data.OleDb;

namespace zj
{
    public partial class FrmBCardUsageStatistics : Form
    {
        int m_id,dwbs;
        string m_dwdm;
        int m_qdfwxy, m_azcwposj, m_azshbxxt, m_ffgwksl, m_dlhsdw,m_txgwkjsdw;
        int m_gsyhfk, m_nyyhfk, m_zgyhfk, m_jsyhfk,m_jtyhfk, m_qtyhfk;
        double m_skzfje, m_xjzfje, m_zzkhdxe;
        double m_bjbsktjdq, m_bjbsktjcs, m_agdzfggr, m_znsyxjjs, m_zxzzdzxrw, m_qtqk;
        string m_bz;
        private AccessHelper m_accessHelper = new AccessHelper();
        private string SQL_Admin_Update = "UPDATE t_gwk SET dwdm=@DWDM,dlhsdw=@DLHSDW,txgwkjsdw=@TXGWKJSDW,qdfwxy=@QDFWXY,azcwposj=@AZCWPOSJ,azshbxxt=@AZSHBXXT,ffgwksl=@FFGWKSL,zzkhdxe=@ZZKHDXE,gsyhfk=@GSYHFK,nyyhfk=@NYYHFK,zgyhfk=@ZGYHFK,jsyhfk=@JSYHFK,jtyhfk=@JTYHFK,qtyhfk=@QTYHFK,skzfje=@SKZFJE,xjzfje=@XJZFJE,bjbsktjdq=@BJBSKTJDQ,bjbsktjcs=@BJBSKTJCS,agdzfggr=@AGDZFGGR,znsyxjjs=@ZNSYXJJS,zxzzdzxrw=@ZXZZDZXRW,qtqk=@QTQK,bz=@BZ WHERE ID=@ID";
        private string SQL_Admin_Insert = "INSERT INTO t_gwk(dwdm,dlhsdw,txgwkjsdw,qdfwxy,azcwposj,azshbxxt,ffgwksl,zzkhdxe,gsyhfk,nyyhfk,zgyhfk,jsyhfk,jtyhfk,qtyhfk,skzfje,xjzfje,bjbsktjdq,bjbsktjcs,agdzfggr,znsyxjjs,zxzzdzxrw,qtqk,bz) values(@DWDM,@DLHSDW,@TXGWKJSDW,@QDFWXY,@AZCWPOSJ,@AZSHBXXT,@FFGWKSL,@ZZKHDXE,@GSYHFK,@NYYHFK,@ZGYHFK,@JSYHFK,@JTYHFK,@QTYHFK,@SKZFJE,@XJZFJE,@BJBSKTJDQ,@BJBSKTJCS,@AGDZFGGR,@ZNSYXJJS,@ZXZZDZXRW,@QTQK,@BZ)";
        private string SQL_Admin_Delete = "DELETE FROM t_gwk WHERE ID=@ID";
        public string filterStr = "";
        private Boolean notsaved = false; //标识数据是否修改，默认没有修改

        public void setnotsaved()
        {
            this.notsaved = true;
        }

        public void setsaved()
        {
            this.notsaved = false;
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
            /*
            for (int i = 0; i < ds.Tables[0].Rows.Count; i++)
            {
                comboDWDM.Items.Add(ds.Tables[0].Rows[i]["dwmc"]);
            }
             */
        }

        public void GetAllDataRefreshGridView()
        {
            //string tempSQL = "SELECT * FROM t_zjjc ORDER BY ID";
            string tempSQL = "SELECT t_gwk.ID as ID号,t_gwk.dwdm as 单位代码,t_dwxx.dwmc as 单位名称,t_dwxx.dwxz as 单位性质,t_dwxx.dwjb as 单位级别,t_dwxx.dwlx as 单位类型,t_dwxx.szss as 所在省市,t_gwk.dlhsdw as 独立核算单位,t_gwk.txgwkjsdw as 推行公务卡结算单位,t_gwk.qdfwxy as 签订服务协议,t_gwk.azcwposj as 安装财务POS机,t_gwk.azshbxxt as 安装审核报销系统,t_gwk.ffgwksl as 公务卡使用数量,t_gwk.zzkhdxe as 单位结算卡核定限额,t_gwk.gsyhfk as 工商银行发卡,t_gwk.nyyhfk as 农业银行发卡,t_gwk.zgyhfk as 中国银行发卡,t_gwk.jsyhfk as 建设银行发卡,t_gwk.jtyhfk as 交通银行发卡,t_gwk.qtyhfk as 其他银行发卡,t_gwk.skzfje as 刷卡支付金额,t_gwk.xjzfje as 现金支付金额,t_gwk.bjbsktjdq as 不具备刷卡条件地区,t_gwk.bjbsktjcs as 不具备刷卡条件场所,t_gwk.agdzfggr as 按规定支付给个人,t_gwk.znsyxjjs as 只能使用现金结算,t_gwk.zxzzdzxrw as 执行重大专项任务等,t_gwk.qtqk as 其它情况,t_gwk.bz as 备注 FROM t_dwxx,t_gwk WHERE t_gwk.dwdm=t_dwxx.dwdm " + filterStr + "ORDER BY t_gwk.dwdm,t_gwk.ID";
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
            string tempSQL = "SELECT * FROM t_gwk WHERE ID=" + dataIndex;
            DataTable dt = m_accessHelper.getDataSet(tempSQL).Tables[0];
            return dt;
        }

        private void ClearControlData()
        {
            tbQDFWXY.Text = "0";
            tbAZCWPOSJ.Text = "0";
            tbAZSHBXXT.Text = "0";
            tbFFGWKSL.Text = "0";
            tbDLHSDW.Text = "0";
            tbTXGWKJSDW.Text = "0";

            tbGSYHFK.Text = "0";
            tbNYYHFK.Text = "0";
            tbZGYHFK.Text = "0";
            tbJSYHFK.Text = "0";
            tbJTYHFK.Text = "0";
            tbQTYHFK.Text = "0";

            tbSKZFJE.Text = "0.00";
            tbXJZFJE.Text = "0.00";
            tbZZKHDXE.Text = "0.00";

            tbBJBSKTJDQ.Text = "0.00";
            tbBJBSKTJCS.Text = "0.00";
            tbAGDZFGGR.Text = "0.00";
            tbZNSYXJJS.Text = "0.00";
            tbZXZZDZXRW.Text = "0.00";
            tbQTQK.Text = "0.00";
            tbBZ.Text = "";

            comboDWDM.Enabled = true;
            if (dwbs != 1)
            {
            comboDWDM.SelectedIndex = 0;
            }

        }

        private void ClearVariableData()
        {

            m_agdzfggr = 0;
            m_azcwposj = 0;
            m_azshbxxt = 0;
            m_bjbsktjcs = 0;
            m_bjbsktjdq = 0;
            m_ffgwksl = 0;
            m_gsyhfk = 0;
            m_jsyhfk = 0;
            m_nyyhfk = 0;
            m_qdfwxy = 0;
            m_qtqk = 0;
            m_qtyhfk = 0;
            m_skzfje = 0;
            m_xjzfje = 0;
            m_zgyhfk = 0;
            m_znsyxjjs = 0;
            m_zxzzdzxrw = 0;
            m_txgwkjsdw=0;
            m_jtyhfk = 0;
            m_zzkhdxe = 0;
            m_bz = "";
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
            m_bz = dt.Rows[0]["bz"].ToString();

            m_dlhsdw = int.Parse(dt.Rows[0]["dlhsdw"].ToString());
            m_txgwkjsdw = int.Parse(dt.Rows[0]["txgwkjsdw"].ToString());
            m_agdzfggr = double.Parse(dt.Rows[0]["agdzfggr"].ToString());
            m_zzkhdxe = double.Parse(dt.Rows[0]["zzkhdxe"].ToString());
            m_azcwposj = int.Parse(dt.Rows[0]["azcwposj"].ToString());
            m_azshbxxt = int.Parse(dt.Rows[0]["azshbxxt"].ToString());
            m_bjbsktjcs = double.Parse(dt.Rows[0]["bjbsktjcs"].ToString());
            m_bjbsktjdq = double.Parse(dt.Rows[0]["bjbsktjdq"].ToString());
            m_ffgwksl = int.Parse(dt.Rows[0]["ffgwksl"].ToString());
            m_gsyhfk = int.Parse(dt.Rows[0]["gsyhfk"].ToString());
            m_jsyhfk = int.Parse(dt.Rows[0]["jsyhfk"].ToString());
            m_nyyhfk = int.Parse(dt.Rows[0]["nyyhfk"].ToString());
            m_qdfwxy = int.Parse(dt.Rows[0]["qdfwxy"].ToString());
            m_qtqk = double.Parse(dt.Rows[0]["qtqk"].ToString());
            m_jtyhfk = int.Parse(dt.Rows[0]["jtyhfk"].ToString());
            m_qtyhfk = int.Parse(dt.Rows[0]["qtyhfk"].ToString());
            m_skzfje = double.Parse(dt.Rows[0]["skzfje"].ToString());
            m_xjzfje = double.Parse(dt.Rows[0]["xjzfje"].ToString());
            m_zgyhfk = int.Parse(dt.Rows[0]["zgyhfk"].ToString());
            m_znsyxjjs = double.Parse(dt.Rows[0]["znsyxjjs"].ToString());
            m_zxzzdzxrw = double.Parse(dt.Rows[0]["zxzzdzxrw"].ToString());
        }

        public void LoadVariablesToControls()
        {
            tbAGDZFGGR.Text = m_agdzfggr.ToString("n");
            tbAZCWPOSJ.Text = m_azcwposj.ToString();
            tbAZSHBXXT.Text = m_azshbxxt.ToString();
            tbBJBSKTJCS.Text = m_bjbsktjcs.ToString("n");
            tbBJBSKTJDQ.Text = m_bjbsktjdq.ToString("n");
            tbBZ.Text = m_bz.ToString();
            tbFFGWKSL.Text = m_ffgwksl.ToString();
            tbGSYHFK.Text = m_gsyhfk.ToString();
            tbJSYHFK.Text = m_jsyhfk.ToString();
            tbNYYHFK.Text = m_nyyhfk.ToString();
            tbQDFWXY.Text = m_qdfwxy.ToString();
            tbQTQK.Text = m_qtqk.ToString("n");
            tbQTYHFK.Text = m_qtyhfk.ToString();
            tbSKZFJE.Text = m_skzfje.ToString("n");
            tbXJZFJE.Text = m_xjzfje.ToString("n");
            tbZGYHFK.Text = m_zgyhfk.ToString();
            tbZNSYXJJS.Text = m_znsyxjjs.ToString("n");
            tbZXZZDZXRW.Text = m_zxzzdzxrw.ToString("n");
            tbZZKHDXE.Text = m_zzkhdxe.ToString("n");
            tbDLHSDW.Text = m_dlhsdw.ToString();
            tbTXGWKJSDW.Text = m_txgwkjsdw.ToString();
            tbJTYHFK.Text = m_jtyhfk.ToString();
            tbBZ.Text = m_bz.ToString();
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


        public FrmBCardUsageStatistics()
        {
            InitializeComponent();
            Load += new EventHandler(FrmBCardUsageStatistics_Load);
        }

        void FrmBCardUsageStatistics_Load(object sender, EventArgs e)
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
            m_agdzfggr = double.Parse(tbAGDZFGGR.Text.Replace(",",""));
            m_azcwposj = int.Parse(tbAZCWPOSJ.Text);
            m_azshbxxt = int.Parse(tbAZSHBXXT.Text);
            m_bjbsktjcs = double.Parse(tbBJBSKTJCS.Text.Replace(",", ""));
            m_bjbsktjdq = double.Parse(tbBJBSKTJDQ.Text.Replace(",", ""));
            m_ffgwksl = int.Parse(tbFFGWKSL.Text);
            m_gsyhfk = int.Parse(tbGSYHFK.Text);
            m_jsyhfk = int.Parse(tbJSYHFK.Text);
            m_nyyhfk = int.Parse(tbNYYHFK.Text);
            m_qdfwxy = int.Parse(tbQDFWXY.Text);
            m_qtqk = double.Parse(tbQTQK.Text.Replace(",", ""));
            m_qtyhfk = int.Parse(tbQTYHFK.Text.Replace(",", ""));
            m_skzfje = double.Parse(tbSKZFJE.Text.Replace(",", ""));
            m_xjzfje = 0;//数据从现金使用表中提取
            m_zgyhfk = int.Parse(tbZGYHFK.Text);
            m_znsyxjjs = double.Parse(tbZNSYXJJS.Text.Replace(",", ""));
            m_jtyhfk = int.Parse(tbJTYHFK.Text);
            m_dlhsdw = int.Parse(tbDLHSDW.Text);
            m_txgwkjsdw = int.Parse(tbTXGWKJSDW.Text);
            m_zxzzdzxrw = double.Parse(tbZXZZDZXRW.Text.Replace(",", ""));
            m_zzkhdxe = double.Parse(tbZZKHDXE.Text.Replace(",", ""));

            m_bz = tbBZ.Text;

            if (m_qtqk > 0 && m_bz.Trim() == "")
            {
                MessageBox.Show("支出类型为‘其他情况’时,在‘备注’中说明开支内容！", "系统提示", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }
            /*
            if (m_dlhsdw < m_txgwkjsdw)
            {
                MessageBox.Show("‘推行公务卡结算单位数量’大于‘独立核算单位数量’！", "系统提示", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }
             */
            if ((m_gsyhfk + m_nyyhfk + m_jsyhfk + m_zgyhfk + m_jtyhfk + m_qtyhfk) < m_ffgwksl)
            {
                MessageBox.Show("‘公务卡使用数量’大于‘银行发卡数量’！", "系统提示", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }


            m_dwdm = comboDWDM.SelectedValue.ToString();

            if (m_id != ClassConstants.JD_NOTSELECTED)
            {
                OleDbParameter[] parms = new OleDbParameter[] { 
                    new OleDbParameter("@DWDM",OleDbType.VarChar),
                    new OleDbParameter("@DLHSDW",OleDbType.Integer),
                    new OleDbParameter("@TXGWKJSDW",OleDbType.Integer),
                    new OleDbParameter("@QDFWXY",OleDbType.Integer),
                    new OleDbParameter("@AZCWPOSJ",OleDbType.Integer),
                    new OleDbParameter("@AZSHBXXT",OleDbType.Integer),
                    new OleDbParameter("@FFGWKSL",OleDbType.Integer),
                    new OleDbParameter("@ZZKHDXE",OleDbType.Double),
                    new OleDbParameter("@GSYHFK",OleDbType.Integer),
                    new OleDbParameter("@NYYHFK",OleDbType.Integer),
                    new OleDbParameter("@ZGYHFK",OleDbType.Integer),
                    new OleDbParameter("@JSYHFK",OleDbType.Integer),
                    new OleDbParameter("@JTYHFK",OleDbType.Integer),
                    new OleDbParameter("@QTYHFK",OleDbType.Integer),
                    new OleDbParameter("@SKZFJE",OleDbType.Double),
                    new OleDbParameter("@XJZFJE",OleDbType.Double),
                    new OleDbParameter("@BJBSKTJDQ",OleDbType.Double),
                    new OleDbParameter("@BJBSKTJCS",OleDbType.Double),
                    new OleDbParameter("@AGDZFGGR",OleDbType.Double),
                    new OleDbParameter("@ZNSYXJJS",OleDbType.Double),
                    new OleDbParameter("@ZXZZDZXRW",OleDbType.Double),
                    new OleDbParameter("@QTQK",OleDbType.Double),
                    new OleDbParameter("@BZ",OleDbType.VarChar),
                    new OleDbParameter("@ID",OleDbType.Integer)
                };
                parms[0].Value = m_dwdm;
                parms[1].Value = m_dlhsdw;
                parms[2].Value = m_txgwkjsdw;
                parms[3].Value = m_qdfwxy;
                parms[4].Value = m_azcwposj;
                parms[5].Value = m_azshbxxt;
                parms[6].Value = m_ffgwksl;
                parms[7].Value = m_zzkhdxe;
                parms[8].Value = m_gsyhfk;
                parms[9].Value = m_nyyhfk;
                parms[10].Value = m_zgyhfk;
                parms[11].Value = m_jsyhfk;
                parms[12].Value = m_jtyhfk;
                parms[13].Value = m_qtyhfk;
                parms[14].Value = m_skzfje;
                parms[15].Value = m_xjzfje;
                parms[16].Value = m_bjbsktjdq;
                parms[17].Value = m_bjbsktjcs;
                parms[18].Value = m_agdzfggr;
                parms[19].Value = m_znsyxjjs;
                parms[20].Value = m_zxzzdzxrw;
                parms[21].Value = m_qtqk;
                parms[22].Value = m_bz;
                parms[23].Value = m_id;

                UpdateData(parms);
                GetAllDataRefreshGridView();
                MessageBox.Show("更新数据完毕！","系统提示",MessageBoxButtons.OK,MessageBoxIcon.Information);
            }
            else
            {
                string tempSql = "SELECT * FROM t_gwk WHERE dwdm='" + m_dwdm + "'";
                DataSet ds = m_accessHelper.getDataSet(tempSql);
                if (ds.Tables[0].Rows.Count != 0)
                {
                    MessageBox.Show("每个单位只需进行一次公务卡情况统计！", "系统提示",MessageBoxButtons.OK,MessageBoxIcon.Stop);
                    return;
                }
                OleDbParameter[] parms = new OleDbParameter[] { 
                    new OleDbParameter("@DWDM",OleDbType.VarChar),
                    new OleDbParameter("@DLHSDW",OleDbType.Integer),
                    new OleDbParameter("@TXGWKJSDW",OleDbType.Integer),
                    new OleDbParameter("@QDFWXY",OleDbType.Integer),
                    new OleDbParameter("@AZCWPOSJ",OleDbType.Integer),
                    new OleDbParameter("@AZSHBXXT",OleDbType.Integer),
                    new OleDbParameter("@FFGWKSL",OleDbType.Integer),
                    new OleDbParameter("@ZZKHDXE",OleDbType.Double),
                    new OleDbParameter("@GSYHFK",OleDbType.Integer),
                    new OleDbParameter("@NYYHFK",OleDbType.Integer),
                    new OleDbParameter("@ZGYHFK",OleDbType.Integer),
                    new OleDbParameter("@JSYHFK",OleDbType.Integer),
                    new OleDbParameter("@JTYHFK",OleDbType.Integer),
                    new OleDbParameter("@QTYHFK",OleDbType.Integer),
                    new OleDbParameter("@SKZFJE",OleDbType.Double),
                    new OleDbParameter("@XJZFJE",OleDbType.Double),
                    new OleDbParameter("@BJBSKTJDQ",OleDbType.Double),
                    new OleDbParameter("@BJBSKTJCS",OleDbType.Double),
                    new OleDbParameter("@AGDZFGGR",OleDbType.Double),
                    new OleDbParameter("@ZNSYXJJS",OleDbType.Double),
                    new OleDbParameter("@ZXZZDZXRW",OleDbType.Double),
                    new OleDbParameter("@QTQK",OleDbType.Double),
                    new OleDbParameter("@BZ",OleDbType.VarChar)
                };
                parms[0].Value = m_dwdm;
                parms[1].Value = m_dlhsdw;
                parms[2].Value = m_txgwkjsdw;
                parms[3].Value = m_qdfwxy;
                parms[4].Value = m_azcwposj;
                parms[5].Value = m_azshbxxt;
                parms[6].Value = m_ffgwksl;
                parms[7].Value = m_zzkhdxe;
                parms[8].Value = m_gsyhfk;
                parms[9].Value = m_nyyhfk;
                parms[10].Value = m_zgyhfk;
                parms[11].Value = m_jsyhfk;
                parms[12].Value = m_jtyhfk;
                parms[13].Value = m_qtyhfk;
                parms[14].Value = m_skzfje;
                parms[15].Value = m_xjzfje;
                parms[16].Value = m_bjbsktjdq;
                parms[17].Value = m_bjbsktjcs;
                parms[18].Value = m_agdzfggr;
                parms[19].Value = m_znsyxjjs;
                parms[20].Value = m_zxzzdzxrw;
                parms[21].Value = m_qtqk;
                parms[22].Value = m_bz;

                InsertData(parms);
                GetAllDataRefreshGridView();
                MessageBox.Show("插入数据完毕！","系统提示",MessageBoxButtons.OK,MessageBoxIcon.Information);
            }
            ClearControlData();
            ClearVariableData();
            setsaved();
        }

        private void btnExit_Click(object sender, EventArgs e)
        {
            this.Close();
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

        private void tbQDFWXY_TextChanged(object sender, EventArgs e)
        {
            ClassInputRestricter.CheckChange(sender);
            //setnotsaved();
        }

        private void tbAZCWPOSJ_TextChanged(object sender, EventArgs e)
        {
            ClassInputRestricter.CheckChange(sender);
            //setnotsaved();
        }

        private void tbAZSHBXXT_TextChanged(object sender, EventArgs e)
        {
            ClassInputRestricter.CheckChange(sender);
            //setnotsaved();
        }

        private void tbFFGWKSL_TextChanged(object sender, EventArgs e)
        {
            ClassInputRestricter.CheckChange(sender);
            //setnotsaved();
        }

        private void tbZXGWKSL_TextChanged(object sender, EventArgs e)
        {
            ClassInputRestricter.CheckChange(sender);
            //setnotsaved();
        }

        private void tbGSYHFK_TextChanged(object sender, EventArgs e)
        {
            ClassInputRestricter.CheckChange(sender);
            //setnotsaved();
        }

        private void tbNYYHFK_TextChanged(object sender, EventArgs e)
        {
            ClassInputRestricter.CheckChange(sender);
            //setnotsaved();
        }

        private void tbZGYHFK_TextChanged(object sender, EventArgs e)
        {
            ClassInputRestricter.CheckChange(sender);
            //setnotsaved();
        }

        private void tbJSYHFK_TextChanged(object sender, EventArgs e)
        {
            ClassInputRestricter.CheckChange(sender);
            //setnotsaved();
        }

        private void tbQTYHFK_TextChanged(object sender, EventArgs e)
        {
            ClassInputRestricter.CheckChange(sender);
            //setnotsaved();
        }

        private void tbBJBSKTJDQ_TextChanged(object sender, EventArgs e)
        {
            ClassInputRestricter.CheckChange(sender);
            //setnotsaved();
        }

        private void tbBJBSKTJCS_TextChanged(object sender, EventArgs e)
        {
            ClassInputRestricter.CheckChange(sender);
            //setnotsaved();
        }

        private void tbAGDZFGGR_TextChanged(object sender, EventArgs e)
        {
            ClassInputRestricter.CheckChange(sender);
            //setnotsaved();
        }

        private void tbZNSYXJJS_TextChanged(object sender, EventArgs e)
        {
            ClassInputRestricter.CheckChange(sender);
            //setnotsaved();
        }

        private void tbZXZZDZXRW_TextChanged(object sender, EventArgs e)
        {
            ClassInputRestricter.CheckChange(sender);
            //setnotsaved();
        }

        private void tbQTQK_TextChanged(object sender, EventArgs e)
        {
            ClassInputRestricter.CheckChange(sender);
            //setnotsaved();
        }

        private void tbSKZFJE_TextChanged(object sender, EventArgs e)
        {
            ClassInputRestricter.CheckChange(sender);
            //setnotsaved();
        }

        private void tbXJZFJE_TextChanged(object sender, EventArgs e)
        {
            ClassInputRestricter.CheckChange(sender);
            //setnotsaved();
        }

        private void tbBJBSKTJDQ_KeyPress(object sender, KeyPressEventArgs e)
        {
            ClassInputRestricter.CheckInput(sender,e);
        }

        private void tbBJBSKTJCS_KeyPress(object sender, KeyPressEventArgs e)
        {
            ClassInputRestricter.CheckInput(sender,e);
        }

        private void tbAGDZFGGR_KeyPress(object sender, KeyPressEventArgs e)
        {
            ClassInputRestricter.CheckInput(sender, e);
        }

        private void tbZNSYXJJS_KeyPress(object sender, KeyPressEventArgs e)
        {
            ClassInputRestricter.CheckInput(sender, e);
        }

        private void tbZXZZDZXRW_KeyPress(object sender, KeyPressEventArgs e)
        {
            ClassInputRestricter.CheckInput(sender, e);
        }

        private void tbQTQK_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (e.KeyChar == '\r')
            {
                this.btnSave.Focus();
            }
        }

        private void tbSKZFJE_KeyPress(object sender, KeyPressEventArgs e)
        {
            ClassInputRestricter.CheckInput(sender, e);
        }

        private void tbXJZFJE_KeyPress(object sender, KeyPressEventArgs e)
        {
            ClassInputRestricter.CheckInput(sender, e);
        }

        private void tbBJBSKTJDQ_Leave(object sender, EventArgs e)
        {
            ClassInputRestricter.CheckFormat(sender);
        }

        private void tbBJBSKTJCS_Leave(object sender, EventArgs e)
        {
            ClassInputRestricter.CheckFormat(sender);
        }

        private void tbAGDZFGGR_Leave(object sender, EventArgs e)
        {
            ClassInputRestricter.CheckFormat(sender);
        }

        private void tbZNSYXJJS_Leave(object sender, EventArgs e)
        {
            ClassInputRestricter.CheckFormat(sender);
        }

        private void tbZXZZDZXRW_Leave(object sender, EventArgs e)
        {
            ClassInputRestricter.CheckFormat(sender);
        }

        private void tbQTQK_Leave(object sender, EventArgs e)
        {
            ClassInputRestricter.CheckFormat(sender);
        }

        private void tbSKZFJE_Leave(object sender, EventArgs e)
        {
            ClassInputRestricter.CheckFormat(sender);
        }

        private void tbXJZFJE_Leave(object sender, EventArgs e)
        {
            ClassInputRestricter.CheckFormat(sender);
        }

        private void tbQDFWXY_KeyPress(object sender, KeyPressEventArgs e)
        {
            ClassInputRestricter.EnterToTab(sender, e);
        }

        private void tbAZCWPOSJ_KeyPress(object sender, KeyPressEventArgs e)
        {
            ClassInputRestricter.EnterToTab(sender, e);
        }

        private void tbAZSHBXXT_KeyPress(object sender, KeyPressEventArgs e)
        {
            ClassInputRestricter.EnterToTab(sender, e);
        }

        private void tbFFGWKSL_KeyPress(object sender, KeyPressEventArgs e)
        {
            ClassInputRestricter.EnterToTab(sender, e);
        }

        private void tbQYGWKSL_KeyPress(object sender, KeyPressEventArgs e)
        {
            ClassInputRestricter.EnterToTab(sender, e);
        }

        private void tbZXGWKSL_KeyPress(object sender, KeyPressEventArgs e)
        {
            ClassInputRestricter.EnterToTab(sender, e);
        }

        private void tbGSYHFK_KeyPress(object sender, KeyPressEventArgs e)
        {
            ClassInputRestricter.EnterToTab(sender, e);
        }

        private void tbNYYHFK_KeyPress(object sender, KeyPressEventArgs e)
        {
            ClassInputRestricter.EnterToTab(sender, e);
        }

        private void tbZGYHFK_KeyPress(object sender, KeyPressEventArgs e)
        {
            ClassInputRestricter.EnterToTab(sender, e);
        }

        private void tbJSYHFK_KeyPress(object sender, KeyPressEventArgs e)
        {
            ClassInputRestricter.EnterToTab(sender, e);
        }

        private void tbQTYHFK_KeyPress(object sender, KeyPressEventArgs e)
        {
            ClassInputRestricter.EnterToTab(sender, e);
        }

        private void btnFilter_Click(object sender, EventArgs e)
        {
            Frm_select_card selectcard = new Frm_select_card();
            selectcard.ShowDialog();
            if (selectcard.DialogResult == DialogResult.OK)
            {
                filterStr = selectcard.GetSQL();
                GetAllDataRefreshGridView();
            }
        }

        private void FrmBCardUsageStatistics_FormClosing(object sender, FormClosingEventArgs e)
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

        private void FrmBCardUsageStatistics_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.Control && e.KeyCode == Keys.S)
            {
                btnSave_Click(sender, e);
            }
        }

        private void tbGSYHFK_Leave(object sender, EventArgs e)
        {
            ClassInputRestricter.CheckNull(sender);
        }

        private void tbNYYHFK_Leave(object sender, EventArgs e)
        {
            ClassInputRestricter.CheckNull(sender);
        }

        private void tbZGYHFK_Leave(object sender, EventArgs e)
        {
            ClassInputRestricter.CheckNull(sender);
        }

        private void tbJSYHFK_Leave(object sender, EventArgs e)
        {
            ClassInputRestricter.CheckNull(sender);
        }

        private void tbQTYHFK_Leave(object sender, EventArgs e)
        {
            ClassInputRestricter.CheckNull(sender);
        }

        private void tbQDFWXY_Leave(object sender, EventArgs e)
        {
            ClassInputRestricter.CheckNull(sender);
        }

        private void tbAZCWPOSJ_Leave(object sender, EventArgs e)
        {
            ClassInputRestricter.CheckNull(sender);
        }

        private void FrmBCardUsageStatistics_Load_1(object sender, EventArgs e)
        {

        }

        private void tbAZSHBXXT_Leave(object sender, EventArgs e)
        {
            ClassInputRestricter.CheckNull(sender);
        }

        private void tbFFGWKSL_Leave(object sender, EventArgs e)
        {
            ClassInputRestricter.CheckNull(sender);
        }

        private void tbQYGWKSL_Leave(object sender, EventArgs e)
        {
            ClassInputRestricter.CheckNull(sender);
        }

        private void tbZXGWKSL_Leave(object sender, EventArgs e)
        {
            ClassInputRestricter.CheckNull(sender);
        }

        private void tbJTYHFK_TextChanged(object sender, EventArgs e)
        {
            ClassInputRestricter.CheckChange(sender);
        }

        private void tbJTYHFK_Leave(object sender, EventArgs e)
        {
            ClassInputRestricter.CheckNull(sender);
        }

        private void tbJTYHFK_KeyPress(object sender, KeyPressEventArgs e)
        {
            ClassInputRestricter.EnterToTab(sender, e);
        }

        private void tbZZKHDXE_TextChanged(object sender, EventArgs e)
        {
            ClassInputRestricter.CheckChange(sender);
        }

        private void tbZZKHDXE_KeyPress(object sender, KeyPressEventArgs e)
        {
            ClassInputRestricter.CheckInput(sender, e);
        }

        private void tbZZKHDXE_Leave(object sender, EventArgs e)
        {
            ClassInputRestricter.CheckFormat(sender);
        }

        private void tbSKZFJE_KeyPress_1(object sender, KeyPressEventArgs e)
        {
            ClassInputRestricter.CheckInput(sender, e);
        }

        private void tbTXGWKJSDW_KeyPress(object sender, KeyPressEventArgs e)
        {
            ClassInputRestricter.CheckInput(sender, e);
        }

        private void tbDLHSDW_KeyPress(object sender, KeyPressEventArgs e)
        {
            ClassInputRestricter.CheckInput(sender, e);
        }

        private void tbDLHSDW_Leave(object sender, EventArgs e)
        {
            ClassInputRestricter.CheckNull(sender);
        }

        private void tbTXGWKJSDW_Leave(object sender, EventArgs e)
        {
            ClassInputRestricter.CheckNull(sender);
        }

        private void tbDLHSDW_TextChanged(object sender, EventArgs e)
        {
            ClassInputRestricter.CheckChange(sender);
        }

        private void tbTXGWKJSDW_TextChanged(object sender, EventArgs e)
        {
            ClassInputRestricter.CheckChange(sender);
        }

    }
}
