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

namespace TennisCourtManager
{
    public partial class Users : Form
    {
        public Users()
        {
            InitializeComponent();
            populate();

        }

        SqlConnection Con = new SqlConnection(@"Data Source=(LocalDB)\MSSQLLocalDB;AttachDbFilename=C:\Users\wziel\OneDrive\Dokumenty\CourtsDbase.mdf;Integrated Security=True;Connect Timeout=30");

        private void populate()
        {
            Con.Open();
            string Querry = "select * from UserTbl";
            SqlDataAdapter sda = new SqlDataAdapter(Querry, Con);
            SqlCommandBuilder builder = new SqlCommandBuilder(sda);
            var ds = new DataSet();
            sda.Fill(ds);
            UserDGV.DataSource = ds.Tables[0];
            Con.Close();
        }

        private void textBox3_TextChanged(object sender, EventArgs e)
        {

        }

        private void SaveBtn_Click(object sender, EventArgs e)
        {
            InsertUser();
        }

        private void InsertUser()
        {
            if (UNameTb.Text == "" || UPhoneTb.Text == "" || UEmailTb.Text == "" || UPasswordTb.Text == "")
            {
                MessageBox.Show("Uzupełnij wszystkie pola!");
            }
            else
            {
                try
                {
                    Con.Open();
                    SqlCommand cmd = new SqlCommand("INSERT INTO UserTbl (UName, UPhone, UEmail, UPassword) VALUES (@UN, @UP, @UE, @UPwd)", Con);
                    cmd.Parameters.AddWithValue("@UN", UNameTb.Text);
                    cmd.Parameters.AddWithValue("@UP", UPhoneTb.Text);
                    cmd.Parameters.AddWithValue("@UE", UEmailTb.Text);
                    cmd.Parameters.AddWithValue("@UPwd", UPasswordTb.Text);
                    cmd.ExecuteNonQuery();
                    Con.Close();
                    MessageBox.Show("Użytkownik został dodany");
                    populate();
                }
                catch (Exception ex)
                {
                    MessageBox.Show(ex.Message);
                    Con.Close();
                }
            }
        }

        private void EditUser(int userID)
        {
            if (UNameTb.Text == "" || UPhoneTb.Text == "" || UEmailTb.Text == "" || UPasswordTb.Text == "")
            {
                MessageBox.Show("Uzupełnij wszystkie pola!");
            }
            else
            {
                try
                {
                    Con.Open();
                    SqlCommand cmd = new SqlCommand("UPDATE UserTbl SET UName = @UN, UPhone = @UP, UEmail = @UE, UPassword = @UPwd WHERE UId = @UID", Con);
                    cmd.Parameters.AddWithValue("@UN", UNameTb.Text);
                    cmd.Parameters.AddWithValue("@UP", UPhoneTb.Text);
                    cmd.Parameters.AddWithValue("@UE", UEmailTb.Text);
                    cmd.Parameters.AddWithValue("@UPwd", UPasswordTb.Text);
                    cmd.Parameters.AddWithValue("@UID", userID);
                    cmd.ExecuteNonQuery();
                    Con.Close();
                    MessageBox.Show("Użytkownik został zaktualizowany");
                    populate();
                }
                catch (Exception ex)
                {
                    MessageBox.Show(ex.Message);
                    Con.Close();
                }
            }
        }

        private void DeleteUser(int userID)
        {
            if (MessageBox.Show("Czy na pewno chcesz usunąć tego użytkownika?", "Usuwanie Użytkownika", MessageBoxButtons.YesNo) == DialogResult.Yes)
            {
                try
                {
                    Con.Open();
                    SqlCommand cmd = new SqlCommand("DELETE FROM UserTbl WHERE UId = @UID", Con);
                    cmd.Parameters.AddWithValue("@UID", userID);
                    cmd.ExecuteNonQuery();
                    Con.Close();
                    MessageBox.Show("Użytkownik został usunięty");
                    populate();
                }
                catch (Exception ex)
                {
                    MessageBox.Show(ex.Message);
                    Con.Close();
                }
            }
        }

        int selectedUserID = 0;

        private void ClearFields()
        {
            UNameTb.Text = string.Empty;
            UPhoneTb.Text = string.Empty;
            UEmailTb.Text = string.Empty;
            UPasswordTb.Text = string.Empty;
            selectedUserID = 0;
        }

        private void UserDGV_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {
            if (e.RowIndex >= 0)
            {
                DataGridViewRow row = UserDGV.Rows[e.RowIndex];

                UNameTb.Text = row.Cells["UName"].Value.ToString();
                UPhoneTb.Text = row.Cells["UPhone"].Value.ToString();
                UEmailTb.Text = row.Cells["UEmail"].Value.ToString();
                UPasswordTb.Text = row.Cells["UPassword"].Value.ToString();

                selectedUserID = Convert.ToInt32(row.Cells["UId"].Value);
            }
        }

        private void label4_Click(object sender, EventArgs e)
        {
            Form1 form1 = new Form1();
            form1.Show();
            this.Hide();
        }

        private void button2_Click(object sender, EventArgs e)
        {
            if (selectedUserID > 0) // Sprawdź, czy jakieś ID zostało wybrane
            {
                EditUser(selectedUserID);
            }
            else
            {
                MessageBox.Show("Proszę najpierw wybrać użytkownika do edycji.");
            }
        }

        private void DeleteBtn_Click(object sender, EventArgs e)
        {
            if (selectedUserID > 0) // Sprawdź, czy jakieś ID zostało wybrane
            {
                DeleteUser(selectedUserID);
            }
            else
            {
                MessageBox.Show("Proszę najpierw wybrać użytkownika do edycji.");
            }
        }

        private void ClearBtn_Click(object sender, EventArgs e)
        {
            ClearFields();
        }

        private void label6_Click(object sender, EventArgs e)
        {

        }

        private void label10_Click(object sender, EventArgs e)
        {

        }

        private void label12_Click(object sender, EventArgs e)
        {

        }

        private void label1_Click(object sender, EventArgs e)
        {
            Login login = new Login();
            login.Show();
            this.Hide();
        }

        private void pictureBox1_Click(object sender, EventArgs e)
        {
            Login login = new Login();
            login.Show();
            this.Hide();
        }
    }
}
