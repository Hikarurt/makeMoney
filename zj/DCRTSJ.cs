using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.Threading;
using System.Windows.Threading;
using System.IO;
using System.Reflection;

namespace zj
{
    public partial class DCRTSJ : Form
    {
        private AccessHelper m_accessHelper = new AccessHelper();
    
        public DCRTSJ()
        {
            InitializeComponent();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            folderBrowserDialog1.SelectedPath = "";
            if (folderBrowserDialog1.ShowDialog() == DialogResult.OK)
                textBox1.Text = folderBrowserDialog1.SelectedPath;
        }

        private void button2_Click(object sender, EventArgs e)
        {
            if (textBox1.Text.Length == 0)
            {
                MessageBox.Show("请选择导出文件存放路径！");
                return;
            }
            else
            {
                string sql_isadd = "select top 1 * from t_dwxx";
                DataTable dt_isadd = m_accessHelper.getDataSet_rt(sql_isadd).Tables[0];
                if (!dt_isadd.Columns.Contains("yjsheng"))
                {
                    string sql_add = "ALTER TABLE t_dwxx  ADD yjsheng VARCHAR(200)";
                    m_accessHelper.ExcueteCommand_rt(sql_add);
                }

                if (!dt_isadd.Columns.Contains("yjshi"))
                {
                    string sql_add = "ALTER TABLE t_dwxx  ADD yjshi VARCHAR(200)";
                    m_accessHelper.ExcueteCommand_rt(sql_add);
                }

                if (!dt_isadd.Columns.Contains("yjjsd"))
                {
                    string sql_add = "ALTER TABLE t_dwxx  ADD yjjsd VARCHAR(200)";
                    m_accessHelper.ExcueteCommand_rt(sql_add);
                }


                List<DWXX> list_dwxx = new List<DWXX>();
                List<LCTC> list_lctc = new List<LCTC>();

                string sql_LCTC_dw = "select dwdm,yjdd,sszd,dwbs,nxsh,nxs,nxjsd from t_dwxx where (dwbs is null or dwbs='') order by dwdm";
                DataTable dt = m_accessHelper.getDataSet(sql_LCTC_dw).Tables[0];
                if (dt.Rows.Count > 0)
                {

                    #region 数据处理，生成唯一单位标识

                    //int codeCount = dt.Rows.Count;
                    List<string> sjb = new List<string>();
                    List<string> codeList = new List<string>();
                    //Random ra9 = new Random(999999999);
                    //Random ra6 = new Random(999999);
                    //for (int i = 1; i <= codeCount; i++)
                    //{
                    //    codeList.Add(ra9.Next(100000000, 999999999).ToString() + ra6.Next(100000, 999999).ToString());
                    //}
                    for (int i = 0; i < dt.Rows.Count; i++)
                    {
                        codeList.Add(dt.Rows[i]["dwdm"].ToString());
                    }
                    int runNum = 0;
                    foreach (var item in codeList)
                    {
                        runNum++;
                        string result = GetInputCode_1(item);
                        sjb.Add(result);
                    }

                    for (int k = 0; k < dt.Rows.Count; k++)
                    {
                        if (string.IsNullOrEmpty(dt.Rows[k]["dwbs"].ToString()))
                        {
                            dt.Rows[k]["dwbs"] = sjb[k].ToString();
                            string sql_up_dwxx = "update t_dwxx set dwbs='" + dt.Rows[k]["dwbs"] + "' where dwdm='" + dt.Rows[k]["dwdm"] + "'";
                            m_accessHelper.ExcueteCommand(sql_up_dwxx);

                        }
                        string sql_up = "UPDATE t_lctc set dwbm ='" + dt.Rows[k]["dwbs"] + "' where dwdm='" + dt.Rows[k]["dwdm"] + "'";
                        m_accessHelper.ExcueteCommand(sql_up);

                        #region 组装数据
                        string sql_select_lctc = "select * from t_lctc  where czfs ='拟移交物品'   and dwbm='" + dt.Rows[k]["dwbs"] + "' and dwdm='" + dt.Rows[k]["dwdm"] + "'  order by id";
                        DataTable wpdt = m_accessHelper.getDataSet(sql_select_lctc).Tables[0];
                        if (wpdt.Rows.Count > 0)
                        {
                            for (int m = 0; m < wpdt.Rows.Count; m++)
                            {
                                list_lctc.Add(new LCTC()
                                {
                                    wpbs = wpdt.Rows[m]["wpbs"].ToString(),
                                    dwdm = wpdt.Rows[m]["dwbm"].ToString(),
                                    lb = wpdt.Rows[m]["lb"].ToString(),
                                    pm = wpdt.Rows[m]["pm"].ToString(),
                                    ly = wpdt.Rows[m]["ly"].ToString(),
                                    hqsj = wpdt.Rows[m]["hqsj"].ToString(),
                                    sl = wpdt.Rows[m]["sl"].ToString(),
                                    jldw = wpdt.Rows[m]["jldw"].ToString(),
                                    dj = wpdt.Rows[m]["dj"].ToString(),
                                    zz = wpdt.Rows[m]["zz"].ToString(),
                                    kysl = wpdt.Rows[m]["kysl"].ToString(),
                                    kbxjz = wpdt.Rows[m]["kbxjz"].ToString(),
                                    czfs = wpdt.Rows[m]["czfs"].ToString(),
                                    bz = wpdt.Rows[m]["bz"].ToString(),
                                    djlx = wpdt.Rows[m]["djlx"].ToString(),
                                    jjqrsl = wpdt.Rows[m]["jjqrsl"].ToString(),
                                    jjqrpm = wpdt.Rows[m]["jjqrpm"].ToString(),
                                    jjbz = wpdt.Rows[m]["jjbz"].ToString()
                                });
                            }
                            list_dwxx.Add(new DWXX()
                            {
                                dwdm = dt.Rows[k]["dwbs"].ToString(),
                                yjdd = string.Empty,
                                sszd = string.Empty,
                                yjsheng = dt.Rows[k]["nxsh"].ToString(),
                                yjshi = dt.Rows[k]["nxs"].ToString(),
                                yjjsd = string.Empty
                            });
                        }
                        #endregion
                    }
                   

                    #endregion


                    #region 删除融通数据

                    string sql_delete_dwxx = "delete from t_dwxx";
                    m_accessHelper.ExcueteCommand_rt(sql_delete_dwxx);

                    string sql_delete_lctc = "delete from t_lctc";
                    m_accessHelper.ExcueteCommand_rt(sql_delete_lctc);
                    #endregion

                    if (list_dwxx.Count < 1)
                    {
                        MessageBox.Show("导出数据为空，请确认！");
                        return;
                    }


                    MessageBoxButtons msgBut = MessageBoxButtons.OKCancel;
                    DialogResult dr = MessageBox.Show("确定导出单位信息吗？", "删除数据", msgBut, MessageBoxIcon.Question);
                    if (dr == DialogResult.OK)
                    {
                        DataTable dt_dwxx1 = ListToDataTable(list_dwxx);//单位信息
                        string a = string.Empty;
                        folderBrowserDialog1.SelectedPath = "";
                        if (folderBrowserDialog1.ShowDialog() == DialogResult.OK)
                            a  = folderBrowserDialog1.SelectedPath;
                        ExcelUI.OpenExcel_A4_3(dt_dwxx1, Application.StartupPath + "\\report\\A30\\导出融通数据包.xlsx", a+ "\\导出融通数据包—单位信息.xlsx", 1, 0, 15);
                        MessageBox.Show("导出成功!");
                    }

                    DialogResult dr1 = MessageBox.Show("确定导出物品明细吗？", "删除数据", msgBut, MessageBoxIcon.Question);
                    if (dr1 == DialogResult.OK)
                    {
                        DataTable dt_lctc1 = ListToDataTable(list_lctc);//物品明细信息
                        string a = string.Empty;
                        folderBrowserDialog1.SelectedPath = "";
                        if (folderBrowserDialog1.ShowDialog() == DialogResult.OK)
                            a = folderBrowserDialog1.SelectedPath;
                        ExcelUI.OpenExcel_A4_3(dt_lctc1, Application.StartupPath + "\\report\\A30\\导出融通数据包 - 物品.xlsx", a + "\\导出融通数据包—物品信息.xlsx", 1, 0, 15);
                        MessageBox.Show("导出成功!");
                    }

                 
 
                    #region 数据写入
                    m_accessHelper.ExPiliang_rt_dwxx(list_dwxx);
                    m_accessHelper.ExPiliang_rt_lctc(list_lctc);
                    #endregion

                    #region 导出文件
                    //获取系统当前工作目录
                    String currentDirectory = "";
                    currentDirectory = Application.StartupPath;

                    string tem_Dfile = currentDirectory + "\\database\\rtsj.mdb";

                    String tem_Dfile_encript = textBox1.Text + "\\融通数据文件" + DateTime.Now.ToString("yyyyMMddHHmmss") + ".bak";
                    CopyFile(tem_Dfile, tem_Dfile_encript, 1024);//复制转换后文件
                    #endregion
                }
                else
                {
                    MessageBox.Show("导出数据为空，请确认！");
                    return;
                }
            }
        }

