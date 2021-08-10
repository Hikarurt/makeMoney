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
    public partial class danwei : Form
    {
        private AccessHelper m_accessHelper = new AccessHelper();
        public int dwid = 0;
        int sfxhwpbs = 0;
        string a = string.Empty;
        String maxdwdmnew = "";
        String slected_dwdm = "";
        public Boolean modefied = false; //默认没有修改数据
        public String m_dwdm, m_dwmc, m_dwxz, m_dwjb, m_dwlx, m_szss, m_xxdz, m_yzbm, m_lxr, m_lxfs, m_szs, m_szx, m_yjdd, m_sszd, m_jxzj = "", m_jxsj = "", m_nxjjsh = "", m_nxjjs = "", m_jsd = "", m_sfxhwp = "", m_xhsj = "", m_xhdd = "";
        public String m_bzk, m_gzkfk;
        private string SQL_Admin_Delete = "DELETE FROM t_dwxx WHERE left(dwdm,3)=@DWDM";
        public danwei()
        {
            InitializeComponent();
        }

        public void setnotsaved()
        {
            this.modefied = true;
        }

        public void setsaved()
        {
            this.modefied = false;
        }

        public void judge()
        {
            if (textBox_dwdm.Text == m_dwdm && textBox_dwmc.Text == m_dwmc && comboBox_dwxz.Text == m_dwxz && comboBox_dwjb.Text == m_dwjb && comboBox_dwlx.Text == m_dwlx && comboBox_szss.Text == m_szss && textBox_xxdz.Text == m_xxdz && textBox_yzbm.Text == m_yzbm && textBox_lxr.Text == m_lxr && textBox_lxfs.Text == m_lxfs)
            {
                setsaved();
            }
            else
            {
                setnotsaved();
            }
        }

        private void cbnxs_SelectedIndexChanged(object sender, EventArgs e)
        {
            string nxjjs = cbnxs.Text;
            if (!string.IsNullOrEmpty(nxjjs))
            {
                string sql = "select mc from t_tcwpzdb where lb='拟选位'  and fjbh='" + nxjjs + "' order by ID";
                DataTable dt = m_accessHelper.getDataSet(sql).Tables[0];
                if (dt.Rows.Count > 0)
                {
                    DataTable dt1 = new DataTable();
                    tbjsd.DataSource = dt1;
                    tbjsd.Visible = true;
                    tbjsd.Visible = true;
                    tbjsd.DataSource = dt;
                    tbjsd.ValueMember = "mc";
                    tbjsd.DisplayMember = "mc";
                }
                else
                {
                    DataTable dt1 = new DataTable();
                    tbjsd.DataSource = dt1;
                }
            }
        }

        private void button2_Click(object sender, EventArgs e)
        {
            AccessHelper AccessHelper = new AccessHelper();
            string sql_dwxx = string.Format("select dwdm as 单位代码,dwmc as 单位名称,dwjb  as 单位级别,dwlx  as 的单位类型,szss  as 所在省,szs as 所在市,szx as 所在县,xxdz  as  详细地址 ,nxsh as 拟选省,nxs as 拟选市,nxjsd as 交接地点,xhdd as 销毁地点,xhsj  as 销毁时间 ,yzbm  as 邮政编码,lxr  as 联系人, jxzj  as 军线座机,jxsj as 手机 from t_dwxx order by dwdm");
            DataTable dt = AccessHelper.getDataSet(sql_dwxx).Tables[0];

            ExcelUI ExcelUI = new ExcelUI();
            ExcelUI.ExportExcelFormat(dt, "");
        }

        private void label12_Click(object sender, EventArgs e)
        {

        }

        private void button4_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void danwei_Load(object sender, EventArgs e)
        {
            treeView1.FullRowSelect = true;
            treeView1.Indent = 18;
            treeView1.ItemHeight = 18;
            treeView1.LabelEdit = false;
            treeView1.Scrollable = true;
            treeView1.ShowPlusMinus = true;
            treeView1.ShowRootLines = true;
            GetNXJJS();

            load_danwei();
            if (login.LogName == "zbgly")
            {
                button1.Visible = false;
            }
            else
            {
                button1.Visible = true;
            }
        }

        private void treeView1_AfterSelect(object sender, TreeViewEventArgs e)
        {
            judge();
            AccessHelper AccessHelper = new AccessHelper();

            if (this.modefied == true && textBox_dwdm.Text != "")//未保存的情况下
            {
                DialogResult dr = MessageBox.Show("数据已经修改，是否先保存数据？", "系统提示", MessageBoxButtons.OKCancel, MessageBoxIcon.Question);
                if (dr == DialogResult.OK)//执行保存
                {
                    button3_Click(sender, e);
                    return;
                }
                else
                {
                    //load_danwei();
                    treeView1.Nodes.Clear();
                    treeView1.ShowLines = true;
                    String sql_dwxx1 = "select id,dwdm,dwmc from t_dwxx where len(dwdm)<4 and dwdm not like '000' order by dwdm asc";
                    DataSet ds1 = AccessHelper.getDataSet(sql_dwxx1);
                    int i = 0;
                    while (i < ds1.Tables[0].Rows.Count)
                    {
                        TreeNode newNode1 = treeView1.Nodes.Add(ds1.Tables[0].Rows[i]["id"].ToString(), ds1.Tables[0].Rows[i]["dwmc"].ToString(), 0, 1);
                        newNode1.ToolTipText = ds1.Tables[0].Rows[i]["dwdm"].ToString();
                        i++;
                    }
                }

            }
            if (login.LogName == "zbgly")
            {
                if (treeView1.SelectedNode != null)
                {
                    slected_dwdm = treeView1.SelectedNode.Name.ToString();
                }
            }
            else
            {

                dwid = Int32.Parse(e.Node.Name);
                slected_dwdm = e.Node.ToolTipText.ToString();
            }



            String sql_dwxx = "select * from t_dwxx where dwdm='" + slected_dwdm + "'";
            DataSet ds = AccessHelper.getDataSet(sql_dwxx);
            if (ds.Tables[0].Rows.Count > 0)
            {
                m_dwdm = ds.Tables[0].Rows[0]["dwdm"].ToString();
                m_dwmc = ds.Tables[0].Rows[0]["dwmc"].ToString();
                m_dwxz = ds.Tables[0].Rows[0]["dwxz"].ToString();
                m_dwjb = ds.Tables[0].Rows[0]["dwjb"].ToString();
                m_dwlx = ds.Tables[0].Rows[0]["dwlx"].ToString();
                m_szss = ds.Tables[0].Rows[0]["szss"].ToString();
                m_xxdz = ds.Tables[0].Rows[0]["xxdz"].ToString();
                m_yzbm = ds.Tables[0].Rows[0]["yzbm"].ToString();
                m_lxr = ds.Tables[0].Rows[0]["lxr"].ToString();
                m_lxfs = ds.Tables[0].Rows[0]["lxfs"].ToString();
                m_bzk = ds.Tables[0].Rows[0]["bzk"].ToString();
                m_gzkfk = ds.Tables[0].Rows[0]["gzkfk"].ToString();
                m_szs = ds.Tables[0].Rows[0]["szs"].ToString();
                m_szx = ds.Tables[0].Rows[0]["szx"].ToString();
                m_yjdd = ds.Tables[0].Rows[0]["yjdd"].ToString();
                m_sszd = ds.Tables[0].Rows[0]["sszd"].ToString();

                tbjxzj.Text = ds.Tables[0].Rows[0]["jxzj"].ToString();
                tbjxsj.Text = ds.Tables[0].Rows[0]["jxsj"].ToString();
                cbnxsh.Text = ds.Tables[0].Rows[0]["nxsh"].ToString();
                cbnxs.Text = ds.Tables[0].Rows[0]["nxs"].ToString();
                tbjsd.Text = ds.Tables[0].Rows[0]["nxjsd"].ToString();
                if (ds.Tables[0].Rows[0]["sfxhwp"].ToString() == "1")
                {
                    cbsfxhwp.Checked = true;
                }
                else
                {
                    cbsfxhwp.Checked = false;
                }
                dtxhsj.Text = ds.Tables[0].Rows[0]["xhsj"].ToString();
                tbxxdd.Text = ds.Tables[0].Rows[0]["xhdd"].ToString();


                textBox_dwdm.Text = ds.Tables[0].Rows[0]["dwdm"].ToString();
                textBox_dwmc.Text = ds.Tables[0].Rows[0]["dwmc"].ToString();
                comboBox_dwxz.SelectedIndex = comboBox_dwxz.Items.IndexOf(ds.Tables[0].Rows[0]["dwxz"].ToString());
                comboBox_dwjb.SelectedIndex = comboBox_dwjb.Items.IndexOf(ds.Tables[0].Rows[0]["dwjb"].ToString());
                comboBox_dwlx.SelectedIndex = comboBox_dwlx.Items.IndexOf(ds.Tables[0].Rows[0]["dwlx"].ToString());
                comboBox_szss.SelectedIndex = comboBox_szss.Items.IndexOf(ds.Tables[0].Rows[0]["szss"].ToString());
                textBox_xxdz.Text = ds.Tables[0].Rows[0]["xxdz"].ToString();
                textBox_yzbm.Text = ds.Tables[0].Rows[0]["yzbm"].ToString();
                textBox_lxr.Text = ds.Tables[0].Rows[0]["lxr"].ToString();
                textBox_lxfs.Text = ds.Tables[0].Rows[0]["lxfs"].ToString();
                textBox_BZK.Text = ds.Tables[0].Rows[0]["bzk"].ToString();
                textBox_GZKFK.Text = ds.Tables[0].Rows[0]["gzkfk"].ToString();
                cbS.Text = m_szs;
                cbX.Text = m_szx;
                tbYJDD.Text = m_yjdd;
                tbSZZD.Text = m_sszd;


            }
            else
            {
                textBox_dwdm.Text = maxdwdmnew;
                textBox_dwmc.Text = "新单位";
                comboBox_dwxz.SelectedIndex = -1;
                comboBox_dwjb.SelectedIndex = -1;
                comboBox_dwlx.SelectedIndex = -1;
                comboBox_szss.SelectedIndex = -1;
                textBox_xxdz.Text = "";
                textBox_yzbm.Text = "";
                textBox_lxr.Text = "";
                textBox_lxfs.Text = "";
                dwid = 0;//标识保存时为增加单位
            }
        }

        private void comboBox_szss_SelectedValueChanged_1(object sender, EventArgs e)
        {

        }

        private void comboBox_szss_SelectedIndexChanged(object sender, EventArgs e)
        {
            string smc = comboBox_szss.Text;
            if (!string.IsNullOrEmpty(smc))
            {
                string sql = "select mc from t_tcwpzdb where (lb='省' or lb='县') and fjbh='" + smc + "' order by ID";
                DataTable dt = m_accessHelper.getDataSet(sql).Tables[0];
                if (dt.Rows.Count > 0)
                {
                    DataTable dt1 = new DataTable();
                    cbS.DataSource = dt1;
                    cbS.Visible = true;
                    cbX.Visible = true;
                    cbS.DataSource = dt;
                    cbS.ValueMember = "mc";
                    cbS.DisplayMember = "mc";
                }
                else
                {
                    cbS.Visible = false;
                    cbX.Visible = false;
                    cbS.Text = "";
                    cbX.Text = "";
                }
            }
        }

        private void cbS_SelectedIndexChanged(object sender, EventArgs e)
        {
            string smc = cbS.Text;
            if (!string.IsNullOrEmpty(smc))
            {
                string sql = "select mc from t_tcwpzdb where lb='市' and fjbh='" + smc + "' order by ID";
                DataTable dt = m_accessHelper.getDataSet(sql).Tables[0];
                if (dt.Rows.Count > 0)
                {
                    DataTable dt1 = new DataTable();
                    cbX.DataSource = dt1;
                    cbX.DataSource = dt;
                    cbX.ValueMember = "mc";
                    cbX.DisplayMember = "mc";
                }

            }
        }

        private void cbnxsh_SelectedIndexChanged(object sender, EventArgs e)
        {
            string nxjjs = cbnxsh.Text;
            if (!string.IsNullOrEmpty(nxjjs))
            {
                string sql = "select mc from t_tcwpzdb where lb='拟选交接地点位市'  and fjbh='" + nxjjs + "' order by ID";
                DataTable dt = m_accessHelper.getDataSet(sql).Tables[0];
                if (dt.Rows.Count > 0)
                {
                    DataTable dt1 = new DataTable();
                    cbnxs.DataSource = dt1;
                    cbnxs.Visible = true;
                    cbnxs.Visible = true;
                    cbnxs.DataSource = dt;
                    cbnxs.ValueMember = "mc";
                    cbnxs.DisplayMember = "mc";
                }
            }
        }

        private void cbsfxhwp_CheckedChanged(object sender, EventArgs e)
        {
            if (cbsfxhwp.Checked)
            {
                sfxhwpbs = 1;
                tbxxdd.Enabled = false;
                dtxhsj.Enabled = false;

                tbxxdd.Text = "";
                dtxhsj.Text = "2019/1/1";
            }
            else
            {
                tbxxdd.Enabled = true;
                dtxhsj.Enabled = true;

                sfxhwpbs = 0;

            }
        }

        private void tbYJDD_TextChanged(object sender, EventArgs e)
        {
            tbSZZD.Text = tbYJDD.Text;
        }

        private void comboBox_szss_SelectedValueChanged(object sender, EventArgs e)
        {
            string smc = comboBox_szss.Text;
            if (!string.IsNullOrEmpty(smc))
            {
                string sql = "select mc from t_tcwpzdb where lb='省' and fjbh='" + smc + "' order by ID";
                DataTable dt = m_accessHelper.getDataSet(sql).Tables[0];

                cbS.DataSource = dt;
                cbS.ValueMember = "mc";
                cbS.DisplayMember = "mc";
            }
        }

        private void cbS_SelectedValueChanged(object sender, EventArgs e)
        {

        }

        private void button1_Click(object sender, EventArgs e)
        {
            AccessHelper AccessHelper = new AccessHelper();


            if (this.modefied == true)//未保存的情况下
            {
                DialogResult dr = MessageBox.Show("数据已经修改，是否先保存数据？", "系统提示", MessageBoxButtons.OKCancel, MessageBoxIcon.Question);
                if (dr == DialogResult.OK)//执行保存
                {
                    button3_Click(sender, e);

                    return;
                }
                else
                {
                    //load_danwei();
                    treeView1.Nodes.Clear();
                    treeView1.ShowLines = true;
                    String sql_dwxx1 = "select id,dwdm,dwmc from t_dwxx where len(dwdm)<4 and dwdm not like '000' order by dwdm asc";
                    DataSet ds1 = AccessHelper.getDataSet(sql_dwxx1);
                    int i = 0;
                    while (i < ds1.Tables[0].Rows.Count)
                    {
                        TreeNode newNode1 = treeView1.Nodes.Add(ds1.Tables[0].Rows[i]["id"].ToString(), ds1.Tables[0].Rows[i]["dwmc"].ToString(), 0, 1);
                        newNode1.ToolTipText = ds1.Tables[0].Rows[i]["dwdm"].ToString();
                        i++;
                    }
                }
            }

            //获取系统中最大的单位代码
            String sql_selectmaxdwdm = "select max(dwdm) as maxdwdm from t_dwxx where len(dwdm)<4 and dwdm not like '000'";
            DataSet ds = AccessHelper.getDataSet(sql_selectmaxdwdm);
            int maxdwdm = 0;
            if (ds.Tables[0].Rows[0]["maxdwdm"].ToString() == "")
            {
                maxdwdmnew = "001";
                maxdwdm = 0;
            }
            else
            {
                maxdwdm = Int32.Parse(ds.Tables[0].Rows[0]["maxdwdm"].ToString());
                //MessageBox.Show(maxdwdm.ToString());
                maxdwdm++;
                if (maxdwdm < 10)
                {
                    maxdwdmnew = "00" + maxdwdm.ToString();
                }
                else if (maxdwdm < 100)
                {
                    maxdwdmnew = "0" + maxdwdm.ToString();
                }
                else if (maxdwdm < 1000)
                {
                    maxdwdmnew = maxdwdm.ToString();
                }
                else
                {
                    MessageBox.Show("最多增加999个下级单位！");
                    return;
                }
            }
            treeView1.Nodes.Add(maxdwdmnew, "新单位", 0, 1);
            //if (maxdwdm>0)
            //{
            TreeNode newNodeA = treeView1.Nodes.Find(maxdwdmnew, false)[0];
            newNodeA.Checked = true;
            //treeView1.Nodes[maxdwdm - 1].Checked = true;
            //}
            //else
            //{
            //    treeView1.Nodes[maxdwdm].Checked = true;
            //}
            textBox_dwdm.Text = maxdwdmnew;
            textBox_dwmc.Text = "新单位";
            comboBox_dwxz.SelectedIndex = -1;
            comboBox_dwjb.SelectedIndex = -1;
            comboBox_dwlx.SelectedIndex = -1;
            comboBox_szss.SelectedIndex = 34;
            cbS.SelectedIndex = -1;
            cbX.SelectedIndex = -1;

            textBox_xxdz.Text = "";
            textBox_yzbm.Text = "";
            textBox_lxr.Text = "";
            textBox_lxfs.Text = "";
            textBox_GZKFK.Text = "";
            textBox_BZK.Text = "";
            tbSZZD.Text = "";
            tbYJDD.Text = "";
            modefied = true;//修改数据
            dwid = 0;//标识保存时为增加单位

            tbjxzj.Text = "";
            tbjxsj.Text = "";
            cbnxsh.Text = "";
            cbnxs.Text = "";
            tbjsd.Text = "";
            cbsfxhwp.Checked = false;
            dtxhsj.Text = "";
            tbxxdd.Text = "";
        }

        public void GetNXJJS()
        {
            string sql = "select mc from t_tcwpzdb where lb='拟选交接地点位省'  and fjbh='A' order by ID";
            DataTable dt = m_accessHelper.getDataSet(sql).Tables[0];
            if (dt.Rows.Count > 0)
            {
                DataTable dt1 = new DataTable();
                cbnxsh.DataSource = dt1;
                cbnxsh.DataSource = dt;
                cbnxsh.ValueMember = "mc";
                cbnxsh.DisplayMember = "mc";
            }
        }
        private void button3_Click(object sender, EventArgs e)
        {
            AccessHelper AccessHelper = new AccessHelper();
            String dwdm = "", dwmc = "", dwxz = "", dwjb = "", dwlx = "", szss = "", xxdz = "", yzbm = "", lxr = "", lxfs = "", szs = "", szx = "", yjdd = "", sszd = "", jxzj = "", jxsj = "", nxjjsh = "", nxjjs = "", jsd = "", sfxhwp = "", xhsj = "", xhdd = "";
            int bzk = 0, gzkfk = 0;
            m_dwdm = textBox_dwdm.Text;
            m_dwmc = textBox_dwmc.Text;
            m_dwxz = comboBox_dwxz.Text;
            m_dwjb = comboBox_dwjb.Text;
            m_dwlx = comboBox_dwlx.Text;
            m_szss = comboBox_szss.Text;
            m_xxdz = textBox_xxdz.Text;
            m_yzbm = textBox_yzbm.Text;
            m_lxr = textBox_lxr.Text;
            m_lxfs = textBox_lxfs.Text;
            m_bzk = textBox_BZK.Text;
            m_gzkfk = textBox_GZKFK.Text;

            m_jxzj = tbjxzj.Text;
            m_jxsj = tbjxsj.Text;
            m_nxjjsh = cbnxsh.Text;
            m_nxjjs = cbnxs.Text;
            m_jsd = tbjsd.Text;
            m_sfxhwp = sfxhwpbs.ToString();
            m_xhsj = Convert.ToDateTime(dtxhsj.Text).ToString("yyyy/MM/dd");
            m_xhdd = tbxxdd.Text;

            m_szs = cbS.Text;
            m_szx = cbX.Text;
            m_bzk = "0";
            m_gzkfk = "0";
            m_yjdd = tbYJDD.Text;
            m_sszd = tbSZZD.Text;

            jxzj = m_jxzj;
            jxsj = m_jxsj;
            nxjjsh = m_nxjjsh;
            nxjjs = m_nxjjs;
            jsd = m_jsd;
            sfxhwp = m_sfxhwp;
            xhsj = m_xhsj;
            xhdd = m_xhdd;
            //if (m_dwdm=="000" &&(m_bzk.Trim() == "" || m_gzkfk.Trim() == ""))
            //{
            //    MessageBox.Show("保障卡和工资卡辅卡信息未填写完整！", "系统提示", MessageBoxButtons.OK, MessageBoxIcon.Hand);
            //    return;
            //}
            //if (m_dwdm != "000" && m_bzk.Trim() == "")
            //{
            //    m_bzk ="0";
            //};
            //if (m_dwdm != "000" && m_gzkfk.Trim() == "")
            //{
            //    m_gzkfk = "0";
            //}
            szs = m_szs;
            szx = m_szx;
            dwdm = m_dwdm;
            dwmc = m_dwmc;
            dwxz = m_dwxz;
            dwjb = m_dwjb;
            dwlx = m_dwlx;
            szss = m_szss;
            xxdz = m_xxdz;
            yzbm = m_yzbm;
            lxr = m_lxr;
            lxfs = m_lxfs;
            bzk = int.Parse(m_bzk);
            gzkfk = int.Parse(m_gzkfk);
            yjdd = m_yjdd;
            sszd = m_sszd;

            if (dwmc == "新单位" || dwjb == "" || dwlx == "" || yzbm == "" || lxr == ""  || jxzj == "" || jxsj == "" || nxjjsh == "" || nxjjs == "" || jsd == "")
            {
                MessageBox.Show("本级单位信息必须填写完整！", "系统提示", MessageBoxButtons.OK, MessageBoxIcon.Hand);
                return;
            }
            if (m_szss == "台湾省" || m_szss == "香港特别行政区" || m_szss == "澳门特别行政区")
            {
                szs = "";
                szx = "";
            }
            if (sfxhwp != "1")
            {
                if (xhdd == "")
                {
                    MessageBox.Show("本级单位信息必须填写完整！", "系统提示", MessageBoxButtons.OK, MessageBoxIcon.Hand);
                    return;
                }
            }

            if (login.LogName == "zbgly")
            {
                if (treeView1.SelectedNode != null)
                {
                    slected_dwdm = treeView1.SelectedNode.Name.ToString();
                    string sql_update = "update t_dwxx set dwmc='" + dwmc + "',dwxz='" + dwxz + "',dwjb='" + dwjb + "',dwlx='" + dwlx + "',szss='" + szss + "',xxdz='" + xxdz + "',yzbm='" + yzbm + "',lxr='" + lxr + "',lxfs='" + lxfs + "',bzk='" + bzk + "',gzkfk='" + gzkfk + "' ,szs='" + szs + "',szx='" + szx + "',sszd='" + szss + "—" + szs + "—" + szx + "—" + xxdz + "',jxzj='" + jxzj + "',jxsj='" + jxsj + "',nxsh='" + nxjjsh + "',nxs='" + nxjjs + "',nxjsd='" + jsd + "',sfxhwp='" + sfxhwp + "',xhsj='" + xhsj + "',xhdd='" + xhdd + "' where dwdm='" + slected_dwdm + "'";

                    int updatenumA = AccessHelper.ExcueteCommand(sql_update);
                    if (updatenumA > 0)
                    {
                        MessageBox.Show("数据更改成功！");
                        modefied = false; //没有修改数据
                        load_danwei();
                    }
                    load_danwei();
                }
            }
            else
            {

                if (dwdm.Length < 3)
                {
                    MessageBox.Show("不能保存数据，请先点击增加按钮！", "系统提示", MessageBoxButtons.OK, MessageBoxIcon.Hand);
                    return;
                }

              

                String sql_add_dw = "insert into t_dwxx(dwdm,dwmc,dwxz,dwjb,dwlx,szss,xxdz,yzbm,lxr,lxfs,bzk,gzkfk,szs,szx,yjdd,sszd,jxzj,jxsj,nxsh,nxs,nxjsd,sfxhwp,xhsj,xhdd) values(@DWDM,@DWMC,@DWXZ,@DWJB,@DWLX,@SZSS,@XXDZ,@YZBM,@LXR,@LXFS,@BZK,@GZKFK,@SZS,@SZX,@YJDD,@SSZD,@JYZJ,@JXSJ,@NXSH,@NXS,@NXJSD,@SFXHWP,@XHSJ,@XHDD)";
                OleDbParameter[] parms = new OleDbParameter[] {
                new OleDbParameter("@DWDM",OleDbType.VarChar),
                new OleDbParameter("@DWMC",OleDbType.VarChar),
                new OleDbParameter("@DWXZ",OleDbType.VarChar),
                new OleDbParameter("@DWJB",OleDbType.VarChar),
                new OleDbParameter("@DWLX",OleDbType.VarChar),
                new OleDbParameter("@SZSS",OleDbType.VarChar),
                new OleDbParameter("@XXDZ",OleDbType.VarChar),
                new OleDbParameter("@YZBM",OleDbType.VarChar),
                new OleDbParameter("@LXR",OleDbType.VarChar),
                new OleDbParameter("@LXFS",OleDbType.VarChar),
                new OleDbParameter("@BZK",OleDbType.Integer),
                new OleDbParameter("@GZKFK",OleDbType.Integer),
                new OleDbParameter("@SZS",OleDbType.VarChar),
                new OleDbParameter("@SZX",OleDbType.VarChar),
                new OleDbParameter("@YJDD",OleDbType.VarChar),
                new OleDbParameter("@SSZD",OleDbType.VarChar),

                new OleDbParameter("@JYZJ",OleDbType.VarChar),
                new OleDbParameter("@JXSJ",OleDbType.VarChar),
                new OleDbParameter("@NXSH",OleDbType.VarChar),
                new OleDbParameter("@NXS",OleDbType.VarChar),
                new OleDbParameter("@NXJSD",OleDbType.VarChar),
                new OleDbParameter("@SFXHWP",OleDbType.VarChar),
                new OleDbParameter("@XHSJ",OleDbType.VarChar),
                new OleDbParameter("@XHDD",OleDbType.VarChar)
            };
                parms[0].Value = dwdm;
                parms[1].Value = dwmc;
                parms[2].Value = dwxz;
                parms[3].Value = dwjb;
                parms[4].Value = dwlx;
                parms[5].Value = szss;
                parms[6].Value = xxdz;
                parms[7].Value = yzbm;
                parms[8].Value = lxr;
                parms[9].Value = lxfs;
                parms[10].Value = bzk;
                parms[11].Value = gzkfk;
                parms[12].Value = szs;
                parms[13].Value = szx;
                parms[14].Value = string.Empty;
                parms[15].Value = szss + "—" + szs + "—" + szx + "—" + xxdz;
                parms[16].Value = jxzj;
                parms[17].Value = jxsj;
                parms[18].Value = nxjjsh;
                parms[19].Value = nxjjs;
                parms[20].Value = jsd;
                parms[21].Value = sfxhwp;
                parms[22].Value = xhsj;
                parms[23].Value = xhdd;

                OleDbParameter[] parms1 = new OleDbParameter[] {
                new OleDbParameter("@DWMC",OleDbType.VarChar),
                new OleDbParameter("@DWXZ",OleDbType.VarChar),
                new OleDbParameter("@DWJB",OleDbType.VarChar),
                new OleDbParameter("@DWLX",OleDbType.VarChar),
                new OleDbParameter("@SZSS",OleDbType.VarChar),
                new OleDbParameter("@XXDZ",OleDbType.VarChar),
                new OleDbParameter("@YZBM",OleDbType.VarChar),
                new OleDbParameter("@LXR",OleDbType.VarChar),
                new OleDbParameter("@LXFS",OleDbType.VarChar),
                new OleDbParameter("@BZK",OleDbType.Integer),
                new OleDbParameter("@GZKFK",OleDbType.Integer),
                new OleDbParameter("@DWDM",OleDbType.VarChar),
                new OleDbParameter("@SZS",OleDbType.VarChar),
                new OleDbParameter("@SZX",OleDbType.VarChar),
                new OleDbParameter("@YJDD",OleDbType.VarChar),
                new OleDbParameter("@SSZD",OleDbType.VarChar),

                new OleDbParameter("@JYZJ",OleDbType.VarChar),
                new OleDbParameter("@JXSJ",OleDbType.VarChar),
                new OleDbParameter("@NXSH",OleDbType.VarChar),
                new OleDbParameter("@NXS",OleDbType.VarChar),
                new OleDbParameter("@NXJSD",OleDbType.VarChar),
                new OleDbParameter("@SFXHWP",OleDbType.VarChar),
                new OleDbParameter("@XHSJ",OleDbType.VarChar),
                new OleDbParameter("@XHDD",OleDbType.VarChar)
            };
                parms1[0].Value = dwmc;
                parms1[1].Value = dwxz;
                parms1[2].Value = dwjb;
                parms1[3].Value = dwlx;
                parms1[4].Value = szss;
                parms1[5].Value = xxdz;
                parms1[6].Value = yzbm;
                parms1[7].Value = lxr;
                parms1[8].Value = lxfs;
                parms1[9].Value = bzk;
                parms1[10].Value = gzkfk;
                parms1[11].Value = dwdm;
                parms1[12].Value = szs;
                parms1[13].Value = szx;
                parms1[14].Value = string.Empty;
                parms1[15].Value = szss + "—" + szs + "—" + szx + "—" + xxdz;
                parms[16].Value = jxzj;
                parms[17].Value = jxsj;
                parms[18].Value = nxjjsh;
                parms[19].Value = nxjjs;
                parms[20].Value = jsd;
                parms[21].Value = sfxhwp;
                parms[22].Value = xhsj;
                parms[23].Value = xhdd;


                int updatenum = 0;
                //判断是增加数据还是修改数据
                if (dwid > 0)
                {
                    string sql_update = "update t_dwxx set dwmc='" + dwmc + "',dwxz='" + dwxz + "',dwjb='" + dwjb + "',dwlx='" + dwlx + "',szss='" + szss + "',xxdz='" + xxdz + "',yzbm='" + yzbm + "',lxr='" + lxr + "',lxfs='" + lxfs + "',bzk='" + bzk + "',gzkfk='" + gzkfk + "' ,szs='" + szs + "',szx='" + szx + "',sszd='" + szss + "—" + szs + "—" + szx + "—" + xxdz + "',jxzj='" + jxzj + "',jxsj='" + jxsj + "',nxsh='" + nxjjsh + "',nxs='" + nxjjs + "',nxjsd='" + jsd + "',sfxhwp='" + sfxhwp + "',xhsj='" + xhsj + "',xhdd='" + xhdd + "' where dwdm='" + dwdm + "'";

                    updatenum = AccessHelper.ExcueteCommand(sql_update);
                }
                else
                {
                    updatenum = AccessHelper.ExcueteCommand(sql_add_dw, parms);
                }
                if (updatenum > 0)
                {
                    MessageBox.Show("数据保存成功！");
                    modefied = false; //没有修改数据
                    load_danwei();
                }
                else
                {
                    MessageBox.Show("数据保存失败！");
                }
            }

        }

        private void comboBox_dwxz_SelectedIndexChanged(object sender, EventArgs e)
        {

            comboBox_dwjb.Enabled = true;
            comboBox_dwlx.Enabled = true;

        }
        //提取单位信息显示到树形列表
        private void load_danwei()
        {

            if (login.LogName == "zbgly")
            {
                treeView1.Nodes.Clear();
                string tempSQL = "SELECT  dwdm,dwmc,id FROM  t_dwxx   ORDER BY  t_dwxx.dwdm, t_dwxx.ID";
                DataSet ds = m_accessHelper.getDataSet(tempSQL);
                for (int i = 0; i < ds.Tables[0].Rows.Count; i++)
                {
                    string dwdm = ds.Tables[0].Rows[i]["dwdm"].ToString();
                  //  int length = 0;
                    if (dwdm == "000")
                    {
                        TreeNode newNode1 = treeView1.Nodes.Add(ds.Tables[0].Rows[i]["dwdm"].ToString(), ds.Tables[0].Rows[i]["dwdm"].ToString() + ds.Tables[0].Rows[i]["dwmc"].ToString());
                    }
                    else
                    {
                        if (dwdm.Length == 3)//二级
                        {
                            TreeNode newNode1 = treeView1.Nodes.Find("000", false)[0];
                            newNode1.Nodes.Add(ds.Tables[0].Rows[i]["dwdm"].ToString(), ds.Tables[0].Rows[i]["dwdm"].ToString() + ds.Tables[0].Rows[i]["dwmc"].ToString());
                        }
                        else if (dwdm.Length == 6)//三级
                        {
                            TreeNode newNodeA = treeView1.Nodes.Find("000", false)[0];
                            string fadm1 = dwdm.Substring(0, 3);
                            TreeNode newNode1 = newNodeA.Nodes.Find(fadm1, false)[0];
                            newNode1.Nodes.Add(ds.Tables[0].Rows[i]["dwdm"].ToString(), ds.Tables[0].Rows[i]["dwdm"].ToString() + ds.Tables[0].Rows[i]["dwmc"].ToString());
                        }
                        else if (dwdm.Length == 9)//四级
                        {
                            TreeNode newNodeA = treeView1.Nodes.Find("000", false)[0];
                            string fadm1 = dwdm.Substring(0, 3);
                            TreeNode newNode1 = newNodeA.Nodes.Find(fadm1, false)[0];
                            string fadm2 = dwdm.Substring(0, 6);
                            TreeNode newNode2 = newNode1.Nodes.Find(fadm2, false)[0];
                            newNode2.Nodes.Add(ds.Tables[0].Rows[i]["dwdm"].ToString(), ds.Tables[0].Rows[i]["dwdm"].ToString() + ds.Tables[0].Rows[i]["dwmc"].ToString());
                        }
                        else if (dwdm.Length == 12)//五级
                        {
                            TreeNode newNodeA = treeView1.Nodes.Find("000", false)[0];
                            string fadm1 = dwdm.Substring(0, 3);
                            TreeNode newNode1 = newNodeA.Nodes.Find(fadm1, false)[0];
                            string fadm2 = dwdm.Substring(0, 6);
                            TreeNode newNode2 = newNode1.Nodes.Find(fadm2, false)[0];
                            string fadm3 = dwdm.Substring(0, 9);
                            TreeNode newNode3 = newNode2.Nodes.Find(fadm3, false)[0];
                            newNode3.Nodes.Add(ds.Tables[0].Rows[i]["dwdm"].ToString(), ds.Tables[0].Rows[i]["dwdm"].ToString() + ds.Tables[0].Rows[i]["dwmc"].ToString());
                        }
                        else if (dwdm.Length == 15)//六级
                        {
                            TreeNode newNodeA = treeView1.Nodes.Find("000", false)[0];
                            string fadm1 = dwdm.Substring(0, 3);
                            TreeNode newNode1 = newNodeA.Nodes.Find(fadm1, false)[0];
                            string fadm2 = dwdm.Substring(0, 6);
                            TreeNode newNode2 = newNode1.Nodes.Find(fadm2, false)[0];
                            string fadm3 = dwdm.Substring(0, 9);
                            TreeNode newNode3 = newNode2.Nodes.Find(fadm3, false)[0];
                            string fadm4 = dwdm.Substring(0, 12);
                            TreeNode newNode4 = newNode3.Nodes.Find(fadm4, false)[0];
                            newNode4.Nodes.Add(ds.Tables[0].Rows[i]["dwdm"].ToString(), ds.Tables[0].Rows[i]["dwdm"].ToString() + ds.Tables[0].Rows[i]["dwmc"].ToString());
                        }
                        else if (dwdm.Length == 18)//七级
                        {
                            TreeNode newNodeA = treeView1.Nodes.Find("000", false)[0];
                            string fadm1 = dwdm.Substring(0, 3);
                            TreeNode newNode1 = newNodeA.Nodes.Find(fadm1, false)[0];
                            string fadm2 = dwdm.Substring(0, 6);
                            TreeNode newNode2 = newNode1.Nodes.Find(fadm2, false)[0];
                            string fadm3 = dwdm.Substring(0, 9);
                            TreeNode newNode3 = newNode2.Nodes.Find(fadm3, false)[0];
                            string fadm4 = dwdm.Substring(0, 12);
                            TreeNode newNode4 = newNode3.Nodes.Find(fadm4, false)[0];
                            string fadm5 = dwdm.Substring(0, 15);
                            TreeNode newNode5 = newNode4.Nodes.Find(fadm5, false)[0];
                            newNode5.Nodes.Add(ds.Tables[0].Rows[i]["dwdm"].ToString(), ds.Tables[0].Rows[i]["dwdm"].ToString() + ds.Tables[0].Rows[i]["dwmc"].ToString());
                        }
                    }
                }
            }
            else
            {
                treeView1.Nodes.Clear();
                treeView1.ShowLines = true;
                String sql_dwxx = "select id,dwdm,dwmc from t_dwxx where len(dwdm)<4 and dwdm not like '000' order by dwdm asc";
                AccessHelper AccessHelper = new AccessHelper();
                DataSet ds = AccessHelper.getDataSet(sql_dwxx);
                for (int i = 0; i < ds.Tables[0].Rows.Count; i++)
                {
                    TreeNode newNode1 = treeView1.Nodes.Add(ds.Tables[0].Rows[i]["id"].ToString(), ds.Tables[0].Rows[i]["dwmc"].ToString(), 0, 1);
                    newNode1.ToolTipText = ds.Tables[0].Rows[i]["dwdm"].ToString();
                }
                //while (i < ds.Tables[0].Rows.Count)
                //{
                //    TreeNode newNode1 = treeView1.Nodes.Add(ds.Tables[0].Rows[i]["id"].ToString(),ds.Tables[0].Rows[i]["dwmc"].ToString(), 0, 1);
                //    newNode1.ToolTipText = ds.Tables[0].Rows[i]["dwdm"].ToString();
                //    i++;
                //}
                if (treeView1.Nodes.Count > 0)
                {
                    treeView1.SelectedNode = treeView1.Nodes[0]; //默认选中第一个单位
                    dwid = 1;
                }
                setsaved();
            }

        }

        private void btnDelete_Click(object sender, EventArgs e)
        {

            if (login.LogName == "zbgly")
            {
                if (treeView1.SelectedNode != null)
                {
                    slected_dwdm = treeView1.SelectedNode.Name.ToString();
                    AccessHelper AccessHelper = new AccessHelper();
                    String sql_del_repeat = "delete from t_dwxx where left(dwdm,3)='" + slected_dwdm + "'";
                    AccessHelper.ExcueteCommand(sql_del_repeat);
                    sql_del_repeat = "delete from t_BM where left(dwdm,3)='" + slected_dwdm + "'";
                    AccessHelper.ExcueteCommand(sql_del_repeat);
                    sql_del_repeat = "delete from t_LCTC where left(dwdm,3)='" + slected_dwdm + "'";
                    AccessHelper.ExcueteCommand(sql_del_repeat);
                    //sql_del_repeat = "delete from t_yhzh where left(dwdm,3)='" + slected_dwdm + "'";
                    //AccessHelper.ExcueteCommand(sql_del_repeat);
                    //sql_del_repeat = "delete from t_yhzhqccl where left(dwdm,3)='" + slected_dwdm + "'";
                    //AccessHelper.ExcueteCommand(sql_del_repeat);
                    //sql_del_repeat = "delete from t_zjjc where left(dwdm,3)='" + slected_dwdm + "'";
                    //AccessHelper.ExcueteCommand(sql_del_repeat);
                    //sql_del_repeat = "delete from t_gwk where left(dwdm,3)='" + slected_dwdm + "'";
                    //AccessHelper.ExcueteCommand(sql_del_repeat);

                    DeleteData(slected_dwdm);
                    slected_dwdm = "";
                    load_danwei();
                }
            }
            else
            {
                if (slected_dwdm != "" && slected_dwdm != "000")
                {
                    MessageBoxButtons msgBut = MessageBoxButtons.OKCancel;
                    DialogResult dr = MessageBox.Show("删除此单位将同时删除该单位下的部门，物品明细等信息，确定删除此单位吗？", "删除数据", msgBut, MessageBoxIcon.Question);
                    if (dr == DialogResult.OK)
                    {
                        //删除单位已经存在的数据
                        AccessHelper AccessHelper = new AccessHelper();
                        String sql_del_repeat = "delete from t_dwxx where left(dwdm,3)='" + slected_dwdm + "'";
                        AccessHelper.ExcueteCommand(sql_del_repeat);
                        sql_del_repeat = "delete from t_BM where left(dwdm,3)='" + slected_dwdm + "'";
                        AccessHelper.ExcueteCommand(sql_del_repeat);
                        sql_del_repeat = "delete from t_LCTC where left(dwdm,3)='" + slected_dwdm + "'";
                        AccessHelper.ExcueteCommand(sql_del_repeat);
                        //sql_del_repeat = "delete from t_yhzh where left(dwdm,3)='" + slected_dwdm + "'";
                        //AccessHelper.ExcueteCommand(sql_del_repeat);
                        //sql_del_repeat = "delete from t_yhzhqccl where left(dwdm,3)='" + slected_dwdm + "'";
                        //AccessHelper.ExcueteCommand(sql_del_repeat);
                        //sql_del_repeat = "delete from t_zjjc where left(dwdm,3)='" + slected_dwdm + "'";
                        //AccessHelper.ExcueteCommand(sql_del_repeat);
                        //sql_del_repeat = "delete from t_gwk where left(dwdm,3)='" + slected_dwdm + "'";
                        //AccessHelper.ExcueteCommand(sql_del_repeat);

                        DeleteData(slected_dwdm);
                        slected_dwdm = "";
                        load_danwei();
                    }
                }
            }

        }

        public void DeleteData(string data)
        {
            OleDbParameter[] parms = new OleDbParameter[] {
                new OleDbParameter("@DWDM",OleDbType.VarChar)
            };
            parms[0].Value = data;
            AccessHelper tempAccessHelper = new AccessHelper();
            tempAccessHelper.ExcueteCommand(SQL_Admin_Delete, parms);
        }

        private void danwei_FormClosing(object sender, FormClosingEventArgs e)
        {

            String sql_dwxx = "select dwmc from t_dwxx where dwdm='000'";
            AccessHelper AccessHelper = new AccessHelper();
            DataSet ds = AccessHelper.getDataSet(sql_dwxx);
            if (ds.Tables[0].Rows.Count == 0 && a != "1")
            {

                DialogResult dr = MessageBox.Show("未设置本单位基本信息，是否确定保存数据？", "系统提示", MessageBoxButtons.OKCancel, MessageBoxIcon.Question);
                if (dr == DialogResult.OK)
                {
                    a = "";
                    this.button3_Click(sender, e);
                    e.Cancel = true;
                }
                else
                {
                    a = "1";
                    e.Cancel = false;
                    this.Close();
                }
            }
            else
            {
                e.Cancel = false;
            }
            judge();
            if (modefied == true && a != "1")
            {
                DialogResult dr = MessageBox.Show("单位信息已经修改,是否确定保存数据？", "系统提示", MessageBoxButtons.OKCancel, MessageBoxIcon.Question);
                if (dr == DialogResult.OK)
                {
                    a = "";
                    this.button3_Click(sender, e);
                    e.Cancel = true;
                }
                else
                {
                    a = "1";
                    e.Cancel = false;
                    this.Close();
                }
            }

        }

        private void textBox_BZK_TextChanged(object sender, EventArgs e)
        {
            ClassInputRestricter.CheckNum(sender);
        }

        private void textBox_GZKFK_TextChanged(object sender, EventArgs e)
        {
            ClassInputRestricter.CheckNum(sender);
        }

        private void button_xj_Click(object sender, EventArgs e)
        {
            FrmBZK Frm_BZK = new FrmBZK();
            Frm_BZK.ShowDialog();
        }
    }
}
