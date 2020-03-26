
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using MySql.Data;
using MySql.Data.MySqlClient;
using BPI_fix.Models;
using System.Data;

namespace BPI_fix.Services
{
    class DBConServices
    {
        public MySqlConnection myConnect;
        List<HistoryModel> historyList = new List<HistoryModel>();
        List<BranchModel> branchList = new List<BranchModel>();
        List<ErrorModel> errorList = new List<ErrorModel>();

        public void DBCon()
        {
            Form1 f1 = new Form1();
            BPI bpi = new BPI();

            string DBConnection = "";
            if (f1.activeB == "BPI")
            {
                DBConnection = "datasource=localhost;port=3306;username=root;password=secret; convert zero datetime=True;";
                MessageBox.Show("Hello Test!!!");
              //  DBConnection = "islabank";
            }
            else
            {
                //  tableName;
                //  DBConnection = "";
                //DBConnection = "datasource=localhost;port=3306;username=root;password=secret; convert zero datetime=True;";
                DBConnection = "datasource=192.168.0.247;port=3306;username=root;password=CorpCaptive; convert zero datetime=True;";
                //MessageBox.Show("HELLO");
              //   DBConnection = "captive_database";
             }



            myConnect = new MySqlConnection(DBConnection);
            myConnect.Open();
        }
        public void DBClosed()
        {
            myConnect.Close();
        }


        public List<HistoryModel> GetHistory(List<HistoryModel> _history)
        {
            DBCon();
            string query = "SELECT BRSTN, ChequeName, MAX(CAST(EndingSerial as DECIMAL(18,0)))EndingSerial FROM captive_database.master_database_bpi WHERE ChequeName <>'' GROUP BY BRSTN, ChequeName";

            //  MySqlConnection conn = new MySqlConnection(myConnect);

            //conn.Open();

            MySqlCommand cmd = new MySqlCommand(query, myConnect);

            MySqlDataReader reader = cmd.ExecuteReader();

            while (reader.Read())
            {
                HistoryModel history = new HistoryModel();

                history.BRSTN = !reader.IsDBNull(0) ? reader.GetString(0) : "";

                history.ChequeName = !reader.IsDBNull(1) ? reader.GetString(1) : "";

                history.MaxEnding = !reader.IsDBNull(2) ? reader.GetInt64(2) : 0;

                _history.Add(history);
            }
            
            DBClosed();
            return _history;
            // RemoveDuplicatePreEncoded();
        }

        //private void RemoveDuplicatePreEncoded()
        //{
        //    var personalPre = historyList.Where(r => r.ChequeName == "Personal Pre-Encoded").ToList();

        //    var commercialPre = historyList.Where(r => r.ChequeName == "Commercial Pre-Encoded").ToList();

        //    personalPre.ForEach(x =>
        //    {
        //        var temp = historyList.FirstOrDefault(r => r.BRSTN == x.BRSTN && r.ChequeName == "Regular Personal");

        //        if (temp != null)
        //        {
        //            if (x.MaxEnding >= temp.MaxEnding)
        //                historyList.Remove(temp);
        //            else
        //                historyList.Remove(x);
        //        }
        //    });

        //    commercialPre.ForEach(y =>
        //    {
        //        var temp = historyList.FirstOrDefault(r => r.BRSTN == y.BRSTN && r.ChequeName == "Regular Commercial");

        //        if (temp != null)
        //        {
        //            if (y.MaxEnding >= temp.MaxEnding)
        //                historyList.Remove(temp);
        //            else
        //                historyList.Remove(y);
        //        }
        //    });
        //}
        public ErrorModel InsertDataBPI(ErrorModel _data, string _date,string _ChequeName)
        {
            MySqlCommand myCmd = new MySqlCommand();
            myCmd = new MySqlCommand("INSERT INTO captive_database.bpi_fix (BRSTN, BranchName, CheckType, OldSerial, CorrectSerial,Date) VALUES" +
                            "('" + _data.BRSTN + "','" + _data.BranchName.Replace("'"," ") + "','" + _ChequeName + "','" + _data.CurrentSerial + "','" + _data.HistorySerial + "','" + _date + "');", myConnect);

            myCmd.ExecuteNonQuery();
            return _data;
               
        }
        public ErrorModel InsertDataFBPI(ErrorModel _data, string _date, string _ChequeName)
        {
            MySqlCommand myCmd = new MySqlCommand();
            myCmd = new MySqlCommand("INSERT INTO captive_database.fbpi_fix (BRSTN, BranchName, CheckType, OldSerial, CorrectSerial,Date) VALUES" +
                            "('" + _data.BRSTN + "','" + _data.BranchName + "','" + _ChequeName + "','" + _data.CurrentSerial + "','" + _data.HistorySerial + "','" + _date + "');", myConnect);

            myCmd.ExecuteNonQuery();
            return _data;

        }
        public List<HistoryModel> GetHistoryFBPI(List<HistoryModel> _history)
        {
            DBCon();
            string query = "SELECT BRSTN, ChequeName, MAX(CAST(EndingSerial as DECIMAL(18,0)))EndingSerial FROM captive_database.master_database_fbpi WHERE ChequeName <>'' GROUP BY BRSTN, ChequeName";

            //  MySqlConnection conn = new MySqlConnection(myConnect);

            //conn.Open();

            MySqlCommand cmd = new MySqlCommand(query, myConnect);

            MySqlDataReader reader = cmd.ExecuteReader();

            while (reader.Read())
            {
                HistoryModel history = new HistoryModel();

                history.BRSTN = !reader.IsDBNull(0) ? reader.GetString(0) : "";

                history.ChequeName = !reader.IsDBNull(1) ? reader.GetString(1) : "";

                history.MaxEnding = !reader.IsDBNull(2) ? reader.GetInt64(2) : 0;

                _history.Add(history);
            }

            DBClosed();
            return _history;
            // RemoveDuplicatePreEncoded();
        }

    }

}
