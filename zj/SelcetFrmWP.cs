using Microsoft.Office.Interop.Access.Dao;
using NPOI.XSSF.UserModel;
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
    public partial class SelcetFrmWP : Form
    {
        private int startrow = -1;
        public static string selectid = "";
        public static string ids = "";
        public static int dqxh = 0;
        public static string sfxzmj = "0";
        public static string dqzt = "";
        public static string dwdm_tree = "";
        private AccessHelper m_accessHelper = new AccessHelper();
        public string filterStr = "";
        string cfsjbs = string.Empty;
        int m_id;
        private string SQL_Admin_Delete = "DELETE FROM t_lctc WHERE ID=@ID";

        DataTable dt_sjsh = null;//数据审核table
        DataTable dt_cfsj = null;


        public SelcetFrmWP()
        {
            InitializeComponent();
            Load += new EventHandler(FrmBcardTJ_Load);
        }



        private void FrmBcardTJ_Load(object sender, EventArgs e)
        {



            this.WindowState = FormWindowState.Maximized;
            sfxzmj = "1";
            //   LoadComboParams();
            GetAllDataRefreshGridView();

        }

        /// <summary>
        /// 获取名贵物品明细信息
        /// </summary>
        public void GetAllDataRefreshGridView()
        {
            button6.Enabled = true;
            string tempSQL = "select A.ID as ID号,A.wpbs as 物品标识码,'' as 检验异常结果, A.dwmc as 单位名称,B.BMMC as 部门, A.lb as 类别, A.pm as 品名, A.ly as 来源,A.hqsj as 获取时间,  A.sl as 数量,  A.jldw as 计量单位, A.djlx as 单价类型,A.dj as 单价, A.zz as 总值, A.kysl as 堪用数量,A.kbxjz as 可变现价值,A.czfs as 处置方式, A.bz as 备注, A.jjqrsl as 实际接收数量,A.jjbz as 交接备注  FROM(SELECT t_lctc.ID, t_lctc.wpbs,t_dwxx.dwdm, t_dwxx.dwmc, t_lctc.bmbs, t_lctc.lb, t_lctc.pm, t_lctc.ly, t_lctc.hqsj, t_lctc.sl, t_lctc.jldw, t_lctc.djlx, t_lctc.dj, t_lctc.zz, t_lctc.kysl, t_lctc.kbxjz, t_lctc.czfs, t_lctc.bz,t_lctc.jjqrsl,t_lctc.jjbz  FROM t_lctc left join t_dwxx on t_lctc.dwdm = t_dwxx.dwdm) as A left join t_bm as b on A.bmbs = b.bmbs and A.dwdm=b.dwdm    where ''  " + filterStr + " ORDER BY A.dwdm,A.ID";

            //DataGridViewCheckBoxColumn DtCheck = new DataGridViewCheckBoxColumn();
            //DtCheck.DataPropertyName = "check";
            //DtCheck.HeaderText = "选择";
            //dataGridView.Columns.Add(DtCheck);

            DataSet ds = m_accessHelper.getDataSet(tempSQL);
            DataTable dt = ds.Tables[0];
            DataColumn dc = dt.Columns.Add("序号", typeof(int));
            dt.Columns["序号"].SetOrdinal(0);

            dt.Columns.Add("选择", typeof(bool));
            dt.Columns["选择"].SetOrdinal(0);

            for (int i = 0; i < dt.Rows.Count; i++)
            {
                dt.Rows[i]["序号"] = i + 1;
                dt.Rows[i]["选择"] = false;
                dt.Rows[i]["数量"] = Convert.ToDecimal(dt.Rows[i]["数量"].ToString()).ToString("0.#####");
                dt.Rows[i]["堪用数量"] = Convert.ToDecimal(dt.Rows[i]["堪用数量"].ToString()).ToString("0.#####");
            }

            dataGridView.DataSource = dt;
            dt_sjsh = dt;
            dataGridView.Columns[0].Visible = false;
            dataGridView.Columns[2].Visible = false;
            dataGridView.Columns[3].Visible = false;
            dataGridView.Columns[4].Visible = false;
            dataGridView.ClearSelection();
            dataGridView.Columns["ID号"].Width = 30;
            dataGridView.Columns["序号"].Width = 60;
            dataGridView.Columns["单位名称"].Width = 200;
            dataGridView.Columns["部门"].Width = 100;
            dataGridView.Columns["类别"].Width = 60;
            dataGridView.Columns["来源"].Width = 70;
            dataGridView.Columns["获取时间"].Width = 90;
            dataGridView.Columns["计量单位"].Width = 90;
            dataGridView.Columns["单价类型"].Width = 90;
            dataGridView.Columns["处置方式"].Width = 100;
            dataGridView.Columns["可变现价值"].Width = 100;
            dataGridView.Columns["备注"].Width = 320;
            if (dataGridView.SelectedRows.Count != 0)
            {
                m_id = int.Parse(dataGridView.SelectedRows[0].Cells[1].Value.ToString());
            }
            else
            {
                m_id = ClassConstants.JD_NOTSELECTED;
            }
            button12.Visible = false;
            button13.Visible = false;
            button14.Visible = false;
            button15.Visible = false;
            button16.Visible = false;
            button17.Visible = false;
        }

        ///// <summary>
        ///// 单位赋值
        ///// </summary>
        //private void LoadComboParams()
        //{
        //    string tempSQL = "SELECT dwdm,dwmc FROM t_dwxx";
        //    DataSet ds = m_accessHelper.getDataSet(tempSQL);

        //    comboDWDM.DataSource = ds.Tables[0];
        //    if (ds.Tables[0].Rows.Count == 0)
        //    {
        //        MessageBox.Show("请先录入单位信息！", "系统提示", MessageBoxButtons.OK, MessageBoxIcon.Information);
        //    }
        //    comboDWDM.ValueMember = "dwdm";
        //    comboDWDM.DisplayMember = "dwmc";
        //}

        private void button2_Click(object sender, EventArgs e)
        {

        }

        private void button4_Click(object sender, EventArgs e)
        {
            //XSSFWorkbook wb = new XSSFWorkbook();
            //NPOI.SS.UserModel.ISheet sheet = wb.CreateSheet("管理员信息");
            //NPOI.SS.UserModel.IRow row = sheet.CreateRow(0);
            //NPOI.SS.UserModel.ICell cell0 = row.CreateCell(0);
            //cell0.SetCellValue("店员信息表");




        }

        private void button5_Click(object sender, EventArgs e)
        {

        }

        private void dataGridView_SelectionChanged(object sender, EventArgs e)
        {
            if (dataGridView.SelectedRows.Count != 0)
            {
                selectid = dataGridView.SelectedRows[0].Cells[1].Value.ToString();
            }
        }

        /// <summary>
        /// 查看
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void button7_Click(object sender, EventArgs e)
        {

        }

        /// <summary>
        /// 编辑
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void button6_Click(object sender, EventArgs e)
        {

        }

        private void btnRemove_Click(object sender, EventArgs e)
        {

        }
        /// <summary>
        /// 删除
        /// </summary>
        /// <param name="dataIndex"></param>
        public void DeleteData(int dataIndex)
        {
            string sql_bj = "select dwdm from t_lctc where id=" + dataIndex;
            DataTable dt = m_accessHelper.getDataSet(sql_bj).Tables[0];
            if (dt.Rows.Count > 0)
            {
                string dwdm = dt.Rows[0]["dwdm"].ToString();
                if (dwdm != "000")
                {
                    MessageBox.Show("只可删除本级单位信息!");
                    return;
                }
            }

            OleDbParameter[] parms = new OleDbParameter[] {
                new OleDbParameter("@ID",OleDbType.Integer)
            };
            parms[0].Value = dataIndex;

            m_accessHelper.ExcueteCommand(SQL_Admin_Delete, parms);
        }

        private void comboDWDM_SelectedValueChanged(object sender, EventArgs e)
        {

        }

        private void button8_Click(object sender, EventArgs e)
        {
            //comboDWDM.SelectedValue = "";
            //comboDWDM.SelectedText = "";
            cbLY.Items.Add("");
            cbLY.Text = "";
            cbCZFS.Items.Add("");
            cbCZFS.Text = "";
            cbLB.Items.Add("");
            cbLB.Text = "";
            dtHQSJ.Items.Add("");
            dtHQSJ.Text = "";
            cbPM.Text = "";
        }

        private void dataGridView_DoubleClick(object sender, EventArgs e)
        {
            dqzt = "0";
            for (int i = 0; i < this.MdiChildren.Count(); i++)
            {
                this.MdiChildren[i].Close();
            }

            FrmBcardTCWP tcwp = new FrmBcardTCWP();
            tcwp.MdiParent = this;

            tcwp.Show();
        }

        private void fileMenu_Click(object sender, EventArgs e)
        {
            dqzt = "1";
            FrmBcardTCWP tcwp = new FrmBcardTCWP();
            tcwp.Show();
        }

        private void editMenu_Click(object sender, EventArgs e)
        {
            dqzt = "0";
            for (int i = 0; i < this.MdiChildren.Count(); i++)
            {
                this.MdiChildren[i].Close();
            }
            FrmBcardTCWP tcwp = new FrmBcardTCWP();
            // tcwp.MdiParent = this;
            tcwp.Show();
        }

        private void viewMenu_Click(object sender, EventArgs e)
        {
            dqzt = "2";
            for (int i = 0; i < this.MdiChildren.Count(); i++)
            {
                this.MdiChildren[i].Close();
            }

            FrmBcardTCWP tcwp = new FrmBcardTCWP();
            // tcwp.MdiParent = this;
            tcwp.Show();
        }

        private void helpMenu_Click(object sender, EventArgs e)
        {

            MessageBoxButtons msgBut = MessageBoxButtons.OKCancel;
            DialogResult dr = MessageBox.Show("确定删除此数据项？", "删除数据", msgBut, MessageBoxIcon.Question);
            if (dr == DialogResult.OK)
            {
                DeleteData(Convert.ToInt32(selectid));

                GetAllDataRefreshGridView();
            }
        }

        private void btnExcel_Click(object sender, EventArgs e)
        {

        }

        private void DC_Click(object sender, EventArgs e)
        {
            DataTable dt = dataGridView.DataSource as DataTable;
            ExcelUI ExcelUI = new ExcelUI();
            ExcelUI.ExportExcel(dt, "");
        }

        private void toolStripMenuItem7_Click(object sender, EventArgs e)
        {
            string path = "";
            folderBrowserDialog1.SelectedPath = "";
            if (folderBrowserDialog1.ShowDialog() == DialogResult.OK)
            {
                //    OpenFileDialog ofd = new OpenFileDialog();
                //ofd.Title = "选择文件";
                //ofd.Filter = "Microsoft Excel文件|*.xls;*.xlsx";
                //ofd.FilterIndex = 1;
                //ofd.DefaultExt = "xls";
                //string path = "";
                //if (ofd.ShowDialog() == DialogResult.OK)
                //{
                //    //if (!ofd.SafeFileName.EndsWith(".xls") && !ofd.SafeFileName.EndsWith(".xlsx"))
                //    //{
                //    //    MessageBox.Show("请选择Excel文件", "文件解析失败!", MessageBoxButtons.OK, MessageBoxIcon.Error);
                //    //    return;
                //    //}
                //    //if (!ofd.CheckFileExists)
                //    //{
                //    //    MessageBox.Show("指定的文件不存在", "请检查！", MessageBoxButtons.OK, MessageBoxIcon.Error);
                //    //    return;
                //    //}
                path = folderBrowserDialog1.SelectedPath;
            }
            if (path == "")
            {
                MessageBox.Show("请选择报表输出路径!");
                return;
            }
            ExcelUI.OpenExcel_DRMB(null, Application.StartupPath + "\\report\\A3\\留存名贵特产类物品明细统计表.xlsx", path + "\\留存名贵特产类物品明细统计表.xlsx", 4, 0);
            MessageBox.Show("下载成功!");
        }

        private void toolStripMenuItem8_Click(object sender, EventArgs e)
        {
            FrmBCardDRMX drmx = new FrmBCardDRMX();
            drmx.Show();
        }

        private void button2_Click_1(object sender, EventArgs e)
        {

            dqzt = "1";
            FrmBcardTCWP tcwp = new FrmBcardTCWP();
            tcwp.Show();
        }

        private void button4_Click_1(object sender, EventArgs e)
        {
            dqzt = "0";
            for (int i = 0; i < this.MdiChildren.Count(); i++)
            {
                this.MdiChildren[i].Close();
            }
            FrmBcardTCWP tcwp = new FrmBcardTCWP();
            // tcwp.MdiParent = this;
            tcwp.Show();
        }

        private void button5_Click_1(object sender, EventArgs e)
        {
            dqzt = "2";
            for (int i = 0; i < this.MdiChildren.Count(); i++)
            {
                this.MdiChildren[i].Close();
            }

            FrmBcardTCWP tcwp = new FrmBcardTCWP();
            // tcwp.MdiParent = this;
            tcwp.Show();
        }

        private void button6_Click_1(object sender, EventArgs e)
        {
            MessageBoxButtons msgBut = MessageBoxButtons.OKCancel;
            DialogResult dr = MessageBox.Show("确定删除此数据项？", "删除数据", msgBut, MessageBoxIcon.Question);
            if (dr == DialogResult.OK)
            {
                DeleteData(Convert.ToInt32(selectid));

                GetAllDataRefreshGridView();
            }
        }

        private void button7_Click_1(object sender, EventArgs e)
        {
            DataTable dt = dataGridView.DataSource as DataTable;
            ExcelUI ExcelUI = new ExcelUI();
            ExcelUI.ExportExcel(dt, "");
        }

        private void button9_Click(object sender, EventArgs e)
        {
            string path = "";
            folderBrowserDialog1.SelectedPath = "";
            if (folderBrowserDialog1.ShowDialog() == DialogResult.OK)
            {
                //    OpenFileDialog ofd = new OpenFileDialog();
                //ofd.Title = "选择文件";
                //ofd.Filter = "Microsoft Excel文件|*.xls;*.xlsx";
                //ofd.FilterIndex = 1;
                //ofd.DefaultExt = "xls";
                //string path = "";
                //if (ofd.ShowDialog() == DialogResult.OK)
                //{
                //    //if (!ofd.SafeFileName.EndsWith(".xls") && !ofd.SafeFileName.EndsWith(".xlsx"))
                //    //{
                //    //    MessageBox.Show("请选择Excel文件", "文件解析失败!", MessageBoxButtons.OK, MessageBoxIcon.Error);
                //    //    return;
                //    //}
                //    //if (!ofd.CheckFileExists)
                //    //{
                //    //    MessageBox.Show("指定的文件不存在", "请检查！", MessageBoxButtons.OK, MessageBoxIcon.Error);
                //    //    return;
                //    //}
                path = folderBrowserDialog1.SelectedPath;
            }
            if (path == "")
            {
                MessageBox.Show("请选择报表输出路径!");
                return;
            }
            ExcelUI.OpenExcel_DRMB(null, Application.StartupPath + "\\report\\A3\\留存名贵特产类物品明细统计表.xlsx", path + "\\留存名贵特产类物品明细统计表.xlsx", 4, 0);
            MessageBox.Show("下载成功!");
        }

        private void button10_Click(object sender, EventArgs e)
        {
            FrmBCardDRMX drmx = new FrmBCardDRMX();
            drmx.ShowDialog();
            if (drmx.isOk)
            {
                GetAllDataRefreshGridView();
            }
        }

        private void button1_Click_1(object sender, EventArgs e)
        {

            filterStr = string.Empty;
            select_report select_report = new select_report();
            string dwdm = string.Empty;
            if (!string.IsNullOrEmpty(textBox3.Text.ToString()))
            {
                dwdm = dwdm_tree;
            }
            string bmbs = string.Empty;
            if (!string.IsNullOrEmpty(textBox1.Text.ToString()))
            {
                bmbs = BMTree.tree_bmbs;
            }
            string dwlx = cbdwlx.Text;
            string ly = cbLY.Text;
            string czfs = cbCZFS.Text.ToString();
            string lb = cbLB.Text.ToString();
            string pm = cbPM.Text.ToString();
            string hqsj = dtHQSJ.Text.ToString();
            if (checkBox1.Checked == true)
            {
                if (!string.IsNullOrEmpty(dwdm))
                {
                    filterStr += "and A.dwdm ='" + dwdm + "'";
                }
                if (!string.IsNullOrEmpty(bmbs))
                {
                    filterStr += "and A.bmbs ='" + bmbs + "'";
                }
            }
            else
            {
                if (!string.IsNullOrEmpty(dwdm) && dwdm != "000")
                {
                    filterStr += "and A.dwdm  like '" + dwdm + "%'";
                }
            }
            if (!string.IsNullOrEmpty(dwlx))
            {
                filterStr += "and A.djlx = '" + dwlx + "'";
            }

            if (!string.IsNullOrEmpty(ly))
            {
                filterStr += "and (A.ly = '" + ly + "'" + " or A.ly = '地方购买')";
            }
            if (!string.IsNullOrEmpty(czfs))
            {
                filterStr += "and A.czfs = '" + czfs + "'";
            }
            if (!string.IsNullOrEmpty(lb))
            {
                filterStr += "and A.lb = '" + lb + "'";
            }
            if (!string.IsNullOrEmpty(pm))
            {
                filterStr += "and A.pm like '%" + pm + "%'";
            }
            if (!string.IsNullOrEmpty(hqsj))
            {
                filterStr += "and A.hqsj = '" + hqsj + "'";
            }
            GetAllDataRefreshGridView();
        }

        private void button8_Click_1(object sender, EventArgs e)
        {
            //comboDWDM.SelectedValue = "";
            //comboDWDM.SelectedText = "";
            cbLY.Items.Add("");
            cbLY.Text = "";
            cbdwlx.Items.Add("");
            cbdwlx.Text = "";
            cbCZFS.Items.Add("");
            cbCZFS.Text = "";
            cbLB.Items.Add("");
            cbLB.Text = "";
            dtHQSJ.Items.Add("");
            dtHQSJ.Text = "";
            cbPM.Text = "";
            textBox1.Text = "";
            textBox3.Text = "";

        }

        private void button3_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void button2_Click_2(object sender, EventArgs e)
        {
            dqzt = "1";
            FrmBcardTCWP tcwp = new FrmBcardTCWP();
            tcwp.ShowDialog();
            if (tcwp.isok)
            {
                GetAllDataRefreshGridView();
            }
        }

        private void button4_Click_2(object sender, EventArgs e)
        {
            dqzt = "0";
            //for (int i = 0; i < this.MdiChildren.Count(); i++)
            //{
            //    this.MdiChildren[i].Close();
            //}
            FrmBcardTCWP tcwp = new FrmBcardTCWP();
            // tcwp.MdiParent = this;
            tcwp.Show();
        }

        private void dataGridView_SelectionChanged_1(object sender, EventArgs e)
        {
            if (dataGridView.SelectedRows.Count == 1)
            {
                selectid = dataGridView.SelectedRows[0].Cells[2].Value.ToString();

            }
        }

        private void button5_Click_2(object sender, EventArgs e)
        {
            if (dataGridView.SelectedRows.Count > 0)
            {
                dqxh = int.Parse(dataGridView.SelectedRows[0].Cells[1].Value.ToString());
                if (!string.IsNullOrEmpty(selectid.ToString()))
                {
                    dqzt = "2";
                    for (int i = 0; i < this.MdiChildren.Count(); i++)
                    {
                        this.MdiChildren[i].Close();
                    }

                    FrmBcardTCWP tcwp = new FrmBcardTCWP();
                    tcwp.ShowDialog();
                    if (tcwp.isok)
                    {
                        GetAllDataRefreshGridView();
                        if (dqxh > 0)
                        {
                            dataGridView.CurrentCell = dataGridView.Rows[dqxh - 1].Cells[1];
                        }
                    }
                    // tcwp.MdiParent = this;
                    // tcwp.Show();
                }
            }
            else
            {
                MessageBox.Show("请先选择一条数据！", "系统提示", MessageBoxButtons.OK, MessageBoxIcon.Information);
                return;
            }

        }

        private void button6_Click_2(object sender, EventArgs e)
        {
            if (!string.IsNullOrEmpty(selectid))
            {
                MessageBoxButtons msgBut = MessageBoxButtons.OKCancel;
                DialogResult dr = MessageBox.Show("确定删除此数据项？", "删除数据", msgBut, MessageBoxIcon.Question);
                if (dr == DialogResult.OK)
                {
                    DeleteData(Convert.ToInt32(selectid));

                    GetAllDataRefreshGridView();
                }
            }

        }

        private void button7_Click_2(object sender, EventArgs e)
        {
            string tempSQL = string.Empty;
            if (login.LogName == "zbgly")
            {
                tempSQL = @"select  A.dwmc as 单位名称,''''+A.dwbm as 单位标识码,B.BMMC as 部门, A.lb as 类别, A.pm as 品名, A.ly as 来源,A.hqsj as 获取时间,  A.sl as 数量,  A.jldw as 计量单位, A.djlx as 单价类型,A.dj as 单价, A.zz as 总值, A.kysl as 堪用数量,A.kbxjz as 可变现价值,A.czfs as 处置方式, A.bz as 备注,A.szss as 所在省,A.szs as 所在市,A.szx as 所在县,A.xxdz as 详细地址,A.wpbs as 物品唯一标识码（不可修改）,A.jjqrsl as 实际接收数量,A.jjbz as 交接备注   FROM(SELECT t_dwxx.szss,t_dwxx.szs,t_dwxx.szx,t_dwxx.xxdz, t_lctc.ID, t_lctc.wpbs,t_lctc.wpbs,t_lctc.dwbm,t_lctc.dwdm, t_dwxx.dwmc, t_lctc.bmbs, t_lctc.lb, t_lctc.pm, t_lctc.ly, t_lctc.hqsj, t_lctc.sl, t_lctc.jldw, t_lctc.djlx, t_lctc.dj, t_lctc.zz, t_lctc.kysl, t_lctc.kbxjz, t_lctc.czfs, t_lctc.bz,t_lctc.wpbs,t_lctc.jjqrsl,t_lctc.jjbz  FROM t_lctc left join t_dwxx on t_lctc.dwdm = t_dwxx.dwdm) as A left join t_bm as b on A.bmbs = b.bmbs   and A.dwdm=b.dwdm     where  ''   " + filterStr + " ORDER BY A.dwdm,A.ID";

            }
            else
            {
                tempSQL = @"select  A.dwmc as 单位名称,B.BMMC as 部门, A.lb as 类别, A.pm as 品名, A.ly as 来源,A.hqsj as 获取时间,  A.sl as 数量,  A.jldw as 计量单位, A.djlx as 单价类型,A.dj as 单价, A.zz as 总值, A.kysl as 堪用数量,A.kbxjz as 可变现价值,A.czfs as 处置方式, A.bz as 备注,A.szss as 所在省,A.szs as 所在市,A.szx as 所在县,A.xxdz as 详细地址 ,A.wpbs as 物品唯一标识码（不可修改）, A.jjqrsl as 实际接收数量,A.jjbz as 交接备注  FROM(SELECT t_dwxx.szss,t_dwxx.szs,t_dwxx.szx,t_dwxx.xxdz, t_lctc.ID, t_lctc.wpbs,t_lctc.wpbs,t_lctc.dwbm,t_lctc.dwdm, t_dwxx.dwmc, t_lctc.bmbs, t_lctc.lb, t_lctc.pm, t_lctc.ly, t_lctc.hqsj, t_lctc.sl, t_lctc.jldw, t_lctc.djlx, t_lctc.dj, t_lctc.zz, t_lctc.kysl, t_lctc.kbxjz, t_lctc.czfs, t_lctc.bz ,t_lctc.wpbs,t_lctc.jjqrsl,t_lctc.jjbz FROM t_lctc left join t_dwxx on t_lctc.dwdm = t_dwxx.dwdm) as A left join t_bm as b on A.bmbs = b.bmbs  and A.dwdm=b.dwdm     where ''   " + filterStr + " ORDER BY A.dwdm,A.ID";

            }

            //tempSQL = "select dwdm as 单位代码,dwbm as 融通单位标识码,wpbs as  物品标识,jjqrsl as 交接确认品名,czfs as 处置方式  from t_lctc   where ''   " + filterStr + " order by dwdm";


            DataSet ds = m_accessHelper.getDataSet(tempSQL);

            DataTable dt = ds.Tables[0];
            ExcelUI ExcelUI = new ExcelUI();
            ExcelUI.ExportExcel(dt, "");
        }

        private void button9_Click_1(object sender, EventArgs e)
        {
            string path = "";
            folderBrowserDialog1.SelectedPath = "";
            if (folderBrowserDialog1.ShowDialog() == DialogResult.OK)
            {
                path = folderBrowserDialog1.SelectedPath;
            }
            if (path == "")
            {
                MessageBox.Show("请选择报表输出路径!");
                return;
            }
            ExcelUI.OpenExcel_DRMB(null, Application.StartupPath + "\\report\\A30\\留存名贵特产类物品明细统计表模板（部门录入用).xlsx", path + "\\留存名贵特产类物品明细统计表模板（部门录入用).xlsx", 4, 0);
            MessageBox.Show("下载成功!");
        }

        private void cbBM_Click(object sender, EventArgs e)
        {
            BMTree bt = new BMTree();
            bt.Show();
        }

        private void textBox1_Click(object sender, EventArgs e)
        {
            if (textBox3.Text == "")
            {
                MessageBox.Show("请先选择单位!");
                return;
            }
            FrmBCardDRMX.BMDR = "";
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



        private void button1_Click(object sender, EventArgs e)
        {

        }

        //private void comboDWDM_SelectedValueChanged_1(object sender, EventArgs e)
        //{
        //    if (comboDWDM.Text != "")
        //    {
        //        textBox1.Text = "";
        //        dwdm_tree = comboDWDM.SelectedValue.ToString();
        //    }

        //}

        private void checkBox1_CheckedChanged(object sender, EventArgs e)
        {
            if (checkBox1.Checked == true)
            {
                textBox1.Enabled = true;
                textBox1.Text = "";
            }
            else
            {
                textBox1.Enabled = false;
                textBox1.Text = "";
            }
        }
        private void textBox3_Click(object sender, EventArgs e)
        {
            DWTree dt = new DWTree();
            dt.ShowDialog();
            //textBox3.Text = dt.GetDWMC();
            dwdm_tree = dt.GetDWdm();
            string sql_dwmc = "select dwmc from t_dwxx where dwdm='" + dwdm_tree + "'";
            DataTable dt1 = m_accessHelper.getDataSet(sql_dwmc).Tables[0];
            if (dt1.Rows.Count > 0)
            {
                textBox3.Text = dt1.Rows[0]["dwmc"].ToString();
            }
        }

        private void button4_Click_3(object sender, EventArgs e)
        {
            PLSJDR plsjdr = new PLSJDR();
            plsjdr.ShowDialog();
            if (plsjdr.isOk)
            {
                GetAllDataRefreshGridView();
            }
        }

        private void button11_Click(object sender, EventArgs e)
        {
            if (dt_sjsh.Rows.Count > 0)
            {

                for (int i = 0; i < dt_sjsh.Rows.Count; i++)
                {

                    // dt_sjsh.Rows[i]["选择"] = false;
                    string error = "";
                    if (dt_sjsh.Rows[i]["类别"].ToString() != "香烟" && dt_sjsh.Rows[i]["类别"].ToString() != "酒水" && dt_sjsh.Rows[i]["类别"].ToString() != "茶叶" && dt_sjsh.Rows[i]["类别"].ToString() != "食材" && dt_sjsh.Rows[i]["类别"].ToString() != "药材" && dt_sjsh.Rows[i]["类别"].ToString() != "瓷器" && dt_sjsh.Rows[i]["类别"].ToString() != "字画" && dt_sjsh.Rows[i]["类别"].ToString() != "金银" && dt_sjsh.Rows[i]["类别"].ToString() != "玉石" && dt_sjsh.Rows[i]["类别"].ToString() != "文玩" && dt_sjsh.Rows[i]["类别"].ToString() != "木材" && dt_sjsh.Rows[i]["类别"].ToString() != "模型" && dt_sjsh.Rows[i]["类别"].ToString() != "纪念币" && dt_sjsh.Rows[i]["类别"].ToString() != "日用品" && dt_sjsh.Rows[i]["类别"].ToString() != "其他")
                    {
                        error += "类别为" + dt_sjsh.Rows[i]["类别"].ToString() + "不是系统预置类别;";
                    }
                    if (dt_sjsh.Rows[i]["处置方式"].ToString() != "拟移交物品" && dt_sjsh.Rows[i]["处置方式"].ToString() != "拟上交物品" && dt_sjsh.Rows[i]["处置方式"].ToString() != "拟捐赠物品" && dt_sjsh.Rows[i]["处置方式"].ToString() != "拟销毁物品" && dt_sjsh.Rows[i]["处置方式"].ToString() != "拟个案处理物品")
                    {
                        error += "处置方式为" + dt_sjsh.Rows[i]["处置方式"].ToString() + "不是系统预置处置方式;";
                    }
                    if ((Convert.ToDecimal(dt_sjsh.Rows[i]["数量"].ToString() == "" ? "0" : dt_sjsh.Rows[i]["数量"].ToString())) * (Convert.ToDecimal(dt_sjsh.Rows[i]["单价"].ToString() == "" ? "0" : dt_sjsh.Rows[i]["单价"].ToString())) != (Convert.ToDecimal(dt_sjsh.Rows[i]["总值"].ToString() == "" ? "0" : dt_sjsh.Rows[i]["总值"].ToString())))
                    {
                        error += "总值不等于单价乘以数量";
                    }
                    if (!string.IsNullOrEmpty(error))
                    {
                        dt_sjsh.Rows[i]["检验异常结果"] = error;
                    }

                }

                dataGridView.DataSource = dt_sjsh;
                for (int i = 0; i < dt_sjsh.Rows.Count; i++)
                {
                    dataGridView.Rows[i].Cells[0].Value = false;
                }
                dataGridView.Columns[2].Visible = false;
                dataGridView.Columns[3].Visible = false;
                dataGridView.Columns[4].Visible = true;
                dataGridView.Columns[0].Visible = true;
                dataGridView.Columns[0].ReadOnly = false;

                dataGridView.ClearSelection();
                dataGridView.Columns["ID号"].Width = 30;
                dataGridView.Columns["序号"].Width = 60;
                dataGridView.Columns["单位名称"].Width = 200;
                dataGridView.Columns["部门"].Width = 100;
                dataGridView.Columns["类别"].Width = 60;
                dataGridView.Columns["来源"].Width = 70;
                dataGridView.Columns["获取时间"].Width = 90;
                dataGridView.Columns["计量单位"].Width = 90;
                dataGridView.Columns["单价类型"].Width = 90;
                dataGridView.Columns["处置方式"].Width = 100;
                dataGridView.Columns["可变现价值"].Width = 100;
                dataGridView.Columns["备注"].Width = 120;
                dataGridView.Columns["检验异常结果"].Width = 320;
                if (dataGridView.SelectedRows.Count != 0)
                {
                    m_id = int.Parse(dataGridView.SelectedRows[0].Cells[1].Value.ToString());
                }
                else
                {
                    m_id = ClassConstants.JD_NOTSELECTED;
                }
                button12.Visible = true;
                button13.Visible = true;
                button14.Visible = true;
                button15.Visible = true;
                button16.Visible = false;
                button17.Visible = true;
            }
            else
            {
                MessageBox.Show("待审核验证数据为空！", "系统提示", MessageBoxButtons.OK, MessageBoxIcon.Information);
                return;
            }
        }

        private void button12_Click(object sender, EventArgs e)
        {
            for (int i = 0; i < dt_sjsh.Rows.Count; i++)
            {
                dataGridView.Rows[i].Cells[0].Value = true;
            }
        }

        private void button13_Click(object sender, EventArgs e)
        {
            for (int i = 0; i < dt_sjsh.Rows.Count; i++)
            {
                dataGridView.Rows[i].Cells[0].Value = false;
            }
        }

        private void button14_Click(object sender, EventArgs e)
        {
            ids = "";

            for (int i = 0; i < dt_sjsh.Rows.Count; i++)
            {
                if (dataGridView.Rows[i].Cells[0].Value.ToString() == "True")
                {
                    ids += dataGridView.Rows[i].Cells[2].Value.ToString();
                    ids += ",";
                }
            }
            if (!string.IsNullOrEmpty(ids))
            {
                ids = ids.Substring(0, ids.Length - 1);
                PLXGSJ dt = new PLXGSJ();
                dt.ShowDialog();
                if (dt.isok)
                {
                    GetAllDataRefreshGridView();
                    button12.Visible = false;
                    button13.Visible = false;
                    button14.Visible = false;
                    button15.Visible = false;
                    button16.Visible = false;
                    button17.Visible = false;
                }
            }
            else
            {
                MessageBox.Show("请先勾选要修改的数据！", "系统提示", MessageBoxButtons.OK, MessageBoxIcon.Information);
                return;
            }
        }

        private void button15_Click(object sender, EventArgs e)
        {
            cfsjbs = "物品";
            string sql_cfdata = "select wpbs from t_lctc group by wpbs having count(*)>1";
            DataTable dt = m_accessHelper.getDataSet(sql_cfdata).Tables[0];

            string ids = string.Empty;
            if (dt.Rows.Count > 0)
            {
                for (int i = 0; i < dt.Rows.Count; i++)
                {
                    ids += "'";
                    ids += dt.Rows[i]["wpbs"].ToString() + "',";
                }
                if (!string.IsNullOrEmpty(ids))
                {
                    ids = ids.Substring(0, ids.Length - 1);
                    string tempSQL = "select ''as 选择,A.ID as ID号,A.wpbs as 物品标识码, A.dwmc as 单位名称,B.BMMC as 部门, A.lb as 类别, A.pm as 品名, A.ly as 来源,A.hqsj as 获取时间,  A.sl as 数量,  A.jldw as 计量单位, A.djlx as 单价类型,A.dj as 单价, A.zz as 总值, A.kysl as 堪用数量,A.kbxjz as 可变现价值,A.czfs as 处置方式, A.bz as 备注  FROM(SELECT t_lctc.ID, t_lctc.wpbs,t_dwxx.dwdm, t_dwxx.dwmc, t_lctc.bmbs, t_lctc.lb, t_lctc.pm, t_lctc.ly, t_lctc.hqsj, t_lctc.sl, t_lctc.jldw, t_lctc.djlx, t_lctc.dj, t_lctc.zz, t_lctc.kysl, t_lctc.kbxjz, t_lctc.czfs, t_lctc.bz  FROM t_lctc left join t_dwxx on t_lctc.dwdm = t_dwxx.dwdm) as A left join t_bm as b on A.bmbs = b.bmbs where wpbs in(" + ids + ")  ORDER BY A.dwdm,A.ID";

                    //DataGridViewCheckBoxColumn DtCheck = new DataGridViewCheckBoxColumn();
                    //DtCheck.DataPropertyName = "check";
                    //DtCheck.HeaderText = "选择";
                    //dataGridView.Columns.Add(DtCheck);
                    DataTable dt_all = m_accessHelper.getDataSet(tempSQL).Tables[0];
                    dataGridView.DataSource = dt_all;
                    dt_cfsj = dt_all;
                    for (int i = 0; i < dt_all.Rows.Count; i++)
                    {
                        dataGridView.Rows[i].Cells[0].Value = false;
                    }
                    dataGridView.Columns[2].Visible = true;
                    dataGridView.Columns[3].Visible = true;
                    dataGridView.Columns[4].Visible = true;
                    dataGridView.Columns[0].Visible = true;
                    dataGridView.Columns[0].ReadOnly = false;

                    dataGridView.ClearSelection();
                    dataGridView.Columns["ID号"].Width = 30;
                    dataGridView.Columns["选择"].Width = 60;
                    dataGridView.Columns["单位名称"].Width = 200;
                    dataGridView.Columns["部门"].Width = 100;
                    dataGridView.Columns["类别"].Width = 60;
                    dataGridView.Columns["来源"].Width = 70;
                    dataGridView.Columns["获取时间"].Width = 90;
                    dataGridView.Columns["计量单位"].Width = 90;
                    dataGridView.Columns["单价类型"].Width = 90;
                    dataGridView.Columns["处置方式"].Width = 100;
                    dataGridView.Columns["可变现价值"].Width = 100;
                    dataGridView.Columns["备注"].Width = 120;
                    if (dataGridView.SelectedRows.Count != 0)
                    {
                        m_id = int.Parse(dataGridView.SelectedRows[0].Cells[1].Value.ToString());
                    }
                    else
                    {
                        m_id = ClassConstants.JD_NOTSELECTED;
                    }
                    button12.Visible = true;
                    button13.Visible = true;
                    button14.Visible = true;
                    button15.Visible = true;
                    button16.Visible = true;
                    button17.Visible = true;
                    button6.Enabled = false;
                }
            }
            else
            {
                GetAllDataRefreshGridView();
                MessageBox.Show("无重复数据！", "系统提示", MessageBoxButtons.OK, MessageBoxIcon.Information);
                return;
            }
        }

        private void button16_Click(object sender, EventArgs e)
        {
            if (selectid == "")
            {
                MessageBox.Show("请先勾选要修改的数据！", "系统提示", MessageBoxButtons.OK, MessageBoxIcon.Information);
                return;
            }
            MessageBoxButtons msgBut = MessageBoxButtons.OKCancel;
            DialogResult dr = MessageBox.Show("确定删除此数据项？", "删除数据", msgBut, MessageBoxIcon.Question);
            if (dr == DialogResult.OK)
            {
                ids = "";
                for (int i = 0; i < dt_cfsj.Rows.Count; i++)
                {
                    if (dataGridView.Rows[i].Cells[0].Value.ToString() == "True")
                    {
                        ids += dataGridView.Rows[i].Cells[1].Value.ToString();
                        ids += ",";
                    }
                }
                if (!string.IsNullOrEmpty(ids))
                {
                    ids = ids.Substring(0, ids.Length - 1);
                    string sql_delete = string.Empty;
                    if (cfsjbs == "物品")
                    {
                        sql_delete = "delete from t_lctc where id in(" + ids + ")";
                    }
                    if (cfsjbs == "部门")
                    {
                        sql_delete = "delete from t_bm where id in(" + ids + ")";
                    }
                    m_accessHelper.ExcueteCommand(sql_delete);

                    GetAllDataRefreshGridView();
                    button12.Visible = false;
                    button13.Visible = false;
                    button14.Visible = false;
                    button15.Visible = false;
                    button16.Visible = false;
                    button17.Visible = false;

                }
                else
                {
                    MessageBox.Show("请先勾选要修改的数据！", "系统提示", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    return;
                }
            }

        }

        private void button17_Click(object sender, EventArgs e)
        {
            cfsjbs = "部门";
            string sql_cfdata = "select bmbs from t_bm group by bmbs having count(*)>1";
            DataTable dt = m_accessHelper.getDataSet(sql_cfdata).Tables[0];

            string ids = string.Empty;
            if (dt.Rows.Count > 0)
            {
                for (int i = 0; i < dt.Rows.Count; i++)
                {
                    ids += "'";
                    ids += dt.Rows[i]["bmbs"].ToString() + "',";
                }
                if (!string.IsNullOrEmpty(ids))
                {
                    ids = ids.Substring(0, ids.Length - 1);
                    string tempSQL = "select ''as 选择,t_bm.ID as ID号,t_bm.bmbs as 部门标识码,t_dwxx.dwdm as 单位代码,t_dwxx.dwmc as 单位名称,t_bm.bmdm as 部门代码 ,t_bm.bmmc as 部门名称 FROM t_bm left join t_dwxx on t_bm.dwdm = t_dwxx.dwdm where bmbs in (" + ids + ")";

                    DataTable dt_all = m_accessHelper.getDataSet(tempSQL).Tables[0];
                    dataGridView.DataSource = dt_all;
                    dt_cfsj = dt_all;
                    for (int i = 0; i < dt_all.Rows.Count; i++)
                    {
                        dataGridView.Rows[i].Cells[0].Value = false;
                    }
                    dataGridView.Columns[2].Visible = true;
                    dataGridView.Columns[3].Visible = true;
                    dataGridView.Columns[4].Visible = true;
                    dataGridView.Columns[0].Visible = true;
                    dataGridView.Columns[0].ReadOnly = false;

                    dataGridView.ClearSelection();
                    dataGridView.Columns["选择"].Width = 180;
                    dataGridView.Columns["ID号"].Width = 180;
                    dataGridView.Columns["单位名称"].Width = 300;
                    dataGridView.Columns["单位代码"].Width = 300;
                    dataGridView.Columns["部门名称"].Width = 300;
                    dataGridView.Columns["部门代码"].Width = 300;
                    dataGridView.Columns["部门标识码"].Width = 300;

                    //if (dataGridView.SelectedRows.Count != 0)
                    //{
                    //    m_id = int.Parse(dataGridView.SelectedRows[0].Cells[1].Value.ToString());
                    //}
                    //else
                    //{
                    //    m_id = ClassConstants.JD_NOTSELECTED;
                    //}
                    button12.Visible = true;
                    button13.Visible = true;
                    button14.Visible = true;
                    button15.Visible = true;
                    button16.Visible = true;
                    button17.Visible = true;
                    button6.Enabled = false;
                }
            }
            else
            {
                GetAllDataRefreshGridView();
                MessageBox.Show("无重复数据！", "系统提示", MessageBoxButtons.OK, MessageBoxIcon.Information);
                return;
            }
        }

        private void dataGridView_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            try
            {
                if (this.dataGridView.SelectedCells.Count > 0 && Control.ModifierKeys == Keys.Shift)
                {
                    int endrow = this.dataGridView.CurrentRow.Index;
                    if (startrow <= endrow)
                    {
                        for (int x = startrow; x <= endrow; x++)
                        {
                            this.dataGridView.Rows[x].Cells[0].Value = true;
                        }
                    }
                    else
                    {
                        //倒序选时
                        for (int x = endrow; x <= startrow-1; x++)
                        {
                            this.dataGridView.Rows[x].Cells[0].Value = true;
                        }
                    }
                }
                else
                {
                    startrow = Convert.ToInt32(dataGridView.SelectedRows[0].Cells[1].Value.ToString());
                    if (dataGridView.SelectedRows[0].Cells[0].Value.ToString() == "True")
                    {
                        dataGridView.SelectedRows[0].Cells[0].Value = false;
                    }
                    else
                    {
                        dataGridView.SelectedRows[0].Cells[0].Value = true;
                    }
                }

            }
            catch
            {


            }
        }

        private void panel1_Paint(object sender, PaintEventArgs e)
        {

        }

    }
}
