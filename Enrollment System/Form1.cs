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
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }

        string connectionString = @"Provider=Microsoft.ACE.OLEDB.12.0;Data Source=C:\Users\kyle\Desktop\APPSDEV FINAL\Enrollment System\DATABASE FINAL.accdb";
      
        private void Form1_Load(object sender, EventArgs e)
        {

        }

        private void NextButton_Click(object sender, EventArgs e)
        {
            SubjectScheduleEntry subjectScheduleEntry = new SubjectScheduleEntry();
            subjectScheduleEntry.Show();
            this.Hide();
        }

        private void SaveButton_Click(object sender, EventArgs e)
        {
            //***************** FOR THE SUBJECTFILE ******************************************

            OleDbConnection thisConnection = new OleDbConnection(connectionString);
            string sql = "SELECT * FROM SUBJECTFILE";
            OleDbDataAdapter thisAdapter = new OleDbDataAdapter(sql, thisConnection);
            OleDbCommandBuilder thisBuilder = new OleDbCommandBuilder(thisAdapter);

            DataSet thisDataSet = new DataSet();

            // Primary key setup by initializing the PrimaryKey property of the DataTable implicitly, simply setup keys object for defining primary key.
            thisAdapter.MissingSchemaAction = MissingSchemaAction.AddWithKey;
            thisAdapter.Fill(thisDataSet, "SubjectFile"); // Fill DataSet using the query defined for DataAdapter

            DataRow findRow = thisDataSet.Tables["SubjectFile"].Rows.Find(SubjectCodeTextBox.Text);
            if (findRow == null)
            {
                DataRow thisRow = thisDataSet.Tables["SubjectFile"].NewRow();
                thisRow["SFSUBJCODE"] = SubjectCodeTextBox.Text;
                thisRow["SFSUBJDESC"] = DescriptionTextBox.Text;
                thisRow["SFSUBJUNITS"] = Convert.ToInt16(UnitsTextBox.Text);
                thisRow["SFSUBJCATEGORY"] = CategoryComboBox.Text.Substring(0, 3);
                thisRow["SFSUBJREGOFRNG"] = Convert.ToUInt16(OfferingComboBox.Text.Substring(0, 1));


                thisDataSet.Tables["SubjectFile"].Rows.Add(thisRow);
                thisAdapter.Update(thisDataSet, "SubjectFile");

                //*****************   TODO: Save DATA TO SUBJPREQFILE **********************************
                OleDbConnection requisiteConnection = new OleDbConnection(connectionString);
                string requisite = "SELECT * FROM SUBJECPREQFILE";
                OleDbDataAdapter requisiteAdapter = new OleDbDataAdapter(requisite, requisiteConnection);
                OleDbCommandBuilder requisiteBuilder = new OleDbCommandBuilder(requisiteAdapter);


                thisAdapter.Fill(thisDataSet, "SUBJECTPREQFILE");
                //setup primary key
                DataColumn[] keys = new DataColumn[2];// DataColumn array is named keys
                                                      //assign the first element of the keys array, keys[0] to the ProductID column in Product table. 
                keys[0] = thisDataSet.Tables["SUBJECTPREQFILE"].Columns["SUBJCODE"];
                keys[1] = thisDataSet.Tables["SUBJECTPREQFILE"].Columns["SUBJPRECODE"];

                // assign the array keys to the PrimaryKey property of the OrderDetails DataTable object.
                thisDataSet.Tables["SUBJECTPREQFILE"].PrimaryKey = keys;

                // values to be searched
                String[] valuesToSearch = new String[2];
                valuesToSearch[0] = SubjectCodeTextBox.Text;
                valuesToSearch[1] = RequisiteTextBox.Text;

                /*DataRow findRequisiteRow = thisDataSet.Tables["SUBJECTPREQFILE"].Rows.Find(valuesToSearch);
                if (findRequisiteRow == null)
                {

                    DataRow thisRequisiteRow = thisDataSet.Tables["SUBJECTPREQFILE"].NewRow();

                    thisRequisiteRow["SUBJCODE"] = SubjectCodeTextBox.Text;

                    //add more codes!!!!

                }
                MessageBox.Show("Entries Recorded");*/
            }
            else
                MessageBox.Show("Duplicate Entries");
        }

        private void ExitButton_Click(object sender, EventArgs e)
        {
            Application.Exit();
        }

        private void RequisiteTextBox_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (e.KeyChar == (char)Keys.Enter)
            {
                OleDbConnection thisConnection = new OleDbConnection(connectionString);
                thisConnection.Open();
                OleDbCommand thisCommand = thisConnection.CreateCommand();

                string sql = "SELECT * FROM SUBJECTFILE";
                thisCommand.CommandText = sql;

                OleDbDataReader thisDataReader = thisCommand.ExecuteReader();

                bool found = false;
                string subjectCode = "";
                string description = "";
                string units = "";

                while (thisDataReader.Read())
                {
                    // MessageBox.Show(thisDataReader["SFSUBJCODE"].ToString());
                    if (thisDataReader["SFSUBJCODE"].ToString().Trim().ToUpper() == RequisiteTextBox.Text.Trim().ToUpper())
                    {
                        found = true;
                        subjectCode = thisDataReader["SFSUBJCODE"].ToString();
                        description = thisDataReader["SFSUBJDESC"].ToString();
                        units = thisDataReader["SFSUBJUNITS"].ToString();
                        break;
                        //
                    }

                }

                int index;
                if (found == false)
                    MessageBox.Show("Subject Code Not Found");
                else
                {
                    index = SubjectDataGridView.Rows.Add();
                    SubjectDataGridView.Rows[index].Cells["SubjectCodeColumn"].Value = subjectCode;
                    SubjectDataGridView.Rows[index].Cells["DescriptionColumn"].Value = description;
                    SubjectDataGridView.Rows[index].Cells["UnitsColumn"].Value = units;
                }

            }
        }
    }
}