        FileStream FormerOpen;
        FileStream ToFileOpen;
        public void CopyFile(string FormerFile, string toFile, int SectSize)
        {
            FileStream fileToCreate = new FileStream(toFile, FileMode.Create);		//创建目的文件，如果已存在将被覆盖
            fileToCreate.Close();										//关闭所有资源
            fileToCreate.Dispose();										//释放所有资源
            FormerOpen = new FileStream(FormerFile, FileMode.Open, FileAccess.Read);//以只读方式打开源文件
            ToFileOpen = new FileStream(toFile, FileMode.Append, FileAccess.Write);	//以写方式打开目的文件
            //根据一次传输的大小，计算传输的个数
            int FileSize;												//要拷贝的文件的大小
            //如果分段拷贝，即每次拷贝内容小于文件总长度
            if (SectSize < FormerOpen.Length)
            {
                byte[] buffer = new byte[SectSize];							//根据传输的大小，定义一个字节数组
                int copied = 0;										//记录传输的大小
                while (copied <= ((int)FormerOpen.Length - SectSize))			//拷贝主体部分
                {
                    FileSize = FormerOpen.Read(buffer, 0, SectSize);			//从0开始读，每次最大读SectSize
                    FormerOpen.Flush();								//清空缓存
                    ToFileOpen.Write(buffer, 0, SectSize);					//向目的文件写入字节
                    ToFileOpen.Flush();									//清空缓存
                    ToFileOpen.Position = FormerOpen.Position;				//使源文件和目的文件流的位置相同
                    copied += FileSize;									//记录已拷贝的大小
                }
                int left = (int)FormerOpen.Length - copied;						//获取剩余大小
                FileSize = FormerOpen.Read(buffer, 0, left);					//读取剩余的字节
                FormerOpen.Flush();									//清空缓存
                ToFileOpen.Write(buffer, 0, left);							//写入剩余的部分
                ToFileOpen.Flush();									//清空缓存
            }
            //如果整体拷贝，即每次拷贝内容大于文件总长度
            else
            {
                byte[] buffer = new byte[FormerOpen.Length];				//获取文件的大小
                FormerOpen.Read(buffer, 0, (int)FormerOpen.Length);			//读取源文件的字节
                FormerOpen.Flush();									//清空缓存
                ToFileOpen.Write(buffer, 0, (int)FormerOpen.Length);			//写放字节
                ToFileOpen.Flush();									//清空缓存
            }
            FormerOpen.Close();										//释放所有资源
            ToFileOpen.Close();										//释放所有资源
            MessageBox.Show("融通数据导出成功！");
        }


