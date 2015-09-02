using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ArrangeOffice
{
    class Control
    {
        private int control = 0;
        private int year = 0;
        private int month = 0;
        private int day = 0;


        //0 all can add
        //1 log_3
        //2 reserve_3
        //3 log_5
        //4 reserve_5
        //5 log_6
        //6 reserve_6
        //7 history_log_3
        //8 history_reserve_3
        //9 history_log_5
        //10 history_reserve_5
        //11 history_log_6
        //12 history_reserve_6


        public int changControl(string name)
        {

            switch (name)
            {
                case "log_3":
                    control = 1;
                    break;
                case "reserve_3":
                    control = 2;
                    break;
                case "log_5":
                    control = 3;
                    break;
                case "reserve_5":
                    control = 4;
                    break;
                case "log_6":
                    control = 5;
                    break;
                case "reserve_6":
                    control = 6;
                    break;
                case "history_log":
                    control = 7;
                    break;
                case "history_reserve":
                    control = 8;
                    break;
                case "sh_log":
                    control = 9;
                    break;
                case "sh_reserve":
                    control = 10;
                    break;
                case "sh_now_log":
                    control = 11;
                    break;
                case "ac_name":
                    control = 12;
                    break;
                case "ac_content":
                    control = 13;
                    break;
                case "onlineState":
                    control = 14;
                    break;
                case "add_recipe":
                    control = 15;
                    break;
                case "modify_recipe_name":
                    control = 16;
                    break;
                case "modify_recipe_content":
                    control = 17;
                    break;
                case "modify_recipe_all_spice":
                    control = 18;
                    break;
                case "modify_spice":
                    control = 19;
                    break;
                case "directRecipe":
                    control = 20;
                    break;
                case "directRecipeName":
                    control = 21;
                    break;

                    /*
                case "history_log_3":
                    control = 7;
                    break;
                case "history_reserve_3":
                    control = 8;
                    break;
                case "history_log_5":
                    control = 9;
                    break;
                case "history_reserve_5":
                    control = 10;
                    break;
                case "history_log_6":
                    control = 11;
                    break;
                case "history_reserve_6":
                    control = 12;
                    break;
                      */

            }
            return control;


        }
        public int showControl()
        {
            return control;
        }

        //now show day
        //
        //
        public void changDay(int year, int month, int day)
        {
            this.year = year;
            this.month = month;
            this.day = day;
        }
        public void plusDay()
        {
            day = day + 1;
        }
        public void minusDay()
        {
            day = day - 1;
        }
        public int nowYear()
        {
            return year;
        }
        public int nowMonth()
        {
            return month;
        }
        public int nowDay()
        {
            return day;
        }

        public string target()
        {
            string tar = null;
            switch (control)
            {

                case 1:
                    tar = "WH_HISTORY 3";
                    break;
                case 2:
                    tar = "WH_NOW 3";
                    break;
                case 3:
                    tar = "WH_HISTORY 5";
                    break;
                case 4:
                    tar = "WH_NOW 5";
                    break;
                case 5:
                    tar = "WH_HISTORY 6";
                    break;
                case 6:
                    tar = "WH_NOW 6";
                    break;
                case 7:
                    tar = "WH_HISTORY 3";
                    break;
                case 8:
                    tar = "WH_NOW 3";
                    break;
                case 9:
                    tar = "WH_HISTORY 5";
                    break;
                case 10:
                    tar = "WH_NOW 5";
                    break;
                case 11:
                    tar = "WH_HISTORY 6";
                    break;
                case 12:
                    tar = "WH_NOW 6";
                    break;

            }
            return tar;

        }


    }
}
