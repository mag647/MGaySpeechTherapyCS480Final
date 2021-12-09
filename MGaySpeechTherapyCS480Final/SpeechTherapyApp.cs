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
        
        public SpeechTherapyApp()
        {
            InitializeComponent();
        }

        private void Form1_Load(object sender, EventArgs e)
        {
           
            //attempt to fill combobox
            //https://stackoverflow.com/questions/12900062/c-sharp-fill-combo-box-from-sql-datatable

            //Test Comment
            fillClientNames();
            fillDiscipline();

        }
        private void fillClientNames()
        {
            try
            {
                addProgClientNameComboBox.Items.Clear();
                comboBox2.Items.Clear();
                comboBox3.Items.Clear();
                comboBox4.Items.Clear();
                comboBox5.Items.Clear();
                comboBox6.Items.Clear();
                comboBox7.Items.Clear();
                comboBox8.Items.Clear();
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
                    comboBox3.Items.Add(wholeName);
                    comboBox4.Items.Add(wholeName);
                    comboBox5.Items.Add(wholeName);
                    comboBox6.Items.Add(wholeName);
                    comboBox7.Items.Add(wholeName);
                    comboBox8.Items.Add(wholeName);
                }
                sqlReader.Close();
                sConn.Close();
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error with updating client name boxes! Please try again.", ex.Message);
            }
        }

        private void fillClientGoal(object sender, EventArgs e)
        {
            
        }
        private void fillDiscipline()
        {
            try
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
            catch(Exception ex)
            {
                MessageBox.Show("Error with filling discipline box! Please try again.", ex.Message);
            }
        }
        private void fillProvider()
        {
            //SELECT ProviderName WHERE Discipline = discComboBox.SelectedItem() FROM ReferralProviders;
            try
            {
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
            catch (Exception ex)
            {
                MessageBox.Show("Error with filling provider box! Please try again.", ex.Message);
            }
        }
        private void dataGridView1_CellDoubleClick(object sender, DataGridViewCellEventArgs e)
        {
            //remove this!
        }

        private void ViewNamesButton_Click(object sender, EventArgs e)
        {
            try
            {
                SqlConnection sConnClientName = new SqlConnection();
                SqlDataAdapter daClientName;
                DataTable ClientNameDataTable = new DataTable();
                
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
            catch (Exception ex)
            {
                MessageBox.Show("Error with viewing client names! Please try again.", ex.Message);
            }
        }

        private void ViewClientDataButton_Click(object sender, EventArgs e)
        {
            try
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
            catch (Exception ex)
            {
                MessageBox.Show("Error with viewing client data! Please try again.", ex.Message);
            }
        }

        private void addClientButton_Click(object sender, EventArgs e)
        {
            //snippet of code to add row modified from: https://www.c-sharpcorner.com/uploadfile/mahesh/commandbuilder-in-ado-net/

            if (nameTextBox.Text == "" || addressTextBox.Text == "" || maskedTextBox1.Text == ""  || diagnosisTextBox.Text == "")
            {
                MessageBox.Show("One or more fields are blank. Please fill in missing information!");
            }
            if (nameTextBox.Text.Length > 50 || addressTextBox.Text.Length > 100 || diagnosisTextBox.Text.Length > 100)
            {
                MessageBox.Show("One or more fields have an incorrect length. Please correct data!");
            }
            else
            {
                try
                {
                    SqlConnection sConnClientData = new SqlConnection();
                    SqlDataAdapter daClientData;

                    sConnClientData.ConnectionString = @"Data Source=LAPTOP-DJFHSMT5\SQLEXPRESS;Initial Catalog=MGaySLPDatabase;Integrated Security=True";
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
                    row["ClientPhone"] = maskedTextBox1.Text;
                    row["ClientBirthDate"] = dateTimePicker1.Text;
                    row["ClientDiagnosis"] = diagnosisTextBox.Text;
                    ClientDataTable.Rows.Add(row);

                    daClientData.Update(ds, "ClientInfo");
                    MessageBox.Show(row["ClientName"].ToString().Trim() + " Added to Clients!");

                    sConnClientData.Close();

                    fillClientNames();

                    nameTextBox.Text = "";
                    addressTextBox.Text = "";
                    maskedTextBox1.Text = "";
                    dateTimePicker1.Value = DateTime.Today;
                    diagnosisTextBox.Text = "";
                }
                catch(Exception ex)
                {
                    MessageBox.Show("Error! Please try again.", ex.Message);
                }
            }
        }

        private void clearClientButton_Click(object sender, EventArgs e)
        {
            nameTextBox.Text = "";
            addressTextBox.Text = "";
            dateTimePicker1.Value = DateTime.Today;
            maskedTextBox1.Text = "";
            diagnosisTextBox.Text = "";
        }

        private void viewProgButton_Click(object sender, EventArgs e)
        {

            if (comboBox3.Text == "")
            {
                MessageBox.Show("Please chose a name to view client progress!");
            }
            else
            {
                try
                {
                    SqlConnection sConnProgressData = new SqlConnection();
                    SqlDataAdapter daProgressData;
                    DataTable ProgressDataTable = new DataTable();

                    sConnProgressData.ConnectionString = @"Data Source=LAPTOP-DJFHSMT5\SQLEXPRESS;Initial Catalog=MGaySLPDatabase;Integrated Security=True";// bu.ConnectionString;
                    sConnProgressData.Open();

                    daProgressData = new SqlDataAdapter("SELECT ClientName, Date, Goal, ProgressData FROM ClientProgress WHERE ClientName = @Name", sConnProgressData);
                    daProgressData.SelectCommand.Parameters.Add("@Name", SqlDbType.NVarChar);
                    daProgressData.SelectCommand.Parameters["@Name"].Value = comboBox3.Text;
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
                catch(Exception ex)
                {
                    MessageBox.Show("Error! Please try again.", ex.Message);
                }
            }
        }

        private void addProgressButton_Click(object sender, EventArgs e)
        {

            //https://www.c-sharpcorner.com/uploadfile/mahesh/commandbuilder-in-ado-net/

            if (addProgClientNameComboBox.Text == "" || comboBox1.Text == "" || dateTimePicker2.Text == "" || addProgMultiTextBox.Text == "")
            {
                MessageBox.Show("One or more fields are blank. Please fill in missing data!");
            }
            else
            {
                try
                {
                    SqlConnection sConnClientData = new SqlConnection();
                    SqlDataAdapter daClientData;

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
                    row["Date"] = dateTimePicker2.Text;
                    row["ProgressData"] = addProgMultiTextBox.Text;
                    ClientDataTable.Rows.Add(row);

                    daClientData.Update(ds, "ClientGoalProgress");
                    MessageBox.Show(row["ClientName"].ToString().Trim() + "'s progress added!");

                    sConnClientData.Close();

                    addProgClientNameComboBox.Text = "";
                    comboBox1.Text = "";
                    dateTimePicker2.Value = DateTime.Today;
                    addProgMultiTextBox.Text = "";
                }
                catch(Exception ex)
                {
                    MessageBox.Show("Error with adding progress report!", ex.Message);
                }
                  
            }
        }

        private void viewEvalButton_Click(object sender, EventArgs e)
        {

            if (comboBox5.Text == "")
            {
                MessageBox.Show("Please chose a name to view client eval!");
            }
            else
            {
                SqlConnection sConnEvalData = new SqlConnection();
                SqlDataAdapter daEvalData;
                DataTable EvalDataTable = new DataTable();

                sConnEvalData.ConnectionString = @"Data Source=LAPTOP-DJFHSMT5\SQLEXPRESS;Initial Catalog=MGaySLPDatabase;Integrated Security=True";// bu.ConnectionString;
                sConnEvalData.Open();

                daEvalData = new SqlDataAdapter("SELECT ClientName, TestName, StandardScore, PercentileRank, AdditionalData, EvalDate FROM EvalData WHERE ClientName = @Name", sConnEvalData);
                daEvalData.SelectCommand.Parameters.Add("@Name", SqlDbType.NVarChar);
                daEvalData.SelectCommand.Parameters["@Name"].Value = comboBox5.Text;
                SqlCommandBuilder m_cbCommandBuilder = new SqlCommandBuilder(daEvalData);
                daEvalData.Fill(EvalDataTable);
                dataGridViewEval.DataSource = EvalDataTable;
                dataGridViewEval.Columns[0].HeaderCell.Value = "Client";
                dataGridViewEval.Columns[1].HeaderCell.Value = "Test Name";
                dataGridViewEval.Columns[2].HeaderCell.Value = "Standard Score";
                dataGridViewEval.Columns[3].HeaderCell.Value = "Percentile Rank";
                dataGridViewEval.Columns[4].HeaderCell.Value = "Additional Data";
                dataGridViewEval.Columns[5].HeaderCell.Value = "Eval Date";
                dataGridViewEval.AutoResizeColumns();
                sConnEvalData.Close();
            }
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

            //standard score and percentile rank, add extre error checking here, can I add a date to this table??

            /*
           if (nameTextBox.Text.Length > 50 || addressTextBox.Text.Length > 100 || phoneTextBox.Text.Length > 12 || dobTextBox.Text.Length > 10 || diagnosisTextBox.Text.Length > 100)
           {
               MessageBox.Show("One or more fields have an incorrect length. Please correct data!");
           }*/
        
            if (comboBox2.Text == "" || textBox3.Text == "" ||  textBox4.Text == "")
            {
                MessageBox.Show("One or more fields are blank. Please fill in missing data!");
            }
            else
            {
                try
                {
                    SqlConnection sConnClientData = new SqlConnection();
                    SqlDataAdapter daClientData;
                    
                    sConnClientData.ConnectionString = @"Data Source=LAPTOP-DJFHSMT5\SQLEXPRESS;Initial Catalog=MGaySLPDatabase;Integrated Security=True";// bu.ConnectionString;
                    sConnClientData.Open();
                    /***Fill View Client Data gridview***/
                    daClientData = new SqlDataAdapter("SELECT ClientName, TestName, StandardScore, PercentileRank, AdditionalData, EvalDate FROM EvalData ORDER by EvalID", sConnClientData);
                    SqlCommandBuilder m_cbCommandBuilder = new SqlCommandBuilder(daClientData);

                    DataSet ds = new DataSet("EvalSet");
                    daClientData.Fill(ds, "EvalData");//
                    DataTable ClientDataTable = ds.Tables["EvalData"];
                    DataRow row = ClientDataTable.NewRow();
                    row["ClientName"] = comboBox2.Text;
                    row["TestName"] = textBox3.Text;
                    row["StandardScore"] = numericUpDown1.Text;
                    row["PercentileRank"] = numericUpDown2.Text;
                    row["AdditionalData"] = textBox4.Text;
                    row["EvalDate"] = dateTimePicker3.Text;
                    ClientDataTable.Rows.Add(row);

                    daClientData.Update(ds, "EvalData");
                    MessageBox.Show(row["ClientName"].ToString().Trim() + " Added to Eval Data!");

                    sConnClientData.Close();
                    comboBox2.Text = "";
                    textBox3.Text = "";
                    numericUpDown2.Value = 0;
                    numericUpDown1.Value = 0;
                    textBox4.Text = "";
                    dateTimePicker3.Value = DateTime.Today;
                }
                catch(Exception ex)
                {
                    MessageBox.Show("Error! Please try again.", ex.Message);
                }
            }
        }

        private bool send_email(string to, string from, string subject, string body)
        {
            //do I need to add error checking around this? //add to try block?
                MailMessage message = new MailMessage(from, to, subject, body);
                SmtpClient client = new SmtpClient("smtp.gmail.com");
                client.Port = 587;
                client.EnableSsl = true;
                client.Credentials = new NetworkCredential("michele.a.gay@gmail.com", "bymrbsfpfjqercmy");

            /*For future, if attachment needed to be emailed*/
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
            if (providerComboBox.Text == "" || discComboBox.Text == "" || comboBox6.Text == "" || referralMultiTextBox.Text == "")
            {
                MessageBox.Show("One or more fields are blank. Please fill in missing information!");
            }
            else
            {
                try
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

                    send_email(providerEmail, "michele.a.gay@gmail.com", "New Referral from Speech for " + comboBox6.Text, referralMultiTextBox.Text);
                    MessageBox.Show("Email sent successfully!");
                    referralMultiTextBox.Text = "";
                    providerComboBox.Text = "";
                    discComboBox.Text = "";
                    comboBox6.Text = "";
                    referralMultiTextBox.Text = "";
                }
                catch (Exception ex)
                {
                    MessageBox.Show("Error! Please try again.", ex.Message);
                }
            }
        }

        private void discComboBox_SelectedIndexChanged(object sender, EventArgs e)
        {

            try
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
            catch (Exception ex)
            {
                MessageBox.Show("Error! Please try again.", ex.Message);
            }
        }

        private void view1ClientButton_Click(object sender, EventArgs e)
        {

            if (comboBox4.Text == "")
            {
                MessageBox.Show("Please chose a name to view client data!");
            }
            else
            {
                try
                {
                    SqlConnection sConnDemoData = new SqlConnection();
                    SqlDataAdapter daDemoData;
                    DataTable DemoDataTable = new DataTable();

                    sConnDemoData.ConnectionString = @"Data Source=LAPTOP-DJFHSMT5\SQLEXPRESS;Initial Catalog=MGaySLPDatabase;Integrated Security=True";// bu.ConnectionString;
                    sConnDemoData.Open();

                    daDemoData = new SqlDataAdapter("SELECT ClientName, ClientAddress, ClientPhone, clientBirthDate, ClientDiagnosis FROM ClientInfo WHERE ClientName = @Name", sConnDemoData);
                    daDemoData.SelectCommand.Parameters.Add("@Name", SqlDbType.NVarChar);
                    daDemoData.SelectCommand.Parameters["@Name"].Value = comboBox4.Text;
                    SqlCommandBuilder m_cbCommandBuilder = new SqlCommandBuilder(daDemoData);
                    daDemoData.Fill(DemoDataTable);
                    dataGridView1Client.DataSource = DemoDataTable;
                    dataGridView1Client.Columns[0].HeaderCell.Value = "Name";
                    dataGridView1Client.Columns[1].HeaderCell.Value = "Address";
                    dataGridView1Client.Columns[2].HeaderCell.Value = "Phone Number";
                    dataGridView1Client.Columns[3].HeaderCell.Value = "Birth Date";
                    dataGridView1Client.Columns[4].HeaderCell.Value = "Primary Diagnosis";
                    dataGridView1Client.AutoResizeColumns();
                    sConnDemoData.Close();
                }
                catch (Exception ex)
                {
                    MessageBox.Show("Error! Please try again.", ex.Message);
                }
            }
        }

        private void viewGoalButton_Click(object sender, EventArgs e)
        {

            if (comboBox7.Text == "")
            {
                MessageBox.Show("Please chose a name to view client goal(s)!");
            }
            else
            {
                try
                {
                    SqlConnection sConnGoalData = new SqlConnection();
                    SqlDataAdapter daGoalData;
                    DataTable GoalDataTable = new DataTable();

                    sConnGoalData.ConnectionString = @"Data Source=LAPTOP-DJFHSMT5\SQLEXPRESS;Initial Catalog=MGaySLPDatabase;Integrated Security=True";// bu.ConnectionString;
                    sConnGoalData.Open();

                    daGoalData = new SqlDataAdapter("SELECT ClientName, Disorder, Goal FROM ClientGoals WHERE ClientName = @Name", sConnGoalData);
                    daGoalData.SelectCommand.Parameters.Add("@Name", SqlDbType.NVarChar);
                    daGoalData.SelectCommand.Parameters["@Name"].Value = comboBox7.Text;
                    SqlCommandBuilder m_cbCommandBuilder = new SqlCommandBuilder(daGoalData);
                    daGoalData.Fill(GoalDataTable);
                    dataGridView2.DataSource = GoalDataTable;
                    dataGridView2.Columns[0].HeaderCell.Value = "Name";
                    dataGridView2.Columns[1].HeaderCell.Value = "Disorder Type";
                    dataGridView2.Columns[2].HeaderCell.Value = "Goal";
                    dataGridView2.AutoResizeColumns();
                    sConnGoalData.Close();
                }
                catch (Exception ex)
                {
                    MessageBox.Show("Error! Please try again.", ex.Message);
                }
            }

        }

        private void addGoalButton_Click(object sender, EventArgs e)
        {
            //https://www.c-sharpcorner.com/uploadfile/mahesh/commandbuilder-in-ado-net/

            //combobox8 is name, 9 is disorder, 10 is goal options 
            

            if (comboBox8.Text == "" || comboBox9.Text == "" || comboBox10.Text == "")
            {
                MessageBox.Show("One or more fields are blank. Please fill in missing data!");
            }
            else
            {
                try
                {
                    //fill diagnosis combo box and goal box, i suppose this will be on selected index changed actually 
                    SqlConnection sConnClientData = new SqlConnection();
                    SqlDataAdapter daClientData;

                    sConnClientData.ConnectionString = @"Data Source=LAPTOP-DJFHSMT5\SQLEXPRESS;Initial Catalog=MGaySLPDatabase;Integrated Security=True";// bu.ConnectionString;
                    sConnClientData.Open();
                    
                    daClientData = new SqlDataAdapter("SELECT ClientName, Disorder, Goal FROM ClientGoals ORDER by ClientName", sConnClientData);
                    SqlCommandBuilder m_cbCommandBuilder = new SqlCommandBuilder(daClientData);

                    DataSet ds = new DataSet("ClientGoalSet");
                    daClientData.Fill(ds, "ClientGoals");
                    DataTable ClientDataTable = ds.Tables["ClientGoals"];
                    DataRow row = ClientDataTable.NewRow();
                    row["ClientName"] = comboBox8.Text;
                    row["Disorder"] = comboBox9.Text;
                    row["Goal"] = comboBox10.Text;
                    ClientDataTable.Rows.Add(row);

                    daClientData.Update(ds, "ClientGoals");
                    MessageBox.Show(row["ClientName"].ToString().Trim() + "'s new goal added!");

                    sConnClientData.Close();

                    comboBox8.Text = "";
                    comboBox9.Text = "";
                    comboBox10.Text = "";
                }
                catch (Exception ex)
                {
                    MessageBox.Show("Error with adding goal!", ex.Message);
                }

            }
        }
        //add goal client name box
        private void comboBox8_SelectedIndexChanged(object sender, EventArgs e)
        {
            SqlConnection sConn = new SqlConnection();
            sConn.ConnectionString = @"Data Source=LAPTOP-DJFHSMT5\SQLEXPRESS;Initial Catalog=MGaySLPDatabase;Integrated Security=True";// bu.ConnectionString;

            SqlCommand sqlCmd = new SqlCommand("SELECT Disorder FROM ClientGoals WHERE ClientName=@Name", sConn);
            sqlCmd.Parameters.Add("@Name", SqlDbType.NVarChar);
            sqlCmd.Parameters["@Name"].Value = comboBox8.Text;
            sConn.Open();
            comboBox9.Items.Clear();
            SqlDataReader sqlReader = sqlCmd.ExecuteReader();
            while (sqlReader.Read())
            {
                comboBox9.Items.Add(sqlReader["Disorder"].ToString());
            }
            sqlReader.Close();
            sConn.Close();
        }
        //disorder combobox 
        private void comboBox9_SelectedIndexChanged(object sender, EventArgs e)
        {
            SqlConnection sConn = new SqlConnection();
            sConn.ConnectionString = @"Data Source=LAPTOP-DJFHSMT5\SQLEXPRESS;Initial Catalog=MGaySLPDatabase;Integrated Security=True";// bu.ConnectionString;

            SqlCommand sqlCmd = new SqlCommand("SELECT GoalText FROM GoalList WHERE Disorder=@Disorder", sConn);
            sqlCmd.Parameters.Add("@Disorder", SqlDbType.NVarChar);
            sqlCmd.Parameters["@Disorder"].Value = comboBox9.Text;
            sConn.Open();
            comboBox10.Items.Clear();
            SqlDataReader sqlReader = sqlCmd.ExecuteReader();
            while (sqlReader.Read())
            {
                comboBox10.Items.Add(sqlReader["GoalText"].ToString());
            }
            sqlReader.Close();
            sConn.Close();
        }
    }
}
