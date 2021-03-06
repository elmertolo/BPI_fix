﻿using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using MySql.Data;
using MySql.Data.MySqlClient;
using BPI_fix.Models;
using BPI_fix.Services;
using System.Data.OleDb;

namespace BPI_fix
{
    public partial class FBPI : Form
    {
        public FBPI()
        {
            InitializeComponent();
        }
        DBConServices dbcon = new DBConServices();
        //public string DBConnection = "datasource=192.168.0.247;port=3306;username=root;password=CorpCaptive; convert zero datetime=True;";
        public string DBConnection = "";
        List<BranchModel> branchList = new List<BranchModel>();

        List<HistoryModel> historyList = new List<HistoryModel>();

        List<ErrorModel> errorList = new List<ErrorModel>();
        private void dataGridView1_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {

        }

        private void menuStrip1_ItemClicked(object sender, ToolStripItemClickedEventArgs e)
        {
           
        }

        private void FBPI_Load(object sender, EventArgs e)
        {

        }

        private void checkToolStripMenuItem_Click(object sender, EventArgs e)
        {
            LoadBranches();
            GetRegular();
            GetMC();
            GetF5();
            GetF6();
            dbcon.DBCon();
               dataGridView1.Rows.Clear();

               dataGridView1.Refresh();
            dbcon.GetHistoryFBPI(historyList);
            CheckErrors();


            BindingSource checkBind = new BindingSource();

            checkBind.DataSource = errorList;

            dataGridView1.DataSource = checkBind;
           // MessageBox.Show(branchList[0].LastNo_Regular.ToString() + " - " + branchList[0].LastNo_MC.ToString() + " - " + branchList[0].LastNo_B3.ToString() + " - " + branchList[0].LastNo_B4.ToString());
        }
        private void LoadBranches()
        {
            //string connString = @"Provider=Microsoft.Jet.OLEDB.4.0; Data Source=" + Application.StartupPath + "; Extended Properties=dBASE III;";
            //string connString = @"Provider=Microsoft.Jet.OLEDB.4.0; Data Source=\\192.168.0.247\captive\Auto\BPI\Regular_Orders; Extended Properties=dBASE III;";
            OleDbConnection conn = new OleDbConnection(@"Provider=Microsoft.Jet.OLEDB.4.0; Data Source=\\192.168.0.247\\captive\\Auto\\BPI\\Regular_Orders; Extended Properties=DBASE IV;");
            // OleDbConnection conn = new OleDbConnection(connString);

            conn.Open();

            DataSet dataset = new DataSet();
            string script = "Select BRSTN, ADDRESS1 from Temp2";
            OleDbDataAdapter da = new OleDbDataAdapter(script, conn);

            da.Fill(dataset);

            DataTable dt = dataset.Tables[0];

            foreach (DataRow dr in dt.Rows)
            {
                BranchModel branch = new BranchModel();

                branch.BRSTN = dr[0].ToString();
                branch.BranchName = dr[1].ToString();

                branchList.Add(branch);
            }

            conn.Close();
        }
        private void GetRegular()
        {
            //string connString = @"Provider=Microsoft.Jet.OLEDB.4.0; Data Source=" + Application.StartupPath + @"\Family; Extended Properties=dBASE III;";
            //string connString = "Provider=Microsoft.Jet.OLEDB.4.0; Data Source=\\\\192.168.0.247\\captive\\Auto\\BPI\\Regular_Orders\\Regular; Extended Properties=DBASE IV;";
            OleDbConnection conn = new OleDbConnection(@"Provider=Microsoft.Jet.OLEDB.4.0; Data Source=\\192.168.0.247\\captive\\Auto\\BPI\\Regular_Orders\\Family; Extended Properties=DBASE IV;");
            //OleDbConnection conn = new OleDbConnection(@connString);

            conn.Open();

            DataSet dataset = new DataSet();

            OleDbDataAdapter comm = new OleDbDataAdapter("SELECT RTNO, LASTNO FROM Ref", conn);

            comm.Fill(dataset);

            DataTable dt = dataset.Tables[0];

            foreach (DataRow dr in dt.Rows)
            {
                string BRSTN = dr[0].ToString();
                //string CheckType = dr[1].ToString();
                Int64 LastNO = Int64.Parse(dr[1].ToString().Replace("'", ""));

                var branch = branchList.FirstOrDefault(r => r.BRSTN == BRSTN);

                if (branch != null)
                    branch.LastNo_Regular = LastNO;

            }

            conn.Close();
        }

