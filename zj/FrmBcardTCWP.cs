using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Data.OleDb;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Windows.Forms;

namespace zj
{
    public partial class FrmBcardTCWP : Form
    {
        public string filterStr = "";
        public string bsbj = "";
        public bool isok = false;
        int m_id, dwbs;
        string m_dwdm, m_lb, m_pm, m_ly, m_jldw,m_bz,m_bm,m_czfs,m_djlx,m_bmmc;
        string m_hqsj;
        double m_sl, m_dj, m_zz, m_kysl, m_kbxjz;
        private Boolean notsaved = false; //标识数据是否修改，默认没有修改
        private AccessHelper m_accessHelper = new AccessHelper();

        /// <summary>
        /// 要改
        /// </summary>
        private string SQL_Admin_Insert = "insert into t_lctc(dwdm,bmbs,lb,pm,ly,hqsj,sl,jldw,dj,zz,kysl,kbxjz,czfs,bz,djlx,wpbs)values(@DWDM, @BMBS, @LB, @PM, @LY, @HQSJ, @SL, @JLDW, @DJ,@ZZ,@KYSL,@KBXJZ,@CZFS,@BZ,@DJLX,@WPLX)";

        private string SQL_Admin_Update = "UPDATE  t_lctc SET dwdm=@DWDM,bmbs=@BMBS,lb=@LB,pm=@PM,ly=@LY,hqsj=@HQSJ,sl=@SL,jldw=@JLDW,dj=@DJ,zz=@ZZ,kysl=@KYSL,kbxjz=@KBXJZ,czfs=@CZFS,bz= @BZ,djlx=@DJLX WHERE ID=@ID";
        private string SQL_Admin_Delete = "DELETE FROM t_lctc WHERE ID=@ID";

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
        /// 保存前获取数据
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btnSave_Click(object sender, EventArgs e)
        {
            m_lb = cbLB.SelectedValue.ToString();
            m_pm = cbPM.Text.ToString();
            m_ly = cbLY.SelectedValue.ToString();
            m_hqsj=dtHQSJ.Text.Replace(",", "");
            m_sl = double.Parse(tbSL.Text);
            m_jldw =cbJLDW.SelectedValue.ToString();
            m_dj = double.Parse(tbDJ.Text);
            m_zz = double.Parse(tbZZ.Text);
            m_kysl = double.Parse(tbKYSL.Text);
            m_kbxjz = double.Parse(tbKBXJZ.Text);
            string bz = cbLY.SelectedValue.ToString();
            if (string.IsNullOrEmpty(bz))
            {
                m_bz = tbBZ.Text;
            }
            else
            {
                m_bz = bz;
            }

            m_dwdm = comboDWDM.SelectedValue.ToString();

            if (m_id != ClassConstants.JD_NOTSELECTED)
            {
                OleDbParameter[] parms = new OleDbParameter[] {
                    new OleDbParameter("@LB",OleDbType.VarChar),
                    new OleDbParameter("@PM",OleDbType.VarChar),
                    new OleDbParameter("@LY",OleDbType.VarChar),
                    new OleDbParameter("@HQSJ",OleDbType.VarChar),
                    new OleDbParameter("@SL",OleDbType.Double),
                    new OleDbParameter("@JLDW",OleDbType.VarChar),
                    new OleDbParameter("@DJ",OleDbType.VarChar),
                    new OleDbParameter("@ZZ",OleDbType.VarChar),
                    new OleDbParameter("@KYSL",OleDbType.VarChar),
                    new OleDbParameter("@KBXJZ",OleDbType.VarChar),
                    new OleDbParameter("@BZ",OleDbType.VarChar),
                    new OleDbParameter("@DWDM",OleDbType.VarChar)
                };
                parms[0].Value = m_lb;
                parms[1].Value = m_pm;
                parms[2].Value = m_ly;
                parms[3].Value = m_hqsj;
                parms[4].Value = m_sl;
                parms[5].Value = m_jldw;
                parms[6].Value = m_dj;
                parms[7].Value = m_zz;
                parms[8].Value = m_kysl;
                parms[9].Value = m_kbxjz;
                parms[10].Value = m_bz;
                parms[11].Value = m_dwdm;

                UpdateData(parms);
              //  GetAllDataRefreshGridView();
                MessageBox.Show("更新数据完毕！", "系统提示", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
            else
            {
                OleDbParameter[] parms = new OleDbParameter[] {
                    new OleDbParameter("@LB",OleDbType.VarChar),
                    new OleDbParameter("@PM",OleDbType.VarChar),
                    new OleDbParameter("@LY",OleDbType.VarChar),
                    new OleDbParameter("@HQSJ",OleDbType.VarChar),
                    new OleDbParameter("@SL",OleDbType.Double),
                    new OleDbParameter("@JLDW",OleDbType.VarChar),
                    new OleDbParameter("@DJ",OleDbType.VarChar),
                    new OleDbParameter("@ZZ",OleDbType.VarChar),
                    new OleDbParameter("@KYSL",OleDbType.VarChar),
                    new OleDbParameter("@KBXJZ",OleDbType.Integer),
                    new OleDbParameter("@BZ",OleDbType.Integer),
                    new OleDbParameter("@DWDM",OleDbType.Integer)
                };
                parms[0].Value = m_lb;
                parms[1].Value = m_pm;
                parms[2].Value = m_ly;
                parms[3].Value = m_hqsj;
                parms[4].Value = m_sl;
                parms[5].Value = m_jldw;
                parms[6].Value = m_dj;
                parms[7].Value = m_zz;
                parms[8].Value = m_kysl;
                parms[9].Value = m_kbxjz;
                parms[10].Value = m_bz;
                parms[11].Value = m_dwdm;

                InsertData(parms);
           //     GetAllDataRefreshGridView();
                MessageBox.Show("插入数据完毕！", "系统提示", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
            ClearControlData();
            ClearVariableData();
            setsaved();
        }

        private void button3_Click(object sender, EventArgs e)
        {
            DRMBXZ dr = new DRMBXZ();
            this.Close();
            dr.Show();
        }

        private void button2_Click(object sender, EventArgs e)
        {
            FrmBCardDRMX drmx = new FrmBCardDRMX();
            drmx.Show();
        }


        /// <summary>
        /// 新增
        /// </summary>
        /// <param name="parms"></param>
        public void InsertData(OleDbParameter[] parms)
        {
            m_accessHelper.ExcueteCommand(SQL_Admin_Insert, parms);
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
                  //  GetAllDataRefreshGridView();
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

        //private void btnExcel_Click(object sender, EventArgs e)
        //{
        //    DataTable dt = dataGridView.DataSource as DataTable;
        //    ExcelUI ExcelUI = new ExcelUI();
        //    ExcelUI.ExportExcelFormat(dt, "");
        //}

        //private void dataGridView_SelectionChanged(object sender, EventArgs e)
        //{
        //    if (dataGridView.SelectedRows.Count != 0 && dataGridView.SelectedRows[0].Cells[1].Value.ToString() != "")
        //    {
        //        //this.checkchagnge();
        //        ReadDataToVariables();
        //        LoadVariablesToControls();
        //    }
        //}

        public void LoadVariablesToControls()
        {
            comboDWDM.SelectedValue = m_dwdm;
           // textBox1.Text = m_bm;
            cbLB.Text = m_lb;
            cbPM.Text = m_pm;
            cbLY.Text = m_ly;
            dtHQSJ.Text = m_hqsj;
            tbSL.Text = m_sl.ToString();
            cbJLDW.Text = m_jldw;
            tbDJ.Text = m_dj.ToString();
            tbZZ.Text = m_zz.ToString();
            tbKYSL.Text = m_kysl.ToString();
            tbKBXJZ.Text = m_kbxjz.ToString();
            cbCZFS.Text = m_czfs;
            tbBZ.Text = m_bz;
            cbDJLX.Text = m_djlx;
        }
        /// <summary>
        /// 赋值
        /// </summary>
        //public void ReadDataToVariables()
        //{
        //    DataTable dt = this.GetDataByIndex(m_id);
        //    if (dt.Rows.Count > 0)
        //    {

        //    m_bm = dt.Rows[0]["bmbs"].ToString();
        //    m_lb = dt.Rows[0]["lb"].ToString();
        //    m_pm = dt.Rows[0]["pm"].ToString();
        //    m_ly = dt.Rows[0]["ly"].ToString();
        //    m_hqsj = dt.Rows[0]["hqsj"].ToString();
        //    m_sl = double.Parse( dt.Rows[0]["sl"].ToString());
        //    m_jldw = dt.Rows[0]["jldw"].ToString();
        //    m_dj =double.Parse( dt.Rows[0]["dj"].ToString());
        //    m_zz =double.Parse( dt.Rows[0]["zz"].ToString());
        //    m_kysl = double.Parse( dt.Rows[0]["kysl"].ToString());
        //    m_kbxjz =double.Parse( dt.Rows[0]["kbxjz"].ToString());
        //    m_czfs = dt.Rows[0]["czfs"].ToString();
        //    m_bz = dt.Rows[0]["bz"].ToString();
        //    m_dwdm = dt.Rows[0]["dwdm"].ToString();
        //    }

        //}
        /// <summary>
        /// 根据id获取一条往来款数据
        /// </summary>
        /// <param name="dataIndex"></param>
        /// <returns></returns>
        public DataTable GetDataByIndex(string dataIndex)
        {
            string tempSQL = "SELECT * FROM t_lctc WHERE ID="+ dataIndex + "" ;
            DataTable dt = m_accessHelper.getDataSet(tempSQL).Tables[0];
            return dt;
        }

        private void tbSL_TextChanged(object sender, EventArgs e)
        {
            if(!string.IsNullOrEmpty(tbSL.Text.ToString())&& !string.IsNullOrEmpty(tbDJ.Text.ToString()))
            {
                double sl = double.Parse(tbSL.Text.ToString());
                double dj = double.Parse(tbDJ.Text.ToString());
                tbZZ.Text = (sl * dj).ToString();
            }
            
        }

        private void label9_Click(object sender, EventArgs e)
        {

        }

        private void FrmBcardTCWP_Load_1(object sender, EventArgs e)
        {

        }
        public bool ISSZ(string a)
        {
            if (SelcetFrmWP.dqzt == "2")
            {
                return true;
            }
            if (a == "0")
            {
                return true;
            }
           // Regex reg = new Regex("^[0-9]+$");
            Regex reg = new Regex(@"^[0-9]+[.]?[0-9]+$");
            Match ma = reg.Match(a);
            if (ma.Success)
            {
                return true;
            }
            else
            {
              return false;
            }
        }
        public int SubstringCount(string str,string substring)
        {
            if (str.Contains(substring))
            {
                string strr = str.Replace(substring, "");
                return (str.Length - strr.Length) / substring.Length;
            }
            return 0;
        }

        private void btnSave_Click_2(object sender, EventArgs e)
        {
            if(!string.IsNullOrEmpty(comboDWDM.Text.ToString()))
            {
                m_dwdm = comboDWDM.SelectedValue.ToString();

            }
            else
            {
                MessageBox.Show("请先选择单位！", "系统提示", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }
            m_bm = BMTree.tree_bmbs;
            m_bmmc = textBox1.Text;
            m_lb = cbLB.Text;
            m_pm = cbPM.Text;
            m_ly = cbLY.Text;
            m_hqsj = dtHQSJ.Text;
            m_sl = double.Parse(tbSL.Text.ToString());
            m_jldw = cbJLDW.Text;
            m_dj = double.Parse(tbDJ.Text.ToString());
            m_zz = double.Parse(tbZZ.Text.ToString());
            m_kysl = double.Parse(tbKYSL.Text.ToString());
            m_kbxjz = double.Parse(tbKBXJZ.Text.ToString());
            m_czfs = cbCZFS.Text.ToString();
            m_bz = tbBZ.Text;
            m_djlx = cbDJLX.Text;

            if(textBox1.Text==""|| m_lb==""|| m_lb==""|| m_pm=="" || m_ly == ""|| m_hqsj =="" || m_djlx == "" || m_jldw == "")
            {
                MessageBox.Show("信息录入不完整！", "系统提示", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }
            if (m_kysl > m_sl)
            {
                MessageBox.Show("堪用数量应小于等于数量！", "系统提示", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }



            if (SelcetFrmWP.dqzt=="2")
            {
                
                OleDbParameter[] parms = new OleDbParameter[] {
                    new OleDbParameter("@DWDM",OleDbType.VarChar),
                    new OleDbParameter("@BMBS",OleDbType.VarChar),
                    new OleDbParameter("@LB",OleDbType.VarChar),
                    new OleDbParameter("@PM",OleDbType.VarChar),
                    new OleDbParameter("@LY",OleDbType.VarChar),
                    new OleDbParameter("@HQSJ",OleDbType.VarChar),
                    new OleDbParameter("@SL",OleDbType.VarChar),
                    new OleDbParameter("@JLDW",OleDbType.VarChar),
                    new OleDbParameter("@DJ",OleDbType.VarChar),
                    new OleDbParameter("@ZZ",OleDbType.VarChar),
                    new OleDbParameter("@KYSL",OleDbType.VarChar),
                    new OleDbParameter("@KBXJZ",OleDbType.VarChar),
                    new OleDbParameter("@CZFS",OleDbType.VarChar),
                    new OleDbParameter("@BZ",OleDbType.VarChar),
                    new OleDbParameter("@DJLX",OleDbType.VarChar),
                    new OleDbParameter("@ID",OleDbType.VarChar)
                };
                parms[0].Value = m_dwdm;
                parms[1].Value = m_bm;
                parms[2].Value = m_lb;
                parms[3].Value = m_pm;
                parms[4].Value = m_ly;
                parms[5].Value = m_hqsj;
                parms[6].Value = m_sl;
                parms[7].Value = m_jldw;
                parms[8].Value = m_dj;
                parms[9].Value = m_zz;
                parms[10].Value = m_kysl;
                parms[11].Value = m_kbxjz;
                parms[12].Value = m_czfs;
                parms[13].Value = m_bz;
                parms[14].Value = m_djlx;
                parms[15].Value = m_id;
                UpdateData(parms);
                // GetAllDataRefreshGridView();
                MessageBox.Show("更新数据完毕！", "系统提示", MessageBoxButtons.OK, MessageBoxIcon.Information);
                this.Close();
            }
            else
            {
                OleDbParameter[] parms = new OleDbParameter[] {
                    new OleDbParameter("@DWDM",OleDbType.VarChar),
                    new OleDbParameter("@BMBS",OleDbType.VarChar),
                    new OleDbParameter("@LB",OleDbType.VarChar),
                    new OleDbParameter("@PM",OleDbType.VarChar),
                    new OleDbParameter("@LY",OleDbType.VarChar),
                    new OleDbParameter("@HQSJ",OleDbType.VarChar),
                    new OleDbParameter("@SL",OleDbType.VarChar),
                    new OleDbParameter("@JLDW",OleDbType.VarChar),
                    new OleDbParameter("@DJ",OleDbType.VarChar),
                    new OleDbParameter("@ZZ",OleDbType.VarChar),
                    new OleDbParameter("@KYSL",OleDbType.VarChar),
                    new OleDbParameter("@KBXJZ",OleDbType.VarChar),
                    new OleDbParameter("@CZFS",OleDbType.VarChar),
                    new OleDbParameter("@BZ",OleDbType.VarChar),
                    new OleDbParameter("@DJLX",OleDbType.VarChar),
                    new OleDbParameter("@WPBS",OleDbType.VarChar)

                };
                parms[0].Value = m_dwdm;
                parms[1].Value = m_bm;
                parms[2].Value = m_lb;
                parms[3].Value = m_pm;
                parms[4].Value = m_ly;
                parms[5].Value = m_hqsj;
                parms[6].Value = m_sl;
                parms[7].Value = m_jldw;
                parms[8].Value = m_dj;
                parms[9].Value = m_zz;
                parms[10].Value = m_kysl;
                parms[11].Value = m_kbxjz;
                parms[12].Value = m_czfs;
                parms[13].Value = m_bz;
                parms[14].Value = m_djlx;
                parms[15].Value = Guid.NewGuid().ToString();

                InsertData(parms);
                // GetAllDataRefreshGridView();
                MessageBox.Show("插入数据完毕！", "系统提示", MessageBoxButtons.OK, MessageBoxIcon.Information);
                this.Close();
                //FrmBcardTCWP tcwp = new FrmBcardTCWP();
                //tcwp.Show();
            }
            //ClearControlData();
            //ClearVariableData();
            setsaved();
            isok = true;
        }

        private void button1_Click_1(object sender, EventArgs e)
        {
            this.Close();
        }

        private void tbSL_TextChanged_1(object sender, EventArgs e)
        {
            if (!string.IsNullOrEmpty(tbSL.Text.ToString()) && !string.IsNullOrEmpty(tbDJ.Text.ToString()))
            {
                try
                {
                    double sl = double.Parse(tbSL.Text.ToString());
                    double dj = double.Parse(tbDJ.Text.ToString());
                    tbZZ.Text = (sl * dj).ToString();
                }
                catch
                {
                    MessageBox.Show("输入格式不是数字类型！", "系统提示", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    tbSL.Text = "0.00";
                }
            }
        }

        private void tbKYSL_TextChanged(object sender, EventArgs e)
        {
            //if (!ISSZ(tbKYSL.Text.ToString()) && tbKYSL.Text.ToString() != "0.00")
            //{
            //    MessageBox.Show("输入格式不是数字类型！", "系统提示", MessageBoxButtons.OK, MessageBoxIcon.Error);
            //    tbKYSL.Text = "0.00";
            //    return;
            //}
        }

        private void tbKBXJZ_TextChanged(object sender, EventArgs e)
        {
            //if (!ISSZ(tbKBXJZ.Text.ToString()) && tbKBXJZ.Text.ToString() != "0.00")
            //{
            //    MessageBox.Show("输入格式不是数字类型！", "系统提示", MessageBoxButtons.OK, MessageBoxIcon.Error);
            //    tbKBXJZ.Text = "0.00";
            //    return;
            //}
        }

        private void cbLB_SelectedValueChanged(object sender, EventArgs e)
        {
            string smc = cbLB.Text;
            if (!string.IsNullOrEmpty(smc))
            {
                string sql = string.Empty;
                if (smc == "其他" || smc == "日用品")
                {
                     sql = "select distinct mc from t_tcwpzdb where lb='计量单位' ";
                }
                else
                {
                    sql = "select mc from t_tcwpzdb where lb='计量单位' and fjbh='" + smc + "' order by ID";
                }
                DataTable dt = m_accessHelper.getDataSet(sql).Tables[0];

                cbJLDW.DataSource = dt;
                cbJLDW.ValueMember = "mc";
                cbJLDW.DisplayMember = "mc";
            }
        }

        private void textBox1_Click(object sender, EventArgs e)
        {
            if (comboDWDM.Text != "")
            {
                SelcetFrmWP.dwdm_tree = comboDWDM.SelectedValue.ToString();
                BMTree bt = new BMTree();
                bt.ShowDialog();
                string bmbs_tree = bt.GetSQL();
                string sql_bmmc = "select bmmc from t_bm where bmbs='" + bmbs_tree + "'";
                DataTable dt = m_accessHelper.getDataSet(sql_bmmc).Tables[0];
                if (dt.Rows.Count > 0)
                {
                    textBox1.Text = dt.Rows[0]["bmmc"].ToString();

                }
            }
            else
            {
                MessageBox.Show("请先选择单位！", "系统提示", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }
           
        }

        private void tbDJ_TextChanged_1(object sender, EventArgs e)
        {
            if (!string.IsNullOrEmpty(tbSL.Text.ToString()) && !string.IsNullOrEmpty(tbDJ.Text.ToString()))
            {
                try
                {
                    double sl = double.Parse(tbSL.Text.ToString());
                    double dj = double.Parse(tbDJ.Text.ToString());
                    tbZZ.Text = (sl * dj).ToString();
                }
                catch
                {
                    MessageBox.Show("输入格式不是数字类型！", "系统提示", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    tbDJ.Text = "0.00";
                }
            }
            //if (!ISSZ(tbDJ.Text.ToString()) && tbDJ.Text.ToString() != "0.00")
            //{
            //    MessageBox.Show("输入格式不是数字类型！", "系统提示", MessageBoxButtons.OK, MessageBoxIcon.Error);
            //    tbDJ.Text = "0.00";
            //    return;
            //}
            //double sl = double.Parse(tbSL.Text.ToString());
            //double dj = double.Parse(tbDJ.Text.ToString());
            //tbZZ.Text = (sl * dj).ToString();
        }

        private void tbDJ_TextChanged(object sender, EventArgs e)
        {
            double sl = double.Parse(tbSL.Text.ToString());
            double dj = double.Parse(tbDJ.Text.ToString());
            tbZZ.Text = (sl * dj).ToString();
        }

        private void cbLB_SelectedIndexChanged(object sender, EventArgs e)
        {
            //string wplb = cbLB.Text.ToString();

            //if (!string.IsNullOrEmpty(wplb))
            //{
            //    string tempSQL = "select ID,mc from t_tcwpzdb where lb='计量单位' and fjbh='" + wplb + "'";
            //    DataSet ds = m_accessHelper.getDataSet(tempSQL);

            //    cbJLDW.DataSource = ds.Tables[0];
            //    cbJLDW.ValueMember = "mc";
            //    cbJLDW.DisplayMember = "mc";
            //}
        }

        private void btnSave_Click_1(object sender, EventArgs e)
        {
            m_dwdm = comboDWDM.SelectedValue.ToString();
            m_bm = BMTree.tree_bmbs;
            m_lb = cbLB.Text;
            m_pm = cbPM.Text;
            m_ly = cbLY.Text;
            m_hqsj = dtHQSJ.Text;
            m_sl =double.Parse(tbSL.Text.ToString());
            m_jldw = cbJLDW.Text;
            m_dj = double.Parse(tbDJ.Text.ToString());
            m_zz = double.Parse(tbZZ.Text.ToString());
            m_kysl = double.Parse(tbKYSL.Text.ToString());
            m_kbxjz = double.Parse(tbKBXJZ.Text.ToString());
            m_czfs = cbCZFS.Text.ToString();
            m_bz = tbBZ.Text;

            if (m_kysl > m_sl)
            {
                MessageBox.Show("堪用数量应小于等于数量！", "系统提示", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }
            


            if (m_id != ClassConstants.JD_NOTSELECTED)
            {

                OleDbParameter[] parms = new OleDbParameter[] {
                    new OleDbParameter("@DWDM",OleDbType.VarChar),
                    new OleDbParameter("@BMBS",OleDbType.VarChar),
                    new OleDbParameter("@LB",OleDbType.VarChar),
                    new OleDbParameter("@PM",OleDbType.VarChar),
                    new OleDbParameter("@LY",OleDbType.VarChar),
                    new OleDbParameter("@HQSJ",OleDbType.VarChar),
                    new OleDbParameter("@SL",OleDbType.VarChar),
                    new OleDbParameter("@JLDW",OleDbType.VarChar),
                    new OleDbParameter("@DJ",OleDbType.VarChar),
                    new OleDbParameter("@ZZ",OleDbType.VarChar),
                    new OleDbParameter("@KYSL",OleDbType.VarChar),
                    new OleDbParameter("@KBXJZ",OleDbType.VarChar),
                    new OleDbParameter("@CZFS",OleDbType.VarChar),
                    new OleDbParameter("@BZ",OleDbType.VarChar),
                    new OleDbParameter("@ID",OleDbType.VarChar),
                };
                parms[0].Value = m_dwdm;
                parms[1].Value = m_bm;
                parms[2].Value = m_lb ;
                parms[3].Value = m_pm;
                parms[4].Value = m_ly ;
                parms[5].Value = m_hqsj;
                parms[6].Value = m_sl;
                parms[7].Value = m_jldw;
                parms[8].Value = m_dj;
                parms[9].Value = m_zz;
                parms[10].Value = m_kysl;
                parms[11].Value = m_kbxjz;
                parms[12].Value = m_czfs;
                parms[13].Value =  m_bz  ;
                parms[14].Value =  m_id;
                UpdateData(parms);
               // GetAllDataRefreshGridView();
                MessageBox.Show("更新数据完毕！", "系统提示", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
            else
            {
                OleDbParameter[] parms = new OleDbParameter[] {
                    new OleDbParameter("@DWDM",OleDbType.VarChar),
                    new OleDbParameter("@BMBS",OleDbType.VarChar),
                    new OleDbParameter("@LB",OleDbType.VarChar),
                    new OleDbParameter("@PM",OleDbType.VarChar),
                    new OleDbParameter("@LY",OleDbType.VarChar),
                    new OleDbParameter("@HQSJ",OleDbType.VarChar),
                    new OleDbParameter("@SL",OleDbType.VarChar),
                    new OleDbParameter("@JLDW",OleDbType.VarChar),
                    new OleDbParameter("@DJ",OleDbType.VarChar),
                    new OleDbParameter("@ZZ",OleDbType.VarChar),
                    new OleDbParameter("@KYSL",OleDbType.VarChar),
                    new OleDbParameter("@KBXJZ",OleDbType.VarChar),
                    new OleDbParameter("@CZFS",OleDbType.VarChar),
                    new OleDbParameter("@BZ",OleDbType.VarChar),
                };
                parms[0].Value = m_dwdm;
                parms[1].Value = m_bm;
                parms[2].Value = m_lb;
                parms[3].Value = m_pm;
                parms[4].Value = m_ly;
                parms[5].Value = m_hqsj;
                parms[6].Value = m_sl;
                parms[7].Value = m_jldw;
                parms[8].Value = m_dj;
                parms[9].Value = m_zz;
                parms[10].Value = m_kysl;
                parms[11].Value = m_kbxjz;
                parms[12].Value = m_czfs;
                parms[13].Value = m_bz;

                InsertData(parms);
               // GetAllDataRefreshGridView();
                MessageBox.Show("插入数据完毕！", "系统提示", MessageBoxButtons.OK, MessageBoxIcon.Information);
                //this.Close();
                //FrmBcardTCWP tcwp = new FrmBcardTCWP();
                //tcwp.Show();
            }
            //ClearControlData();
            //ClearVariableData();
            setsaved();
        }

        /// <summary>
        /// 修改
        /// </summary>
        /// <param name="parms"></param>
        public void UpdateData(OleDbParameter[] parms)
        {
            m_accessHelper.ExcueteCommand(SQL_Admin_Update, parms);
        }

        public FrmBcardTCWP()
        {
            InitializeComponent();
            // this.WindowState = FormWindowState.Maximized;
            Load += new EventHandler(FrmBcardTCWP_Load);
           
        }

      
        private void btnFilter_Click(object sender, EventArgs e)
        {

        }

        /// <summary>
        /// 页面加载方法
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void FrmBcardTCWP_Load(object sender, EventArgs e)
        {
            
            LoadComboParams();
            if (dwbs != 1)
            {
                string id = SelcetFrmWP.selectid.ToString();
              string zt = SelcetFrmWP.dqzt.ToString();
                ClearVariableData();
                ClearControlData();

                if (zt == "0")//查看
                {
                    DataTable dt = this.GetDataByIndex(id);
                    if (dt.Rows.Count > 0)
                    {
                        comboDWDM.SelectedValue= dt.Rows[0]["dwdm"].ToString();
                        string bmbs_tree = dt.Rows[0]["bmbs"].ToString();
                        string sql_bmmc = "select bmmc from t_bm where bmbs='" + bmbs_tree + "'";
                        DataTable dt1 = m_accessHelper.getDataSet(sql_bmmc).Tables[0];
                        if (dt1.Rows.Count > 0)
                        {
                            textBox1.Text = dt1.Rows[0]["bmmc"].ToString();

                        }
                        m_bm = dt.Rows[0]["bmbs"].ToString();
                        m_lb = dt.Rows[0]["lb"].ToString();
                        m_pm = dt.Rows[0]["pm"].ToString();
                        m_ly = dt.Rows[0]["ly"].ToString();
                        m_hqsj = dt.Rows[0]["hqsj"].ToString();
                        m_sl = double.Parse(dt.Rows[0]["sl"].ToString());
                        m_jldw = dt.Rows[0]["jldw"].ToString();
                        m_dj = double.Parse(dt.Rows[0]["dj"].ToString());
                        m_zz = double.Parse(dt.Rows[0]["zz"].ToString());
                        m_kysl = double.Parse(dt.Rows[0]["kysl"].ToString());
                        m_kbxjz = double.Parse(dt.Rows[0]["kbxjz"].ToString());
                        m_czfs = dt.Rows[0]["czfs"].ToString();
                        m_bz = dt.Rows[0]["bz"].ToString();
                        m_dwdm = dt.Rows[0]["dwdm"].ToString();
                        m_djlx= dt.Rows[0]["djlx"].ToString();
                        
                    }
                    this.comboDWDM.Enabled = false;
                    textBox1.Enabled = false;
                    cbLB.Enabled =false;
                    cbPM.Enabled = false;
                    cbLY.Enabled =false;
                    dtHQSJ.Enabled=false;
                    tbSL.Enabled = false;
                    cbJLDW.Enabled=false;
                    tbDJ.Enabled = false;
                    tbZZ.Enabled = false;
                    tbKYSL.Enabled = false; ;
                    tbKBXJZ.Enabled = false;
                    cbCZFS.Enabled = false;
                    tbBZ.Enabled = false;
                    tbBZ.Enabled = false;
                    btnSave.Visible = false;
                    cbDJLX.Enabled = false;
                }

                if (zt == "2")//编辑
                {
                    DataTable dt = this.GetDataByIndex(id);
                    if (dt.Rows.Count > 0)
                    {

                        m_bm = dt.Rows[0]["bmbs"].ToString();
                        string bmbs_tree = dt.Rows[0]["bmbs"].ToString();
                        BMTree.tree_bmbs= dt.Rows[0]["bmbs"].ToString();
                        string sql_bmmc = "select bmmc from t_bm where bmbs='" + bmbs_tree + "'";
                        DataTable dt1 = m_accessHelper.getDataSet(sql_bmmc).Tables[0];
                        if (dt1.Rows.Count > 0)
                        {
                            textBox1.Text = dt1.Rows[0]["bmmc"].ToString();
                        }
                        m_lb = dt.Rows[0]["lb"].ToString();
                        m_pm = dt.Rows[0]["pm"].ToString();
                        m_ly = dt.Rows[0]["ly"].ToString();
                        m_hqsj = dt.Rows[0]["hqsj"].ToString();
                        m_sl = double.Parse(dt.Rows[0]["sl"].ToString() == "" ? "0" : dt.Rows[0]["sl"].ToString());
                        m_jldw = dt.Rows[0]["jldw"].ToString();
                        m_dj = double.Parse(dt.Rows[0]["dj"].ToString()==""?"0": dt.Rows[0]["dj"].ToString());
                        m_zz = double.Parse(dt.Rows[0]["zz"].ToString() == "" ? "0" : dt.Rows[0]["zz"].ToString());
                        m_kysl = double.Parse(dt.Rows[0]["kysl"].ToString() == "" ? "0" : dt.Rows[0]["kysl"].ToString());
                        m_kbxjz = double.Parse(dt.Rows[0]["kbxjz"].ToString() == "" ? "0" : dt.Rows[0]["kbxjz"].ToString());
                        m_czfs = dt.Rows[0]["czfs"].ToString();
                        m_bz = dt.Rows[0]["bz"].ToString();
                        m_dwdm = dt.Rows[0]["dwdm"].ToString();
                        m_djlx = dt.Rows[0]["djlx"].ToString();
                        m_id = Convert.ToInt32( dt.Rows[0]["id"].ToString());
                    }
                    if (dt.Rows[0]["dwdm"].ToString() != "000")
                    {
                        this.comboDWDM.Enabled = false;
                        textBox1.Enabled = false;
                        cbLB.Enabled = false;
                        cbPM.Enabled = false;
                        cbLY.Enabled = false;
                        dtHQSJ.Enabled = false;
                        tbSL.Enabled = false;
                        cbJLDW.Enabled = false;
                        tbDJ.Enabled = false;
                        tbZZ.Enabled = false;
                        tbKYSL.Enabled = false; ;
                        tbKBXJZ.Enabled = false;
                        cbCZFS.Enabled = true;
                        tbBZ.Enabled = false;
                        tbBZ.Enabled = false;
                        cbDJLX.Enabled = false;
                    }
                    else
                    {
                        this.comboDWDM.Enabled = false;
                    }
                }

                

                LoadVariablesToControls();

                //Load0therDta_Bydw();
            }
           
        }

        /// <summary>
        /// 单位赋值
        /// </summary>
        private void LoadComboParams()
        {
            string tempSQL = "";
            if (SelcetFrmWP.dqzt.ToString() == "1")
            {
                 tempSQL = "SELECT dwdm,dwmc FROM t_dwxx where dwdm='000' ";
            }
            else
            {
                tempSQL = "SELECT dwdm,dwmc FROM t_dwxx ";
            }
          
            DataSet ds = m_accessHelper.getDataSet(tempSQL);

            comboDWDM.DataSource = ds.Tables[0];
            if (ds.Tables[0].Rows.Count == 0)
            {
                MessageBox.Show("请先录入单位信息！", "系统提示", MessageBoxButtons.OK, MessageBoxIcon.Information);
                dwbs = 1;
                Application.Exit();
            }
            else
            {
                dwbs = 0;
                comboDWDM.ValueMember = "dwdm";
                comboDWDM.DisplayMember = "dwmc";
            }
          
        }



        private void tbBZ_TextChanged(object sender, EventArgs e)
        {

        }

        /// <summary>
        /// 清理变量值
        /// </summary>
        private void ClearVariableData()
        {
            m_id=ClassConstants.JD_NOTSELECTED;
            m_lb= "";
            m_pm = string.Empty;
            m_ly = string.Empty;
            m_jldw = string.Empty;
            m_bz = string.Empty;
            DateTime m_hqsj = DateTime.Now;
            m_sl = 0.00;
            m_dj = 0.00;
            m_zz = 0.00;
            m_kysl = 0.00;
            m_czfs = "";
            m_kbxjz = 0.00;
            if (dwbs != 1)
            {
                m_dwdm = comboDWDM.Items[0].ToString();
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

            cbLB.Text = "香烟";
            cbPM.Text = "";
            cbLY.Text = cbLY.Items[0].ToString();
            dtHQSJ.Text ="";
            tbSL.Text = "0.00";
            cbJLDW.Text = "";
            tbDJ.Text = "0.00";
            tbZZ.Text = "0.00";
            cbCZFS.Text = "";
            tbKYSL.Text = "0.00";
            tbKBXJZ.Text = "0.00";
            tbKBXJZ.Text = "0.00";
            tbBZ.Text = string.Empty;
        }

        ///// <summary>
        ///// 获取名贵物品明细信息
        ///// </summary>
        //public void GetAllDataRefreshGridView(string id)
        //{

        //    string tempSQL = "SELECT t_lctc.ID as ID号, t_dwxx.dwmc as 单位名称, t_lctc.lb as 类别, t_lctc.pm as 品名, t_lctc.ly as 来源,t_lctc.hqsj as 获取时间,  t_lctc.sl as 数量,  t_lctc.jldw as 计量单位,  t_lctc.dj as 单价, t_lctc.zz as 总值, t_lctc.kysl as 堪用数量,t_lctc.kbxjz as 可变现价值, t_lctc.bz as 备注 FROM t_dwxx, t_lctc WHERE t_lctc.dwdm = t_dwxx.dwdm and id="+id;

        //    DataSet ds = m_accessHelper.getDataSet(tempSQL);
        //    DataTable dt = ds.Tables[0];
        //    DataColumn dc = dt.Columns.Add("序号", typeof(int));
        //    dt.Columns["序号"].SetOrdinal(0);
        //    for (int i = 0; i < dt.Rows.Count; i++)
        //    {
        //        dt.Rows[i][0] = i + 1;
        //    }
        //    dataGridView.DataSource = dt;
        //    dataGridView.Columns[1].Visible = false;
        //    dataGridView.ClearSelection();
        //    dataGridView.Columns["ID号"].Width = 60;
        //    dataGridView.Columns["单位名称"].Width = 200;
        //    dataGridView.Columns["备注"].Width = 320;
        //    if (dataGridView.SelectedRows.Count != 0)
        //    {
        //        m_id = int.Parse(dataGridView.SelectedRows[0].Cells[1].Value.ToString());
        //    }
        //    else
        //    {
        //        m_id = ClassConstants.JD_NOTSELECTED;
        //    }
        //}
    }
}
