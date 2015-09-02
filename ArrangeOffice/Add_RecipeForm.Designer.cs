namespace ArrangeOffice
{
    partial class Add_RecipeForm
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
            this.label1 = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.Recipe_NameTextBox = new System.Windows.Forms.TextBox();
            this.Recipe_NumberTextBox = new System.Windows.Forms.TextBox();
            this.addButton = new System.Windows.Forms.Button();
            this.noAddButton = new System.Windows.Forms.Button();
            this.SuspendLayout();
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Font = new System.Drawing.Font("微軟正黑體", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(136)));
            this.label1.Location = new System.Drawing.Point(69, 58);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(89, 20);
            this.label1.TabIndex = 0;
            this.label1.Text = "配方名稱：";
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Font = new System.Drawing.Font("微軟正黑體", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(136)));
            this.label2.Location = new System.Drawing.Point(69, 132);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(89, 20);
            this.label2.TabIndex = 1;
            this.label2.Text = "配方號碼：";
            // 
            // Recipe_NameTextBox
            // 
            this.Recipe_NameTextBox.Font = new System.Drawing.Font("微軟正黑體", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(136)));
            this.Recipe_NameTextBox.Location = new System.Drawing.Point(164, 55);
            this.Recipe_NameTextBox.Name = "Recipe_NameTextBox";
            this.Recipe_NameTextBox.Size = new System.Drawing.Size(202, 29);
            this.Recipe_NameTextBox.TabIndex = 2;
            // 
            // Recipe_NumberTextBox
            // 
            this.Recipe_NumberTextBox.Font = new System.Drawing.Font("微軟正黑體", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(136)));
            this.Recipe_NumberTextBox.Location = new System.Drawing.Point(164, 129);
            this.Recipe_NumberTextBox.Name = "Recipe_NumberTextBox";
            this.Recipe_NumberTextBox.Size = new System.Drawing.Size(202, 29);
            this.Recipe_NumberTextBox.TabIndex = 3;
            this.Recipe_NumberTextBox.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.Recipe_NumberTextBox_KeyPress);
            // 
            // addButton
            // 
            this.addButton.Font = new System.Drawing.Font("微軟正黑體", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(136)));
            this.addButton.Location = new System.Drawing.Point(89, 205);
            this.addButton.Name = "addButton";
            this.addButton.Size = new System.Drawing.Size(87, 28);
            this.addButton.TabIndex = 4;
            this.addButton.Text = "新增配方";
            this.addButton.UseVisualStyleBackColor = true;
            this.addButton.Click += new System.EventHandler(this.addButton_Click);
            // 
            // noAddButton
            // 
            this.noAddButton.Font = new System.Drawing.Font("微軟正黑體", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(136)));
            this.noAddButton.Location = new System.Drawing.Point(217, 205);
            this.noAddButton.Name = "noAddButton";
            this.noAddButton.Size = new System.Drawing.Size(87, 28);
            this.noAddButton.TabIndex = 5;
            this.noAddButton.Text = "取消新增";
            this.noAddButton.UseVisualStyleBackColor = true;
            this.noAddButton.Click += new System.EventHandler(this.noAddButton_Click);
            // 
            // Add_RecipeForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(399, 266);
            this.Controls.Add(this.noAddButton);
            this.Controls.Add(this.addButton);
            this.Controls.Add(this.Recipe_NumberTextBox);
            this.Controls.Add(this.Recipe_NameTextBox);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.label1);
            this.Name = "Add_RecipeForm";
            this.Text = "新增配方";
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.TextBox Recipe_NameTextBox;
        private System.Windows.Forms.TextBox Recipe_NumberTextBox;
        private System.Windows.Forms.Button addButton;
        private System.Windows.Forms.Button noAddButton;
    }
}