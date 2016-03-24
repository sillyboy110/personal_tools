namespace Get_Tweet
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
            this.panel1 = new System.Windows.Forms.Panel();
            this.button_crawel = new System.Windows.Forms.Button();
            this.SuspendLayout();
            // 
            // panel1
            // 
            this.panel1.Location = new System.Drawing.Point(12, 37);
            this.panel1.Name = "panel1";
            this.panel1.Size = new System.Drawing.Size(716, 384);
            this.panel1.TabIndex = 0;
            // 
            // button_crawel
            // 
            this.button_crawel.Location = new System.Drawing.Point(653, 12);
            this.button_crawel.Name = "button_crawel";
            this.button_crawel.Size = new System.Drawing.Size(75, 23);
            this.button_crawel.TabIndex = 1;
            this.button_crawel.Text = "crawel";
            this.button_crawel.UseVisualStyleBackColor = true;
            this.button_crawel.Click += new System.EventHandler(this.button_crawel_Click);
            // 
            // Form1
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(740, 433);
            this.Controls.Add(this.button_crawel);
            this.Controls.Add(this.panel1);
            this.Name = "Form1";
            this.Text = "Get Tweet";
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Panel panel1;
        private System.Windows.Forms.Button button_crawel;
    }
}

