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
    public partial class Confirm_RecipeForm : Form
    {
        private ArrangeOffice form;
        private string number;
        private int control = 0;
        private string bucket = "";

        public Confirm_RecipeForm()
        {
            InitializeComponent();
        }

        public void pushMainForm(ArrangeOffice form){
        
             this.form = form;
        
        }
        public void pushLabel(string name , string number) 
        {
            this.number = number;
            recipeNameLabel.Text = name;
            recipeNumberLabel.Text = number;
            
           
        }

        private void confirmButton_Click(object sender, EventArgs e)
        {
            if (control == 1)
            {

                form.send("EXE\tRECIPE\t" + number + "<END>");
                this.Close();
            }
            else if (control == 2) 
            {
                form.sendNewRecipe();
                this.Close();
            }
            else if (control == 3) 
            {
                form.modifyRecipe();
                this.Close();
            }
            else if (control == 4) 
            {
                form.deleteRecipe();
                this.Close();
            
            }
            else if (control == 5) 
            {
                form.deleteSpiceRow();
                this.Close();
            
            }
            else if (control == 6) 
            {
                assignRecipeBucket();
                this.Close();
            
            }

        }

        public void assignRecipeBucket() 
        {

            form.send("EXE\tCHANGE_RECIPE\t"+bucket+"\t"+number+"<END>");
          //  Console.WriteLine("EXE\tCHANGE_RECIPE\t" + bucket + "\t" + number + "<END>");
        }

        public void pushBucket(string str) 
        {
            bucket = str;
            
        }


        private void noButton_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        public void changControl(string state) 
        {
            switch(state)
            {
                case "actionRecipe":
                    control = 1;
                    this.Text = "進行配料";
                    break;
                case "addRecipe":
                    control = 2;
                    this.Text = "新增配方";
                    confirmButton.Text = "新增";
                    break;
                case "modifyRecipe":
                    control = 3;
                    this.Text = "修改配方";
                    confirmButton.Text = "修改";
                    break;
                case "deleteRecipe":
                    control = 4;
                    this.Text = "刪除配方";
                    confirmButton.Text = "刪除";
                    break;
                case "deleteSpice":
                    control = 5;
                    this.Text = "刪除香料";
                    confirmButton.Text = "刪除";
                    break;
                case "directRecipe":
                    control = 6;
                    this.Text = "指定配料";
                    confirmButton.Text = "確定";
                    break;


            
            }
        
        }
    }
}
