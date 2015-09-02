using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data;

namespace ArrangeOffice
{
    class HandleDataTable
    {
        public DataTable handleDataLog(DataTable dataLog)
        {

            dataLog.Columns.Add("進出貨時間");
            dataLog.Columns.Add("進出貨動作");
            dataLog.Columns.Add("物料編號");
            dataLog.Columns.Add("物料名稱");
            dataLog.Columns.Add("物料數量");
            dataLog.Columns.Add("物料單位");
            dataLog.Columns.Add("負責員工姓名");
            return dataLog;
        }
        public DataTable handleDataReserve(DataTable dataReserve)
        {

            dataReserve.Columns.Add("物料編號");
            dataReserve.Columns.Add("物料名稱");
            dataReserve.Columns.Add("物料數量");
            dataReserve.Columns.Add("物料單位");

            return dataReserve;
        }

        public DataTable handleRecipe(DataTable dataRecpie)
        {
            
            dataRecpie.Columns.Add("香料編號");
            dataRecpie.Columns.Add("存放倉位");
            dataRecpie.Columns.Add("香料名稱");
            dataRecpie.Columns.Add("香料重量");
            dataRecpie.Columns.Add("香料單位");         

            return dataRecpie;
        }
        public DataTable handleAddRecipe(DataTable dataAddRecpie)
        {

            dataAddRecpie.Columns.Add("香料編號");
            dataAddRecpie.Columns.Add("存放倉位");
            dataAddRecpie.Columns.Add("香料名稱");
            dataAddRecpie.Columns.Add("香料單位");

            return dataAddRecpie;
        }

    }
}
