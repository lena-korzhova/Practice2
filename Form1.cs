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

namespace Lenapoject
{
    public partial class Form1 : Form
    {
        SqlConnection sqlConnection;

        public Form1()
        {
            InitializeComponent();
        }

        private void textBox1_TextChanged(object sender, EventArgs e)
        {

        }

        private void tabPage3_Click(object sender, EventArgs e)
        {

        }

        private async void Form1_Load(object sender, EventArgs e)
        {
            string connectionString = @"Data Source=(LocalDB)\MSSQLLocalDB;AttachDbFilename=C:\Users\Елена\source\repos\Lenapoject\Lenapoject\Database1.mdf;Integrated Security=True";
            sqlConnection = new SqlConnection(connectionString);
            await sqlConnection.OpenAsync();
            SqlDataReader sqlReader = null;
            SqlCommand command = new SqlCommand("Select  * from[mission] where health = 1",sqlConnection);
            try
            {
                sqlReader =await command.ExecuteReaderAsync();
                while (await sqlReader.ReadAsync())
                {
                    listBox1.Items.Add(Convert.ToString(sqlReader["id"]) + "      " + Convert.ToString(sqlReader["ns"]) + "      " + Convert.ToString(sqlReader["epoch_start"]) + "      " + Convert.ToString(sqlReader["epoch_fin"]) + "      " + Convert.ToString(sqlReader["health"]));
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message.ToString(), ex.Source.ToString(), MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            finally
            {
                if (sqlReader != null)
                    sqlReader.Close();
            }

        }

        private void выходToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (sqlConnection != null && sqlConnection.State != ConnectionState.Closed)
                sqlConnection.Close(); 
        }

        private void Form1_FormClosed(object sender, FormClosedEventArgs e)
        {
            if (sqlConnection != null && sqlConnection.State != ConnectionState.Closed)
                sqlConnection.Close();
        }

        private async void button1_Click(object sender, EventArgs e)

        {
            if (label11.Visible)
                label11.Visible = false;

            if (!string.IsNullOrEmpty(textBox1.Text) && !string.IsNullOrWhiteSpace(textBox1.Text) &&
                !string.IsNullOrEmpty(textBox2.Text) && !string.IsNullOrWhiteSpace(textBox2.Text) &&
                !string.IsNullOrEmpty(textBox3.Text) && !string.IsNullOrWhiteSpace(textBox3.Text) &&
                !string.IsNullOrEmpty(textBox4.Text) && !string.IsNullOrWhiteSpace(textBox4.Text))
            {
                SqlCommand command = new SqlCommand("Insert into [mission] (ns, epoch_start, epoch_fin, health)values(@ns,@epoch_start,@epoch_fin,@health)", sqlConnection);

                command.Parameters.AddWithValue("ns", textBox1.Text);
                command.Parameters.AddWithValue("epoch_start", textBox3.Text );
                command.Parameters.AddWithValue("epoch_fin", textBox2.Text);
                command.Parameters.AddWithValue("health", textBox4.Text);
                await command.ExecuteNonQueryAsync();

            }
            else
            {
                label11.Visible = true;
                label11.Text = "Все поля должны быть заполнены!";
            }
        }

        private async void обновитьToolStripMenuItem_Click(object sender, EventArgs e)
        {
            listBox1.Items.Clear();
            SqlDataReader sqlReader = null;
            SqlCommand command = new SqlCommand("Select*FROM[mission]", sqlConnection);
            try
            {
                sqlReader = await command.ExecuteReaderAsync();
                while (await sqlReader.ReadAsync())
                {
                    listBox1.Items.Add(Convert.ToString(sqlReader["id"]) + "      " + Convert.ToString(sqlReader["ns"]) + "      " + Convert.ToString(sqlReader["epoch_start"]) + "      " + Convert.ToString(sqlReader["epoch_fin"]) + "      " + Convert.ToString(sqlReader["health"]));
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message.ToString(), ex.Source.ToString(), MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            finally
            {
                if (sqlReader != null)
                    sqlReader.Close();
            }
        }

        private async void button2_Click(object sender, EventArgs e)
        {
            if (label15.Visible)
                label15.Visible = false;

            if (!string.IsNullOrEmpty(textBox8.Text) && !string.IsNullOrWhiteSpace(textBox8.Text) &&
                !string.IsNullOrEmpty(textBox6.Text) && !string.IsNullOrWhiteSpace(textBox6.Text) &&
                !string.IsNullOrEmpty(textBox5.Text) && !string.IsNullOrWhiteSpace(textBox5.Text) &&
                !string.IsNullOrEmpty(textBox7.Text) && !string.IsNullOrWhiteSpace(textBox7.Text))
            {
                SqlCommand command = new SqlCommand("Update [mission] SET [ns]=@ns, [epoch_start]=@epoch_start,[epoch_fin]=@epoch_fin, [health]=@health WHERE [id]=@id", sqlConnection);
                command.Parameters.AddWithValue("ns", textBox8.Text);
                command.Parameters.AddWithValue("epoch_start", textBox6.Text);
                command.Parameters.AddWithValue("epoch_fin", textBox7.Text);
                command.Parameters.AddWithValue("health", textBox5.Text);
                command.Parameters.AddWithValue("id", textBox10.Text);

                await command.ExecuteNonQueryAsync();
            }
            else if (!string.IsNullOrEmpty(textBox10.Text) && !string.IsNullOrWhiteSpace(textBox10.Text))
            {
                label15.Visible = true;
                label15.Text = "ID должен заполнен";
            }
            else
                label15.Visible = true;
            label15.Text = "Заполните все поля";

        }

        private async void button3_Click(object sender, EventArgs e)
        {
            if (label16.Visible)
                label16.Visible = false;

            if (!string.IsNullOrEmpty(textBox11.Text) && !string.IsNullOrWhiteSpace(textBox11.Text))
            {
                SqlCommand command = new SqlCommand("Delete FROM [mission] WHERE [id]=@id",sqlConnection);
                command.Parameters.AddWithValue("id", textBox11.Text);

                await command.ExecuteNonQueryAsync();
            }
            else
            {
                label16.Text = "ID должен заполнен";
            }
      

        }
    }
}
