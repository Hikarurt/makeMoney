namespace zj
{
    partial class PLXGSJ
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            this.cbCZFS = new System.Windows.Forms.ComboBox();
            this.label15 = new System.Windows.Forms.Label();
            this.label1 = new System.Windows.Forms.Label();
            this.checkBox1 = new System.Windows.Forms.CheckBox();
            this.button1 = new System.Windows.Forms.Button();
            this.button3 = new System.Windows.Forms.Button();
            this.cbLB = new System.Windows.Forms.ComboBox();
            this.SuspendLayout();
            // 
            // cbCZFS
            // 
            this.cbCZFS.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cbCZFS.Font = new System.Drawing.Font("宋体", 10.8F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.cbCZFS.FormattingEnabled = true;
            this.cbCZFS.Items.AddRange(new object[] {
            "请选择",
            "拟移交物品",
            "拟上交物品",
            "拟捐赠物品",
            "拟销毁物品",
            "拟个案处理物品"});
            this.cbCZFS.Location = new System.Drawing.Point(198, 103);
            this.cbCZFS.Margin = new System.Windows.Forms.Padding(4);
            this.cbCZFS.Name = "cbCZFS";
            this.cbCZFS.Size = new System.Drawing.Size(237, 26);
            this.cbCZFS.TabIndex = 289;
            // 
            // label15
            // 
            this.label15.AutoSize = true;
            this.label15.Font = new System.Drawing.Font("宋体", 10.8F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.label15.Location = new System.Drawing.Point(20, 106);
            this.label15.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.label15.Name = "label15";
            this.label15.Size = new System.Drawing.Size(161, 19);
            this.label15.TabIndex = 288;
            this.label15.Text = "处置方式（建议）";
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Font = new System.Drawing.Font("宋体", 10.8F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.label1.Location = new System.Drawing.Point(134, 48);
            this.label1.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(47, 19);
            this.label1.TabIndex = 282;
            this.label1.Text = "类别";
            // 
            // checkBox1
            // 
            this.checkBox1.AutoSize = true;
            this.checkBox1.Font = new System.Drawing.Font("宋体", 12F);
            this.checkBox1.Location = new System.Drawing.Point(198, 167);
            this.checkBox1.Name = "checkBox1";
            this.checkBox1.Size = new System.Drawing.Size(191, 24);
            this.checkBox1.TabIndex = 305;
            this.checkBox1.Text = "自动修改错误总值";
            this.checkBox1.UseVisualStyleBackColor = true;
            // 
            // button1
            // 
            this.button1.Location = new System.Drawing.Point(90, 224);
            this.button1.Margin = new System.Windows.Forms.Padding(4);
            this.button1.Name = "button1";
            this.button1.Size = new System.Drawing.Size(111, 41);
            this.button1.TabIndex = 307;
            this.button1.Text = "确  定";
            this.button1.UseVisualStyleBackColor = true;
            this.button1.Click += new System.EventHandler(this.button1_Click);
            // 
            // button3
            // 
            this.button3.Location = new System.Drawing.Point(251, 224);
            this.button3.Margin = new System.Windows.Forms.Padding(4);
            this.button3.Name = "button3";
            this.button3.Size = new System.Drawing.Size(112, 41);
            this.button3.TabIndex = 306;
            this.button3.Text = "关  闭";
            this.button3.UseVisualStyleBackColor = true;
            this.button3.Click += new System.EventHandler(this.button3_Click);
            // 
            // cbLB
            // 
            this.cbLB.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cbLB.Font = new System.Drawing.Font("宋体", 10.8F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.cbLB.FormattingEnabled = true;
            this.cbLB.Items.AddRange(new object[] {
            "请选择",
            "香烟",
            "酒水",
            "茶叶",
            "食材",
            "药材",
            "瓷器",
            "字画",
            "金银",
            "玉石",
            "文玩",
            "木材",
            "模型",
            "纪念币",
            "日用品",
            "其他"});
            this.cbLB.Location = new System.Drawing.Point(198, 48);
            this.cbLB.Margin = new System.Windows.Forms.Padding(4);
            this.cbLB.Name = "cbLB";
            this.cbLB.Size = new System.Drawing.Size(237, 26);
            this.cbLB.TabIndex = 308;
            // 
            // PLXGSJ
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 15F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(509, 278);
            this.Controls.Add(this.cbLB);
            this.Controls.Add(this.button1);
            this.Controls.Add(this.button3);
            this.Controls.Add(this.checkBox1);
            this.Controls.Add(this.cbCZFS);
            this.Controls.Add(this.label15);
            this.Controls.Add(this.label1);
            this.Name = "PLXGSJ";
            this.Text = "批量修改数据";
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.ComboBox cbCZFS;
        private System.Windows.Forms.Label label15;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.CheckBox checkBox1;
        private System.Windows.Forms.Button button1;
        private System.Windows.Forms.Button button3;
        private System.Windows.Forms.ComboBox cbLB;
    }
}