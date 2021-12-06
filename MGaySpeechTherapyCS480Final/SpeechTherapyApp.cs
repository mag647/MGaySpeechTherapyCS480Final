using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Data.SqlClient;
using System.Drawing;
using System.Linq;
using System.Net;
using System.Net.Mail;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
//possibly add in a goal table, possibly add a goal and disorder table, add info into eval table!

namespace MGaySpeechTherapyCS480Final
{
    public partial class SpeechTherapyApp : Form
    {
        /*SqlConnection sConn = new SqlConnection();
        SqlDataAdapter mdaTick;
        DataTable TickData = new DataTable();
        //SqlConnectionStringBuilder bu = new SqlConnectionStringBuilder(); //notneeded
        DataTable PersonData = new DataTable();
        DataSet PersonSet = new DataSet();
        SqlDataAdapter mdaPerson;*/
        public SpeechTherapyApp()
        {
            InitializeComponent();
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            /***SQL connection settings***/
            /*bu.DataSource = @"LAPTOP-DJFHSMT5\SQLEXPRESS"; //notneeded
            bu.InitialCatalog = "MGaySLPDatabase.mdf"; //not needed
            bu.IntegratedSecurity = false;//notneeded
            bu.UserInstance = false;//notneeded
            bu.UserID = "Ext_Prg";//notneeded
            bu.Password = "passwerd";*/ //not needed

            /*code next few lines needed
            sConn.ConnectionString = @"Data Source=LAPTOP-DJFHSMT5\SQLEXPRESS;Initial Catalog=MGaySLPDatabase;Integrated Security=True";// bu.ConnectionString;
            sConn.Open(); still needed*/
            /***Fill View Client gridview***/
            /*code needed
            mdaTick = new SqlDataAdapter("SELECT ClientFirstName, ClientLastName FROM ClientInfo", sConn);
            SqlCommandBuilder m_cbCommandBuilder = new SqlCommandBuilder(mdaTick);
            mdaTick.Fill(TickData);
            dataGridView1.DataSource = TickData;
            dataGridView1.Columns[0].Name = "First Name";
            dataGridView1.Columns[1].Name = "Last Name"; */

            /* makes button column
            DataGridViewButtonColumn col = new DataGridViewButtonColumn();
            col.UseColumnTextForButtonValue = true;
            col.Text = "Oops!";
            col.Name = "Redo";
            dataGridView1.Columns.Add(col);
            */
            //needed: sConn.Close();
            //attempt to fill combobox
            //https://stackoverflow.com/questions/12900062/c-sharp-fill-combo-box-from-sql-datatable

            //Test Comment
            fillClientNames();
            fillDiscipline();

        }
        private void fillClientNames()
        {
            addProgClientNameComboBox.Items.Clear();
            comboBox2.Items.Clear();
            SqlConnection sConn = new SqlConnection();
            sConn.ConnectionString = @"Data Source=LAPTOP-DJFHSMT5\SQLEXPRESS;Initial Catalog=MGaySLPDatabase;Integrated Security=True";// bu.ConnectionString;

            SqlCommand sqlCmd = new SqlCommand("SELECT ClientName FROM ClientInfo ORDER BY ClientName", sConn);
            sConn.Open();
            SqlDataReader sqlReader = sqlCmd.ExecuteReader();
            while (sqlReader.Read())
            {
                string wholeName = sqlReader["ClientName"].ToString();
                addProgClientNameComboBox.Items.Add(wholeName);
                comboBox2.Items.Add(wholeName);
            }
            sqlReader.Close();
            sConn.Close();
        }

