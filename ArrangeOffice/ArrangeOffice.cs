using System;
using System.Collections.Generic;
using System.Collections.Specialized;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Threading;

namespace ArrangeOffice
{
    public partial class ArrangeOffice : Form
    {
        public ArrangeOffice()
        {
            InitializeComponent();
        }

        private Client client;
        private delegate void deleStatus(string status);
        private delegate void deleShowData(bool show, string[] content, int controlNumer);
        private delegate void deleStateShow(string[] state);
        private delegate void deleshowDialog();
        //     private delegate void deleDuplicate();
        private Control control;
        private DateTime currentTime;
        private DataTable dataTableLog;
        private DataTable dataTableReserve;
        private DataTable dataTableRecipe;
        private DataTable dataTableAddRecipe;
        //combobox ac_name
        private OrderedDictionary ACName;
        private string modifyOldRecipeNumber = "";

        private void ArrangeOffice_Load(object sender, EventArgs e)
        {
            //宣告全域變數
            formInitial();
            hidePanel();

            //建立連線
            client = new Client();

            if (!client.isError())
            {
                //剛連線傳遞CONNECT    
                Thread clientThread = new Thread(new ParameterizedThreadStart(client.run));
                clientThread.Start("CONNECT\tAC<END>");

                login();     

                client.pushForm(this);
            }
            //剛開始沒連到的話
            else
            {
                //一開始斷線 計時器 重新啟動
                timer.Interval = 1200;
                timer.Start();
            }

        }

        public void adjustButton()
        {
            int bidth = 120;
            int height = allSpiceDataGridView.Location.Y + allSpiceDataGridView.Size.Height + 50;
            addSpiceButton.Location = new Point(100, height);
            modifySpiceButton.Location = new Point(addSpiceButton.Location.X + addSpiceButton.Size.Width + bidth, height);
            deleteSpiceButton.Location = new Point(modifySpiceButton.Location.X + modifySpiceButton.Size.Width + bidth, height);

        }

        public void addRecipeRow(string unit, int index)
        {

            DataRow dataRow = dataTableRecipe.NewRow();
            DataRow sourceRow = dataTableAddRecipe.Rows[index];
            dataRow.ItemArray = sourceRow.ItemArray.Clone() as object[];
            dataTableRecipe.Rows.InsertAt(dataRow, 0);
            dataRow[4] = dataRow[3];
            dataRow[3] = unit;

            add_RecipeDatagridview.DataSource = dataTableRecipe;
            modifyDataGridview.DataSource = dataTableRecipe;
            noSort(modifyDataGridview);
            noSort(add_RecipeDatagridview);
            adjustDataGridView(modifyDataGridview);
            adjustDataGridView(add_RecipeDatagridview);
            add_RecipeDatagridview.Visible = true;
            modifyDataGridview.Visible = true;
        }

        public void addSpiceRow(string[] spiceString)
        {
            DataRow dataRow = dataTableAddRecipe.NewRow();
            dataRow.ItemArray = spiceString;
            dataTableAddRecipe.Rows.InsertAt(dataRow, 0);
            adjustDataGridView(allSpiceDataGridView);
            adjustButton();

            send("EXE\tADD_SPICE\t" + spiceString[0] + "\t" + spiceString[1] + "\t" + spiceString[2] + "\t" + spiceString[3] + "<END>");
         
        }

        public void close() 
        {
            client.stop();
            Application.Exit();
        
        }

        public void modifySpiceRow(string[] spiceString)
        {
            DataRow dataRow = dataTableAddRecipe.NewRow();
            dataRow.ItemArray = spiceString;

            string oldSpiceNumber = dataTableAddRecipe.Rows[allSpiceDataGridView.CurrentCell.RowIndex][0].ToString();

            dataTableAddRecipe.Rows.RemoveAt(allSpiceDataGridView.CurrentCell.RowIndex);
            dataTableAddRecipe.Rows.InsertAt(dataRow, 0);
            adjustDataGridView(allSpiceDataGridView);

            send("EXE\tFIX_SPICE\t" + oldSpiceNumber +"\t"+ spiceString[0] + "\t" + spiceString[1] + "\t" + spiceString[2] + "\t" + spiceString[3] + "<END>");
            
        }

        public void login() 
        {
            if (this.InvokeRequired)
            {
                deleshowDialog DD = new deleshowDialog(login);
                this.Invoke(DD);
            }
            else
            {
                loginForm loginForm = new loginForm();
                loginForm.pushForm(this);
                loginForm.StartPosition = FormStartPosition.CenterScreen;
                loginForm.ShowDialog();
            }
        
        }

        public void loginError() 
        {
            if (this.InvokeRequired)
            {
                deleshowDialog DD = new deleshowDialog(loginError);
                this.Invoke(DD);
            }
            else
            {
                DialogResult mDialogResult = MessageBox.Show("帳號或密碼錯誤", "錯誤", MessageBoxButtons.OK, MessageBoxIcon.Error);

                loginForm loginForm = new loginForm();
                loginForm.pushForm(this);
                loginForm.StartPosition = FormStartPosition.CenterScreen;
                loginForm.ShowDialog();
            }
        
        }

        public void loginTime() 
        {
            if (this.InvokeRequired)
            {
                deleshowDialog DD = new deleshowDialog(loginTime);
                this.Invoke(DD);
            }
            else
            {
                DialogResult mDialogResult = MessageBox.Show("登入成功", "成功", MessageBoxButtons.OK, MessageBoxIcon.Information);
                // almost 5 minute
             //   timerLogin.Interval = 3000;
                timerLogin.Interval = 72000;
                timerLogin.Start();
            }
        
        }

        public void deleteSpiceRow() 
        {

            dataTableAddRecipe.Rows.RemoveAt(allSpiceDataGridView.CurrentCell.RowIndex);
            string oldSpiceNumber = dataTableAddRecipe.Rows[allSpiceDataGridView.CurrentCell.RowIndex][0].ToString();
            adjustDataGridView(allSpiceDataGridView);
            adjustButton();

            send("EXE\tDEL_SPICE\t" + oldSpiceNumber + "<END>");
        }

