using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace TennisCourtManager
{
    public partial class Admin : Form
    {
        public Admin()
        {
            InitializeComponent();
        }

        private void AdminLb_Click(object sender, EventArgs e)
        {
            Login login = new Login();
            login.Show();
            this.Hide();
        }

        private void ClosePb_Click(object sender, EventArgs e)
        {
            Application.Exit();
        }

        private void LoginBtn_Click(object sender, EventArgs e)
        {
            if(PasswordTb.Text == "")
            {
                MessageBox.Show("Wprowadź hasło!");
            }
            else
            {
                if(PasswordTb.Text == "Pass")
                {
                    Users users = new Users();
                    users.Show();
                    this.Hide();
                }
                else
                {
                    MessageBox.Show("Nieprawidłowe hasło");
                }
            }
        }
    }
}
