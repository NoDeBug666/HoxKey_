namespace HoxKey
{
    partial class Form1
    {
        /// <summary>
        /// 設計工具所需的變數。
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// 清除任何使用中的資源。
        /// </summary>
        /// <param name="disposing">如果應該處置 Managed 資源則為 true，否則為 false。</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form 設計工具產生的程式碼

        /// <summary>
        /// 此為設計工具支援所需的方法 - 請勿使用程式碼編輯器修改
        /// 這個方法的內容。
        /// </summary>
        private void InitializeComponent()
        {
            this.KeepTimeNum = new System.Windows.Forms.NumericUpDown();
            this.label1 = new System.Windows.Forms.Label();
            this.ClickTestButton = new System.Windows.Forms.Button();
            this.label2 = new System.Windows.Forms.Label();
            this.ClickTestLabel = new System.Windows.Forms.Label();
            this.StartBtn = new System.Windows.Forms.Button();
            this.label4 = new System.Windows.Forms.Label();
            this.BeginDelayNum = new System.Windows.Forms.NumericUpDown();
            this.ClickClearButton = new System.Windows.Forms.Button();
            this.msgLabel = new System.Windows.Forms.Label();
            this.ShutDownBtn = new System.Windows.Forms.Button();
            this.button5 = new System.Windows.Forms.Button();
            this.label3 = new System.Windows.Forms.Label();
            this.HoldTimeNum = new System.Windows.Forms.NumericUpDown();
            this.label5 = new System.Windows.Forms.Label();
            this.ClickCountNum = new System.Windows.Forms.NumericUpDown();
            this.FixedModeButton = new System.Windows.Forms.RadioButton();
            this.TimeModeButton = new System.Windows.Forms.RadioButton();
            this.CustomModeBtn = new System.Windows.Forms.RadioButton();
            this.CustomMenuBtn = new System.Windows.Forms.Button();
            this.button1 = new System.Windows.Forms.Button();
            this.richTextBox1 = new System.Windows.Forms.RichTextBox();
            this.WorkPropertiesPanel = new System.Windows.Forms.Panel();
            ((System.ComponentModel.ISupportInitialize)(this.KeepTimeNum)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.BeginDelayNum)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.HoldTimeNum)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.ClickCountNum)).BeginInit();
            this.WorkPropertiesPanel.SuspendLayout();
            this.SuspendLayout();
            // 
            // KeepTimeNum
            // 
            this.KeepTimeNum.Location = new System.Drawing.Point(153, 20);
            this.KeepTimeNum.Maximum = new decimal(new int[] {
            1800,
            0,
            0,
            0});
            this.KeepTimeNum.Name = "KeepTimeNum";
            this.KeepTimeNum.Size = new System.Drawing.Size(94, 27);
            this.KeepTimeNum.TabIndex = 0;
            this.KeepTimeNum.Tag = "TimeMode";
            this.KeepTimeNum.Value = new decimal(new int[] {
            40,
            0,
            0,
            0});
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(3, 26);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(144, 16);
            this.label1.TabIndex = 1;
            this.label1.Tag = "TimeMode";
            this.label1.Text = "連點持續時間(秒):";
            // 
            // ClickTestButton
            // 
            this.ClickTestButton.BackColor = System.Drawing.SystemColors.ActiveBorder;
            this.ClickTestButton.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.ClickTestButton.Location = new System.Drawing.Point(282, 12);
            this.ClickTestButton.Name = "ClickTestButton";
            this.ClickTestButton.Size = new System.Drawing.Size(132, 95);
            this.ClickTestButton.TabIndex = 2;
            this.ClickTestButton.Text = "連點測試按鈕\r\n\r\n點我累計次數\r\n";
            this.ClickTestButton.UseVisualStyleBackColor = false;
            this.ClickTestButton.Click += new System.EventHandler(this.ClickTestButton_Click);
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(280, 110);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(88, 16);
            this.label2.TabIndex = 3;
            this.label2.Text = "已點擊次數";
            // 
            // ClickTestLabel
            // 
            this.ClickTestLabel.AutoSize = true;
            this.ClickTestLabel.Location = new System.Drawing.Point(374, 110);
            this.ClickTestLabel.Name = "ClickTestLabel";
            this.ClickTestLabel.Size = new System.Drawing.Size(16, 16);
            this.ClickTestLabel.TabIndex = 4;
            this.ClickTestLabel.Text = "0";
            // 
            // StartBtn
            // 
            this.StartBtn.BackColor = System.Drawing.Color.Red;
            this.StartBtn.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.StartBtn.Location = new System.Drawing.Point(287, 244);
            this.StartBtn.Name = "StartBtn";
            this.StartBtn.Size = new System.Drawing.Size(127, 78);
            this.StartBtn.TabIndex = 5;
            this.StartBtn.Text = "開始\r\n(Ctrl + F12)";
            this.StartBtn.UseVisualStyleBackColor = false;
            this.StartBtn.Click += new System.EventHandler(this.StartBtn_Click);
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Location = new System.Drawing.Point(3, 204);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(144, 16);
            this.label4.TabIndex = 7;
            this.label4.Tag = "Common";
            this.label4.Text = "開始延遲時間(秒):";
            // 
            // BeginDelayNum
            // 
            this.BeginDelayNum.Location = new System.Drawing.Point(153, 198);
            this.BeginDelayNum.Maximum = new decimal(new int[] {
            60,
            0,
            0,
            0});
            this.BeginDelayNum.Name = "BeginDelayNum";
            this.BeginDelayNum.Size = new System.Drawing.Size(94, 27);
            this.BeginDelayNum.TabIndex = 6;
            this.BeginDelayNum.Tag = "Common";
            this.BeginDelayNum.Value = new decimal(new int[] {
            3,
            0,
            0,
            0});
            // 
            // ClickClearButton
            // 
            this.ClickClearButton.BackColor = System.Drawing.SystemColors.ActiveBorder;
            this.ClickClearButton.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.ClickClearButton.Location = new System.Drawing.Point(282, 142);
            this.ClickClearButton.Name = "ClickClearButton";
            this.ClickClearButton.Size = new System.Drawing.Size(132, 28);
            this.ClickClearButton.TabIndex = 8;
            this.ClickClearButton.Text = "清除";
            this.ClickClearButton.UseVisualStyleBackColor = false;
            this.ClickClearButton.Click += new System.EventHandler(this.ClickClearButton_Click);
            // 
            // msgLabel
            // 
            this.msgLabel.AutoSize = true;
            this.msgLabel.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(128)))), ((int)(((byte)(255)))), ((int)(((byte)(128)))));
            this.msgLabel.Location = new System.Drawing.Point(284, 220);
            this.msgLabel.Name = "msgLabel";
            this.msgLabel.Size = new System.Drawing.Size(56, 16);
            this.msgLabel.TabIndex = 9;
            this.msgLabel.Text = "待命中";
            // 
            // ShutDownBtn
            // 
            this.ShutDownBtn.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(128)))), ((int)(((byte)(128)))), ((int)(((byte)(255)))));
            this.ShutDownBtn.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.ShutDownBtn.Location = new System.Drawing.Point(11, 244);
            this.ShutDownBtn.Name = "ShutDownBtn";
            this.ShutDownBtn.Size = new System.Drawing.Size(244, 77);
            this.ShutDownBtn.TabIndex = 12;
            this.ShutDownBtn.Text = "強制中斷\r\n(Ctrl + F11)";
            this.ShutDownBtn.UseVisualStyleBackColor = false;
            this.ShutDownBtn.Click += new System.EventHandler(this.ShutDownBtn_Click);
            // 
            // button5
            // 
            this.button5.BackColor = System.Drawing.Color.White;
            this.button5.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.button5.Location = new System.Drawing.Point(11, 328);
            this.button5.Name = "button5";
            this.button5.Size = new System.Drawing.Size(402, 31);
            this.button5.TabIndex = 13;
            this.button5.Text = "關閉程式";
            this.button5.UseVisualStyleBackColor = false;
            this.button5.Click += new System.EventHandler(this.button5_Click);
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(3, 170);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(136, 16);
            this.label3.TabIndex = 15;
            this.label3.Tag = "Common";
            this.label3.Text = "按住彈起延遲(ms)";
            // 
            // HoldTimeNum
            // 
            this.HoldTimeNum.Location = new System.Drawing.Point(153, 165);
            this.HoldTimeNum.Maximum = new decimal(new int[] {
            1000,
            0,
            0,
            0});
            this.HoldTimeNum.Minimum = new decimal(new int[] {
            1,
            0,
            0,
            0});
            this.HoldTimeNum.Name = "HoldTimeNum";
            this.HoldTimeNum.Size = new System.Drawing.Size(94, 27);
            this.HoldTimeNum.TabIndex = 14;
            this.HoldTimeNum.Tag = "Common";
            this.HoldTimeNum.Value = new decimal(new int[] {
            5,
            0,
            0,
            0});
            // 
            // label5
            // 
            this.label5.AutoSize = true;
            this.label5.Location = new System.Drawing.Point(3, 80);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(104, 16);
            this.label5.TabIndex = 17;
            this.label5.Tag = "FixedMode";
            this.label5.Text = "固定點擊次數";
            // 
            // ClickCountNum
            // 
            this.ClickCountNum.Location = new System.Drawing.Point(153, 75);
            this.ClickCountNum.Maximum = new decimal(new int[] {
            10000,
            0,
            0,
            0});
            this.ClickCountNum.Minimum = new decimal(new int[] {
            1,
            0,
            0,
            0});
            this.ClickCountNum.Name = "ClickCountNum";
            this.ClickCountNum.Size = new System.Drawing.Size(94, 27);
            this.ClickCountNum.TabIndex = 16;
            this.ClickCountNum.Tag = "FixedMode";
            this.ClickCountNum.Value = new decimal(new int[] {
            100,
            0,
            0,
            0});
            // 
            // FixedModeButton
            // 
            this.FixedModeButton.AutoSize = true;
            this.FixedModeButton.Location = new System.Drawing.Point(3, 54);
            this.FixedModeButton.Name = "FixedModeButton";
            this.FixedModeButton.Size = new System.Drawing.Size(122, 20);
            this.FixedModeButton.TabIndex = 18;
            this.FixedModeButton.Text = "固定次數模式";
            this.FixedModeButton.UseVisualStyleBackColor = true;
            this.FixedModeButton.CheckedChanged += new System.EventHandler(this.radioButton1_CheckedChanged);
            // 
            // TimeModeButton
            // 
            this.TimeModeButton.AutoSize = true;
            this.TimeModeButton.Checked = true;
            this.TimeModeButton.Location = new System.Drawing.Point(3, 3);
            this.TimeModeButton.Name = "TimeModeButton";
            this.TimeModeButton.Size = new System.Drawing.Size(90, 20);
            this.TimeModeButton.TabIndex = 19;
            this.TimeModeButton.TabStop = true;
            this.TimeModeButton.Text = "時間模式";
            this.TimeModeButton.UseVisualStyleBackColor = true;
            this.TimeModeButton.CheckedChanged += new System.EventHandler(this.radioButton2_CheckedChanged);
            // 
            // CustomModeBtn
            // 
            this.CustomModeBtn.AutoSize = true;
            this.CustomModeBtn.Location = new System.Drawing.Point(2, 103);
            this.CustomModeBtn.Name = "CustomModeBtn";
            this.CustomModeBtn.Size = new System.Drawing.Size(122, 20);
            this.CustomModeBtn.TabIndex = 20;
            this.CustomModeBtn.Text = "自訂腳本模式";
            this.CustomModeBtn.UseVisualStyleBackColor = true;
            // 
            // CustomMenuBtn
            // 
            this.CustomMenuBtn.Location = new System.Drawing.Point(15, 129);
            this.CustomMenuBtn.Name = "CustomMenuBtn";
            this.CustomMenuBtn.Size = new System.Drawing.Size(231, 30);
            this.CustomMenuBtn.TabIndex = 21;
            this.CustomMenuBtn.Tag = "CustomMode";
            this.CustomMenuBtn.Text = "選用腳本";
            this.CustomMenuBtn.UseVisualStyleBackColor = true;
            this.CustomMenuBtn.Click += new System.EventHandler(this.CustomMenuBtn_Click);
            // 
            // button1
            // 
            this.button1.Location = new System.Drawing.Point(459, 26);
            this.button1.Name = "button1";
            this.button1.Size = new System.Drawing.Size(142, 22);
            this.button1.TabIndex = 22;
            this.button1.Text = "Test Key";
            this.button1.UseVisualStyleBackColor = true;
            this.button1.Click += new System.EventHandler(this.button1_Click);
            // 
            // richTextBox1
            // 
            this.richTextBox1.Location = new System.Drawing.Point(433, 54);
            this.richTextBox1.Name = "richTextBox1";
            this.richTextBox1.Size = new System.Drawing.Size(187, 255);
            this.richTextBox1.TabIndex = 23;
            this.richTextBox1.Text = "";
            // 
            // WorkPropertiesPanel
            // 
            this.WorkPropertiesPanel.Controls.Add(this.TimeModeButton);
            this.WorkPropertiesPanel.Controls.Add(this.KeepTimeNum);
            this.WorkPropertiesPanel.Controls.Add(this.label1);
            this.WorkPropertiesPanel.Controls.Add(this.CustomMenuBtn);
            this.WorkPropertiesPanel.Controls.Add(this.BeginDelayNum);
            this.WorkPropertiesPanel.Controls.Add(this.CustomModeBtn);
            this.WorkPropertiesPanel.Controls.Add(this.label4);
            this.WorkPropertiesPanel.Controls.Add(this.HoldTimeNum);
            this.WorkPropertiesPanel.Controls.Add(this.FixedModeButton);
            this.WorkPropertiesPanel.Controls.Add(this.label3);
            this.WorkPropertiesPanel.Controls.Add(this.label5);
            this.WorkPropertiesPanel.Controls.Add(this.ClickCountNum);
            this.WorkPropertiesPanel.Location = new System.Drawing.Point(11, 8);
            this.WorkPropertiesPanel.Name = "WorkPropertiesPanel";
            this.WorkPropertiesPanel.Size = new System.Drawing.Size(263, 228);
            this.WorkPropertiesPanel.TabIndex = 24;
            // 
            // Form1
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 16F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(631, 371);
            this.Controls.Add(this.WorkPropertiesPanel);
            this.Controls.Add(this.richTextBox1);
            this.Controls.Add(this.button1);
            this.Controls.Add(this.button5);
            this.Controls.Add(this.ShutDownBtn);
            this.Controls.Add(this.msgLabel);
            this.Controls.Add(this.ClickClearButton);
            this.Controls.Add(this.StartBtn);
            this.Controls.Add(this.ClickTestLabel);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.ClickTestButton);
            this.Font = new System.Drawing.Font("細明體", 12F);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.None;
            this.Margin = new System.Windows.Forms.Padding(4);
            this.Name = "Form1";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "HoxKey";
            this.FormClosing += new System.Windows.Forms.FormClosingEventHandler(this.Form1_FormClosing);
            this.Load += new System.EventHandler(this.Form1_Load);
            ((System.ComponentModel.ISupportInitialize)(this.KeepTimeNum)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.BeginDelayNum)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.HoldTimeNum)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.ClickCountNum)).EndInit();
            this.WorkPropertiesPanel.ResumeLayout(false);
            this.WorkPropertiesPanel.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.NumericUpDown KeepTimeNum;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Button ClickTestButton;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Label ClickTestLabel;
        private System.Windows.Forms.Button StartBtn;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.NumericUpDown BeginDelayNum;
        private System.Windows.Forms.Button ClickClearButton;
        private System.Windows.Forms.Label msgLabel;
        private System.Windows.Forms.Button ShutDownBtn;
        private System.Windows.Forms.Button button5;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.NumericUpDown HoldTimeNum;
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.NumericUpDown ClickCountNum;
        private System.Windows.Forms.RadioButton FixedModeButton;
        private System.Windows.Forms.RadioButton TimeModeButton;
        private System.Windows.Forms.RadioButton CustomModeBtn;
        private System.Windows.Forms.Button CustomMenuBtn;
        private System.Windows.Forms.Button button1;
        private System.Windows.Forms.RichTextBox richTextBox1;
        private System.Windows.Forms.Panel WorkPropertiesPanel;
    }
}

