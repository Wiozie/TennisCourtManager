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
    public partial class Customers : Form
    {
        public Customers()
        {
            InitializeComponent();
            populate();
        }

        SqlConnection Con = new SqlConnection(@"Data Source=(LocalDB)\MSSQLLocalDB;AttachDbFilename=C:\Users\wziel\OneDrive\Dokumenty\CourtsDbase.mdf;Integrated Security=True;Connect Timeout=30");

        private void populate()
        {
            Con.Open();
            string Querry = "select * from CustomerTbl";
            SqlDataAdapter sda = new SqlDataAdapter(Querry, Con);
            SqlCommandBuilder builder = new SqlCommandBuilder(sda);
            var ds = new DataSet();
            sda.Fill(ds);
            CustDGV.DataSource = ds.Tables[0];
            Con.Close();
        }

        private void InsertCustomer()
        {
            if (CustNameTb.Text == "" || CustPhoneTb.Text == "" || CustEmailTb.Text == "")
            {
                MessageBox.Show("Uzupełnij wszystkie pola!");
            }
            else
            {
                try
                {
                    Con.Open();
                    SqlCommand cmd = new SqlCommand("INSERT INTO CustomerTbl (CustName, CustPhone, CustEmail) VALUES (@CN, @CP, @CE)", Con);
                    cmd.Parameters.AddWithValue("@CN", CustNameTb.Text);
                    cmd.Parameters.AddWithValue("@CP", CustPhoneTb.Text);
                    cmd.Parameters.AddWithValue("@CE", CustEmailTb.Text);
                    cmd.ExecuteNonQuery();
                    Con.Close();
                    MessageBox.Show("Klient został dodany");
                    populate();
                }
                catch (Exception ex)
                {
                    MessageBox.Show(ex.Message);
                    Con.Close();
                }
            }
        }

        private void EditCustomer(int customerID)
        {
            if (CustNameTb.Text == "" || CustPhoneTb.Text == "" || CustEmailTb.Text == "")
            {
                MessageBox.Show("Uzupełnij wszystkie pola!");
            }
            else
            {
                try
                {
                    Con.Open();
                    SqlCommand cmd = new SqlCommand("UPDATE CustomerTbl SET CustName = @CN, CustPhone = @CP, CustEmail = @CE WHERE CustId = @CID", Con);
                    cmd.Parameters.AddWithValue("@CN", CustNameTb.Text);
                    cmd.Parameters.AddWithValue("@CP", CustPhoneTb.Text);
                    cmd.Parameters.AddWithValue("@CE", CustEmailTb.Text);
                    cmd.Parameters.AddWithValue("@CID", customerID);
                    cmd.ExecuteNonQuery();
                    Con.Close();
                    MessageBox.Show("Klient został zaktualizowany");
                    populate();
                }
                catch (Exception ex)
                {
                    MessageBox.Show(ex.Message);
                    Con.Close();
                }
            }
        }

        private void DeleteCustomer(int customerID)
        {
            if (MessageBox.Show("Czy na pewno chcesz usunąć tego klienta?", "Usuwanie Klienta", MessageBoxButtons.YesNo) == DialogResult.Yes)
            {
                try
                {
                    Con.Open();
                    SqlCommand cmd = new SqlCommand("DELETE FROM CustomerTbl WHERE CustId = @CID", Con);
                    cmd.Parameters.AddWithValue("@CID", customerID);
                    cmd.ExecuteNonQuery();
                    Con.Close();
                    MessageBox.Show("Klient został usunięty");
                    populate();
                }
                catch (Exception ex)
                {
                    MessageBox.Show(ex.Message);
                    Con.Close();
                }
            }
        }

        int selectedCustomerID = 0;

        private void ClearFields()
        {
            CustNameTb.Text = string.Empty;
            CustPhoneTb.Text = string.Empty;
            CustEmailTb.Text = string.Empty;
            selectedCustomerID = 0;
        }

        private void SaveBtn_Click(object sender, EventArgs e)
        {
            InsertCustomer();
        }

        private void button2_Click(object sender, EventArgs e)
        {
            if (selectedCustomerID > 0) // Sprawdź, czy jakieś ID zostało wybrane
            {
                EditCustomer(selectedCustomerID);
            }
            else
            {
                MessageBox.Show("Proszę najpierw wybrać użytkownika do edycji.");
            }
        }

        private void CustDGV_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {
            if (e.RowIndex >= 0)
            {
                DataGridViewRow row = CustDGV.Rows[e.RowIndex];

                CustNameTb.Text = row.Cells["CustName"].Value.ToString();
                CustPhoneTb.Text = row.Cells["CustPhone"].Value.ToString();
                CustEmailTb.Text = row.Cells["CustEmail"].Value.ToString();

                selectedCustomerID = Convert.ToInt32(row.Cells["CustId"].Value);
            }
        }

        private void DeleteBtn_Click(object sender, EventArgs e)
        {
            if (selectedCustomerID > 0) // Sprawdź, czy jakieś ID zostało wybrane
            {
                DeleteCustomer(selectedCustomerID);
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

        private void label4_Click(object sender, EventArgs e)
        {
            Form1 form = new Form1();
            form.Show();
            this.Hide();
        }

        private void label6_Click(object sender, EventArgs e)
        {
            Kategorie kategorie = new Kategorie();
            kategorie.Show();
            this.Hide();
        }

        private void label8_Click(object sender, EventArgs e)
        {
            MessageBox.Show("Nie posiadasz odpowiednich uprawnień");
        }

        private void label12_Click(object sender, EventArgs e)
        {
            Reservations reservation = new Reservations();
            reservation.Show();
            this.Hide();
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
