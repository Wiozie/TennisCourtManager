using System.Data;
using System.Data.SqlClient;

namespace TennisCourtManager
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
            LoadComboBoxData();
            populate();
        }

        SqlConnection Con = new SqlConnection(@"Data Source=(LocalDB)\MSSQLLocalDB;AttachDbFilename=C:\Users\wziel\OneDrive\Dokumenty\CourtsDbase.mdf;Integrated Security=True;Connect Timeout=30");

        private void LoadComboBoxData()
        {
            Con.Open();
            try
            {
                SqlCommand cmd = new SqlCommand("SELECT CatName FROM CatTbl", Con);
                SqlDataReader reader = cmd.ExecuteReader();
                CTypeCb.Items.Clear(); // Czyszczenie ComboBox

                while (reader.Read())
                {
                    string sName = reader.GetString("CatName");
                    CTypeCb.Items.Add(sName);
                }

                reader.Close();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
            Con.Close();
        }

        private void label1_Click(object sender, EventArgs e)
        {

        }

        private void panel1_Paint(object sender, PaintEventArgs e)
        {

        }

        private void label4_Click(object sender, EventArgs e)
        {

        }

        private void label15_Click(object sender, EventArgs e)
        {

        }

        private void label16_Click(object sender, EventArgs e)
        {

        }

        private void textBox1_TextChanged(object sender, EventArgs e)
        {

        }

        private void populate()
        {
            Con.Open();
            string Querry = "SELECT CourtTbl.CNum, CourtTbl.CName, CatTbl.CatName AS CType, CourtTbl.CStatus FROM CourtTbl JOIN CatTbl ON CourtTbl.CType = CatTbl.CatID";
            SqlDataAdapter sda = new SqlDataAdapter(Querry, Con);
            SqlCommandBuilder builder = new SqlCommandBuilder(sda);
            var ds = new DataSet();
            sda.Fill(ds);
            CourtsDGV.DataSource = ds.Tables[0];
            Con.Close();
        }

        private int GetCatID(string catName)
        {
            int catID = -1;
            Con.Open();
            SqlCommand cmd = new SqlCommand("SELECT CatID FROM CatTbl WHERE CatName = @CatName", Con);
            cmd.Parameters.AddWithValue("@CatName", catName);

            SqlDataReader reader = cmd.ExecuteReader();
            if (reader.Read())
            {
                catID = Convert.ToInt32(reader["CatID"]);
            }
            reader.Close();
            Con.Close();
            return catID;
        }

        private void InsertCourt()
        {
            if (CNameTb.Text == "" || CTypeCb.SelectedIndex == -1 || CStatusCb.SelectedItem == null)
            {
                MessageBox.Show("Uzupe³nij wszystkie pola!");
            }
            else
            {
                try
                {
                    int catID = GetCatID(CTypeCb.SelectedItem.ToString());
                    if (catID != -1)
                    {
                        Con.Open();
                        SqlCommand cmd = new SqlCommand("insert into CourtTbl(CName, CType, CStatus) values(@CN, @CT, @CS)", Con);
                        cmd.Parameters.AddWithValue("@CN", CNameTb.Text);
                        cmd.Parameters.AddWithValue("@CT", catID);
                        cmd.Parameters.AddWithValue("@CS", CStatusCb.SelectedItem.ToString());
                        cmd.ExecuteNonQuery();
                        Con.Close();
                        MessageBox.Show("Kort zosta³ dodany poprawnie!");
                        populate();
                    }
                    else
                    {
                        MessageBox.Show("Nie znaleziono odpowiedniego CatID.");
                    }
                }
                catch (Exception ex)
                {
                    MessageBox.Show(ex.Message);
                    if (Con.State == ConnectionState.Open)
                        Con.Close();
                }
            }
        }

        private void EditCourt(int courtID)
        {
            if (CNameTb.Text == "" || CTypeCb.SelectedIndex == -1 || CStatusCb.SelectedItem == null)
            {
                MessageBox.Show("Uzupe³nij wszystkie pola!");
            }
            else
            {
                try
                {
                    int catID = GetCatID(CTypeCb.SelectedItem.ToString());
                    if (catID != -1)
                    {
                        Con.Open();
                        SqlCommand cmd = new SqlCommand("UPDATE CourtTbl SET CName = @CN, CType = @CT, CStatus = @CS WHERE CNum = @CID", Con);
                        cmd.Parameters.AddWithValue("@CN", CNameTb.Text);
                        cmd.Parameters.AddWithValue("@CT", catID);
                        cmd.Parameters.AddWithValue("@CS", CStatusCb.SelectedItem.ToString());
                        cmd.Parameters.AddWithValue("@CID", courtID);
                        cmd.ExecuteNonQuery();
                        Con.Close();
                        MessageBox.Show("Kort zosta³ zaktualizowany poprawnie!");
                        populate();
                    }
                    else
                    {
                        MessageBox.Show("Nie znaleziono odpowiedniego CatID.");
                    }
                }
                catch (Exception ex)
                {
                    MessageBox.Show(ex.Message);
                    if (Con.State == ConnectionState.Open)
                        Con.Close();
                }
            }
        }

        private void DeleteCourt(int courtID)
        {
            if (MessageBox.Show("Czy na pewno chcesz usun¹æ ten kort?", "Usuwanie Kortu", MessageBoxButtons.YesNo) == DialogResult.Yes)
            {
                try
                {
                    Con.Open();
                    SqlCommand cmd = new SqlCommand("DELETE FROM CourtTbl WHERE CNum = @CID", Con);
                    cmd.Parameters.AddWithValue("@CID", courtID);
                    cmd.ExecuteNonQuery();
                    Con.Close();
                    MessageBox.Show("Kort zosta³ usuniêty poprawnie!");
                    populate();
                }
                catch (Exception ex)
                {
                    MessageBox.Show(ex.Message);
                    if (Con.State == ConnectionState.Open)
                        Con.Close();
                }
            }
        }


        private void SaveBtn_Click(object sender, EventArgs e)
        {
            InsertCourt();
        }

        private void CTypeCb_SelectedIndexChanged(object sender, EventArgs e)
        {

        }

        int Key = 0;

        private void CourtsDGV_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {
            if (e.RowIndex >= 0)
            {
                DataGridViewRow row = CourtsDGV.Rows[e.RowIndex];

                CNameTb.Text = row.Cells["CName"].Value.ToString();
                CTypeCb.Text = row.Cells["CType"].Value.ToString();
                CStatusCb.Text = row.Cells["CStatus"].Value.ToString();

                Key = Convert.ToInt32(row.Cells["CNum"].Value);
            }
        }

        private void EditBtn_Click(object sender, EventArgs e)
        {
            if (Key > 0) // SprawdŸ, czy jakieœ ID zosta³o wybrane
            {
                EditCourt(Key);
            }
            else
            {
                MessageBox.Show("Proszê najpierw wybraæ kort do edycji.");
            }
        }

        private void DeleteBtn_Click(object sender, EventArgs e)
        {
            if (Key > 0) // SprawdŸ, czy jakieœ ID zosta³o wybrane
            {
                DeleteCourt(Key);
            }
            else
            {
                MessageBox.Show("Proszê najpierw wybraæ kort do edycji.");
            }
        }

        private void ClearBtn_Click(object sender, EventArgs e)
        {
            CNameTb.Text = string.Empty;

            if (CTypeCb.Items.Count > 0)
                CTypeCb.SelectedIndex = -1;
            if (CStatusCb.Items.Count > 0)
                CStatusCb.SelectedIndex = -1;

            Key = 0;
        }

        private void Label6_Click(object sender, EventArgs e)
        {
            Kategorie Obj = new Kategorie();
            Obj.Show();
            this.Hide();
        }

        private void label8_Click(object sender, EventArgs e)
        {
            Users Obj = new Users();
            Obj.Show();
            this.Hide();
        }

        private void label10_Click(object sender, EventArgs e)
        {
            Customers Obj = new Customers();
            Obj.Show();
            this.Hide();
        }

        private void label12_Click(object sender, EventArgs e)
        {
            Reservations Obj = new Reservations();
            Obj.Show();
            this.Hide();
        }
    }
}
