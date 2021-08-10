using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Linq;
using System.Drawing;
using System.Text;
using System.Windows.Forms;
using System.Data.OleDb;


namespace zj
{
    public partial class FrmBankAccountStatistics : Form
    {
        int m_id ,dwbs;
        double m_ckye;
        string m_dwfzr, m_dwfzrlxdh, m_khhlxdh, m_zh, m_zhmc, m_khh, m_khhlxr, m_pzkhhzh, m_dwdm,m_bzyy,m_bzyymx;
        string m_clyj,m_hb,m_szss,m_zhlb,m_zhxz;
        string m_khsj,m_pzchsj,m_pzkhsj;
        private string SQL_Admin_Update = "UPDATE t_yhzh SET dwdm=@DWDM,zhmc=@ZHMC,zh=@ZH,zhlb=@ZHLB,zhxz=@ZHXZ,hb=@HB,khh=@KHH,khhlxr=@KHHLXR,khhlxdh=@KHHLXDH,khsj=@KHSJ,pzkhsj=@PZKHSJ,pzkhhzh=@PZKHHZH,pzchsj=@PZCHSJ,dwfzr=@DWFZR,dwfzrlxdh=@DWFZRLXDH,szss=@SZSS,ckye=@CKYE,clyj=@CLYJ,bzyy=@BZYY,bzyymx=@BZYYMX WHERE ID=@ID";
        private string SQL_Admin_Insert = "INSERT INTO t_yhzh(dwdm,zhmc,zh,zhlb,zhxz,hb,khh,khhlxr,khhlxdh,khsj,pzkhsj,pzkhhzh,pzchsj,dwfzr,dwfzrlxdh,szss,ckye,clyj,bzyy,bzyymx) values(@DWDM,@ZHMC,@ZH,@ZHLB,@ZHXZ,@HB,@KHH,@KHHLXR,@KHHLXDH,@KHSJ,@PZKHSJ,@PZKHHZH,@PZCHSJ,@DWFZR,@DWFZRLXDH,@SZSS,@CKYE,@CLYJ,@BZYY,@BZYYMX)";
        private string SQL_Admin_Delete = "DELETE FROM t_yhzh WHERE ID=@ID";
        public String filterStr = "";
        private Boolean notsaved = false; //标识数据是否修改，默认没有修改
        private AccessHelper m_accessHelper = new AccessHelper();

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
            string tempSQL = "SELECT * FROM t_yhzh WHERE ID=" + dataIndex;
            DataTable dt = m_accessHelper.getDataSet(tempSQL).Tables[0];
            return dt;
        }

        public FrmBankAccountStatistics()
        {
            InitializeComponent();
            Load += new EventHandler(FrmBankAccountStatistics_Load);
        }

