using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace ArrangeOffice
{
    public partial class Add_RecipeForm : Form
    {
        private ArrangeOffice form;
        public Add_RecipeForm()
        {
            InitializeComponent();
        }

        public void pushForm(ArrangeOffice form) 
        {
            this.form = form;     
        }

        private void addButton_Click(object sender, EventArgs e)
        {

            string checkNumber = "";
            checkNumber = Recipe_NumberTextBox.Text.ToString();


            if (Recipe_NameTextBox.Text == "")
            {
                DialogResult mDialogResult = MessageBox.Show("請輸入配方名稱", "警告", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                if (mDialogResult == DialogResult.OK)
                    return;
            }
            else if (Recipe_NumberTextBox.Text == "") 
            {
                DialogResult mDialogResult = MessageBox.Show("請輸入配方號碼", "警告", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                if (mDialogResult == DialogResult.OK)
                    return;
            
            }
            else if (checkNumber.Length != 14) 
            {
                DialogResult mDialogResult = MessageBox.Show("配方號碼格式錯誤", "警告", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                if (mDialogResult == DialogResult.OK)
                    return;
          
            }
            else if (checkNumber.IndexOf("-", 0, 14) != 1) 
            {
                DialogResult mDialogResult = MessageBox.Show("配方號碼格式錯誤", "警告", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                if (mDialogResult == DialogResult.OK)
                    return;
            
            }


           // Console.WriteLine(checkNumber.IndexOf("-", 0, 5));

            form.send("QUERY\tALL_SPICE<END>");

            form.comfirmAddRecipe(Recipe_NameTextBox.Text,Recipe_NumberTextBox.Text);
            this.Close();
        }

        private void noAddButton_Click(object sender, EventArgs e)
        {
            this.Close();


        }

        private void Recipe_NumberTextBox_KeyPress(object sender, KeyPressEventArgs e)
        {
            
            if (!char.IsControl(e.KeyChar) && !char.IsDigit(e.KeyChar) &&
             (e.KeyChar != '-'))
            {
                e.Handled = true;
            }

            // only allow one decimal point
            if ((e.KeyChar == '-') && ((sender as TextBox).Text.IndexOf('-') > -1))
            {
                e.Handled = true;
            }
             
        }

        
    }
}
