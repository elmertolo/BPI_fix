using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Data.OleDb;
using BPI_fix.Models;
using MySql.Data.MySqlClient;
using MySql.Data;
using BPI_fix.Services;

namespace BPI_fix
{
    public partial class BPI : Form
    {
        public BPI()
        {
            InitializeComponent();
        }
        DBConServices dbcon = new DBConServices();
        //public string DBConnection = "datasource=192.168.0.247;port=3306;username=root;password=CorpCaptive; convert zero datetime=True;";
        public string DBConnection = "";
        List<BranchModel> branchList = new List<BranchModel>();

        List<HistoryModel> historyList = new List<HistoryModel>();

        List<ErrorModel> errorList = new List<ErrorModel>();
        private void checkToolStripMenuItem_Click(object sender, EventArgs e)
        {
            
            LoadBranches();
            GetRegular();
            GetMC();
            GetB3();
            GetB4();
            GetB6();
            dbcon.DBCon();
            dgvBPI.Rows.Clear();

            dgvBPI.Refresh();
            dbcon.GetHistory(historyList);
            CheckErrors();
        

            BindingSource checkBind = new BindingSource();

            checkBind.DataSource = errorList;

            dgvBPI.DataSource = checkBind;
            //MessageBox.Show(branchList[0].LastNo_Regular.ToString() + " - " + branchList[0].LastNo_B3.ToString() + " - " +
            //     branchList[0].LastNo_B4.ToString() + " - " + branchList[0].LastNo_B6.ToString());
            //MessageBox.Show("Done!");
        }

