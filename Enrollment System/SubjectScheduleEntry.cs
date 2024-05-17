using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Data.OleDb;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Enrollment_System
{
    public partial class SubjectScheduleEntry : Form
    {
        public SubjectScheduleEntry()
        {
            InitializeComponent();
        }
        string connectionString = @"Provider=Microsoft.ACE.OLEDB.12.0;Data Source=C:\Users\kyle\Desktop\APPSDEV FINAL\Enrollment System\DATABASE FINAL.accdb";

        private void SubjectScheduleEntry_Load(object sender, EventArgs e)
        {

        }
        bool subjectSearch = false;
        private void SaveButton_Click(object sender, EventArgs e)
        {
            bool conflict = false;
            if (subjectSearch == true)
            {
                try
                {


                    OleDbConnection thisConnection2 = new OleDbConnection(@"Provider=Microsoft.ACE.OLEDB.12.0;Data Source=C:\Users\kyle\Desktop\APPSDEV FINAL\Enrollment System\DATABASE FINAL.accdb");
                    thisConnection2.Open();

                    string commandText2 = "SELECT * FROM SUBJECTSCHEDFILE";
                    OleDbCommand thisCommand2 = thisConnection2.CreateCommand();
                    thisCommand2.CommandText = commandText2;

                    OleDbDataReader thisReader2 = thisCommand2.ExecuteReader();
                    /*while (thisReader2.Read())
                    {
                        //Overall, this line of code retrieves a value from the database reader, converts it to a DateTime object, and assigns it to the startTime variable for further usage in the program.
                        DateTime startTime = DateTime.Parse(thisReader2["SFSTARTTIME"].ToString());
                        DateTime endTime = DateTime.Parse(thisReader2["SFENDTIME"].ToString());
                        DateTime start = DateTime.Parse(StartTimePicker.Value.ToString("hh:mm tt"));
                        DateTime end = DateTime.Parse(EndTimePicker.Value.ToString("hh:mm tt"));



                        if (DaysTextBox.Text.Contains('M') && (thisReader2["SSFDAYS"].ToString().Trim().Contains('M') || thisReader2["SSFDAYS"].ToString().Trim() == DaysTextBox.Text))
                        {
                            if ((((start >= startTime && start < endTime) || (end > startTime && end <= endTime)) || (start < startTime && end > endTime)) && thisReader2["SSFROOM"].ToString().Trim() == RoomTextBox.Text)
                            {
                                conflict = true;
                                break;
                            }
                        }
                        else if (DaysTextBox.Text.Contains('T') && (thisReader2["SSFDAYS"].ToString().Trim().Contains('T') && (!(thisReader2["SSFDAYS"].ToString().Trim().Contains("SAT"))) || thisReader2["SSFDAYS"].ToString().Trim() == DaysTextBox.Text))
                        {
                            if ((((start >= startTime && start < endTime) || (end > startTime && end <= endTime)) || (start < startTime && end > endTime)) && thisReader2["SSFROOM"].ToString().Trim() == RoomTextBox.Text)
                            {
                                conflict = true;
                                break;
                            }
                        }

                        else if (DaysTextBox.Text.Contains('W') && (thisReader2["SSFDAYS"].ToString().Trim().Contains('W') || thisReader2["SSFDAYS"].ToString().Trim() == DaysTextBox.Text))
                        {
                            if ((((start >= startTime && start < endTime) || (end > startTime && end <= endTime)) || (start < startTime && end > endTime)) && thisReader2["SSFROOM"].ToString().Trim() == RoomTextBox.Text)
                            {
                                conflict = true;
                                break;
                            }
                        }

                        else if (DaysTextBox.Text.Contains("TH") && (thisReader2["SSFDAYS"].ToString().Trim().Contains("TH") || thisReader2["SSFDAYS"].ToString().Trim() == DaysTextBox.Text))
                        {
                            if ((((start >= startTime && start < endTime) || (end > startTime && end <= endTime)) || (start < startTime && end > endTime)) && thisReader2["SSFROOM"].ToString().Trim() == RoomTextBox.Text)
                            {
                                conflict = true;
                                break;
                            }
                        }

                        else if (DaysTextBox.Text.Contains('F') && (thisReader2["SSFDAYS"].ToString().Trim().Contains('F') || thisReader2["SSFDAYS"].ToString().Trim() == DaysTextBox.Text))
                        {
                            if ((((start >= startTime && start < endTime) || (end > startTime && end <= endTime)) || (start < startTime && end > endTime)) && thisReader2["SSFROOM"].ToString().Trim() == RoomTextBox.Text)
                            {
                                conflict = true;
                                break;
                            }
                        }
                        else if (DaysTextBox.Text.Contains('S') && (thisReader2["SSFDAYS"].ToString().Trim().Contains('S') || thisReader2["SSFDAYS"].ToString().Trim() == DaysTextBox.Text))
                        {
                            if ((((start >= startTime && start < endTime) || (end > startTime && end <= endTime)) || (start < startTime && end > endTime)) && thisReader2["SSFROOM"].ToString().Trim() == RoomTextBox.Text)
                            {
                                conflict = true;
                                break;
                            }
                        }

                        else if (DaysTextBox.Text.Contains("TTH") && (thisReader2["SSFDAYS"].ToString().Trim().Contains("TTH") || thisReader2["SSFDAYS"].ToString().Trim() == DaysTextBox.Text))
                        {
                            if (((start >= startTime && start < endTime) || (end > startTime && end <= endTime)) && thisReader2["SSFROOM"].ToString().Trim() == RoomTextBox.Text)
                            {
                                conflict = true;
                                break;
                            }
                        }

                        else if (DaysTextBox.Text.Contains("MWF") && (thisReader2["SSFDAYS"].ToString().Trim().Contains("MWF") || thisReader2["SSFDAYS"].ToString().Trim() == DaysTextBox.Text))
                        {
                            if (((start >= startTime && start < endTime) || (end > startTime && end <= endTime)) && thisReader2["SSFROOM"].ToString().Trim() == RoomTextBox.Text)
                            {
                                conflict = true;
                                break;
                            }
                        }
                    }*/

                    if (conflict == false)
                    {
                        string connectionString = @"Provider=Microsoft.ACE.OLEDB.12.0;Data Source=C:\Users\kyle\Desktop\APPSDEV FINAL\Enrollment System\DATABASE FINAL.accdb";
                        OleDbConnection thisConnection = new OleDbConnection(connectionString);
                        thisConnection.Open();

                        string commandText = "SELECT * FROM SUBJECTSCHEDFILE";
                        OleDbDataAdapter thisAdapter = new OleDbDataAdapter(commandText, thisConnection);
                        OleDbCommandBuilder thisCommandBuilder = new OleDbCommandBuilder(thisAdapter);
                        DataSet thisDataSet = new DataSet();
                        thisAdapter.Fill(thisDataSet, "SUBJECTSCHEDFILE");

                        //setup primary key
                        DataColumn[] keys = new DataColumn[2];// DataColumn array is named keys
                                                              // assgin the first element of the keys arrray, keys[0] to the ProductID column in Product tabel.
                        keys[0] = thisDataSet.Tables["SUBJECTSCHEDFILE"].Columns["SFEDPCODE"];

                        // assign the array keys to the PrimaryKey property of the OrderDetails DataTable object.
                        thisDataSet.Tables["SUBJECTSCHEDFILE"].PrimaryKey = keys;

                        //values to be searched
                        String[] valuesToSearch = new string[1];
                        valuesToSearch[0] = EDPCodeTextBox.Text;

                        DataRow findRow = thisDataSet.Tables["SUBJECTSCHEDFILE"].Rows.Find(valuesToSearch);

                        DataRow thisRow = thisDataSet.Tables["SUBJECTSCHEDFILE"].NewRow();
                        thisRow["SFEDPCODE"] = EDPCodeTextBox.Text;
                        thisRow["SFSUBJCODE"] = SubjectCodeTextBox.Text;
                        thisRow["SFSTARTTIME"] = StartTimePicker.Value.ToString("hh:mm tt");
                        thisRow["SFENDTIME"] = EndTimePicker.Value.ToString("hh:mm tt");
                        thisRow["SFDAYS"] = DaysTextBox.Text;
                        thisRow["SFROOM"] = RoomTextBox.Text;
                        thisRow["SFSECTION"] = SectionTextBox.Text;
                        thisRow["SFSCHOOLYEAR"] = Convert.ToInt32(SchoolYearTextBox.Text);


                        thisDataSet.Tables["SUBJECTSCHEDFILE"].Rows.Add(thisRow);
                        thisAdapter.Update(thisDataSet, "SUBJECTSCHEDFILE");
                        MessageBox.Show("Schedule Recorded!", "Subject Schedule");


                    }
                    else
                    {
                        MessageBox.Show("Schedule Conflict!", "Error");
                    }
                }
                catch (Exception ex)
                {
                    MessageBox.Show(ex.Message, "Error");
                }
            }
            else
            {
                MessageBox.Show("Search Subject First", "Information Message");
            }
        }

        private void NextButton1_Click(object sender, EventArgs e)
        {
            StudentEnrollmentEntry studentEnrollmentEntry = new StudentEnrollmentEntry();
            studentEnrollmentEntry.Show();
            this.Hide();
        }

        private void ClearButton_Click(object sender, EventArgs e)
        {
            EDPCodeTextBox.Text = "";
            SubjectCodeTextBox.Text = "";
            DaysTextBox.Text = "";
            SectionTextBox.Text = "";
            RoomTextBox.Text = "";
            SchoolYearTextBox.Text = "";
        }

        private void BackButton_Click(object sender, EventArgs e)
        {
            Form1 form1 = new Form1();
            form1.Show();
            this.Hide();
        }

        private void SubjectCodeTextBox_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (e.KeyChar == (char)Keys.Enter)
            {
                try
                {



                    OleDbConnection thisConnection = new OleDbConnection(connectionString);
                    thisConnection.Open();

                    string commandText = "SELECT * FROM SUBJECTFILE";
                    OleDbCommand thisCommand = thisConnection.CreateCommand();
                    thisCommand.CommandText = commandText;

                    OleDbDataReader thisReader = thisCommand.ExecuteReader();
                    while (thisReader.Read())
                    {
                        if (thisReader["SFSUBJCODE"].ToString().ToLower() == SubjectCodeTextBox.Text.ToLower())
                        {
                            DescriptionLabel.Text = thisReader["SFSUBJDESC"].ToString();
                            subjectSearch = true;
                            break;
                        }
                        else
                        {
                            continue;
                        }
                    }
                }
                catch (Exception ex)
                {
                    MessageBox.Show(ex.Message, "Information Message");
                }
            }
        }
    }
}
