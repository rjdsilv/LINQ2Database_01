using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Data.SqlClient;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace LINQ2Database_01
{
    public partial class Linq2Db01Form : Form
    {
        public Linq2Db01Form()
        {
            InitializeComponent();
        }

        private void saveButton_Click(object sender, EventArgs e)
        {
            string cnnStr = @"Data Source=localhost\SQLEXPRESS;Initial Catalog=COMP211_Week06;Integrated Security=True;Pooling=False";

            try
            {
                using (SqlConnection cnn = new SqlConnection(cnnStr))
                {
                    using (SqlCommand cmd = new SqlCommand())
                    {
                        cmd.Connection = cnn;
                        cmd.CommandText = "INSERT into ExtraWork(Name, Hours, Date) VALUES(@Name, @Hours, @Date)";

                        int hours;

                        if (int.TryParse(hoursTextBox.Text, out hours))
                        {
                            if (hours > 8)
                            {
                                MessageBox.Show("The numberof hours can't be greater than 8. Please correct the entered value!", "Warning", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                                return;
                            }
                        }
                        else
                        {
                            MessageBox.Show("Please, enter a number on the hours text box!", "Warning", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                            return;
                        }

                        cmd.Parameters.AddWithValue("@Name", nameTextBox.Text);
                        cmd.Parameters.AddWithValue("@Hours", hours);
                        cmd.Parameters.AddWithValue("@Date", datePicker.Value);

                        cnn.Open();
                        cmd.ExecuteNonQuery();
                        MessageBox.Show("Record successfully added!", "Success", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Cannot open connection!", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }
    }
}
