using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Data;

namespace ArrangeOffice
{
    partial class ArrangeOffice
    {
        
        private int num = 5;
        //如果一開始斷線 顯示斷線
        private void timer_Tick(object sender, EventArgs e)
        {
            clientState.ForeColor = System.Drawing.Color.Red;
            clientState.Text = "目前狀態 : 斷線 " + num + "秒後重新連接";
            num = num - 1;
        }
        private void timerLogin_Tick(object sender, EventArgs e)
        {
            timerLogin.Stop();
            login();
        }

        private int count = 0;//倒數
        //計算倒數5次重新啟動
        private void clientState_TextChanged(object sender, EventArgs e)
        {
            //倒數計時 時間到 重新啟動
            count = count + 1;
            if (count == 6)
            {
                count = 0;
              //  Application.Restart();
            }
        }

        private void Exit_ToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (MessageBox.Show("確定要離開嗎??", "警告", MessageBoxButtons.OKCancel, MessageBoxIcon.Warning) == DialogResult.OK)
            {
                client.stop();
                this.Close();
            }
            //Console.WriteLine(control.showControl());
        }

        private void WH_3_REC_ToolStripMenuItem_Click(object sender, EventArgs e)
        {
            hidePanel();
            control.changControl("log_3");
            initialDateTable();
            send("QUERY\tWH_HISTORY\t3\t" + currentTime.Year + "\t" + currentTime.Month + "\t" + currentTime.Day + "<END>");
            WH_REC_label.Text = "3 號倉庫："+currentTime.Year+"年 "+currentTime.Month+"月 "+currentTime.Day+"日 進出貨情形";           
            WH_REC_Panel.Visible = true;
        }

        private void WH_3_NOW_ToolStripMenuItem_Click(object sender, EventArgs e)
        {
            hidePanel();
            control.changControl("reserve_3");
            initialDateTable();
            send("QUERY\tWH_NOW\t3\t" + currentTime.Year + "\t" + currentTime.Month + "\t" + currentTime.Day + "<END>");
            WH_REC_label.Text = "3 號倉庫：" + currentTime.Year + "年 " + currentTime.Month + "月 " + currentTime.Day + "日 庫存情形";
            WH_REC_Panel.Visible = true;          
        }

        private void WH_5_REC_ToolStripMenuItem_Click(object sender, EventArgs e)
        {
            hidePanel();
            control.changControl("log_5");
            initialDateTable();
            send("QUERY\tWH_HISTORY\t5\t" + currentTime.Year + "\t" + currentTime.Month + "\t" + currentTime.Day + "<END>");
            WH_REC_label.Text = "5 號倉庫：" + currentTime.Year + "年 " + currentTime.Month + "月 " + currentTime.Day + "日 進出貨情形";
            WH_REC_Panel.Visible = true;          
        }

        private void WH_5_NOW_ToolStripMenuItem_Click(object sender, EventArgs e)
        {
            hidePanel();
            control.changControl("reserve_5");
            initialDateTable();
            send("QUERY\tWH_NOW\t5\t" + currentTime.Year + "\t" + currentTime.Month + "\t" + currentTime.Day + "<END>");
            WH_REC_label.Text = "5 號倉庫：" + currentTime.Year + "年 " + currentTime.Month + "月 " + currentTime.Day + "日 庫存情形";
            WH_REC_Panel.Visible = true;          
        }

        private void WH_6_REC_ToolStripMenuItem_Click(object sender, EventArgs e)
        {
            hidePanel();
            control.changControl("log_6");
            initialDateTable();
            send("QUERY\tWH_HISTORY\t6\t" + currentTime.Year + "\t" + currentTime.Month + "\t" + currentTime.Day + "<END>");
            WH_REC_label.Text = "6 號倉庫：" + currentTime.Year + "年 " + currentTime.Month + "月 " + currentTime.Day + "日 進出貨情形";
            WH_REC_Panel.Visible = true;
        }

        private void WH_6_NOW_ToolStripMenuItem_Click(object sender, EventArgs e)
        {
            hidePanel();
            control.changControl("reserve_6");
            initialDateTable();
            send("QUERY\tWH_NOW\t6\t" + currentTime.Year + "\t" + currentTime.Month + "\t" + currentTime.Day + "<END>");
            WH_REC_label.Text = "6 號倉庫：" + currentTime.Year + "年 " + currentTime.Month + "月 " + currentTime.Day + "日 庫存情形";
            WH_REC_Panel.Visible = true;
        }

