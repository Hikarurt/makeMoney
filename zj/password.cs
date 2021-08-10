using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
//using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.Data.OleDb;

namespace zj
{
    public partial class password : Form
    {
        public password()
        {
            InitializeComponent();
        }

        private void label1_Click(object sender, EventArgs e)
        {

        }

        private void pictureBox1_Click(object sender, EventArgs e)
        {

        }

        private void button2_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void password_Load(object sender, EventArgs e)
        {
            string sql_login = "select username1 from t_user";
            AccessHelper AccessHelper = new AccessHelper();
            DataSet ds = AccessHelper.getDataSet(sql_login);
            username.Text = ds.Tables[0].Rows[0]["username1"].ToString();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            String s_username, s_password_old, s_password_new1, s_password_new2;
            s_username = username.Text;
            s_password_old = password_old.Text;
            s_password_new1 = password_new1.Text;
            s_password_new2 = password_new2.Text;
            if (s_password_old == "")
            {
                MessageBox.Show("原始密码不能为空！");
                password_old.Focus();
                return;
            }
            if (s_password_new1 == "")
            {
                MessageBox.Show("新密码不能为空！");
                password_new1.Focus();
                return;
            }
            if (s_password_new1 != s_password_new2)
            {
                MessageBox.Show("两次输入密码不一致，请重新输入！");
                password_new1.Text = "";
                password_new2.Text = "";
                return;
            }
            String sql_password = "update t_user set password1=@PASSWORDNEW where username1=@USERNAME and password1=@PASSWORDOLD";
            OleDbParameter[] parms = new OleDbParameter[] { 
                new OleDbParameter("@PASSWORDNEW",OleDbType.VarChar),
                new OleDbParameter("@USERNAME",OleDbType.VarChar),
                new OleDbParameter("@PASSWORDOLD",OleDbType.VarChar)
            };
            parms[0].Value = s_password_new1;
            parms[1].Value = s_username;
            parms[2].Value = s_password_old;
            AccessHelper AccessHelper = new AccessHelper();
            int updatenum = AccessHelper.ExcueteCommand(sql_password, parms);
            if (updatenum > 0)
            {
                MessageBox.Show("密码修改成功，请使用新密码登录！");
            }
            else
            {
                MessageBox.Show("密码修改失败！");
            }
        }
    }
}
