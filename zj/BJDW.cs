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
    public partial class BJDW : Form
    {
        private AccessHelper m_accessHelper = new AccessHelper();
        bool sjbs = false;
        int sfxhwpbs = 0;
        string czbs,rtdwbsm="";
        public BJDW()
        {
            InitializeComponent();
            Load += new EventHandler(FrmBcardDW_Load);
        }

        private void button1_Click(object sender, EventArgs e)
        {
            string dwdm = "", dwmc = "", dwxz = "", dwjb = "", dwlx = "", szss = "", xxdz = "", yzbm = "", lxr = "", lxfs = "";
            dwdm = textBox_dwdm.Text;
            dwmc = textBox_dwmc.Text;
            dwxz = comboBox_dwxz.Text;
            dwjb = comboBox_dwjb.Text;
            dwlx = comboBox_dwlx.Text;
            szss = comboBox_szss.Text;
            xxdz = textBox_xxdz.Text;
            yzbm = textBox_yzbm.Text;
            lxr = textBox_lxr.Text;
            lxfs = textBox_lxfs.Text;

        }
        /// <summary>
        /// 页面加载方法
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void FrmBcardDW_Load(object sender, EventArgs e)
        {
            //选交接地点省
            GetNXJJS();
            string tempSQL = "SELECT * FROM t_dwxx where dwdm='000'";
            DataTable dt = m_accessHelper.getDataSet(tempSQL).Tables[0];
            if (dt.Rows.Count > 0)
            {
                textBox_dwmc.Text = dt.Rows[0]["dwmc"].ToString();
                comboBox_dwxz.Text = dt.Rows[0]["dwxz"].ToString();
                comboBox_dwjb.Text = dt.Rows[0]["dwjb"].ToString();
                comboBox_dwlx.Text = dt.Rows[0]["dwlx"].ToString();
                comboBox_szss.Text = dt.Rows[0]["szss"].ToString();
                textBox_xxdz.Text = dt.Rows[0]["xxdz"].ToString();
                textBox_lxr.Text = dt.Rows[0]["lxr"].ToString();
                textBox_lxfs.Text = dt.Rows[0]["lxfs"].ToString();
                textBox_yzbm.Text = dt.Rows[0]["yzbm"].ToString();
                cbS.Text = dt.Rows[0]["szs"].ToString();
                cbX.Text = dt.Rows[0]["szx"].ToString();
                tbYJDD.Text = dt.Rows[0]["yjdd"].ToString();
                tbSZZD.Text = dt.Rows[0]["sszd"].ToString();
                BJ.Enabled = true;


                 tbjxzj.Text=dt.Rows[0]["jxzj"].ToString();
                tbjxsj.Text= dt.Rows[0]["jxsj"].ToString();
                 cbnxsh.Text= dt.Rows[0]["nxsh"].ToString();
                 cbnxs.Text= dt.Rows[0]["nxs"].ToString();
                tbjsd.Text= dt.Rows[0]["nxjsd"].ToString();
                rtdwbsm = dt.Rows[0]["dwbs"].ToString();
                if (dt.Rows[0]["sfxhwp"].ToString() == "1")
                {
                    cbsfxhwp.Checked=true;
                }
                else
                {
                    cbsfxhwp.Checked = false;
                }
                dtxhsj.Text= dt.Rows[0]["xhsj"].ToString();
                 tbxxdd.Text= dt.Rows[0]["xhdd"].ToString();
            }
            else
            {
                BJ.Enabled = false;
            }

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
            string dwdm = "", dwmc = "", dwxz = "", dwjb = "", dwlx = "", szss = "", xxdz = "", yzbm = "", lxr = "", lxfs = "", szs = "", szx = "", yjdd = "", szzd = "", jxzj = "", jxsj = "", nxjjsh = "", nxjjs = "", jsd = "", sfxhwp = "", xhsj = "", xhdd = "";
            dwdm = textBox_dwdm.Text;
            dwmc = textBox_dwmc.Text;
            dwxz = comboBox_dwxz.Text;
            dwjb = comboBox_dwjb.Text;
            dwlx = comboBox_dwlx.Text;
            szss = comboBox_szss.Text;
            xxdz = textBox_xxdz.Text;
            yzbm = textBox_yzbm.Text;
            lxr = textBox_lxr.Text;
            lxfs = textBox_lxfs.Text;
            szs = cbS.Text;
            szx = cbX.Text;
            yjdd = tbYJDD.Text;
            szzd = tbSZZD.Text;

            jxzj = tbjxzj.Text;
            jxsj = tbjxsj.Text;
            nxjjsh = cbnxsh.Text;
            nxjjs = cbnxs.Text;
            jsd = tbjsd.Text;
            sfxhwp = sfxhwpbs.ToString();
            xhsj = Convert.ToDateTime(dtxhsj.Text).ToString("yyyy/MM/dd");
            xhdd = tbxxdd.Text;

            if ( dwjb == "" || dwlx == "" || szss == "" || yzbm == "" || lxr == ""  || jxzj == "" || jxsj == "" || nxjjsh == "" || nxjjs == "" || jsd == "")
            {
                MessageBox.Show("本级单位信息必须填写完整！", "系统提示", MessageBoxButtons.OK, MessageBoxIcon.Hand);
                return;
            }
            if (sfxhwp != "1")
            {
                if ( xhdd == "")
                {
                    MessageBox.Show("本级单位信息必须填写完整！", "系统提示", MessageBoxButtons.OK, MessageBoxIcon.Hand);
                    return;
                }
            }

            string sql_delete_dw = "delete from t_dwxx where dwdm='000'";
            int num = m_accessHelper.ExcueteCommand(sql_delete_dw);
            if (szss == "台湾省" || szss == "香港特别行政区" || szss == "澳门特别行政区")
            {
                szs = "";
                szx = "";
            }
            string sql_add_dw = "insert into t_dwxx(dwdm,dwmc,dwxz,dwjb,dwlx,szss,xxdz,yzbm,lxr,lxfs,szs,szx,yjdd,sszd,jxzj,jxsj,nxsh,nxs,nxjsd,sfxhwp,xhsj,xhdd,dwbs) values(@DWDM,@DWMC,@DWXZ,@DWJB,@DWLX,@SZSS,@XXDZ,@YZBM,@LXR,@LXFS,@SZS,@SZX,@YJDD,@SSZD,@JYZJ,@JXSJ,@NXSH,@NXS,@NXJSD,@SFXHWP,@XHSJ,@XHDD,@DWBS)";
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
                new OleDbParameter("@XHDD",OleDbType.VarChar),
                new OleDbParameter("@DWBS",OleDbType.VarChar)
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
            parms[10].Value = szs;
            parms[11].Value = szx;
            parms[12].Value = string.Empty;
            parms[13].Value = szss + "—" + szs + "—" + szx + "—" + xxdz;
            parms[14].Value = jxzj;
            parms[15].Value = jxsj;
            parms[16].Value = nxjjsh;
            parms[17].Value = nxjjs;
            parms[18].Value = jsd;
            parms[19].Value = sfxhwp;
            parms[20].Value = xhsj;
            parms[21].Value = xhdd;
            parms[22].Value = rtdwbsm;


            int updatenum = 0;
            //判断是增加数据还是修改数据

            updatenum = m_accessHelper.ExcueteCommand(sql_add_dw, parms);

            if (updatenum > 0)
            {
                MessageBox.Show("数据保存成功！");

            }
            textBox_dwdm.Enabled = false;
            textBox_dwmc.Enabled = false;
            comboBox_dwxz.Enabled = false;
            comboBox_dwjb.Enabled = false;
            comboBox_dwlx.Enabled = false;
            comboBox_szss.Enabled = false;
            textBox_xxdz.Enabled = false;
            textBox_yzbm.Enabled = false;
            textBox_lxr.Enabled = false;
            textBox_lxfs.Enabled = false;
            cbS.Enabled = false;
            cbX.Enabled = false;
            tbYJDD.Enabled = false;
            tbSZZD.Enabled = false;

            tbjxzj.Enabled = false;
            tbjxsj.Enabled = false;
            cbnxsh.Enabled = false;
            cbnxs.Enabled = false;
            tbjsd.Enabled = false;
            cbsfxhwp.Enabled = false;
            dtxhsj.Enabled = false;
            tbxxdd.Enabled = false;
        }

        private void BJ_Click(object sender, EventArgs e)
        {
            czbs = "1";//编辑
            textBox_dwdm.Enabled = true;
            textBox_dwmc.Enabled = true;
            comboBox_dwxz.Enabled = true;
            comboBox_dwjb.Enabled = true;
            comboBox_dwlx.Enabled = true;
            comboBox_szss.Enabled = true;
            textBox_xxdz.Enabled = true;
            textBox_yzbm.Enabled = true;
            textBox_lxr.Enabled = true;
            textBox_lxfs.Enabled = true;
            cbS.Enabled = true;
            cbX.Enabled = true;
            tbYJDD.Enabled = true;
            tbSZZD.Enabled = true;
            tbjxzj.Enabled = true;
            tbjxsj.Enabled = true;
            cbnxsh.Enabled = true;
            cbnxs.Enabled = true;
            tbjsd.Enabled = true;
            cbsfxhwp.Enabled = true;
            dtxhsj.Enabled = true;
            tbxxdd.Enabled = true;
        }

        private void button4_Click(object sender, EventArgs e)
        {
            //string tempSQL = "SELECT dwdm,dwmc FROM t_dwxx where dwdm='000'";
            //DataSet ds = m_accessHelper.getDataSet(tempSQL);

            //if (ds.Tables[0].Rows.Count == 0)
            //{
            //    MessageBoxButtons msgBut = MessageBoxButtons.OKCancel;
            //    DialogResult dr = MessageBox.Show("本级单位信息不完整，是否继续维护本级单位信息！？", "系统提示", msgBut, MessageBoxIcon.Question);
            //    if (dr == DialogResult.OK)
            //    {

            //    }
            //    else
            //    {
            //        Application.Exit();
            //    }

            //}
            //else
            //{
            this.Close();
            //}
        }

        private void comboBox_szss_SelectedValueChanged(object sender, EventArgs e)
        {

        }

        private void cbS_SelectedValueChanged(object sender, EventArgs e)
        {

        }

        private void button1_Click_1(object sender, EventArgs e)
        {
            OpenFileDialog ofd = new OpenFileDialog();
            ofd.Title = "选择文件";
            ofd.Filter = "Microsoft Excel文件|*.xls;*.xlsx";
            ofd.FilterIndex = 1;
            ofd.DefaultExt = "xls";
            string path = "";
            if (ofd.ShowDialog() == DialogResult.OK)
            {
                if (!ofd.SafeFileName.EndsWith(".xls") && !ofd.SafeFileName.EndsWith(".xlsx"))
                {
                    MessageBox.Show("请选择Excel文件", "文件解析失败!", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    return;
                }
                if (!ofd.CheckFileExists)
                {
                    MessageBox.Show("指定的文件不存在", "请检查！", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    return;
                }
                path = ofd.FileName;
            }
            List<ZDDATA> list = new List<ZDDATA>();
            if (!string.IsNullOrEmpty(path))
            {
                MessageBoxButtons msgBut = MessageBoxButtons.OKCancel;
                DialogResult dr = MessageBox.Show("导入后将清除该部门已录入数据，确定导入该目录下的数据吗？" + path, "名贵特产类物品数据导入", msgBut, MessageBoxIcon.Question);
                if (dr == DialogResult.OK)
                {
                    System.Data.DataTable dt = new System.Data.DataTable();
                    try
                    {
                        dt = AccessHelper.ExcelToDataTable(path, "111");
                    }
                    catch
                    {
                        MessageBox.Show("请选择系统所提供的导入模板", "请检查！", MessageBoxButtons.OK, MessageBoxIcon.Error);
                        return;
                    }
                    if (dt.Rows.Count > 0)
                    {
                        for (int i = 0; i < dt.Rows.Count; i++)
                        {

                            list.Add(new ZDDATA()
                            {
                                mc = dt.Rows[i]["F1"].ToString(),
                                fjbh = dt.Rows[i]["F2"].ToString(),
                                lb = dt.Rows[i]["F3"].ToString(),
                            });
                        }

                        for (int i = 0; i < list.Count; i++)
                        {
                            OleDbParameter[] parms = new OleDbParameter[] {
                    new OleDbParameter("@MC",OleDbType.VarChar),
                    new OleDbParameter("@FJBH",OleDbType.VarChar),
                    new OleDbParameter("@LB",OleDbType.VarChar),

                };
                            parms[0].Value = list[i].mc;
                            parms[1].Value = list[i].fjbh;
                            parms[2].Value = list[i].lb;
                            InsertData(parms);

                        }
                        MessageBox.Show("导入成功！", "系统提示", MessageBoxButtons.OK, MessageBoxIcon.Information);
                        this.Close();
                    }
                    else
                    {
                        return;
                    }

                }

            }

        }

        /// <summary>
        /// 新增
        /// </summary>
        /// <param name="parms"></param>
        public void InsertData(OleDbParameter[] parms)
        {
            m_accessHelper.ExcueteCommand("insert into t_tcwpzdb (mc,fjbh,lb) values (@MC,@Z2FJBH,@LB)", parms);
        }

        public class ZDDATA
        {
            public string mc { get; set; }
            public string fjbh { get; set; }
            public string lb { get; set; }
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
                    //cbS.Text = "";
                    //cbX.Text = "";
                }
            }
        }

        private void cbS_SelectedIndexChanged(object sender, EventArgs e)
        {
            string smc = cbS.Text;
            DataRow sItem = cbS.SelectedItem as DataRow;
            if (!string.IsNullOrEmpty(smc))
            {
                string sql = "select mc from t_tcwpzdb where lb='市' and fjbh='" + smc + "' order by ID";
                DataTable dt = m_accessHelper.getDataSet(sql).Tables[0];
                DataTable dt1 = new DataTable();
                cbX.DataSource = dt1;
                cbX.DataSource = dt;
                cbX.ValueMember = "mc";
                cbX.DisplayMember = "mc";
            }
        }

        private void BJDW_FormClosing(object sender, FormClosingEventArgs e)
        {

            if (sjbs == false)
            {
                sjbs = true;
                e.Cancel = false;
                string tempSQL = "SELECT dwdm,dwmc FROM t_dwxx where dwdm='000'";
                DataSet ds = m_accessHelper.getDataSet(tempSQL);
                if (ds.Tables[0].Rows.Count == 0)
                {
                    MessageBoxButtons msgBut = MessageBoxButtons.OKCancel;
                    DialogResult dr = MessageBox.Show("本级单位信息不完整，是否继续维护本级单位信息！？", "系统提示", msgBut, MessageBoxIcon.Question);
                    if (dr == DialogResult.OK)
                    {
                        sjbs = false;
                        e.Cancel = true;
                    }
                    else
                    {
                        Application.Exit();
                        return;
                    }
                }
                else
                {
                    e.Cancel = false;
                }

            }
        }

        private void tbYJDD_TextChanged(object sender, EventArgs e)
        {
            tbSZZD.Text = tbYJDD.Text;
        }

        private void checkBox1_CheckedChanged(object sender, EventArgs e)
        {
            if (cbsfxhwp.Checked)
            {
                sfxhwpbs = 1;
                tbxxdd.Enabled = false;
                dtxhsj.Enabled = false;

                tbxxdd.Text ="";
                dtxhsj.Text = "2019/1/1";
            }
            else
            {
                tbxxdd.Enabled = true;
                dtxhsj.Enabled = true;
             
                sfxhwpbs = 0;
               
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
    }

}
