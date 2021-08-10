using Microsoft.Office.Interop.Access.Dao;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.OleDb;
using System.IO;
using System.Windows.Forms;
namespace zj
{
    public partial class FrmBCardDRMX : Form
    {
        public static string BMDR = "";
        public bool isOk = false;
        private string SQL_Admin_Insert = "insert into t_lctc(dwdm,bmbs,lb,pm,ly,hqsj,sl,jldw,dj,zz,kysl,kbxjz,czfs,bz,djlx,wpbs)values(@DWDM, @BMBS, @LB, @PM, @LY, @HQSJ, @SL, @JLDW, @DJ,@ZZ,@KYSL,@KBXJZ,@CZFS,@BZ,@DJLX,@WPBS)";
        private AccessHelper m_accessHelper = new AccessHelper();
        public FrmBCardDRMX()
        {
            InitializeComponent();
        }
        private void button1_Click(object sender, EventArgs e)
        {
            if (string.IsNullOrEmpty(textBox1.Text))
            {
                MessageBox.Show("请先选择导入的部门！", "系统提示", MessageBoxButtons.OK, MessageBoxIcon.Information);
                return;
            }

            string path = textBox2.Text;
            if (string.IsNullOrEmpty(path))
            {
                MessageBox.Show("请选择要导入的文件", "请检查！", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }
            List<TCLCData> list = new List<TCLCData>();
            if (!string.IsNullOrEmpty(path))
            {
                string bmbs = BMTree.tree_bmbs;
                string sql_isdata = "select count(*) from t_lctc where bmbs='" + bmbs + "'";
                DataTable dt_lctc = m_accessHelper.getDataSet(sql_isdata).Tables[0];
                int num = 0;
                if (dt_lctc.Rows.Count > 0)
                {
                    num = Convert.ToInt32(dt_lctc.Rows[0][0].ToString());
                }
                MessageBoxButtons msgBut = MessageBoxButtons.OKCancel;
                if (num > 0)
                {
                    DialogResult dr = MessageBox.Show("导入后将覆盖该部门已录入数据，确定要导入吗？", "数据导入提示", msgBut, MessageBoxIcon.Question);
                    if (dr != DialogResult.OK)
                    {
                        return;
                    }
                }

                System.Data.DataTable dt = new System.Data.DataTable();
                try
                {
                    dt = AccessHelper.ExcelToDataTable(path, "留存名贵特产类物品明细");
                }
                catch
                {
                    MessageBox.Show("请选择系统所提供的导入模板", "请检查！", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    return;
                }
                if (dt.Rows.Count > 0)
                {
                    string ycjl = string.Empty;
                    string drbu = dt.Rows[0]["留存名贵特产类物品明细统计表（本级部门）"].ToString();

                    int wz = drbu.LastIndexOf("：");
                    drbu = drbu.Substring(wz, drbu.Length - wz);
                    DialogResult drt = MessageBox.Show("正在导入" + drbu + "的数据，是否继续?", "数据导入提示", msgBut, MessageBoxIcon.Question);
                    if (drt == DialogResult.OK)
                    {
                        for (int i = 2; i < dt.Rows.Count; i++)
                        {
                            if (string.IsNullOrEmpty(dt.Rows[i]["F6"].ToString()))
                            {
                                dt.Rows[i]["F6"] = "0";
                                ycjl+="第" + (i-1) + "行物品数量为0，不能导入;\r\n";
                            }
                            if (string.IsNullOrEmpty(dt.Rows[i]["F9"].ToString()))
                            {
                                dt.Rows[i]["F9"] = "0";
                                ycjl += "第" +(i-1) + "行物品单价为0，不能导入;\r\n";
                            }
                            if (string.IsNullOrEmpty(dt.Rows[i]["F11"].ToString()))
                            {
                                dt.Rows[i]["F11"] = "0";
                            }
                            if (string.IsNullOrEmpty(dt.Rows[i]["F12"].ToString()))
                            {
                                dt.Rows[i]["F12"] = "0";
                            }
                            if (!string.IsNullOrEmpty(dt.Rows[i]["F3"].ToString()))
                            {
                                list.Add(new TCLCData()
                                {
                                    dwdm = "000",
                                    bmbs = BMTree.tree_bmbs,
                                    lb = dt.Rows[i]["F2"].ToString(),
                                    pm = dt.Rows[i]["F3"].ToString(),
                                    ly = dt.Rows[i]["F4"].ToString(),
                                    hqsj = dt.Rows[i]["F5"].ToString(),

                                    sl = double.Parse(dt.Rows[i]["F6"].ToString()),
                                    jldw = dt.Rows[i]["F7"].ToString(),
                                    djlx = dt.Rows[i]["F8"].ToString(),
                                    dj = double.Parse(dt.Rows[i]["F9"].ToString()),
                                    zz = double.Parse(dt.Rows[i]["F6"].ToString()) * double.Parse(dt.Rows[i]["F9"].ToString()),
                                    kysl = double.Parse(dt.Rows[i]["F11"].ToString()),
                                    kbxjz = double.Parse(dt.Rows[i]["F12"].ToString()),
                                    czfs = dt.Rows[i]["F13"].ToString(),
                                    bz = dt.Rows[i]["F14"].ToString(),
                                    wpbs = Guid.NewGuid().ToString()
                                });
                            }
                        }
                        if (!string.IsNullOrEmpty(ycjl))
                        {
                            MessageBox.Show(ycjl);
                            return;
                        }
                        string SQL_Admin_Delete = "delete from t_lctc where bmbs=@bmbs ";

                        OleDbParameter[] parms1 = new OleDbParameter[] {
                new OleDbParameter("@bmbs",OleDbType.VarChar)
            };
                        parms1[0].Value = BMTree.tree_bmbs;

                        m_accessHelper.ExcueteCommand(SQL_Admin_Delete, parms1);



                        bool exresult = m_accessHelper.ExPiliang(list);

                        //string strql = "";
                        //for (int i = 0; i < list.Count; i++)
                        //{
                        //    strql += "insert into t_lctc(dwdm, bmbs, lb, pm, ly, hqsj, sl, jldw, dj, zz, kysl, kbxjz, czfs, bz, djlx, wpbs)values ";
                        //    strql += "(";
                        //    strql += "'" + list[i].dwdm + "',";
                        //    strql += "'" + list[i].bmbs + "',";
                        //    strql += "'" + list[i].lb + "',";
                        //    strql += "'" + list[i].pm + "',";
                        //    strql += "'" + list[i].ly + "',";
                        //    strql += "'" + list[i].hqsj + "',";
                        //    strql += "'" + list[i].sl + "',";
                        //    strql += "'" + list[i].jldw + "',";
                        //    strql += "'" + list[i].dj + "',";
                        //    strql += "'" + list[i].zz + "',";
                        //    strql += "'" + list[i].kysl + "',";
                        //    strql += "'" + list[i].kbxjz + "',";
                        //    strql += "'" + list[i].czfs + "',";
                        //    strql += "'" + list[i].bz + "',";
                        //    strql += "'" + list[i].djlx + "',";
                        //    strql += "'" + Guid.NewGuid().ToString() + "'";
                        //    strql += ");";
                        //}
                        ////strql = strql.Substring(0, strql.Length - 1);
                        //string SQL_Admin_Insert1 = "insert into t_lctc(dwdm,bmbs,lb,pm,ly,hqsj,sl,jldw,dj,zz,kysl,kbxjz,czfs,bz,djlx,wpbs)values " + strql + ";";
                        //m_accessHelper.ExcueteCommand(strql);
                        if (exresult)
                        {
                            MessageBox.Show("已成功导入" + list.Count + "条数据！", "系统提示", MessageBoxButtons.OK, MessageBoxIcon.Information);
                            isOk = true;
                        }
                        else
                            MessageBox.Show("导入失败");

                        this.Close();
                    }
                }
                else
                {
                    return;
                }



            }


            //SelcetFrmWP wp = new SelcetFrmWP();
            //wp.ShowDialog();
        }

        public string NULLOdata(string data)
        {
            if (string.IsNullOrEmpty(data))
            {
                return "0";
            }
            else
            {
                return data;
            }
        }

        /// <summary>
        /// 新增
        /// </summary>
        /// <param name="parms"></param>
        public void InsertData(OleDbParameter[] parms)
        {
            m_accessHelper.ExcueteCommand(SQL_Admin_Insert, parms);
        }

        public DataSet ExcelToDataSet(string filename, string tsql)
        {
            DataSet ds;
            string strCon = "ProVider = Microsoft.ACE.OLEDB.12.0; Extended Properties = Excel 8.0; Data Source = " + filename;
            OleDbConnection myConn = new OleDbConnection(strCon);
            myConn.Open();
            OleDbDataAdapter mycommand = new OleDbDataAdapter(tsql, myConn);
            ds = new DataSet();
            mycommand.Fill(ds);
            myConn.Close();
            return ds;
        }

        public string GetExcelFirstTableName(string path)
        {
            string tableanme = "";
            if (File.Exists(path))
            {
                using (OleDbConnection conn = new OleDbConnection("ProVider=Microsoft.ACE.OLEDB.12.0;Extended Properties=Excel 8.0;Data Source=" + path))
                {
                    conn.Open();
                    tableanme = conn.GetOleDbSchemaTable(OleDbSchemaGuid.Tables, null).Rows[0][2].ToString().Trim();
                }

            }
            return tableanme;
        }

        private void button2_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void textBox1_Click(object sender, EventArgs e)
        {
            BMDR = "1";
            BMTree bt = new BMTree();
            bt.ShowDialog();
            string bmbs_tree = bt.GetSQL();
            string sql_bmmc = "select bmmc from t_bm where bmbs='" + bmbs_tree + "'";
            System.Data.DataTable dt = m_accessHelper.getDataSet(sql_bmmc).Tables[0];
            if (dt.Rows.Count > 0)
            {
                textBox1.Text = dt.Rows[0]["bmmc"].ToString();

            }
        }

        private void textBox2_Click(object sender, EventArgs e)
        {
            OpenFileDialog ofd = new OpenFileDialog();
            ofd.Title = "选择文件";
            ofd.Filter = "Microsoft Excel文件|*.xls;*.xlsx";
            ofd.FilterIndex = 1;
            ofd.DefaultExt = "xls";
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
                textBox2.Text = ofd.FileName;
            }
        }
    }
}