        private void WH_History_ToolStripMenuItem_Click(object sender, EventArgs e)
        {
            hidePanel();
            initialDateTable();
            WH_REC_History_Year_ComboBox.Items.Clear();
            WH_REC_History_Month_ComboBox.Items.Clear();
            WH_REC_History_Day_ComboBox.Items.Clear();

            //選倉庫
            Dictionary<string, int> myList = new Dictionary<string,int>();
            myList.Add("3號倉庫", 1);
            myList.Add("5號倉庫", 2);
            myList.Add("6號倉庫", 3);
            WH_REC_History_WHNo_ComboBox.DataSource = new BindingSource(myList,null);
            WH_REC_History_WHNo_ComboBox.DisplayMember = "key";
            WH_REC_History_WHNo_ComboBox.ValueMember = "value";

            //加年份
            for (int i = 2015; i <= currentTime.Year; i++) {
                WH_REC_History_Year_ComboBox.Items.Add(i+"年");
            }

            //選type
            myList = new Dictionary<string, int>();
            myList.Add("進出貨情形", 4);
            myList.Add("庫存情況", 5);
            WH_REC_History_Type_ComboBox.DataSource = new BindingSource(myList, null);
            WH_REC_History_Type_ComboBox.DisplayMember = "key";
            WH_REC_History_Type_ComboBox.ValueMember = "value";

            WH_REC_History_Panel.Visible = true;
            WH_REC_Panel.Visible = true;
            WH_REC_dataGridView.Visible = false;
            

        }
        private void WH_REC_History_Year_ComboBox_SelectionChangeCommitted(object sender, EventArgs e)
        {
            WH_REC_History_Month_ComboBox.Items.Clear();
            WH_REC_History_Day_ComboBox.Items.Clear();
            string year = WH_REC_History_Year_ComboBox.Text.Remove(WH_REC_History_Year_ComboBox.Text.Length - 1);
            //判斷是不是現在這一年
            if (int.Parse(year) == currentTime.Year)
            {

                for (int i = 1; i <= currentTime.Month; i++)
                {
                    WH_REC_History_Month_ComboBox.Items.Add(i + "月");
                }
            }
            else {
                for (int i = 1; i <= 12; i++) 
                {
                    WH_REC_History_Month_ComboBox.Items.Add(i + "月");
                }
                       
            }

        }
        private void WH_REC_History_Month_ComboBox_SelectionChangeCommitted(object sender, EventArgs e) 
        {        

            WH_REC_History_Day_ComboBox.Items.Clear();    

            string year = WH_REC_History_Year_ComboBox.Text.Remove(WH_REC_History_Year_ComboBox.Text.Length - 1);
            string month = WH_REC_History_Month_ComboBox.Text.Remove(WH_REC_History_Month_ComboBox.Text.Length - 1);
            //判斷是不是今年今月
            if (int.Parse(year) == currentTime.Year && int.Parse(month) == currentTime.Month)
            {
                for (int i = 1; i <currentTime.Day; i++)
                {
                    WH_REC_History_Day_ComboBox.Items.Add(i + "日");
                }
            }
            else 
            {
                for (int i = 1; i <= DateTime.DaysInMonth(int.Parse(year),int.Parse(month)); i++)
                {
                    WH_REC_History_Day_ComboBox.Items.Add(i + "日");
                }
            }
        
        }

