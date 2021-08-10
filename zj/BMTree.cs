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
    public partial class BMTree : Form
    {
        private AccessHelper m_accessHelper = new AccessHelper();
        public static string tree_bmbs = "";
        public static string tree_bmdm = "";
        public static string tree_bmmc = "";
        public BMTree()
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

        /// <summary>
        /// 页面加载方法
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void FrmBcardBM_Load(object sender, EventArgs e)
        {
            GetAllDataRefreshGridView();
            //AutoSizeColumn(dataGridView);
        }

        /// <summary>
        /// 获取明细信息
        /// </summary>
        public void GetAllDataRefreshGridView()
        {
            BMTree.tree_bmbs = "";
            string dmwd_tree = "";
            if (FrmBCardDRMX.BMDR == "1")
            {
                 dmwd_tree = "000";
            }
            else
            {
                 dmwd_tree = SelcetFrmWP.dwdm_tree;
            }
            //string dmwd_tree = SelcetFrmWP.dwdm_tree;
            string tempSQL = "SELECT  t_bm.ID as ID号,t_bm.bmdm ,t_bm.bmmc , t_bm.bz as 备注,t_bm.bmbs FROM  t_bm WHERE dwdm='" + dmwd_tree + "'  ORDER BY  t_bm.bmdm,t_bm.bmbs, t_bm.ID";

            DataSet ds = m_accessHelper.getDataSet(tempSQL);
            for (int i = 0; i < ds.Tables[0].Rows.Count; i++)
            {
                string bmbdf = ds.Tables[0].Rows[i]["bmdm"].ToString();
                if (ds.Tables[0].Rows[i]["bmdm"].ToString().Length > 3)
                {
                    bmbdf = bmbdf.Substring(0, 3);
                    string tempFSQL = "SELECT  t_bm.bmdm ,t_bm.bmmc ,t_bm.bmbs FROM  t_bm WHERE dwdm='"+ dmwd_tree + "'  and  bmdm='" + bmbdf + "'  ORDER BY  t_bm.bmdm,t_bm.bmbs, t_bm.ID";

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
        private void treeView1_AfterSelect(object sender, TreeViewEventArgs e)
        {
            if (treeView1.SelectedNode != null)
            {
                tree_bmbs = treeView1.SelectedNode.Name.ToString();
                tree_bmdm = treeView1.SelectedNode.ImageIndex.ToString();
                tree_bmmc = treeView1.SelectedNode.ImageKey.ToString();
            }
        }

        private void button1_Click(object sender, EventArgs e)
        {
            if (treeView1.SelectedNode != null)
            {
                tree_bmbs = treeView1.SelectedNode.Name.ToString();
                tree_bmdm = treeView1.SelectedNode.ImageIndex.ToString();
                tree_bmmc = treeView1.SelectedNode.ImageKey.ToString();
                if (tree_bmdm.ToString().Length == 3 && treeView1.SelectedNode.LastNode != null && FrmBCardDRMX.BMDR == "1")
                {
                    MessageBox.Show("请选择最末级部门!");
                    tree_bmbs = "";
                    tree_bmdm = "";
                    tree_bmmc = "";
                    return;
                }

            }
            GetSQL();
            this.Hide();
        }
        public string GetSQL()
        {
            return tree_bmbs;
        }

        private void button2_Click(object sender, EventArgs e)
        {
            tree_bmbs = "";
            this.Close();
        }

        private void treeView1_DoubleClick(object sender, EventArgs e)
        {
            if (treeView1.SelectedNode != null)
            {
                tree_bmbs = treeView1.SelectedNode.Name.ToString();
                tree_bmdm = treeView1.SelectedNode.ImageIndex.ToString();
                tree_bmmc = treeView1.SelectedNode.ImageKey.ToString();
                if (tree_bmdm.ToString().Length == 3 && treeView1.SelectedNode.LastNode != null)
                {
                    MessageBox.Show("请选择最末级部门!");
                    tree_bmbs = "";
                    tree_bmdm = "";
                    tree_bmmc = "";
                    return;
                }

            }
            GetSQL();
            this.Hide();
        }
    }
}
