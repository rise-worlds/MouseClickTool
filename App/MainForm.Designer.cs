namespace MouseClickTool
{
    partial class MainForm
    {
        private System.ComponentModel.IContainer components = null;

        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        private void InitializeComponent()
        {
            dvl = new Label();
            tvl = new Label();
            bh = new Button();
            hkl = new Label();
            wb1 = new Label();
            ct = new ComboBox();
            hk = new ComboBox();
            pk = new DateTimePicker();
            dv = new TextBox();
            pv = new TextBox();
            bs = new Button();
            SuspendLayout();
            // 
            // dvl
            // 
            dvl.AutoSize = true;
            dvl.Location = new Point(12, 9);
            dvl.Name = "dvl";
            dvl.Size = new Size(89, 17);
            dvl.TabIndex = 0;
            dvl.Text = "间隔(毫秒/ms):";
            dvl.TextAlign = ContentAlignment.BottomCenter;
            // 
            // tvl
            // 
            tvl.AutoSize = true;
            tvl.Location = new Point(12, 99);
            tvl.Name = "tvl";
            tvl.Size = new Size(96, 17);
            tvl.TabIndex = 1;
            tvl.Text = "快捷键(Hotkey):";
            tvl.TextAlign = ContentAlignment.BottomCenter;
            // 
            // bh
            // 
            bh.AutoSize = true;
            bh.BackColor = Color.Transparent;
            bh.Font = new Font("Consolas", 12F, FontStyle.Regular, GraphicsUnit.Point);
            bh.Location = new Point(332, 92);
            bh.Name = "bh";
            bh.Size = new Size(66, 29);
            bh.TabIndex = 4;
            bh.Text = "?";
            bh.UseVisualStyleBackColor = false;
            bh.Click += bh_Click;
            // 
            // hkl
            // 
            hkl.AutoSize = true;
            hkl.Location = new Point(12, 39);
            hkl.Name = "hkl";
            hkl.Size = new Size(110, 17);
            hkl.TabIndex = 5;
            hkl.Text = "定时触发(Trigger):";
            hkl.TextAlign = ContentAlignment.BottomCenter;
            // 
            // wb1
            // 
            wb1.AutoSize = true;
            wb1.Location = new Point(12, 69);
            wb1.Name = "wb1";
            wb1.Size = new Size(101, 17);
            wb1.TabIndex = 6;
            wb1.Text = "点击次数(Count):";
            wb1.TextAlign = ContentAlignment.BottomCenter;
            // 
            // ct
            // 
            ct.DropDownStyle = ComboBoxStyle.DropDownList;
            ct.Items.AddRange(new object[] { "左键(Left)", "右键(Right)", "左键长按(Left Long Press)", "右键长按(Right Long Press)", "向上滚动(Scroll Up)", "向下滚动(Scroll Down)", "启动程序(Launch Program)" });
            ct.Location = new Point(198, 6);
            ct.Name = "ct";
            ct.Size = new Size(106, 25);
            ct.TabIndex = 7;
            ct.SelectedIndexChanged += ct_SelectedIndexChanged;
            // 
            // hk
            // 
            hk.DropDownStyle = ComboBoxStyle.DropDownList;
            hk.Location = new Point(114, 95);
            hk.Name = "hk";
            hk.Size = new Size(106, 25);
            hk.TabIndex = 8;
            hk.SelectedIndexChanged += hk_SelectedIndexChanged;
            // 
            // pk
            // 
            pk.Format = DateTimePickerFormat.Short;
            pk.Location = new Point(128, 37);
            pk.Name = "pk";
            pk.ShowUpDown = true;
            pk.Size = new Size(176, 23);
            pk.TabIndex = 9;
            // 
            // dv
            // 
            dv.Location = new Point(107, 8);
            dv.Name = "dv";
            dv.Size = new Size(88, 23);
            dv.TabIndex = 10;
            // 
            // pv
            // 
            pv.Location = new Point(111, 66);
            pv.Name = "pv";
            pv.Size = new Size(193, 23);
            pv.TabIndex = 11;
            // 
            // bs
            // 
            bs.AutoSize = true;
            bs.Location = new Point(237, 92);
            bs.Name = "bs";
            bs.Size = new Size(67, 29);
            bs.TabIndex = 12;
            bs.Text = "开始";
            bs.Click += bs_Click;
            // 
            // MainForm
            // 
            AutoScaleDimensions = new SizeF(7F, 17F);
            AutoScaleMode = AutoScaleMode.Font;
            ClientSize = new Size(408, 128);
            Controls.Add(dvl);
            Controls.Add(tvl);
            Controls.Add(bh);
            Controls.Add(hkl);
            Controls.Add(wb1);
            Controls.Add(ct);
            Controls.Add(hk);
            Controls.Add(pk);
            Controls.Add(dv);
            Controls.Add(pv);
            Controls.Add(bs);
            HelpButton = true;
            MaximizeBox = false;
            Name = "MainForm";
            Text = "MouseClickTool";
            FormClosing += MainForm_FormClosing;
            ResumeLayout(false);
            PerformLayout();
        }

        private System.Windows.Forms.Label dvl;
        private System.Windows.Forms.Label tvl;
        private System.Windows.Forms.Button bh;
        private System.Windows.Forms.Label hkl;
        private System.Windows.Forms.Label wb1;
        private System.Windows.Forms.ComboBox ct;
        private System.Windows.Forms.ComboBox hk;
        private System.Windows.Forms.DateTimePicker pk;
        private System.Windows.Forms.TextBox dv;
        private System.Windows.Forms.TextBox pv;
        private System.Windows.Forms.Button bs;
    }
}