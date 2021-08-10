namespace zj
{
    partial class FrmBankAccountCheck
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
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle1 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle2 = new System.Windows.Forms.DataGridViewCellStyle();
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(FrmBankAccountCheck));
            this.dataGridView = new System.Windows.Forms.DataGridView();
            this.groupBox = new System.Windows.Forms.GroupBox();
            this.btnExcel = new System.Windows.Forms.Button();
            this.btnExit = new System.Windows.Forms.Button();
            this.btnSave = new System.Windows.Forms.Button();
            this.btnRemove = new System.Windows.Forms.Button();
            this.btnAdd = new System.Windows.Forms.Button();
            this.groupBoxInfo = new System.Windows.Forms.GroupBox();
            this.groupBox1 = new System.Windows.Forms.GroupBox();
            this.label2 = new System.Windows.Forms.Label();
            this.tbQTWGHS = new System.Windows.Forms.TextBox();
            this.tbYCWCZHS = new System.Windows.Forms.TextBox();
            this.label3 = new System.Windows.Forms.Label();
            this.tbYQZHS = new System.Windows.Forms.TextBox();
            this.label1 = new System.Windows.Forms.Label();
            this.tbSZKSZHS = new System.Windows.Forms.TextBox();
            this.label8 = new System.Windows.Forms.Label();
            this.groupBox3 = new System.Windows.Forms.GroupBox();
            this.tbGFSXBS = new System.Windows.Forms.TextBox();
            this.tbGFSXJE = new System.Windows.Forms.TextBox();
            this.label9 = new System.Windows.Forms.Label();
            this.label10 = new System.Windows.Forms.Label();
            this.groupBox2 = new System.Windows.Forms.GroupBox();
            this.tbWGCCJE = new System.Windows.Forms.TextBox();
            this.tbQTYYJE = new System.Windows.Forms.TextBox();
            this.tbQTYYBS = new System.Windows.Forms.TextBox();
            this.label11 = new System.Windows.Forms.Label();
            this.label6 = new System.Windows.Forms.Label();
            this.tbWGCCBS = new System.Windows.Forms.TextBox();
            this.label7 = new System.Windows.Forms.Label();
            this.tbWGJDBS = new System.Windows.Forms.TextBox();
            this.label12 = new System.Windows.Forms.Label();
            this.tbWGJDJE = new System.Windows.Forms.TextBox();
            this.label4 = new System.Windows.Forms.Label();
            this.label5 = new System.Windows.Forms.Label();
            this.tbSJFK = new System.Windows.Forms.TextBox();
            this.label16 = new System.Windows.Forms.Label();
            this.comboDWDM = new System.Windows.Forms.ComboBox();
            this.label = new System.Windows.Forms.Label();
            this.label15 = new System.Windows.Forms.Label();
            this.btnFilter = new System.Windows.Forms.Button();
            this.label13 = new System.Windows.Forms.Label();
            this.label14 = new System.Windows.Forms.Label();
            this.label22 = new System.Windows.Forms.Label();
            this.label18 = new System.Windows.Forms.Label();
            ((System.ComponentModel.ISupportInitialize)(this.dataGridView)).BeginInit();
            this.groupBox.SuspendLayout();
            this.groupBoxInfo.SuspendLayout();
            this.groupBox1.SuspendLayout();
            this.groupBox3.SuspendLayout();
            this.groupBox2.SuspendLayout();
            this.SuspendLayout();
            // 
            // dataGridView
            // 
            this.dataGridView.BackgroundColor = System.Drawing.Color.White;
            dataGridViewCellStyle1.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleCenter;
            dataGridViewCellStyle1.BackColor = System.Drawing.SystemColors.Control;
            dataGridViewCellStyle1.Font = new System.Drawing.Font("宋体", 10.5F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            dataGridViewCellStyle1.ForeColor = System.Drawing.SystemColors.WindowText;
            dataGridViewCellStyle1.SelectionBackColor = System.Drawing.SystemColors.Highlight;
            dataGridViewCellStyle1.SelectionForeColor = System.Drawing.SystemColors.HighlightText;
            dataGridViewCellStyle1.WrapMode = System.Windows.Forms.DataGridViewTriState.True;
            this.dataGridView.ColumnHeadersDefaultCellStyle = dataGridViewCellStyle1;
            this.dataGridView.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dataGridView.Location = new System.Drawing.Point(15, 81);
            this.dataGridView.MultiSelect = false;
            this.dataGridView.Name = "dataGridView";
            this.dataGridView.ReadOnly = true;
            dataGridViewCellStyle2.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleCenter;
            dataGridViewCellStyle2.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(211)))), ((int)(((byte)(228)))), ((int)(((byte)(252)))));
            this.dataGridView.RowsDefaultCellStyle = dataGridViewCellStyle2;
            this.dataGridView.RowTemplate.Height = 23;
            this.dataGridView.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.FullRowSelect;
            this.dataGridView.Size = new System.Drawing.Size(1242, 315);
            this.dataGridView.TabIndex = 3;
            this.dataGridView.SelectionChanged += new System.EventHandler(this.dataGridView_SelectionChanged);
            // 
            // groupBox
            // 
            this.groupBox.Controls.Add(this.btnExcel);
            this.groupBox.Controls.Add(this.btnExit);
            this.groupBox.Controls.Add(this.btnSave);
            this.groupBox.Controls.Add(this.btnRemove);
            this.groupBox.Controls.Add(this.btnAdd);
            this.groupBox.Location = new System.Drawing.Point(657, 568);
            this.groupBox.Name = "groupBox";
            this.groupBox.Size = new System.Drawing.Size(599, 54);
            this.groupBox.TabIndex = 74;
            this.groupBox.TabStop = false;
            // 
            // btnExcel
            // 
            this.btnExcel.Font = new System.Drawing.Font("宋体", 10.5F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.btnExcel.Location = new System.Drawing.Point(365, 15);
            this.btnExcel.Name = "btnExcel";
            this.btnExcel.Size = new System.Drawing.Size(108, 33);
            this.btnExcel.TabIndex = 52;
            this.btnExcel.Text = "导  出";
            this.btnExcel.UseVisualStyleBackColor = true;
            this.btnExcel.Click += new System.EventHandler(this.btnExcel_Click);
            // 
            // btnExit
            // 
            this.btnExit.Font = new System.Drawing.Font("宋体", 10.5F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.btnExit.Location = new System.Drawing.Point(485, 15);
            this.btnExit.Name = "btnExit";
            this.btnExit.Size = new System.Drawing.Size(108, 33);
            this.btnExit.TabIndex = 51;
            this.btnExit.Text = "退  出";
            this.btnExit.UseVisualStyleBackColor = true;
            this.btnExit.Click += new System.EventHandler(this.btnExit_Click);
            // 
            // btnSave
            // 
            this.btnSave.Font = new System.Drawing.Font("宋体", 10.5F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.btnSave.Location = new System.Drawing.Point(246, 15);
            this.btnSave.Name = "btnSave";
            this.btnSave.Size = new System.Drawing.Size(108, 33);
            this.btnSave.TabIndex = 10;
            this.btnSave.Text = "保  存";
            this.btnSave.UseVisualStyleBackColor = true;
            this.btnSave.Click += new System.EventHandler(this.btnSave_Click);
            // 
            // btnRemove
            // 
            this.btnRemove.Font = new System.Drawing.Font("宋体", 10.5F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.btnRemove.Location = new System.Drawing.Point(126, 15);
            this.btnRemove.Name = "btnRemove";
            this.btnRemove.Size = new System.Drawing.Size(108, 33);
            this.btnRemove.TabIndex = 49;
            this.btnRemove.Text = "删  除";
            this.btnRemove.UseVisualStyleBackColor = true;
            this.btnRemove.Click += new System.EventHandler(this.btnRemove_Click);
            // 
            // btnAdd
            // 
            this.btnAdd.Font = new System.Drawing.Font("宋体", 10.5F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.btnAdd.Location = new System.Drawing.Point(7, 15);
            this.btnAdd.Name = "btnAdd";
            this.btnAdd.Size = new System.Drawing.Size(108, 33);
            this.btnAdd.TabIndex = 48;
            this.btnAdd.Text = "增  加";
            this.btnAdd.UseVisualStyleBackColor = true;
            this.btnAdd.Click += new System.EventHandler(this.btnAdd_Click);
            // 
            // groupBoxInfo
            // 
            this.groupBoxInfo.Controls.Add(this.groupBox1);
            this.groupBoxInfo.Controls.Add(this.groupBox3);
            this.groupBoxInfo.Controls.Add(this.groupBox2);
            this.groupBoxInfo.Controls.Add(this.comboDWDM);
            this.groupBoxInfo.Controls.Add(this.label);
            this.groupBoxInfo.Location = new System.Drawing.Point(10, 402);
            this.groupBoxInfo.Name = "groupBoxInfo";
            this.groupBoxInfo.Size = new System.Drawing.Size(1246, 165);
            this.groupBoxInfo.TabIndex = 51;
            this.groupBoxInfo.TabStop = false;
            // 
            // groupBox1
            // 
            this.groupBox1.Controls.Add(this.label2);
            this.groupBox1.Controls.Add(this.tbQTWGHS);
            this.groupBox1.Controls.Add(this.tbYCWCZHS);
            this.groupBox1.Controls.Add(this.label3);
            this.groupBox1.Controls.Add(this.tbYQZHS);
            this.groupBox1.Controls.Add(this.label1);
            this.groupBox1.Controls.Add(this.tbSZKSZHS);
            this.groupBox1.Controls.Add(this.label8);
            this.groupBox1.Font = new System.Drawing.Font("宋体", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.groupBox1.Location = new System.Drawing.Point(7, 52);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Size = new System.Drawing.Size(415, 104);
            this.groupBox1.TabIndex = 66;
            this.groupBox1.TabStop = false;
            this.groupBox1.Text = "撤销银行账户";
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Font = new System.Drawing.Font("宋体", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.label2.Location = new System.Drawing.Point(205, 68);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(149, 20);
            this.label2.TabIndex = 73;
            this.label2.Text = "其他违规账户数";
            // 
            // tbQTWGHS
            // 
            this.tbQTWGHS.Font = new System.Drawing.Font("宋体", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.tbQTWGHS.Location = new System.Drawing.Point(323, 65);
            this.tbQTWGHS.Name = "tbQTWGHS";
            this.tbQTWGHS.Size = new System.Drawing.Size(84, 25);
            this.tbQTWGHS.TabIndex = 72;
            this.tbQTWGHS.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
            this.tbQTWGHS.TextChanged += new System.EventHandler(this.tbQTWGHS_TextChanged);
            this.tbQTWGHS.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.tbQTWGHS_KeyPress);
            this.tbQTWGHS.Leave += new System.EventHandler(this.tbQTWGHS_Leave);
            // 
            // tbYCWCZHS
            // 
            this.tbYCWCZHS.Font = new System.Drawing.Font("宋体", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.tbYCWCZHS.Location = new System.Drawing.Point(322, 23);
            this.tbYCWCZHS.Name = "tbYCWCZHS";
            this.tbYCWCZHS.Size = new System.Drawing.Size(84, 25);
            this.tbYCWCZHS.TabIndex = 70;
            this.tbYCWCZHS.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
            this.tbYCWCZHS.TextChanged += new System.EventHandler(this.tbYCWCZHS_TextChanged);
            this.tbYCWCZHS.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.tbYCWCZHS_KeyPress);
            this.tbYCWCZHS.Leave += new System.EventHandler(this.tbYCWCZHS_Leave);
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Font = new System.Drawing.Font("宋体", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.label3.Location = new System.Drawing.Point(204, 26);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(149, 20);
            this.label3.TabIndex = 69;
            this.label3.Text = "应撤未撤账户数";
            // 
            // tbYQZHS
            // 
            this.tbYQZHS.Font = new System.Drawing.Font("宋体", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.tbYQZHS.Location = new System.Drawing.Point(120, 65);
            this.tbYQZHS.Name = "tbYQZHS";
            this.tbYQZHS.Size = new System.Drawing.Size(84, 25);
            this.tbYQZHS.TabIndex = 68;
            this.tbYQZHS.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
            this.tbYQZHS.TextChanged += new System.EventHandler(this.tbYQZHS_TextChanged);
            this.tbYQZHS.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.tbYQZHS_KeyPress);
            this.tbYQZHS.Leave += new System.EventHandler(this.tbYQZHS_Leave);
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Font = new System.Drawing.Font("宋体", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.label1.Location = new System.Drawing.Point(37, 68);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(109, 20);
            this.label1.TabIndex = 67;
            this.label1.Text = "逾期账户数";
            // 
            // tbSZKSZHS
            // 
            this.tbSZKSZHS.Font = new System.Drawing.Font("宋体", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.tbSZKSZHS.Location = new System.Drawing.Point(119, 23);
            this.tbSZKSZHS.Name = "tbSZKSZHS";
            this.tbSZKSZHS.Size = new System.Drawing.Size(84, 25);
            this.tbSZKSZHS.TabIndex = 66;
            this.tbSZKSZHS.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
            this.tbSZKSZHS.TextChanged += new System.EventHandler(this.tbSZKSZHS_TextChanged);
            this.tbSZKSZHS.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.tbSZKSZHS_KeyPress);
            this.tbSZKSZHS.Leave += new System.EventHandler(this.tbSZKSZHS_Leave);
            // 
            // label8
            // 
            this.label8.AutoSize = true;
            this.label8.Font = new System.Drawing.Font("宋体", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.label8.Location = new System.Drawing.Point(0, 26);
            this.label8.Name = "label8";
            this.label8.Size = new System.Drawing.Size(149, 20);
            this.label8.TabIndex = 65;
            this.label8.Text = "擅自开设账户数";
            // 
            // groupBox3
            // 
            this.groupBox3.Controls.Add(this.tbGFSXBS);
            this.groupBox3.Controls.Add(this.tbGFSXJE);
            this.groupBox3.Controls.Add(this.label9);
            this.groupBox3.Controls.Add(this.label10);
            this.groupBox3.Font = new System.Drawing.Font("宋体", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.groupBox3.Location = new System.Drawing.Point(1056, 52);
            this.groupBox3.Name = "groupBox3";
            this.groupBox3.Size = new System.Drawing.Size(187, 105);
            this.groupBox3.TabIndex = 73;
            this.groupBox3.TabStop = false;
            this.groupBox3.Text = "规范资金存储或借垫手续";
            // 
            // tbGFSXBS
            // 
            this.tbGFSXBS.Font = new System.Drawing.Font("宋体", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.tbGFSXBS.Location = new System.Drawing.Point(106, 22);
            this.tbGFSXBS.Name = "tbGFSXBS";
            this.tbGFSXBS.Size = new System.Drawing.Size(76, 25);
            this.tbGFSXBS.TabIndex = 86;
            this.tbGFSXBS.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
            this.tbGFSXBS.TextChanged += new System.EventHandler(this.tbGFSXBS_TextChanged);
            this.tbGFSXBS.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.tbGFSXBS_KeyPress);
            this.tbGFSXBS.Leave += new System.EventHandler(this.tbGFSXBS_Leave);
            // 
            // tbGFSXJE
            // 
            this.tbGFSXJE.Font = new System.Drawing.Font("宋体", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.tbGFSXJE.Location = new System.Drawing.Point(106, 64);
            this.tbGFSXJE.Name = "tbGFSXJE";
            this.tbGFSXJE.Size = new System.Drawing.Size(76, 25);
            this.tbGFSXJE.TabIndex = 88;
            this.tbGFSXJE.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
            this.tbGFSXJE.TextChanged += new System.EventHandler(this.tbGFSXJE_TextChanged);
            this.tbGFSXJE.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.tbGFSXJE_KeyPress);
            this.tbGFSXJE.Leave += new System.EventHandler(this.tbGFSXJE_Leave);
            // 
            // label9
            // 
            this.label9.AutoSize = true;
            this.label9.Font = new System.Drawing.Font("宋体", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.label9.Location = new System.Drawing.Point(2, 66);
            this.label9.Name = "label9";
            this.label9.Size = new System.Drawing.Size(129, 20);
            this.label9.TabIndex = 73;
            this.label9.Text = "规范手续金额";
            // 
            // label10
            // 
            this.label10.AutoSize = true;
            this.label10.Font = new System.Drawing.Font("宋体", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.label10.Location = new System.Drawing.Point(2, 24);
            this.label10.Name = "label10";
            this.label10.Size = new System.Drawing.Size(129, 20);
            this.label10.TabIndex = 69;
            this.label10.Text = "规范手续笔数";
            // 
            // groupBox2
            // 
            this.groupBox2.Controls.Add(this.tbWGCCJE);
            this.groupBox2.Controls.Add(this.tbQTYYJE);
            this.groupBox2.Controls.Add(this.tbQTYYBS);
            this.groupBox2.Controls.Add(this.label11);
            this.groupBox2.Controls.Add(this.label6);
            this.groupBox2.Controls.Add(this.tbWGCCBS);
            this.groupBox2.Controls.Add(this.label7);
            this.groupBox2.Controls.Add(this.tbWGJDBS);
            this.groupBox2.Controls.Add(this.label12);
            this.groupBox2.Controls.Add(this.tbWGJDJE);
            this.groupBox2.Controls.Add(this.label4);
            this.groupBox2.Controls.Add(this.label5);
            this.groupBox2.Controls.Add(this.tbSJFK);
            this.groupBox2.Controls.Add(this.label16);
            this.groupBox2.Font = new System.Drawing.Font("宋体", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.groupBox2.Location = new System.Drawing.Point(424, 52);
            this.groupBox2.Name = "groupBox2";
            this.groupBox2.Size = new System.Drawing.Size(628, 103);
            this.groupBox2.TabIndex = 72;
            this.groupBox2.TabStop = false;
            this.groupBox2.Text = "收拢资金";
            // 
            // tbWGCCJE
            // 
            this.tbWGCCJE.Font = new System.Drawing.Font("宋体", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.tbWGCCJE.Location = new System.Drawing.Point(100, 63);
            this.tbWGCCJE.Name = "tbWGCCJE";
            this.tbWGCCJE.Size = new System.Drawing.Size(70, 25);
            this.tbWGCCJE.TabIndex = 76;
            this.tbWGCCJE.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
            this.tbWGCCJE.TextChanged += new System.EventHandler(this.tbWGCCJE_TextChanged);
            this.tbWGCCJE.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.tbWGCCJE_KeyPress);
            this.tbWGCCJE.Leave += new System.EventHandler(this.tbWGCCJE_Leave);
            // 
            // tbQTYYJE
            // 
            this.tbQTYYJE.Font = new System.Drawing.Font("宋体", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.tbQTYYJE.Location = new System.Drawing.Point(410, 63);
            this.tbQTYYJE.Name = "tbQTYYJE";
            this.tbQTYYJE.Size = new System.Drawing.Size(70, 25);
            this.tbQTYYJE.TabIndex = 84;
            this.tbQTYYJE.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
            this.tbQTYYJE.TextChanged += new System.EventHandler(this.tbQTYYJE_TextChanged);
            this.tbQTYYJE.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.tbQTYYJE_KeyPress);
            this.tbQTYYJE.Leave += new System.EventHandler(this.tbQTYYJE_Leave);
            // 
            // tbQTYYBS
            // 
            this.tbQTYYBS.Font = new System.Drawing.Font("宋体", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.tbQTYYBS.Location = new System.Drawing.Point(410, 22);
            this.tbQTYYBS.Name = "tbQTYYBS";
            this.tbQTYYBS.Size = new System.Drawing.Size(70, 25);
            this.tbQTYYBS.TabIndex = 82;
            this.tbQTYYBS.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
            this.tbQTYYBS.TextChanged += new System.EventHandler(this.tbQTYYBS_TextChanged);
            this.tbQTYYBS.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.tbQTYYBS_KeyPress);
            this.tbQTYYBS.Leave += new System.EventHandler(this.tbQTYYBS_Leave);
            // 
            // label11
            // 
            this.label11.AutoSize = true;
            this.label11.Font = new System.Drawing.Font("宋体", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.label11.Location = new System.Drawing.Point(343, 66);
            this.label11.Name = "label11";
            this.label11.Size = new System.Drawing.Size(89, 20);
            this.label11.TabIndex = 77;
            this.label11.Text = "没收利息";
            // 
            // label6
            // 
            this.label6.AutoSize = true;
            this.label6.Font = new System.Drawing.Font("宋体", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.label6.Location = new System.Drawing.Point(1, 66);
            this.label6.Name = "label6";
            this.label6.Size = new System.Drawing.Size(129, 20);
            this.label6.TabIndex = 67;
            this.label6.Text = "违规存储金额";
            // 
            // tbWGCCBS
            // 
            this.tbWGCCBS.Font = new System.Drawing.Font("宋体", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.tbWGCCBS.Location = new System.Drawing.Point(100, 22);
            this.tbWGCCBS.Name = "tbWGCCBS";
            this.tbWGCCBS.Size = new System.Drawing.Size(70, 25);
            this.tbWGCCBS.TabIndex = 74;
            this.tbWGCCBS.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
            this.tbWGCCBS.TextChanged += new System.EventHandler(this.tbWGCCBS_TextChanged);
            this.tbWGCCBS.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.tbWGCCBS_KeyPress);
            this.tbWGCCBS.Leave += new System.EventHandler(this.tbWGCCBS_Leave);
            // 
            // label7
            // 
            this.label7.AutoSize = true;
            this.label7.Font = new System.Drawing.Font("宋体", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.label7.Location = new System.Drawing.Point(1, 24);
            this.label7.Name = "label7";
            this.label7.Size = new System.Drawing.Size(129, 20);
            this.label7.TabIndex = 65;
            this.label7.Text = "违规存储笔数";
            // 
            // tbWGJDBS
            // 
            this.tbWGJDBS.Font = new System.Drawing.Font("宋体", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.tbWGJDBS.Location = new System.Drawing.Point(271, 22);
            this.tbWGJDBS.Name = "tbWGJDBS";
            this.tbWGJDBS.Size = new System.Drawing.Size(70, 25);
            this.tbWGJDBS.TabIndex = 78;
            this.tbWGJDBS.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
            this.tbWGJDBS.TextChanged += new System.EventHandler(this.tbWGJDBS_TextChanged);
            this.tbWGJDBS.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.tbWGJDBS_KeyPress);
            this.tbWGJDBS.Leave += new System.EventHandler(this.tbWGJDBS_Leave);
            // 
            // label12
            // 
            this.label12.AutoSize = true;
            this.label12.Font = new System.Drawing.Font("宋体", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.label12.Location = new System.Drawing.Point(343, 24);
            this.label12.Name = "label12";
            this.label12.Size = new System.Drawing.Size(89, 20);
            this.label12.TabIndex = 74;
            this.label12.Text = "处罚笔数";
            // 
            // tbWGJDJE
            // 
            this.tbWGJDJE.Font = new System.Drawing.Font("宋体", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.tbWGJDJE.Location = new System.Drawing.Point(271, 63);
            this.tbWGJDJE.Name = "tbWGJDJE";
            this.tbWGJDJE.Size = new System.Drawing.Size(70, 25);
            this.tbWGJDJE.TabIndex = 80;
            this.tbWGJDJE.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
            this.tbWGJDJE.TextChanged += new System.EventHandler(this.tbWGJDJE_TextChanged);
            this.tbWGJDJE.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.tbWGJDJE_KeyPress);
            this.tbWGJDJE.Leave += new System.EventHandler(this.tbWGJDJE_Leave);
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Font = new System.Drawing.Font("宋体", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.label4.Location = new System.Drawing.Point(171, 66);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(129, 20);
            this.label4.TabIndex = 73;
            this.label4.Text = "违规借垫金额";
            // 
            // label5
            // 
            this.label5.AutoSize = true;
            this.label5.Font = new System.Drawing.Font("宋体", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.label5.Location = new System.Drawing.Point(171, 24);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(129, 20);
            this.label5.TabIndex = 69;
            this.label5.Text = "违规借垫笔数";
            // 
            // tbSJFK
            // 
            this.tbSJFK.Font = new System.Drawing.Font("宋体", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.tbSJFK.Location = new System.Drawing.Point(550, 63);
            this.tbSJFK.Name = "tbSJFK";
            this.tbSJFK.Size = new System.Drawing.Size(70, 25);
            this.tbSJFK.TabIndex = 86;
            this.tbSJFK.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
            this.tbSJFK.TextChanged += new System.EventHandler(this.tbSJFK_TextChanged);
            this.tbSJFK.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.tbSJFK_KeyPress);
            this.tbSJFK.Leave += new System.EventHandler(this.tbSJFK_Leave);
            // 
            // label16
            // 
            this.label16.AutoSize = true;
            this.label16.Font = new System.Drawing.Font("宋体", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.label16.Location = new System.Drawing.Point(481, 66);
            this.label16.Name = "label16";
            this.label16.Size = new System.Drawing.Size(89, 20);
            this.label16.TabIndex = 85;
            this.label16.Text = "上交罚款";
            // 
            // comboDWDM
            // 
            this.comboDWDM.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.comboDWDM.Font = new System.Drawing.Font("宋体", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.comboDWDM.FormattingEnabled = true;
            this.comboDWDM.Location = new System.Drawing.Point(110, 26);
            this.comboDWDM.Name = "comboDWDM";
            this.comboDWDM.Size = new System.Drawing.Size(261, 23);
            this.comboDWDM.TabIndex = 64;
            // 
            // label
            // 
            this.label.AutoSize = true;
            this.label.Font = new System.Drawing.Font("宋体", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.label.Location = new System.Drawing.Point(17, 28);
            this.label.Name = "label";
            this.label.Size = new System.Drawing.Size(89, 20);
            this.label.TabIndex = 70;
            this.label.Text = "单位名称";
            // 
            // label15
            // 
            this.label15.AutoSize = true;
            this.label15.Font = new System.Drawing.Font("宋体", 21.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.label15.Location = new System.Drawing.Point(356, 16);
            this.label15.Name = "label15";
            this.label15.Size = new System.Drawing.Size(625, 37);
            this.label15.TabIndex = 53;
            this.label15.Text = "银行账户和资金清查处理情况统计表";
            // 
            // btnFilter
            // 
            this.btnFilter.Font = new System.Drawing.Font("宋体", 10.5F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.btnFilter.Location = new System.Drawing.Point(1013, 20);
            this.btnFilter.Name = "btnFilter";
            this.btnFilter.Size = new System.Drawing.Size(108, 33);
            this.btnFilter.TabIndex = 80;
            this.btnFilter.Text = "查  询";
            this.btnFilter.UseVisualStyleBackColor = true;
            this.btnFilter.Click += new System.EventHandler(this.btnFilter_Click);
            // 
            // label13
            // 
            this.label13.AutoSize = true;
            this.label13.Font = new System.Drawing.Font("宋体", 10.5F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.label13.Location = new System.Drawing.Point(14, 64);
            this.label13.Name = "label13";
            this.label13.Size = new System.Drawing.Size(341, 18);
            this.label13.TabIndex = 74;
            this.label13.Text = "统计日期：2018年7月1日至2019年6月30日";
            // 
            // label14
            // 
            this.label14.AutoSize = true;
            this.label14.Font = new System.Drawing.Font("宋体", 10.5F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.label14.ForeColor = System.Drawing.Color.Red;
            this.label14.Location = new System.Drawing.Point(1347, 64);
            this.label14.Name = "label14";
            this.label14.Size = new System.Drawing.Size(134, 18);
            this.label14.TabIndex = 81;
            this.label14.Text = "金额单位：万元";
            // 
            // label22
            // 
            this.label22.AutoSize = true;
            this.label22.Location = new System.Drawing.Point(16, 584);
            this.label22.Name = "label22";
            this.label22.Size = new System.Drawing.Size(485, 18);
            this.label22.TabIndex = 82;
            this.label22.Text = "填报说明：此表统计区间为2018年7月1日至2019年6月30日。";
            // 
            // label18
            // 
            this.label18.AutoSize = true;
            this.label18.BackColor = System.Drawing.Color.Transparent;
            this.label18.Font = new System.Drawing.Font("宋体", 10.5F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.label18.ForeColor = System.Drawing.Color.Red;
            this.label18.Location = new System.Drawing.Point(1152, 64);
            this.label18.Name = "label18";
            this.label18.Size = new System.Drawing.Size(134, 18);
            this.label18.TabIndex = 83;
            this.label18.Text = "金额单位：万元";
            // 
            // FrmBankAccountCheck
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(9F, 17F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(211)))), ((int)(((byte)(228)))), ((int)(((byte)(252)))));
            this.ClientSize = new System.Drawing.Size(1272, 638);
            this.Controls.Add(this.label18);
            this.Controls.Add(this.label22);
            this.Controls.Add(this.label14);
            this.Controls.Add(this.label13);
            this.Controls.Add(this.btnFilter);
            this.Controls.Add(this.label15);
            this.Controls.Add(this.groupBox);
            this.Controls.Add(this.groupBoxInfo);
            this.Controls.Add(this.dataGridView);
            this.Font = new System.Drawing.Font("宋体", 10.5F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.KeyPreview = true;
            this.Name = "FrmBankAccountCheck";
            this.Text = "账户资金清理检查处理情况统计";
            this.WindowState = System.Windows.Forms.FormWindowState.Maximized;
            this.FormClosing += new System.Windows.Forms.FormClosingEventHandler(this.FrmBankAccountCheck_FormClosing);
            this.KeyDown += new System.Windows.Forms.KeyEventHandler(this.FrmBankAccountCheck_KeyDown);
            ((System.ComponentModel.ISupportInitialize)(this.dataGridView)).EndInit();
            this.groupBox.ResumeLayout(false);
            this.groupBoxInfo.ResumeLayout(false);
            this.groupBoxInfo.PerformLayout();
            this.groupBox1.ResumeLayout(false);
            this.groupBox1.PerformLayout();
            this.groupBox3.ResumeLayout(false);
            this.groupBox3.PerformLayout();
            this.groupBox2.ResumeLayout(false);
            this.groupBox2.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.DataGridView dataGridView;
        private System.Windows.Forms.GroupBox groupBox;
        private System.Windows.Forms.Button btnExit;
        private System.Windows.Forms.Button btnSave;
        private System.Windows.Forms.Button btnRemove;
        private System.Windows.Forms.Button btnAdd;
        private System.Windows.Forms.GroupBox groupBoxInfo;
        private System.Windows.Forms.GroupBox groupBox1;
        private System.Windows.Forms.TextBox tbSZKSZHS;
        private System.Windows.Forms.Label label8;
        private System.Windows.Forms.TextBox tbYQZHS;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.TextBox tbQTWGHS;
        private System.Windows.Forms.TextBox tbYCWCZHS;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.GroupBox groupBox2;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.TextBox tbWGJDJE;
        private System.Windows.Forms.TextBox tbWGJDBS;
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.TextBox tbWGCCJE;
        private System.Windows.Forms.Label label6;
        private System.Windows.Forms.TextBox tbWGCCBS;
        private System.Windows.Forms.Label label7;
        private System.Windows.Forms.ComboBox comboDWDM;
        private System.Windows.Forms.Label label;
        private System.Windows.Forms.GroupBox groupBox3;
        private System.Windows.Forms.Label label9;
        private System.Windows.Forms.TextBox tbGFSXJE;
        private System.Windows.Forms.TextBox tbGFSXBS;
        private System.Windows.Forms.Label label10;
        private System.Windows.Forms.Label label11;
        private System.Windows.Forms.TextBox tbQTYYJE;
        private System.Windows.Forms.TextBox tbQTYYBS;
        private System.Windows.Forms.Label label12;
        private System.Windows.Forms.Label label15;
        private System.Windows.Forms.Button btnExcel;
        private System.Windows.Forms.Button btnFilter;
        private System.Windows.Forms.Label label13;
        private System.Windows.Forms.Label label14;
        private System.Windows.Forms.TextBox tbSJFK;
        private System.Windows.Forms.Label label16;
        private System.Windows.Forms.Label label22;
        private System.Windows.Forms.Label label18;
    }
}