using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Collections;
using System.Net.Sockets;
using System.Threading;
using System.Windows.Forms;

namespace ArrangeOffice
{
    class TCPListener
    {

        private Socket socket;
        //  private Label label;
        private ArrangeOffice form;
        private HandleString handleString;

        public TCPListener()
        {
            handleString = new HandleString();

        }
        public void run()
        {
            //num byte to connect
            int startIndex = 0;
            byte[] completeBuf = new byte[30000];
            List<string> removeNString;
            while (true)
            {
                int bytes;
                int end = -1;
                byte[] buf = new byte[2048];

                try
                {
                    //receive from server
                    bytes = socket.Receive(buf);
                    
                }
                catch (SocketException exception)
                {
                    for (int i = 0; i < 6; i++)
                    {
                        int a = 5 - i;
                        Thread.Sleep(1200);
                        form.printStatus("目前狀態: 斷線 " + a + "秒後重新連接");
                    }
                    Console.WriteLine("read error" + exception);
                    break;
                }
                if (bytes == 0)
                {
                    Console.WriteLine("Server disconnect");
                    for (int i = 0; i < 6; i++)
                    {
                        int a = 5 - i;
                        Thread.Sleep(1200);
                        form.printStatus("目前狀態: 斷線 " + a + "秒後重新連接");

                    }
                    break;
                }
                else if (bytes < 0)
                {
                    Console.WriteLine("detect problem bytes < 0");
                }
                //bytes > 0
                else
                {
                    string recStr = Encoding.UTF8.GetString(buf, 0, bytes);


                    if (bytes >= 5)
                    {
                        end = recStr.IndexOf("<END>", recStr.Length - 5, 5);
                    }
                    
                    //connect all byte[] before<END>
                    //not <END> add byte

                    if (end == -1)
                    {
                       
                        buf.CopyTo(completeBuf, startIndex);
                        startIndex = startIndex + bytes;
                        //test
                        string testStr = Encoding.UTF8.GetString(completeBuf, 0, startIndex);
                   
                   //    Thread.Sleep(1);
                        
                    }
                    // <END>
                    else
                    {
                        buf.CopyTo(completeBuf, startIndex);
                        //plus completeBuf
                        startIndex = startIndex + bytes;
                        //test
                        string completeStr = Encoding.UTF8.GetString(completeBuf, 0, startIndex);
                        //remove <N>
                        Console.WriteLine(completeStr);

                        //split with <N>
                        removeNString = handleString.stringToArrayList(completeStr);
                        
                        //remove <end> of the last one
                        removeNString[removeNString.Count - 1] = handleString.removeEnd(removeNString[removeNString.Count - 1]);

                        for (int i = 0; i < removeNString.Count; i++)
                        {

                            //中間夾有  update  .... <END>
                            if (removeNString[i].IndexOf("<END>") > 0)
                            {

                                string[] hasEndString = removeNString[i].Split(new string[] { "<END>" }, StringSplitOptions.None);
                                for (int j = 0; j < hasEndString.Length; j++)
                                {
                                    //end
                                    if (j < hasEndString.Length-1)
                                    {
                                        decideCommond(hasEndString[j], 1);
                                    }
                                    //not end string
                                    else
                                    {
                                        decideCommond(hasEndString[j], 0);
                                    }
                                }
                                continue;

                            }

                            //end string
                            if (i == removeNString.Count - 1)
                            {
                                decideCommond(removeNString[i], 1);
                            }
                            //not end string
                            else
                            {
                                decideCommond(removeNString[i], 0);
                            }
                        }

                        //clear byte[]
                        Array.Clear(completeBuf, 0, startIndex);
                        startIndex = 0;
                    }


                }

            }

        }

        public void decideCommond(string recStr, int end)
        {


            string[] decStr = recStr.Split('\t');

            int controlNumber = 0;

            switch (decStr[0])
            {

                case "CONNECT":

                    if (end == 0)
                    {


                    }
                    else
                    {
                        // allStr = allStr + recStr;                        
                        // allStr = null;  

                    }
                    break;
                case "QUERY_REPLY":
                    if (end == 0)
                    {
                        //delete first argument "QUERY_REPLY"
                        decStr = handleString.handlestring(decStr);

                        //add data on datagridview
                        form.showHW(false, decStr, 0);

                    }
                    else
                    {

                        //delete first argument "QUERY_REPLY"
                        decStr = handleString.handlestring(decStr);

                        //add data on datagridview
                        form.showHW(true, decStr, 0);
                    }
                    break;
                case "QUERY_NULL":

                  ///  form.dataNo();
                    form.showHW(true, decStr, 0);
                      
                    break;
                case "LOGIN_NULL":
                    Thread.Sleep(100);
                    form.loginError();
                    break;
                case "LOGIN_REPLY":
                    Thread.Sleep(100);
                    form.loginTime();
                    break;
                case "UPDATE_WH_HISTORY":
                    //Set controlNumber
                    controlNumber = handleString.setControlNumber(decStr[0], decStr[1]);
                    //delete first second argument "UPDATE_WH_HISTORY" , storeNumber
                    decStr = handleString.handlestring(decStr);
                    decStr = handleString.handlestring(decStr);

                    //add data on datagridview
                    form.showHW(true, decStr, controlNumber);
                    break;
                case "UPDATE_WH_NOW":
                    //Set controlNumber
                    controlNumber = handleString.setControlNumber(decStr[0], decStr[1]);

                    //delete first second argument "UPDATE_WH_HISTORY" , storeNumber
                    decStr = handleString.handlestring(decStr);
                    decStr = handleString.handlestring(decStr);

                    //add data on datagridview
                     form.showHW(true, decStr, controlNumber);
                    break;
                case "UPDATE_SH_HISTORY":

                    decStr = handleString.handlestring(decStr);
                    form.showHW(true, decStr, 11);

                    break;

                case "UPDATE_SH_NOW":

                    decStr = handleString.handlestring(decStr);
                    form.showHW(true, decStr, 10);

                    break;
                case "UPDATE_ONLINE":

                    decStr = handleString.handlestring(decStr);
                    form.showHW(true, decStr, 14);
                    break;
                case "UPDATE_RECIPE_NOW":
                    decStr = handleString.handlestring(decStr);
                    form.showHW(true, decStr, 20);
                    break;
                case "EXE_NOT_ONLINE":

                    form.noOnline();
                    break;
                case "EXE_DUPLICATE":
                    form.duplicateRecipe();
                    break;
                case "BROADCAST":

                    if (end == 0)
                    {

                        // broadStr = broadStr + recStr;

                    }
                    else
                    {
                        //  broadStr = broadStr + recStr;
                        //  Console.WriteLine(broadStr);
                        //  broadStr = null;

                    }

                    break;
                
            }

        }


        public void push(Socket server)
        {
            this.socket = server;
        }
        public void pushForm(ArrangeOffice form)
        {
            this.form = form;

        }

    }

}
