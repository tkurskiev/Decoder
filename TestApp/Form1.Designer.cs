namespace TestApp
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
            this.UserInputText = new System.Windows.Forms.TextBox();
            this.ResultLable = new System.Windows.Forms.Label();
            this.SuspendLayout();
            // 
            // UserInputText
            // 
            this.UserInputText.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.UserInputText.Location = new System.Drawing.Point(13, 13);
            this.UserInputText.Name = "UserInputText";
            this.UserInputText.Size = new System.Drawing.Size(427, 22);
            this.UserInputText.TabIndex = 0;
            this.UserInputText.TextChanged += new System.EventHandler(this.UserInputText_TextChanged);
            // 
            // ResultLable
            // 
            this.ResultLable.AutoSize = true;
            this.ResultLable.Location = new System.Drawing.Point(13, 42);
            this.ResultLable.Name = "ResultLable";
            this.ResultLable.Size = new System.Drawing.Size(48, 17);
            this.ResultLable.TabIndex = 1;
            this.ResultLable.Text = "Result";
            // 
            // Form1
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 16F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(452, 423);
            this.Controls.Add(this.ResultLable);
            this.Controls.Add(this.UserInputText);
            this.MinimumSize = new System.Drawing.Size(470, 470);
            this.Name = "Form1";
            this.Text = "Form1";
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.TextBox UserInputText;
        private System.Windows.Forms.Label ResultLable;
    }
}

