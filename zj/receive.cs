using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
//using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.IO;
namespace zj
{
    public partial class receive : Form
    {
        public String dwdm = "";
        public String selected_dwmc = "";
        public receive()
        {
            InitializeComponent();
        }

        private void receive_Load(object sender, EventArgs e)
        {
            String sql_dwxx = "select dwdm,dwdm+dwmc as newdwmc from t_dwxx where len(dwdm)<4 and dwdm<>'000' order by dwdm asc";
            AccessHelper AccessHelper = new AccessHelper();
            DataSet ds = AccessHelper.getDataSet(sql_dwxx);
            comboBox1.DataSource = ds.Tables[0];
            comboBox1.ValueMember = "dwdm";
            comboBox1.DisplayMember = "newdwmc";

        }

        private void comboBox1_SelectedIndexChanged(object sender, EventArgs e)
        {
            //MessageBox.Show(comboBox1.SelectedValue.ToString());
        }

        private void button2_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void button3_Click(object sender, EventArgs e)
        {

        }

        private void pictureBox1_Click(object sender, EventArgs e)
        {
            openFileDialog1.FileName = "";
            openFileDialog1.Filter = "系统报表文件 *.dat|*.dat";
            if (openFileDialog1.ShowDialog() == DialogResult.OK)
                textBox1.Text = openFileDialog1.FileName;
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
            MessageBox.Show("文件读取成功，开始导入数据！");
        }

        private void button1_Click(object sender, EventArgs e)
        {
            String tem_Dfile = textBox1.Text;
            if (!File.Exists(tem_Dfile)){
                MessageBox.Show("未选择有效报表文件或者路径出现错误!");
                return;
            }
            String currentDirectory = Application.StartupPath;
            String tem_Dfile_decrypt = currentDirectory + "\\receive\\decrypt.mdb";
            CopyFile(tem_Dfile, tem_Dfile_decrypt, 1024);//复制转换后文件
            if(string.IsNullOrEmpty(comboBox1.Text))
            {
                MessageBox.Show("请先录入接收单位!");
                return;
            }
            dwdm = comboBox1.SelectedValue.ToString();
            selected_dwmc = comboBox1.Text.Substring(3, (comboBox1.Text.Length - 3));
            if (string.IsNullOrEmpty(dwdm))
            {
                MessageBox.Show("所选接收单位的单位代码为空！");
                return;
            }
            update_dwdm(dwdm);
        }

