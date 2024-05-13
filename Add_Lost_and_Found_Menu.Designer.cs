
namespace HotelManageMentSystem
{
    partial class Add_Lost_and_Found_Menu
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
            this.homepanel = new System.Windows.Forms.Panel();
            this.BackBT = new System.Windows.Forms.Button();
            this.label1 = new System.Windows.Forms.Label();
            this.guna2Button1 = new Guna.UI2.WinForms.Guna2Button();
            this.SubmitBT = new Guna.UI2.WinForms.Guna2Button();
            this.homepanel.SuspendLayout();
            this.SuspendLayout();
            // 
            // homepanel
            // 
            this.homepanel.BackColor = System.Drawing.SystemColors.ControlLight;
            this.homepanel.Controls.Add(this.BackBT);
            this.homepanel.Controls.Add(this.label1);
            this.homepanel.Controls.Add(this.guna2Button1);
            this.homepanel.Controls.Add(this.SubmitBT);
            this.homepanel.Dock = System.Windows.Forms.DockStyle.Fill;
            this.homepanel.Location = new System.Drawing.Point(0, 0);
            this.homepanel.Name = "homepanel";
            this.homepanel.Size = new System.Drawing.Size(1080, 729);
            this.homepanel.TabIndex = 1;
            // 
            // BackBT
            // 
            this.BackBT.FlatAppearance.BorderSize = 0;
            this.BackBT.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.BackBT.Font = new System.Drawing.Font("Verdana", 15.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.BackBT.Location = new System.Drawing.Point(0, 0);
            this.BackBT.Name = "BackBT";
            this.BackBT.Size = new System.Drawing.Size(43, 40);
            this.BackBT.TabIndex = 44;
            this.BackBT.Text = "<";
            this.BackBT.UseVisualStyleBackColor = true;
            this.BackBT.Click += new System.EventHandler(this.BackBT_Click);
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Font = new System.Drawing.Font("Century Gothic", 27.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label1.Location = new System.Drawing.Point(49, 0);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(372, 44);
            this.label1.TabIndex = 43;
            this.label1.Text = "Add Lost and Found";
            // 
            // guna2Button1
            // 
            this.guna2Button1.DisabledState.BorderColor = System.Drawing.Color.DarkGray;
            this.guna2Button1.DisabledState.CustomBorderColor = System.Drawing.Color.DarkGray;
            this.guna2Button1.DisabledState.FillColor = System.Drawing.Color.FromArgb(((int)(((byte)(169)))), ((int)(((byte)(169)))), ((int)(((byte)(169)))));
            this.guna2Button1.DisabledState.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(141)))), ((int)(((byte)(141)))), ((int)(((byte)(141)))));
            this.guna2Button1.FillColor = System.Drawing.Color.FromArgb(((int)(((byte)(64)))), ((int)(((byte)(64)))), ((int)(((byte)(64)))));
            this.guna2Button1.Font = new System.Drawing.Font("Century Gothic", 21.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.guna2Button1.ForeColor = System.Drawing.Color.White;
            this.guna2Button1.Location = new System.Drawing.Point(565, 290);
            this.guna2Button1.Name = "guna2Button1";
            this.guna2Button1.Size = new System.Drawing.Size(193, 112);
            this.guna2Button1.TabIndex = 42;
            this.guna2Button1.Text = "Add Lost Item";
            this.guna2Button1.Click += new System.EventHandler(this.guna2Button1_Click);
            // 
            // SubmitBT
            // 
            this.SubmitBT.DisabledState.BorderColor = System.Drawing.Color.DarkGray;
            this.SubmitBT.DisabledState.CustomBorderColor = System.Drawing.Color.DarkGray;
            this.SubmitBT.DisabledState.FillColor = System.Drawing.Color.FromArgb(((int)(((byte)(169)))), ((int)(((byte)(169)))), ((int)(((byte)(169)))));
            this.SubmitBT.DisabledState.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(141)))), ((int)(((byte)(141)))), ((int)(((byte)(141)))));
            this.SubmitBT.FillColor = System.Drawing.Color.FromArgb(((int)(((byte)(64)))), ((int)(((byte)(64)))), ((int)(((byte)(64)))));
            this.SubmitBT.Font = new System.Drawing.Font("Century Gothic", 21.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.SubmitBT.ForeColor = System.Drawing.Color.White;
            this.SubmitBT.Location = new System.Drawing.Point(325, 290);
            this.SubmitBT.Name = "SubmitBT";
            this.SubmitBT.Size = new System.Drawing.Size(193, 112);
            this.SubmitBT.TabIndex = 41;
            this.SubmitBT.Text = "Add Found Item";
            this.SubmitBT.Click += new System.EventHandler(this.SubmitBT_Click);
            // 
            // Add_Lost_and_Found_Menu
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1080, 729);
            this.Controls.Add(this.homepanel);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.None;
            this.Name = "Add_Lost_and_Found_Menu";
            this.Text = "Add_Lost_and_Found_Menu";
            this.homepanel.ResumeLayout(false);
            this.homepanel.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Panel homepanel;
        private System.Windows.Forms.Button BackBT;
        private System.Windows.Forms.Label label1;
        private Guna.UI2.WinForms.Guna2Button guna2Button1;
        private Guna.UI2.WinForms.Guna2Button SubmitBT;
    }
}