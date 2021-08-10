using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.Data.OleDb;

namespace zj
{
    public partial class report_A4_1 : Form
    {


        public report_A4_1()
        {
            InitializeComponent();
        }
        //导出到EXCEL
        private void button2_Click(object sender, EventArgs e)
        {
            if (textBox1.Text == "")
            {
                MessageBox.Show("请选择报表输出路径!");
                return;
            }
            DataTable dt1 = dataGridView1.DataSource as DataTable;
            if (dt1 == null)
            {
                MessageBox.Show("未提取统计数据，不能导出！");
                return;
            }
            if (comboBox1.Text.Substring(0, 2) == "表一")
            {
                ExcelUI.OpenExcel_A4_3(dt1, Application.StartupPath + "\\report\\A4\\3.xls", textBox1.Text+"\\表一.xls",7, 0);
            }
            if (comboBox1.Text.Substring(0, 2) == "表二")
            {
                ExcelUI.OpenExcel_A4_3(dt1, Application.StartupPath + "\\report\\A4\\4.xls", textBox1.Text+"\\表二.xls",6, 0);
            }
            if (comboBox1.Text.Substring(0, 2) == "表三")
            {
                ExcelUI.OpenExcel_A4_3(dt1, Application.StartupPath + "\\report\\A4\\5.xls", textBox1.Text + "\\表三.xls", 7, 0);
            }
            if (comboBox1.Text.Substring(0, 2) == "表四")
            {
                ExcelUI.OpenExcel_A4_3(dt1, Application.StartupPath + "\\report\\A4\\6.xls", textBox1.Text + "\\表四.xls", 7, 0);
            }
            if (comboBox1.Text.Substring(0, 2) == "表五")
            {
                ExcelUI.OpenExcel_A4_3(dt1, Application.StartupPath + "\\report\\A4\\7.xls", textBox1.Text + "\\表五.xls", 7, 0);
                MessageBox.Show("请在备注栏填写现金支出的开支内容，然后再打印！");
            }
            if (comboBox1.Text.Substring(0, 2) == "表六")
            {
                ExcelUI.OpenExcel_A4_3(dt1, Application.StartupPath + "\\report\\A4\\8.xls", textBox1.Text + "\\表六.xls", 7, 0);
            }
        }

        private void button3_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void comboBox1_SelectedIndexChanged(object sender, EventArgs e)
        {
            dataGridView1.DataSource = null;
        }

        private void button4_Click(object sender, EventArgs e)
        {
            folderBrowserDialog1.SelectedPath = "";
            if (folderBrowserDialog1.ShowDialog() == DialogResult.OK)
                textBox1.Text = folderBrowserDialog1.SelectedPath;
        }