        public  DataTable ListToDataTable<T>(List<T> entitys)
        {

            //检查实体集合不能为空
            if (entitys == null || entitys.Count < 1)
            {
                return new DataTable();
            }

            //取出第一个实体的所有Propertie
            Type entityType = entitys[0].GetType();
            PropertyInfo[] entityProperties = entityType.GetProperties();

            //生成DataTable的structure
            //生产代码中，应将生成的DataTable结构Cache起来，此处略
            DataTable dt = new DataTable("dt");
            for (int i = 0; i < entityProperties.Length; i++)
            {
                //dt.Columns.Add(entityProperties[i].Name, entityProperties[i].PropertyType);
                dt.Columns.Add(entityProperties[i].Name);
            }

            //将所有entity添加到DataTable中
            foreach (object entity in entitys)
            {
                //检查所有的的实体都为同一类型
                if (entity.GetType() != entityType)
                {
                    throw new Exception("要转换的集合元素类型不一致");
                }
                object[] entityValues = new object[entityProperties.Length];
                for (int i = 0; i < entityProperties.Length; i++)
                {
                    entityValues[i] = entityProperties[i].GetValue(entity, null);

                }
                dt.Rows.Add(entityValues);
            }
            return dt;
        }

        /// <summary>
        /// 获取输入值的加密串
        /// </summary>
        private string GetInputCode_1(string inputStr)
        {
            string result = string.Empty;
            string inputNum = "";
            int keyNum = 0;
            #region 验证及补齐18位
            try
            {
                inputNum = Convert.ToString(inputStr);
            }
            catch
            {
                result = "只能输入不超过18位的数字";
                return result;
            }
            if (inputStr.Length > 18)
            {
                result = "只能输入不超过18位的数字";
                return result;
            }
            switch (inputStr)
            {
                case "011004006010":
                    keyNum = inputStr.Length;
                    break;
                case "011001002011":
                    keyNum = inputStr.Length;
                    break;
                case "016001002010":
                    keyNum = inputStr.Length;
                    break;
                case "018001002010":
                    keyNum = inputStr.Length;
                    break;
                case "024003006010":
                    keyNum = inputStr.Length;
                    break;
                case "018001004010":
                    keyNum = inputStr.Length;
                    break;
            }
            int inputLen = inputStr.Length;
            for (int i = 1; i <= 18 - inputLen; i++)
            {
                inputStr += "0";
            }
            #endregion

            //翻转数值
            string turnStr = GetNumTurn(inputStr);

            //原数值和翻转后数值拆分
            char[] turnStrArr = turnStr.ToCharArray();
            char[] numStrArr = inputStr.ToCharArray();
            for (int i = 0; i < numStrArr.Length; i++)
            {
                int sourceN = int.Parse(numStrArr[i].ToString());
                int turnN = int.Parse(turnStrArr[i].ToString());
                //取索引、原数值、翻转数值 对应索引位置的差值，获取绝对值
                string seedStr = Math.Abs(i - sourceN - turnN - keyNum).ToString();
                //确认是否为1位数，不是则循环拆分相加
                while (seedStr.Length > 1)
                {
                    char[] seedArr = seedStr.ToCharArray();
                    int seedArrSum = 0;
                    foreach (var item in seedArr)
                    {
                        seedArrSum += int.Parse(item.ToString());
                    }
                    seedStr = seedArrSum.ToString();
                }
                result += seedStr;
            }
            return result;
        }

        /// <summary>
        /// 翻转数字
        /// </summary>
        /// <param name="n"></param>
        /// <returns></returns>
        private string GetNumTurn(string inputStr)
        {
            string result = string.Empty;
            char[] splitArr = inputStr.ToCharArray();
            string resultStr = string.Empty;
            for (int i = splitArr.Length - 1; i >= 0; i--)
            {
                result += splitArr[i].ToString();
            }
            return result;
        }

    }
}
