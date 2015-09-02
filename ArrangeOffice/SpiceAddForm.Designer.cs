namespace ArrangeOffice
{
    partial class SpiceAddForm
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
            this.spiceNameTextbox = new System.Windows.Forms.TextBox();
            this.label1 = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.spiceNumberTextbox = new System.Windows.Forms.TextBox();
            this.label3 = new System.Windows.Forms.Label();
            this.label4 = new System.Windows.Forms.Label();
            this.spiceUnitTextbox = new System.Windows.Forms.TextBox();
            this.confirmSpiceButton = new System.Windows.Forms.Button();
            this.CanelSpiceButton = new System.Windows.Forms.Button();
            this.spiceStoreCombobox = new System.Windows.Forms.ComboBox();
            this.SuspendLayout();
            // 
            // spiceNameTextbox
            // 
            this.spiceNameTextbox.Font = new System.Drawing.Font("微軟正黑體", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(136)));
            this.spiceNameTextbox.Location = new System.Drawing.Point(188, 46);
            this.spiceNameTextbox.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
            this.spiceNameTextbox.Name = "spiceNameTextbox";
            this.spiceNameTextbox.Size = new System.Drawing.Size(207, 29);
            this.spiceNameTextbox.TabIndex = 0;
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Font = new System.Drawing.Font("微軟正黑體", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(136)));
            this.label1.Location = new System.Drawing.Point(77, 50);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(89, 20);
            this.label1.TabIndex = 1;
            this.label1.Text = "香料名稱：";
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Font = new System.Drawing.Font("微軟正黑體", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(136)));
            this.label2.Location = new System.Drawing.Point(77, 114);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(89, 20);
            this.label2.TabIndex = 2;
            this.label2.Text = "香料編號：";
            // 
            // spiceNumberTextbox
            // 
            this.spiceNumberTextbox.Font = new System.Drawing.Font("微軟正黑體", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(136)));
            this.spiceNumberTextbox.Location = new System.Drawing.Point(188, 110);
            this.spiceNumberTextbox.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
            this.spiceNumberTextbox.Name = "spiceNumberTextbox";
            this.spiceNumberTextbox.Size = new System.Drawing.Size(207, 29);
            this.spiceNumberTextbox.TabIndex = 3;
            this.spiceNumberTextbox.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.spiceNumberTextbox_KeyPress);
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Font = new System.Drawing.Font("微軟正黑體", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(136)));
            this.label3.Location = new System.Drawing.Point(77, 172);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(89, 20);
            this.label3.TabIndex = 4;
            this.label3.Text = "存放倉位：";
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Font = new System.Drawing.Font("微軟正黑體", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(136)));
            this.label4.Location = new System.Drawing.Point(77, 234);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(89, 20);
            this.label4.TabIndex = 6;
            this.label4.Text = "香料單位：";
            // 
            // spiceUnitTextbox
            // 
            this.spiceUnitTextbox.Font = new System.Drawing.Font("微軟正黑體", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(136)));
            this.spiceUnitTextbox.Location = new System.Drawing.Point(188, 230);
            this.spiceUnitTextbox.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
            this.spiceUnitTextbox.Name = "spiceUnitTextbox";
            this.spiceUnitTextbox.Size = new System.Drawing.Size(207, 29);
            this.spiceUnitTextbox.TabIndex = 7;
            // 
            // confirmSpiceButton
            // 
            this.confirmSpiceButton.Font = new System.Drawing.Font("微軟正黑體", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(136)));
            this.confirmSpiceButton.Location = new System.Drawing.Point(93, 314);
            this.confirmSpiceButton.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
            this.confirmSpiceButton.Name = "confirmSpiceButton";
            this.confirmSpiceButton.Size = new System.Drawing.Size(113, 42);
            this.confirmSpiceButton.TabIndex = 8;
            this.confirmSpiceButton.Text = "新增香料";
            this.confirmSpiceButton.UseVisualStyleBackColor = true;
            this.confirmSpiceButton.Click += new System.EventHandler(this.confirmSpiceButton_Click);
            // 
            // CanelSpiceButton
            // 
            this.CanelSpiceButton.Font = new System.Drawing.Font("微軟正黑體", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(136)));
            this.CanelSpiceButton.Location = new System.Drawing.Point(260, 314);
            this.CanelSpiceButton.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
            this.CanelSpiceButton.Name = "CanelSpiceButton";
            this.CanelSpiceButton.Size = new System.Drawing.Size(113, 42);
            this.CanelSpiceButton.TabIndex = 9;
            this.CanelSpiceButton.Text = "取消";
            this.CanelSpiceButton.UseVisualStyleBackColor = true;
            this.CanelSpiceButton.Click += new System.EventHandler(this.CanelSpiceButton_Click);
            // 
            // spiceStoreCombobox
            // 
            this.spiceStoreCombobox.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.spiceStoreCombobox.Font = new System.Drawing.Font("微軟正黑體", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(136)));
            this.spiceStoreCombobox.FormattingEnabled = true;
            this.spiceStoreCombobox.Items.AddRange(new object[] {
            "3",
            "5",
            "6"});
            this.spiceStoreCombobox.Location = new System.Drawing.Point(188, 172);
            this.spiceStoreCombobox.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
            this.spiceStoreCombobox.Name = "spiceStoreCombobox";
            this.spiceStoreCombobox.Size = new System.Drawing.Size(207, 28);
            this.spiceStoreCombobox.TabIndex = 10;
            // 
            // SpiceAddForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(7F, 16F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(490, 418);
            this.Controls.Add(this.spiceStoreCombobox);
            this.Controls.Add(this.CanelSpiceButton);
            this.Controls.Add(this.confirmSpiceButton);
            this.Controls.Add(this.spiceUnitTextbox);
            this.Controls.Add(this.label4);
            this.Controls.Add(this.label3);
            this.Controls.Add(this.spiceNumberTextbox);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.spiceNameTextbox);
            this.Font = new System.Drawing.Font("微軟正黑體", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(136)));
            this.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
            this.Name = "SpiceAddForm";
            this.Text = "新增香料";
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.TextBox spiceNameTextbox;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.TextBox spiceNumberTextbox;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.TextBox spiceUnitTextbox;
        private System.Windows.Forms.Button confirmSpiceButton;
        private System.Windows.Forms.Button CanelSpiceButton;
        private System.Windows.Forms.ComboBox spiceStoreCombobox;
    }
}