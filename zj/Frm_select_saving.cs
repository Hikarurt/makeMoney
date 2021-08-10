using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
//using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.Collections;

namespace zj
{
    public partial class Frm_select_saving : Form
    {
        public string m_sql;
        public List<ClassHirachy> m_nodeBuf;
        public Frm_select_saving()
        {
            InitializeComponent();
        }

        public string GetSQL()
        {
            return m_sql;
        }

        public void SetSQL(string sql)
        {
            m_sql = sql;
        }

        private void button1_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void btnExit_Click(object sender, EventArgs e)
        {
            String f_dwdm = textBoxDWDM.Text.Trim();
            String f_dwmc = textBoxDWMC.Text.Trim();
            String sql = "";

            if (f_dwdm != "")
            {
                sql = "and t_zjjc.dwdm like '"+f_dwdm+"%'";
            }
            if (f_dwmc != "")
            {
                sql = sql + " and t_dwxx.dwmc like '%" + f_dwmc + "%'";
            }
            SetSQL(sql);
        }

        private void Frm_select_account_Load(object sender, EventArgs e)
        {
            List<ClassHirachy> m_nodeBuf = new List<ClassHirachy>();

            treeView1.ShowLines = true;
            String sql_dwxx = "select id,dwdm,dwmc from t_dwxx order by dwdm asc";
            AccessHelper AccessHelper = new AccessHelper();
            DataSet ds = AccessHelper.getDataSet(sql_dwxx);

            for(int i=0;i<ds.Tables[0].Rows.Count;i++){
                ClassHirachy tempHirachy = new ClassHirachy();
                TreeNode tempNode = new TreeNode();
                tempNode.Tag = ds.Tables[0].Rows[i]["dwdm"].ToString();
                tempNode.Text = ds.Tables[0].Rows[i]["dwmc"].ToString();
                tempHirachy.SetNode(tempNode);
                int hirachy = tempNode.Tag.ToString().Length / 3;
                tempHirachy.SetHirachy(hirachy);
                m_nodeBuf.Add(tempHirachy);
            }
            foreach(ClassHirachy childNode in m_nodeBuf){
                foreach (ClassHirachy parentNode in m_nodeBuf)
                {
                    string childDWDM=childNode.GetNode().Tag.ToString();
                    string parentDWDM=parentNode.GetNode().Tag.ToString();
                    if (childDWDM.Substring(0, childDWDM.Length - 3) == parentDWDM)
                        parentNode.GetNode().Nodes.Add(childNode.GetNode());
                }
                int DWDMLength=childNode.GetNode().Tag.ToString().Length;
                if (DWDMLength == 3)
                {
                    treeView1.Nodes.Add(childNode.GetNode());
                }
            }
        }

        private void treeView1_DoubleClick(object sender, EventArgs e)
        {
            this.textBoxDWDM.Text = treeView1.SelectedNode.Tag.ToString();
        }

    }
}