        private void fillClientGoal(object sender, EventArgs e)
        {
            
        }
        private void fillDiscipline()
        {
            SqlConnection sConn = new SqlConnection();
            sConn.ConnectionString = @"Data Source=LAPTOP-DJFHSMT5\SQLEXPRESS;Initial Catalog=MGaySLPDatabase;Integrated Security=True";// bu.ConnectionString;

            SqlCommand sqlCmd = new SqlCommand("SELECT Discipline FROM ReferralProviders", sConn);
            sConn.Open();
            SqlDataReader sqlReader = sqlCmd.ExecuteReader();
            while (sqlReader.Read())
            {
                string discipline = sqlReader["Discipline"].ToString();
                discComboBox.Items.Add(discipline);
            }
            sqlReader.Close();
            sConn.Close();
        }
        private void fillProvider()
        {
            //SELECT ProviderName WHERE Discipline = discComboBox.SelectedItem() FROM ReferralProviders;
            SqlConnection sConn = new SqlConnection();
            sConn.ConnectionString = @"Data Source=LAPTOP-DJFHSMT5\SQLEXPRESS;Initial Catalog=MGaySLPDatabase;Integrated Security=True";// bu.ConnectionString;

            SqlCommand sqlCmd = new SqlCommand("SELECT Provider FROM ReferralProviders WHERE Discipline=@Disc", sConn);
            SqlParameter param = new SqlParameter();
            param.ParameterName = "@Disc";
            param.Value = discComboBox.Text;
            sqlCmd.Parameters.Add(param);
            sConn.Open();
            SqlDataReader sqlReader = sqlCmd.ExecuteReader();
            while (sqlReader.Read())
            {
                string provider = sqlReader["Provider"].ToString();
                discComboBox.Items.Add(provider);
            }
            sqlReader.Close();
            sConn.Close();
        }

        private void sample_query_with_parameters()
        {
            
            //sample query
            /*PersonData.Clear();
            PersonSet.Clear();
            mdaPerson = new SqlDataAdapter("SELECT Reviewed_By, Tested_By, Assigned_To, count, Status FROM Test_Status WHERE [Test_Request] = @TR AND [Test_Name] = @Test", sConn);
            //mdaPerson.SelectCommand.Parameters.AddWithValue("@TR", dataGridView1.Rows[e.RowIndex].Cells[0].Value.ToString());
            //mdaPerson.SelectCommand.Parameters.AddWithValue("@Test", dataGridView1.Rows[e.RowIndex].Cells[2].Value.ToString());
            SqlCommandBuilder m_cbCommandBuilder1 = new SqlCommandBuilder(mdaPerson);
            mdaPerson.Fill(PersonData);
            mdaPerson.Fill(PersonSet, "Test_Status");
            //string assignee = dataGridView1.Rows[e.RowIndex].Cells[3].Value.ToString();
            string reviewer = PersonData.Rows[0][0].ToString();
            string tester = PersonData.Rows[0][1].ToString();
            PersonSet.Tables["Test_Status"].Rows[0]["Assigned_To"] = reviewer;
            mdaPerson.Update(PersonSet, "Test_Status");*/
        }

        private void dataGridView1_CellDoubleClick(object sender, DataGridViewCellEventArgs e)
        {

        }

        private void ViewNamesButton_Click(object sender, EventArgs e)
        {
            SqlConnection sConnClientName = new SqlConnection();
            SqlDataAdapter daClientName;
            DataTable ClientNameDataTable = new DataTable(); 
            //SqlConnectionStringBuilder bu = new SqlConnectionStringBuilder(); //notneeded

            //DataTable PersonData = new DataTable();
            //DataSet PersonSet = new DataSet();
            //SqlDataAdapter mdaPerson;

            sConnClientName.ConnectionString = @"Data Source=LAPTOP-DJFHSMT5\SQLEXPRESS;Initial Catalog=MGaySLPDatabase;Integrated Security=True";// bu.ConnectionString;
            sConnClientName.Open();
            /***Fill View Client Names gridview***/
            daClientName = new SqlDataAdapter("SELECT ClientName FROM ClientInfo", sConnClientName);
            SqlCommandBuilder m_cbCommandBuilder = new SqlCommandBuilder(daClientName);
            daClientName.Fill(ClientNameDataTable);
            dataGridView1.DataSource = ClientNameDataTable;
            dataGridView1.Columns[0].HeaderCell.Value = "Name";
            dataGridView1.AutoResizeColumns();
            sConnClientName.Close();
        }

