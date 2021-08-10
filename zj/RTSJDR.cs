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
    public partial class RTSJDR : Form
    {
        public RTSJDR()
        {
            InitializeComponent();
        }

        private void button4_Click(object sender, EventArgs e)
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
                textBox1.Text = ofd.FileName;
            }
        }

        private void button1_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void button2_Click(object sender, EventArgs e)
        {
            string path = textBox1.Text;
            if (string.IsNullOrEmpty(path))
            {
                MessageBox.Show("请选择要导入的文件", "请检查！", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }
            label1.Visible = true;
            DataTable dt = new DataTable();
            AccessHelper m_accessHelper = new AccessHelper();
            try
            {
                //导入明细实际交接数据
                if (MDIMain.dw0rlctc == "lctc")
                {
                    dt = AccessHelper.ExcelToDataTable(path, "已核查明细");

                    if (dt.Rows.Count > 0)
                    {
                        for (int i = 0; i < dt.Rows.Count; i++)
                        {
                            dt.Rows[i][0] = m_accessHelper.DecodeBase64(dt.Rows[i][0].ToString());
                            dt.Rows[i][1] = m_accessHelper.DecodeBase64(dt.Rows[i][1].ToString());
                            dt.Rows[i][2] = m_accessHelper.DecodeBase64(dt.Rows[i][2].ToString());
                            dt.Rows[i][3] = m_accessHelper.DecodeBase64(dt.Rows[i][3].ToString());
                            string sql_update = "update t_lctc set jjqrsl=" + dt.Rows[i][2].ToString() + ",jjbz='" + dt.Rows[i][3].ToString() + "' where wpbs='" + dt.Rows[i][0].ToString() + "' and dwbm='" + dt.Rows[i][1].ToString() + "'";
                            m_accessHelper.ExcueteCommand(sql_update);
                        }
                        MessageBox.Show("已成功接收" + dt.Rows.Count + "条数据！");
                        label1.Visible = false;
                    }
                }
                //导入单位实际交接数据
                if (MDIMain.dw0rlctc == "dwxx")
                {
                    dt = AccessHelper.ExcelToDataTable(path, "查询单位核查数量");
                    if (dt.Rows.Count > 0)
                    {
                        for (int i = 0; i < dt.Rows.Count; i++)
                        {
                            string sql_update = "update t_dwxx set jjqrsl=" + dt.Rows[i][1].ToString() + " where dwbs='" + dt.Rows[i][0].ToString() + "'";
                            m_accessHelper.ExcueteCommand(sql_update);
                        }
                        MessageBox.Show("已成功接收" + dt.Rows.Count + "条数据！");
                        label1.Visible = false;
                    }
                }
                //导入融通接收的数据（区分有单位和没单位标识）
                if (MDIMain.dw0rlctc == "DR")
                {
                    List<WP_LCTCdata> list_lctc = new List<WP_LCTCdata>();

                    try
                    {
                        dt = AccessHelper.ExcelToDataTable(path, "商品信息查询");
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
                            if (string.IsNullOrEmpty(dt.Rows[i]["序号"].ToString()))
                            {

                            }
                            else
                            {
                                string dwbs = dt.Rows[i]["单位代码"].ToString();
                                if (!string.IsNullOrEmpty(dwbs))
                                {
                                    //修改物品交接信息
                                    string sql_update = string.Format("update t_lctc set jjqrsl={0}, jjqrpm='{1}',jjbz='{2}' where wpbs='{3}'", dt.Rows[i]["交接确认数量"].ToString(), dt.Rows[i]["交接确认品名"].ToString(), dt.Rows[i]["交接备注"].ToString(), dt.Rows[i]["物品标识"].ToString());
                                    int changeCount = m_accessHelper.ExcueteCommand(sql_update);

                                    if (changeCount > 0)  //修改成功，表明数据已存在现有系统内
                                    {
                                    }
                                    else //无受影响行数
                                    {
                                        //确认单位是否存在
                                        string sql_dw = string.Format("select dwdm from t_dwxx where dwbs='{0}'", dwbs);
                                        DataTable dt_dwxxcount = m_accessHelper.getDataSet(sql_dw).Tables[0];
                                        string num = string.Empty;
                                        if (dt_dwxxcount.Rows.Count > 0)
                                        {
                                            num = dt_dwxxcount.Rows[0][0].ToString();
                                        }

                                        //存在
                                        if (!string.IsNullOrEmpty(num))
                                        {
                                            //判断当前单位下是否已创建其他部门
                                            string sql_bm = string.Format("select bmdm,bmmc,bmbs from t_bm where dwdm='{0}' and bmmc='其他部门'", num);
                                            DataTable dt_bm = m_accessHelper.getDataSet(sql_bm).Tables[0];
                                            string bmbs = string.Empty;
                                            if (dt_bm.Rows.Count > 0)
                                            {
                                                bmbs = dt_bm.Rows[0]["bmbs"].ToString();
                                            }
                                            else //数据不存在
                                            {
                                                bmbs = Guid.NewGuid().ToString();
                                                string sql_maxbmdm = string.Format("select max(bmdm) from t_bm where  dwdm='{0}' and len(bmdm)=3", num);
                                                int max_bmdm = Convert.ToInt32(m_accessHelper.getDataSet(sql_maxbmdm).Tables[0].Rows[0][0].ToString() == "" ? "111" : m_accessHelper.getDataSet(sql_maxbmdm).Tables[0].Rows[0][0].ToString());
                                                max_bmdm++;
                                                //创建其他部门
                                                string sql_addbm = string.Format("insert into t_bm(bmbs,dwdm,bmdm,bmmc,bmfjdm) values('{0}','{1}','{2}','{3}','{4}')", bmbs, num, max_bmdm, "其他部门", max_bmdm);
                                                m_accessHelper.ExcueteCommand(sql_addbm);

                                            }
                                            list_lctc.Add(new WP_LCTCdata()
                                            {
                                                dwdm = num,
                                                bmbs = bmbs,
                                                lb = dt.Rows[i]["类别"].ToString(),
                                                pm = dt.Rows[i]["品名"].ToString(),
                                                ly = dt.Rows[i]["来源"].ToString(),
                                                hqsj = dt.Rows[i]["获取时间"].ToString(),
                                                sl = double.Parse(dt.Rows[i]["数量"].ToString()),
                                                jldw = dt.Rows[i]["计量单位"].ToString(),
                                                dj = double.Parse(dt.Rows[i]["单价"].ToString()),
                                                zz = double.Parse(dt.Rows[i]["总值"].ToString()),
                                                kysl = double.Parse(dt.Rows[i]["刊用数量"].ToString()),
                                                kbxjz = double.Parse(dt.Rows[i]["可变现价值"].ToString()),
                                                czfs = dt.Rows[i]["处置方式"].ToString(),
                                                bz = dt.Rows[i]["备注"].ToString(),
                                                wpbs = dt.Rows[i]["物品标识"].ToString(),
                                                djlx = dt.Rows[i]["单价类型"].ToString(),
                                                dwbm = dwbs,
                                                jjqrsl = double.Parse(dt.Rows[i]["交接确认数量"].ToString()),
                                                jjqrpm = dt.Rows[i]["交接确认品名"].ToString(),
                                                jjbz = dt.Rows[i]["交接备注"].ToString(),
                                            });
                                        }
                                        else //不存在
                                        {
                                            string sql_selectdwxx = "select dwdm,dwbs from t_dwxx where dwdm='011025'";
                                            DataTable dt_dwxx = m_accessHelper.getDataSet(sql_selectdwxx).Tables[0];
                                            string dwdm = string.Empty;
                                            string dwds = string.Empty;
                                            string bmbs = string.Empty;
                                            if (dt_dwxx.Rows.Count > 0)
                                            {
                                                dwdm = dt_dwxx.Rows[0]["dwdm"].ToString();
                                                dwds = dt_dwxx.Rows[0]["dwbs"].ToString();
                                                string sql_bmbs = "select bmbs from t_bm where dwdm='" + dwdm + "' and bmmc='其他部门'";
                                                bmbs = m_accessHelper.getDataSet(sql_bmbs).Tables[0].Rows[0][0].ToString();
                                            }
                                            else
                                            {
                                                dwdm = "011025";
                                                dwds = GetInputCode_1(dwdm);
                                                bmbs = Guid.NewGuid().ToString();
                                                //创建其他部门
                                                string sql_adddw = string.Format("insert into t_dwxx(dwdm,dwmc,dwjb,dwlx,szss,xxdz,yzbm,lxr,lxfs,szs,szx,dwbs,sszd,jxzj,jxsj,nxsh,nxs,nxjsd,sfxhwp,xhsj,xhdd) values('{0}','其他单位','正战区级','陆军','贵州省','12','12','张三','12133311331','贵阳市','市辖区','{1}','贵州省—贵阳市—市辖区—12','12','12','北京市','北京市','军委办公厅',0,'2019/12/25','12')", dwdm, dwbs);
                                                m_accessHelper.ExcueteCommand(sql_adddw);
                                                //创建其他部门
                                                string sql_addbm = string.Format("insert into t_bm(bmbs,dwdm,bmdm,bmmc,bmfjdm) values('{0}','{1}','{2}','{3}','{4}')", bmbs, dwdm, "111", "其他部门", "111");
                                                m_accessHelper.ExcueteCommand(sql_addbm);
                                            }

                                            list_lctc.Add(new WP_LCTCdata()
                                            {
                                                dwdm = dwdm,
                                                bmbs = bmbs,
                                                lb = dt.Rows[i]["类别"].ToString(),
                                                pm = dt.Rows[i]["品名"].ToString(),
                                                ly = dt.Rows[i]["来源"].ToString(),
                                                hqsj = dt.Rows[i]["获取时间"].ToString(),
                                                sl = double.Parse(dt.Rows[i]["数量"].ToString()),
                                                jldw = dt.Rows[i]["计量单位"].ToString(),
                                                dj = double.Parse(dt.Rows[i]["单价"].ToString()),
                                                zz = double.Parse(dt.Rows[i]["总值"].ToString()),
                                                kysl = double.Parse(dt.Rows[i]["刊用数量"].ToString()),
                                                kbxjz = double.Parse(dt.Rows[i]["可变现价值"].ToString()),
                                                czfs = dt.Rows[i]["处置方式"].ToString(),
                                                bz = dt.Rows[i]["备注"].ToString(),
                                                wpbs = dt.Rows[i]["物品标识"].ToString(),
                                                djlx = dt.Rows[i]["单价类型"].ToString(),
                                                dwbm = dwds,
                                                jjqrsl = double.Parse(dt.Rows[i]["交接确认数量"].ToString()),
                                                jjqrpm = dt.Rows[i]["交接确认品名"].ToString(),
                                                jjbz = dt.Rows[i]["交接备注"].ToString(),
                                            });


                                        }

                                    }
                                }
                            }
                        }
                    }
                    else
                    {
                        MessageBox.Show("接收数据为空");
                        label1.Visible = false;
                    }

                    if (list_lctc.Count > 0)
                    {
                        bool exresult = m_accessHelper.ExPiliang_rt(list_lctc);
                        if (exresult)
                        {
                            MessageBox.Show("已成功导入" + dt.Rows.Count + "条数据！", "系统提示", MessageBoxButtons.OK, MessageBoxIcon.Information);
                        }
                        else
                        {
                            MessageBox.Show("导入失败");
                        }
                        this.Close();
                    }
                    else
                    {
                        MessageBox.Show("已成功接收" + dt.Rows.Count + "条数据！");
                        label1.Visible = false;
                    }
                }

            }
            catch (Exception ex)
            {
                MessageBox.Show("请选择系统所提供的导入模板", "请检查！", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }

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

        public class WP_LCTCdata
        {
            public string dwdm { get; set; }
            public string bmbs { get; set; }

            public string lb { get; set; }

            public string pm { get; set; }

            public string ly { get; set; }
            public string hqsj { get; set; }
            public double sl { get; set; }
            public string jldw { get; set; }
            public double dj { get; set; }
            public double zz { get; set; }
            public double kysl { get; set; }
            public double kbxjz { get; set; }
            public string czfs { get; set; }
            public string bz { get; set; }

            public string wpbs { get; set; }
            public string djlx { get; set; }
            public string dwbm { get; set; }
            public double jjqrsl { get; set; }
            public string jjqrpm { get; set; }
            public string jjbz { get; set; }


        }
    }
}
