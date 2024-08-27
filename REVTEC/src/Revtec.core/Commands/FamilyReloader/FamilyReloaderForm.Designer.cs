namespace Revtec.core.Commands.FamilyStuff
{
    partial class FamilyReloaderForm
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
            this.choose_source_btn = new System.Windows.Forms.Button();
            this.choose_target_btn = new System.Windows.Forms.Button();
            this.SuspendLayout();
            // 
            // choose_source_btn
            // 
            this.choose_source_btn.Location = new System.Drawing.Point(71, 181);
            this.choose_source_btn.Name = "choose_source_btn";
            this.choose_source_btn.Size = new System.Drawing.Size(349, 100);
            this.choose_source_btn.TabIndex = 0;
            this.choose_source_btn.Text = "Choose source folder";
            this.choose_source_btn.UseVisualStyleBackColor = true;
            this.choose_source_btn.Click += new System.EventHandler(this.button1_Click);
            // 
            // choose_target_btn
            // 
            this.choose_target_btn.Location = new System.Drawing.Point(71, 307);
            this.choose_target_btn.Name = "choose_target_btn";
            this.choose_target_btn.Size = new System.Drawing.Size(349, 98);
            this.choose_target_btn.TabIndex = 1;
            this.choose_target_btn.Text = "Choose Target Folder";
            this.choose_target_btn.UseVisualStyleBackColor = true;
            // 
            // FamilyReloaderForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(9F, 20F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1251, 803);
            this.Controls.Add(this.choose_target_btn);
            this.Controls.Add(this.choose_source_btn);
            this.Name = "FamilyReloaderForm";
            this.Text = "FamilyReloaderForm";
            this.Load += new System.EventHandler(this.FamilyReloaderForm_Load);
            this.ResumeLayout(false);

        }

        #endregion


        private System.Windows.Forms.Button btnOk;
        private System.Windows.Forms.Button btnCancel;
        private System.Windows.Forms.GroupBox groupBox1;
        private System.Windows.Forms.DataGridView dataGridView1;
        private System.Windows.Forms.DataGridViewTextBoxColumn DGcategoryNames;
        private System.Windows.Forms.DataGridViewTextBoxColumn DGfamilyNames;
        private System.Windows.Forms.Button choose_source_btn;
        private System.Windows.Forms.Button choose_target_btn;
    }
}