        private void ViewClientDataButton_Click(object sender, EventArgs e)
        {
            SqlConnection sConnClientData = new SqlConnection();
            SqlDataAdapter daClientData;
            DataTable ClientDataTable = new DataTable();
           
            sConnClientData.ConnectionString = @"Data Source=LAPTOP-DJFHSMT5\SQLEXPRESS;Initial Catalog=MGaySLPDatabase;Integrated Security=True";// bu.ConnectionString;
            sConnClientData.Open();
            /***Fill View Client Data gridview***/
            daClientData = new SqlDataAdapter("SELECT ClientName, ClientAddress, ClientPhone, ClientBirthDate, ClientDiagnosis FROM ClientInfo", sConnClientData);
            SqlCommandBuilder m_cbCommandBuilder = new SqlCommandBuilder(daClientData);
            daClientData.Fill(ClientDataTable);
            dataGridView1.DataSource = ClientDataTable;
            dataGridView1.Columns[0].HeaderCell.Value = "Name";
            dataGridView1.Columns[1].HeaderCell.Value = "Address";
            dataGridView1.Columns[2].HeaderCell.Value = "Phone Number";
            dataGridView1.Columns[3].HeaderCell.Value = "Birth Date";
            dataGridView1.Columns[4].HeaderCell.Value = "Primary Diagnosis";
            dataGridView1.AutoResizeColumns();

            sConnClientData.Close();
        }

        private void addClientButton_Click(object sender, EventArgs e)
        {
            //https://www.c-sharpcorner.com/uploadfile/mahesh/commandbuilder-in-ado-net/

            SqlConnection sConnClientData = new SqlConnection();
            SqlDataAdapter daClientData;
            //DataTable ClientDataTable = new DataTable();

            sConnClientData.ConnectionString = @"Data Source=LAPTOP-DJFHSMT5\SQLEXPRESS;Initial Catalog=MGaySLPDatabase;Integrated Security=True";// bu.ConnectionString;
            sConnClientData.Open();
            /***Fill View Client Data gridview***/
            daClientData = new SqlDataAdapter("SELECT ClientName, ClientAddress, ClientPhone, ClientBirthDate, ClientDiagnosis FROM ClientInfo ORDER by ClientID", sConnClientData);
            SqlCommandBuilder m_cbCommandBuilder = new SqlCommandBuilder(daClientData);

            DataSet ds = new DataSet("ClientSet");
            daClientData.Fill(ds, "ClientInfo");
            DataTable ClientDataTable = ds.Tables["ClientInfo"];
            DataRow row = ClientDataTable.NewRow();
            row["ClientName"] = nameTextBox.Text;
            row["ClientAddress"] = addressTextBox.Text;
            row["ClientPhone"] = phoneTextBox.Text;
            row["ClientBirthDate"] = dobTextBox.Text;
            row["ClientDiagnosis"] = diagnosisTextBox.Text;
            ClientDataTable.Rows.Add(row);

            daClientData.Update(ds, "ClientInfo");
            MessageBox.Show(row["ClientName"].ToString().Trim() + " Added to Clients!");

            sConnClientData.Close();

            fillClientNames();
        }

        private void clearClientButton_Click(object sender, EventArgs e)
        {
            nameTextBox.Text = "";
            addressTextBox.Text = "";
            phoneTextBox.Text = "";
            dobTextBox.Text = "";
            diagnosisTextBox.Text = "";
        }

        private void viewProgButton_Click(object sender, EventArgs e)
        {
            SqlConnection sConnProgressData = new SqlConnection();
            SqlDataAdapter daProgressData;
            DataTable ProgressDataTable = new DataTable();

            sConnProgressData.ConnectionString = @"Data Source=LAPTOP-DJFHSMT5\SQLEXPRESS;Initial Catalog=MGaySLPDatabase;Integrated Security=True";// bu.ConnectionString;
            sConnProgressData.Open();

            daProgressData = new SqlDataAdapter("SELECT ClientName, Date, Goal, ProgressData FROM ClientProgress WHERE ClientName = @Name", sConnProgressData);
            daProgressData.SelectCommand.Parameters.Add("@Name", SqlDbType.NVarChar);
            daProgressData.SelectCommand.Parameters["@Name"].Value = vprogNameTextBox.Text;
            SqlCommandBuilder m_cbCommandBuilder = new SqlCommandBuilder(daProgressData);
            daProgressData.Fill(ProgressDataTable);
            dataGridViewProgress.DataSource = ProgressDataTable;
            dataGridViewProgress.Columns[0].HeaderCell.Value = "Client";
            dataGridViewProgress.Columns[1].HeaderCell.Value = "Date";
            dataGridViewProgress.Columns[2].HeaderCell.Value = "Goal";
            dataGridViewProgress.Columns[3].HeaderCell.Value = "Progress Data";
            dataGridViewProgress.AutoResizeColumns();
            sConnProgressData.Close();
        }