        private void WH_REC_History_Search_Button_Click(object sender, EventArgs e)
        {
           
            // change control history_log or history_reserve
            if (Convert.ToInt32(WH_REC_History_Type_ComboBox.SelectedValue) == 4)
            {
                control.changControl("history_log");
            }
            else
            {
               
                control.changControl("history_reserve");
            }


            if (WH_REC_History_Year_ComboBox.Text == "") {
                DialogResult mDialogResult = MessageBox.Show("請選擇年份", "警告", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                if (mDialogResult == DialogResult.OK)
                      return ;              
            }
            else if (WH_REC_History_Month_ComboBox.Text == "") {
                DialogResult mDialogResult = MessageBox.Show("請選擇月份", "警告", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                if (mDialogResult == DialogResult.OK)
                    return;        
            }
            else if (WH_REC_History_Day_ComboBox.Text == "") {
                DialogResult mDialogResult = MessageBox.Show("請選擇幾號", "警告", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                if (mDialogResult == DialogResult.OK)
                    return;    
            }

            string year = WH_REC_History_Year_ComboBox.Text.Remove(WH_REC_History_Year_ComboBox.Text.Length - 1);
            string month = WH_REC_History_Month_ComboBox.Text.Remove(WH_REC_History_Month_ComboBox.Text.Length - 1);
            string day = WH_REC_History_Day_ComboBox.Text.Remove(WH_REC_History_Day_ComboBox.Text.Length - 1);

            initialDateTable();

            sendHW(Convert.ToInt32(WH_REC_History_WHNo_ComboBox.SelectedValue)
                 , Convert.ToInt32(WH_REC_History_Type_ComboBox.SelectedValue)
                , int.Parse(year)
                , int.Parse(month)
                , int.Parse(day));

            //show datagridview
            WH_REC_dataGridView.Visible = true;

        }

        private void OnlineStatusToolStripMenuItem_Click(object sender, EventArgs e)
        {
            hidePanel();
            statePanel.Visible = true;
            control.changControl("onlineState");
            send("QUERY\tONLINE_STATE<END>");
        }

        private void SH_REC_ToolStripMenuItem_Click(object sender, EventArgs e)
        {
            hidePanel();
            SH_REC_LOG_Panel.Visible = true;
            send("QUERY\tSH_HISTORY\t"+currentTime.Year+"\t"+currentTime.Month +"\t"+currentTime.Day+"<END>");
            SH_LOG_dataGridView.Visible = true;
            initialDateTable();
            sh_nowLabel.Text = "線邊倉：" + currentTime.Year + "年 " + currentTime.Month + "月 " + currentTime.Day + "日 進出貨情形"; 
            control.changControl("sh_now_log");

        }

        private void SH_REC_History_ToolStripMenuItem_Click(object sender, EventArgs e)
        {
            hidePanel();
            SH_REC_Panel.Visible = true;
            SH_REC_DataGridView.Visible = false;
            initialDateTable();
            control.changControl("sh_log");

            SH_REC_Year_ComboBox.Items.Clear();
            SH_REC_Month_ComboBox.Items.Clear();
            SH_REC_Day_ComboBox.Items.Clear();

            //加年份
            for (int i = 2015; i <= currentTime.Year; i++)
            {
                SH_REC_Year_ComboBox.Items.Add(i + "年");
            }
        }
        
        private void SH_REC_Year_ComboBox_SelectionChangeCommitted(object sender, EventArgs e)
        {
            SH_REC_Month_ComboBox.Items.Clear();
            SH_REC_Day_ComboBox.Items.Clear();
            string year = SH_REC_Year_ComboBox.Text.Remove(SH_REC_Year_ComboBox.Text.Length - 1);
            //判斷是不是現在這一年
            if (int.Parse(year) == currentTime.Year)
            {

                for (int i = 1; i <= currentTime.Month; i++)
                {
                    SH_REC_Month_ComboBox.Items.Add(i + "月");
                }
            }
            else
            {
                for (int i = 1; i <= 12; i++)
                {
                    SH_REC_Month_ComboBox.Items.Add(i + "月");
                }

            }

        }

        private void SH_REC_Month_ComboBox_SelectionChangeCommitted(object sender, EventArgs e)
        {
            SH_REC_Day_ComboBox.Items.Clear();

            string year = SH_REC_Year_ComboBox.Text.Remove(SH_REC_Year_ComboBox.Text.Length - 1);
            string month = SH_REC_Month_ComboBox.Text.Remove(SH_REC_Month_ComboBox.Text.Length - 1);
            //判斷是不是今年今月
            if (int.Parse(year) == currentTime.Year && int.Parse(month) == currentTime.Month)
            {
                for (int i = 1; i < currentTime.Day; i++)
                {
                    SH_REC_Day_ComboBox.Items.Add(i + "日");
                }
            }
            else
            {
                for (int i = 1; i <= DateTime.DaysInMonth(int.Parse(year), int.Parse(month)); i++)
                {
                    SH_REC_Day_ComboBox.Items.Add(i + "日");
                }
            }

        }

        private void SH_REC_Search_Button_Click(object sender, EventArgs e)
        {
           // send("QUERY SH_HISTORY 2015 8 19<END>");


            if (SH_REC_Year_ComboBox.Text == "")
            {
                DialogResult mDialogResult = MessageBox.Show("請選擇年份", "警告", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                if (mDialogResult == DialogResult.OK)
                    return;
            }
            else if (SH_REC_Month_ComboBox.Text == "")
            {
                DialogResult mDialogResult = MessageBox.Show("請選擇月份", "警告", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                if (mDialogResult == DialogResult.OK)
                    return;
            }
            else if (SH_REC_Day_ComboBox.Text == "")
            {
                DialogResult mDialogResult = MessageBox.Show("請選擇幾號", "警告", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                if (mDialogResult == DialogResult.OK)
                    return;
            }

            string year = SH_REC_Year_ComboBox.Text.Remove(SH_REC_Year_ComboBox.Text.Length - 1);
            string month = SH_REC_Month_ComboBox.Text.Remove(SH_REC_Month_ComboBox.Text.Length - 1);
            string day = SH_REC_Day_ComboBox.Text.Remove(SH_REC_Day_ComboBox.Text.Length - 1);

            initialDateTable();

            send("QUERY\tSH_HISTORY\t" + int.Parse(year) + "\t" + int.Parse(month) + "\t" + int.Parse(day)+"<END>");

            SH_REC_DataGridView.Visible = true;
            
        }
        private void SH_Amount_ToolStripMenuItem_Click(object sender, EventArgs e)
        {
            hidePanel();
            SH_Amount_Panel.Visible = true;
            initialDateTable();
            control.changControl("sh_reserve");
            send("QUERY\tSH_NOW<END>");

        }

        private void Burden_ToolStripMenuItem_Click(object sender, EventArgs e)
        {
            initialPanelItem();
            hidePanel();
            Del_Recipe_button.Visible = false;
            DB_Recipe_dataGridView.Visible = false;
            DB_AddFix_Recipe_panel.Visible = false;
            DB_Recipe_Panel.Visible = true;            
            initialDateTable();
            control.changControl("ac_name");
            send("QUERY\tRECIPE_NAME<END>");
           // send("QUERY RECIPE_DATA 2-111410407347<END>");
        }

        private void DB_Recipe_comboBox_SelectionChangeCommitted(object sender, EventArgs e)
        {
            control.changControl("ac_content");
            initialDateTable();
            DB_Recipe_dataGridView.Visible = true;
            DB_Recipe_Serial_label.Text = DB_Recipe_comboBox.SelectedValue.ToString();
            send("QUERY\tRECIPE_DATA\t"+DB_Recipe_comboBox.SelectedValue+"<END>");


        }

        private void Fix_Recipe_Confirm_button_Click(object sender, EventArgs e)
        {
            if (DB_Recipe_comboBox.Text == "") {
                DialogResult mDialogResult = MessageBox.Show("請選擇香料配方", "警告", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                if (mDialogResult == DialogResult.OK)
                    return;
            }

            else if (DB_Recipe_comboBox.SelectedValue == null ) {

                DialogResult mDialogResult = MessageBox.Show("請選擇香料配方", "警告", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                if (mDialogResult == DialogResult.OK)
                    return;
            }           

            Confirm_RecipeForm confirm_recipeForm = new Confirm_RecipeForm();
            confirm_recipeForm.pushMainForm(this);
            confirm_recipeForm.StartPosition = FormStartPosition.CenterScreen;
            confirm_recipeForm.changControl("actionRecipe");
            confirm_recipeForm.pushLabel(DB_Recipe_comboBox.Text, DB_Recipe_comboBox.SelectedValue.ToString());
            confirm_recipeForm.ShowDialog();


        }

        private void Fix_Recipe_Cancel_button_Click(object sender, EventArgs e)
        {
            initialDateTable();
            DB_Recipe_dataGridView.Visible = false;
            DB_Recipe_Serial_label.Text = "";
            DB_Recipe_comboBox.SelectedValue = 0;
        }


        private void Add_Recipe_ToolStripMenuItem_Click(object sender, EventArgs e)
        {
            initialPanelItem();
            hidePanel();
            initialDateTable(); 
            Add_Recipe_Panel.Visible = true;
            add_RecipeDatagridview.Visible = false;
           
        }

        private void Add_RecipeButton_Click(object sender, EventArgs e)
        {
            Add_RecipeForm addRecipeForm = new Add_RecipeForm();
            addRecipeForm.pushForm(this);
            control.changControl("add_recipe");
            addRecipeForm.StartPosition = FormStartPosition.CenterScreen;
            addRecipeForm.Show();
        }
        private void noAddButton_Click(object sender, EventArgs e)
        {
            
            plus_Recipe_Panel.Visible = false;
            Add_RecipeButton.Visible = true;
            add_RecipeDatagridview.Visible = false;
            Recipe_NameLabel.Text = "";
            Recipe_NumerLabel.Text = "";
            initialDateTable();

        }
        private void add_newRecipeButton_Click(object sender, EventArgs e)
        {
            Add_ModifyForm addWeightForm = new Add_ModifyForm();
            addWeightForm.pushForm(this);
            addWeightForm.changControl("add_weight");
            addWeightForm.currentIndex(chooseRecipeDateGridView.CurrentCell.RowIndex);
            addWeightForm.pushLabel(
            dataTableAddRecipe.Rows[chooseRecipeDateGridView.CurrentCell.RowIndex][2].ToString()
            , dataTableAddRecipe.Rows[chooseRecipeDateGridView.CurrentCell.RowIndex][0].ToString()
            , dataTableAddRecipe.Rows[chooseRecipeDateGridView.CurrentCell.RowIndex][3].ToString()
            );
            addWeightForm.StartPosition = FormStartPosition.CenterScreen;
            addWeightForm.Show();

            
                                                
        }        

        private void remove_newRecipeButton_Click(object sender, EventArgs e)
        {
            if (dataTableRecipe.Rows.Count == 0)
            {
                DialogResult mDialogResult = MessageBox.Show("未存在香料可以移除", "警告", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                if (mDialogResult == DialogResult.OK)
                    return;
            }

           dataTableRecipe.Rows.RemoveAt(add_RecipeDatagridview.CurrentCell.RowIndex);
           

           if (dataTableRecipe.Rows.Count == 0) 
           {
               //clear datagridview
               add_RecipeDatagridview.Visible = false;       
           }
           adjustDataGridView(add_RecipeDatagridview);         
            
        }
        
        private void modify_newRecipeButton_Click(object sender, EventArgs e)
        {
            if (dataTableRecipe.Rows.Count == 0)
            {
                DialogResult mDialogResult = MessageBox.Show("未存在香料可以修改", "警告", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                if (mDialogResult == DialogResult.OK)
                    return;
            }

            Add_ModifyForm add_modifyForm = new Add_ModifyForm();
            add_modifyForm.pushForm(this);
            add_modifyForm.changControl("add_modify");
            add_modifyForm.pushLabel(dataTableRecipe.Rows[add_RecipeDatagridview.CurrentRow.Index][2].ToString()
                                     ,dataTableRecipe.Rows[add_RecipeDatagridview.CurrentRow.Index][0].ToString()
                                     ,dataTableRecipe.Rows[add_RecipeDatagridview.CurrentRow.Index][4].ToString());
            add_modifyForm.StartPosition = FormStartPosition.CenterScreen;
            add_modifyForm.Show();
          


        }
        private void confirmButton_Click(object sender, EventArgs e)
        {

            if (dataTableRecipe.Rows.Count == 0)
            {
                DialogResult mDialogResult = MessageBox.Show("配方未存在香料", "警告", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                if (mDialogResult == DialogResult.OK)
                    return;
            }
           // Console.WriteLine(dataTableAddRecipe.Rows.Count);
            Confirm_RecipeForm confirm_recipeForm = new Confirm_RecipeForm();
            confirm_recipeForm.pushMainForm(this);
            confirm_recipeForm.StartPosition = FormStartPosition.CenterScreen;
            confirm_recipeForm.changControl("addRecipe");
            confirm_recipeForm.pushLabel(Recipe_NameLabel.Text
                                        ,Recipe_NumerLabel.Text);
            confirm_recipeForm.ShowDialog();
           
        }
        private void Modify_Recipe_ToolStripMenuItem_Click(object sender, EventArgs e)
        {
            initialPanelItem();
            hidePanel();
            initialDateTable();
            Modify_Recipe_Panel.Visible = true;
            modifyDataGridview.Visible = false;
            addModiy_Panel.Visible = false;
            control.changControl("modify_recipe_name");
            send("QUERY\tRECIPE_NAME<END>");

        }
        private void modifyRecipeNumber_TextBox_KeyPress(object sender, KeyPressEventArgs e)
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
        private void modify_recipe_combobox_SelectionChangeCommitted(object sender, EventArgs e)
        {
            control.changControl("modify_recipe_content");
            initialDateTable();
            modifyDataGridview.Visible = true;
            modifyRecipeNumber_TextBox.Text = modify_recipe_combobox.SelectedValue.ToString();
            //store number
            modifyOldRecipeNumber = modify_recipe_combobox.SelectedValue.ToString();
            send("QUERY\tRECIPE_DATA\t" + modify_recipe_combobox.SelectedValue + "<END>");
            
        }
        //修改配方
        private void modify_Recipe_Button_Click(object sender, EventArgs e)
        {
            if (dataTableRecipe.Rows.Count == 0  )
            {
                DialogResult mDialogResult = MessageBox.Show("未選擇修改配方", "警告", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                if (mDialogResult == DialogResult.OK)
                    return;
            }
            
            control.changControl("modify_recipe_all_spice");
            send("QUERY\tALL_SPICE<END>");

            //modifyDataGridview
            addModiy_Panel.Visible = true;
            modify_Recipe_Button.Visible = false;
            delete_Recipe_Button.Visible = false;
         
        }
        //修改配方 <-
        private void addModifyButton_Click(object sender, EventArgs e)
        {
            Add_ModifyForm addWeightForm = new Add_ModifyForm();
            addWeightForm.pushForm(this);
            addWeightForm.changControl("add_weight");
            addWeightForm.currentIndex(modify_allDataGridview.CurrentCell.RowIndex);
            addWeightForm.pushLabel(
            dataTableAddRecipe.Rows[modify_allDataGridview.CurrentCell.RowIndex][2].ToString()
            , dataTableAddRecipe.Rows[modify_allDataGridview.CurrentCell.RowIndex][0].ToString()
            , dataTableAddRecipe.Rows[modify_allDataGridview.CurrentCell.RowIndex][3].ToString()
            );
            addWeightForm.StartPosition = FormStartPosition.CenterScreen;
            addWeightForm.Show();


        }
        private void modifyRemoveButton_Click(object sender, EventArgs e)
        {
            if (dataTableRecipe.Rows.Count == 0)
            {
                DialogResult mDialogResult = MessageBox.Show("未存在香料可以移除", "警告", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                if (mDialogResult == DialogResult.OK)
                    return;
            }

            dataTableRecipe.Rows.RemoveAt(modifyDataGridview.CurrentCell.RowIndex);


            if (dataTableRecipe.Rows.Count == 0)
            {
                //clear datagridview
                modifyDataGridview.Visible = false;
            }
            adjustDataGridView(modifyDataGridview);     
        }
        private void modify_modifyButton_Click(object sender, EventArgs e)
        {
            if (dataTableRecipe.Rows.Count == 0)
            {
                DialogResult mDialogResult = MessageBox.Show("未存在香料可以修改", "警告", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                if (mDialogResult == DialogResult.OK)
                    return;
            }

            Add_ModifyForm add_modifyForm = new Add_ModifyForm();
            add_modifyForm.pushForm(this);
            add_modifyForm.changControl("add_modify");
            add_modifyForm.pushLabel(dataTableRecipe.Rows[modifyDataGridview.CurrentRow.Index][2].ToString()
                                     , dataTableRecipe.Rows[modifyDataGridview.CurrentRow.Index][0].ToString()
                                     , dataTableRecipe.Rows[modifyDataGridview.CurrentRow.Index][4].ToString());
            add_modifyForm.StartPosition = FormStartPosition.CenterScreen;
            add_modifyForm.Show();

        }
        private void MD_Canel_Button_Click(object sender, EventArgs e)
        {
            addModiy_Panel.Visible = false;
            modify_Recipe_Button.Visible = true;
            delete_Recipe_Button.Visible = true;
            modifyDataGridview.Visible = false;
            modify_recipe_combobox.SelectedValue = 0;
            modify_recipe_combobox.Text = "";
            modifyRecipeNumber_TextBox.Text = "";
          //  modifyNumberLabel.Text = "";
            initialDateTable();

        }

        private void MD_Confirm_Button_Click(object sender, EventArgs e)
        {
            if (dataTableRecipe.Rows.Count == 0)
            {
                DialogResult mDialogResult = MessageBox.Show("配方未存在香料", "警告", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                if (mDialogResult == DialogResult.OK)
                    return;
            }
            else if (modifyRecipeNumber_TextBox.Text.Length != 14)
            {
                DialogResult mDialogResult = MessageBox.Show("配方號碼格式錯誤", "警告", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                if (mDialogResult == DialogResult.OK)
                    return;

            }
            else if (modifyRecipeNumber_TextBox.Text.IndexOf("-", 0, 14) != 1)
            {
                DialogResult mDialogResult = MessageBox.Show("配方號碼格式錯誤", "警告", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                if (mDialogResult == DialogResult.OK)
                    return;

            }
            // Console.WriteLine(dataTableAddRecipe.Rows.Count);
            Confirm_RecipeForm confirm_recipeForm = new Confirm_RecipeForm();
            confirm_recipeForm.pushMainForm(this);
            confirm_recipeForm.StartPosition = FormStartPosition.CenterScreen;
            confirm_recipeForm.changControl("modifyRecipe");
            confirm_recipeForm.pushLabel(modify_recipe_combobox.Text
                // , modifyNumberLabel.Text);
                                          , modifyRecipeNumber_TextBox.Text);
            confirm_recipeForm.ShowDialog();

        }
        private void delete_Recipe_Button_Click(object sender, EventArgs e)
        {
            if (dataTableRecipe.Rows.Count == 0)
            {
                DialogResult mDialogResult = MessageBox.Show("配方未存在香料", "警告", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                if (mDialogResult == DialogResult.OK)
                    return;
            }

            Confirm_RecipeForm confirm_recipeForm = new Confirm_RecipeForm();
            confirm_recipeForm.pushMainForm(this);
            confirm_recipeForm.StartPosition = FormStartPosition.CenterScreen;
            confirm_recipeForm.changControl("deleteRecipe");
            confirm_recipeForm.pushLabel(modify_recipe_combobox.Text
                                          , modifyRecipeNumber_TextBox.Text);
            confirm_recipeForm.ShowDialog();
        }

        private void spice_ToolStripMenuItem_Click(object sender, EventArgs e)
        {
            initialPanelItem();
            hidePanel();
            spicePanel.Visible = true;
            initialDateTable();
            control.changControl("modify_spice");
            send("QUERY\tALL_SPICE<END>");

        }
        private void addSpiceButton_Click(object sender, EventArgs e)
        {
            SpiceAddForm addSpiceForm = new SpiceAddForm();
            addSpiceForm.pushForm(this);
            addSpiceForm.StartPosition = FormStartPosition.CenterScreen;
            addSpiceForm.changControl("addSpice");
            addSpiceForm.Show();


        }
        private void modifySpiceButton_Click(object sender, EventArgs e)
        {

            SpiceAddForm addSpiceForm = new SpiceAddForm();
            addSpiceForm.pushForm(this);
            addSpiceForm.StartPosition = FormStartPosition.CenterScreen;
            addSpiceForm.changControl("modifySpice");
            addSpiceForm.pushLabel(dataTableAddRecipe.Rows[allSpiceDataGridView.CurrentCell.RowIndex][2].ToString()
                                 , dataTableAddRecipe.Rows[allSpiceDataGridView.CurrentCell.RowIndex][0].ToString()
                                 , dataTableAddRecipe.Rows[allSpiceDataGridView.CurrentCell.RowIndex][1].ToString()
                                 , dataTableAddRecipe.Rows[allSpiceDataGridView.CurrentCell.RowIndex][3].ToString());
            addSpiceForm.Show();
        }

        private void deleteSpiceButton_Click(object sender, EventArgs e)
        {
            Confirm_RecipeForm removeSpiceForm = new Confirm_RecipeForm();
            removeSpiceForm.pushMainForm(this);
            removeSpiceForm.StartPosition = FormStartPosition.CenterScreen;
            removeSpiceForm.changControl("deleteSpice");

            removeSpiceForm.pushLabel(dataTableAddRecipe.Rows[allSpiceDataGridView.CurrentCell.RowIndex][3].ToString()
                                     ,dataTableAddRecipe.Rows[allSpiceDataGridView.CurrentCell.RowIndex][0].ToString());
            removeSpiceForm.ShowDialog();

        }

        private void directRecpie_ToolStripMenuItem_Click(object sender, EventArgs e)
        {    
            hidePanel();
            initialDateTable();
            directRecipeButton.Visible = true;
            assignRecipePanel.Visible = true;
            control.changControl("directRecipe");

            send("QUERY\tRECIPE_NOW<END>");


        }
        private void directRecipeButton_Click(object sender, EventArgs e)
        {
            directRecipeButton.Visible = false;

            chooseDirectRecpiePanel.Visible = true;
            control.changControl("directRecipeName");
            send("QUERY\tRECIPE_NAME<END>");

        }

        private void confirmDirectButton_1_Click(object sender, EventArgs e)
        {
            control.changControl("directRecipe");
            Confirm_RecipeForm directSpiceForm = new Confirm_RecipeForm();
            directSpiceForm.pushMainForm(this);
            directSpiceForm.StartPosition = FormStartPosition.CenterScreen;
            directSpiceForm.changControl("directRecipe");

            directSpiceForm.pushLabel(BucketComBoBox_1.Text
                                     , BucketComBoBox_1.SelectedValue.ToString());
            directSpiceForm.pushBucket("1");
            directSpiceForm.ShowDialog();

        }

        private void confirmDirectButton_2_Click(object sender, EventArgs e)
        {
            control.changControl("directRecipe");
            Confirm_RecipeForm directSpiceForm = new Confirm_RecipeForm();
            directSpiceForm.pushMainForm(this);
            directSpiceForm.StartPosition = FormStartPosition.CenterScreen;
            directSpiceForm.changControl("directRecipe");

            directSpiceForm.pushLabel(BucketComBoBox_2.Text
                                     , BucketComBoBox_2.SelectedValue.ToString());
            directSpiceForm.pushBucket("2");
            directSpiceForm.ShowDialog();
        }

        private void confirmDirectButton_3_Click(object sender, EventArgs e)
        {
            control.changControl("directRecipe");
            Confirm_RecipeForm directSpiceForm = new Confirm_RecipeForm();
            directSpiceForm.pushMainForm(this);
            directSpiceForm.StartPosition = FormStartPosition.CenterScreen;
            directSpiceForm.changControl("directRecipe");

            directSpiceForm.pushLabel(BucketComBoBox_3.Text
                                     , BucketComBoBox_3.SelectedValue.ToString());
            directSpiceForm.pushBucket("3");
            directSpiceForm.ShowDialog();
        }

        private void confirmDirectButton_4_Click(object sender, EventArgs e)
        {
            control.changControl("directRecipe");
            Confirm_RecipeForm directSpiceForm = new Confirm_RecipeForm();
            directSpiceForm.pushMainForm(this);
            directSpiceForm.StartPosition = FormStartPosition.CenterScreen;
            directSpiceForm.changControl("directRecipe");

            directSpiceForm.pushLabel(BucketComBoBox_4.Text
                                     , BucketComBoBox_4.SelectedValue.ToString());
            directSpiceForm.pushBucket("4");
            directSpiceForm.ShowDialog();
        }

        private void confirmDirectButton_5_Click(object sender, EventArgs e)
        {
            control.changControl("directRecipe");
            Confirm_RecipeForm directSpiceForm = new Confirm_RecipeForm();
            directSpiceForm.pushMainForm(this);
            directSpiceForm.StartPosition = FormStartPosition.CenterScreen;
            directSpiceForm.changControl("directRecipe");

            directSpiceForm.pushLabel(BucketComBoBox_5.Text
                                     , BucketComBoBox_5.SelectedValue.ToString());
            directSpiceForm.pushBucket("5");
            directSpiceForm.ShowDialog();
        }

        private void confirmDirectButton_6_Click(object sender, EventArgs e)
        {
            control.changControl("directRecipe");
            Confirm_RecipeForm directSpiceForm = new Confirm_RecipeForm();
            directSpiceForm.pushMainForm(this);
            directSpiceForm.StartPosition = FormStartPosition.CenterScreen;
            directSpiceForm.changControl("directRecipe");

            directSpiceForm.pushLabel(BucketComBoBox_6.Text
                                     , BucketComBoBox_6.SelectedValue.ToString());
            directSpiceForm.pushBucket("6");
            directSpiceForm.ShowDialog();
        }

        private void confirmDirectButton_7_Click(object sender, EventArgs e)
        {
            control.changControl("directRecipe");
            Confirm_RecipeForm directSpiceForm = new Confirm_RecipeForm();
            directSpiceForm.pushMainForm(this);
            directSpiceForm.StartPosition = FormStartPosition.CenterScreen;
            directSpiceForm.changControl("directRecipe");

            directSpiceForm.pushLabel(BucketComBoBox_7.Text
                                     , BucketComBoBox_7.SelectedValue.ToString());
            directSpiceForm.pushBucket("7");
            directSpiceForm.ShowDialog();
        }

        private void confirmDirectButton_8_Click(object sender, EventArgs e)
        {
            control.changControl("directRecipe");
            Confirm_RecipeForm directSpiceForm = new Confirm_RecipeForm();
            directSpiceForm.pushMainForm(this);
            directSpiceForm.StartPosition = FormStartPosition.CenterScreen;
            directSpiceForm.changControl("directRecipe");

            directSpiceForm.pushLabel(BucketComBoBox_8.Text
                                     , BucketComBoBox_8.SelectedValue.ToString());
            directSpiceForm.pushBucket("8");
            directSpiceForm.ShowDialog();
        }



    }
}
