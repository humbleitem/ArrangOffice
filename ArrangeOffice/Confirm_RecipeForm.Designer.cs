namespace ArrangeOffice
{
    partial class Confirm_RecipeForm
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
            this.confirmButton = new System.Windows.Forms.Button();
            this.RecipeName = new System.Windows.Forms.Label();
            this.noButton = new System.Windows.Forms.Button();
            this.recipeNumber = new System.Windows.Forms.Label();
            this.recipeNameLabel = new System.Windows.Forms.Label();
            this.recipeNumberLabel = new System.Windows.Forms.Label();
            this.SuspendLayout();
            // 
            // confirmButton
            // 
            this.confirmButton.Font = new System.Drawing.Font("微軟正黑體", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(136)));
            this.confirmButton.Location = new System.Drawing.Point(107, 231);
            this.confirmButton.Name = "confirmButton";
            this.confirmButton.Size = new System.Drawing.Size(76, 31);
            this.confirmButton.TabIndex = 0;
            this.confirmButton.Text = "確定";
            this.confirmButton.UseVisualStyleBackColor = true;
            this.confirmButton.Click += new System.EventHandler(this.confirmButton_Click);
            // 
            // RecipeName
            // 
            this.RecipeName.AutoSize = true;
            this.RecipeName.Font = new System.Drawing.Font("微軟正黑體", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(136)));
            this.RecipeName.Location = new System.Drawing.Point(42, 63);
            this.RecipeName.Name = "RecipeName";
            this.RecipeName.Size = new System.Drawing.Size(89, 20);
            this.RecipeName.TabIndex = 2;
            this.RecipeName.Text = "配方名稱：";
            // 
            // noButton
            // 
            this.noButton.Font = new System.Drawing.Font("微軟正黑體", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(136)));
            this.noButton.Location = new System.Drawing.Point(226, 231);
            this.noButton.Name = "noButton";
            this.noButton.Size = new System.Drawing.Size(76, 31);
            this.noButton.TabIndex = 3;
            this.noButton.Text = "取消";
            this.noButton.UseVisualStyleBackColor = true;
            this.noButton.Click += new System.EventHandler(this.noButton_Click);
            // 
            // recipeNumber
            // 
            this.recipeNumber.AutoSize = true;
            this.recipeNumber.Font = new System.Drawing.Font("微軟正黑體", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(136)));
            this.recipeNumber.Location = new System.Drawing.Point(42, 132);
            this.recipeNumber.Name = "recipeNumber";
            this.recipeNumber.Size = new System.Drawing.Size(89, 20);
            this.recipeNumber.TabIndex = 4;
            this.recipeNumber.Text = "配方編號：";
            // 
            // recipeNameLabel
            // 
            this.recipeNameLabel.AutoSize = true;
            this.recipeNameLabel.Font = new System.Drawing.Font("微軟正黑體", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(136)));
            this.recipeNameLabel.Location = new System.Drawing.Point(154, 63);
            this.recipeNameLabel.Name = "recipeNameLabel";
            this.recipeNameLabel.Size = new System.Drawing.Size(13, 20);
            this.recipeNameLabel.TabIndex = 5;
            this.recipeNameLabel.Text = " ";
            // 
            // recipeNumberLabel
            // 
            this.recipeNumberLabel.AutoSize = true;
            this.recipeNumberLabel.Font = new System.Drawing.Font("微軟正黑體", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(136)));
            this.recipeNumberLabel.Location = new System.Drawing.Point(154, 132);
            this.recipeNumberLabel.Name = "recipeNumberLabel";
            this.recipeNumberLabel.Size = new System.Drawing.Size(13, 20);
            this.recipeNumberLabel.TabIndex = 6;
            this.recipeNumberLabel.Text = " ";
            // 
            // Confirm_RecipeForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(414, 296);
            this.Controls.Add(this.recipeNumberLabel);
            this.Controls.Add(this.recipeNameLabel);
            this.Controls.Add(this.recipeNumber);
            this.Controls.Add(this.noButton);
            this.Controls.Add(this.RecipeName);
            this.Controls.Add(this.confirmButton);
            this.Name = "Confirm_RecipeForm";
            this.Text = "進行配料";
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Button confirmButton;
        private System.Windows.Forms.Label RecipeName;
        private System.Windows.Forms.Button noButton;
        private System.Windows.Forms.Label recipeNumber;
        private System.Windows.Forms.Label recipeNameLabel;
        private System.Windows.Forms.Label recipeNumberLabel;
    }
}