        private void addProgressButton_Click(object sender, EventArgs e)
        {
            
            //https://www.c-sharpcorner.com/uploadfile/mahesh/commandbuilder-in-ado-net/

            SqlConnection sConnClientData = new SqlConnection();
            SqlDataAdapter daClientData;
            //DataTable ClientDataTable = new DataTable();

            sConnClientData.ConnectionString = @"Data Source=LAPTOP-DJFHSMT5\SQLEXPRESS;Initial Catalog=MGaySLPDatabase;Integrated Security=True";// bu.ConnectionString;
            sConnClientData.Open();
            ///comment
            daClientData = new SqlDataAdapter("SELECT Goal, Date, ProgressData, ClientName FROM ClientGoalProgress ORDER by ClientName", sConnClientData);
            SqlCommandBuilder m_cbCommandBuilder = new SqlCommandBuilder(daClientData);
            
            DataSet ds = new DataSet("ClientGoalSet");
            daClientData.Fill(ds, "ClientGoalProgress");
            DataTable ClientDataTable = ds.Tables["ClientGoalProgress"];
            DataRow row = ClientDataTable.NewRow();
            row["ClientName"] = addProgClientNameComboBox.Text;
            row["Goal"] = comboBox1.Text;
            row["Date"] = addProgDateTextBox.Text;
            row["ProgressData"] = addProgMultiTextBox.Text;
            ClientDataTable.Rows.Add(row);

            daClientData.Update(ds, "ClientGoalProgress");
            MessageBox.Show(row["ClientName"].ToString().Trim() + "'s progress added!");

            sConnClientData.Close();
        }

        private void viewEvalButton_Click(object sender, EventArgs e)
        {
            SqlConnection sConnEvalData = new SqlConnection();
            SqlDataAdapter daEvalData;
            DataTable EvalDataTable = new DataTable();

            sConnEvalData.ConnectionString = @"Data Source=LAPTOP-DJFHSMT5\SQLEXPRESS;Initial Catalog=MGaySLPDatabase;Integrated Security=True";// bu.ConnectionString;
            sConnEvalData.Open();

            daEvalData = new SqlDataAdapter("SELECT ClientName, TestName, StandardScore, PercentileRank, AdditionalData FROM EvalData WHERE ClientName = @Name", sConnEvalData);
            daEvalData.SelectCommand.Parameters.Add("@Name", SqlDbType.NVarChar);
            daEvalData.SelectCommand.Parameters["@Name"].Value = viewEvalNameTextBox.Text;
            SqlCommandBuilder m_cbCommandBuilder = new SqlCommandBuilder(daEvalData);
            daEvalData.Fill(EvalDataTable);
            dataGridViewEval.DataSource = EvalDataTable;
            dataGridViewEval.Columns[0].HeaderCell.Value = "Client";
            dataGridViewEval.Columns[1].HeaderCell.Value = "Test Name";
            dataGridViewEval.Columns[2].HeaderCell.Value = "Standard Score";
            dataGridViewEval.Columns[3].HeaderCell.Value = "Percentile Rank";
            dataGridViewEval.Columns[4].HeaderCell.Value = "Additional Data";
            dataGridViewEval.AutoResizeColumns();
            sConnEvalData.Close();
        }

        private void addProgClientNameComboBox_SelectedValueChanged(object sender, EventArgs e)
        {
            SqlConnection sConn = new SqlConnection();
            sConn.ConnectionString = @"Data Source=LAPTOP-DJFHSMT5\SQLEXPRESS;Initial Catalog=MGaySLPDatabase;Integrated Security=True";// bu.ConnectionString;

            SqlCommand sqlCmd = new SqlCommand("SELECT Goal FROM ClientProgress WHERE ClientName=@Name", sConn);
            sqlCmd.Parameters.Add("@Name", SqlDbType.NVarChar);
            sqlCmd.Parameters["@Name"].Value = addProgClientNameComboBox.Text; 
            sConn.Open();
            comboBox1.Items.Clear();
            SqlDataReader sqlReader = sqlCmd.ExecuteReader();
            while (sqlReader.Read())
            {
                comboBox1.Items.Add(sqlReader["Goal"].ToString());
            }
            sqlReader.Close();
            sConn.Close();
        }