        private void update_dwdm(String dwdm_super)
        {
            AccessHelper AccessHelper = new AccessHelper();
            DataTable dt_djsdwxx = null;//待接收前单位信息
            DataTable dt_djslctc = null;//待接收前物品信息
            DataTable dt_djsbm = null;//待接收前部门信息
            try
            {
                #region 待接收数据备份
                string sql_dwxxtab = "select * from t_dwxx where dwdm='" + dwdm_super + "'";
                dt_djsdwxx = AccessHelper.getDataSet(sql_dwxxtab).Tables[0];

                string sql_lctctab = "select * from t_lctc where dwdm='" + dwdm_super + "'";
                dt_djslctc = AccessHelper.getDataSet(sql_lctctab).Tables[0];

                string sql_bmtab = "select * from t_bm where dwdm='" + dwdm_super + "'";
                dt_djsbm = AccessHelper.getDataSet(sql_bmtab).Tables[0];
                #endregion

                #region 数据接收
                String sql_select_dw = "select dwmc from t_dwxx where dwdm='000'";
                DataSet ds = new DataSet();
                ds = AccessHelper.getDataSet_receive(sql_select_dw); 
                if (ds.Tables[0].Rows.Count != 1)
                {
                    MessageBox.Show("接收单位报表代码错误，请重新接收！");
                    return;
                }
                MessageBoxButtons messBotton = MessageBoxButtons.OKCancel;
                DialogResult dr = MessageBox.Show("当前选择单位： " + selected_dwmc + "，待接收单位：" + ds.Tables[0].Rows[0]["dwmc"] + "，确认接收？", "导入报表", messBotton);
                if (dr == DialogResult.OK)
                {
                    //在待接收单位代码前加相应前缀
                    String sql_dwxx = "update t_dwxx set dwdm='" + dwdm_super + "'+dwdm";
                    AccessHelper.ExcueteCommand_receive(sql_dwxx);

                    String sql_lctc = "update t_lctc set dwdm='" + dwdm_super + "'+dwdm";
                    AccessHelper.ExcueteCommand_receive(sql_lctc);

                    //以000结尾时删除000
                    sql_dwxx = "update t_dwxx set dwdm=left(dwdm,len(dwdm)-3) where dwdm like '%000'";
                    AccessHelper.ExcueteCommand_receive(sql_dwxx);

                    sql_lctc = "update t_lctc set dwdm=left(dwdm,len(dwdm)-3) where dwdm like '%000'";
                    AccessHelper.ExcueteCommand_receive(sql_lctc);

                    sql_lctc = "update t_bm set dwdm=left(dwdm,len(dwdm)-3) where dwdm like '%000'";
                    AccessHelper.ExcueteCommand_receive(sql_lctc);

                    
                    //删除待接收单位已经存在的数据
                    String sql_del_repeat = "delete from t_dwxx where left(dwdm,3)='" + dwdm + "'";
                    AccessHelper.ExcueteCommand(sql_del_repeat);

                    String sql_del_repeat1 = "delete from t_lctc where left(dwdm,3)='" + dwdm + "'";
                    AccessHelper.ExcueteCommand(sql_del_repeat1);

                    //在待接收单位代码前加相应前缀
                    String sql_BM = "update t_bm set dwdm='" + dwdm_super + "'+dwdm";
                    AccessHelper.ExcueteCommand_receive(sql_BM);

                    //以000结尾时删除000
                    sql_BM = "update t_bm set dwdm=left(dwdm,len(dwdm)-3) where dwdm like '%000'";
                    AccessHelper.ExcueteCommand_receive(sql_BM);

                    //删除待接收单位已经存在的数据
                    sql_del_repeat = "delete from t_bm where left(dwdm,3)='" + dwdm + "'";
                    AccessHelper.ExcueteCommand(sql_del_repeat);

                    String xtgzlj = AccessHelper.m_strCurrentPath;

                    //导入数据
                    String sql_daoru_dwxx = "insert into [;database=" + xtgzlj + "database\\zj.mdb;pwd=*#886402#;].t_dwxx(dwdm,dwmc,dwxz,dwjb,dwlx,szss,xxdz,yzbm,lxr,lxfs,bzk,gzkfk,szs,szx,yjdd,sszd,jxzj,jxsj,nxsh,nxs,nxjsd,sfxhwp,xhsj,xhdd) select dwdm,dwmc,dwxz,dwjb,dwlx,szss,xxdz,yzbm,lxr,lxfs,bzk,gzkfk,szs,szx,yjdd,sszd,jxzj,jxsj,nxsh,nxs,nxjsd,sfxhwp,xhsj,xhdd  from [;database=" + xtgzlj + "receive\\decrypt.mdb;pwd=*#886402#;].t_dwxx";

                    String sql_daoru_lctc = "insert into [;database=" + xtgzlj + "database\\zj.mdb;pwd=*#886402#;].t_lctc(dwdm,bmbs,lb,pm,ly,hqsj,sl,jldw,dj,zz,kysl,kbxjz,czfs,bz,wpbs,djlx,dwbm,jjqrsl,jjqrpm,jjbz) select dwdm,bmbs,lb,pm,ly,hqsj,sl,jldw,dj,zz,kysl,kbxjz,czfs,bz,wpbs,djlx,dwbm,jjqrsl,jjqrpm,jjbz from [;database=" + xtgzlj + "receive\\decrypt.mdb;pwd=*#886402#;].t_lctc";

                    String sql_daoru_bm = "insert into [;database=" + xtgzlj + "database\\zj.mdb;pwd=*#886402#;].t_bm(dwdm,bmdm,bmmc,bmbs,bz,bmfjdm) select dwdm,bmdm,bmmc,bmbs,bz,bmfjdm from [;database=" + xtgzlj + "receive\\decrypt.mdb;pwd=*#886402#;].t_bm";

                    int daorunum1 = AccessHelper.ExcueteCommand(sql_daoru_dwxx);
                    int daorunum10 = AccessHelper.ExcueteCommand(sql_daoru_lctc);
                    int daorunum11 = AccessHelper.ExcueteCommand(sql_daoru_bm);

                    MessageBox.Show("数据导入完成！共导入 " + (daorunum10).ToString() + " 条数据。");
                }
                else
                {
                    return;
                }
                #endregion
            }
            catch (Exception ex)
            {
                #region 删除已接收的数据
                String sql_del_repeat = "delete from t_dwxx where left(dwdm,3)='" + dwdm_super + "'";
                AccessHelper.ExcueteCommand(sql_del_repeat);

                String sql_del_wp = "delete from t_lctc where left(dwdm,3)='" + dwdm_super + "'";
                AccessHelper.ExcueteCommand(sql_del_wp);

                String sql_del_bm = "delete from t_bm where left(dwdm,3)='" + dwdm_super + "'";
                AccessHelper.ExcueteCommand(sql_del_bm);

                #endregion

                #region 数据恢复
                if (dt_djsdwxx.Rows.Count > 0)
                {
                    String sql_insert_dwxx = string.Format("insert into t_dwxx(dwdm,dwmc,dwxz,dwjb,dwlx,szss,xxdz,yzbm,lxr,lxfs,bzk,gzkfk,szs,szx,yjdd,sszd,dwbs) values('{0}','{1}','{2}','{3}','{4}','{5}','{6}','{7}','{8}','{9}','{10}','{11}','{12}','{13}','{14}','{15}','{16}')",dt_djsdwxx.Rows[0]["dwdm"],  dt_djsdwxx.Rows[0]["dwmc"], dt_djsdwxx.Rows[0]["dwxz"],  dt_djsdwxx.Rows[0]["dwjb"], dt_djsdwxx.Rows[0]["dwlx"],  dt_djsdwxx.Rows[0]["szss"], dt_djsdwxx.Rows[0]["xxdz"],  dt_djsdwxx.Rows[0]["yzbm"], dt_djsdwxx.Rows[0]["lxr"],  dt_djsdwxx.Rows[0]["lxfs"], dt_djsdwxx.Rows[0]["bzk"],  dt_djsdwxx.Rows[0]["gzkfk"], dt_djsdwxx.Rows[0]["szs"],  dt_djsdwxx.Rows[0]["szx"], dt_djsdwxx.Rows[0]["yjdd"],  dt_djsdwxx.Rows[0]["sszd"], dt_djsdwxx.Rows[0]["dwbs"]);
                    AccessHelper.ExcueteCommand(sql_insert_dwxx);
                }
                if (dt_djslctc.Rows.Count > 0)
                {
                    for (int i = 0; i < dt_djslctc.Rows.Count; i++)
                    {
                        if (dt_djslctc.Rows[i]["jjqrsl"].ToString() == "")
                        {
                            dt_djslctc.Rows[i]["jjqrsl"] ="0";
                        }
                        string sql_insertLctc = string.Format("insert into t_lctc(dwdm,bmbs,lb,pm,ly,hqsj,sl,jldw,dj,zz,kysl,kbxjz,czfs,bz,wpbs,djlx,dwbm,jjqrsl,jjqrpm,jjbz) values ('{0}','{1}','{2}','{3}','{4}','{5}','{6}','{7}','{8}','{9}','{10}','{11}','{12}','{13}','{14}','{15}','{16}','{17}','{18}','{19}')",   dt_djslctc.Rows[i]["dwdm"], dt_djslctc.Rows[i]["bmbs"], dt_djslctc.Rows[i]["lb"], dt_djslctc.Rows[i]["pm"], dt_djslctc.Rows[i]["ly"], dt_djslctc.Rows[i]["hqsj"], dt_djslctc.Rows[i]["sl"], dt_djslctc.Rows[i]["jldw"], dt_djslctc.Rows[i]["dj"], dt_djslctc.Rows[i]["zz"], dt_djslctc.Rows[i]["kysl"], dt_djslctc.Rows[i]["kbxjz"], dt_djslctc.Rows[i]["czfs"], dt_djslctc.Rows[i]["bz"], dt_djslctc.Rows[i]["wpbs"], dt_djslctc.Rows[i]["djlx"], dt_djslctc.Rows[i]["dwbm"], dt_djslctc.Rows[i]["jjqrsl"], dt_djslctc.Rows[i]["jjqrpm"], dt_djslctc.Rows[i]["jjbz"]);
                        AccessHelper.ExcueteCommand(sql_insertLctc);
                    }
                }
                if (dt_djsbm.Rows.Count > 0)
                {
                    for (int k = 0; k < dt_djsbm.Rows.Count; k++)
                    {
                        string sql_insertBM = string.Format("insert into t_bm(dwdm,bmdm,bmmc,bmbs,bz,bmfjdm) values('{0}','{1}','{2}','{3}','{4}','{5}')", dt_djsbm.Rows[k]["dwdm"], dt_djsbm.Rows[k]["bmdm"], dt_djsbm.Rows[k]["bmmc"], dt_djsbm.Rows[k]["bmbs"], dt_djsbm.Rows[k]["bz"], dt_djsbm.Rows[k]["bmfjdm"]);
                        AccessHelper.ExcueteCommand(sql_insertBM);
                    }
                }
                #endregion

                MessageBox.Show("数据导入失败，异常信息如下： " +ex);
            }
        }
    }
}