        private void GetMC()
        {
            //string connString = @"Provider=Microsoft.Jet.OLEDB.4.0; Data Source=" + Application.StartupPath + @"\FBPI_MC; Extended Properties=dBASE III;";
            //string connString = @"Provider=Microsoft.Jet.OLEDB.4.0; Data Source=\\192.168.0.247\captive\Auto\BPI\Regular_Orders\RBPI_MC; Extended Properties=DBASE IV;";
            OleDbConnection conn = new OleDbConnection(@"Provider=Microsoft.Jet.OLEDB.4.0; Data Source=\\192.168.0.247\\captive\\Auto\\BPI\\Regular_Orders\\FBPI_MC; Extended Properties=DBASE IV;");
            //OleDbConnection conn = new OleDbConnection(@connString);

            conn.Open();

            DataSet dataset = new DataSet();

            OleDbDataAdapter comm = new OleDbDataAdapter("SELECT RTNO, LASTNO FROM Ref", conn);

            comm.Fill(dataset);

            DataTable dt = dataset.Tables[0];

            foreach (DataRow dr in dt.Rows)
            {
                string BRSTN = dr[0].ToString();
                // string CheckType = dr[1].ToString();
                Int64 LastNO = Int64.Parse(dr[1].ToString().Replace("'", ""));

                var branch = branchList.FirstOrDefault(r => r.BRSTN == BRSTN);

                if (branch != null)
                    branch.LastNo_MC = LastNO;
            }

            conn.Close();
        }
        private void GetF5()
        {
            //string connString = @"Provider=Microsoft.Jet.OLEDB.4.0; Data Source=" + Application.StartupPath + @"\F5; Extended Properties=dBASE III;";
            //string connString = @"Provider=Microsoft.Jet.OLEDB.4.0; Data Source=\\192.168.0.247\captive\Auto\BPI\Regular_Orders\B3; Extended Properties=DBASE IV;";
            //  OleDbConnection conn = new OleDbConnection(@connString);
            OleDbConnection conn = new OleDbConnection(@"Provider=Microsoft.Jet.OLEDB.4.0; Data Source=\\192.168.0.247\\captive\\Auto\\BPI\\Regular_Orders\\F5; Extended Properties=DBASE IV;");
            conn.Open();

            DataSet dataset = new DataSet();

            OleDbDataAdapter comm = new OleDbDataAdapter("SELECT RTNO, LASTNO FROM Ref", conn);

            comm.Fill(dataset);

            DataTable dt = dataset.Tables[0];

            foreach (DataRow dr in dt.Rows)
            {
                string BRSTN = dr[0].ToString();
                //string CheckType = dr[1].ToString();
                Int64 LastNO = Int64.Parse(dr[1].ToString().Replace("'", ""));

                var branch = branchList.FirstOrDefault(r => r.BRSTN == BRSTN);

                if (branch != null)
                    branch.LastNo_B3 = LastNO;

            }

            conn.Close();
        }
        private void GetF6()
        {

            //string connString = @"Provider=Microsoft.Jet.OLEDB.4.0; Data Source=" + Application.StartupPath + @"\F6; Extended Properties=dBASE III;";
            OleDbConnection conn = new OleDbConnection(@"Provider=Microsoft.Jet.OLEDB.4.0; Data Source=\\192.168.0.247\\captive\\Auto\\BPI\\Regular_Orders\\F6; Extended Properties=DBASE IV;");

            //OleDbConnection conn = new OleDbConnection(@connString);

            conn.Open();

            DataSet dataset = new DataSet();

            OleDbDataAdapter comm = new OleDbDataAdapter("SELECT RTNO, LASTNO FROM Ref", conn);

            comm.Fill(dataset);

            DataTable dt = dataset.Tables[0];

            foreach (DataRow dr in dt.Rows)
            {
                string BRSTN = dr[0].ToString();
                //string CheckType = dr[1].ToString();
                Int64 LastNO = Int64.Parse(dr[1].ToString().Replace("'", ""));

                var branch = branchList.FirstOrDefault(r => r.BRSTN == BRSTN);

                if (branch != null)
                    branch.LastNo_B4 = LastNO;

            }

            conn.Close();
        }

   
        private void CheckErrors()
        {
            historyList.ForEach(hist =>
            {
                var branch = branchList.FirstOrDefault(r => r.BRSTN == hist.BRSTN);

                if (branch != null)
                {
                    if (hist.ChequeName == "Family Personal Check (F1)" || hist.ChequeName == "Family Personal Check (F2)"
                    || hist.ChequeName == "Family Personal Check (F7)" || hist.ChequeName == "Family Personal Check (F8)"
                    || hist.ChequeName == "Family Commercial Check (F1)" || hist.ChequeName == "Family Commercial Check (F2)"
                    || hist.ChequeName == "Family Commercial Check (F7)" || hist.ChequeName == "Family Commercial Check (F8)")
                    {
                        if (hist.MaxEnding > branch.LastNo_Regular)
                        {
                            ErrorModel error = new ErrorModel
                            {
                                BRSTN = branch.BRSTN,
                                BranchName = branch.BranchName,
                                CheckType = "Regular Checks",
                                HistorySerial = hist.MaxEnding,
                                CurrentSerial = branch.LastNo_Regular
                            };

                            errorList.Add(error);
                        }
                    }
                    else if (hist.ChequeName == "Manager's Checks")
                    {
                        if (hist.MaxEnding > branch.LastNo_MC)
                        {
                            ErrorModel error = new ErrorModel
                            {
                                BRSTN = branch.BRSTN,
                                BranchName = branch.BranchName,
                                CheckType = "Manager's Checks",
                                HistorySerial = hist.MaxEnding,
                                CurrentSerial = branch.LastNo_MC
                            };

                            errorList.Add(error);
                        }
                    }
                    else if (hist.ChequeName == "Mortgage Check (F5A)")
                    {
                        if (hist.MaxEnding > branch.LastNo_B3)
                        {
                            ErrorModel error = new ErrorModel
                            {
                                BRSTN = branch.BRSTN,
                                BranchName = branch.BranchName,
                                CheckType = "Mortgage Check (F5A)",
                                HistorySerial = hist.MaxEnding,
                                CurrentSerial = branch.LastNo_B4
                            };

                            errorList.Add(error);
                        }
                    }
                    else if (hist.ChequeName == "Starter Check (F6A)")
                    {
                        if (hist.MaxEnding > branch.LastNo_B4)
                        {
                            ErrorModel error = new ErrorModel
                            {
                                BRSTN = branch.BRSTN,
                                BranchName = branch.BranchName,
                                CheckType = "Starter Check (F6A)",
                                HistorySerial = hist.MaxEnding,
                                CurrentSerial = branch.LastNo_B3
                            };

                            errorList.Add(error);
                        }
                    }
                  
                }
            });
        }
        private void FixError()
        {
            OleDbConnection conn;

            OleDbCommand command;
            dbcon.DBCon();


            var Regular = errorList.Where(r => r.CheckType == "Regular Checks").ToList();


            if (Regular != null)
            {
                //conn = new OleDbConnection(@"Provider=Microsoft.Jet.OLEDB.4.0; Data Source=" + Application.StartupPath + @"\Family; Extended Properties=DBASE IV;");
                conn = new OleDbConnection(@"Provider=Microsoft.Jet.OLEDB.4.0; Data Source=\\192.168.0.247\\captive\\Auto\\BPI\\Regular_Orders\\Family; Extended Properties=DBASE IV;");

                conn.Open();

                Regular.ForEach(p =>
                {
                    //UPDATE REF
                    command = new OleDbCommand("UPDATE Ref SET LASTNO = '" + p.HistorySerial + "' WHERE RTNO = '" + p.BRSTN + "'", conn);

                    command.ExecuteNonQuery();

                    ////SAVE TO HISTORY
                    dbcon.InsertDataFBPI(p, DateTime.Now.ToString("yyyy-MM-dd"), "Regular Checks");
                });
                conn.Close();
            }

            var ManagersCheck = errorList.Where(r => r.CheckType == "Manager's Checks").ToList();

            if (ManagersCheck != null)
            {
                //conn = new OleDbConnection(@"Provider=Microsoft.Jet.OLEDB.4.0; Data Source=" + Application.StartupPath + @"\FBPI_MC; Extended Properties=DBASE IV;");
                conn = new OleDbConnection(@"Provider=Microsoft.Jet.OLEDB.4.0; Data Source=\\192.168.0.247\\captive\\Auto\\BPI\\Regular_Orders\\FBPI_MC; Extended Properties=DBASE IV;");

                conn.Open();

                ManagersCheck.ForEach(mc =>
                {
                    //UPDATE REF
                    command = new OleDbCommand("UPDATE Ref SET LASTNO = '" + mc.HistorySerial + "' WHERE RTNO = '" + mc.BRSTN + "'", conn);

                    command.ExecuteNonQuery();

                    ////SAVE TO HISTORY
                    dbcon.InsertDataBPI(mc, DateTime.Now.ToString("yyyy-MM-dd"), "Managers Check");
                });

                conn.Close();
            }

            var F5 = errorList.Where(r => r.CheckType == "Mortgage Check (F5A)").ToList();

            if (F5 != null)
            {
                //conn = new OleDbConnection(@"Provider=Microsoft.Jet.OLEDB.4.0; Data Source=" + Application.StartupPath + @"\F5; Extended Properties=DBASE IV;");
                conn = new OleDbConnection(@"Provider=Microsoft.Jet.OLEDB.4.0; Data Source=\\192.168.0.247\\captive\\Auto\\BPI\\Regular_Orders\\F5; Extended Properties=DBASE IV;");

                conn.Open();

                F5.ForEach(b =>
                {
                    //UPDATE REF
                    command = new OleDbCommand("UPDATE Ref SET LASTNO = '" + b.HistorySerial + "' WHERE RTNO = '" + b.BRSTN + "'", conn);

                    command.ExecuteNonQuery();

                    ////SAVE TO HISTORY
                    dbcon.InsertDataFBPI(b, DateTime.Now.ToString("yyyy-MM-dd"), "Mortgage Check (F5A)");
                });

                conn.Close();
            }

            var F6 = errorList.Where(r => r.CheckType == "Starter Check (F6A)").ToList();


            if (F6 != null)
            {
                //conn = new OleDbConnection(@"Provider=Microsoft.Jet.OLEDB.4.0; Data Source=" + Application.StartupPath + @"\F6; Extended Properties=DBASE IV;");
                conn = new OleDbConnection(@"Provider=Microsoft.Jet.OLEDB.4.0; Data Source=\\192.168.0.247\\captive\\Auto\\BPI\\Regular_Orders\\F6; Extended Properties=DBASE IV;");

                conn.Open();

                F6.ForEach(gc =>
                {
                    //UPDATE REF
                    command = new OleDbCommand("UPDATE Ref SET LASTNO = '" + gc.HistorySerial + "' WHERE RTNO = '" + gc.BRSTN + "'", conn);

                    command.ExecuteNonQuery();

                    ////SAVE TO HISTORY
                    dbcon.InsertDataFBPI(gc, DateTime.Now.ToString("yyyy-MM-dd"), "Starter Check (F6A)");
                });

                conn.Close();
            }
            
            dbcon.DBClosed();
        }
        private static DialogResult InputBox(string title, string promptText, ref string value)
        {
            Form form = new Form();
            Label label = new Label();
            TextBox textBox = new TextBox();
            Button buttonOk = new Button();
            Button buttonCancel = new Button();

            form.Text = title;
            label.Text = promptText;
            textBox.Text = value;

            buttonOk.Text = "OK";
            buttonCancel.Text = "Cancel";
            buttonOk.DialogResult = DialogResult.OK;
            buttonCancel.DialogResult = DialogResult.Cancel;

            label.SetBounds(9, 20, 372, 13);
            textBox.SetBounds(12, 36, 372, 20);
            buttonOk.SetBounds(228, 72, 75, 23);
            buttonCancel.SetBounds(309, 72, 75, 23);

            label.AutoSize = true;
            textBox.Anchor = textBox.Anchor | AnchorStyles.Right;
            buttonOk.Anchor = AnchorStyles.Bottom | AnchorStyles.Right;
            buttonCancel.Anchor = AnchorStyles.Bottom | AnchorStyles.Right;
            textBox.PasswordChar = '*';

            form.ClientSize = new Size(396, 107);
            form.Controls.AddRange(new Control[] { label, textBox, buttonOk, buttonCancel });
            form.ClientSize = new Size(Math.Max(300, label.Right + 10), form.ClientSize.Height);
            form.FormBorderStyle = FormBorderStyle.FixedDialog;
            form.StartPosition = FormStartPosition.CenterScreen;
            form.MinimizeBox = false;
            form.MaximizeBox = false;
            form.AcceptButton = buttonOk;
            form.CancelButton = buttonCancel;

            DialogResult dialogResult = form.ShowDialog();
            value = textBox.Text;
            return dialogResult;
        }

        private void exitToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Environment.Exit(0);
        }

        private void fixToolStripMenuItem_Click(object sender, EventArgs e)
        {
            string value = "";

            DialogResult result = InputBox("Admin Credentials", "Input Admin Password", ref value);

            if (result == DialogResult.OK)
            {
                if (value == "secret")
                {
                    FixError();

                    MessageBox.Show("Security Bank Database has been Fixed");
                }
                else
                    MessageBox.Show("Invalid Password", "System Error");
                dataGridView1.Refresh();
            }
        }
    }
}
