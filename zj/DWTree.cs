using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace zj
{
    public partial class DWTree : Form
    {
        private AccessHelper m_accessHelper = new AccessHelper();
        public string dwTreeDm = string.Empty;
        public string dwTreeName = string.Empty;
        public DWTree()
        {
            InitializeComponent();
            treeView1.FullRowSelect = true;
            treeView1.Indent = 20;
            treeView1.ItemHeight = 20;
            treeView1.LabelEdit = false;
            treeView1.Scrollable = true;
            treeView1.ShowPlusMinus = true;
            treeView1.ShowRootLines = true;
            Load += new EventHandler(FrmBcardDW_Load);
        }

        /// <summary>
        /// 页面加载方法
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void FrmBcardDW_Load(object sender, EventArgs e)
        {
            GetAllDataRefreshGridView();
            //AutoSizeColumn(dataGridView);
        }

        /// <summary>
        /// 获取明细信息
        /// </summary>
        public void GetAllDataRefreshGridView()
        {
            string tempSQL = "SELECT  dwdm,dwmc,id FROM  t_dwxx   ORDER BY  t_dwxx.dwdm, t_dwxx.ID";
            DataSet ds = m_accessHelper.getDataSet(tempSQL);
            for (int i = 0; i < ds.Tables[0].Rows.Count; i++)
            {
                string dwdm = ds.Tables[0].Rows[i]["dwdm"].ToString();
                //int length = 0;
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
                    else if(dwdm.Length == 6)//三级
                    {
                        TreeNode newNodeA= treeView1.Nodes.Find("000", false)[0];
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
          // treeView1.ExpandAll();//默认树展开
        }
        private void button1_Click(object sender, EventArgs e)
        {
            if (treeView1.SelectedNode != null)
            {
                dwTreeName = treeView1.SelectedNode.Text.ToString();
                dwTreeDm = treeView1.SelectedNode.Name.ToString();
                GetDWdm();
                GetDWMC();
            }
            this.Hide();
        }
        public string GetDWdm()
        {
            return dwTreeDm;
        }

        public string GetDWMC()
        {
            return dwTreeName;
        }

        private void button2_Click(object sender, EventArgs e)
        {
            dwTreeDm = "";
            dwTreeName = "";
            this.Close();
        }

        private void treeView1_AfterSelect(object sender, TreeViewEventArgs e)
        {
            if (treeView1.SelectedNode != null)
            {
                dwTreeName = treeView1.SelectedNode.Text.ToString();
                dwTreeDm = treeView1.SelectedNode.Name.ToString();
            }
        }

        private void treeView1_DoubleClick(object sender, EventArgs e)
        {
            if (treeView1.SelectedNode != null)
            {
                dwTreeName = treeView1.SelectedNode.Text.ToString();
                dwTreeDm = treeView1.SelectedNode.Name.ToString();
                GetDWdm();
                GetDWMC();
            }
            this.Hide();
        }
    }
}