        private void addEvalButton_Click(object sender, EventArgs e)
        {
            //https://www.c-sharpcorner.com/uploadfile/mahesh/commandbuilder-in-ado-net/

            SqlConnection sConnClientData = new SqlConnection();
            SqlDataAdapter daClientData;
            //DataTable ClientDataTable = new DataTable();

            sConnClientData.ConnectionString = @"Data Source=LAPTOP-DJFHSMT5\SQLEXPRESS;Initial Catalog=MGaySLPDatabase;Integrated Security=True";// bu.ConnectionString;
            sConnClientData.Open();
            /***Fill View Client Data gridview***/
            daClientData = new SqlDataAdapter("SELECT ClientName, TestName, StandardScore, PercentileRank, AdditionalData FROM EvalData ORDER by EvalID", sConnClientData);
            SqlCommandBuilder m_cbCommandBuilder = new SqlCommandBuilder(daClientData);

            DataSet ds = new DataSet("EvalSet");
            daClientData.Fill(ds, "EvalData");//
            DataTable ClientDataTable = ds.Tables["EvalData"];
            DataRow row = ClientDataTable.NewRow();
            row["ClientName"] = comboBox2.Text;
            row["TestName"] = textBox3.Text;
            row["StandardScore"] = textBox2.Text;
            row["PercentileRank"] = textBox1.Text;
            row["AdditionalData"] = textBox4.Text;
            ClientDataTable.Rows.Add(row);

            daClientData.Update(ds, "EvalData");
            MessageBox.Show(row["ClientName"].ToString().Trim() + " Added to Eval Data!");

            sConnClientData.Close();
        }

        private bool send_email(string to, string from, string subject, string body)
        {
            MailMessage message = new MailMessage(from, to, subject, body);
            SmtpClient client = new SmtpClient("smtp.gmail.com");
            client.Port = 587;
            client.EnableSsl = true;
            client.Credentials = new NetworkCredential("michele.a.gay@gmail.com", "bymrbsfpfjqercmy");
            //Attachment attachment = new Attachment("", MediaTypeNames.Application.Octet);
            //attachment.Name = "test.pdf";
            //message.Attachments.Add(attachment);
            try
            {
                /***Send Email***/
                client.Send(message);
                return true;
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
                return false;
            }
        }

        private void referralButton_Click(object sender, EventArgs e)
        {
            SqlConnection sConn = new SqlConnection();
            sConn.ConnectionString = @"Data Source=LAPTOP-DJFHSMT5\SQLEXPRESS;Initial Catalog=MGaySLPDatabase;Integrated Security=True";// bu.ConnectionString;

            SqlCommand sqlCmd = new SqlCommand("SELECT Email FROM ReferralProviders WHERE ProviderName=@Name", sConn);
            sqlCmd.Parameters.Add("@Name", SqlDbType.NVarChar);
            sqlCmd.Parameters["@Name"].Value = providerComboBox.Text;
            sConn.Open();
           
            SqlDataReader sqlReader = sqlCmd.ExecuteReader();
            sqlReader.Read();
            string providerEmail = sqlReader["Email"].ToString();
          
            sqlReader.Close();
            sConn.Close();

            send_email(providerEmail, "michele.a.gay@gmail.com", "New Referral from Speech", referralMultiTextBox.Text);
            MessageBox.Show(providerEmail);
        }

        private void discComboBox_SelectedIndexChanged(object sender, EventArgs e)
        {
            SqlConnection sConn = new SqlConnection();
            sConn.ConnectionString = @"Data Source=LAPTOP-DJFHSMT5\SQLEXPRESS;Initial Catalog=MGaySLPDatabase;Integrated Security=True";// bu.ConnectionString;

            SqlCommand sqlCmd = new SqlCommand("SELECT ProviderName FROM ReferralProviders WHERE Discipline=@Discipline", sConn);
            sqlCmd.Parameters.Add("@Discipline", SqlDbType.NVarChar);
            sqlCmd.Parameters["@Discipline"].Value = discComboBox.Text;
            sConn.Open();
            providerComboBox.Items.Clear();
            SqlDataReader sqlReader = sqlCmd.ExecuteReader();
            while (sqlReader.Read())
            {
                providerComboBox.Items.Add(sqlReader["ProviderName"].ToString());
            }
            sqlReader.Close();
            sConn.Close();
        }
    }
}
