
namespace HotelManageMentSystem
{
    partial class Add_Found_Item
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
            this.label3 = new System.Windows.Forms.Label();
            this.submitBT = new Guna.UI2.WinForms.Guna2Button();
            this.label7 = new System.Windows.Forms.Label();
            this.label1 = new System.Windows.Forms.Label();
            this.panel3 = new System.Windows.Forms.Panel();
            this.txtItemDescription = new Guna.UI2.WinForms.Guna2TextBox();
            this.txtFoundItem = new Guna.UI2.WinForms.Guna2TextBox();
            this.BackBT = new System.Windows.Forms.Button();
            this.panel3.SuspendLayout();
            this.SuspendLayout();
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Font = new System.Drawing.Font("Century Gothic", 26.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label3.Location = new System.Drawing.Point(49, 0);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(271, 41);
            this.label3.TabIndex = 22;
            this.label3.Text = "Lost and Found";
            // 
            // submitBT
            // 
            this.submitBT.DisabledState.BorderColor = System.Drawing.Color.DarkGray;
            this.submitBT.DisabledState.CustomBorderColor = System.Drawing.Color.DarkGray;
            this.submitBT.DisabledState.FillColor = System.Drawing.Color.FromArgb(((int)(((byte)(169)))), ((int)(((byte)(169)))), ((int)(((byte)(169)))));
            this.submitBT.DisabledState.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(141)))), ((int)(((byte)(141)))), ((int)(((byte)(141)))));
            this.submitBT.FillColor = System.Drawing.Color.FromArgb(((int)(((byte)(64)))), ((int)(((byte)(64)))), ((int)(((byte)(64)))));
            this.submitBT.Font = new System.Drawing.Font("Century Gothic", 14.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.submitBT.ForeColor = System.Drawing.Color.White;
            this.submitBT.Location = new System.Drawing.Point(474, 412);
            this.submitBT.Name = "submitBT";
            this.submitBT.Size = new System.Drawing.Size(152, 69);
            this.submitBT.TabIndex = 18;
            this.submitBT.Text = "Submit";
            this.submitBT.Click += new System.EventHandler(this.submitBT_Click);
            // 
            // label7
            // 
            this.label7.AutoSize = true;
            this.label7.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label7.Location = new System.Drawing.Point(234, 337);
            this.label7.Name = "label7";
            this.label7.Size = new System.Drawing.Size(52, 20);
            this.label7.TabIndex = 12;
            this.label7.Text = "Note:";
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label1.Location = new System.Drawing.Point(234, 252);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(106, 20);
            this.label1.TabIndex = 0;
            this.label1.Text = "Found Item:";
            // 
            // panel3
            // 
            this.panel3.BackColor = System.Drawing.SystemColors.ControlLight;
            this.panel3.Controls.Add(this.txtItemDescription);
            this.panel3.Controls.Add(this.txtFoundItem);
            this.panel3.Controls.Add(this.BackBT);
            this.panel3.Controls.Add(this.label3);
            this.panel3.Controls.Add(this.submitBT);
            this.panel3.Controls.Add(this.label7);
            this.panel3.Controls.Add(this.label1);
            this.panel3.Dock = System.Windows.Forms.DockStyle.Fill;
            this.panel3.Location = new System.Drawing.Point(0, 0);
            this.panel3.Name = "panel3";
            this.panel3.Size = new System.Drawing.Size(1080, 729);
            this.panel3.TabIndex = 12;
            // 
            // txtItemDescription
            // 
            this.txtItemDescription.Cursor = System.Windows.Forms.Cursors.IBeam;
            this.txtItemDescription.DefaultText = "";
            this.txtItemDescription.DisabledState.BorderColor = System.Drawing.Color.FromArgb(((int)(((byte)(208)))), ((int)(((byte)(208)))), ((int)(((byte)(208)))));
            this.txtItemDescription.DisabledState.FillColor = System.Drawing.Color.FromArgb(((int)(((byte)(226)))), ((int)(((byte)(226)))), ((int)(((byte)(226)))));
            this.txtItemDescription.DisabledState.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(138)))), ((int)(((byte)(138)))), ((int)(((byte)(138)))));
            this.txtItemDescription.DisabledState.PlaceholderForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(138)))), ((int)(((byte)(138)))), ((int)(((byte)(138)))));
            this.txtItemDescription.FocusedState.BorderColor = System.Drawing.Color.FromArgb(((int)(((byte)(94)))), ((int)(((byte)(148)))), ((int)(((byte)(255)))));
            this.txtItemDescription.Font = new System.Drawing.Font("Segoe UI", 9F);
            this.txtItemDescription.HoverState.BorderColor = System.Drawing.Color.FromArgb(((int)(((byte)(94)))), ((int)(((byte)(148)))), ((int)(((byte)(255)))));
            this.txtItemDescription.Location = new System.Drawing.Point(414, 324);
            this.txtItemDescription.Name = "txtItemDescription";
            this.txtItemDescription.PasswordChar = '\0';
            this.txtItemDescription.PlaceholderText = "";
            this.txtItemDescription.SelectedText = "";
            this.txtItemDescription.Size = new System.Drawing.Size(285, 45);
            this.txtItemDescription.TabIndex = 28;
            this.txtItemDescription.KeyDown += new System.Windows.Forms.KeyEventHandler(this.txtItemDescription_KeyDown);
            // 
            // txtFoundItem
            // 
            this.txtFoundItem.Cursor = System.Windows.Forms.Cursors.IBeam;
            this.txtFoundItem.DefaultText = "";
            this.txtFoundItem.DisabledState.BorderColor = System.Drawing.Color.FromArgb(((int)(((byte)(208)))), ((int)(((byte)(208)))), ((int)(((byte)(208)))));
            this.txtFoundItem.DisabledState.FillColor = System.Drawing.Color.FromArgb(((int)(((byte)(226)))), ((int)(((byte)(226)))), ((int)(((byte)(226)))));
            this.txtFoundItem.DisabledState.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(138)))), ((int)(((byte)(138)))), ((int)(((byte)(138)))));
            this.txtFoundItem.DisabledState.PlaceholderForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(138)))), ((int)(((byte)(138)))), ((int)(((byte)(138)))));
            this.txtFoundItem.FocusedState.BorderColor = System.Drawing.Color.FromArgb(((int)(((byte)(94)))), ((int)(((byte)(148)))), ((int)(((byte)(255)))));
            this.txtFoundItem.Font = new System.Drawing.Font("Segoe UI", 9F);
            this.txtFoundItem.HoverState.BorderColor = System.Drawing.Color.FromArgb(((int)(((byte)(94)))), ((int)(((byte)(148)))), ((int)(((byte)(255)))));
            this.txtFoundItem.Location = new System.Drawing.Point(414, 238);
            this.txtFoundItem.Name = "txtFoundItem";
            this.txtFoundItem.PasswordChar = '\0';
            this.txtFoundItem.PlaceholderText = "";
            this.txtFoundItem.SelectedText = "";
            this.txtFoundItem.Size = new System.Drawing.Size(285, 45);
            this.txtFoundItem.TabIndex = 27;
            // 
            // BackBT
            // 
            this.BackBT.FlatAppearance.BorderSize = 0;
            this.BackBT.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.BackBT.Font = new System.Drawing.Font("Verdana", 15.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.BackBT.Location = new System.Drawing.Point(0, 0);
            this.BackBT.Name = "BackBT";
            this.BackBT.Size = new System.Drawing.Size(43, 48);
            this.BackBT.TabIndex = 26;
            this.BackBT.Text = "<";
            this.BackBT.UseVisualStyleBackColor = true;
            this.BackBT.Click += new System.EventHandler(this.BackBT_Click);
            // 
            // Add_Found_Item
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1080, 729);
            this.Controls.Add(this.panel3);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.None;
            this.Name = "Add_Found_Item";
            this.Text = "Add_Found_Item";
            this.panel3.ResumeLayout(false);
            this.panel3.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Label label3;
        private Guna.UI2.WinForms.Guna2Button submitBT;
        private System.Windows.Forms.Label label7;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Panel panel3;
        private Guna.UI2.WinForms.Guna2TextBox txtItemDescription;
        private Guna.UI2.WinForms.Guna2TextBox txtFoundItem;
        private System.Windows.Forms.Button BackBT;
    }
}