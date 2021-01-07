namespace TestBusiness
{
    partial class Form1
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
            this.buttonDamian = new System.Windows.Forms.Button();
            this.listView1 = new System.Windows.Forms.ListView();
            this.DP_startdate = new System.Windows.Forms.DateTimePicker();
            this.label1 = new System.Windows.Forms.Label();
            this.DP_enddate = new System.Windows.Forms.DateTimePicker();
            this.SuspendLayout();
            // 
            // buttonDamian
            // 
            this.buttonDamian.Location = new System.Drawing.Point(267, 133);
            this.buttonDamian.Name = "buttonDamian";
            this.buttonDamian.Size = new System.Drawing.Size(214, 58);
            this.buttonDamian.TabIndex = 0;
            this.buttonDamian.Text = "Pruebas Damian";
            this.buttonDamian.UseVisualStyleBackColor = true;
            this.buttonDamian.Click += new System.EventHandler(this.buttonDamian_Click);
            // 
            // listView1
            // 
            this.listView1.Location = new System.Drawing.Point(12, 12);
            this.listView1.Name = "listView1";
            this.listView1.Size = new System.Drawing.Size(233, 179);
            this.listView1.TabIndex = 3;
            this.listView1.UseCompatibleStateImageBehavior = false;
            // 
            // DP_startdate
            // 
            this.DP_startdate.Location = new System.Drawing.Point(261, 12);
            this.DP_startdate.Name = "DP_startdate";
            this.DP_startdate.Size = new System.Drawing.Size(214, 20);
            this.DP_startdate.TabIndex = 4;
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(51, 194);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(35, 13);
            this.label1.TabIndex = 6;
            this.label1.Text = "label1";
            // 
            // DP_enddate
            // 
            this.DP_enddate.Location = new System.Drawing.Point(261, 60);
            this.DP_enddate.Name = "DP_enddate";
            this.DP_enddate.Size = new System.Drawing.Size(200, 20);
            this.DP_enddate.TabIndex = 7;
            // 
            // Form1
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(493, 252);
            this.Controls.Add(this.DP_enddate);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.DP_startdate);
            this.Controls.Add(this.listView1);
            this.Controls.Add(this.buttonDamian);
            this.Name = "Form1";
            this.Text = "Test";
            this.Load += new System.EventHandler(this.Form1_Load);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Button buttonDamian;
        private System.Windows.Forms.ListView listView1;
        private System.Windows.Forms.DateTimePicker DP_startdate;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.DateTimePicker DP_enddate;
    }
}