        private void dgvBPI_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {

        }
        private void LoadBranches()
        {
            string connString = @"Provider=Microsoft.Jet.OLEDB.4.0; Data Source="+Application.StartupPath+"; Extended Properties=dBASE III;";
            //string connString = @"Provider=Microsoft.Jet.OLEDB.4.0; Data Source=\\192.168.0.247\captive\Auto\BPI\Regular_Orders; Extended Properties=dBASE III;";

            OleDbConnection conn = new OleDbConnection(connString);

            conn.Open();

            DataSet dataset = new DataSet();
            string script = "Select BRSTN, ADDRESS1 from Temp1";
            OleDbDataAdapter da = new OleDbDataAdapter(script,conn);

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
            string connString = @"Provider=Microsoft.Jet.OLEDB.4.0; Data Source=" + Application.StartupPath + @"\Regular; Extended Properties=dBASE III;";
            //string connString = "Provider=Microsoft.Jet.OLEDB.4.0; Data Source=\\\\192.168.0.247\\captive\\Auto\\BPI\\Regular_Orders\\Regular; Extended Properties=DBASE IV;";

            OleDbConnection conn = new OleDbConnection(@connString);

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
            string connString = @"Provider=Microsoft.Jet.OLEDB.4.0; Data Source=" + Application.StartupPath + @"\RBPI_MC; Extended Properties=dBASE III;";
            //string connString = @"Provider=Microsoft.Jet.OLEDB.4.0; Data Source=\\192.168.0.247\captive\Auto\BPI\Regular_Orders\RBPI_MC; Extended Properties=DBASE IV;";

            OleDbConnection conn = new OleDbConnection(@connString);

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
        private void GetB3()
        {
            string connString = @"Provider=Microsoft.Jet.OLEDB.4.0; Data Source=" + Application.StartupPath + @"\B3; Extended Properties=dBASE III;";
            //string connString = @"Provider=Microsoft.Jet.OLEDB.4.0; Data Source=\\192.168.0.247\captive\Auto\BPI\Regular_Orders\B3; Extended Properties=DBASE IV;";

            OleDbConnection conn = new OleDbConnection(@connString);

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
        private void GetB4()
        {
            string connString = @"Provider=Microsoft.Jet.OLEDB.4.0; Data Source=" + Application.StartupPath + @"\B4; Extended Properties=dBASE III;";
            //string connString = @"Provider=Microsoft.Jet.OLEDB.4.0; Data Source=\\192.168.0.247\captive\Auto\BPI\Regular_Orders\B4; Extended Properties=DBASE IV;";

            OleDbConnection conn = new OleDbConnection(@connString);

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

        private void GetB6()
        {
            string connString = @"Provider=Microsoft.Jet.OLEDB.4.0; Data Source=" + Application.StartupPath + @"\B6; Extended Properties=dBASE III;";
            //string connString = @"Provider=Microsoft.Jet.OLEDB.4.0; Data Source=\\192.168.0.247\captive\Auto\BPI\Regular_Orders\B6; Extended Properties=DBASE IV;";

            OleDbConnection conn = new OleDbConnection(@connString);

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
                    branch.LastNo_B6 = LastNO;

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
                    if (hist.ChequeName == "Regular Personal Check (B1)" || hist.ChequeName == "Regular Personal Check (B2)"
                    || hist.ChequeName == "Regular Personal Check (B7)" || hist.ChequeName == "Regular Personal Check (B8)"
                    || hist.ChequeName == "Regular Commercial Check (B1)" || hist.ChequeName == "Regular Commercial Check (B2)"
                    || hist.ChequeName == "Regular Commercial Check (B7)" || hist.ChequeName == "Regular Commercial Check (B8)")
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
                    else if (hist.ChequeName == "Direct Check (B4)")
                    {
                        if (hist.MaxEnding > branch.LastNo_B4)
                        {
                            ErrorModel error = new ErrorModel
                            {
                                BRSTN = branch.BRSTN,
                                BranchName = branch.BranchName,
                                CheckType = "Direct Check (B4)",
                                HistorySerial = hist.MaxEnding,
                                CurrentSerial = branch.LastNo_B4
                            };

                            errorList.Add(error);
                        }
                    }
                    else if (hist.ChequeName == "Dollar Check (B3)")
                    {
                        if (hist.MaxEnding > branch.LastNo_B3)
                        {
                            ErrorModel error = new ErrorModel
                            {
                                BRSTN = branch.BRSTN,
                                BranchName = branch.BranchName,
                                CheckType = "Dollar Check (B3)",
                                HistorySerial = hist.MaxEnding,
                                CurrentSerial = branch.LastNo_B3
                            };

                            errorList.Add(error);
                        }
                    }
                    else if (hist.ChequeName == "Starter Check")
                    {
                        if (hist.MaxEnding > branch.LastNo_B6)
                        {
                            ErrorModel error = new ErrorModel
                            {
                                BRSTN = branch.BRSTN,
                                BranchName = branch.BranchName,
                                CheckType = "Starter Check",
                                HistorySerial = hist.MaxEnding,
                                CurrentSerial = branch.LastNo_B6
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
                conn = new OleDbConnection(@"Provider=Microsoft.Jet.OLEDB.4.0; Data Source=" + Application.StartupPath + @"\Regular; Extended Properties=DBASE IV;");
                //conn = new OleDbConnection("Provider=Microsoft.Jet.OLEDB.4.0; Data Source=\\\\192.168.0.247\\captive\\Auto\\BPI\\Regular_Orders\\Regular; Extended Properties=DBASE IV;");

                conn.Open();

                Regular.ForEach(p =>
                {
                    //UPDATE REF
                    command = new OleDbCommand("UPDATE Ref SET LASTNO = '" + p.HistorySerial + "' WHERE RTNO = '" + p.BRSTN +"'", conn);

                    command.ExecuteNonQuery();

                    ////SAVE TO HISTORY
                    dbcon.InsertDataBPI(p, DateTime.Now.ToString("yyyy-MM-dd"), "Regular Checks");
                });
                conn.Close();
            }

            var ManagersCheck = errorList.Where(r => r.CheckType == "Manager's Checks").ToList();

            if (ManagersCheck != null)
            {
                conn = new OleDbConnection(@"Provider=Microsoft.Jet.OLEDB.4.0; Data Source=" + Application.StartupPath + @"\RBPI_MC; Extended Properties=DBASE IV;");
                //conn = new OleDbConnection("Provider=Microsoft.Jet.OLEDB.4.0; Data Source=\\\\192.168.0.254\\captive\\Auto\\SBTC\\Regular\\MC; Extended Properties=DBASE IV;");

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

            var B3 = errorList.Where(r => r.CheckType == "Dollar Check (B3)").ToList();

            if (B3 != null)
            {
                conn = new OleDbConnection(@"Provider=Microsoft.Jet.OLEDB.4.0; Data Source=" + Application.StartupPath + @"\B3; Extended Properties=DBASE IV;");
                //conn = new OleDbConnection("Provider=Microsoft.Jet.OLEDB.4.0; Data Source=\\\\192.168.0.254\\captive\\Auto\\SBTC\\Regular\\GiftCheck; Extended Properties=DBASE IV;");

                conn.Open();

                B3.ForEach(b =>
                {
                    //UPDATE REF
                    command = new OleDbCommand("UPDATE Ref SET LASTNO = '" + b.HistorySerial + "' WHERE RTNO = '" + b.BRSTN + "'", conn);

                    command.ExecuteNonQuery();

                    ////SAVE TO HISTORY
                    dbcon.InsertDataBPI(b, DateTime.Now.ToString("yyyy-MM-dd"), "Dollar Check (B3)");
                });

                conn.Close();
            }

            var B4 = errorList.Where(r => r.CheckType == "Direct Check (B4)").ToList();
            

            if (B4 != null)
            {
                conn = new OleDbConnection(@"Provider=Microsoft.Jet.OLEDB.4.0; Data Source=" + Application.StartupPath + @"\B4; Extended Properties=DBASE IV;");
                //conn = new OleDbConnection("Provider=Microsoft.Jet.OLEDB.4.0; Data Source=\\\\192.168.0.254\\captive\\Auto\\SBTC\\Regular\\CheckOne; Extended Properties=DBASE IV;");

                conn.Open();

                B4.ForEach(gc =>
                {
                    //UPDATE REF
                    command = new OleDbCommand("UPDATE Ref SET LASTNO = '" + gc.HistorySerial + "' WHERE RTNO = '" + gc.BRSTN + "'", conn);

                    command.ExecuteNonQuery();

                    ////SAVE TO HISTORY
                    dbcon.InsertDataBPI(gc, DateTime.Now.ToString("yyyy-MM-dd"), "Direct Check (B4)");
                });

               conn.Close();
            }

            var B6 = errorList.Where(r => r.CheckType == "Starter Check").ToList();
            

            if (B6 != null)
            {
                conn = new OleDbConnection(@"Provider=Microsoft.Jet.OLEDB.4.0; Data Source=" + Application.StartupPath + @"\B6; Extended Properties=DBASE IV;");
            //    conn = new OleDbConnection("Provider=Microsoft.Jet.OLEDB.4.0; Data Source=\\\\192.168.0.254\\captive\\Auto\\SBTC\\Regular\\CheckOne; Extended Properties=DBASE IV;");

                conn.Open();

                B6.ForEach(gc =>
                {
                    //UPDATE REF
                    command = new OleDbCommand("UPDATE Ref SET LASTNO = '" + gc.HistorySerial + "' WHERE RTNO = '" + gc.BRSTN + "'", conn);

                    command.ExecuteNonQuery();

                    ////SAVE TO HISTORY
                    dbcon.InsertDataBPI(gc, DateTime.Now.ToString("yyyy-MM-dd"), "Starter Check");
                });

               
                conn.Close();
            }

            dbcon.DBClosed();
        }

        private void fIxToolStripMenuItem_Click(object sender, EventArgs e)
        {
            string value = "";

            DialogResult result = InputBox("Admin Credentials", "Input Admin Password", ref value);

            if (result == DialogResult.OK)
            {
                if (value == "secret")
                {
                    FixError();

                    MessageBox.Show("Bank of the Philippine Islands Database has been Fixed");
                }
                else
                    MessageBox.Show("Invalid Password", "System Error");
            }
            dgvBPI.Refresh();
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

        private void menuStrip1_ItemClicked(object sender, ToolStripItemClickedEventArgs e)
        {

        }

        private void exitToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Environment.Exit(0);
        }
    }
}
