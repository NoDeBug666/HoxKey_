namespace HoxKey
{
    partial class CustomMenu
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
            this.ScriptList = new System.Windows.Forms.ListBox();
            this.splitter1 = new System.Windows.Forms.Splitter();
            this.Script = new System.Windows.Forms.ListBox();
            this.label1 = new System.Windows.Forms.Label();
            this.LoadScript_btn = new System.Windows.Forms.Button();
            this.Sure_Btn = new System.Windows.Forms.Button();
            this.Remove_btn = new System.Windows.Forms.Button();
            this.ScriptName_tb = new System.Windows.Forms.TextBox();
            this.label2 = new System.Windows.Forms.Label();
            this.Rename_btn = new System.Windows.Forms.Button();
            this.SaveScript_Btn = new System.Windows.Forms.Button();
            this.CommandList = new System.Windows.Forms.ListBox();
            this.panel1 = new System.Windows.Forms.Panel();
            this.StartRecord_Btn = new System.Windows.Forms.Button();
            this.AddScript_Btn = new System.Windows.Forms.Button();
            this.RemoveScript_Btn = new System.Windows.Forms.Button();
            this.StatusStrip = new System.Windows.Forms.StatusStrip();
            this.toolStripStatusLabel1 = new System.Windows.Forms.ToolStripStatusLabel();
            this.toolStripStatusLabel2 = new System.Windows.Forms.ToolStripStatusLabel();
            this.StopRecording_btn = new System.Windows.Forms.Button();
            this.menuStrip1 = new System.Windows.Forms.MenuStrip();
            this.腳本ToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.優化ToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.滑鼠移動整合ToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.StatusStrip.SuspendLayout();
            this.menuStrip1.SuspendLayout();
            this.SuspendLayout();
            // 
            // ScriptList
            // 
            this.ScriptList.FormattingEnabled = true;
            this.ScriptList.ItemHeight = 16;
            this.ScriptList.Location = new System.Drawing.Point(15, 29);
            this.ScriptList.Margin = new System.Windows.Forms.Padding(4);
            this.ScriptList.Name = "ScriptList";
            this.ScriptList.Size = new System.Drawing.Size(225, 468);
            this.ScriptList.TabIndex = 0;
            // 
            // splitter1
            // 
            this.splitter1.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(224)))), ((int)(((byte)(224)))), ((int)(((byte)(224)))));
            this.splitter1.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.splitter1.Location = new System.Drawing.Point(0, 0);
            this.splitter1.Name = "splitter1";
            this.splitter1.Size = new System.Drawing.Size(255, 630);
            this.splitter1.TabIndex = 1;
            this.splitter1.TabStop = false;
            // 
            // Script
            // 
            this.Script.FormattingEnabled = true;
            this.Script.ItemHeight = 16;
            this.Script.Location = new System.Drawing.Point(432, 74);
            this.Script.Margin = new System.Windows.Forms.Padding(4);
            this.Script.Name = "Script";
            this.Script.SelectionMode = System.Windows.Forms.SelectionMode.MultiExtended;
            this.Script.Size = new System.Drawing.Size(326, 420);
            this.Script.TabIndex = 2;
            this.Script.SelectedIndexChanged += new System.EventHandler(this.Script_SelectedIndexChanged);
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(12, 9);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(72, 16);
            this.label1.TabIndex = 3;
            this.label1.Text = "腳本集合";
            // 
            // LoadScript_btn
            // 
            this.LoadScript_btn.Location = new System.Drawing.Point(15, 505);
            this.LoadScript_btn.Name = "LoadScript_btn";
            this.LoadScript_btn.Size = new System.Drawing.Size(225, 30);
            this.LoadScript_btn.TabIndex = 4;
            this.LoadScript_btn.Text = "套用";
            this.LoadScript_btn.UseVisualStyleBackColor = true;
            this.LoadScript_btn.Click += new System.EventHandler(this.LoadScript_btn_Click);
            // 
            // Sure_Btn
            // 
            this.Sure_Btn.Location = new System.Drawing.Point(648, 539);
            this.Sure_Btn.Name = "Sure_Btn";
            this.Sure_Btn.Size = new System.Drawing.Size(110, 45);
            this.Sure_Btn.TabIndex = 5;
            this.Sure_Btn.Text = "確定";
            this.Sure_Btn.UseVisualStyleBackColor = true;
            this.Sure_Btn.Click += new System.EventHandler(this.Sure_Btn_Click);
            // 
            // Remove_btn
            // 
            this.Remove_btn.Location = new System.Drawing.Point(15, 575);
            this.Remove_btn.Name = "Remove_btn";
            this.Remove_btn.Size = new System.Drawing.Size(225, 30);
            this.Remove_btn.TabIndex = 6;
            this.Remove_btn.Text = "刪除";
            this.Remove_btn.UseVisualStyleBackColor = true;
            this.Remove_btn.Click += new System.EventHandler(this.Remove_btn_Click);
            // 
            // ScriptName_tb
            // 
            this.ScriptName_tb.Location = new System.Drawing.Point(507, 40);
            this.ScriptName_tb.MaxLength = 255;
            this.ScriptName_tb.Name = "ScriptName_tb";
            this.ScriptName_tb.Size = new System.Drawing.Size(251, 27);
            this.ScriptName_tb.TabIndex = 7;
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(429, 43);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(72, 16);
            this.label2.TabIndex = 8;
            this.label2.Text = "腳本名稱";
            this.label2.Click += new System.EventHandler(this.label2_Click);
            // 
            // Rename_btn
            // 
            this.Rename_btn.Location = new System.Drawing.Point(15, 541);
            this.Rename_btn.Name = "Rename_btn";
            this.Rename_btn.Size = new System.Drawing.Size(225, 30);
            this.Rename_btn.TabIndex = 9;
            this.Rename_btn.Text = "重新命名";
            this.Rename_btn.UseVisualStyleBackColor = true;
            this.Rename_btn.Click += new System.EventHandler(this.Rename_btn_Click);
            // 
            // SaveScript_Btn
            // 
            this.SaveScript_Btn.Location = new System.Drawing.Point(532, 539);
            this.SaveScript_Btn.Name = "SaveScript_Btn";
            this.SaveScript_Btn.Size = new System.Drawing.Size(110, 45);
            this.SaveScript_Btn.TabIndex = 10;
            this.SaveScript_Btn.Text = "儲存此腳本";
            this.SaveScript_Btn.UseVisualStyleBackColor = true;
            this.SaveScript_Btn.Click += new System.EventHandler(this.SaveScript_Btn_Click);
            // 
            // CommandList
            // 
            this.CommandList.FormattingEnabled = true;
            this.CommandList.ItemHeight = 16;
            this.CommandList.Location = new System.Drawing.Point(273, 74);
            this.CommandList.Margin = new System.Windows.Forms.Padding(4);
            this.CommandList.Name = "CommandList";
            this.CommandList.Size = new System.Drawing.Size(151, 180);
            this.CommandList.TabIndex = 11;
            this.CommandList.SelectedIndexChanged += new System.EventHandler(this.CommandList_SelectedIndexChanged);
            // 
            // panel1
            // 
            this.panel1.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(224)))), ((int)(((byte)(224)))), ((int)(((byte)(224)))));
            this.panel1.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.panel1.Location = new System.Drawing.Point(273, 261);
            this.panel1.Name = "panel1";
            this.panel1.Size = new System.Drawing.Size(151, 263);
            this.panel1.TabIndex = 12;
            // 
            // StartRecord_Btn
            // 
            this.StartRecord_Btn.Location = new System.Drawing.Point(273, 539);
            this.StartRecord_Btn.Name = "StartRecord_Btn";
            this.StartRecord_Btn.Size = new System.Drawing.Size(121, 45);
            this.StartRecord_Btn.TabIndex = 13;
            this.StartRecord_Btn.Text = "開始錄製腳本\r\n(Ctrl + F12)";
            this.StartRecord_Btn.UseVisualStyleBackColor = true;
            this.StartRecord_Btn.Click += new System.EventHandler(this.StartRecord_Btn_Click);
            // 
            // AddScript_Btn
            // 
            this.AddScript_Btn.Location = new System.Drawing.Point(432, 496);
            this.AddScript_Btn.Name = "AddScript_Btn";
            this.AddScript_Btn.Size = new System.Drawing.Size(151, 28);
            this.AddScript_Btn.TabIndex = 0;
            this.AddScript_Btn.Text = "加入";
            this.AddScript_Btn.UseVisualStyleBackColor = true;
            this.AddScript_Btn.Click += new System.EventHandler(this.AddScript_Btn_Click);
            // 
            // RemoveScript_Btn
            // 
            this.RemoveScript_Btn.Location = new System.Drawing.Point(607, 496);
            this.RemoveScript_Btn.Name = "RemoveScript_Btn";
            this.RemoveScript_Btn.Size = new System.Drawing.Size(151, 28);
            this.RemoveScript_Btn.TabIndex = 14;
            this.RemoveScript_Btn.Text = "移除";
            this.RemoveScript_Btn.UseVisualStyleBackColor = true;
            this.RemoveScript_Btn.Click += new System.EventHandler(this.RemoveScript_Btn_Click);
            // 
            // StatusStrip
            // 
            this.StatusStrip.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.toolStripStatusLabel1,
            this.toolStripStatusLabel2});
            this.StatusStrip.Location = new System.Drawing.Point(255, 608);
            this.StatusStrip.Name = "StatusStrip";
            this.StatusStrip.Size = new System.Drawing.Size(523, 22);
            this.StatusStrip.TabIndex = 15;
            this.StatusStrip.Text = "statusStrip1";
            // 
            // toolStripStatusLabel1
            // 
            this.toolStripStatusLabel1.Name = "toolStripStatusLabel1";
            this.toolStripStatusLabel1.Size = new System.Drawing.Size(55, 17);
            this.toolStripStatusLabel1.Text = "自訂編輯";
            this.toolStripStatusLabel1.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // toolStripStatusLabel2
            // 
            this.toolStripStatusLabel2.Name = "toolStripStatusLabel2";
            this.toolStripStatusLabel2.Size = new System.Drawing.Size(453, 17);
            this.toolStripStatusLabel2.Spring = true;
            this.toolStripStatusLabel2.Text = "行數";
            this.toolStripStatusLabel2.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            this.toolStripStatusLabel2.TextDirection = System.Windows.Forms.ToolStripTextDirection.Horizontal;
            this.toolStripStatusLabel2.TextImageRelation = System.Windows.Forms.TextImageRelation.TextAboveImage;
            // 
            // StopRecording_btn
            // 
            this.StopRecording_btn.Location = new System.Drawing.Point(400, 539);
            this.StopRecording_btn.Name = "StopRecording_btn";
            this.StopRecording_btn.Size = new System.Drawing.Size(121, 45);
            this.StopRecording_btn.TabIndex = 16;
            this.StopRecording_btn.Text = "終止錄製\r\n(Ctrl + F11)\r\n";
            this.StopRecording_btn.UseVisualStyleBackColor = true;
            this.StopRecording_btn.Click += new System.EventHandler(this.StopRecording_btn_Click);
            // 
            // menuStrip1
            // 
            this.menuStrip1.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.腳本ToolStripMenuItem,
            this.優化ToolStripMenuItem});
            this.menuStrip1.Location = new System.Drawing.Point(255, 0);
            this.menuStrip1.Name = "menuStrip1";
            this.menuStrip1.Size = new System.Drawing.Size(523, 24);
            this.menuStrip1.TabIndex = 17;
            this.menuStrip1.Text = "menuStrip1";
            // 
            // 腳本ToolStripMenuItem
            // 
            this.腳本ToolStripMenuItem.Name = "腳本ToolStripMenuItem";
            this.腳本ToolStripMenuItem.Size = new System.Drawing.Size(43, 20);
            this.腳本ToolStripMenuItem.Text = "腳本";
            // 
            // 優化ToolStripMenuItem
            // 
            this.優化ToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.滑鼠移動整合ToolStripMenuItem});
            this.優化ToolStripMenuItem.Name = "優化ToolStripMenuItem";
            this.優化ToolStripMenuItem.Size = new System.Drawing.Size(43, 20);
            this.優化ToolStripMenuItem.Text = "優化";
            // 
            // 滑鼠移動整合ToolStripMenuItem
            // 
            this.滑鼠移動整合ToolStripMenuItem.Name = "滑鼠移動整合ToolStripMenuItem";
            this.滑鼠移動整合ToolStripMenuItem.Size = new System.Drawing.Size(152, 22);
            this.滑鼠移動整合ToolStripMenuItem.Text = "滑鼠移動整合";
            this.滑鼠移動整合ToolStripMenuItem.Click += new System.EventHandler(this.滑鼠移動整合ToolStripMenuItem_Click);
            // 
            // CustomMenu
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(9F, 16F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(778, 630);
            this.Controls.Add(this.StopRecording_btn);
            this.Controls.Add(this.StatusStrip);
            this.Controls.Add(this.menuStrip1);
            this.Controls.Add(this.RemoveScript_Btn);
            this.Controls.Add(this.AddScript_Btn);
            this.Controls.Add(this.StartRecord_Btn);
            this.Controls.Add(this.panel1);
            this.Controls.Add(this.CommandList);
            this.Controls.Add(this.SaveScript_Btn);
            this.Controls.Add(this.Rename_btn);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.ScriptName_tb);
            this.Controls.Add(this.Remove_btn);
            this.Controls.Add(this.Sure_Btn);
            this.Controls.Add(this.LoadScript_btn);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.Script);
            this.Controls.Add(this.ScriptList);
            this.Controls.Add(this.splitter1);
            this.Font = new System.Drawing.Font("新細明體", 12F);
            this.MainMenuStrip = this.menuStrip1;
            this.Margin = new System.Windows.Forms.Padding(4);
            this.Name = "CustomMenu";
            this.Text = "自定義腳本";
            this.FormClosing += new System.Windows.Forms.FormClosingEventHandler(this.CustomMenu_FormClosing);
            this.Load += new System.EventHandler(this.CustomMenu_Load);
            this.StatusStrip.ResumeLayout(false);
            this.StatusStrip.PerformLayout();
            this.menuStrip1.ResumeLayout(false);
            this.menuStrip1.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.ListBox ScriptList;
        private System.Windows.Forms.Splitter splitter1;
        private System.Windows.Forms.ListBox Script;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Button LoadScript_btn;
        private System.Windows.Forms.Button Sure_Btn;
        private System.Windows.Forms.Button Remove_btn;
        private System.Windows.Forms.TextBox ScriptName_tb;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Button Rename_btn;
        private System.Windows.Forms.Button SaveScript_Btn;
        private System.Windows.Forms.ListBox CommandList;
        private System.Windows.Forms.Panel panel1;
        private System.Windows.Forms.Button StartRecord_Btn;
        private System.Windows.Forms.Button AddScript_Btn;
        private System.Windows.Forms.Button RemoveScript_Btn;
        private System.Windows.Forms.StatusStrip StatusStrip;
        private System.Windows.Forms.ToolStripStatusLabel toolStripStatusLabel1;
        private System.Windows.Forms.ToolStripStatusLabel toolStripStatusLabel2;
        private System.Windows.Forms.Button StopRecording_btn;
        private System.Windows.Forms.MenuStrip menuStrip1;
        private System.Windows.Forms.ToolStripMenuItem 腳本ToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem 優化ToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem 滑鼠移動整合ToolStripMenuItem;
    }
}