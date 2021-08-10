using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;
using System.Data.OleDb;


namespace zj
{
    public partial class initialization : Form
    {
        public initialization()
        {
            InitializeComponent();
        }

        /// <summary>
        /// 系统初始化
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void button1_Click(object sender, EventArgs e)
        {
            String sql_user = "update t_user set username1='admin',password1='88888888' where username1 ='admin'";
            String sql_del_dwxx = "delete from t_dwxx";
            //String sql_del_yhzhqccl = "delete from t_yhzhqccl";
            //String sql_del_yhzh = "delete from t_yhzh";
            //String sql_del_dqck = "delete from t_dqck";
            //String sql_del_zjjc = "delete from t_zjjc";
            //String sql_del_xjsy = "delete from t_xjsy";
            //String sql_del_gwk = "delete from t_gwk";
            //String sql_del_jdk = "delete from t_jdk";
            //String sql_del_wlkx = "delete from t_wlkx";
            String sql_del_lctc = "delete from t_lctc";
            String sql_del_bm = "delete from t_bm";

            AccessHelper AccessHelper = new AccessHelper();
            AccessHelper.ExcueteCommand(sql_user);
            AccessHelper.ExcueteCommand(sql_del_dwxx);
            AccessHelper.ExcueteCommand(sql_del_lctc);
            AccessHelper.ExcueteCommand(sql_del_bm);
            //AccessHelper.ExcueteCommand(sql_del_dqck);
            //AccessHelper.ExcueteCommand(sql_del_zjjc);
            //AccessHelper.ExcueteCommand(sql_del_xjsy);
            //AccessHelper.ExcueteCommand(sql_del_gwk);
            //AccessHelper.ExcueteCommand(sql_del_jdk);
            //AccessHelper.ExcueteCommand(sql_del_wlkx);

            MessageBox.Show("系统初始化完成，请重新登录！");
            Application.Exit();
        }
    }
}
