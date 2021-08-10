﻿using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace zj
{
    public partial class MDIMain : Form
    {
        public int pass = 0; //默认情况下审核未通过

        private int childFormNumber = 0;
        public static string dw0rlctc = string.Empty;

        public MDIMain()
        {
            InitializeComponent();
        }

        private void ShowNewForm(object sender, EventArgs e)
        {
            Form childForm = new Form();
            childForm.MdiParent = this;
            childForm.Text = "窗口 " + childFormNumber++;
            childForm.Show();
        }

        private void OpenFile(object sender, EventArgs e)
        {
            OpenFileDialog openFileDialog = new OpenFileDialog();
            openFileDialog.InitialDirectory = Environment.GetFolderPath(Environment.SpecialFolder.Personal);
            openFileDialog.Filter = "文本文件(*.txt)|*.txt|所有文件(*.*)|*.*";
            if (openFileDialog.ShowDialog(this) == DialogResult.OK)
            {
                string FileName = openFileDialog.FileName;
            }
        }

        private void SaveAsToolStripMenuItem_Click(object sender, EventArgs e)
        {
            SaveFileDialog saveFileDialog = new SaveFileDialog();
            saveFileDialog.InitialDirectory = Environment.GetFolderPath(Environment.SpecialFolder.Personal);
            saveFileDialog.Filter = "文本文件(*.txt)|*.txt|所有文件(*.*)|*.*";
            if (saveFileDialog.ShowDialog(this) == DialogResult.OK)
            {
                string FileName = saveFileDialog.FileName;
            }
        }

        private void ExitToolsStripMenuItem_Click(object sender, EventArgs e)
        {
            MessageBoxButtons msgBut = MessageBoxButtons.OKCancel;
            DialogResult dr = MessageBox.Show("确定退出系统？", "系统退出", msgBut);
            if (dr == DialogResult.OK)
            {
                this.Close();
            }
        }

        private void CutToolStripMenuItem_Click(object sender, EventArgs e)
        {
        }

        private void CopyToolStripMenuItem_Click(object sender, EventArgs e)
        {
        }

        private void PasteToolStripMenuItem_Click(object sender, EventArgs e)
        {
        }

        private void ToolBarToolStripMenuItem_Click(object sender, EventArgs e)
        {
            toolStrip.Visible = toolBarToolStripMenuItem.Checked;
        }

        private void StatusBarToolStripMenuItem_Click(object sender, EventArgs e)
        {
            statusStrip.Visible = statusBarToolStripMenuItem.Checked;
        }

        private void CascadeToolStripMenuItem_Click(object sender, EventArgs e)
        {
            LayoutMdi(MdiLayout.Cascade);
        }

        private void TileVerticalToolStripMenuItem_Click(object sender, EventArgs e)
        {
            LayoutMdi(MdiLayout.TileVertical);
        }

        private void TileHorizontalToolStripMenuItem_Click(object sender, EventArgs e)
        {
            LayoutMdi(MdiLayout.TileHorizontal);
        }

        private void ArrangeIconsToolStripMenuItem_Click(object sender, EventArgs e)
        {
            LayoutMdi(MdiLayout.ArrangeIcons);
        }

        private void CloseAllToolStripMenuItem_Click(object sender, EventArgs e)
        {
            foreach (Form childForm in MdiChildren)
            {
                childForm.Close();
            }
        }

        private void toolStripMenuItem1_Click(object sender, EventArgs e)
        {
            for (int i = 0; i < this.MdiChildren.Count(); i++)
            {
                this.MdiChildren[i].Close();
            }
            password password = new password();
            password.MdiParent = this;
            password.Show();
            password.WindowState = FormWindowState.Normal;
        }

        private void toolStripMenuItem2_Click(object sender, EventArgs e)
        {

        }

        private void toolStripMenuItem6_Click(object sender, EventArgs e)
        {
            pass = 0;
            for (int i = 0; i < this.MdiChildren.Count(); i++)
            {
                this.MdiChildren[i].Close();
            }
            initialization initialization = new initialization();
            initialization.MdiParent = this;
            initialization.Show();
            initialization.WindowState = FormWindowState.Normal;
        }

        private void toolStripMenuItem3_Click(object sender, EventArgs e)
        {
            for (int i = 0; i < this.MdiChildren.Count(); i++)
            {
                this.MdiChildren[i].Close();
            }
            babackup backup = new babackup();
            backup.MdiParent = this;
            backup.Show();
            backup.WindowState = FormWindowState.Normal;
        }

        private void toolStripMenuItem4_Click(object sender, EventArgs e)
        {
            for (int i = 0; i < this.MdiChildren.Count(); i++)
            {
                this.MdiChildren[i].Close();
            }
            recover recover = new recover();
            recover.MdiParent = this;
            recover.Show();
            recover.WindowState = FormWindowState.Normal;
        }

        private void saveToolStripButton_Click(object sender, EventArgs e)
        {
            for (int i = 0; i < this.MdiChildren.Count(); i++)
            {
                this.MdiChildren[i].Close();
            }
            danwei danwei = new danwei();
            danwei.MdiParent = this;
            danwei.Show();
            danwei.WindowState = FormWindowState.Normal;
        }

        private void toolStripButton3_Click(object sender, EventArgs e)
        {
            for (int i = 0; i < this.MdiChildren.Count(); i++)
            {
                this.MdiChildren[i].Close();
            }
            password password = new password();
            password.MdiParent = this;
            password.Show();
            password.WindowState = FormWindowState.Normal;
        }

        private void 上报报表ToolStripMenuItem_Click(object sender, EventArgs e)
        {
            for (int i = 0; i < this.MdiChildren.Count(); i++)
            {
                this.MdiChildren[i].Close();
            }
            report report = new report();
            report.MdiParent = this;
            report.Show();
            report.WindowState = FormWindowState.Normal;
        }

        private void toolStripButton1_Click(object sender, EventArgs e)
        {
            for (int i = 0; i < this.MdiChildren.Count(); i++)
            {
                this.MdiChildren[i].Close();
            }
            report report = new report();
            report.MdiParent = this;
            report.Show();
            report.WindowState = FormWindowState.Normal;
        }

        private void 接收报表ToolStripMenuItem_Click(object sender, EventArgs e)
        {
            pass = 0;
            for (int i = 0; i < this.MdiChildren.Count(); i++)
            {
                this.MdiChildren[i].Close();
            }
            receive recevice = new receive();
            recevice.MdiParent = this;
            recevice.Show();
            recevice.WindowState = FormWindowState.Normal;
        }

        private void toolStripButton2_Click(object sender, EventArgs e)
        {

            pass = 0;
            for (int i = 0; i < this.MdiChildren.Count(); i++)
            {
                this.MdiChildren[i].Close();
            }
            receive recevice = new receive();
            recevice.MdiParent = this;
            recevice.Show();
            recevice.WindowState = FormWindowState.Normal;
        }

        private void 报表封面ToolStripMenuItem1_Click(object sender, EventArgs e)
        {
            ExcelUI.OpenExcel_A4_cover(Application.StartupPath + "\\report\\A4\\1.xls", Application.StartupPath + "\\export\\封面.xls");

        }

        private void 填报说明ToolStripMenuItem1_Click(object sender, EventArgs e)
        {
            ExcelUI.OpenExcel_A4_cover(Application.StartupPath + "\\report\\A4\\2.xls", Application.StartupPath + "\\export\\填报说明.xls");

        }

        private void 统计报表ToolStripMenuItem_Click(object sender, EventArgs e)
        {
            for (int i = 0; i < this.MdiChildren.Count(); i++)
            {
                this.MdiChildren[i].Close();
            }
            report_A4 report_A4 = new report_A4();
            report_A4.MdiParent = this;
            report_A4.Show();
        }

        private void 报表主体ToolStripMenuItem_Click(object sender, EventArgs e)
        {
            
            for (int i = 0; i < this.MdiChildren.Count(); i++)
            {
                this.MdiChildren[i].Close();
            }
            report_A3 report_A3 = new report_A3();
            report_A3.MdiParent = this;
            report_A3.Show();
        }

        private void 报表封面ToolStripMenuItem_Click(object sender, EventArgs e)
        {
            ExcelUI.OpenExcel_A4_cover(Application.StartupPath + "\\report\\A3\\1.xls", Application.StartupPath + "\\export\\A3封面.xls");

        }

        private void 填报说明ToolStripMenuItem_Click(object sender, EventArgs e)
        {
            ExcelUI.OpenExcel_A4_cover(Application.StartupPath + "\\report\\A3\\2.xls", Application.StartupPath + "\\export\\A3填报说明.xls");

        }

        private void MDIMain_Load(object sender, EventArgs e)
        {

            login tempLogin = new login();
            tempLogin.ShowDialog();
            if (tempLogin.DialogResult == DialogResult.OK)
            {
                //MessageBox.Show("用户登录成功！");   
            }
            else
            {
                MessageBox.Show("用户名或者密码错误！");
                Application.Exit();
            }
            if(login.LogName == "zbgly")
            {
                导出融通数据ToolStripMenuItem.Visible = true;
                toolStripSeparator9.Visible = true;
            }
            else
            {
                导出融通数据ToolStripMenuItem.Visible = false;
                toolStripSeparator9.Visible = false;
            }

            //没有单位信息时提示输入
            String sql_dwxx = "select dwmc from t_dwxx where dwdm='000'";
            AccessHelper AccessHelper = new AccessHelper();
            DataSet ds = AccessHelper.getDataSet(sql_dwxx);
            if (ds.Tables[0].Rows.Count == 0)
            {
                //fileMenu.Enabled = false;
                //editMenu.Enabled = false;
                //报表输出ToolStripMenuItem.Enabled = false;
                //报表填报说明ToolStripMenuItem.Enabled = false;
                //viewMenu.Enabled = false;
                BJDW danwei = new BJDW();
                danwei.MdiParent = this;
                danwei.Show();
                danwei.WindowState = FormWindowState.Normal;
            }
            //else
            //{
            //    fileMenu.Enabled = true;
            //    editMenu.Enabled = true;
            //    报表输出ToolStripMenuItem.Enabled = true;
            //    报表填报说明ToolStripMenuItem.Enabled = true;
            //    viewMenu.Enabled = true;
            //}

        }

        private void selectAllToolStripMenuItem_Click(object sender, EventArgs e)
        {
            pass = 0;
            for (int i = 0; i < this.MdiChildren.Count(); i++)
            {
                this.MdiChildren[i].Close();
            }
            FrmBankAccountStatistics BankAccountStatistics = new FrmBankAccountStatistics();
            BankAccountStatistics.MdiParent = this;
            BankAccountStatistics.Show();
        }

        private void 定期通知存款明细统计表ToolStripMenuItem_Click(object sender, EventArgs e)
        {
            pass = 0;
            for (int i = 0; i < this.MdiChildren.Count(); i++)
            {
                this.MdiChildren[i].Close();
            }
            FrmDQCKMX DQCKMX = new FrmDQCKMX();
            DQCKMX.MdiParent = this;
            DQCKMX.Show();
        }

        private void 资金结存情况统计表ToolStripMenuItem_Click(object sender, EventArgs e)
        {
            pass = 0;
            for (int i = 0; i < this.MdiChildren.Count(); i++)
            {
                this.MdiChildren[i].Close();
            }
            FrmAssetsSaving AssetsSaving = new FrmAssetsSaving();
            AssetsSaving.MdiParent = this;
            AssetsSaving.Show();
        }

        private void 现金使用情况ToolStripMenuItem_Click(object sender, EventArgs e)
        {
            pass = 0;
            for (int i = 0; i < this.MdiChildren.Count(); i++)
            {
                this.MdiChildren[i].Close();
            }
            FrmCashUsage CashUsage = new FrmCashUsage();
            CashUsage.MdiParent = this;
            CashUsage.Show();
        }

        private void 账户资金清理检查处理情况统计表ToolStripMenuItem_Click(object sender, EventArgs e)
        {
            pass = 0;
            for (int i = 0; i < this.MdiChildren.Count(); i++)
            {
                this.MdiChildren[i].Close();
            }
            FrmBankAccountCheck BankAccountCheck = new FrmBankAccountCheck();
            BankAccountCheck.MdiParent = this;
            BankAccountCheck.Show();
        }

        private void 往来款ToolStripMenuItem_Click(object sender, EventArgs e)
        {
            pass = 0;
            for (int i = 0; i < this.MdiChildren.Count(); i++)
            {
                this.MdiChildren[i].Close();
            }
            FrmBcardWLK frmBcardWLK = new FrmBcardWLK();
            frmBcardWLK.MdiParent = this;
            frmBcardWLK.Show();
        }

        private void tsbExit_Click(object sender, EventArgs e)
        {
            MessageBoxButtons msgBut = MessageBoxButtons.OKCancel;
            DialogResult dr = MessageBox.Show("确定退出系统？", "系统退出", msgBut, MessageBoxIcon.Question);
            if (dr == DialogResult.OK)
            {
                this.Close();
            }
        }

        private void toolStripMenuItem7_Click(object sender, EventArgs e)
        {
            for (int i = 0; i < this.MdiChildren.Count(); i++)
            {
                this.MdiChildren[i].Close();
            }
            check check = new check();
            check.MdiParent = this;
            check.Show();
            check.WindowState = FormWindowState.Normal;
        }

        private void indexToolStripMenuItem_Click(object sender, EventArgs e)
        {
            WordUI WordUI = new WordUI();
            WordUI.openWord(Application.StartupPath + "\\帮助文件.doc");
        }

        private void toolStripMenuItem9_Click(object sender, EventArgs e)
        {
            pass = 0;
            FrmBCardUsageStatistics card = new FrmBCardUsageStatistics();
            card.MdiParent = this;
            card.Show();
        }

        private void aboutToolStripMenuItem_Click(object sender, EventArgs e)
        {
            copyright copyright = new copyright();
            copyright.MdiParent = this;
            copyright.Show();
        }

        private void toolStripMenuItem10_Click(object sender, EventArgs e)
        {
            instruction instruction = new instruction();
            instruction.MdiParent = this;
            instruction.Show();
        }

        private void toolStripButton4_Click(object sender, EventArgs e)
        {
            check check = new check();
            check.MdiParent = this;
            check.Show();
            check.WindowState = FormWindowState.Normal;
        }

        private void toolStripButton5_Click(object sender, EventArgs e)
        {
            WordUI WordUI = new WordUI();
            WordUI.openWord(Application.StartupPath + "\\帮助文件.doc");
        }

        private void toolStripButton7_Click(object sender, EventArgs e)
        {
            报表主体ToolStripMenuItem_Click(sender, e);
        }

        private void toolStripButton8_Click(object sender, EventArgs e)
        {
            统计报表ToolStripMenuItem_Click(sender, e);
        }

        //private void 借垫款ToolStripMenuItem_Click(object sender, EventArgs e)
        //{
        //    pass = 0;
        //    for (int i = 0; i < this.MdiChildren.Count(); i++)
        //    {
        //        this.MdiChildren[i].Close();
        //    }
        //    FrmBcardJDK frmBcardJDK= new FrmBcardJDK();
        //    frmBcardJDK.MdiParent = this;
        //    frmBcardJDK.Show();
        //}

        private void 留存名贵特产ToolStripMenuItem_Click(object sender, EventArgs e)
        {
            pass = 0;
            for (int i = 0; i < this.MdiChildren.Count(); i++)
            {
                this.MdiChildren[i].Close();
            }
            SelcetFrmWP frmBcardTCWP = new SelcetFrmWP();
            frmBcardTCWP.MdiParent = this;
            frmBcardTCWP.Show();
        }

        private void 部门管理ToolStripMenuItem_Click(object sender, EventArgs e)
        {

        }

        private void 留存名贵特产类物品明细统计表ToolStripMenuItem_Click(object sender, EventArgs e)
        {
            报表主体ToolStripMenuItem_Click(sender, e);
        }

        private void 留存名贵特产类物品明细汇总表ToolStripMenuItem_Click(object sender, EventArgs e)
        {
            统计报表ToolStripMenuItem_Click(sender, e);
        }

        private void 本级单位管理ToolStripMenuItem_Click(object sender, EventArgs e)
        {

            for (int i = 0; i < this.MdiChildren.Count(); i++)
            {
                this.MdiChildren[i].Close();
            }
            danwei danwei = new danwei();
            danwei.MdiParent = this;
            danwei.Show();
            danwei.WindowState = FormWindowState.Normal;

        }

        private void 部门信息ToolStripMenuItem_Click(object sender, EventArgs e)
        {
            for (int i = 0; i < this.MdiChildren.Count(); i++)
            {
                this.MdiChildren[i].Close();
            }
            FrmBcardBM bm = new FrmBcardBM();
            bm.MdiParent = this;
            bm.Left = this.Left + (this.Width - bm.Width) / 2;
            bm.Top = this.Top + (this.Top - bm.Top) / 2;
            bm.Show();
        }

        private void 单位信息ToolStripMenuItem_Click(object sender, EventArgs e)
        {
            for (int i = 0; i < this.MdiChildren.Count(); i++)
            {
                this.MdiChildren[i].Close();
            }
            BJDW danwei = new BJDW();
            danwei.MdiParent = this;
            danwei.Show();
            danwei.WindowState = FormWindowState.Normal;
        }

        private void 留存名贵特产类物品明细统计表ToolStripMenuItem1_Click(object sender, EventArgs e)
        {
            报表主体ToolStripMenuItem_Click(sender, e);
        }

        private void 留存名贵特产类物品明细汇总表ToolStripMenuItem1_Click(object sender, EventArgs e)
        {
            for (int i = 0; i < this.MdiChildren.Count(); i++)
            {
                this.MdiChildren[i].Close();
            }
            SSDWHZ ssdwhz = new SSDWHZ();
            ssdwhz.MdiParent = this;
            ssdwhz.Show();
        }

        private void 所属单位汇总统计表ToolStripMenuItem_Click(object sender, EventArgs e)
        {
            统计报表ToolStripMenuItem_Click(sender, e);

        }

        private void 模板下载ToolStripMenuItem_Click(object sender, EventArgs e)
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

        private void 下级数据接收ToolStripMenuItem_Click(object sender, EventArgs e)
        {
            pass = 0;
            for (int i = 0; i < this.MdiChildren.Count(); i++)
            {
                this.MdiChildren[i].Close();
            }
            receive recevice = new receive();
            recevice.MdiParent = this;
            recevice.Show();
            recevice.WindowState = FormWindowState.Normal;
        }

        private void 数据上报ToolStripMenuItem_Click(object sender, EventArgs e)
        {
            for (int i = 0; i < this.MdiChildren.Count(); i++)
            {
                this.MdiChildren[i].Close();
            }
            report report = new report();
            report.MdiParent = this;
            report.Show();
            report.WindowState = FormWindowState.Normal;
        }

        private void 报表填报说明ToolStripMenuItem_Click(object sender, EventArgs e)
        {
            instruction instruction = new instruction();
            instruction.MdiParent = this;
            instruction.Show();
        }

        private void 数据审核ToolStripMenuItem_Click(object sender, EventArgs e)
        {
            check check = new check();
            check.MdiParent = this;
            check.Show();
            check.WindowState = FormWindowState.Normal;
        }

        private void 导出融通数据ToolStripMenuItem_Click(object sender, EventArgs e)
        {
            for (int i = 0; i < this.MdiChildren.Count(); i++)
            {
                this.MdiChildren[i].Close();
            }
            DCRTSJ dcrtsj = new DCRTSJ();
            dcrtsj.MdiParent = this;
            dcrtsj.Show();
            dcrtsj.WindowState = FormWindowState.Normal;
        }

        private void 物品分布情况统计ToolStripMenuItem_Click(object sender, EventArgs e)
        {
            for (int i = 0; i < this.MdiChildren.Count(); i++)
            {
                this.MdiChildren[i].Close();
            }
            WPFBQKTJ wpfbqktj = new WPFBQKTJ();
            wpfbqktj.MdiParent = this;
            wpfbqktj.Show();
        }

        private void 不对留存名贵特产类物品销毁计划表ToolStripMenuItem_Click(object sender, EventArgs e)
        {
            for (int i = 0; i < this.MdiChildren.Count(); i++)
            {
                this.MdiChildren[i].Close();
            }
            WPXHJHB wpxhjhb = new WPXHJHB();
            wpxhjhb.MdiParent = this;
            wpxhjhb.Show();
        }

        private void 向军队资产管理公司移交名贵特产类物品交接计划表ToolStripMenuItem_Click(object sender, EventArgs e)
        {
            for (int i = 0; i < this.MdiChildren.Count(); i++)
            {
                this.MdiChildren[i].Close();
            }
            WPYJJHB wpyjjhb = new WPYJJHB();
            wpyjjhb.MdiParent = this;
            wpyjjhb.Show();
            
        }

        private void toolStripMenuItem7_Click_1(object sender, EventArgs e)
        {
            AccessHelper m_accessHelper = new AccessHelper();

            string sql_isadd = "select top 1 * from t_dwxx";
            DataTable dt = m_accessHelper.getDataSet(sql_isadd).Tables[0];
            string sql_isadd1 = "select top 1 * from t_lctc";
            DataTable dt1 = m_accessHelper.getDataSet(sql_isadd1).Tables[0];

            string sql_USER = "select  * from t_user where username1='zbgly'";
            DataTable dt_user = m_accessHelper.getDataSet(sql_USER).Tables[0];

            if (dt_user.Rows.Count == 0)
            {
                string sql_adduser = "insert into t_user (username1,password1) values ('zbgly','aaaaaa')";
                m_accessHelper.ExcueteCommand(sql_adduser);
            }
            if (!dt.Columns.Contains("yjdd"))
            {
                string sql_add = "ALTER TABLE t_dwxx  ADD yjdd VARCHAR(200)";
                m_accessHelper.ExcueteCommand(sql_add);
            }

            if (!dt.Columns.Contains("jxzj"))
            {
                string sql_addjxzj = "ALTER TABLE t_dwxx  ADD jxzj VARCHAR(200)";
                m_accessHelper.ExcueteCommand(sql_addjxzj);
            }
            if (!dt.Columns.Contains("jxsj"))
            {
                string sql_addjxsj = "ALTER TABLE t_dwxx  ADD jxsj VARCHAR(200)";
                m_accessHelper.ExcueteCommand(sql_addjxsj);
            }

            if (!dt.Columns.Contains("nxsh"))
            {
                string sql_addnxsh = "ALTER TABLE t_dwxx  ADD nxsh VARCHAR(200)";
                m_accessHelper.ExcueteCommand(sql_addnxsh);
            }

            if (!dt.Columns.Contains("nxs"))
            {
                string sql_addnxs = "ALTER TABLE t_dwxx  ADD nxs VARCHAR(200)";
                m_accessHelper.ExcueteCommand(sql_addnxs);
            }

            if (!dt.Columns.Contains("nxjsd"))
            {
                string sql_addnxjsd = "ALTER TABLE t_dwxx  ADD nxjsd VARCHAR(200)";
                m_accessHelper.ExcueteCommand(sql_addnxjsd);
            }

            if (!dt.Columns.Contains("sfxhwp"))
            {
                string sql_addsfxhwp = "ALTER TABLE t_dwxx  ADD sfxhwp int";
                m_accessHelper.ExcueteCommand(sql_addsfxhwp);
            }
            if (!dt.Columns.Contains("xhsj"))
            {
                string sql_addxhsj = "ALTER TABLE t_dwxx  ADD xhsj datetime";
                m_accessHelper.ExcueteCommand(sql_addxhsj);
            }
            if (!dt.Columns.Contains("xhdd"))
            {
                string sql_addxhdd = "ALTER TABLE t_dwxx  ADD xhdd VARCHAR(200)";
                m_accessHelper.ExcueteCommand(sql_addxhdd);
            }


            if (!dt.Columns.Contains("dwbs"))
            {
                string sql_add5 = "ALTER TABLE t_dwxx  ADD dwbs VARCHAR(200)";
                m_accessHelper.ExcueteCommand(sql_add5);
            }
            if (!dt.Columns.Contains("sszd"))
            {
                string sql_add1 = "ALTER TABLE t_dwxx  ADD sszd VARCHAR(200)";
                m_accessHelper.ExcueteCommand(sql_add1);
            }
            if (!dt1.Columns.Contains("jjqrsl"))
            {
                string sql_add2 = "ALTER TABLE t_lctc  ADD jjqrsl int default 0";
                m_accessHelper.ExcueteCommand(sql_add2);
            }
            if (dt1.Columns.Contains("sl"))
            {
                string sql_add2 = "ALTER TABLE t_lctc  alter column sl decimal(18,5)";
                m_accessHelper.ExcueteCommand(sql_add2);
            }
            if (dt1.Columns.Contains("kysl"))
            {
                string sql_add2 = "ALTER TABLE t_lctc  alter column kysl decimal(18,5)";
                m_accessHelper.ExcueteCommand(sql_add2);
            }

            if (!dt1.Columns.Contains("jjqrpm"))
            {
                string sql_add3 = "ALTER TABLE t_lctc  ADD jjqrpm VARCHAR(200)";
                m_accessHelper.ExcueteCommand(sql_add3);
            }
            if (!dt1.Columns.Contains("jjbz"))
            {
                string sql_add4 = "ALTER TABLE t_lctc  ADD jjbz VARCHAR(200)";
                m_accessHelper.ExcueteCommand(sql_add4);
            }

            string sql_update_sszd = string.Format("update t_dwxx set sszd=szss & '—' & szs & '—' & szx & '—' & xxdz where sszd is null or sszd=''");
            m_accessHelper.ExcueteCommand(sql_update_sszd);

            #region 预置基础数据

            string sql_delete_zd = string.Format("delete from t_tcwpzdb where lb like '%拟选%'");
            m_accessHelper.ExcueteCommand(sql_delete_zd);

            string sql_count_zd = string.Format("select count(1) as zh from t_tcwpzdb where lb='拟选交接地点位省'");
            DataTable dt_count_zd = m_accessHelper.getDataSet(sql_count_zd).Tables[0];
            if (Convert.ToDecimal(dt_count_zd.Rows[0]["zh"].ToString()) == 0)
            {
                string sql_insert = string.Format("insert into t_tcwpzdb(lb,mc,fjbh) values  ('{0}','{1}','{2}')", "拟选交接地点位省", "北京市", "A");
                string sql_insert1 = string.Format("insert into t_tcwpzdb(lb,mc,fjbh) values ('{0}','{1}','{2}')", "拟选交接地点位省", "天津市", "A");
                string sql_insert2 = string.Format("insert into t_tcwpzdb(lb,mc,fjbh) values ('{0}','{1}','{2}')", "拟选交接地点位省", "河北省", "A");
                string sql_insert3 = string.Format("insert into t_tcwpzdb(lb,mc,fjbh) values ('{0}','{1}','{2}')", "拟选交接地点位省", "山西省", "A");
                string sql_insert4 = string.Format("insert into t_tcwpzdb(lb,mc,fjbh) values ('{0}','{1}','{2}')", "拟选交接地点位省", "内蒙古自治区", "A");
                string sql_insert5 = string.Format("insert into t_tcwpzdb(lb,mc,fjbh) values ('{0}','{1}','{2}')", "拟选交接地点位省", "辽宁省", "A");
                string sql_insert6 = string.Format("insert into t_tcwpzdb(lb,mc,fjbh) values ('{0}','{1}','{2}')", "拟选交接地点位省", "吉林省", "A");
                string sql_insert7 = string.Format("insert into t_tcwpzdb(lb,mc,fjbh) values ('{0}','{1}','{2}')", "拟选交接地点位省", "黑龙江省", "A");
                string sql_insert8 = string.Format("insert into t_tcwpzdb(lb,mc,fjbh) values ('{0}','{1}','{2}')", "拟选交接地点位省", "上海市", "A");
                string sql_insert9 = string.Format("insert into t_tcwpzdb(lb,mc,fjbh) values ('{0}','{1}','{2}')", "拟选交接地点位省", "江苏省", "A");
                string sql_insert10 = string.Format("insert into t_tcwpzdb(lb,mc,fjbh) values ('{0}','{1}','{2}')", "拟选交接地点位省", "浙江省", "A");
                string sql_insert11 = string.Format("insert into t_tcwpzdb(lb,mc,fjbh) values ('{0}','{1}','{2}')", "拟选交接地点位省", "安徽省", "A");
                string sql_insert12 = string.Format("insert into t_tcwpzdb(lb,mc,fjbh) values ('{0}','{1}','{2}')", "拟选交接地点位省", "福建省", "A");
                string sql_insert13 = string.Format("insert into t_tcwpzdb(lb,mc,fjbh) values ('{0}','{1}','{2}')", "拟选交接地点位省", "江西省", "A");
                string sql_insert14 = string.Format("insert into t_tcwpzdb(lb,mc,fjbh) values ('{0}','{1}','{2}')", "拟选交接地点位省", "山东省", "A");
                string sql_insert15 = string.Format("insert into t_tcwpzdb(lb,mc,fjbh) values ('{0}','{1}','{2}')", "拟选交接地点位省", "河南省", "A");
                string sql_insert16 = string.Format("insert into t_tcwpzdb(lb,mc,fjbh) values ('{0}','{1}','{2}')", "拟选交接地点位省", "湖北省", "A");
                string sql_insert17 = string.Format("insert into t_tcwpzdb(lb,mc,fjbh) values ('{0}','{1}','{2}')", "拟选交接地点位省", "湖南省", "A");
                string sql_insert18 = string.Format("insert into t_tcwpzdb(lb,mc,fjbh) values ('{0}','{1}','{2}')", "拟选交接地点位省", "广东省", "A");
                string sql_insert19 = string.Format("insert into t_tcwpzdb(lb,mc,fjbh) values ('{0}','{1}','{2}')", "拟选交接地点位省", "广西壮族自治区", "A");
                string sql_insert20 = string.Format("insert into t_tcwpzdb(lb,mc,fjbh) values ('{0}','{1}','{2}')", "拟选交接地点位省", "海南省", "A");
                string sql_insert21 = string.Format("insert into t_tcwpzdb(lb,mc,fjbh) values ('{0}','{1}','{2}')", "拟选交接地点位省", "重庆市", "A");
                string sql_insert22 = string.Format("insert into t_tcwpzdb(lb,mc,fjbh) values ('{0}','{1}','{2}')", "拟选交接地点位省", "四川省", "A");
                string sql_insert23 = string.Format("insert into t_tcwpzdb(lb,mc,fjbh) values ('{0}','{1}','{2}')", "拟选交接地点位省", "贵州省", "A");
                string sql_insert24 = string.Format("insert into t_tcwpzdb(lb,mc,fjbh) values ('{0}','{1}','{2}')", "拟选交接地点位省", "云南省", "A");
                string sql_insert25 = string.Format("insert into t_tcwpzdb(lb,mc,fjbh) values ('{0}','{1}','{2}')", "拟选交接地点位省", "西藏自治区", "A");
                string sql_insert26 = string.Format("insert into t_tcwpzdb(lb,mc,fjbh) values ('{0}','{1}','{2}')", "拟选交接地点位省", "陕西省", "A");
                string sql_insert27 = string.Format("insert into t_tcwpzdb(lb,mc,fjbh) values ('{0}','{1}','{2}')", "拟选交接地点位省", "甘肃省", "A");
                string sql_insert28 = string.Format("insert into t_tcwpzdb(lb,mc,fjbh) values ('{0}','{1}','{2}')", "拟选交接地点位省", "青海省", "A");
                string sql_insert29 = string.Format("insert into t_tcwpzdb(lb,mc,fjbh) values ('{0}','{1}','{2}')", "拟选交接地点位省", "宁夏回族自治区", "A");
                string sql_insert30 = string.Format("insert into t_tcwpzdb(lb,mc,fjbh) values ('{0}','{1}','{2}')", "拟选交接地点位省", "新疆维吾尔自治区", "A");
                string sql_insert31 = string.Format("insert into t_tcwpzdb(lb,mc,fjbh) values ('{0}','{1}','{2}')", "拟选交接地点位市", "北京市", "北京市");
                string sql_insert32 = string.Format("insert into t_tcwpzdb(lb,mc,fjbh) values ('{0}','{1}','{2}')", "拟选交接地点位市", "天津市", "天津市");
                string sql_insert33 = string.Format("insert into t_tcwpzdb(lb,mc,fjbh) values ('{0}','{1}','{2}')", "拟选交接地点位市", "石家庄市", "河北省");
                string sql_insert34 = string.Format("insert into t_tcwpzdb(lb,mc,fjbh) values ('{0}','{1}','{2}')", "拟选交接地点位市", "张家口市", "河北省");
                string sql_insert35 = string.Format("insert into t_tcwpzdb(lb,mc,fjbh) values ('{0}','{1}','{2}')", "拟选交接地点位市", "太原市", "山西省");
                string sql_insert36 = string.Format("insert into t_tcwpzdb(lb,mc,fjbh) values ('{0}','{1}','{2}')", "拟选交接地点位市", "忻州市", "山西省");
                string sql_insert37 = string.Format("insert into t_tcwpzdb(lb,mc,fjbh) values ('{0}','{1}','{2}')", "拟选交接地点位市", "呼和浩特市", "内蒙古自治区");
                string sql_insert38 = string.Format("insert into t_tcwpzdb(lb,mc,fjbh) values ('{0}','{1}','{2}')", "拟选交接地点位市", "阿拉善盟", "内蒙古自治区");
                string sql_insert39 = string.Format("insert into t_tcwpzdb(lb,mc,fjbh) values ('{0}','{1}','{2}')", "拟选交接地点位市", "锡林郭勒盟", "内蒙古自治区");
                string sql_insert40 = string.Format("insert into t_tcwpzdb(lb,mc,fjbh) values ('{0}','{1}','{2}')", "拟选交接地点位市", "沈阳市", "辽宁省");
                string sql_insert41 = string.Format("insert into t_tcwpzdb(lb,mc,fjbh) values ('{0}','{1}','{2}')", "拟选交接地点位市", "大连市", "辽宁省");
                string sql_insert42 = string.Format("insert into t_tcwpzdb(lb,mc,fjbh) values ('{0}','{1}','{2}')", "拟选交接地点位市", "长春市", "吉林省");
                string sql_insert43 = string.Format("insert into t_tcwpzdb(lb,mc,fjbh) values ('{0}','{1}','{2}')", "拟选交接地点位市", "哈尔滨市", "黑龙江省");
                string sql_insert44 = string.Format("insert into t_tcwpzdb(lb,mc,fjbh) values ('{0}','{1}','{2}')", "拟选交接地点位市", "上海市", "上海市");
                string sql_insert45 = string.Format("insert into t_tcwpzdb(lb,mc,fjbh) values ('{0}','{1}','{2}')", "拟选交接地点位市", "南京市", "江苏省");
                string sql_insert46 = string.Format("insert into t_tcwpzdb(lb,mc,fjbh) values ('{0}','{1}','{2}')", "拟选交接地点位市", "徐州市", "江苏省");
                string sql_insert47 = string.Format("insert into t_tcwpzdb(lb,mc,fjbh) values ('{0}','{1}','{2}')", "拟选交接地点位市", "杭州市", "浙江省");
                string sql_insert48 = string.Format("insert into t_tcwpzdb(lb,mc,fjbh) values ('{0}','{1}','{2}')", "拟选交接地点位市", "宁波市", "浙江省");
                string sql_insert49 = string.Format("insert into t_tcwpzdb(lb,mc,fjbh) values ('{0}','{1}','{2}')", "拟选交接地点位市", "合肥市", "安徽省");
                string sql_insert50 = string.Format("insert into t_tcwpzdb(lb,mc,fjbh) values ('{0}','{1}','{2}')", "拟选交接地点位市", "蚌埠市", "安徽省");
                string sql_insert51 = string.Format("insert into t_tcwpzdb(lb,mc,fjbh) values ('{0}','{1}','{2}')", "拟选交接地点位市", "福州市", "福建省");
                string sql_insert52 = string.Format("insert into t_tcwpzdb(lb,mc,fjbh) values ('{0}','{1}','{2}')", "拟选交接地点位市", "南昌市", "江西省");
                string sql_insert53 = string.Format("insert into t_tcwpzdb(lb,mc,fjbh) values ('{0}','{1}','{2}')", "拟选交接地点位市", "济南市", "山东省");
                string sql_insert54 = string.Format("insert into t_tcwpzdb(lb,mc,fjbh) values ('{0}','{1}','{2}')", "拟选交接地点位市", "青岛市", "山东省");
                string sql_insert55 = string.Format("insert into t_tcwpzdb(lb,mc,fjbh) values ('{0}','{1}','{2}')", "拟选交接地点位市", "郑州市", "河南省");
                string sql_insert56 = string.Format("insert into t_tcwpzdb(lb,mc,fjbh) values ('{0}','{1}','{2}')", "拟选交接地点位市", "洛阳市", "河南省");
                string sql_insert57 = string.Format("insert into t_tcwpzdb(lb,mc,fjbh) values ('{0}','{1}','{2}')", "拟选交接地点位市", "武汉市", "湖北省");
                string sql_insert58 = string.Format("insert into t_tcwpzdb(lb,mc,fjbh) values ('{0}','{1}','{2}')", "拟选交接地点位市", "长沙市", "湖南省");
                string sql_insert59 = string.Format("insert into t_tcwpzdb(lb,mc,fjbh) values ('{0}','{1}','{2}')", "拟选交接地点位市", "怀化市", "湖南省");
                string sql_insert60 = string.Format("insert into t_tcwpzdb(lb,mc,fjbh) values ('{0}','{1}','{2}')", "拟选交接地点位市", "广州市", "广东省");
                string sql_insert61 = string.Format("insert into t_tcwpzdb(lb,mc,fjbh) values ('{0}','{1}','{2}')", "拟选交接地点位市", "湛江市", "广东省");
                string sql_insert62 = string.Format("insert into t_tcwpzdb(lb,mc,fjbh) values ('{0}','{1}','{2}')", "拟选交接地点位市", "南宁市", "广西壮族自治区");
                string sql_insert63 = string.Format("insert into t_tcwpzdb(lb,mc,fjbh) values ('{0}','{1}','{2}')", "拟选交接地点位市", "海口市", "海南省");
                string sql_insert64 = string.Format("insert into t_tcwpzdb(lb,mc,fjbh) values ('{0}','{1}','{2}')", "拟选交接地点位市", "三亚市", "海南省");
                string sql_insert65 = string.Format("insert into t_tcwpzdb(lb,mc,fjbh) values ('{0}','{1}','{2}')", "拟选交接地点位市", "重庆市", "重庆市");
                string sql_insert66 = string.Format("insert into t_tcwpzdb(lb,mc,fjbh) values ('{0}','{1}','{2}')", "拟选交接地点位市", "成都市", "四川省");
                string sql_insert67 = string.Format("insert into t_tcwpzdb(lb,mc,fjbh) values ('{0}','{1}','{2}')", "拟选交接地点位市", "绵阳市", "四川省");
                string sql_insert68 = string.Format("insert into t_tcwpzdb(lb,mc,fjbh) values ('{0}','{1}','{2}')", "拟选交接地点位市", "贵阳市", "贵州省");
                string sql_insert69 = string.Format("insert into t_tcwpzdb(lb,mc,fjbh) values ('{0}','{1}','{2}')", "拟选交接地点位市", "昆明市", "云南省");
                string sql_insert70 = string.Format("insert into t_tcwpzdb(lb,mc,fjbh) values ('{0}','{1}','{2}')", "拟选交接地点位市", "拉萨市", "西藏自治区");
                string sql_insert71 = string.Format("insert into t_tcwpzdb(lb,mc,fjbh) values ('{0}','{1}','{2}')", "拟选交接地点位市", "林芝地区", "西藏自治区");
                string sql_insert72 = string.Format("insert into t_tcwpzdb(lb,mc,fjbh) values ('{0}','{1}','{2}')", "拟选交接地点位市", "西安市", "陕西省");
                string sql_insert73 = string.Format("insert into t_tcwpzdb(lb,mc,fjbh) values ('{0}','{1}','{2}')", "拟选交接地点位市", "宝鸡市", "陕西省");
                string sql_insert74 = string.Format("insert into t_tcwpzdb(lb,mc,fjbh) values ('{0}','{1}','{2}')", "拟选交接地点位市", "渭南市", "陕西省");
                string sql_insert75 = string.Format("insert into t_tcwpzdb(lb,mc,fjbh) values ('{0}','{1}','{2}')", "拟选交接地点位市", "兰州市", "甘肃省");
                string sql_insert76 = string.Format("insert into t_tcwpzdb(lb,mc,fjbh) values ('{0}','{1}','{2}')", "拟选交接地点位市", "西宁市", "青海省");
                string sql_insert77 = string.Format("insert into t_tcwpzdb(lb,mc,fjbh) values ('{0}','{1}','{2}')", "拟选交接地点位市", "海西蒙古族藏族自治州区", "青海省");
                string sql_insert78 = string.Format("insert into t_tcwpzdb(lb,mc,fjbh) values ('{0}','{1}','{2}')", "拟选交接地点位市", "银川市", "宁夏回族自治区");
                string sql_insert79 = string.Format("insert into t_tcwpzdb(lb,mc,fjbh) values ('{0}','{1}','{2}')", "拟选交接地点位市", "乌鲁木齐市", "新疆维吾尔自治区");
                string sql_insert80 = string.Format("insert into t_tcwpzdb(lb,mc,fjbh) values ('{0}','{1}','{2}')", "拟选交接地点位市", "巴音郭楞蒙古自治州", "新疆维吾尔自治区");




                m_accessHelper.ExcueteCommand(sql_insert);
                m_accessHelper.ExcueteCommand(sql_insert1);
                m_accessHelper.ExcueteCommand(sql_insert2);
                m_accessHelper.ExcueteCommand(sql_insert3);
                m_accessHelper.ExcueteCommand(sql_insert4);
                m_accessHelper.ExcueteCommand(sql_insert5);
                m_accessHelper.ExcueteCommand(sql_insert6);
                m_accessHelper.ExcueteCommand(sql_insert7);
                m_accessHelper.ExcueteCommand(sql_insert8);
                m_accessHelper.ExcueteCommand(sql_insert9);
                m_accessHelper.ExcueteCommand(sql_insert10);
                m_accessHelper.ExcueteCommand(sql_insert11);
                m_accessHelper.ExcueteCommand(sql_insert12);
                m_accessHelper.ExcueteCommand(sql_insert13);
                m_accessHelper.ExcueteCommand(sql_insert14);
                m_accessHelper.ExcueteCommand(sql_insert15);
                m_accessHelper.ExcueteCommand(sql_insert16);
                m_accessHelper.ExcueteCommand(sql_insert17);
                m_accessHelper.ExcueteCommand(sql_insert18);
                m_accessHelper.ExcueteCommand(sql_insert19);
                m_accessHelper.ExcueteCommand(sql_insert20);
                m_accessHelper.ExcueteCommand(sql_insert21);
                m_accessHelper.ExcueteCommand(sql_insert22);
                m_accessHelper.ExcueteCommand(sql_insert23);
                m_accessHelper.ExcueteCommand(sql_insert24);
                m_accessHelper.ExcueteCommand(sql_insert25);
                m_accessHelper.ExcueteCommand(sql_insert26);
                m_accessHelper.ExcueteCommand(sql_insert27);
                m_accessHelper.ExcueteCommand(sql_insert28);
                m_accessHelper.ExcueteCommand(sql_insert29);
                m_accessHelper.ExcueteCommand(sql_insert30);
                m_accessHelper.ExcueteCommand(sql_insert31);
                m_accessHelper.ExcueteCommand(sql_insert32);
                m_accessHelper.ExcueteCommand(sql_insert33);
                m_accessHelper.ExcueteCommand(sql_insert34);
                m_accessHelper.ExcueteCommand(sql_insert35);
                m_accessHelper.ExcueteCommand(sql_insert36);
                m_accessHelper.ExcueteCommand(sql_insert37);
                m_accessHelper.ExcueteCommand(sql_insert38);
                m_accessHelper.ExcueteCommand(sql_insert39);
                m_accessHelper.ExcueteCommand(sql_insert40);
                m_accessHelper.ExcueteCommand(sql_insert41);
                m_accessHelper.ExcueteCommand(sql_insert42);
                m_accessHelper.ExcueteCommand(sql_insert43);
                m_accessHelper.ExcueteCommand(sql_insert44);
                m_accessHelper.ExcueteCommand(sql_insert45);
                m_accessHelper.ExcueteCommand(sql_insert46);
                m_accessHelper.ExcueteCommand(sql_insert47);
                m_accessHelper.ExcueteCommand(sql_insert48);
                m_accessHelper.ExcueteCommand(sql_insert49);
                m_accessHelper.ExcueteCommand(sql_insert50);
                m_accessHelper.ExcueteCommand(sql_insert51);
                m_accessHelper.ExcueteCommand(sql_insert52);
                m_accessHelper.ExcueteCommand(sql_insert53);
                m_accessHelper.ExcueteCommand(sql_insert54);
                m_accessHelper.ExcueteCommand(sql_insert55);
                m_accessHelper.ExcueteCommand(sql_insert56);
                m_accessHelper.ExcueteCommand(sql_insert57);
                m_accessHelper.ExcueteCommand(sql_insert58);
                m_accessHelper.ExcueteCommand(sql_insert59);
                m_accessHelper.ExcueteCommand(sql_insert60);
                m_accessHelper.ExcueteCommand(sql_insert61);
                m_accessHelper.ExcueteCommand(sql_insert62);
                m_accessHelper.ExcueteCommand(sql_insert63);
                m_accessHelper.ExcueteCommand(sql_insert64);
                m_accessHelper.ExcueteCommand(sql_insert65);
                m_accessHelper.ExcueteCommand(sql_insert66);
                m_accessHelper.ExcueteCommand(sql_insert67);
                m_accessHelper.ExcueteCommand(sql_insert68);
                m_accessHelper.ExcueteCommand(sql_insert69);
                m_accessHelper.ExcueteCommand(sql_insert70);
                m_accessHelper.ExcueteCommand(sql_insert71);
                m_accessHelper.ExcueteCommand(sql_insert72);
                m_accessHelper.ExcueteCommand(sql_insert73);
                m_accessHelper.ExcueteCommand(sql_insert74);
                m_accessHelper.ExcueteCommand(sql_insert75);
                m_accessHelper.ExcueteCommand(sql_insert76);
                m_accessHelper.ExcueteCommand(sql_insert77);
                m_accessHelper.ExcueteCommand(sql_insert78);
                m_accessHelper.ExcueteCommand(sql_insert79);
                m_accessHelper.ExcueteCommand(sql_insert80);
            }

            string sql_count_dw = string.Format("select count(1) as zh from t_tcwpzdb where lb='拟选位'");
            DataTable dt_count_dw = m_accessHelper.getDataSet(sql_count_dw).Tables[0];
            if (Convert.ToDecimal(dt_count_zd.Rows[0]["zh"].ToString()) == 0)
            {
                string sql_insert = string.Format("insert into t_tcwpzdb(lb,mc,fjbh) values  ('{0}','{1}','{2}')", "拟选位", "军委办公厅", "北京市");
                string sql_insert1 = string.Format("insert into t_tcwpzdb(lb,mc,fjbh) values ('{0}','{1}','{2}')", "拟选位", "军委联合参谋部", "北京市");
                string sql_insert2 = string.Format("insert into t_tcwpzdb(lb,mc,fjbh) values ('{0}','{1}','{2}')", "拟选位", "军委政治工作部", "北京市");
                string sql_insert3 = string.Format("insert into t_tcwpzdb(lb,mc,fjbh) values ('{0}','{1}','{2}')", "拟选位", "军委后勤保障部", "北京市");
                string sql_insert4 = string.Format("insert into t_tcwpzdb(lb,mc,fjbh) values ('{0}','{1}','{2}')", "拟选位", "军委装备发展部", "北京市");
                string sql_insert5 = string.Format("insert into t_tcwpzdb(lb,mc,fjbh) values ('{0}','{1}','{2}')", "拟选位", "军委训练管理部", "北京市");
                string sql_insert6 = string.Format("insert into t_tcwpzdb(lb,mc,fjbh) values ('{0}','{1}','{2}')", "拟选位", "军委国防动员部", "北京市");
                string sql_insert7 = string.Format("insert into t_tcwpzdb(lb,mc,fjbh) values ('{0}','{1}','{2}')", "拟选位", "军委纪律检查委员会", "北京市");
                string sql_insert8 = string.Format("insert into t_tcwpzdb(lb,mc,fjbh) values ('{0}','{1}','{2}')", "拟选位", "军委政法委员会", "北京市");
                string sql_insert9 = string.Format("insert into t_tcwpzdb(lb,mc,fjbh) values ('{0}','{1}','{2}')", "拟选位", "军委科学技术委员会", "北京市");
                string sql_insert10 = string.Format("insert into t_tcwpzdb(lb,mc,fjbh) values ('{0}','{1}','{2}')", "拟选位", "军委战略规划办公室", "北京市");
                string sql_insert11 = string.Format("insert into t_tcwpzdb(lb,mc,fjbh) values ('{0}','{1}','{2}')", "拟选位", "军委国际军事合作办公室", "北京市");
                string sql_insert12 = string.Format("insert into t_tcwpzdb(lb,mc,fjbh) values ('{0}','{1}','{2}')", "拟选位", "军委改革和编制办公室", "北京市");
                string sql_insert13 = string.Format("insert into t_tcwpzdb(lb,mc,fjbh) values ('{0}','{1}','{2}')", "拟选位", "军委审计署", "北京市");
                string sql_insert14 = string.Format("insert into t_tcwpzdb(lb,mc,fjbh) values ('{0}','{1}','{2}')", "拟选位", "机关事务管理总局", "北京市");
                string sql_insert15 = string.Format("insert into t_tcwpzdb(lb,mc,fjbh) values ('{0}','{1}','{2}')", "拟选位", "中部战区", "北京市");
                string sql_insert16 = string.Format("insert into t_tcwpzdb(lb,mc,fjbh) values ('{0}','{1}','{2}')", "拟选位", "陆军", "北京市");
                string sql_insert17 = string.Format("insert into t_tcwpzdb(lb,mc,fjbh) values ('{0}','{1}','{2}')", "拟选位", "海军", "北京市");
                string sql_insert18 = string.Format("insert into t_tcwpzdb(lb,mc,fjbh) values ('{0}','{1}','{2}')", "拟选位", "空军", "北京市");
                string sql_insert19 = string.Format("insert into t_tcwpzdb(lb,mc,fjbh) values ('{0}','{1}','{2}')", "拟选位", "火箭军", "北京市");
                string sql_insert20 = string.Format("insert into t_tcwpzdb(lb,mc,fjbh) values ('{0}','{1}','{2}')", "拟选位", "战略支援部队", "北京市");
                string sql_insert21 = string.Format("insert into t_tcwpzdb(lb,mc,fjbh) values ('{0}','{1}','{2}')", "拟选位", "军事科学院", "北京市");
                string sql_insert22 = string.Format("insert into t_tcwpzdb(lb,mc,fjbh) values ('{0}','{1}','{2}')", "拟选位", "国防大学", "北京市");
                string sql_insert23 = string.Format("insert into t_tcwpzdb(lb,mc,fjbh) values ('{0}','{1}','{2}')", "拟选位", "武警部队", "北京市");

                string sql_insert25 = string.Format("insert into t_tcwpzdb(lb,mc,fjbh) values ('{0}','{1}','{2}')", "拟选位", "天津警备区", "天津市");
                string sql_insert26 = string.Format("insert into t_tcwpzdb(lb,mc,fjbh) values ('{0}','{1}','{2}')", "拟选位", "河北省军区", "石家庄市");
                string sql_insert27 = string.Format("insert into t_tcwpzdb(lb,mc,fjbh) values ('{0}','{1}','{2}')", "拟选位", "河北片区张家口办事处", "张家口市");
                string sql_insert28 = string.Format("insert into t_tcwpzdb(lb,mc,fjbh) values ('{0}','{1}','{2}')", "拟选位", "山西省军区", "太原市");
                string sql_insert29 = string.Format("insert into t_tcwpzdb(lb,mc,fjbh) values ('{0}','{1}','{2}')", "拟选位", "战略支援部队第二十五基地", "忻州市");
                string sql_insert30 = string.Format("insert into t_tcwpzdb(lb,mc,fjbh) values ('{0}','{1}','{2}')", "拟选位", "内蒙古省军区", "呼和浩特市");
                string sql_insert31 = string.Format("insert into t_tcwpzdb(lb,mc,fjbh) values ('{0}','{1}','{2}')", "拟选位", "战略支援部队第二十基地", "阿拉善盟");
                string sql_insert32 = string.Format("insert into t_tcwpzdb(lb,mc,fjbh) values ('{0}','{1}','{2}')", "拟选位", "朱日和联合训练基地", "锡林郭勒盟");
                string sql_insert33 = string.Format("insert into t_tcwpzdb(lb,mc,fjbh) values ('{0}','{1}','{2}')", "拟选位", "辽宁省军区", "沈阳市");
                string sql_insert34 = string.Format("insert into t_tcwpzdb(lb,mc,fjbh) values ('{0}','{1}','{2}')", "拟选位", "联勤保障部队大连康复疗养中心", "大连市");
                string sql_insert35 = string.Format("insert into t_tcwpzdb(lb,mc,fjbh) values ('{0}','{1}','{2}')", "拟选位", "吉林省军区", "长春市");
                string sql_insert36 = string.Format("insert into t_tcwpzdb(lb,mc,fjbh) values ('{0}','{1}','{2}')", "拟选位", "黑龙江省军区", "哈尔滨市");
                string sql_insert37 = string.Format("insert into t_tcwpzdb(lb,mc,fjbh) values ('{0}','{1}','{2}')", "拟选位", "上海警报区", "上海市");
                string sql_insert38 = string.Format("insert into t_tcwpzdb(lb,mc,fjbh) values ('{0}','{1}','{2}')", "拟选位", "南京军区善后工作办公室", "南京市");
                string sql_insert39 = string.Format("insert into t_tcwpzdb(lb,mc,fjbh) values ('{0}','{1}','{2}')", "拟选位", "徐州军分区", "徐州市");
                string sql_insert40 = string.Format("insert into t_tcwpzdb(lb,mc,fjbh) values ('{0}','{1}','{2}')", "拟选位", "浙江省军区", "杭州市");
                string sql_insert41 = string.Format("insert into t_tcwpzdb(lb,mc,fjbh) values ('{0}','{1}','{2}')", "拟选位", "东部战区海军", "宁波市");
                string sql_insert42 = string.Format("insert into t_tcwpzdb(lb,mc,fjbh) values ('{0}','{1}','{2}')", "拟选位", "安徽省军区", "合肥市");
                string sql_insert43 = string.Format("insert into t_tcwpzdb(lb,mc,fjbh) values ('{0}','{1}','{2}')", "拟选位", "陆军军事交通学院汽车士官学院", "蚌埠市");
                string sql_insert44 = string.Format("insert into t_tcwpzdb(lb,mc,fjbh) values ('{0}','{1}','{2}')", "拟选位", "福建省军区", "福州市");
                string sql_insert45 = string.Format("insert into t_tcwpzdb(lb,mc,fjbh) values ('{0}','{1}','{2}')", "拟选位", "江西省军区", "南昌市");
                string sql_insert46 = string.Format("insert into t_tcwpzdb(lb,mc,fjbh) values ('{0}','{1}','{2}')", "拟选位", "山东省军区", "济南市");
                string sql_insert47 = string.Format("insert into t_tcwpzdb(lb,mc,fjbh) values ('{0}','{1}','{2}')", "拟选位", "北部战区海军", "青岛市");
                string sql_insert48 = string.Format("insert into t_tcwpzdb(lb,mc,fjbh) values ('{0}','{1}','{2}')", "拟选位", "河南省军区", "郑州市");
                string sql_insert49 = string.Format("insert into t_tcwpzdb(lb,mc,fjbh) values ('{0}','{1}','{2}')", "拟选位", "火箭军第六十六基地", "洛阳市");
                string sql_insert50 = string.Format("insert into t_tcwpzdb(lb,mc,fjbh) values ('{0}','{1}','{2}')", "拟选位", "湖北省军区", "武汉市");
                string sql_insert51 = string.Format("insert into t_tcwpzdb(lb,mc,fjbh) values ('{0}','{1}','{2}')", "拟选位", "湖南省军区", "长沙市");
                string sql_insert52 = string.Format("insert into t_tcwpzdb(lb,mc,fjbh) values ('{0}','{1}','{2}')", "拟选位", "火箭军第六十三基地", "怀化市");
                string sql_insert53 = string.Format("insert into t_tcwpzdb(lb,mc,fjbh) values ('{0}','{1}','{2}')", "拟选位", "广州军区善后办", "广州市");
                string sql_insert54 = string.Format("insert into t_tcwpzdb(lb,mc,fjbh) values ('{0}','{1}','{2}')", "拟选位", "南部战区海军", "湛江市");
                string sql_insert55 = string.Format("insert into t_tcwpzdb(lb,mc,fjbh) values ('{0}','{1}','{2}')", "拟选位", "广西省军区", "南宁市");
                string sql_insert56 = string.Format("insert into t_tcwpzdb(lb,mc,fjbh) values ('{0}','{1}','{2}')", "拟选位", "海南省军区", "海口市");
                string sql_insert57 = string.Format("insert into t_tcwpzdb(lb,mc,fjbh) values ('{0}','{1}','{2}')", "拟选位", "三亚警备区", "三亚市");
                string sql_insert58 = string.Format("insert into t_tcwpzdb(lb,mc,fjbh) values ('{0}','{1}','{2}')", "拟选位", "重庆警备区", "重庆市");
                string sql_insert59 = string.Format("insert into t_tcwpzdb(lb,mc,fjbh) values ('{0}','{1}','{2}')", "拟选位", "成都军区善后工作办公室", "成都市");
                string sql_insert60 = string.Format("insert into t_tcwpzdb(lb,mc,fjbh) values ('{0}','{1}','{2}')", "拟选位", "军事科学院空气动力实验基地", "绵阳市");
                string sql_insert61 = string.Format("insert into t_tcwpzdb(lb,mc,fjbh) values ('{0}','{1}','{2}')", "拟选位", "贵州省军区", "贵阳市");
                string sql_insert62 = string.Format("insert into t_tcwpzdb(lb,mc,fjbh) values ('{0}','{1}','{2}')", "拟选位", "云南省军区", "昆明市");
                string sql_insert63 = string.Format("insert into t_tcwpzdb(lb,mc,fjbh) values ('{0}','{1}','{2}')", "拟选位", "西藏军区", "拉萨市");
                string sql_insert64 = string.Format("insert into t_tcwpzdb(lb,mc,fjbh) values ('{0}','{1}','{2}')", "拟选位", "西藏林芝军分区", "林芝地区");
                string sql_insert65 = string.Format("insert into t_tcwpzdb(lb,mc,fjbh) values ('{0}','{1}','{2}')", "拟选位", "陕西省军区", "西安市");
                string sql_insert66 = string.Format("insert into t_tcwpzdb(lb,mc,fjbh) values ('{0}','{1}','{2}')", "拟选位", "火箭军第六十七基地", "宝鸡市");
                string sql_insert67 = string.Format("insert into t_tcwpzdb(lb,mc,fjbh) values ('{0}','{1}','{2}')", "拟选位", "战略支援部队第二十六基地技术勤务站", "渭南市");
                string sql_insert68 = string.Format("insert into t_tcwpzdb(lb,mc,fjbh) values ('{0}','{1}','{2}')", "拟选位", "甘肃省军区", "兰州市");
                string sql_insert69 = string.Format("insert into t_tcwpzdb(lb,mc,fjbh) values ('{0}','{1}','{2}')", "拟选位", "青海省军区", "西宁市");
                string sql_insert70 = string.Format("insert into t_tcwpzdb(lb,mc,fjbh) values ('{0}','{1}','{2}')", "拟选位", "青藏兵站部格尔木大站", "海西蒙古族藏族自治州区");
                string sql_insert71 = string.Format("insert into t_tcwpzdb(lb,mc,fjbh) values ('{0}','{1}','{2}')", "拟选位", "宁夏军区", "银川市");
                string sql_insert72 = string.Format("insert into t_tcwpzdb(lb,mc,fjbh) values ('{0}','{1}','{2}')", "拟选位", "新疆生产建设兵团军事部", "乌鲁木齐市");
                string sql_insert73 = string.Format("insert into t_tcwpzdb(lb,mc,fjbh) values ('{0}','{1}','{2}')", "拟选位", "第二十一实验训练基地", "巴音郭楞蒙古自治州");

                m_accessHelper.ExcueteCommand(sql_insert);
                m_accessHelper.ExcueteCommand(sql_insert1);
                m_accessHelper.ExcueteCommand(sql_insert2);
                m_accessHelper.ExcueteCommand(sql_insert3);
                m_accessHelper.ExcueteCommand(sql_insert4);
                m_accessHelper.ExcueteCommand(sql_insert5);
                m_accessHelper.ExcueteCommand(sql_insert6);
                m_accessHelper.ExcueteCommand(sql_insert7);
                m_accessHelper.ExcueteCommand(sql_insert8);
                m_accessHelper.ExcueteCommand(sql_insert9);
                m_accessHelper.ExcueteCommand(sql_insert10);
                m_accessHelper.ExcueteCommand(sql_insert11);
                m_accessHelper.ExcueteCommand(sql_insert12);
                m_accessHelper.ExcueteCommand(sql_insert13);
                m_accessHelper.ExcueteCommand(sql_insert14);
                m_accessHelper.ExcueteCommand(sql_insert15);
                m_accessHelper.ExcueteCommand(sql_insert16);
                m_accessHelper.ExcueteCommand(sql_insert17);
                m_accessHelper.ExcueteCommand(sql_insert18);
                m_accessHelper.ExcueteCommand(sql_insert19);
                m_accessHelper.ExcueteCommand(sql_insert20);
                m_accessHelper.ExcueteCommand(sql_insert21);
                m_accessHelper.ExcueteCommand(sql_insert22);
                m_accessHelper.ExcueteCommand(sql_insert23);
                m_accessHelper.ExcueteCommand(sql_insert25);
                m_accessHelper.ExcueteCommand(sql_insert26);
                m_accessHelper.ExcueteCommand(sql_insert27);
                m_accessHelper.ExcueteCommand(sql_insert28);
                m_accessHelper.ExcueteCommand(sql_insert29);
                m_accessHelper.ExcueteCommand(sql_insert30);
                m_accessHelper.ExcueteCommand(sql_insert31);
                m_accessHelper.ExcueteCommand(sql_insert32);
                m_accessHelper.ExcueteCommand(sql_insert33);
                m_accessHelper.ExcueteCommand(sql_insert34);
                m_accessHelper.ExcueteCommand(sql_insert35);
                m_accessHelper.ExcueteCommand(sql_insert36);
                m_accessHelper.ExcueteCommand(sql_insert37);
                m_accessHelper.ExcueteCommand(sql_insert38);
                m_accessHelper.ExcueteCommand(sql_insert39);
                m_accessHelper.ExcueteCommand(sql_insert40);
                m_accessHelper.ExcueteCommand(sql_insert41);
                m_accessHelper.ExcueteCommand(sql_insert42);
                m_accessHelper.ExcueteCommand(sql_insert43);
                m_accessHelper.ExcueteCommand(sql_insert44);
                m_accessHelper.ExcueteCommand(sql_insert45);
                m_accessHelper.ExcueteCommand(sql_insert46);
                m_accessHelper.ExcueteCommand(sql_insert47);
                m_accessHelper.ExcueteCommand(sql_insert48);
                m_accessHelper.ExcueteCommand(sql_insert49);
                m_accessHelper.ExcueteCommand(sql_insert50);
                m_accessHelper.ExcueteCommand(sql_insert51);
                m_accessHelper.ExcueteCommand(sql_insert52);
                m_accessHelper.ExcueteCommand(sql_insert53);
                m_accessHelper.ExcueteCommand(sql_insert54);
                m_accessHelper.ExcueteCommand(sql_insert55);
                m_accessHelper.ExcueteCommand(sql_insert56);
                m_accessHelper.ExcueteCommand(sql_insert57);
                m_accessHelper.ExcueteCommand(sql_insert58);
                m_accessHelper.ExcueteCommand(sql_insert59);
                m_accessHelper.ExcueteCommand(sql_insert60);
                m_accessHelper.ExcueteCommand(sql_insert61);
                m_accessHelper.ExcueteCommand(sql_insert62);
                m_accessHelper.ExcueteCommand(sql_insert63);
                m_accessHelper.ExcueteCommand(sql_insert64);
                m_accessHelper.ExcueteCommand(sql_insert65);
                m_accessHelper.ExcueteCommand(sql_insert66);
                m_accessHelper.ExcueteCommand(sql_insert67);
                m_accessHelper.ExcueteCommand(sql_insert68);
                m_accessHelper.ExcueteCommand(sql_insert69);
                m_accessHelper.ExcueteCommand(sql_insert70);
                m_accessHelper.ExcueteCommand(sql_insert71);
                m_accessHelper.ExcueteCommand(sql_insert72);
                m_accessHelper.ExcueteCommand(sql_insert73);
            }
            #endregion

            #region 融通重复数据处理

            string sql_update_rt = "update t_dwxx set dwbs='332982661931412235' where dwdm='011004006010'";
            string sql_update_rt1 = "update t_dwxx set dwbs='332988766532112235' where dwdm='011001002011'";
            string sql_update_rt2 = "update t_lctc set dwbm='332982661931412235' where dwdm='011004006010'";
            string sql_update_rt3 = "update t_lctc set dwbm='332988766532112235' where dwdm='011001002011'";

            m_accessHelper.ExcueteCommand(sql_update_rt);
            m_accessHelper.ExcueteCommand(sql_update_rt1);
            m_accessHelper.ExcueteCommand(sql_update_rt2);
            m_accessHelper.ExcueteCommand(sql_update_rt3);
            #endregion

            MessageBox.Show("基础数据预置成功！！");
        }

        private void 部队留存名贵特产类物品汇总统计表ToolStripMenuItem_Click(object sender, EventArgs e)
        {
            for (int i = 0; i < this.MdiChildren.Count(); i++)
            {
                this.MdiChildren[i].Close();
            }
            WPHZTJB wphztjb = new WPHZTJB();
            wphztjb.MdiParent = this;
            wphztjb.Show();
        }

        private void 部队留存名贵特产类物品明细统计表ToolStripMenuItem_Click(object sender, EventArgs e)
        {
            for (int i = 0; i < this.MdiChildren.Count(); i++)
            {
                this.MdiChildren[i].Close();
            }
            WPMXTJB wphztjb = new WPMXTJB();
            wphztjb.MdiParent = this;
            wphztjb.Show();
        }

        private void 向军队资产管理公司移交名贵特产类物品进度表ToolStripMenuItem_Click(object sender, EventArgs e)
        {
            for (int i = 0; i < this.MdiChildren.Count(); i++)
            {
                this.MdiChildren[i].Close();
            }
            YJWPJDB yjwpjdb = new YJWPJDB();
            yjwpjdb.MdiParent = this;
            yjwpjdb.Show();
        }

        private void 融通数据ToolStripMenuItem_Click(object sender, EventArgs e)
        {
            dw0rlctc = "lctc";
            for (int i = 0; i < this.MdiChildren.Count(); i++)
            {
                this.MdiChildren[i].Close();
            }
            RTSJDR rtsjdr = new RTSJDR();
            rtsjdr.MdiParent = this;
            rtsjdr.Text="接收融通物品移交数量";
            rtsjdr.Show();
        }

        private void 接收融通单位数据ToolStripMenuItem_Click(object sender, EventArgs e)
        {
            dw0rlctc = "dwxx";
            for (int i = 0; i < this.MdiChildren.Count(); i++)
            {
                this.MdiChildren[i].Close();
            }
            RTSJDR rtsjdr = new RTSJDR();
            rtsjdr.MdiParent = this;
            rtsjdr.Text = "接收融通单位移交数量";
            rtsjdr.Show();
        }

        private void 导入融通接收物品数据ToolStripMenuItem_Click(object sender, EventArgs e)
        {
            dw0rlctc = "DR";
            for (int i = 0; i < this.MdiChildren.Count(); i++)
            {
                this.MdiChildren[i].Close();
            }
            RTSJDR rtsjdr = new RTSJDR();
            rtsjdr.MdiParent = this;
            rtsjdr.Text = "导入融通接收物品数据";
            rtsjdr.Show();
        }

        private void 同步单位实际交接数量暂时不用ToolStripMenuItem_Click(object sender, EventArgs e)
        {
            AccessHelper m_accessHelper = new AccessHelper();
            string sql = "select sum(jjqrsl) as zjjsl ,dwbm from t_lctc group by dwbm";
            DataTable dt = m_accessHelper.getDataSet(sql).Tables[0];
            if (dt.Rows.Count > 0)
            {
                for (int i = 0; i < dt.Rows.Count; i++)
                {
                    if (string.IsNullOrEmpty(dt.Rows[i]["zjjsl"].ToString()))
                    {
                        dt.Rows[i]["zjjsl"]= "0";
                    }
                    string sql_update = string.Format("update t_dwxx set jjqrsl='{0}' where dwbs='{1}'", dt.Rows[i]["zjjsl"].ToString(), dt.Rows[i]["dwbm"].ToString());
                    m_accessHelper.ExcueteCommand(sql_update);
                }
                MessageBox.Show("更改成功");
            }
        }
    }
}
