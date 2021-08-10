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
    public partial class FrmBcardBM : Form
    {
        /// <summary>
        /// 条件
        /// </summary>
        public string selectId = "";
        public string zjdbs = "";
        public string filterStr = "";
        int m_id, dwbs;
        string m_bmbs;
        string m_dwdm, m_bmdm, m_bmmc, m_bz;
        private AccessHelper m_accessHelper = new AccessHelper();
        private Boolean notsaved = false; //标识数据是否修改，默认没有修改

        private string SQL_Admin_Insert = "insert into t_bm (dwdm,bmdm,bmmc,bmbs,bz,bmfjdm)values(@DWDM, @BMDM, @BMMC, @BMBS, @BZ,@FJDM)";

        private string SQL_Admin_Update = "UPDATE  t_bm SET dwdm=@DWDM,bmdm=@BMDM,bmmc=@BMMC,bz= @BZ,bmfjdm=@FJDM WHERE bmbs=@BMBS";


        public FrmBcardBM()
        {

            InitializeComponent();
            treeView1.FullRowSelect = true;
            treeView1.Indent = 20;
            treeView1.ItemHeight = 20;
            treeView1.LabelEdit = false;
            treeView1.Scrollable = true;
            treeView1.ShowPlusMinus = true;
            treeView1.ShowRootLines = true;
            Load += new EventHandler(FrmBcardBM_Load);


        }

        private void button1_Click(object sender, EventArgs e)
        {
            //if (this.notsaved == true)//未保存的情况下
            //{
            //    DialogResult dr = MessageBox.Show("数据已经修改，是否先保存数据？", "系统提示", MessageBoxButtons.OKCancel, MessageBoxIcon.Question);
            //    if (dr == DialogResult.OK)//执行保存
            //    {
            //        this.btnSave_Click(sender, e);
            //    }
            //    else//执行新增加数据
            //    {

            //        ClearControlData();
            //        ClearVariableData();
            //        setnotsaved();
            //    }
            //}
            //else
            //{
            int num = 0;
            if (treeView1.SelectedNode != null)
            {
                if (treeView1.SelectedNode.ImageIndex.ToString().Length > 3)
                {
                    string bmdmz = treeView1.SelectedNode.ImageIndex.ToString().Substring(0, 3);
                    string sql_maxdmz = "select max(bmdm) as bmdmbh from t_bm where bmdm like '" + bmdmz + "%' ";
                    DataTable dtz = m_accessHelper.getDataSet(sql_maxdmz).Tables[0];
                    if (dtz.Rows.Count > 0)
                    {
                        if (!string.IsNullOrEmpty(dtz.Rows[0]["bmdmbh"].ToString()))
                        {
                            num = Convert.ToInt32(dtz.Rows[0]["bmdmbh"].ToString()) + 1;
                        }
                        else
                        {
                            num = 111;
                        }
                    }
                }
                else
                {
                    string sql_maxdm = "select max(bmdm) as bmdmbh from t_bm where len(bmdm)=3 and dwdm='000' ";
                    DataTable dt = m_accessHelper.getDataSet(sql_maxdm).Tables[0];

                    if (dt.Rows.Count > 0)
                    {
                        if (!string.IsNullOrEmpty(dt.Rows[0]["bmdmbh"].ToString()))
                        {
                            num = Convert.ToInt32(dt.Rows[0]["bmdmbh"].ToString()) + 1;
                        }
                        else
                        {
                            num = 111;
                        }
                    }
                    else
                    {
                        num = 111;

                    }
                }
            }
            //}
            else
            {
                //string sql_maxdm = "select  bmdm   from t_bm ";
                //DataTable dt = m_accessHelper.getDataSet(sql_maxdm).Tables[0];

                //if (dt.Rows.Count == 0)
                //{
                num = 111;
                //}
                //else
                //{
                //    MessageBox.Show("请选择部门");
                //    return;
                //}

            }
            ClearControlData();
            ClearVariableData();
            setnotsaved();


            tbBMDM.Text = num.ToString();
        }

        public void setnotsaved()
        {
            this.notsaved = true;
        }


        /// <summary>
        /// 保存前获取数据
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btnSave_Click(object sender, EventArgs e)
        {
            m_bmdm = tbBMDM.Text;
            m_bmmc = tbBMMC.Text;
            m_bz = tbBZ.Text.Replace(",", "");
            m_dwdm = "000";
            m_bmbs = Guid.NewGuid().ToString();

            if (m_id != ClassConstants.JD_NOTSELECTED)
            {
                OleDbParameter[] parms = new OleDbParameter[] {
                    new OleDbParameter("@DWDM",OleDbType.VarChar),
                    new OleDbParameter("@BMDM",OleDbType.VarChar),
                    new OleDbParameter("@BMMC",OleDbType.VarChar),
                    new OleDbParameter("@BZ",OleDbType.VarChar),
                };
                parms[0].Value = m_dwdm;
                parms[1].Value = m_bmdm;
                parms[2].Value = m_bmmc;
                parms[3].Value = m_bz;
                UpdateData(parms);
                GetAllDataRefreshGridView();
                MessageBox.Show("更新数据完毕！", "系统提示", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
            ClearControlData();
            ClearVariableData();
            setsaved();
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

        /// <summary>
        /// 页面加载方法
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void FrmBcardBM_Load(object sender, EventArgs e)
        {
            LoadComboParams();
            ClearVariableData();
            ClearControlData();
            GetAllDataRefreshGridView();
            //AutoSizeColumn(dataGridView);
        }

        private void button3_Click(object sender, EventArgs e)
        {
            string bmdmSet = "";
            string bmbsSet = "";
            if (treeView1.SelectedNode != null)
            {
                bmdmSet = treeView1.SelectedNode.ImageIndex.ToString();
                bmbsSet = treeView1.SelectedNode.Name.ToString();
            }


            m_bz = tbBZ.Text.Replace(",", "");
            m_bmmc = tbBMMC.Text.Replace(",", "");
            m_bmdm = tbBMDM.Text.Replace(",", "");
            string fjdm = "";
            if (!string.IsNullOrEmpty(m_bmdm))
            {
                fjdm = m_bmdm.ToString().Substring(0, 3);
            }
            m_bmbs = Guid.NewGuid().ToString();
            m_dwdm = "000";
            if (m_bmmc == "" || m_bmdm == "")
            {
                MessageBox.Show("部门信息不完整！", "系统提示", MessageBoxButtons.OK, MessageBoxIcon.Information);
                return;
            }
            if (bmdmSet == m_bmdm)
            {
                string sql_update = "UPDATE  t_bm SET dwdm='" + m_dwdm + "',bmdm='" + m_bmdm + "',bmmc='" + m_bmmc + "',bz= '" + m_bz + "',bmfjdm='" + fjdm + "' WHERE bmbs='" + bmbsSet + "'";
                m_accessHelper.ExcueteCommand(sql_update);
                MessageBox.Show("更新数据完毕！", "系统提示", MessageBoxButtons.OK, MessageBoxIcon.Information);
                treeView1.Nodes.Clear();
                GetAllDataRefreshGridView();
            }
            else
            {
                OleDbParameter[] parms = new OleDbParameter[] {
                    new OleDbParameter("@DWDM",OleDbType.VarChar),
                    new OleDbParameter("@BMDM",OleDbType.VarChar),
                    new OleDbParameter("@BMMC",OleDbType.VarChar),
                    new OleDbParameter("@BMBS",OleDbType.VarChar),
                    new OleDbParameter("@BZ",OleDbType.VarChar),
                    new OleDbParameter("@FJDM",OleDbType.VarChar),
                };
                parms[0].Value = m_dwdm;
                parms[1].Value = m_bmdm;
                parms[2].Value = m_bmmc;
                parms[3].Value = m_bmbs;
                parms[4].Value = m_bz;
                parms[5].Value = fjdm;

                InsertData(parms);
                MessageBox.Show("插入数据完毕！", "系统提示", MessageBoxButtons.OK, MessageBoxIcon.Information);
                treeView1.Nodes.Clear();
                GetAllDataRefreshGridView();
                //if (m_bmdm.Length > 3)
                //{
                //    string a = treeView1.SelectedNode.Name;
                //    TreeNode tn = new TreeNode(a);
                //    //每一个节点都可以看做是一个节点集合 也可以无限的向下添加子节点
                //    tn.Nodes.Add(m_bmbs.ToString(), m_bmdm.ToString() + m_bmmc.ToString(), m_bmdm);
                //}
                //else
                //{
                //    TreeNode newNode1 = treeView1.Nodes.Add(m_bmbs.ToString(), m_bmdm.ToString() + m_bmmc.ToString(), m_bmdm);
                //}
            }
            //if (m_bmdm.Length > 3)
            //{
            //    string bmdm = m_bmdm.Substring(0, 3);
            //    string sql_fjbs = "select bmbs from t_bm where bmdm='" + bmdm + "' and dwdm='000'";
            //    DataTable dt = m_accessHelper.getDataSet(sql_fjbs).Tables[0];
            //    if (dt.Rows.Count > 0)
            //    {
            //        string bmbs = dt.Rows[0]["bmbs"].ToString();
            //        TreeNode newNode1 = treeView1.Nodes.Find(bmbs, false)[0];
            //        TreeNode newNode2 = newNode1.Nodes.Find(m_bmbs, false)[0];
            //        treeView1.SelectedNode = newNode2;
            //    }

            //}
            //else
            //{
            //    TreeNode newNode1 = treeView1.Nodes.Find(m_bmbs, false)[0];
            //    treeView1.SelectedNode = newNode1;
            //}
            //    ClearControlData();
            ClearVariableData();
            setsaved();
            //    this.tree.VisualComponent.FindNodeByID(index).Selected = true;//刷新树后将焦点还原为刷新树之前的

        }

        /// <summary>
        /// 新增
        /// </summary>
        /// <param name="parms"></param>
        public void InsertData(OleDbParameter[] parms)
        {
            m_accessHelper.ExcueteCommand(SQL_Admin_Insert, parms);
        }

        private void btnDelete_Click(object sender, EventArgs e)
        {
            if (treeView1.SelectedNode == null)
            {
                MessageBox.Show("请先选择部门！", "系统提示", MessageBoxButtons.OK, MessageBoxIcon.Information);
                return;
            }
            string bmbs = treeView1.SelectedNode.Name.ToString();
            string bmdm = treeView1.SelectedNode.ImageIndex.ToString();
            MessageBoxButtons msgBut = MessageBoxButtons.OKCancel;
            DialogResult dr = MessageBox.Show("此操作将会删除子节点部门以及物品明细内此部门的数据，是否继续？", "删除数据", msgBut, MessageBoxIcon.Question);
            if (dr == DialogResult.OK)
            {

                DeleteData(bmbs, bmdm);
                ClearControlData();
                ClearVariableData();
                treeView1.Nodes.Clear();
                GetAllDataRefreshGridView();
                if (treeView1.Nodes.Count > 0)
                {
                    treeView1.SelectedNode = treeView1.Nodes[0];
                }
            }
        }

        /// <summary>
        /// 删除
        /// </summary>
        /// <param name="dataIndex"></param>
        public void DeleteData(string dataIndex, string bmdm)
        {
            string select_dwdm = "select bmbs from t_bm where dwdm ='000' and bmdm like '" + bmdm + "%'";
            DataTable dt = m_accessHelper.getDataSet(select_dwdm).Tables[0];
            if (dt.Rows.Count > 0)
            {
                for (int i = 0; i < dt.Rows.Count; i++)
                {
                    string sql_lctc_delete = "delete from t_lctc where bmbs ='" + dt.Rows[i]["bmbs"] + "'";
                    m_accessHelper.ExcueteCommand(sql_lctc_delete);
                }
            }



            //string sql_lctc_delete = "delete from t_lctc where bmbs in(select bmbs from t_bm where dwdm ='000' and bmdm like '" + bmdm + "*')";
            //m_accessHelper.ExcueteCommand(sql_lctc_delete);

            OleDbParameter[] parms = new OleDbParameter[] {
                new OleDbParameter("@BMBS",OleDbType.VarChar)
            };
            parms[0].Value = dataIndex;
            string SQL_Admin_Delete = "DELETE FROM t_bm WHERE dwdm='000' and bmdm like '" + bmdm + "%'";
            m_accessHelper.ExcueteCommand(SQL_Admin_Delete);

        }

        private void button4_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void dataGridView_SystemColorsChanged(object sender, EventArgs e)
        {

        }

        public void LoadVariablesToControls()
        {
            //  comboDWDM.SelectedValue = m_dwdm;
            tbBMDM.Text = m_bmdm.ToString();
            tbBMMC.Text = m_bmmc.ToString();
            tbBZ.Text = m_bz;
        }

        /// <summary>
        /// 赋值
        /// </summary>
        public void ReadDataToVariables()
        {
            //  m_id = int.Parse(dataGridView.SelectedRows[0].Cells[1].Value.ToString());
            DataTable dt = this.GetDataByIndex(m_id);
            if (dt.Rows.Count > 0)
            {
                m_bmdm = dt.Rows[0]["bmdm"].ToString();
                m_bmmc = dt.Rows[0]["bmmc"].ToString();
                m_bz = dt.Rows[0]["bz"].ToString();
                m_dwdm = dt.Rows[0]["dwdm"].ToString();
            }
        }

        /// <summary>
        /// 根据id获取一条往来款数据
        /// </summary>
        /// <param name="dataIndex"></param>
        /// <returns></returns>
        public DataTable GetDataByIndex(int dataIndex)
        {
            string tempSQL = "SELECT * FROM t_bm WHERE ID=" + dataIndex;
            DataTable dt = m_accessHelper.getDataSet(tempSQL).Tables[0];
            return dt;
        }

        private void dataGridView_SelectionChanged(object sender, EventArgs e)
        {
            //if (dataGridView.SelectedRows.Count != 0 && dataGridView.SelectedRows[0].Cells[1].Value.ToString() != "")
            //{
            //    //this.checkchagnge();
            //    ReadDataToVariables();
            //    LoadVariablesToControls();
            //}
        }

        private void label27_Click(object sender, EventArgs e)
        {

        }

        /// <summary>
        /// 单位赋值
        /// </summary>
        private void LoadComboParams()
        {
            string tempSQL = "SELECT dwdm,dwmc FROM t_dwxx where dwdm='000'";
            DataSet ds = m_accessHelper.getDataSet(tempSQL);


            if (ds.Tables[0].Rows.Count == 0)
            {
                MessageBox.Show("请先录入单位信息！", "系统提示", MessageBoxButtons.OK, MessageBoxIcon.Information);
                dwbs = 1;
                Application.Exit();
            }
            else
            {
                dwbs = 0;
                //comboDWDM.ValueMember = "dwdm";
                //comboDWDM.DisplayMember = "dwmc";
                //label4.Text = ds.Tables[0].Rows[0]["dwmc"].ToString() + "单位本级机关部门信息维护";
            }

        }

        private void treeView1_AfterSelect(object sender, TreeViewEventArgs e)
        {
            string bmbsS = treeView1.SelectedNode.Name.ToString();
            if (bmbsS != "")
            {
                string tempSQL = "SELECT * FROM t_bm WHERE bmbs='" + bmbsS + "'";
                DataTable dt = m_accessHelper.getDataSet(tempSQL).Tables[0];
                if (dt.Rows.Count > 0)
                {
                    tbBMDM.Text = dt.Rows[0]["bmdm"].ToString();
                    tbBMMC.Text = dt.Rows[0]["bmmc"].ToString();
                    tbBZ.Text = dt.Rows[0]["bz"].ToString();
                }
            }
        }

        private void button2_Click(object sender, EventArgs e)
        {
            if (treeView1.SelectedNode == null)
            {
                return;
            }
            if (treeView1.SelectedNode.ImageIndex.ToString().Length == 6)
            {
                MessageBox.Show("部门最多只可以录入两级！", "系统提示", MessageBoxButtons.OK, MessageBoxIcon.Information);
                return;
            }
            if (treeView1.SelectedNode == null)
            {
                return;
            }
            else if (treeView1.SelectedNode.LastNode != null)
            {
                string bmbm = "";
                int num = Int32.Parse(treeView1.SelectedNode.LastNode.ImageIndex.ToString().Substring(3, 3));
                num = num + 1;
                if (num.ToString().Length < 10)
                {
                    bmbm = treeView1.SelectedNode.ImageIndex + "00" + num;
                }
                else if (num.ToString().Length < 100)
                {
                    bmbm = treeView1.SelectedNode.ImageIndex + "0" + num;
                }
                else
                {
                    bmbm = treeView1.SelectedNode.ImageIndex + num.ToString();
                }

                tbBMDM.Text = bmbm;
            }
            else
            {
                tbBMDM.Text = treeView1.SelectedNode.ImageIndex + "001";
            }
            tbBMMC.Text = "";
            tbBZ.Text = "";
            zjdbs = "1";
        }

        /// <summary>
        /// 清理变量值
        /// </summary>
        private void ClearVariableData()
        {
            m_bmdm = "";
            m_bz = "";
            m_bmmc = "";
            m_id = ClassConstants.JD_NOTSELECTED;
        }

        /// <summary>
        /// 清理控件值
        /// </summary>
        private void ClearControlData()
        {

            tbBMDM.Text = "";
            tbBMMC.Text = "";
            tbBZ.Text = "";
        }

        /// <summary>
        /// 获取明细信息
        /// </summary>
        public void GetAllDataRefreshGridView()
        {

            string tempSQL = "SELECT  t_bm.ID as ID号,t_bm.bmdm ,t_bm.bmmc , t_bm.bz as 备注,t_bm.bmbs FROM  t_bm WHERE dwdm='000'  ORDER BY  t_bm.bmdm, t_bm.ID";

            DataSet ds = m_accessHelper.getDataSet(tempSQL);
            for (int i = 0; i < ds.Tables[0].Rows.Count; i++)
            {
                string bmbdf = ds.Tables[0].Rows[i]["bmdm"].ToString();
                if (ds.Tables[0].Rows[i]["bmdm"].ToString().Length > 3)
                {
                    bmbdf = bmbdf.Substring(0, 3);
                    string tempFSQL = "SELECT  t_bm.bmdm ,t_bm.bmmc ,t_bm.bmbs FROM  t_bm WHERE dwdm='000' and bmdm='" + bmbdf + "'  ORDER BY  t_bm.bmdm, t_bm.ID";

                    DataTable dtf = m_accessHelper.getDataSet(tempFSQL).Tables[0];
                    if (dtf.Rows.Count > 0)
                    {
                        string a = dtf.Rows[0]["bmbs"].ToString();
                        TreeNode newNode1 = treeView1.Nodes.Find(a, false)[0];

                        //每一个节点都可以看做是一个节点集合 也可以无限的向下添加子节点
                        newNode1.Nodes.Add(ds.Tables[0].Rows[i]["bmbs"].ToString(), ds.Tables[0].Rows[i]["bmdm"].ToString() + ds.Tables[0].Rows[i]["bmmc"].ToString(), Convert.ToInt32(ds.Tables[0].Rows[i]["bmdm"].ToString()));
                    }
                }
                else
                {
                    TreeNode newNode1 = treeView1.Nodes.Add(ds.Tables[0].Rows[i]["bmbs"].ToString(), ds.Tables[0].Rows[i]["bmdm"].ToString() + ds.Tables[0].Rows[i]["bmmc"].ToString(), Convert.ToInt32(ds.Tables[0].Rows[i]["bmdm"].ToString()));
                    TreeNode newNode2 = treeView1.Nodes.Find(ds.Tables[0].Rows[i]["bmbs"].ToString(), false)[0];
                }

            }
            treeView1.ExpandAll();//默认树展开
        }
        //for (int i = 0; i < ds.Tables[0].Rows.Count; i++)
        //{

        //    if (ds.Tables[0].Rows[i]["bmdm"].ToString().Length > 3)
        //    {
        //        string a = ds.Tables[0].Rows[i]["bmbs"].ToString();
        //        if (treeView1.Nodes.Find(a, false).Count > 0)
        //        {
        //            TreeNode newNode1 =
        //        }

        //        //每一个节点都可以看做是一个节点集合 也可以无限的向下添加子节点
        //        newNode1.Nodes.Add(ds.Tables[0].Rows[i]["bmbs"].ToString(), ds.Tables[0].Rows[i]["bmdm"].ToString() + ds.Tables[0].Rows[i]["bmmc"].ToString(), Convert.ToInt32(ds.Tables[0].Rows[i]["bmdm"].ToString()));
        //    }
        //}
        //DataTable dt = ds.Tables[0];
        //DataColumn dc = dt.Columns.Add("序号", typeof(int));
        //dt.Columns["序号"].SetOrdinal(0);
        //for (int i = 0; i < dt.Rows.Count; i++)
        //{
        //    dt.Rows[i][0] = i + 1;
        //}
        //dataGridView.DataSource = dt;
        //dataGridView.Columns[1].Visible = false;
        //dataGridView.ClearSelection();
        //dataGridView.Columns["ID号"].Width = 60;
        ////dataGridView.Columns["单位名称"].Width = 200;
        ////dataGridView.Columns["备注"].Width = 320;
        //if (dataGridView.SelectedRows.Count != 0)
        //{
        //    m_id = int.Parse(dataGridView.SelectedRows[0].Cells[1].Value.ToString());
        //}
        //else
        //{
        //    m_id = ClassConstants.JD_NOTSELECTED;
        //}
        //}

        /// <summary>
        /// 使DataGridView的列自适应宽度
        /// </summary>
        /// <param name="dgViewFiles"></param>
        private void AutoSizeColumn(DataGridView dgViewFiles)
        {
            int width = 0;
            //使列自使用宽度
            //对于DataGridView的每一个列都调整
            for (int i = 0; i < dgViewFiles.Columns.Count; i++)
            {
                //将每一列都调整为自动适应模式
                dgViewFiles.AutoResizeColumn(i, DataGridViewAutoSizeColumnMode.AllCells);
                //记录整个DataGridView的宽度
                width += dgViewFiles.Columns[i].Width;
            }
            //判断调整后的宽度与原来设定的宽度的关系，如果是调整后的宽度大于原来设定的宽度，
            //则将DataGridView的列自动调整模式设置为显示的列即可，
            //如果是小于原来设定的宽度，将模式改为填充。
            if (width > dgViewFiles.Size.Width)
            {
                dgViewFiles.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.DisplayedCells;
            }
            else
            {
                dgViewFiles.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill;
            }
            //冻结某列 从左开始 0，1，2
            dgViewFiles.Columns[1].Frozen = true;
        }


    }
}
