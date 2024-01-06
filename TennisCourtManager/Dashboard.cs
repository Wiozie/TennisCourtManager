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
    public partial class Dashboard : Form
    {
        public Dashboard()
        {
            InitializeComponent();
            DisplayTotalCourtsCount();
            DisplayTotalCustomersCount();
            DisplayTotalUsersCount();
            DisplayTotalReservationsCount();
            DisplayTotalRevenue();
            LoadCustomersIntoComboBox();
            LoadCourtsIntoComboBox();
            DisplayMostPopularCourt();
            DisplayMostPopularHour();
            DisplayMostPopularCustomer();
            DisplayMostPopularDay();
        }

        SqlConnection Con = new SqlConnection(@"Data Source=(LocalDB)\MSSQLLocalDB;AttachDbFilename=C:\Users\wziel\OneDrive\Dokumenty\CourtsDbase.mdf;Integrated Security=True;Connect Timeout=30");

        private void DisplayMostPopularDay()
        {
            try
            {
                Con.Open();
                // Zapytanie do bazy danych, aby znaleźć dzień z największą liczbą rezerwacji
                SqlCommand cmd = new SqlCommand(@"
            SELECT TOP 1 CAST(ResStart AS DATE) AS Date
            FROM ReservationTbl
            GROUP BY CAST(ResStart AS DATE)
            ORDER BY COUNT(*) DESC", Con);

                object result = cmd.ExecuteScalar();
                string mostPopularDay = result != null ? Convert.ToDateTime(result).ToShortDateString() : "Brak danych";
                PopDayLbl.Text = mostPopularDay;
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


        private void DisplayMostPopularCustomer()
        {
            try
            {
                Con.Open();
                // Zapytanie do bazy danych, aby znaleźć klienta z największą liczbą rezerwacji
                SqlCommand cmd = new SqlCommand(@"
            SELECT TOP 1 cu.CustName
            FROM CustomerTbl cu
            JOIN ReservationTbl r ON cu.CustID = r.ResCustomer
            GROUP BY cu.CustName
            ORDER BY COUNT(r.ResID) DESC", Con);

                object result = cmd.ExecuteScalar();
                string mostPopularCustomer = result != null ? result.ToString() : "Brak danych";
                PopCustLbl.Text = mostPopularCustomer;
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

        private void DisplayTotalCourtsCount()
        {
            try
            {
                Con.Open();
                SqlCommand cmd = new SqlCommand("SELECT COUNT(*) FROM CourtTbl", Con);
                int count = Convert.ToInt32(cmd.ExecuteScalar());
                CountCourtsLbl.Text = count.ToString();
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

        private void DisplayTotalCustomersCount()
        {
            try
            {
                Con.Open();
                SqlCommand cmd = new SqlCommand("SELECT COUNT(*) FROM CustomerTbl", Con);
                int count = Convert.ToInt32(cmd.ExecuteScalar());
                CountCustLbl.Text = count.ToString();
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

        private void DisplayTotalUsersCount()
        {
            try
            {
                Con.Open();
                SqlCommand cmd = new SqlCommand("SELECT COUNT(*) FROM UserTbl", Con);
                int count = Convert.ToInt32(cmd.ExecuteScalar());
                CountUsersLbl.Text = count.ToString();
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

        private void DisplayTotalReservationsCount()
        {
            try
            {
                Con.Open();
                SqlCommand cmd = new SqlCommand("SELECT COUNT(*) FROM ReservationTbl", Con);
                int count = Convert.ToInt32(cmd.ExecuteScalar());
                CountResLbl.Text = count.ToString();
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

        private void DisplayTotalRevenue()
        {
            try
            {
                Con.Open();
                SqlCommand cmd = new SqlCommand("SELECT SUM(ResCost) FROM ReservationTbl", Con);
                // Zabezpieczenie przed wartością NULL z bazy danych
                object result = cmd.ExecuteScalar();
                int totalRevenue = result != DBNull.Value ? Convert.ToInt32(result) : 0;
                TotalRevenueLbl.Text = totalRevenue.ToString("C"); // Formatowanie jako wartość walutowa
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

        private void LoadCustomersIntoComboBox()
        {
            try
            {
                Con.Open();
                SqlCommand cmd = new SqlCommand("SELECT CustName FROM CustomerTbl", Con);
                SqlDataReader reader = cmd.ExecuteReader();

                CustomerCb.Items.Clear(); // Czyszczenie ComboBox przed załadowaniem nowych danych

                while (reader.Read())
                {
                    CustomerCb.Items.Add(reader["CustName"].ToString());
                }

                reader.Close();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
            finally
            {
                Con.Close();
            }
        }

        private void LoadCourtsIntoComboBox()
        {
            try
            {
                Con.Open();
                SqlCommand cmd = new SqlCommand("SELECT CName FROM CourtTbl", Con);
                SqlDataReader reader = cmd.ExecuteReader();

                CourtCb.Items.Clear(); // Czyszczenie ComboBox przed załadowaniem nowych danych

                while (reader.Read())
                {
                    CourtCb.Items.Add(reader["CName"].ToString());
                }

                reader.Close();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
            finally
            {
                Con.Close();
            }
        }

        private void DisplayCustomerRevenue()
        {
            string selectedCustomer = CustomerCb.SelectedItem?.ToString();
            if (selectedCustomer == null)
            {
                MessageBox.Show("Proszę wybrać klienta.");
                return;
            }

            try
            {
                Con.Open();
                // Zapytanie do bazy danych, aby obliczyć sumę zysków z rezerwacji dla wybranego klienta
                SqlCommand cmd = new SqlCommand("SELECT SUM(ResCost) FROM ReservationTbl WHERE ResCustomer = (SELECT CustID FROM CustomerTbl WHERE CustName = @CustName)", Con);
                cmd.Parameters.AddWithValue("@CustName", selectedCustomer);

                object result = cmd.ExecuteScalar();
                int totalRevenue = result != DBNull.Value ? Convert.ToInt32(result) : 0;
                CustRevenueLbl.Text = totalRevenue.ToString("C2"); // Formatowanie jako wartość walutowa
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

        private void DisplayCourtRevenue()
        {
            string selectedCourt = CourtCb.SelectedItem?.ToString();
            if (selectedCourt == null)
            {
                MessageBox.Show("Proszę wybrać kort.");
                return;
            }

            try
            {
                Con.Open();
                // Zapytanie do bazy danych, aby obliczyć sumę zysków z rezerwacji dla wybranego kortu
                SqlCommand cmd = new SqlCommand("SELECT SUM(ResCost) FROM ReservationTbl WHERE ResCourt = (SELECT CNum FROM CourtTbl WHERE CName = @CourtName)", Con);
                cmd.Parameters.AddWithValue("@CourtName", selectedCourt);

                object result = cmd.ExecuteScalar();
                int totalRevenue = result != DBNull.Value ? Convert.ToInt32(result) : 0;
                CourtRevenueLbl.Text = totalRevenue.ToString("C2"); // Formatowanie jako wartość walutowa
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

        private void DisplayRevenueForSelectedDate()
        {
            DateTime selectedDate = DateDtp.Value.Date; // Pobieranie tylko daty, bez czasu

            try
            {
                Con.Open();
                // Zapytanie do bazy danych, aby obliczyć sumę przychodów z rezerwacji dla wybranego dnia
                SqlCommand cmd = new SqlCommand("SELECT SUM(ResCost) FROM ReservationTbl WHERE CAST(ResStart AS DATE) = @SelectedDate", Con);
                cmd.Parameters.AddWithValue("@SelectedDate", selectedDate);

                object result = cmd.ExecuteScalar();
                int dailyRevenue = result != DBNull.Value ? Convert.ToInt32(result) : 0;
                DailyRevenueLbl.Text = dailyRevenue.ToString("C2"); // Formatowanie jako wartość walutowa
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

        private void DisplayMostPopularCourt()
        {
            try
            {
                Con.Open();
                // Zapytanie do bazy danych, aby znaleźć kort z największą liczbą rezerwacji
                SqlCommand cmd = new SqlCommand(@"
            SELECT TOP 1 c.CName
            FROM CourtTbl c
            JOIN ReservationTbl r ON c.CNum = r.ResCourt
            GROUP BY c.CName
            ORDER BY COUNT(r.ResID) DESC", Con);

                object result = cmd.ExecuteScalar();
                string mostPopularCourt = result != null ? result.ToString() : "Brak danych";
                PopCourtLbl.Text = mostPopularCourt;
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

        private void DisplayMostPopularHour()
        {
            try
            {
                Con.Open();
                // Zapytanie do bazy danych, aby znaleźć najczęstszą godzinę rozpoczęcia rezerwacji
                SqlCommand cmd = new SqlCommand(@"
            SELECT TOP 1 DATEPART(HOUR, ResStart) AS Hour, COUNT(*) AS Count
            FROM ReservationTbl
            GROUP BY DATEPART(HOUR, ResStart)
            ORDER BY Count DESC", Con);

                object result = cmd.ExecuteScalar();
                string mostPopularHour = result != null ? result.ToString() + ":00" : "Brak danych";
                PopHourLbl.Text = mostPopularHour;
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

        private void label16_Click(object sender, EventArgs e)
        {

        }

        private void CustomerCb_SelectedIndexChanged(object sender, EventArgs e)
        {
            DisplayCustomerRevenue();
        }

        private void CourtCb_SelectedIndexChanged(object sender, EventArgs e)
        {
            DisplayCourtRevenue();
        }

        private void label35_Click(object sender, EventArgs e)
        {

        }

        private void DateDtp_ValueChanged(object sender, EventArgs e)
        {
            DisplayRevenueForSelectedDate();
        }

        private void label8_Click(object sender, EventArgs e)
        {
            Users us = new Users();
            us.Show();
            this.Hide();
        }

        private void label1_Click(object sender, EventArgs e)
        {
            Login login = new Login();
            login.Show();
            this.Hide();
        }
    }
}