        private void button1_Click_1(object sender, EventArgs e)
        {
            select_report select_report = new select_report();
            if (comboBox1.Text.Substring(0, 2) == "表一")
            {
                dataGridView1.DataSource = select_report.report_A4_1("");
                dataGridView1.Columns[0].HeaderCell.Value = "序号";
                dataGridView1.Columns[1].HeaderCell.Value = "单位名称";
                dataGridView1.Columns[2].HeaderCell.Value = "数量合计";
                dataGridView1.Columns[3].HeaderCell.Value = "存款金额合计";
                dataGridView1.Columns[4].HeaderCell.Value = "财务单一账户数量";
                dataGridView1.Columns[5].HeaderCell.Value = "财务单一账户存款余额";
                dataGridView1.Columns[6].HeaderCell.Value = "小额账户数量";
                dataGridView1.Columns[7].HeaderCell.Value = "小额账户存款余额";
                dataGridView1.Columns[8].HeaderCell.Value = "零余额账户数量";
                dataGridView1.Columns[9].HeaderCell.Value = "一般存款账户数量";
                dataGridView1.Columns[10].HeaderCell.Value = "一般存款账户存款余额";
                dataGridView1.Columns[11].HeaderCell.Value = "专用存款账户数量";
                dataGridView1.Columns[12].HeaderCell.Value = "专用存款账户存款余额";
                dataGridView1.Columns[13].HeaderCell.Value = "临时存款账户数量";
                dataGridView1.Columns[14].HeaderCell.Value = "临时存款账户存款余额";
                dataGridView1.Columns[15].HeaderCell.Value = "POS转账卡账户数量";
                dataGridView1.Columns[16].HeaderCell.Value = "POS转账卡账户存款余额";
            }
            if (comboBox1.Text.Substring(0, 2) == "表二")
            {
                dataGridView1.DataSource = select_report.report_A4_2("");
                dataGridView1.Columns[0].HeaderCell.Value = "序号";
                dataGridView1.Columns[1].HeaderCell.Value = "单位名称";
                dataGridView1.Columns[2].HeaderCell.Value = "数量合计";
                dataGridView1.Columns[3].HeaderCell.Value = "存款金额合计";
                dataGridView1.Columns[4].HeaderCell.Value = "工商银行账户数量";
                dataGridView1.Columns[5].HeaderCell.Value = "工商银行存款余额";
                dataGridView1.Columns[6].HeaderCell.Value = "农业银行账户数量";
                dataGridView1.Columns[7].HeaderCell.Value = "农业银行存款余额";
                dataGridView1.Columns[8].HeaderCell.Value = "中国银行账户数量";
                dataGridView1.Columns[9].HeaderCell.Value = "中国银行存款余额";
                dataGridView1.Columns[10].HeaderCell.Value = "建设银行账户数量";
                dataGridView1.Columns[11].HeaderCell.Value = "建设银行存款余额";
                dataGridView1.Columns[12].HeaderCell.Value = "全国股份制商业银行账户数量";
                dataGridView1.Columns[13].HeaderCell.Value = "全国股份制商业银行存款余额";
                dataGridView1.Columns[14].HeaderCell.Value = "其他银行账户数量";
                dataGridView1.Columns[15].HeaderCell.Value = "其他银行存款余额";
            }
            if (comboBox1.Text.Substring(0, 2) == "表三")
            {
                dataGridView1.DataSource = select_report.report_A4_3("");
                dataGridView1.Columns[0].HeaderCell.Value = "序号";
                dataGridView1.Columns[1].HeaderCell.Value = "单位名称";
                dataGridView1.Columns[2].HeaderCell.Value = "小计笔数";
                dataGridView1.Columns[3].HeaderCell.Value = "小计金额";
                dataGridView1.Columns[4].HeaderCell.Value = "开户银行笔数";
                dataGridView1.Columns[5].HeaderCell.Value = "开户银行金额";
                dataGridView1.Columns[6].HeaderCell.Value = "非开户行工商银行笔数";
                dataGridView1.Columns[7].HeaderCell.Value = "非开户行工商银行金额";
                dataGridView1.Columns[8].HeaderCell.Value = "非开户行农业银行笔数";
                dataGridView1.Columns[9].HeaderCell.Value = "非开户行农业银行金额";
                dataGridView1.Columns[10].HeaderCell.Value = "非开户行中国银行金额";
                dataGridView1.Columns[11].HeaderCell.Value = "非开户行中国银行金额";
                dataGridView1.Columns[12].HeaderCell.Value = "非开户行建设银行笔数";
                dataGridView1.Columns[13].HeaderCell.Value = "非开户行建设银行金额";
                dataGridView1.Columns[14].HeaderCell.Value = "非开户行全国股份制商业银行笔数";
                dataGridView1.Columns[15].HeaderCell.Value = "非开户行全国股份制商业银行金额";
                dataGridView1.Columns[16].HeaderCell.Value = "非开户行其他银行笔数";
                dataGridView1.Columns[17].HeaderCell.Value = "非开户行其他银行金额";
            }
            if (comboBox1.Text.Substring(0, 2) == "表四")
            {
                dataGridView1.DataSource = select_report.report_A4_4("");
                dataGridView1.Columns[0].HeaderCell.Value = "序号";
                dataGridView1.Columns[1].HeaderCell.Value = "单位名称";
                dataGridView1.Columns[2].HeaderCell.Value = "资金结存合计";
                dataGridView1.Columns[3].HeaderCell.Value = "库存现金";
                dataGridView1.Columns[4].HeaderCell.Value = "活期存款";
                dataGridView1.Columns[5].HeaderCell.Value = "定期（通知）存款";
                dataGridView1.Columns[6].HeaderCell.Value = "有价证券";
                dataGridView1.Columns[7].HeaderCell.Value = "资金来源合计";
                dataGridView1.Columns[8].HeaderCell.Value = "预算经费";
                dataGridView1.Columns[9].HeaderCell.Value = "当年预算外经费";
                dataGridView1.Columns[10].HeaderCell.Value = "历年经费结余";
                dataGridView1.Columns[11].HeaderCell.Value = "专项资金";
                dataGridView1.Columns[12].HeaderCell.Value = "应上缴经费";
                dataGridView1.Columns[13].HeaderCell.Value = "专项基金";
                dataGridView1.Columns[14].HeaderCell.Value = "往来款项等";
            }
            if (comboBox1.Text.Substring(0, 2) == "表五")
            {
                dataGridView1.DataSource = select_report.report_A4_5("");
                dataGridView1.Columns[0].HeaderCell.Value = "序号";
                dataGridView1.Columns[1].HeaderCell.Value = "单位名称";
                dataGridView1.Columns[2].HeaderCell.Value = "合  计";
                dataGridView1.Columns[3].HeaderCell.Value = "人员待遇支出";
                dataGridView1.Columns[4].HeaderCell.Value = "支付个体经营者支出";
                dataGridView1.Columns[5].HeaderCell.Value = "差旅费、探亲路费支出";
                dataGridView1.Columns[6].HeaderCell.Value = "演习、抢险救灾等支出";
                dataGridView1.Columns[7].HeaderCell.Value = "1000元以内零星支出";
                dataGridView1.Columns[8].HeaderCell.Value = "其他支出";
                dataGridView1.Columns[9].HeaderCell.Value = "备  注";
            }
            if (comboBox1.Text.Substring(0, 2) == "表六")
            {
                dataGridView1.DataSource = select_report.report_A4_6("");
                dataGridView1.Columns[0].HeaderCell.Value = "序号";
                dataGridView1.Columns[1].HeaderCell.Value = "单位名称";
                dataGridView1.Columns[2].HeaderCell.Value = "撤销擅自开设账户数";
                dataGridView1.Columns[3].HeaderCell.Value = "撤销逾期账户数";
                dataGridView1.Columns[4].HeaderCell.Value = "撤销应撤未撤账户数";
                dataGridView1.Columns[5].HeaderCell.Value = "撤销其他违规账户数";
                dataGridView1.Columns[6].HeaderCell.Value = "收拢违规资金笔数";
                dataGridView1.Columns[7].HeaderCell.Value = "收拢违规资金金额";
                dataGridView1.Columns[8].HeaderCell.Value = "收拢违规借垫款笔数";
                dataGridView1.Columns[9].HeaderCell.Value = "收拢违规借垫款金额";
                dataGridView1.Columns[10].HeaderCell.Value = "收拢其他资金笔数";
                dataGridView1.Columns[11].HeaderCell.Value = "收拢其他资金金额";
                dataGridView1.Columns[12].HeaderCell.Value = "规范资金手续笔数";
                dataGridView1.Columns[13].HeaderCell.Value = "规范资金手续金额";
            }
        }
    
    }
}
