namespace ExSharpBase.Menu
{
    partial class BasePlate
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
            this.TopPanel = new System.Windows.Forms.Panel();
            this.label1 = new System.Windows.Forms.Label();
            this.DrawPlayerCheckBox = new System.Windows.Forms.CheckBox();
            this.LootCheckBox = new System.Windows.Forms.CheckBox();
            this.chk2dBox = new System.Windows.Forms.RadioButton();
            this.chk3dBox = new System.Windows.Forms.RadioButton();
            this.AimbotCheckBox = new System.Windows.Forms.CheckBox();
            this.trackBar1 = new System.Windows.Forms.TrackBar();
            this.label2 = new System.Windows.Forms.Label();
            this.MagicBulletCheckBox = new System.Windows.Forms.CheckBox();
            this.label3 = new System.Windows.Forms.Label();
            this.chkDrawSkelton = new System.Windows.Forms.CheckBox();
            this.btnBoxColor = new System.Windows.Forms.Button();
            this.lblBoxColor = new System.Windows.Forms.Label();
            this.lblSkeltonColor = new System.Windows.Forms.Label();
            this.btnSkeltonColor = new System.Windows.Forms.Button();
            this.chkDrawFOV = new System.Windows.Forms.CheckBox();
            this.chkDrawCrosshair = new System.Windows.Forms.CheckBox();
            this.checkBox1 = new System.Windows.Forms.CheckBox();
            this.radioButton1 = new System.Windows.Forms.RadioButton();
            this.radioButton2 = new System.Windows.Forms.RadioButton();
            this.TopPanel.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.trackBar1)).BeginInit();
            this.SuspendLayout();
            // 
            // TopPanel
            // 
            this.TopPanel.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.TopPanel.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(6)))), ((int)(((byte)(45)))), ((int)(((byte)(66)))));
            this.TopPanel.Controls.Add(this.label1);
            this.TopPanel.Location = new System.Drawing.Point(0, 0);
            this.TopPanel.Name = "TopPanel";
            this.TopPanel.Size = new System.Drawing.Size(273, 30);
            this.TopPanel.TabIndex = 0;
            this.TopPanel.MouseDown += new System.Windows.Forms.MouseEventHandler(this.TopPanel_MouseDown);
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label1.ForeColor = System.Drawing.Color.White;
            this.label1.Location = new System.Drawing.Point(11, 6);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(49, 20);
            this.label1.TabIndex = 0;
            this.label1.Text = "Menu";
            // 
            // DrawPlayerCheckBox
            // 
            this.DrawPlayerCheckBox.AutoSize = true;
            this.DrawPlayerCheckBox.Checked = true;
            this.DrawPlayerCheckBox.CheckState = System.Windows.Forms.CheckState.Checked;
            this.DrawPlayerCheckBox.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.DrawPlayerCheckBox.ForeColor = System.Drawing.Color.White;
            this.DrawPlayerCheckBox.Location = new System.Drawing.Point(23, 65);
            this.DrawPlayerCheckBox.Name = "DrawPlayerCheckBox";
            this.DrawPlayerCheckBox.Size = new System.Drawing.Size(112, 24);
            this.DrawPlayerCheckBox.TabIndex = 1;
            this.DrawPlayerCheckBox.Text = "Draw Player";
            this.DrawPlayerCheckBox.UseVisualStyleBackColor = true;
            this.DrawPlayerCheckBox.CheckedChanged += new System.EventHandler(this.DrawRangeCheckBox_CheckedChanged);
            // 
            // LootCheckBox
            // 
            this.LootCheckBox.AutoSize = true;
            this.LootCheckBox.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.LootCheckBox.ForeColor = System.Drawing.Color.White;
            this.LootCheckBox.Location = new System.Drawing.Point(23, 252);
            this.LootCheckBox.Name = "LootCheckBox";
            this.LootCheckBox.Size = new System.Drawing.Size(117, 24);
            this.LootCheckBox.TabIndex = 2;
            this.LootCheckBox.Text = "Loot [NUM3]";
            this.LootCheckBox.UseVisualStyleBackColor = true;
            this.LootCheckBox.CheckedChanged += new System.EventHandler(this.MoveToMouseCheckBox_CheckedChanged);
            // 
            // chk2dBox
            // 
            this.chk2dBox.AutoSize = true;
            this.chk2dBox.Checked = true;
            this.chk2dBox.Font = new System.Drawing.Font("Microsoft Sans Serif", 10F);
            this.chk2dBox.ForeColor = System.Drawing.Color.White;
            this.chk2dBox.Location = new System.Drawing.Point(34, 95);
            this.chk2dBox.Name = "chk2dBox";
            this.chk2dBox.Size = new System.Drawing.Size(69, 21);
            this.chk2dBox.TabIndex = 4;
            this.chk2dBox.TabStop = true;
            this.chk2dBox.Text = "2d Box";
            this.chk2dBox.UseVisualStyleBackColor = true;
            this.chk2dBox.CheckedChanged += new System.EventHandler(this.chk2dBox_CheckedChanged);
            // 
            // chk3dBox
            // 
            this.chk3dBox.AutoSize = true;
            this.chk3dBox.Font = new System.Drawing.Font("Microsoft Sans Serif", 10F);
            this.chk3dBox.ForeColor = System.Drawing.Color.White;
            this.chk3dBox.Location = new System.Drawing.Point(109, 95);
            this.chk3dBox.Name = "chk3dBox";
            this.chk3dBox.Size = new System.Drawing.Size(69, 21);
            this.chk3dBox.TabIndex = 5;
            this.chk3dBox.Text = "3d Box";
            this.chk3dBox.UseVisualStyleBackColor = true;
            this.chk3dBox.CheckedChanged += new System.EventHandler(this.chk3dBox_CheckedChanged);
            // 
            // AimbotCheckBox
            // 
            this.AimbotCheckBox.AutoSize = true;
            this.AimbotCheckBox.Checked = true;
            this.AimbotCheckBox.CheckState = System.Windows.Forms.CheckState.Checked;
            this.AimbotCheckBox.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.AimbotCheckBox.ForeColor = System.Drawing.Color.White;
            this.AimbotCheckBox.Location = new System.Drawing.Point(23, 282);
            this.AimbotCheckBox.Name = "AimbotCheckBox";
            this.AimbotCheckBox.Size = new System.Drawing.Size(78, 24);
            this.AimbotCheckBox.TabIndex = 6;
            this.AimbotCheckBox.Text = "Aimbot";
            this.AimbotCheckBox.UseVisualStyleBackColor = true;
            this.AimbotCheckBox.CheckedChanged += new System.EventHandler(this.checkBox1_CheckedChanged);
            // 
            // trackBar1
            // 
            this.trackBar1.AutoSize = false;
            this.trackBar1.Location = new System.Drawing.Point(13, 337);
            this.trackBar1.Maximum = 200;
            this.trackBar1.Minimum = 100;
            this.trackBar1.Name = "trackBar1";
            this.trackBar1.Size = new System.Drawing.Size(244, 23);
            this.trackBar1.TabIndex = 7;
            this.trackBar1.Value = 100;
            this.trackBar1.Scroll += new System.EventHandler(this.trackBar1_Scroll);
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F);
            this.label2.ForeColor = System.Drawing.Color.White;
            this.label2.Location = new System.Drawing.Point(19, 314);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(102, 20);
            this.label2.TabIndex = 8;
            this.label2.Text = "Field Of View";
            // 
            // MagicBulletCheckBox
            // 
            this.MagicBulletCheckBox.AutoSize = true;
            this.MagicBulletCheckBox.Checked = true;
            this.MagicBulletCheckBox.CheckState = System.Windows.Forms.CheckState.Checked;
            this.MagicBulletCheckBox.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.MagicBulletCheckBox.ForeColor = System.Drawing.Color.White;
            this.MagicBulletCheckBox.Location = new System.Drawing.Point(23, 370);
            this.MagicBulletCheckBox.Name = "MagicBulletCheckBox";
            this.MagicBulletCheckBox.Size = new System.Drawing.Size(114, 24);
            this.MagicBulletCheckBox.TabIndex = 9;
            this.MagicBulletCheckBox.Text = "Magic Bullet";
            this.MagicBulletCheckBox.UseVisualStyleBackColor = true;
            this.MagicBulletCheckBox.CheckedChanged += new System.EventHandler(this.MagicBulletCheckBox_CheckedChanged);
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Font = new System.Drawing.Font("Microsoft Sans Serif", 7F);
            this.label3.ForeColor = System.Drawing.Color.Red;
            this.label3.Location = new System.Drawing.Point(48, 397);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(213, 13);
            this.label3.TabIndex = 10;
            this.label3.Text = "Note :Magic bullet dosen\'t work while aiming";
            // 
            // chkDrawSkelton
            // 
            this.chkDrawSkelton.AutoSize = true;
            this.chkDrawSkelton.Checked = true;
            this.chkDrawSkelton.CheckState = System.Windows.Forms.CheckState.Checked;
            this.chkDrawSkelton.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.chkDrawSkelton.ForeColor = System.Drawing.Color.White;
            this.chkDrawSkelton.Location = new System.Drawing.Point(143, 65);
            this.chkDrawSkelton.Name = "chkDrawSkelton";
            this.chkDrawSkelton.Size = new System.Drawing.Size(123, 24);
            this.chkDrawSkelton.TabIndex = 11;
            this.chkDrawSkelton.Text = "Draw Skelton";
            this.chkDrawSkelton.UseVisualStyleBackColor = true;
            this.chkDrawSkelton.CheckedChanged += new System.EventHandler(this.chkDrawSkelton_CheckedChanged);
            // 
            // btnBoxColor
            // 
            this.btnBoxColor.ForeColor = System.Drawing.Color.White;
            this.btnBoxColor.Location = new System.Drawing.Point(23, 122);
            this.btnBoxColor.Name = "btnBoxColor";
            this.btnBoxColor.Size = new System.Drawing.Size(99, 23);
            this.btnBoxColor.TabIndex = 12;
            this.btnBoxColor.Text = "Box Color";
            this.btnBoxColor.UseVisualStyleBackColor = true;
            this.btnBoxColor.Click += new System.EventHandler(this.btnBoxColor_Click);
            // 
            // lblBoxColor
            // 
            this.lblBoxColor.AutoSize = true;
            this.lblBoxColor.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F);
            this.lblBoxColor.ForeColor = System.Drawing.Color.Red;
            this.lblBoxColor.Location = new System.Drawing.Point(128, 122);
            this.lblBoxColor.Name = "lblBoxColor";
            this.lblBoxColor.Size = new System.Drawing.Size(77, 20);
            this.lblBoxColor.TabIndex = 13;
            this.lblBoxColor.Text = "Box Color";
            // 
            // lblSkeltonColor
            // 
            this.lblSkeltonColor.AutoSize = true;
            this.lblSkeltonColor.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F);
            this.lblSkeltonColor.ForeColor = System.Drawing.Color.White;
            this.lblSkeltonColor.Location = new System.Drawing.Point(128, 150);
            this.lblSkeltonColor.Name = "lblSkeltonColor";
            this.lblSkeltonColor.Size = new System.Drawing.Size(104, 20);
            this.lblSkeltonColor.TabIndex = 15;
            this.lblSkeltonColor.Text = "Skelton Color";
            // 
            // btnSkeltonColor
            // 
            this.btnSkeltonColor.ForeColor = System.Drawing.Color.White;
            this.btnSkeltonColor.Location = new System.Drawing.Point(23, 150);
            this.btnSkeltonColor.Name = "btnSkeltonColor";
            this.btnSkeltonColor.Size = new System.Drawing.Size(99, 23);
            this.btnSkeltonColor.TabIndex = 14;
            this.btnSkeltonColor.Text = "Skelton Color";
            this.btnSkeltonColor.UseVisualStyleBackColor = true;
            this.btnSkeltonColor.Click += new System.EventHandler(this.btnSkeltonColor_Click);
            // 
            // chkDrawFOV
            // 
            this.chkDrawFOV.AutoSize = true;
            this.chkDrawFOV.Checked = true;
            this.chkDrawFOV.CheckState = System.Windows.Forms.CheckState.Checked;
            this.chkDrawFOV.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.chkDrawFOV.ForeColor = System.Drawing.Color.White;
            this.chkDrawFOV.Location = new System.Drawing.Point(155, 313);
            this.chkDrawFOV.Name = "chkDrawFOV";
            this.chkDrawFOV.Size = new System.Drawing.Size(102, 24);
            this.chkDrawFOV.TabIndex = 16;
            this.chkDrawFOV.Text = "Draw FOV";
            this.chkDrawFOV.UseVisualStyleBackColor = true;
            this.chkDrawFOV.CheckedChanged += new System.EventHandler(this.checkBox1_CheckedChanged_1);
            // 
            // chkDrawCrosshair
            // 
            this.chkDrawCrosshair.AutoSize = true;
            this.chkDrawCrosshair.Checked = true;
            this.chkDrawCrosshair.CheckState = System.Windows.Forms.CheckState.Checked;
            this.chkDrawCrosshair.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.chkDrawCrosshair.ForeColor = System.Drawing.Color.White;
            this.chkDrawCrosshair.Location = new System.Drawing.Point(23, 193);
            this.chkDrawCrosshair.Name = "chkDrawCrosshair";
            this.chkDrawCrosshair.Size = new System.Drawing.Size(136, 24);
            this.chkDrawCrosshair.TabIndex = 17;
            this.chkDrawCrosshair.Text = "Draw Crosshair";
            this.chkDrawCrosshair.UseVisualStyleBackColor = true;
            this.chkDrawCrosshair.CheckedChanged += new System.EventHandler(this.checkBox2_CheckedChanged);
            // 
            // checkBox1
            // 
            this.checkBox1.AutoSize = true;
            this.checkBox1.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.checkBox1.ForeColor = System.Drawing.Color.White;
            this.checkBox1.Location = new System.Drawing.Point(23, 223);
            this.checkBox1.Name = "checkBox1";
            this.checkBox1.Size = new System.Drawing.Size(129, 24);
            this.checkBox1.TabIndex = 18;
            this.checkBox1.Text = "Draw Vehicles";
            this.checkBox1.UseVisualStyleBackColor = true;
            // 
            // radioButton1
            // 
            this.radioButton1.AutoSize = true;
            this.radioButton1.Font = new System.Drawing.Font("Microsoft Sans Serif", 10F);
            this.radioButton1.ForeColor = System.Drawing.Color.White;
            this.radioButton1.Location = new System.Drawing.Point(195, 284);
            this.radioButton1.Name = "radioButton1";
            this.radioButton1.Size = new System.Drawing.Size(62, 21);
            this.radioButton1.TabIndex = 20;
            this.radioButton1.Tag = "3";
            this.radioButton1.Text = "Chest";
            this.radioButton1.UseVisualStyleBackColor = true;
            this.radioButton1.CheckedChanged += new System.EventHandler(this.radioButton1_CheckedChanged);
            // 
            // radioButton2
            // 
            this.radioButton2.AutoSize = true;
            this.radioButton2.Checked = true;
            this.radioButton2.Font = new System.Drawing.Font("Microsoft Sans Serif", 10F);
            this.radioButton2.ForeColor = System.Drawing.Color.White;
            this.radioButton2.Location = new System.Drawing.Point(136, 284);
            this.radioButton2.Name = "radioButton2";
            this.radioButton2.Size = new System.Drawing.Size(60, 21);
            this.radioButton2.TabIndex = 19;
            this.radioButton2.TabStop = true;
            this.radioButton2.Tag = "6";
            this.radioButton2.Text = "Head";
            this.radioButton2.UseVisualStyleBackColor = true;
            this.radioButton2.CheckedChanged += new System.EventHandler(this.radioButton2_CheckedChanged);
            // 
            // BasePlate
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(4)))), ((int)(((byte)(33)))), ((int)(((byte)(48)))));
            this.ClientSize = new System.Drawing.Size(273, 418);
            this.Controls.Add(this.radioButton1);
            this.Controls.Add(this.radioButton2);
            this.Controls.Add(this.checkBox1);
            this.Controls.Add(this.chkDrawCrosshair);
            this.Controls.Add(this.chkDrawFOV);
            this.Controls.Add(this.lblSkeltonColor);
            this.Controls.Add(this.btnSkeltonColor);
            this.Controls.Add(this.lblBoxColor);
            this.Controls.Add(this.btnBoxColor);
            this.Controls.Add(this.chkDrawSkelton);
            this.Controls.Add(this.label3);
            this.Controls.Add(this.MagicBulletCheckBox);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.trackBar1);
            this.Controls.Add(this.AimbotCheckBox);
            this.Controls.Add(this.chk3dBox);
            this.Controls.Add(this.chk2dBox);
            this.Controls.Add(this.DrawPlayerCheckBox);
            this.Controls.Add(this.LootCheckBox);
            this.Controls.Add(this.TopPanel);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.None;
            this.Name = "BasePlate";
            this.ShowIcon = false;
            this.ShowInTaskbar = false;
            this.TopMost = true;
            this.Load += new System.EventHandler(this.BasePlate_Load);
            this.TopPanel.ResumeLayout(false);
            this.TopPanel.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.trackBar1)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Panel TopPanel;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.CheckBox DrawPlayerCheckBox;
        private System.Windows.Forms.CheckBox LootCheckBox;
        private System.Windows.Forms.RadioButton chk2dBox;
        private System.Windows.Forms.RadioButton chk3dBox;
        private System.Windows.Forms.CheckBox AimbotCheckBox;
        private System.Windows.Forms.TrackBar trackBar1;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.CheckBox MagicBulletCheckBox;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.CheckBox chkDrawSkelton;
        private System.Windows.Forms.Button btnBoxColor;
        private System.Windows.Forms.Label lblBoxColor;
        private System.Windows.Forms.Label lblSkeltonColor;
        private System.Windows.Forms.Button btnSkeltonColor;
        private System.Windows.Forms.CheckBox chkDrawFOV;
        private System.Windows.Forms.CheckBox chkDrawCrosshair;
        private System.Windows.Forms.CheckBox checkBox1;
        private System.Windows.Forms.RadioButton radioButton1;
        private System.Windows.Forms.RadioButton radioButton2;
    }
}