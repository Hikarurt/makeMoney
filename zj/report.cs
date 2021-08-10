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
    public partial class report : Form
    {
        public String dwmc = "";
        public report()
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
            MDIMain MDIMain = (MDIMain)this.MdiParent;
            if (MDIMain.pass == 0)
            {
                MessageBox.Show("审核未通过，不能上报报表！", "系统提示");
                return;
            }


            //获取系统当前工作目录
            String currentDirectory = "";
            currentDirectory = Application.StartupPath;
            //获取目标路径
            if (textBox1.Text.Length == 0)
            {
                MessageBox.Show("请选择报表存放的文件路径！");
                return;
            }
            else
            {
                string tem_Dfile = currentDirectory+ "\\database\\zj.mdb";
                String tem_Dfile_encript = textBox1.Text + "\\留存名贵特产上报数据（"+ dwmc +","+ DateTime.Now.ToString("yyyyMMddHHmmss") + ").dat"; 
              //  
                CopyFile(tem_Dfile, tem_Dfile_encript, 1024);//复制转换后文件
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
            MessageBox.Show("报表已生成！");
        }

        private void report_Load(object sender, EventArgs e)
        {

        }

        private void pictureBox1_Click(object sender, EventArgs e)
        {

        }

        private void report_Shown(object sender, EventArgs e)
        {
            String sql_dwxx = "select dwmc from t_dwxx where dwdm='000'";
            AccessHelper AccessHelper = new AccessHelper();
            DataSet ds = AccessHelper.getDataSet(sql_dwxx);
            if (ds.Tables[0].Rows.Count == 0)
            {
                MessageBox.Show("未设置单位信息");
                return;
            }
            dwmc = ds.Tables[0].Rows[0]["dwmc"].ToString();
            label1.Text = "上报单位: " + dwmc;
        }
    }
}