        public void deleteRecipe()
        {

            send("EXE\tDEL_RECIPE\t" + modifyOldRecipeNumber + "<END>");



            addModiy_Panel.Visible = false;
            modify_Recipe_Button.Visible = true;
            delete_Recipe_Button.Visible = true;
            modifyDataGridview.Visible = false;
            modify_recipe_combobox.SelectedValue = 0;
            modify_recipe_combobox.Text = "";
            modifyRecipeNumber_TextBox.Text = "";
            initialDateTable();

            control.changControl("modify_recipe_name");
            send("QUERY\tRECIPE_NAME<END>");

        }

        public void duplicateRecipe()
        {
            if (this.InvokeRequired)
            {
                deleshowDialog DD = new deleshowDialog(duplicateRecipe);
                this.Invoke(DD);
            }
            else
            {
                //add recipe
                if (control.showControl() == 15)
                {

                    DialogResult mDialogResult = MessageBox.Show("配料編號已存在 新增配料失敗", "警告", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    if (mDialogResult == DialogResult.OK)
                        return;
                }
                else if (control.showControl() == 19) 
                {
                    //刪除第一列 如果編號重複
                    dataTableAddRecipe.Rows.RemoveAt(0);
                    adjustDataGridView(allSpiceDataGridView);
                    adjustButton();
                    DialogResult mDialogResult = MessageBox.Show("香料編號已存在 新增香料失敗", "警告", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    if (mDialogResult == DialogResult.OK)
                        return;
                }

            }


        }

        public void modifyRecipeRow(string unit)
        {
            dataTableRecipe.Rows[add_RecipeDatagridview.CurrentRow.Index][3] = unit;

        }

        public void modifyRecipe()
        {
            int last = 1;

            foreach (DataRow dr in dataTableRecipe.Rows)
            {
                if (dataTableRecipe.Rows.Count > last)
                {

                    send("EXE\tFIX_RECIPE\t" + modifyOldRecipeNumber
                        + "\t" + modifyRecipeNumber_TextBox.Text
                        + "\t" + modify_recipe_combobox.Text
                        + "\t" + dr[0]
                        + "\t" + dr[3] + "<N>");
                }
                else
                {

                    send("EXE\tFIX_RECIPE\t" + modifyOldRecipeNumber
                        + "\t" + modifyRecipeNumber_TextBox.Text
                        + "\t" + modify_recipe_combobox.Text
                        + "\t" + dr[0]
                        + "\t" + dr[3] + "<END>");

                }
                last += 1;

            }

            addModiy_Panel.Visible = false;
            modify_Recipe_Button.Visible = true;
            delete_Recipe_Button.Visible = true;
            modifyDataGridview.Visible = false;
            modify_recipe_combobox.SelectedValue = 0;
            modify_recipe_combobox.Text = "";
            modifyRecipeNumber_TextBox.Text = "";
            initialDateTable();


        }
        public void sendNewRecipe()
        {
            int last = 1;

            foreach (DataRow dr in dataTableRecipe.Rows)
            {
                if (dataTableRecipe.Rows.Count > last)
                {

                    send("EXE\tADD_RECIPE\t" + Recipe_NumerLabel.Text
                        + "\t" + Recipe_NameLabel.Text
                        + "\t" + dr[0]
                        + "\t" + dr[3] + "<N>");

                }
                else
                {
                    send("EXE\tADD_RECIPE\t" + Recipe_NumerLabel.Text
                        + "\t" + Recipe_NameLabel.Text
                        + "\t" + dr[0]
                        + "\t" + dr[3] + "<END>");

                }
                last += 1;

            }

            plus_Recipe_Panel.Visible = false;
            Add_RecipeButton.Visible = true;
            add_RecipeDatagridview.Visible = false;
            Recipe_NameLabel.Text = "";
            Recipe_NumerLabel.Text = "";
            initialDateTable();

        }

        

        public void noSort(DataGridView dataGridView)
        {
            for (int i = 0; i < dataGridView.Columns.Count; i++)
            {
                dataGridView.Columns[i].SortMode = DataGridViewColumnSortMode.NotSortable;
                dataGridView.Columns[i].ReadOnly = true;
            }

        }

        public void noOnline()
        {

            if (this.InvokeRequired)
            {
                deleshowDialog DO = new deleshowDialog(noOnline);
                this.Invoke(DO);
            }
            else
            {
                DialogResult mDialogResult = MessageBox.Show("看板未上線 請上線後再進行配料", "警告", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                if (mDialogResult == DialogResult.OK)
                    return;
            }
        }


        public void showHW(bool show, string[] content, int controlNumer)
        {
            DataRow rowReserve;
            DataRow rowLog;
            DataRow rowRecipe;
            if (this.InvokeRequired)
            {
                deleShowData DS = new deleShowData(showHW);
                this.Invoke(DS, new object[] { show, content, controlNumer });
            }
            else
            {

                //have <END>
                if (show)
                {
                    //not update
                    if (controlNumer == 0)
                    {
                        //control == 2 4 6 8 ....
                        if (control.showControl() % 2 == 0)
                        {
                            //query null
                            if (content[0] == "QUERY_NULL")
                            {
                                HandleString handleString = new HandleString();
                                // two is reserve
                                content = handleString.setQueryNull(2);

                            }
                            //add row on top
                            rowReserve = dataTableReserve.NewRow();
                            rowReserve.ItemArray = content;
                            dataTableReserve.Rows.InsertAt(rowReserve, 0);
                            //which datagridview store or sh
                            if (control.showControl() < 10)
                            {
                                //dataTable = showMessage
                                WH_REC_dataGridView.DataSource = dataTableReserve;
                                adjustDataGridView(WH_REC_dataGridView);
                                WH_REC_dataGridView.Show();
                            }
                            //SH reserve control = 10
                            else if (control.showControl() < 12)
                            {
                                showSHReserve(content);

                            }
                            // control = 12
                            else if (control.showControl() < 14)
                            {
                                AC_NAME(content, true);
                            }
                            // control = 14
                            else if (control.showControl() < 16)
                            {
                                stateShow(content);
                            }
                            // control = 16
                            else if (control.showControl() < 18)
                            {
                                modify_Recipe_Name(content, true);
                            }
                            // control = 18
                            else if (control.showControl() < 20)
                            {
                                //add row on top
                                rowRecipe = dataTableAddRecipe.NewRow();
                                rowRecipe.ItemArray = content;
                                dataTableAddRecipe.Rows.InsertAt(rowRecipe, 0);

                                //showMessage = dataTable
                                modify_allDataGridview.DataSource = dataTableAddRecipe;
                                noSort(modify_allDataGridview);
                                adjustDataGridView(modify_allDataGridview);
                                modify_allDataGridview.Show();
                            }
                            //control = 20
                            else if (control.showControl() < 22) 
                            {
                                showDirectRecipe(content);
                                                       
                            }

                        }



                           //control = 1 3 5 7...
                        else
                        {
                            //query null
                            if (content[0] == "QUERY_NULL")
                            {
                                HandleString handleString = new HandleString();
                                // one is log
                                content = handleString.setQueryNull(1);
                            }
                            //add row on top
                            rowLog = dataTableLog.NewRow();
                            rowLog.ItemArray = content;
                            dataTableLog.Rows.InsertAt(rowLog, 0);
                            //which datagridview store or sh
                            if (control.showControl() < 9)
                            {
                                //dataTable = showMessage
                                WH_REC_dataGridView.DataSource = dataTableLog;
                                adjustDataGridView(WH_REC_dataGridView);
                                WH_REC_dataGridView.Show();
                            }
                            //history control = 9
                            else if (control.showControl() < 11)
                            {
                                SH_REC_DataGridView.DataSource = dataTableLog;
                                adjustDataGridView(SH_REC_DataGridView);
                                SH_REC_DataGridView.Show();
                            }
                            //now log of  control = 11
                            else if (control.showControl() < 13)
                            {
                                SH_LOG_dataGridView.DataSource = dataTableLog; ;
                                adjustDataGridView(SH_LOG_dataGridView);
                                SH_LOG_dataGridView.Show();
                            }//control = 13
                            else if (control.showControl() < 15)
                            {
                                if (content[0] == "QUERY_NULL")
                                {
                                    HandleString handleString = new HandleString();
                                    // one is log
                                    content = handleString.setQueryNull(3);
                                }
                                //add row on top
                                rowRecipe = dataTableRecipe.NewRow();
                                rowRecipe.ItemArray = content;
                                dataTableRecipe.Rows.InsertAt(rowRecipe, 0);

                                //dataTable = showMessage
                                DB_Recipe_dataGridView.DataSource = dataTableRecipe;
                                adjustDataGridView(DB_Recipe_dataGridView);
                                DB_Recipe_dataGridView.Show();

                            }
                            // control = 15
                            else if (control.showControl() < 17)
                            {
                                if (content[0] == "QUERY_NULL")
                                {
                                    HandleString handleString = new HandleString();
                                    // one is log
                                    content = handleString.setQueryNull(2);
                                }
                                //add row on top
                                rowRecipe = dataTableAddRecipe.NewRow();
                                rowRecipe.ItemArray = content;
                                dataTableAddRecipe.Rows.InsertAt(rowRecipe, 0);

                                //dataTable = showMessage
                                chooseRecipeDateGridView.DataSource = dataTableAddRecipe;
                                noSort(chooseRecipeDateGridView);
                                adjustDataGridView(chooseRecipeDateGridView);
                                chooseRecipeDateGridView.Show();

                            }
                            //control = 17
                            else if (control.showControl() < 19)
                            {
                                if (content[0] == "QUERY_NULL")
                                {
                                    HandleString handleString = new HandleString();
                                    // one is log
                                    content = handleString.setQueryNull(3);
                                }
                                //add row on top
                                rowRecipe = dataTableRecipe.NewRow();
                                rowRecipe.ItemArray = content;
                                dataTableRecipe.Rows.InsertAt(rowRecipe, 0);

                                //dataTable = showMessage
                                modifyDataGridview.DataSource = dataTableRecipe;
                                adjustDataGridView(modifyDataGridview);
                                modifyDataGridview.Show();
                            }
                            //control = 19
                            else if (control.showControl() < 21)
                            {
                                //add row on top
                                rowRecipe = dataTableAddRecipe.NewRow();
                                rowRecipe.ItemArray = content;
                                dataTableAddRecipe.Rows.InsertAt(rowRecipe, 0);

                                //showMessage = dataTable
                                allSpiceDataGridView.DataSource = dataTableAddRecipe;
                                noSort(allSpiceDataGridView);
                                adjustDataGridView(allSpiceDataGridView);

                                adjustButton();
                                allSpiceDataGridView.Show();


                            }
                            //control = 21
                            else if (control.showControl() < 23) 
                            {
                                directRecipeName(content,true);
                            }


                        }
                    }
                    //update
                    else
                    {
                        //update same store
                        if (controlNumer == control.showControl())
                        {
                            
                            if (control.showControl() % 2 == 0)
                            {
                                //control = 10
                                if (control.showControl() == 10) 
                                {
                                    showSHReserve(content);                            
                                }
                                //control = 20
                                if (control.showControl() == 20)
                                {
                                    showDirectRecipe(content);
                                }
                                else
                                {
                                    //delete material number same with new update
                                    string query = "物料編號 = '" + content[0] + "'";
                                    DataRow[] dataRow = dataTableReserve.Select(query);
                                    // find the row  not find no remove
                                    if (dataRow.Length > 0)
                                    {
                                        dataTableReserve.Rows.Remove(dataRow[0]);
                                    }

                                    //add row on top
                                    rowReserve = dataTableReserve.NewRow();
                                    rowReserve.ItemArray = content;
                                    dataTableReserve.Rows.InsertAt(rowReserve, 0);




                                    //dataTable = showMessage
                                    WH_REC_dataGridView.DataSource = dataTableReserve;
                                    adjustDataGridView(WH_REC_dataGridView);
                                    WH_REC_dataGridView.Show();
                                }
                            }
                            //reserve
                            else
                            {
                                //add top
                                rowLog = dataTableLog.NewRow();
                                rowLog.ItemArray = content;
                                dataTableLog.Rows.InsertAt(rowLog, 0);


                                WH_REC_dataGridView.DataSource = dataTableLog;
                                adjustDataGridView(WH_REC_dataGridView);
                                WH_REC_dataGridView.Show();


                            }
                        }


                    }

                }
                //don't have <END>
                else
                {
                    //control = 2 4 6 8 ....
                    if (control.showControl() % 2 == 0)
                    {
                        //sh reserve
                        if (control.showControl() == 10)
                        {
                            showSHReserve(content);
                        }
                        // ac_name list control == 12
                        else if (control.showControl() == 12)
                        {
                            AC_NAME(content);
                        }
                        else if (control.showControl() == 14)
                        {
                            stateShow(content);

                        }
                        else if (control.showControl() == 16)
                        {
                            modify_Recipe_Name(content);
                        }
                        else if (control.showControl() == 18)
                        {
                            //add row on top
                            rowRecipe = dataTableAddRecipe.NewRow();
                            rowRecipe.ItemArray = content;
                            dataTableAddRecipe.Rows.InsertAt(rowRecipe, 0);


                        }
                        else if (control.showControl() == 20) 
                        {
                            showDirectRecipe(content);
                        
                        }
                        //control = 2 4 6 8
                        else//
                        {
                            rowReserve = dataTableReserve.NewRow();
                            rowReserve.ItemArray = content;
                            dataTableReserve.Rows.InsertAt(rowReserve, 0);
                        }

                    }

                    //control = 1 3 5 7...
                    else
                    {
                        if (control.showControl() < 13)
                        {
                            //dataTableLog.Rows.Add(content);
                            //add row top
                            rowLog = dataTableLog.NewRow();
                            rowLog.ItemArray = content;
                            dataTableLog.Rows.InsertAt(rowLog, 0);
                        }
                        // control == 13
                        else if (control.showControl() < 15)
                        {
                            //add row top
                            rowRecipe = dataTableRecipe.NewRow();
                            rowRecipe.ItemArray = content;
                            dataTableRecipe.Rows.InsertAt(rowRecipe, 0);
                        }
                        //control == 15
                        else if (control.showControl() < 17)
                        {
                            rowRecipe = dataTableAddRecipe.NewRow();
                            rowRecipe.ItemArray = content;
                            dataTableAddRecipe.Rows.InsertAt(rowRecipe, 0);

                        }
                        // control = 17
                        else if (control.showControl() < 19)
                        {
                            rowRecipe = dataTableRecipe.NewRow();
                            rowRecipe.ItemArray = content;
                            dataTableRecipe.Rows.InsertAt(rowRecipe, 0);

                        }
                        //control = 19
                        else if (control.showControl() < 21)
                        {
                            //add row on top
                            rowRecipe = dataTableAddRecipe.NewRow();
                            rowRecipe.ItemArray = content;
                            dataTableAddRecipe.Rows.InsertAt(rowRecipe, 0);

                        }
                        //control = 21;
                        else if (control.showControl() < 23)
                        {
                            directRecipeName(content);
                        }


                    }

                }
            }

        }

        public void comfirmAddRecipe(string name, string number)
        {
            plus_Recipe_Panel.Visible = true;
            Recipe_NameLabel.Text = name;
            Recipe_NumerLabel.Text = number;
            Add_RecipeButton.Visible = false;
        }

        public void stateShow(string[] state)
        {

            if (this.InvokeRequired)
            {
                deleStateShow DS = new deleStateShow(stateShow);
                this.Invoke(DS, new object[] { state });

                
            }
            else
            {
                //show state
                for (int i = 0; i < state.Length; i = i + 2)
                {

                    bool on = false;
                    if (int.Parse(state[i + 1]) == 1)
                    {
                        on = true;
                    }
                    stateON(state[i], on);


                }
            }
        }





        //print connect status 
        public void printStatus(string status)
        {
            if (this.InvokeRequired)
            {
                deleStatus ds = new deleStatus(printStatus);
                this.Invoke(ds, new object[] { status });
            }
            else
            {
                this.clientState.ForeColor = System.Drawing.Color.Red;
                this.clientState.Text = status;
            }

        }
        public void hidePanel()
        {

            WH_REC_Panel.Visible = false;
            SH_Amount_Panel.Visible = false;
            SH_REC_LOG_Panel.Visible = false;
            WH_REC_History_Panel.Visible = false;
            SH_REC_Panel.Visible = false;
            statePanel.Visible = false;
            DB_Recipe_Panel.Visible = false;
            Add_Recipe_Panel.Visible = false;
            plus_Recipe_Panel.Visible = false;
            Modify_Recipe_Panel.Visible = false;
            spicePanel.Visible = false;
            assignRecipePanel.Visible = false;
            chooseDirectRecpiePanel.Visible = false;

        }
        //設定初值
        public void formInitial()
        {

            control = new Control();
            currentTime = System.DateTime.Now;
            //宣告dataTable
            dataTableLog = new DataTable();
            dataTableReserve = new DataTable();
            dataTableRecipe = new DataTable();
            dataTableAddRecipe = new DataTable();

            HandleDataTable handleDataTable = new HandleDataTable();
            //initial dataTable
            dataTableLog = handleDataTable.handleDataLog(dataTableLog);
            dataTableReserve = handleDataTable.handleDataReserve(dataTableReserve);
            dataTableRecipe = handleDataTable.handleRecipe(dataTableRecipe);
            dataTableAddRecipe = handleDataTable.handleAddRecipe(dataTableAddRecipe);
            //combobox name list
            ACName = new OrderedDictionary();


        }
        // send message use thread
        public void send(string mes)
        {
            Thread clientThread = new Thread(new ParameterizedThreadStart(client.run));
            clientThread.Start(mes);
        }

        public void sendHW(int hwNum, int type, int year, int month, int day)
        {

            switch (hwNum)
            {

                case 1:
                    if (type == 4)
                    {
                        send("QUERY\tWH_HISTORY\t3\t" + year + "\t" + month + "\t" + day + "<END>");
                    }
                    else if (type == 5)
                    {  
                        send("QUERY\tWH_NOW\t3\t" + year + "\t" + month + "\t" + day + "<END>");
                    }
                    break;
                case 2:
                    if (type == 4)
                    {
                        send("QUERY\tWH_HISTORY\t5\t" + year + "\t" + month + "\t" + day + "<END>");
                    }
                    else if (type == 5)
                    {
                        send("QUERY\tWH_NOW\t5\t" + year + "\t" + month + "\t" + day + "<END>");
                    }
                    break;
                case 3:
                    if (type == 4)
                    {
                        send("QUERY\tWH_HISTORY\t6\t" + year + "\t" + month + "\t" + day + "<END>");
                    }
                    else if (type == 5)
                    {
                        send("QUERY WH_NOW\t6\t" + year + "\t" + month + "\t" + day + "<END>");
                    }
                    break;


            }

        }
        private void adjustDataGridView(DataGridView mDataGridView, Int32 maxLine = 25)
        {
            int height = 0; int width = 0; int count = 0;

            // Adjust Row
            foreach (DataGridViewRow row in mDataGridView.Rows)
            {
                height += row.Height;
                count++;
                if (count == maxLine)
                {
                    width = 16;
                    break;
                }
            }
            height += mDataGridView.ColumnHeadersHeight;
            
            // Adjust Col
            foreach (DataGridViewColumn col in mDataGridView.Columns)
            {
                width += col.Width;
            }
            width += mDataGridView.RowHeadersWidth;
            mDataGridView.Size = new Size(width + 3, height + 3);
            
        }
        public void initialDateTable()
        {
            dataTableLog.Rows.Clear();
            dataTableReserve.Rows.Clear();
            dataTableRecipe.Rows.Clear();
            dataTableAddRecipe.Rows.Clear();
            ACName.Clear();

        }

        public void AC_NAME(string[] str, bool end = false)
        {
            ACName.Insert(0, str[0], str[1]);

            //plus last one
            if (end)
            {
                DB_Recipe_comboBox.DataSource = new BindingSource(ACName, null);
                DB_Recipe_comboBox.DisplayMember = "value";
                DB_Recipe_comboBox.ValueMember = "key";
                DB_Recipe_comboBox.SelectedValue = 0;
            }

        }
        
        public void modify_Recipe_Name(string[] str, bool end = false)
        {
            ACName.Insert(0, str[0], str[1]);

            //plus last one
            if (end)
            {
                modify_recipe_combobox.DataSource = new BindingSource(ACName, null);
                modify_recipe_combobox.DisplayMember = "value";
                modify_recipe_combobox.ValueMember = "key";
                modify_recipe_combobox.SelectedValue = 0;
            }

        }

        public void initialPanelItem()
        {
            Add_RecipeButton.Visible = true;
            //修改配方
            modify_Recipe_Button.Visible = true;
            Recipe_NameLabel.Text = "";
            Recipe_NumerLabel.Text = "";
            DB_Recipe_Serial_label.Text = "";
            modifyRecipeNumber_TextBox.Text = "";


        }



        public bool stateON(string device, bool on)
        {

            Color change = Color.Red;
            if (on)
            {
                change = Color.Lime;
            }


            switch (device)
            {

                case "物料倉庫辦公室": WH_OFFICE_onlineLight.BackColor = change; return true;
                case "3 號倉庫電腦": WH_3_PC_onlineLight.BackColor = change; return true;
                case "5 號倉庫電腦": WH_5_PC_onlineLight.BackColor = change; return true;
                case "6 號倉庫電腦": WH_6_PC_onlineLight.BackColor = change; return true;
                case "3 號倉庫PDA": WH_3_PDA_onlineLight.BackColor = change; return true;
                case "5 號倉庫PDA": WH_5_PDA_onlineLight.BackColor = change; return true;
                case "6 號倉庫PDA": WH_6_PDA_onlineLight.BackColor = change; return true;
                case "線邊倉PDA 1": SH_1_onlineLight.BackColor = change; return true;
                case "線邊倉PDA 2": SH_2_onlineLight.BackColor = change; return true;
                case "線邊倉PDA 3": SH_3_onlineLight.BackColor = change; return true;
                case "料位看板 1": SHM_1_onlineLight.BackColor = change; return true;
                case "料位看板 2": SHM_2_onlineLight.BackColor = change; return true;
                case "料位看板 3": SHM_3_onlineLight.BackColor = change; return true;
                case "料位看板 4": SHM_4_onlineLight.BackColor = change; return true;
                case "料位看板 5": SHM_5_onlineLight.BackColor = change; return true;
                case "料位看板 6": SHM_6_onlineLight.BackColor = change; return true;
                case "料位看板 7": SHM_7_onlineLight.BackColor = change; return true;
                case "料位看板 8": SHM_8_onlineLight.BackColor = change; return true;
                case "料位看板 9": SHM_9_onlineLight.BackColor = change; return true;
                case "料位看板 10": SHM_10_onlineLight.BackColor = change; return true;
                case "料位看板 11": SHM_11_onlineLight.BackColor = change; return true;
                case "料位看板 12": SHM_12_onlineLight.BackColor = change; return true;
                case "料位看板 13": SHM_13_onlineLight.BackColor = change; return true;
                case "料位看板 14": SHM_14_onlineLight.BackColor = change; return true;
                case "料位看板 15": SHM_15_onlineLight.BackColor = change; return true;
                case "料位看板 16": SHM_16_onlineLight.BackColor = change; return true;
                case "料位看板 17": SHM_17_onlineLight.BackColor = change; return true;
                case "料位看板 18": SHM_18_onlineLight.BackColor = change; return true;
                case "料位看板 19": SHM_19_onlineLight.BackColor = change; return true;
                case "料位看板 20": SHM_20_onlineLight.BackColor = change; return true;
                case "料位看板 21": SHM_21_onlineLight.BackColor = change; return true;
                case "料位看板 22": SHM_22_onlineLight.BackColor = change; return true;
                case "料位看板 23": SHM_23_onlineLight.BackColor = change; return true;
                case "料位看板 24": SHM_24_onlineLight.BackColor = change; return true;
                case "料位看板 25": SHM_25_onlineLight.BackColor = change; return true;
                case "料位看板 26": SHM_26_onlineLight.BackColor = change; return true;
                case "料位看板 27": SHM_27_onlineLight.BackColor = change; return true;
                case "料位看板 28": SHM_28_onlineLight.BackColor = change; return true;
                case "料位看板 29": SHM_29_onlineLight.BackColor = change; return true;
                case "料位看板 30": SHM_30_onlineLight.BackColor = change; return true;
                case "料位看板 31": SHM_31_onlineLight.BackColor = change; return true;
                case "料位看板 32": SHM_32_onlineLight.BackColor = change; return true;
                case "料位看板 33": SHM_33_onlineLight.BackColor = change; return true;
                case "料位看板 34": SHM_34_onlineLight.BackColor = change; return true;
                case "料位看板 35": SHM_35_onlineLight.BackColor = change; return true;
                case "料位看板 36": SHM_36_onlineLight.BackColor = change; return true;
                case "料位看板 37": SHM_37_onlineLight.BackColor = change; return true;
                case "料位看板 38": SHM_38_onlineLight.BackColor = change; return true;
                case "料位看板 39": SHM_39_onlineLight.BackColor = change; return true;
                case "料位看板 40": SHM_40_onlineLight.BackColor = change; return true;
                case "料位看板 41": SHM_41_onlineLight.BackColor = change; return true;
                case "料位看板 42": SHM_42_onlineLight.BackColor = change; return true;
                case "料位看板 43": SHM_43_onlineLight.BackColor = change; return true;
                case "料位看板 44": SHM_44_onlineLight.BackColor = change; return true;
                case "料位看板 45": SHM_45_onlineLight.BackColor = change; return true;
                case "捲菸機看板 1": CM_1_M_onlineLight.BackColor = change; return true;
                case "捲菸機看板 2": CM_2_M_onlineLight.BackColor = change; return true;
                case "捲菸機看板 3": CM_3_M_onlineLight.BackColor = change; return true;
                case "捲菸機看板 4": CM_4_M_onlineLight.BackColor = change; return true;
                case "捲菸機看板 5": CM_5_M_onlineLight.BackColor = change; return true;
                case "捲菸機看板 6": CM_6_M_onlineLight.BackColor = change; return true;
                case "捲菸機看板 7": CM_7_M_onlineLight.BackColor = change; return true;
                case "捲菸機看板 8": CM_8_M_onlineLight.BackColor = change; return true;
                case "包裝機看板 1": PM_1_M_onlineLight.BackColor = change; return true;
                case "包裝機看板 2": PM_2_M_onlineLight.BackColor = change; return true;
                case "包裝機看板 3": PM_3_M_onlineLight.BackColor = change; return true;
                case "包裝機看板 4": PM_4_M_onlineLight.BackColor = change; return true;
                case "包裝機看板 5": PM_5_M_onlineLight.BackColor = change; return true;
                case "包裝機看板 6": PM_6_M_onlineLight.BackColor = change; return true;
                case "包裝機看板 7": PM_7_M_onlineLight.BackColor = change; return true;
                case "包裝機看板 8": PM_8_M_onlineLight.BackColor = change; return true;
                case "捲菸機PDA 1": CM_1_PDA_onlineLight.BackColor = change; return true;
                case "捲菸機PDA 2": CM_2_PDA_onlineLight.BackColor = change; return true;
                case "捲菸機PDA 3": CM_3_PDA_onlineLight.BackColor = change; return true;
                case "捲菸機PDA 4": CM_4_PDA_onlineLight.BackColor = change; return true;
                case "捲菸機PDA 5": CM_5_PDA_onlineLight.BackColor = change; return true;
                case "捲菸機PDA 6": CM_6_PDA_onlineLight.BackColor = change; return true;
                case "捲菸機PDA 7": CM_7_PDA_onlineLight.BackColor = change; return true;
                case "捲菸機PDA 8": CM_8_PDA_onlineLight.BackColor = change; return true;
                case "包裝機PDA 1": PM_1_PDA_onlineLight.BackColor = change; return true;
                case "包裝機PDA 2": PM_2_PDA_onlineLight.BackColor = change; return true;
                case "包裝機PDA 3": PM_3_PDA_onlineLight.BackColor = change; return true;
                case "包裝機PDA 4": PM_4_PDA_onlineLight.BackColor = change; return true;
                case "包裝機PDA 5": PM_5_PDA_onlineLight.BackColor = change; return true;
                case "包裝機PDA 6": PM_6_PDA_onlineLight.BackColor = change; return true;
                case "包裝機PDA 7": PM_7_PDA_onlineLight.BackColor = change; return true;
                case "包裝機PDA 8": PM_8_PDA_onlineLight.BackColor = change; return true;
                case "濾嘴工場看板": FF_onlineLight.BackColor = change; return true;
                case "9 號機看板": N9_M_onlineLight.BackColor = change; return true;
                case "9 號機PDA": N9_PDA_onlineLight.BackColor = change; return true;
                case "配料看板": MS_M_onlineLight.BackColor = change; return true;
                case "配料PDA": MS_PDA_onlineLight.BackColor = change; return true;
                case "加香看板": AS_M_onlineLight.BackColor = change; return true;
                case "加香PDA": AS_PDA_onlineLight.BackColor = change; return true;
                case "菸絲看板": CT_onlineLight.BackColor = change; return true;
                case "理切辦公室": AC_onlineLight.BackColor = change; return true;
                case "主管查詢 1": MI_1_onlineLight.BackColor = change; return true;
                case "主管查詢 2": MI_2_onlineLight.BackColor = change; return true;
                case "主管查詢 3": MI_3_onlineLight.BackColor = change; return true;
                case "主管查詢 4": MI_4_onlineLight.BackColor = change; return true;
                case "主管查詢 5": MI_5_onlineLight.BackColor = change; return true;
                case "主管查詢 6": MI_6_onlineLight.BackColor = change; return true;
                default: return false;

            }
        }
        public void showSHReserve(string[] str)
        {
            switch (str[0])
            {
                case "料位看板 1":
                    SH_Amount_Name_1_Label.Text = str[1];
                    SH_Amount_1_Label.Text = str[2];
                    break;
                case "料位看板 2":
                    SH_Amount_Name_2_Label.Text = str[1];
                    SH_Amount_2_Label.Text = str[2];
                    break;
                case "料位看板 3":
                    SH_Amount_Name_3_Label.Text = str[1];
                    SH_Amount_3_Label.Text = str[2];
                    break;
                case "料位看板 4":
                    SH_Amount_Name_4_Label.Text = str[1];
                    SH_Amount_4_Label.Text = str[2];
                    break;
                case "料位看板 5":
                    SH_Amount_Name_5_Label.Text = str[1];
                    SH_Amount_5_Label.Text = str[2];
                    break;
                case "料位看板 6":
                    SH_Amount_Name_6_Label.Text = str[1];
                    SH_Amount_6_Label.Text = str[2];
                    break;
                case "料位看板 7":
                    SH_Amount_Name_7_Label.Text = str[1];
                    SH_Amount_7_Label.Text = str[2];
                    break;
                case "料位看板 8":
                    SH_Amount_Name_8_Label.Text = str[1];
                    SH_Amount_8_Label.Text = str[2];
                    break;
                case "料位看板 9":
                    SH_Amount_Name_9_Label.Text = str[1];
                    SH_Amount_9_Label.Text = str[2];
                    break;
                case "料位看板 10":
                    SH_Amount_Name_10_Label.Text = str[1];
                    SH_Amount_10_Label.Text = str[2];
                    break;
                case "料位看板 11":
                    SH_Amount_Name_11_Label.Text = str[1];
                    SH_Amount_11_Label.Text = str[2];
                    break;
                case "料位看板 12":
                    SH_Amount_Name_12_Label.Text = str[1];
                    SH_Amount_12_Label.Text = str[2];
                    break;
                case "料位看板 13":
                    SH_Amount_Name_13_Label.Text = str[1];
                    SH_Amount_13_Label.Text = str[2];
                    break;
                case "料位看板 14":
                    SH_Amount_Name_14_Label.Text = str[1];
                    SH_Amount_14_Label.Text = str[2];
                    break;
                case "料位看板 15":
                    SH_Amount_Name_15_Label.Text = str[1];
                    SH_Amount_15_Label.Text = str[2];
                    break;
                case "料位看板 16":
                    SH_Amount_Name_16_Label.Text = str[1];
                    SH_Amount_16_Label.Text = str[2];
                    break;
                case "料位看板 17":
                    SH_Amount_Name_17_Label.Text = str[1];
                    SH_Amount_17_Label.Text = str[2];
                    break;
                case "料位看板 18":
                    SH_Amount_Name_18_Label.Text = str[1];
                    SH_Amount_18_Label.Text = str[2];
                    break;
                case "料位看板 19":
                    SH_Amount_Name_19_Label.Text = str[1];
                    SH_Amount_19_Label.Text = str[2];
                    break;
                case "料位看板 20":
                    SH_Amount_Name_20_Label.Text = str[1];
                    SH_Amount_20_Label.Text = str[2];
                    break;
                case "料位看板 21":
                    SH_Amount_Name_21_Label.Text = str[1];
                    SH_Amount_21_Label.Text = str[2];
                    break;
                case "料位看板 22":
                    SH_Amount_Name_22_Label.Text = str[1];
                    SH_Amount_22_Label.Text = str[2];
                    break;
                case "料位看板 23":
                    SH_Amount_Name_23_Label.Text = str[1];
                    SH_Amount_23_Label.Text = str[2];
                    break;
                case "料位看板 24":
                    SH_Amount_Name_24_Label.Text = str[1];
                    SH_Amount_24_Label.Text = str[2];
                    break;
                case "料位看板 25":
                    SH_Amount_Name_25_Label.Text = str[1];
                    SH_Amount_25_Label.Text = str[2];
                    break;
                case "料位看板 26":
                    SH_Amount_Name_26_Label.Text = str[1];
                    SH_Amount_26_Label.Text = str[2];
                    break;
                case "料位看板 27":
                    SH_Amount_Name_27_Label.Text = str[1];
                    SH_Amount_27_Label.Text = str[2];
                    break;
                case "料位看板 28":
                    SH_Amount_Name_28_Label.Text = str[1];
                    SH_Amount_28_Label.Text = str[2];
                    break;
                case "料位看板 29":
                    SH_Amount_Name_29_Label.Text = str[1];
                    SH_Amount_29_Label.Text = str[2];
                    break;
                case "料位看板 30":
                    SH_Amount_Name_30_Label.Text = str[1];
                    SH_Amount_30_Label.Text = str[2];
                    break;
                case "料位看板 31":
                    SH_Amount_Name_31_Label.Text = str[1];
                    SH_Amount_31_Label.Text = str[2];
                    break;
                case "料位看板 32":
                    SH_Amount_Name_32_Label.Text = str[1];
                    SH_Amount_32_Label.Text = str[2];
                    break;
                case "料位看板 33":
                    SH_Amount_Name_33_Label.Text = str[1];
                    SH_Amount_33_Label.Text = str[2];
                    break;
                case "料位看板 34":
                    SH_Amount_Name_34_Label.Text = str[1];
                    SH_Amount_34_Label.Text = str[2];
                    break;
                case "料位看板 35":
                    SH_Amount_Name_35_Label.Text = str[1];
                    SH_Amount_35_Label.Text = str[2];
                    break;
                case "料位看板 36":
                    SH_Amount_Name_36_Label.Text = str[1];
                    SH_Amount_36_Label.Text = str[2];
                    break;
                case "料位看板 37":
                    SH_Amount_Name_37_Label.Text = str[1];
                    SH_Amount_37_Label.Text = str[2];
                    break;
                case "料位看板 38":
                    SH_Amount_Name_38_Label.Text = str[1];
                    SH_Amount_38_Label.Text = str[2];
                    break;
                case "料位看板 39":
                    SH_Amount_Name_39_Label.Text = str[1];
                    SH_Amount_39_Label.Text = str[2];
                    break;
                case "料位看板 40":
                    SH_Amount_Name_40_Label.Text = str[1];
                    SH_Amount_40_Label.Text = str[2];
                    break;
                case "料位看板 41":
                    SH_Amount_Name_41_Label.Text = str[1];
                    SH_Amount_41_Label.Text = str[2];
                    break;
                case "料位看板 42":
                    SH_Amount_Name_42_Label.Text = str[1];
                    SH_Amount_42_Label.Text = str[2];
                    break;
                case "料位看板 43":
                    SH_Amount_Name_43_Label.Text = str[1];
                    SH_Amount_43_Label.Text = str[2];
                    break;
                case "料位看板 44":
                    SH_Amount_Name_44_Label.Text = str[1];
                    SH_Amount_44_Label.Text = str[2];
                    break;
                case "料位看板 45":
                    SH_Amount_Name_45_Label.Text = str[1];
                    SH_Amount_45_Label.Text = str[2];
                    break;

            }

        }

        public void showDirectRecipe(string[] showStr) 
        {
            switch (showStr[0]) 
            {
                case "1":
                    addRecipeNumber_1.Text = "配方編號：" + showStr[1];
                    addRecipeName_1.Text = "配方名稱：" + showStr[2];
                    break;
                case "2":
                    addRecipeNumber_2.Text = "配方編號：" + showStr[1];
                    addRecipeName_2.Text = "配方名稱：" + showStr[2];
                    break;
                case "3":
                    addRecipeNumber_3.Text = "配方編號：" + showStr[1];
                    addRecipeName_3.Text = "配方名稱：" + showStr[2];
                    break;
                case "4":
                    addRecipeNumber_4.Text = "配方編號：" + showStr[1];
                    addRecipeName_4.Text = "配方名稱：" + showStr[2];
                    break;
                case "5":
                    addRecipeNumber_5.Text = "配方編號：" + showStr[1];
                    addRecipeName_5.Text = "配方名稱：" + showStr[2];
                    break;
                case "6":
                    addRecipeNumber_6.Text = "配方編號：" + showStr[1];
                    addRecipeName_6.Text = "配方名稱：" + showStr[2];
                    break;
                case "7":
                    addRecipeNumber_7.Text = "配方編號：" + showStr[1];
                    addRecipeName_7.Text = "配方名稱：" + showStr[2];
                    break;
                case "8":
                    addRecipeNumber_8.Text = "配方編號：" + showStr[1];
                    addRecipeName_8.Text = "配方名稱：" + showStr[2];
                    break;
            
            }
        
        }

        public void directRecipeName(string[] str, bool end = false)
        {

            
            ACName.Insert(0, str[0], str[1]);
            
            //plus last one
            if (end)
            {
                ACName.Insert(ACName.Count, "0", "未指定");
                BucketComBoBox_1.DataSource = new BindingSource(ACName, null);
                BucketComBoBox_1.DisplayMember = "value";
                BucketComBoBox_1.ValueMember = "key";
                BucketComBoBox_1.SelectedValue = 0;

                BucketComBoBox_2.DataSource = new BindingSource(ACName, null);
                BucketComBoBox_2.DisplayMember = "value";
                BucketComBoBox_2.ValueMember = "key";
                BucketComBoBox_2.SelectedValue = 0;

                BucketComBoBox_3.DataSource = new BindingSource(ACName, null);
                BucketComBoBox_3.DisplayMember = "value";
                BucketComBoBox_3.ValueMember = "key";
                BucketComBoBox_3.SelectedValue = 0;

                BucketComBoBox_4.DataSource = new BindingSource(ACName, null);
                BucketComBoBox_4.DisplayMember = "value";
                BucketComBoBox_4.ValueMember = "key";
                BucketComBoBox_4.SelectedValue = 0;

                BucketComBoBox_5.DataSource = new BindingSource(ACName, null);
                BucketComBoBox_5.DisplayMember = "value";
                BucketComBoBox_5.ValueMember = "key";
                BucketComBoBox_5.SelectedValue = 0;

                BucketComBoBox_6.DataSource = new BindingSource(ACName, null);
                BucketComBoBox_6.DisplayMember = "value";
                BucketComBoBox_6.ValueMember = "key";
                BucketComBoBox_6.SelectedValue = 0;

                BucketComBoBox_7.DataSource = new BindingSource(ACName, null);
                BucketComBoBox_7.DisplayMember = "value";
                BucketComBoBox_7.ValueMember = "key";
                BucketComBoBox_7.SelectedValue = 0;

                BucketComBoBox_8.DataSource = new BindingSource(ACName, null);
                BucketComBoBox_8.DisplayMember = "value";
                BucketComBoBox_8.ValueMember = "key";
                BucketComBoBox_8.SelectedValue = 0;


            }

        }
       

        


        

       

      

    }
}