        void FrmBankAccountStatistics_Load(object sender, EventArgs e)
        {
            LoadComboParams();
            ClearVariableData();
            ClearControlData();
            GetAllDataRefreshGridView();

            DateTime minDate = new DateTime(1950, 1, 1);
            DateTime maxDate = new DateTime(2019, 6, 30);
            dtpKHSJ.MinDate = minDate;
            dtpKHSJ.MaxDate = maxDate;
            dtpPZKHSJ.MinDate = minDate;
            dtpPZKHSJ.MaxDate = maxDate;
            dtpPZCHSJ.MinDate = minDate;
        }
        //增加时清除控件值
        private void ClearControlData()
        {
            tbCKYE.Text = "0";
            tbDWFZR.Text = "";
            tbDWFZRLXDH.Text = "";
            tbKHHLXDH.Text = "";
            tbZH.Text = "";
            tbZHMC.Text = "";
            tbKHH.Text = "";
            tbKHHLXR.Text = "";
            tbPZKHHZH_4.Text = "";
            textBoxPZKHHZH_3.Text = "";
            textboxBZYYMX.Text = "";

            comboCLYJ.SelectedIndex = -1;

            if (dwbs != 1)
            {
                comboDWDM.SelectedIndex = 0;
            }
            comboHB.SelectedIndex = -1;
            comboSZSS.SelectedIndex = -1;
            comboZHLB.SelectedIndex = -1;
            comboZHXZ.SelectedIndex = -1;
            comboBoxPZKHHZH_1.SelectedIndex = -1;
            comboBoxPZKHHZH_2.SelectedIndex = -1;
            comboBZYY.SelectedIndex = -1;

            DateTime maxDate = new DateTime(2019, 6, 30);
            dtpKHSJ.Value = maxDate;
            dtpPZKHSJ.Value = maxDate;
            dtpPZCHSJ.Value = new DateTime(2020, 12, 31);


        }
        //清除系统中间变量
        private void ClearVariableData()
        {
            m_clyj = comboCLYJ.Items[0].ToString();
            if (dwbs != 1)
            {
                m_dwdm = comboDWDM.Items[0].ToString();
            }
            m_hb = comboHB.Items[0].ToString();
            m_szss = comboSZSS.Items[0].ToString();
            m_zhlb = comboZHLB.Items[0].ToString();
            m_zhxz = comboZHXZ.Items[0].ToString();

            DateTime maxDate = new DateTime(2019, 6, 30);
            m_khsj = maxDate.ToString();
            m_pzkhsj = maxDate.ToString();
            m_pzchsj = new DateTime(2020, 12, 31).ToString();


            m_ckye = 0;
            m_dwfzr = "";
            m_dwfzrlxdh = "";
            m_khh = "";
            m_khhlxdh = "";
            m_khhlxr = "";
            m_zh = "";
            m_zhmc = "";
            m_pzkhhzh = "";
            m_bzyy = "";
            m_bzyymx = "";
            
            m_id = ClassConstants.JD_NOTSELECTED;
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
        //设置默认值
        public void setControl()
        {
            tbDWFZR.Text = m_dwfzr;
            tbZHMC.Text = m_zhmc;
            tbDWFZR.Text = m_dwfzr;
            tbDWFZRLXDH.Text = m_dwfzrlxdh;
            tbKHHLXR.Text = m_khhlxr;
            tbKHHLXDH.Text = m_khhlxdh;
            tbKHH.Text = m_khh;
            textboxBZYYMX.Text = m_bzyymx;
        }

        /// <summary>
        /// 增加
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
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

        private bool CheckInputEmpty()
        {
            bool result = true;
            if (comboZHLB.SelectedIndex < 0)
            {
                MessageBox.Show("账户类别不能为空！", "系统提示", MessageBoxButtons.OK, MessageBoxIcon.Error);
                comboZHLB.Focus();
                //comboZHLB.BackColor = Color.Yellow;
                result = false;
            }
            else if (comboZHXZ.SelectedIndex < 0)
            {
                MessageBox.Show("账户性质不能为空！", "系统提示", MessageBoxButtons.OK, MessageBoxIcon.Error);
                comboZHXZ.Focus();
                result = false;
            }
            else if (tbZHMC.Text.Length == 0)
            {
                MessageBox.Show("账户名称不能为空！", "系统提示",MessageBoxButtons.OK,MessageBoxIcon.Error);
                tbZHMC.Focus();
                result = false;
            }
            else if (tbZH.Text.Length == 0)
            {
                MessageBox.Show("账号不能为空！", "系统提示", MessageBoxButtons.OK, MessageBoxIcon.Error);
                tbZH.Focus();
                result = false;
            }
            else if (comboSZSS.SelectedIndex < 0)
            {
                MessageBox.Show("所在省市不能为空！", "系统提示", MessageBoxButtons.OK, MessageBoxIcon.Error);
                comboSZSS.Focus();
                result = false;
            }
            else if (comboHB.SelectedIndex < 0)
            {
                MessageBox.Show("行别不能为空！", "系统提示", MessageBoxButtons.OK, MessageBoxIcon.Error);
                comboHB.Focus();
                result = false;
            }
            else if (tbKHH.Text.Length == 0)
            {
                MessageBox.Show("开户行不能为空！", "系统提示", MessageBoxButtons.OK, MessageBoxIcon.Error);
                tbKHH.Focus();
                result = false;
            }
            else if (tbKHHLXR.Text.Length == 0)
            {
                MessageBox.Show("开户行联系人不能为空！", "系统提示", MessageBoxButtons.OK, MessageBoxIcon.Error);
                tbKHHLXR.Focus();
                result = false;
            }
            else if (tbKHHLXDH.Text.Length == 0)
            {
                MessageBox.Show("开户行联系电话不能为空！", "系统提示", MessageBoxButtons.OK, MessageBoxIcon.Error);
                tbKHHLXDH.Focus();
                result = false;
            }
            /*
            else if (tbPZKHHZH_4.Text.Length == 0&&this.radioButton2.Checked==true)
            {
                MessageBox.Show("批准开户核准号不能为空！", "系统提示", MessageBoxButtons.OK, MessageBoxIcon.Error);
                tbPZKHHZH_4.Focus();
                result = false;
            }*/
            else if (tbDWFZR.Text.Length == 0)
            {
                MessageBox.Show("单位负责人不能为空！", "系统提示", MessageBoxButtons.OK, MessageBoxIcon.Error);
                tbDWFZR.Focus();
                result = false;
            }
            else if (tbDWFZRLXDH.Text.Length == 0)
            {
                MessageBox.Show("单位负责人联系电话不能为空！", "系统提示", MessageBoxButtons.OK, MessageBoxIcon.Error);
                tbDWFZRLXDH.Focus();
                result = false;
            }else if (comboCLYJ.SelectedIndex < 0)
            {
                MessageBox.Show("处理意见不能为空！", "系统提示", MessageBoxButtons.OK, MessageBoxIcon.Error);
                comboCLYJ.Focus();
                result = false;
            }
            else if (comboHB.SelectedIndex > 4&&comboBZYY.SelectedIndex < 0)
            {
                MessageBox.Show("在五大行以外开户，请说明原因！", "系统提示", MessageBoxButtons.OK, MessageBoxIcon.Error);
                comboBZYY.Focus();
                result = false;
            }
            else if ((comboBZYY.SelectedIndex == 1)&&(textboxBZYYMX.Text==""))
            {
                MessageBox.Show("请说明在五大行以外开户的详细原因！", "系统提示", MessageBoxButtons.OK, MessageBoxIcon.Error);
                comboBZYY.Focus();
                result = false;
            }

            return result;
        }

        private bool CheckInputContent()
        {
            if (dtpPZKHSJ.Value.ToShortDateString() == new DateTime(2019, 6, 30).ToShortDateString())
            {
                MessageBox.Show("请选择批准开户日期（不能为默认2019-6-30）！", "系统提示", MessageBoxButtons.OK, MessageBoxIcon.Stop);
                return false;
            }
            if (dtpKHSJ.Value.ToShortDateString() == new DateTime(2019, 6, 30).ToShortDateString())
            {
                MessageBox.Show("请选择开户日期（不能为默认2019-6-30）！", "系统提示", MessageBoxButtons.OK, MessageBoxIcon.Stop);
                return false;
            }
            if (dtpPZCHSJ.Value.ToShortDateString() == new DateTime(2020, 12, 31).ToShortDateString() && comboCLYJ.Text == "已撤销")
            {
                MessageBox.Show("请选择批准撤户日期（处理意见为‘已撤销’时不能为默认2020-12-31）！","系统提示",MessageBoxButtons.OK,MessageBoxIcon.Stop);
                return false;
            }
            if (dtpPZCHSJ.Value.ToShortDateString() == new DateTime(2020, 12, 31).ToShortDateString() && comboCLYJ.Text == "拟撤销")
            {
                MessageBoxButtons msgBut = MessageBoxButtons.OKCancel;
                DialogResult dr = MessageBox.Show("批准撤户日期为系统默认2020-12-31,是否确定保存数据？", "系统提示", msgBut, MessageBoxIcon.Question);
                if (dr == DialogResult.Cancel)
                {
                    return false;
                }                    
            }
            if (!ClassInputRestricter.LenCheck(m_zh, 5, 30))
            {
                MessageBox.Show("请检查账号长度为5-30！", "系统提示", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return false;
            }
            else
            {
                if ("建设银行" == m_hb)
                {
                    if (!ClassInputRestricter.LenCheck(m_zh, 20, 20))
                    {
                        MessageBoxButtons msgBut = MessageBoxButtons.OKCancel;
                        DialogResult dr = MessageBox.Show("建设银行账号长度为20位,是否确定保存数据？", "系统提示", msgBut,MessageBoxIcon.Question);
                        if (dr == DialogResult.OK)
                        {
                        }
                        else
                            return false;
                    }
                }
                else if ("农业银行" == m_hb)
                {
                    if (!ClassInputRestricter.LenCheck(m_zh, 17, 17))
                    {
                        MessageBoxButtons msgBut = MessageBoxButtons.OKCancel;
                        DialogResult dr = MessageBox.Show("农业银行账号长度为17位,是否确定保存数据？", "系统提示", msgBut, MessageBoxIcon.Question);
                        if (dr == DialogResult.OK)
                        {
                        }
                        else
                            return false;
                    }
                }
                else if ("工商银行" == m_hb)
                {
                    if (!ClassInputRestricter.LenCheck(m_zh, 19, 19))
                    {
                        MessageBoxButtons msgBut = MessageBoxButtons.OKCancel;
                        DialogResult dr = MessageBox.Show("工商银行账号长度为19位,是否确定保存数据？", "系统提示", msgBut, MessageBoxIcon.Question);
                        if (dr == DialogResult.OK)
                        {
                        }
                        else
                            return false;
                    }
                }
                else if ("中国银行" == m_hb)
                {
                    if (!ClassInputRestricter.LenCheck(m_zh, 12, 12))
                    {
                        MessageBoxButtons msgBut = MessageBoxButtons.OKCancel;
                        DialogResult dr = MessageBox.Show("中国银行账号长度为12位,是否确定保存数据？", "系统提示", msgBut, MessageBoxIcon.Question);
                        if (dr == DialogResult.OK)
                        {
                        }
                        else
                            return false;
                    }
                }
                return true;
            }
        }
        /// <summary>
        /// 保存
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btnSave_Click(object sender, EventArgs e)
        {
            m_ckye = double.Parse(tbCKYE.Text.Replace(",",""));
            m_clyj = comboCLYJ.Text;
            m_dwfzr = tbDWFZR.Text;
            m_dwfzrlxdh = tbDWFZRLXDH.Text;
            m_dwdm = comboDWDM.SelectedValue.ToString();
            m_hb = comboHB.Text;
            m_khh = tbKHH.Text;
            m_khhlxdh = tbKHHLXDH.Text;
            m_khhlxr = tbKHHLXR.Text;
            m_khsj = dtpKHSJ.Value.ToShortDateString();
            m_pzchsj = dtpPZCHSJ.Value.ToShortDateString();
            m_pzkhsj = dtpPZKHSJ.Value.ToShortDateString();
            m_szss = comboSZSS.Text;
            m_zh = tbZH.Text;
            m_zhlb = comboZHLB.Text;
            m_zhmc = tbZHMC.Text;
            m_zhxz = comboZHXZ.Text;
            m_bzyy = comboBZYY.Text;
            m_bzyymx = textboxBZYYMX.Text;
            m_pzkhhzh = this.getPzkhhzh();

            if (!CheckInputEmpty())
            {
                return;
            }

            if (!CheckInputContent())
            {
                return;
            }

            if (m_clyj == "保留")
            {
                m_pzchsj = "";
            }

            int result = ValidateAccountFormat(m_pzkhhzh);
            if (result != 0)
            {
                MessageBox.Show("军队批准开户核准号填写错误（正确格式为：[X]第YJB123456号），请检查！", "信息提示",MessageBoxButtons.OK,MessageBoxIcon.Error);
                return;
            }

            if (m_id != ClassConstants.JD_NOTSELECTED)
            {
                OleDbParameter[] parms = new OleDbParameter[] { 
                    new OleDbParameter("@DWDM",OleDbType.VarChar),
                    new OleDbParameter("@ZHMC",OleDbType.VarChar),
                    new OleDbParameter("@ZH",OleDbType.VarChar),
                    new OleDbParameter("@ZHLB",OleDbType.VarChar),
                    new OleDbParameter("@ZHXZ",OleDbType.VarChar),
                    new OleDbParameter("@HB",OleDbType.VarChar),
                    new OleDbParameter("@KHH",OleDbType.VarChar),
                    new OleDbParameter("@KHHLXR",OleDbType.VarChar),
                    new OleDbParameter("@KHHLXDH",OleDbType.VarChar),
                    new OleDbParameter("@KHSJ",OleDbType.VarChar),
                    new OleDbParameter("@PZKHSJ",OleDbType.VarChar),
                    new OleDbParameter("@PZKHHZH",OleDbType.VarChar),
                    new OleDbParameter("@PZCHSJ",OleDbType.VarChar),
                    new OleDbParameter("@DWFZR",OleDbType.VarChar),
                    new OleDbParameter("@DWFZRLXDH",OleDbType.VarChar),
                    new OleDbParameter("@SZSS",OleDbType.VarChar),
                    new OleDbParameter("@CKYE",OleDbType.Double),
                    new OleDbParameter("@CLYJ",OleDbType.VarChar),
                    new OleDbParameter("@BZYY",OleDbType.VarChar),
                    new OleDbParameter("@BZYYMX",OleDbType.VarChar),
                    new OleDbParameter("@ID",OleDbType.Integer)
                };
                parms[0].Value = m_dwdm;
                parms[1].Value = m_zhmc;
                parms[2].Value = m_zh;
                parms[3].Value = m_zhlb;
                parms[4].Value = m_zhxz;
                parms[5].Value = m_hb;
                parms[6].Value = m_khh;
                parms[7].Value = m_khhlxr;
                parms[8].Value = m_khhlxdh;
                parms[9].Value = m_khsj;
                parms[10].Value = m_pzkhsj;
                parms[11].Value = m_pzkhhzh;
                parms[12].Value = m_pzchsj;
                parms[13].Value = m_dwfzr;
                parms[14].Value = m_dwfzrlxdh;
                parms[15].Value = m_szss;
                parms[16].Value = m_ckye;
                parms[17].Value = m_clyj;
                parms[18].Value = m_bzyy;
                parms[19].Value = m_bzyymx;
                parms[20].Value = m_id;

                UpdateData(parms);
                GetAllDataRefreshGridView();
                MessageBox.Show("更新数据完毕！", "系统提示",MessageBoxButtons.OK,MessageBoxIcon.Information);
            }
            else
            {
                OleDbParameter[] parms = new OleDbParameter[] { 
                    new OleDbParameter("@DWDM",OleDbType.VarChar),
                    new OleDbParameter("@ZHMC",OleDbType.VarChar),
                    new OleDbParameter("@ZH",OleDbType.VarChar),
                    new OleDbParameter("@ZHLB",OleDbType.VarChar),
                    new OleDbParameter("@ZHXZ",OleDbType.VarChar),
                    new OleDbParameter("@HB",OleDbType.VarChar),
                    new OleDbParameter("@KHH",OleDbType.VarChar),
                    new OleDbParameter("@KHHLXR",OleDbType.VarChar),
                    new OleDbParameter("@KHHLXDH",OleDbType.VarChar),
                    new OleDbParameter("@KHSJ",OleDbType.VarChar),
                    new OleDbParameter("@PZKHSJ",OleDbType.VarChar),
                    new OleDbParameter("@PZKHHZH",OleDbType.VarChar),
                    new OleDbParameter("@PZCHSJ",OleDbType.VarChar),
                    new OleDbParameter("@DWFZR",OleDbType.VarChar),
                    new OleDbParameter("@DWFZRLXDH",OleDbType.VarChar),
                    new OleDbParameter("@SZSS",OleDbType.VarChar),
                    new OleDbParameter("@CKYE",OleDbType.Double),
                    new OleDbParameter("@CLYJ",OleDbType.VarChar),
                    new OleDbParameter("@BZYY",OleDbType.VarChar),
                    new OleDbParameter("@BZYYMX",OleDbType.VarChar)
                };
                parms[0].Value = m_dwdm;
                parms[1].Value = m_zhmc;
                parms[2].Value = m_zh;
                parms[3].Value = m_zhlb;
                parms[4].Value = m_zhxz;
                parms[5].Value = m_hb;
                parms[6].Value = m_khh;
                parms[7].Value = m_khhlxr;
                parms[8].Value = m_khhlxdh;
                parms[9].Value = m_khsj;
                parms[10].Value = m_pzkhsj;
                parms[11].Value = m_pzkhhzh;
                parms[12].Value = m_pzchsj;
                parms[13].Value = m_dwfzr;
                parms[14].Value = m_dwfzrlxdh;
                parms[15].Value = m_szss;
                parms[16].Value = m_ckye;
                parms[17].Value = m_clyj;
                parms[18].Value = m_bzyy;
                parms[19].Value = m_bzyymx;

                InsertData(parms);
                GetAllDataRefreshGridView();
                MessageBox.Show("插入数据完毕！","系统提示",MessageBoxButtons.OK,MessageBoxIcon.Information);
            }
            setsaved();
            ClearControlData();
            //setControl();
            ClearVariableData();
        }

        /// <summary>
        /// 退出
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btnExit_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void checkchagnge()
        {
            double temp_ckye = double.Parse(tbCKYE.Text.Replace(",", ""));
            String temp_clyj = comboCLYJ.Text;
            String temp_dwfzr = tbDWFZR.Text;
            String temp_dwfzrlxdh = tbDWFZRLXDH.Text;
            String temp_dwdm = ReturnDeptCode(comboDWDM.Text);
            String temp_hb = comboHB.Text;
            String temp_khh = tbKHH.Text;
            String temp_khhlxdh = tbKHHLXDH.Text;
            String temp_khhlxr = tbKHHLXR.Text;
            String temp_khsj = dtpKHSJ.Value.ToShortDateString();
            String temp_pzchsj = dtpPZCHSJ.Value.ToShortDateString();
            String temp_pzkhsj = dtpPZKHSJ.Value.ToShortDateString();
            String temp_szss = comboSZSS.Text;
            String temp_zh = tbZH.Text;
            String temp_zhlb = comboZHLB.Text;
            String temp_zhmc = tbZHMC.Text;
            String temp_zhxz = comboZHXZ.Text;
            String temp_pzkhhzh = this.getPzkhhzh();
            String temp_bzyy = comboBZYY.Text;
            String temp_bzyymx = textboxBZYYMX.Text;

            //MessageBox.Show(temp_pzkhhzh.ToString() + m_pzkhhzh.ToString());
            if (temp_ckye != m_ckye || temp_clyj != m_clyj || temp_dwfzr != m_dwfzr || temp_dwfzrlxdh != m_dwfzrlxdh || temp_dwdm != m_dwdm || temp_hb != m_hb || temp_khh != m_khh || temp_khhlxdh != m_khhlxdh || temp_khhlxr != m_khhlxr || temp_szss != m_szss || temp_zh != m_zh || temp_zhlb != m_zhlb || temp_zhmc != m_zhmc || temp_zhxz != m_zhxz || temp_pzkhhzh != m_pzkhhzh||temp_bzyy!=m_bzyy||temp_bzyymx!=m_bzyymx)
            {
                setnotsaved();
            }
            else
            {
                setsaved();
            }
        }

        public void GetAllDataRefreshGridView()
        {
            //string tempSQL = "SELECT * FROM t_yhzh ORDER BY ID";
            string tempSQL = "SELECT t_yhzh.ID as ID号,t_yhzh.dwdm as 单位代码,t_dwxx.dwmc as 单位名称,t_dwxx.dwxz as 单位性质,t_dwxx.dwjb as 单位级别,t_dwxx.dwlx as 单位类型,t_dwxx.szss as 所在省市,t_yhzh.zhmc as 账户名称,t_yhzh.zh as 账号,t_yhzh.zhlb as 账户类别,t_yhzh.zhxz as 账户性质,t_yhzh.hb as 行别,t_yhzh.khsj as 开户时间,t_yhzh.pzkhsj as 开户核准时间,t_yhzh.pzkhhzh as 批准开户核准号,t_yhzh.pzchsj as 批准撤户时间,t_yhzh.dwfzr as 单位负责人,t_yhzh.dwfzrlxdh as 单位负责人联系电话,t_yhzh.szss as 省市,t_yhzh.khh as 开户行,t_yhzh.khhlxr as 开户行联系人,t_yhzh.khhlxdh as 开户行联系电话,t_yhzh.ckye as 存款余额,t_yhzh.clyj as 处理意见,t_yhzh.bzyy as 在五大行以外开户原因,t_yhzh.bzyymx as 备注 FROM t_dwxx,t_yhzh where t_yhzh.dwdm=t_dwxx.dwdm " + this.filterStr + "ORDER BY t_yhzh.dwdm,t_yhzh.ID";
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
            dataGridView.Columns["单位名称"].Width = 200;

            //dataGridView.Rows[dataGridView.Rows.Count-2].Selected = true;
            if (dataGridView.SelectedRows.Count != 0)
            {
                m_id = int.Parse(dataGridView.SelectedRows[0].Cells[1].Value.ToString());
            }
            else
            {
                m_id = ClassConstants.JD_NOTSELECTED;
            }
        }
        public struct dwItem
        {
            public String dwdm;
            public String dwmc;
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

        public void JudgeControlVariables()
        {

        }

        public void ReadDataToVariables()
        {
            m_id = int.Parse(dataGridView.SelectedRows[0].Cells[1].Value.ToString());
            DataTable dt = this.GetDataByIndex(m_id);
            m_ckye = double.Parse(dt.Rows[0]["ckye"].ToString());
            m_clyj = dt.Rows[0]["clyj"].ToString();
            m_dwdm = dt.Rows[0]["dwdm"].ToString();
            m_dwfzr = dt.Rows[0]["dwfzr"].ToString();
            m_dwfzrlxdh = dt.Rows[0]["dwfzrlxdh"].ToString();
            m_hb = dt.Rows[0]["hb"].ToString();
            m_khh = dt.Rows[0]["khh"].ToString();
            m_khhlxdh = dt.Rows[0]["khhlxdh"].ToString();
            m_khhlxr = dt.Rows[0]["khhlxr"].ToString();
            m_khsj = dt.Rows[0]["khsj"].ToString();
            m_pzchsj = dt.Rows[0]["pzchsj"].ToString();
            m_pzkhhzh = dt.Rows[0]["pzkhhzh"].ToString();
            m_pzkhsj = dt.Rows[0]["pzkhsj"].ToString();
            m_szss = dt.Rows[0]["szss"].ToString();
            m_zh = dt.Rows[0]["zh"].ToString();
            m_zhlb = dt.Rows[0]["zhlb"].ToString();
            m_zhmc = dt.Rows[0]["zhmc"].ToString();
            m_zhxz = dt.Rows[0]["zhxz"].ToString();
            m_bzyy = dt.Rows[0]["bzyy"].ToString();
            m_bzyymx = dt.Rows[0]["bzyymx"].ToString();

            if (m_pzchsj == "")
            {
                m_pzchsj = "2020-12-31";
            }
        }

        public void LoadVariablesToControls()
        {
            tbCKYE.Text = m_ckye.ToString("n");
            tbDWFZR.Text = m_dwfzr;
            tbDWFZRLXDH.Text = m_dwfzrlxdh;
            tbKHH.Text = m_khh;
            tbKHHLXDH.Text = m_khhlxdh;
            tbKHHLXR.Text = m_khhlxr;
            //tbPZKHHZH_4.Text = m_pzkhhzh;
            tbZH.Text = m_zh;
            tbZHMC.Text = m_zhmc;
            textboxBZYYMX.Text = m_bzyymx;

            comboCLYJ.SelectedIndex = comboCLYJ.Items.IndexOf(m_clyj);
            comboHB.SelectedIndex = comboHB.Items.IndexOf(m_hb);
            comboSZSS.SelectedIndex = comboSZSS.Items.IndexOf(m_szss);
            comboZHLB.SelectedIndex = comboZHLB.Items.IndexOf(m_zhlb);
            comboZHXZ.SelectedIndex = comboZHXZ.Items.IndexOf(m_zhxz);
            comboBZYY.SelectedIndex = comboBZYY.Items.IndexOf(m_bzyy);
            setPzkhhzh(m_pzkhhzh);

            string str = m_khsj;
            string[] result = str.Split('-');
            LoadDateToDateTimePicker(str, dtpKHSJ);

            result = str.Split('-');
            str = m_pzkhsj;
            LoadDateToDateTimePicker(str, dtpPZKHSJ);

            result = str.Split('-');
            str = m_pzchsj;
            LoadDateToDateTimePicker(str, dtpPZCHSJ);

            comboDWDM.SelectedValue = m_dwdm;
        }

        private void LoadDateToDateTimePicker(string date,DateTimePicker destPicker)
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

        private void comboDWDM_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (comboDWDM.SelectedValue != null)
                m_dwdm = comboDWDM.SelectedValue.ToString();
        }

        private string ReturnDeptCode(string deptName)
        {
            string tempSQL = "SELECT * FROM t_dwxx WHERE dwmc='" + deptName + "'";
            DataSet ds = m_accessHelper.getDataSet(tempSQL);
            return ds.Tables[0].Rows[0]["dwdm"].ToString();
        }

        private void tbCKYE_TextChanged(object sender, EventArgs e)
        {
            ClassInputRestricter.CheckChange(sender);
            //this.setnotsaved();
        }

        /// <summary>
        /// 导出
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btnExport_Click(object sender, EventArgs e)
        {
            DataTable dt = dataGridView.DataSource as DataTable;
            ExcelUI ExcelUI = new ExcelUI();
            ExcelUI.ExportExcelFormat(dt, "");
        }

        private void tbKHHLXDH_TextChanged(object sender, EventArgs e)
        {
            if (!ClassInputRestricter.IsUnInteger(tbKHHLXDH.Text.Trim()))
            {
                tbKHHLXDH.Text = tbKHHLXDH.Text.Substring(0, tbKHHLXDH.Text.Length - 1);
                tbKHHLXDH.Select(tbKHHLXDH.SelectionStart, tbKHHLXDH.Text.Length);
                
            }
            //this.setnotsaved();
        }

        private void tbDWFZRLXDH_TextChanged(object sender, EventArgs e)
        {
            if (!ClassInputRestricter.IsUnInteger(tbDWFZRLXDH.Text.Trim()))
            {
                tbDWFZRLXDH.Text = tbDWFZRLXDH.Text.Substring(0, tbDWFZRLXDH.Text.Length - 1);
                tbDWFZRLXDH.Select(tbDWFZRLXDH.SelectionStart, tbDWFZRLXDH.Text.Length);
                
            }
        }

        /// <summary>
        /// 账号限制
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void tbZH_TextChanged(object sender, EventArgs e)
        {
            if (!ClassInputRestricter.IsUnInteger(tbZH.Text.Trim()))
            {
                tbZH.Text = tbZH.Text.Substring(0, tbZH.Text.Length - 1);
                tbZH.Select(tbZH.SelectionStart, tbZH.Text.Length);
                
            }
            //this.setnotsaved();
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

        private int ValidateAccountFormat(string accountName)
        {
            if (this.radioButton1.Checked == true)//部队审批
            {   
                if (accountName.Length != 14)
                {
                    return -1;
                }
                for (int i = 0; i < accountName.Length; i++)
                {
                    switch (i)
                    {
                        case 0:
                            if (accountName[i] != '[')
                                return -2;
                            break;
                        case 1:
                            break;
                        case 2:
                            if (accountName[i] != ']')
                                return -2;
                            break;
                        case 3:
                            if (accountName[i] != '第')
                                return -2;
                            break;
                        case 7:
                            if (!IsNumeric(accountName[i]))
                                return -2;
                            break;
                        case 8:
                            if (!IsNumeric(accountName[i]))
                                return -2;
                            break;
                        case 9:
                            if (!IsNumeric(accountName[i]))
                                return -2;
                            break;
                        case 10:
                            if (!IsNumeric(accountName[i]))
                                return -2;
                            break;
                        case 11:
                            if (!IsNumeric(accountName[i]))
                                return -2;
                            break;
                        case 12:
                            if (!IsNumeric(accountName[i]))
                                return -2;
                            break;
                        case 13:
                            if (accountName[i] != '号')
                                return -2;
                            break;
                        default:
                            break;
                    }
                }
            }
            return 0;
        }

        private bool IsNumeric(char character)
        {
            if (character == '0' || character == '1' || character == '2' || character == '3' || character == '4'
                || character == '5' || character == '6' || character == '7' || character == '8' || character == '9')
                return true;
            else
                return false;
        }

        private void tbCKYE_KeyPress(object sender, KeyPressEventArgs e)
        {
            ClassInputRestricter.CheckInput(sender, e);
        }

        private void tbCKYE_Leave(object sender, EventArgs e)
        {
            ClassInputRestricter.CheckFormat(sender);
        }

        private void comboDWDM_KeyPress(object sender, KeyPressEventArgs e)
        {
            ClassInputRestricter.EnterToTab(sender,e);
        }

        private void comboZHLB_KeyPress(object sender, KeyPressEventArgs e)
        {
            ClassInputRestricter.EnterToTab(sender, e);
        }

        private void comboZHXZ_KeyPress(object sender, KeyPressEventArgs e)
        {
            ClassInputRestricter.EnterToTab(sender, e);
        }

        private void tbZHMC_KeyPress(object sender, KeyPressEventArgs e)
        {
            ClassInputRestricter.EnterToTab(sender, e);
        }

        private void comboSZSS_KeyPress(object sender, KeyPressEventArgs e)
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

        private void dtpPZKHSJ_KeyPress(object sender, KeyPressEventArgs e)
        {
            ClassInputRestricter.EnterToTab(sender, e);
        }

        private void tbDWFZR_KeyPress(object sender, KeyPressEventArgs e)
        {
            ClassInputRestricter.EnterToTab(sender, e);
        }

        private void tbDWFZRLXDH_KeyPress(object sender, KeyPressEventArgs e)
        {
            ClassInputRestricter.EnterToTab(sender, e);
        }

        private void tbPZKHHZH_KeyPress(object sender, KeyPressEventArgs e)
        {
            ClassInputRestricter.EnterToTab(sender, e);
        }

        private void dtpKHSJ_KeyPress(object sender, KeyPressEventArgs e)
        {
            ClassInputRestricter.EnterToTab(sender, e);
        }

        private void tbKHHLXR_KeyPress(object sender, KeyPressEventArgs e)
        {
            ClassInputRestricter.EnterToTab(sender, e);
        }

        private void tbKHHLXDH_KeyPress(object sender, KeyPressEventArgs e)
        {
            ClassInputRestricter.EnterToTab(sender, e);
        }

        private void comboCLYJ_KeyPress(object sender, KeyPressEventArgs e)
        {
            ClassInputRestricter.EnterToTab(sender, e);
        }

        private void dtpPZCHSJ_KeyPress(object sender, KeyPressEventArgs e)
        {
            ClassInputRestricter.EnterToTab(sender, e);
        }

        private void tbZH_KeyPress(object sender, KeyPressEventArgs e)
        {
            ClassInputRestricter.EnterToTab(sender, e);
        }

        private void btnFilter_Click(object sender, EventArgs e)
        {

            Frm_select_account selectAccount = new Frm_select_account();
            selectAccount.ShowDialog();
            if (selectAccount.DialogResult == DialogResult.OK)
            {
                filterStr = selectAccount.GetSQL();
                GetAllDataRefreshGridView();
              }
        }

        private void radioButton1_CheckedChanged(object sender, EventArgs e)
        {
            if (this.radioButton1.Checked == true)
            {
                comboBoxPZKHHZH_1.Enabled = true;
                comboBoxPZKHHZH_2.Enabled = true;
                textBoxPZKHHZH_3.Enabled = true;
            }
            else
            {
                comboBoxPZKHHZH_1.Enabled = false;
                comboBoxPZKHHZH_2.Enabled = false;
                textBoxPZKHHZH_3.Enabled = false;
            }
        }

        private void radioButton2_CheckedChanged(object sender, EventArgs e)
        {
            if (this.radioButton2.Checked == true)
            {
                tbPZKHHZH_4.Enabled = true;
            }
            else
            {
                tbPZKHHZH_4.Enabled = false;
            }
        }

        private void textBoxPZKHHZH_3_KeyPress(object sender, KeyPressEventArgs e)
        {
            ClassInputRestricter.EnterToTab(sender, e);
            if ((e.KeyChar < '0' || e.KeyChar > '9') && e.KeyChar != '\b')
            {
                e.Handled = true;
            }
        }

        private void setPzkhhzh(string str)
        {
            if (str.Length==14&&str.Substring(3, 1) == "第")
            {
                comboBoxPZKHHZH_1.SelectedIndex = comboBoxPZKHHZH_1.Items.IndexOf(str.Substring(1, 1));
                comboBoxPZKHHZH_2.Items.Clear();
                comboBoxPZKHHZH_2.Items.Add(str.Substring(4, 3));
                comboBoxPZKHHZH_2.SelectedIndex = 0;
                textBoxPZKHHZH_3.Text = str.Substring(7, 6);
                tbPZKHHZH_4.Text = "";
                this.radioButton1.Checked = true;
            }
            else
            {
                tbPZKHHZH_4.Text = str;
                comboBoxPZKHHZH_1.SelectedIndex = -1;
                comboBoxPZKHHZH_2.SelectedIndex = -1;
                textBoxPZKHHZH_3.Text = "";
                this.radioButton2.Checked = true;
            }
        }

        private String getPzkhhzh()
        {
            String str = "";
            if (this.radioButton1.Checked == true)
            {
                str = "[" + comboBoxPZKHHZH_1.Text + "]第" + comboBoxPZKHHZH_2.Text + textBoxPZKHHZH_3.Text + "号";
            }
            else
            {
                str = tbPZKHHZH_4.Text;
                if (str.Trim() == "")
                {
                    str = "未审批";
                }
            }
            return str;
        }

        private void setSelectedPzkhhzh()
        {
            comboBoxPZKHHZH_2.Items.Clear();
            //MessageBox.Show("类别："+comboZHLB.Text);
            //MessageBox.Show("性质："+comboZHXZ.Text);
            if (comboZHXZ.Text == "特种预算存款")
            {
                if (comboZHLB.Text == "一般存款账户")
                {
                    comboBoxPZKHHZH_2.Items.Add("YYB");
                }
                if (comboZHLB.Text == "专用存款账户")
                {
                    comboBoxPZKHHZH_2.Items.Add("YZY");
                    comboBoxPZKHHZH_2.Items.Add("YDZ");
                }
                if (comboZHLB.Text == "财务单一账户")
                {
                    comboBoxPZKHHZH_2.Items.Add("YJB");
                    comboBoxPZKHHZH_2.Items.Add("YDD");
                }
                if (comboZHLB.Text == "小额账户")
                {
                    comboBoxPZKHHZH_2.Items.Add("YDX");
                }
                if (comboZHLB.Text == "零余额账户")
                {
                    comboBoxPZKHHZH_2.Items.Add("YDL");
                }
                if (comboZHLB.Text == "临时存款账户")
                {
                    comboBoxPZKHHZH_2.Items.Add("YLS");
                }
                if (comboZHLB.Text == "POS转账卡账户")
                {
                    comboBoxPZKHHZH_2.Items.Add("YZY");
                    comboBoxPZKHHZH_2.Items.Add("YDZ");
                }
            }
            else
            {
                if (comboZHXZ.Text == "特种事业存款")
                {
                    if (comboZHLB.Text == "一般存款账户")
                    {
                        comboBoxPZKHHZH_2.Items.Add("SYB");
                    }
                    if (comboZHLB.Text == "专用存款账户")
                    {
                        comboBoxPZKHHZH_2.Items.Add("SZY");
                        comboBoxPZKHHZH_2.Items.Add("SDZ");
                    }
                    if (comboZHLB.Text == "财务单一账户")
                    {
                        comboBoxPZKHHZH_2.Items.Add("SJB");
                    }
                    if (comboZHLB.Text == "小额账户")
                    {
                        comboBoxPZKHHZH_2.Items.Add("SDX");
                    }
                    if (comboZHLB.Text == "零余额账户")
                    {
                        comboBoxPZKHHZH_2.Items.Add("SDL");
                    }
                    if (comboZHLB.Text == "临时存款账户")
                    {
                        comboBoxPZKHHZH_2.Items.Add("SLS");
                    }
                    if (comboZHLB.Text == "POS转账卡账户")
                    {
                        comboBoxPZKHHZH_2.Items.Add("SZY");
                        comboBoxPZKHHZH_2.Items.Add("SDZ");
                    }
                }
                else//特种企业存款
                { 
                    if (comboZHLB.Text == "一般存款账户")
                    {
                        comboBoxPZKHHZH_2.Items.Add("QYB");
                    }
                    if (comboZHLB.Text == "专用存款账户")
                    {
                        comboBoxPZKHHZH_2.Items.Add("QZY");
                        comboBoxPZKHHZH_2.Items.Add("QDZ");
                    }
                    if (comboZHLB.Text == "财务单一账户")
                    {
                        comboBoxPZKHHZH_2.Items.Add("QJB");
                    }
                    if (comboZHLB.Text == "小额账户")
                    {
                        comboBoxPZKHHZH_2.Items.Add("QDX");
                    }
                    if (comboZHLB.Text == "零余额账户")
                    {
                        comboBoxPZKHHZH_2.Items.Add("QDL");
                    }
                    if (comboZHLB.Text == "临时存款账户")
                    {
                        comboBoxPZKHHZH_2.Items.Add("QLS");
                    }
                    if (comboZHLB.Text == "POS转账卡账户")
                    {
                        comboBoxPZKHHZH_2.Items.Add("QZY");
                        comboBoxPZKHHZH_2.Items.Add("QDZ");
                    }
                }
            }
            //comboBoxPZKHHZH_2.SelectedIndex = 0;
        }

        /// <summary>
        /// 账户类别
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void comboZHLB_SelectedIndexChanged(object sender, EventArgs e)
        {
            setSelectedPzkhhzh();
        }

        /// <summary>
        /// 账户行政
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void comboZHXZ_SelectedIndexChanged(object sender, EventArgs e)
        {
            setSelectedPzkhhzh();
        }

        private void FrmBankAccountStatistics_FormClosing(object sender, FormClosingEventArgs e)
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

        private void comboBoxPZKHHZH_1_KeyPress(object sender, KeyPressEventArgs e)
        {
            ClassInputRestricter.EnterToTab(sender, e);
        }

        private void comboBoxPZKHHZH_2_KeyPress(object sender, KeyPressEventArgs e)
        {
            ClassInputRestricter.EnterToTab(sender, e);
        }

        private void comboSZSS_SelectedIndexChanged(object sender, EventArgs e)
        {

        }

        private void comboCLYJ_SelectedIndexChanged(object sender, EventArgs e)
        {

        }

        private void FrmBankAccountStatistics_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.Control && e.KeyCode == Keys.S)
            {
                btnSave_Click(sender, e);
            }
        }

        private void radioButton1_KeyPress(object sender, KeyPressEventArgs e)
        {
            ClassInputRestricter.EnterToTab(sender, e);
        }

        private void radioButton2_KeyPress(object sender, KeyPressEventArgs e)
        {
            ClassInputRestricter.EnterToTab(sender, e);
        }

        private void dtpPZKHSJ_KeyPress_1(object sender, KeyPressEventArgs e)
        {
            ClassInputRestricter.EnterToTab(sender, e);
        }

        private void dtpKHSJ_KeyPress_1(object sender, KeyPressEventArgs e)
        {
            ClassInputRestricter.EnterToTab(sender, e);
        }

        private void dtpPZCHSJ_KeyPress_1(object sender, KeyPressEventArgs e)
        {
            ClassInputRestricter.EnterToTab(sender, e);
        }

        private void tbZHMC_TextChanged(object sender, EventArgs e)
        {
            //this.setnotsaved();
        }

        private void tbKHH_TextChanged(object sender, EventArgs e)
        {
            //this.setnotsaved();
        }

        private void tbDWFZR_TextChanged(object sender, EventArgs e)
        {
            //this.setnotsaved();
        }

        private void tbKHHLXR_TextChanged(object sender, EventArgs e)
        {
            //this.setnotsaved();
        }

        private void FrmBankAccountStatistics_Load_1(object sender, EventArgs e)
        {

        }

        private void comboBoxBZYY_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (comboBZYY.Text == "其他原因")
            {
                textboxBZYYMX.Enabled = true;
            }
            else
            {
                textboxBZYYMX.Text = "";
                textboxBZYYMX.Enabled = false;
            }
        }

        private void comboHB_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (comboHB.SelectedIndex < 5)
            {
                comboBZYY.Enabled = false;
                comboBZYY.SelectedIndex = -1;
            }
            else
            {
                comboBZYY.Enabled = true;
            }
        }

        private void groupBoxInfo_Enter(object sender, EventArgs e)
        {

        }

        private void comboBZYY_KeyPress(object sender, KeyPressEventArgs e)
        {
            ClassInputRestricter.EnterToTab(sender, e);
        }

        private void textboxBZYYMX_KeyPress(object sender, KeyPressEventArgs e)
        {
            ClassInputRestricter.EnterToTab(sender, e);
        }
    }
}
