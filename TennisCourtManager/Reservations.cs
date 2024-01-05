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
    public partial class Reservations : Form
    {
        public Reservations()
        {
            InitializeComponent();
            LoadCustomerNames();
            populate();
        }

        SqlConnection Con = new SqlConnection(@"Data Source=(LocalDB)\MSSQLLocalDB;AttachDbFilename=C:\Users\wziel\OneDrive\Dokumenty\CourtsDbase.mdf;Integrated Security=True;Connect Timeout=30");

        private void populate()
        {
            Con.Open();
            string Querry = @"
        SELECT 
            r.ResID, 
            r.ResStart, 
            r.ResEnd, 
            c.CName AS ResCourt, 
            cu.CustName AS ResCustomer, 
            r.ResCost 
        FROM 
            ReservationTbl r
            INNER JOIN CourtTbl c ON r.ResCourt = c.CNum
            INNER JOIN CustomerTbl cu ON r.ResCustomer = cu.CustID";

            SqlDataAdapter sda = new SqlDataAdapter(Querry, Con);
            SqlCommandBuilder builder = new SqlCommandBuilder(sda);
            var ds = new DataSet();
            sda.Fill(ds);
            ResDGV.DataSource = ds.Tables[0];
            Con.Close();
        }


        private DateTime? GetReservationStart()
        {
            // Pobieranie daty
            DateTime date = ResStartDtp.Value.Date;

            // Pobieranie i konwersja godziny
            if (ResStartCb.SelectedItem != null)
            {
                string timeString = ResStartCb.SelectedItem.ToString();
                TimeSpan time;
                if (TimeSpan.TryParse(timeString, out time))
                {
                    // Łączenie daty i czasu
                    return date.Add(time);
                }
            }
            return null; // Jeśli czas nie został wybrany lub jest nieprawidłowy
        }

        private DateTime? GetReservationEnd()
        {
            var start = GetReservationStart();
            if (start.HasValue && ResTimeCb.SelectedItem != null)
            {
                int durationInHours;
                if (int.TryParse(ResTimeCb.SelectedItem.ToString(), out durationInHours))
                {
                    return start.Value.AddHours(durationInHours);
                }
            }
            return null; // Jeśli czas trwania nie został wybrany lub jest nieprawidłowy
        }

        private void LoadCustomerNames()
        {
            try
            {
                Con.Open();
                SqlCommand cmd = new SqlCommand("SELECT CustName FROM CustomerTbl", Con);
                SqlDataReader reader = cmd.ExecuteReader();

                ResCustomerCb.Items.Clear(); // Czyszczenie ComboBox przed załadowaniem nowych danych

                while (reader.Read())
                {
                    ResCustomerCb.Items.Add(reader["CustName"].ToString());
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

        private void LoadAvailableCourts()
        {
            var start = GetReservationStart();
            var end = GetReservationEnd();
            if (start != null && end != null)
            {
                try
                {
                    Con.Open();
                    SqlCommand cmd = new SqlCommand("SELECT CName FROM CourtTbl WHERE CStatus = 'Dostępny' AND CNum NOT IN (SELECT ResCourt FROM ReservationTbl WHERE NOT (ResEnd <= @Start OR ResStart >= @End))", Con);
                    cmd.Parameters.AddWithValue("@Start", start);
                    cmd.Parameters.AddWithValue("@End", end);

                    SqlDataReader reader = cmd.ExecuteReader();

                    ResCourtCb.Items.Clear(); // Czyszczenie ComboBox przed załadowaniem nowych danych

                    while (reader.Read())
                    {
                        ResCourtCb.Items.Add(reader["CName"].ToString());
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
        }

        private void InsertReservation()
        {
            var start = GetReservationStart();
            var end = GetReservationEnd();
            var selectedCourtName = ResCourtCb.SelectedItem?.ToString();
            var selectedCustomerName = ResCustomerCb.SelectedItem?.ToString();
            var durationInHours = (end.Value - start.Value).TotalHours;

            if (start == null || end == null || selectedCourtName == null || selectedCustomerName == null)
            {
                MessageBox.Show("Proszę uzupełnić wszystkie pola przed zapisem rezerwacji.");
                return;
            }

            try
            {
                Con.Open();

                // Pobranie ID wybranego kortu
                SqlCommand cmdCourt = new SqlCommand("SELECT CNum FROM CourtTbl WHERE CName = @CourtName", Con);
                cmdCourt.Parameters.AddWithValue("@CourtName", selectedCourtName);
                var courtId = cmdCourt.ExecuteScalar().ToString();

                // Pobranie ID wybranego klienta
                SqlCommand cmdCustomer = new SqlCommand("SELECT CustId FROM CustomerTbl WHERE CustName = @CustomerName", Con);
                cmdCustomer.Parameters.AddWithValue("@CustomerName", selectedCustomerName);
                var customerId = cmdCustomer.ExecuteScalar().ToString();

                // Obliczenie całkowitego kosztu
                SqlCommand cmdCost = new SqlCommand("SELECT CatCost FROM CourtTbl INNER JOIN CatTbl ON CourtTbl.CType = CatTbl.CatID WHERE CName = @CourtName", Con);
                cmdCost.Parameters.AddWithValue("@CourtName", selectedCourtName);
                var costPerHour = Convert.ToInt32(cmdCost.ExecuteScalar());
                var totalCost = (int)(costPerHour * durationInHours);

                // Zapisanie rezerwacji
                SqlCommand cmdInsert = new SqlCommand("INSERT INTO ReservationTbl (ResStart, ResEnd, ResCourt, ResCustomer, ResCost) VALUES (@Start, @End, @Court, @Customer, @Cost)", Con);
                cmdInsert.Parameters.AddWithValue("@Start", start.Value);
                cmdInsert.Parameters.AddWithValue("@End", end.Value);
                cmdInsert.Parameters.AddWithValue("@Court", courtId);
                cmdInsert.Parameters.AddWithValue("@Customer", customerId);
                cmdInsert.Parameters.AddWithValue("@Cost", totalCost);

                cmdInsert.ExecuteNonQuery();
                MessageBox.Show("Rezerwacja została zapisana pomyślnie.");
            }
            catch (Exception ex)
            {
                MessageBox.Show("Wystąpił błąd podczas zapisu rezerwacji: " + ex.Message);
            }
            finally
            {
                Con.Close();
                populate();
                ClearFields();
            }
        }

        private void ClearFields()
        {
            // Resetowanie DateTimePicker do bieżącej daty
            ResStartDtp.Value = DateTime.Now;

            // Czyszczenie ComboBoxów i ustawianie ich indeksów na -1 (brak selekcji)
            ResStartCb.SelectedIndex = -1;
            ResTimeCb.SelectedIndex = -1;
            ResCustomerCb.SelectedIndex = -1;
            ResCourtCb.SelectedIndex = -1;

            // Czyszczenie TextBoxa kosztu rezerwacji
            ResCostTb.Clear();
        }

        private void ResSaveBtn_Click(object sender, EventArgs e)
        {
            InsertReservation();
        }

        private void CalculateAndDisplayTotalCost()
        {
            var end = GetReservationEnd();
            if (ResCourtCb.SelectedItem != null && end != null)
            {
                try
                {
                    // Pobierz wybrany kort
                    string selectedCourt = ResCourtCb.SelectedItem?.ToString();
                    if (selectedCourt == null)
                    {
                        throw new InvalidOperationException("Nie wybrano kortu.");
                    }

                    // Pobierz czas trwania rezerwacji
                    int durationInHours = int.Parse(ResTimeCb.SelectedItem.ToString());

                    Con.Open();
                    // Znajdź CType dla wybranego kortu
                    SqlCommand cmd1 = new SqlCommand("SELECT CType FROM CourtTbl WHERE CName = @CourtName", Con);
                    cmd1.Parameters.AddWithValue("@CourtName", selectedCourt);
                    string courtType = cmd1.ExecuteScalar().ToString();

                    // Znajdź CatCost dla tego CType
                    SqlCommand cmd2 = new SqlCommand("SELECT CatCost FROM CatTbl WHERE CatID = @CourtType", Con);
                    cmd2.Parameters.AddWithValue("@CourtType", courtType);
                    decimal courtCostPerHour = Convert.ToDecimal(cmd2.ExecuteScalar());

                    Con.Close();

                    // Oblicz całkowity koszt
                    decimal totalCost = courtCostPerHour * durationInHours;
                    ResCostTb.Text = totalCost.ToString("C2"); // Formatowanie jako waluta
                }
                catch (Exception ex)
                {
                    MessageBox.Show("Błąd: " + ex.Message);
                    Con.Close();
                }
            }
        }

        private void ResStartDtp_ValueChanged(object sender, EventArgs e)
        {
            LoadAvailableCourts();
        }

        private void ResStartCb_SelectedIndexChanged(object sender, EventArgs e)
        {
            LoadAvailableCourts();
        }

        private void ResTimeCb_SelectedIndexChanged(object sender, EventArgs e)
        {
            LoadAvailableCourts();
            if (ResStartCb.SelectedItem != null && ResTimeCb.SelectedItem != null)
            {
                CalculateAndDisplayTotalCost();
            }
        }

        private void ResCourtCb_SelectedIndexChanged(object sender, EventArgs e)
        {
            CalculateAndDisplayTotalCost();
            if (ResStartCb.SelectedItem != null && ResTimeCb.SelectedItem != null)
            {
                CalculateAndDisplayTotalCost();
            }
        }

        private void ResClearBtn_Click(object sender, EventArgs e)
        {
            ClearFields();
        }

        int selectedResID = 0;

        private void ResDGV_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {
            if (e.RowIndex >= 0)
            {
                DataGridViewRow row = ResDGV.Rows[e.RowIndex];

                // Ustawienie kontrolki DateTimePicker na datę i godzinę rozpoczęcia rezerwacji
                ResStartDtp.Value = Convert.ToDateTime(row.Cells["ResStart"].Value);

                // Ustawienie ComboBoxów na wybrane wartości; może wymagać dodatkowej logiki
                // do znalezienia odpowiednich indeksów na podstawie wartości
                SetComboBoxSelectedItem(ResCourtCb, row.Cells["ResCourt"].Value.ToString());
                SetComboBoxSelectedItem(ResCustomerCb, row.Cells["ResCustomer"].Value.ToString());

                // Ustawienie wartości czasu trwania na podstawie ResStart i ResEnd
                var start = Convert.ToDateTime(row.Cells["ResStart"].Value);
                var end = Convert.ToDateTime(row.Cells["ResEnd"].Value);
                var duration = end - start;
                ResTimeCb.SelectedItem = duration.TotalHours.ToString();

                // Ustawienie TextBox na koszt rezerwacji
                ResCostTb.Text = row.Cells["ResCost"].Value.ToString();

                // Zapisanie wybranego ResID
                selectedResID = Convert.ToInt32(row.Cells["ResID"].Value);
            }
        }

        private void SetComboBoxSelectedItem(ComboBox comboBox, string value)
        {
            comboBox.SelectedIndex = comboBox.Items.IndexOf(value);
        }

        private void ResCancelBtn_Click(object sender, EventArgs e)
        {
            // Sprawdzenie, czy zaznaczono rezerwację do anulowania
            if (selectedResID <= 0)
            {
                MessageBox.Show("Proszę wybrać rezerwację do anulowania.");
                return;
            }

            // Okno dialogowe potwierdzenia
            if (MessageBox.Show("Czy na pewno chcesz anulować wybraną rezerwację?", "Anulowanie Rezerwacji", MessageBoxButtons.YesNo) == DialogResult.Yes)
            {
                try
                {
                    Con.Open();
                    SqlCommand cmd = new SqlCommand("DELETE FROM ReservationTbl WHERE ResID = @ResID", Con);
                    cmd.Parameters.AddWithValue("@ResID", selectedResID);
                    int rowsAffected = cmd.ExecuteNonQuery();

                    if (rowsAffected > 0)
                    {
                        MessageBox.Show("Rezerwacja została anulowana.");
                        // Odświeżanie DataGridView
                        populate();
                    }
                    else
                    {
                        MessageBox.Show("Nie udało się anulować rezerwacji.");
                    }
                }
                catch (Exception ex)
                {
                    MessageBox.Show("Wystąpił błąd podczas anulowania rezerwacji: " + ex.Message);
                }
                finally
                {
                    Con.Close();
                }
            }
        }

        private void panel5_Paint(object sender, PaintEventArgs e)
        {

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

        private void label10_Click(object sender, EventArgs e)
        {
            Customers customers = new Customers();
            customers.Show();
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
