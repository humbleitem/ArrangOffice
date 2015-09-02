using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Collections;

namespace ArrangeOffice
{
    class HandleString
    {


        public string[] handlestring(string[] str)
        {

            string[] newStr = new string[str.Length - 1];
            for (int i = 0; i < str.Length - 1; i++)
            {
                newStr[i] = str[i + 1];
            }

            return newStr;

        }
        
        public int test(string[] str)
        {
            return str.Length;
        }
        public string removeEnd(string str)
        {

            int start = str.IndexOf("<END>");

            if (start == -1)
            {
                //刪除<N>
                str = str.Remove(str.Length - 3);
            }
            else
            {
                //刪除<END>
                str = str.Remove(str.Length - 5);
            }
            //   Console.WriteLine(str);
            return str;

        }
        public string[] removeBaseLine(string[] str)
        {

            int start = str[0].IndexOf('_');
            str[0] = str[0].Remove(start, 1);
            str[0] = str[0].Insert(start, " ");

            return str;

        }
        public List<string> stringToArrayList(string str)
        {

            List<string> arrayList = str.Split(new string[] { "<N>" }, StringSplitOptions.None).ToList();

            return arrayList;

        }
        public int setControlNumber(string action, string store)
        {
            int controlNumber = 0;

            switch (action)
            {

                case "UPDATE_WH_HISTORY":
                    switch (store)
                    {

                        case "3":
                            controlNumber = 1;
                            break;
                        case "5":
                            controlNumber = 3;
                            break;
                        case "6":
                            controlNumber = 5;
                            break;

                    }
                    break;
                case "UPDATE_WH_NOW":
                    switch (store)
                    {
                        case "3":
                            controlNumber = 2;
                            break;
                        case "5":
                            controlNumber = 4;
                            break;
                        case "6":
                            controlNumber = 6;
                            break;
                    }
                    break;

            }

            return controlNumber;

        }
        public string[] setQueryNull(int action){

            if (action == 1)
            {

                string[] log = { "查無資料", "查無資料", "查無資料", "查無資料", "查無資料", "查無資料", "查無資料" };
                return log;
            }
            else if (action == 3) 
            {
                string[] recipe = { "查無資料", "查無資料", "查無資料", "查無資料", "查無資料" };
                return recipe;
            }
            else
            {
                string[] reserve = { "查無資料", "查無資料", "查無資料", "查無資料" };
                return reserve;
            }
        
        }

    }
}
