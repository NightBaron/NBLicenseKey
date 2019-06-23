namespace NBKey
{
    partial class Main
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(Main));
            this.label1 = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.txt_hwid = new System.Windows.Forms.TextBox();
            this.txt_day = new System.Windows.Forms.TextBox();
            this.btn_genkey = new System.Windows.Forms.Button();
            this.label3 = new System.Windows.Forms.Label();
            this.txt_key = new System.Windows.Forms.TextBox();
            this.btn_checkkey = new System.Windows.Forms.Button();
            this.backgroundWorker1 = new System.ComponentModel.BackgroundWorker();
            this.SuspendLayout();
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(12, 9);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(57, 20);
            this.label1.TabIndex = 0;
            this.label1.Text = "HWID:";
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(12, 44);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(41, 20);
            this.label2.TabIndex = 1;
            this.label2.Text = "Day:";
            // 
            // txt_hwid
            // 
            this.txt_hwid.Location = new System.Drawing.Point(75, 6);
            this.txt_hwid.Name = "txt_hwid";
            this.txt_hwid.Size = new System.Drawing.Size(256, 26);
            this.txt_hwid.TabIndex = 2;
            this.txt_hwid.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
            // 
            // txt_day
            // 
            this.txt_day.Location = new System.Drawing.Point(75, 41);
            this.txt_day.Name = "txt_day";
            this.txt_day.Size = new System.Drawing.Size(256, 26);
            this.txt_day.TabIndex = 3;
            this.txt_day.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
            this.txt_day.TextChanged += new System.EventHandler(this.Txt_day_TextChanged);
            // 
            // btn_genkey
            // 
            this.btn_genkey.Cursor = System.Windows.Forms.Cursors.Hand;
            this.btn_genkey.Enabled = false;
            this.btn_genkey.Location = new System.Drawing.Point(75, 73);
            this.btn_genkey.Name = "btn_genkey";
            this.btn_genkey.Size = new System.Drawing.Size(256, 35);
            this.btn_genkey.TabIndex = 4;
            this.btn_genkey.Text = "Gen Key";
            this.btn_genkey.UseVisualStyleBackColor = true;
            this.btn_genkey.Click += new System.EventHandler(this.Btn_genkey_Click);
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(12, 117);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(39, 20);
            this.label3.TabIndex = 5;
            this.label3.Text = "Key:";
            // 
            // txt_key
            // 
            this.txt_key.Location = new System.Drawing.Point(75, 114);
            this.txt_key.Name = "txt_key";
            this.txt_key.Size = new System.Drawing.Size(256, 26);
            this.txt_key.TabIndex = 6;
            this.txt_key.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
            // 
            // btn_checkkey
            // 
            this.btn_checkkey.Cursor = System.Windows.Forms.Cursors.Hand;
            this.btn_checkkey.Location = new System.Drawing.Point(75, 146);
            this.btn_checkkey.Name = "btn_checkkey";
            this.btn_checkkey.Size = new System.Drawing.Size(256, 35);
            this.btn_checkkey.TabIndex = 7;
            this.btn_checkkey.Text = "Check Key";
            this.btn_checkkey.UseVisualStyleBackColor = true;
            this.btn_checkkey.Click += new System.EventHandler(this.Btn_checkkey_Click);
            // 
            // backgroundWorker1
            // 
            this.backgroundWorker1.DoWork += new System.ComponentModel.DoWorkEventHandler(this.BackgroundWorker1_DoWork);
            // 
            // Main
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(9F, 20F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(342, 186);
            this.Controls.Add(this.btn_checkkey);
            this.Controls.Add(this.txt_key);
            this.Controls.Add(this.label3);
            this.Controls.Add(this.btn_genkey);
            this.Controls.Add(this.txt_day);
            this.Controls.Add(this.txt_hwid);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.label1);
            this.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedToolWindow;
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.Margin = new System.Windows.Forms.Padding(4, 5, 4, 5);
            this.Name = "Main";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "NB License Key Generator v1.0";
            this.TopMost = true;
            this.Load += new System.EventHandler(this.Main_Load);
            this.Shown += new System.EventHandler(this.Main_Shown);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.TextBox txt_hwid;
        private System.Windows.Forms.TextBox txt_day;
        private System.Windows.Forms.Button btn_genkey;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.TextBox txt_key;
        private System.Windows.Forms.Button btn_checkkey;
        private System.ComponentModel.BackgroundWorker backgroundWorker1;
    }
}

