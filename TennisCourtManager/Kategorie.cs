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
    public partial class Kategorie : Form
    {
        public Kategorie()
        {
            InitializeComponent();
            populate();
        }

        private void label4_Click(object sender, EventArgs e)
        {
            Form1 Obj = new Form1();
            Obj.Show();
            this.Hide();
        }

        private void label16_Click(object sender, EventArgs e)
        {

        }

        private void textBox1_TextChanged(object sender, EventArgs e)
        {

        }

        SqlConnection Con = new SqlConnection(@"Data Source=(LocalDB)\MSSQLLocalDB;AttachDbFilename=C:\Users\wziel\OneDrive\Dokumenty\CourtsDbase.mdf;Integrated Security=True;Connect Timeout=30");

        private void populate()
        {
            Con.Open();
            string Querry = "select * from CatTbl";
            SqlDataAdapter sda = new SqlDataAdapter(Querry, Con);
            SqlCommandBuilder builder = new SqlCommandBuilder(sda);
            var ds = new DataSet();
            sda.Fill(ds);
            CatDGV.DataSource = ds.Tables[0];
            Con.Close();
        }
        private void InsertCat()
        {
            if (CatNameTb.Text == "" || CatCostTb.Text == "")
            {
                MessageBox.Show("Uzupełnij wszystkie pola!");
            }
            else
            {
                try
                {
                    Con.Open();
                    SqlCommand cmd = new SqlCommand("insert into CatTbl(CatName, CatCost) values(@CN, @CC)", Con);
                    cmd.Parameters.AddWithValue("@CN", CatNameTb.Text);
                    cmd.Parameters.AddWithValue("@CC", CatCostTb.Text);
                    cmd.ExecuteNonQuery();
                    MessageBox.Show("Kategoria została dodana");
                    Con.Close();
                    populate();
                }
                catch (Exception ex)
                {
                    MessageBox.Show(ex.Message);
                    Con.Close();
                }
            }
        }

        private void EditCat(int catID)
        {
            if (CatNameTb.Text == "" || CatCostTb.Text == "")
            {
                MessageBox.Show("Uzupełnij wszystkie pola!");
            }
            else
            {
                try
                {
                    Con.Open();
                    SqlCommand cmd = new SqlCommand("UPDATE CatTbl SET CatName = @CN, CatCost = @CC WHERE CatID = @CID", Con);
                    cmd.Parameters.AddWithValue("@CN", CatNameTb.Text);
                    cmd.Parameters.AddWithValue("@CC", CatCostTb.Text);
                    cmd.Parameters.AddWithValue("@CID", catID);
                    cmd.ExecuteNonQuery();
                    Con.Close();
                    MessageBox.Show("Kategoria została zaktualizowana");
                    populate();
                }
                catch (Exception ex)
                {
                    MessageBox.Show(ex.Message);
                    Con.Close();
                }
            }
        }

        private void DeleteCat(int catID)
        {
            // Sprawdzenie, czy kategoria jest już przypisana do kortu
            Con.Open();
            SqlCommand checkCmd = new SqlCommand("SELECT COUNT(*) FROM CourtTbl WHERE CNum = @CID", Con);
            checkCmd.Parameters.AddWithValue("@CID", catID);
            int count = Convert.ToInt32(checkCmd.ExecuteScalar());
            Con.Close();

            if (count > 0)
            {
                MessageBox.Show("Nie można usunąć tej kategorii, ponieważ jest przypisana do istniejącego kortu.");
                return;
            }

            if (MessageBox.Show("Czy na pewno chcesz usunąć tę kategorię?", "Usuwanie Kategorii", MessageBoxButtons.YesNo) == DialogResult.Yes)
            {
                try
                {
                    Con.Open();
                    SqlCommand cmd = new SqlCommand("DELETE FROM CatTbl WHERE CatID = @CID", Con);
                    cmd.Parameters.AddWithValue("@CID", catID);
                    cmd.ExecuteNonQuery();
                    Con.Close();
                    MessageBox.Show("Kategoria została usunięta");
                    populate();
                }
                catch (Exception ex)
                {
                    MessageBox.Show(ex.Message);
                    Con.Close();
                }
            }
        }

        int selectedCatID = 0;

        private void ClearFields()
        {
            CatNameTb.Text = string.Empty;
            CatCostTb.Text = string.Empty;
            selectedCatID = 0;
        }

        private void SaveBtn_Click(object sender, EventArgs e)
        {
            InsertCat();
        }

        private void panel5_Paint(object sender, PaintEventArgs e)
        {
        }

        private void CatDGV_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {
            if (e.RowIndex >= 0)
            {
                DataGridViewRow row = CatDGV.Rows[e.RowIndex];

                CatNameTb.Text = row.Cells["CatName"].Value.ToString();
                CatCostTb.Text = row.Cells["CatCost"].Value.ToString();

                selectedCatID = Convert.ToInt32(row.Cells["CatID"].Value);
            }
        }

        private void EditBtn_Click(object sender, EventArgs e)
        {
            if (selectedCatID > 0) // Sprawdź, czy jakieś ID zostało wybrane
            {
                EditCat(selectedCatID);
            }
            else
            {
                MessageBox.Show("Proszę najpierw wybrać kategorię do edycji.");
            }
        }

        private void DeleteBtn_Click(object sender, EventArgs e)
        {
            if (selectedCatID > 0) // Sprawdź, czy jakieś ID zostało wybrane
            {
                DeleteCat(selectedCatID);
            }
            else
            {
                MessageBox.Show("Proszę najpierw wybrać kategorię do edycji.");
            }
        }

        private void ClearBtn_Click(object sender, EventArgs e)
        {
            ClearFields();
        }
    }
}
