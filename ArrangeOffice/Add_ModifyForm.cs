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
    public partial class Add_ModifyForm : Form
    {
        private int control = 0;
        private int index = 0;
        ArrangeOffice form;
        public Add_ModifyForm()
        {
            InitializeComponent();
        }
        public void pushForm(ArrangeOffice form) 
        {
            this.form = form;
             
        }
        public void pushLabel(string name,string number ,string unit)
        {
            nameLabel.Text = name;
            numberLabel.Text = number;
            unitLabel.Text = unit;
        
        }

        private void confirmButton_Click(object sender, EventArgs e)
        {
            if (unitTextbox.Text == "")
            {
                DialogResult mDialogResult = MessageBox.Show("請輸入香料數量", "警告", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                if (mDialogResult == DialogResult.OK)
                    return;
            }
            if (control == 1)
            {
                form.addRecipeRow(unitTextbox.Text,index);
            }
            else if (control == 2) 
            {
                form.modifyRecipeRow(unitTextbox.Text);
            }

            this.Close();
        }

        private void unitTextbox_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (!char.IsControl(e.KeyChar) && !char.IsDigit(e.KeyChar) &&
              (e.KeyChar != '.'))
            {
                e.Handled = true;
            }

            // only allow one decimal point
            if ((e.KeyChar == '.') && ((sender as TextBox).Text.IndexOf('.') > -1))
            {
                e.Handled = true;
            }
        }

        private void exitButton_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        public void changControl(string state)
        {
            switch (state)
            {
                case "add_weight":
                    control = 1;
                    this.Text = "新增香料";
                    break;
                case "add_modify":
                    control = 2;
                    this.Text = "修改香料數據";
                    break;
                  

            }

        }
        public void currentIndex(int index) 
        {
            this.index = index;
        
        }
    }
}
