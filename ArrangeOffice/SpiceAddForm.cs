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
    public partial class SpiceAddForm : Form
    {
        private ArrangeOffice form;
        private int control = 0;

        public SpiceAddForm()
        {
            InitializeComponent();
        }

        public void pushForm(ArrangeOffice form)
        {
            this.form = form;

        }
        public void changControl(string state)
        {
            switch (state)
            {
                case "addSpice":
                    control = 1;
                    this.Text = "新增香料";
                    confirmSpiceButton.Text = "新增香料";
                    break;
                case "modifySpice":
                    control = 2;
                    this.Text = "修改香料";
                    confirmSpiceButton.Text = "修改香料";
                    break;




            }

        }

        private void confirmSpiceButton_Click(object sender, EventArgs e)
        {


            if (spiceNameTextbox.Text == "")
            {
                DialogResult mDialogResult = MessageBox.Show("請輸入香料名稱", "警告", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                if (mDialogResult == DialogResult.OK)
                    return;
            }
            else if (spiceNumberTextbox.Text == "")
            {
                DialogResult mDialogResult = MessageBox.Show("請輸入香料號碼", "警告", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                if (mDialogResult == DialogResult.OK)
                    return;

            }
            else if (spiceStoreCombobox.Text == "")
            {
                DialogResult mDialogResult = MessageBox.Show("請選擇存放倉位", "警告", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                if (mDialogResult == DialogResult.OK)
                    return;

            }
            else if (spiceUnitTextbox.Text == "")
            {
                DialogResult mDialogResult = MessageBox.Show("請輸入香料單位", "警告", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                if (mDialogResult == DialogResult.OK)
                    return;

            }

            else if (spiceNumberTextbox.Text.Length != 14)
            {
                DialogResult mDialogResult = MessageBox.Show("香料號碼格式錯誤", "警告", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                if (mDialogResult == DialogResult.OK)
                    return;

            }
            else if (spiceNumberTextbox.Text.IndexOf("-", 0, 14) != 1)
            {
                DialogResult mDialogResult = MessageBox.Show("香料號碼格式錯誤", "警告", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                if (mDialogResult == DialogResult.OK)
                    return;

            }
            string[] spice = 
            {
                    spiceNumberTextbox.Text
                   ,spiceStoreCombobox.Text
                   ,spiceNameTextbox.Text
                   ,spiceUnitTextbox.Text };

            if (control == 1)
            {
                form.addSpiceRow(spice);                
                this.Close();
            }
            else if (control == 2)
            {
                form.modifySpiceRow(spice);     
                this.Close();

            }


        }

        private void spiceNumberTextbox_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (!char.IsControl(e.KeyChar) && !char.IsDigit(e.KeyChar) &&
             (e.KeyChar != '-'))
            {
                e.Handled = true;
            }

            // only allow one decimal point
            if ((e.KeyChar == '-') && ((sender as TextBox).Text.IndexOf('.') > -1))
            {
                e.Handled = true;
            }
        }

        private void CanelSpiceButton_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        public void pushLabel(string name,string number,string store,string unit) 
        {
            spiceNameTextbox.Text = name;
            spiceNumberTextbox.Text = number;
            spiceStoreCombobox.Text = store;
            spiceUnitTextbox.Text = unit;
        
        }
    }
}
