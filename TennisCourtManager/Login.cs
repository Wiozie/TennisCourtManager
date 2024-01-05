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
    public partial class Login : Form
    {
        public Login()
        {
            InitializeComponent();
        }

        private void label15_Click(object sender, EventArgs e)
        {

        }

        SqlConnection Con = new SqlConnection(@"Data Source=(LocalDB)\MSSQLLocalDB;AttachDbFilename=C:\Users\wziel\OneDrive\Dokumenty\CourtsDbase.mdf;Integrated Security=True;Connect Timeout=30");

        private void LoginBtn_Click(object sender, EventArgs e)
        {
            string userName = UserNameTb.Text;
            string password = PasswordTb.Text;

            try
            {
                Con.Open();
                SqlCommand cmd = new SqlCommand("SELECT COUNT(1) FROM UserTbl WHERE UName = @UName AND UPassword = @UPassword", Con);
                cmd.Parameters.AddWithValue("@UName", userName);
                cmd.Parameters.AddWithValue("@UPassword", password);
                int count = Convert.ToInt32(cmd.ExecuteScalar());

                if (count == 1)
                {
                    // Pomyślne logowanie
                    Form1 mainForm = new Form1();
                    this.Hide();
                    mainForm.Show();
                }
                else
                {
                    // Nieudane logowanie
                    MessageBox.Show("Nazwa użytkownika lub hasło są nieprawidłowe.");
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Wystąpił błąd: " + ex.Message);
            }
            finally
            {
                Con.Close();
            }
        }

        private void ClosePb_Click(object sender, EventArgs e)
        {
            Application.Exit();
        }

        private void AdminLb_Click(object sender, EventArgs e)
        {
            Admin admin = new Admin();
            admin.Show();
            this.Hide();
        }
    }
}
