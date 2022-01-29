using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Data.OleDb;
using System.Data.SqlClient;

namespace Exporttst
{
    public partial class Form1 : Form
    {
        DataTable dt = new DataTable();

        public Form1()
        {
            InitializeComponent();
        }

        private void btnGozat_Click(object sender, EventArgs e)
        {
            //////////////////////////////////////////////////
            openFileDialog1.ShowDialog();
            txtDosyaYolu.Text = openFileDialog1.FileName;
            string dosyaYolu = openFileDialog1.FileName;
            OleDbConnection baglantii = new OleDbConnection("Provider=Microsoft.ACE.OLEDB.12.0; Data Source=" + dosyaYolu + ";Extended Properties=Excel 12.0");
            baglantii.Open();
            string sorgu = "select * from [Sayfa1$]";
            OleDbDataAdapter data_adaptor = new OleDbDataAdapter(sorgu, baglantii);
            baglantii.Close();
           
            data_adaptor.Fill(dt);
            //////////////////////////////////////////////////
        }




        private void btnAktar_Click(object sender, EventArgs e)
        {


            SqlConnection baglan = new SqlConnection("Data Source=.; Initial Catalog=.; User Id=.; password=.");

            


            for (int i = 0; i < dt.Rows.Count - 1; i++)
            {

                //////////////////////////////////////////////////

                SqlCommand cmd2 = new SqlCommand();
                baglan.Open();
                cmd2.Connection = baglan;

                cmd2.CommandText = "select ProductId from  Product where ProductCode = '" + dt.Rows[i][0].ToString() + "'";
                int productid = (int)cmd2.ExecuteScalar();
                cmd2.Dispose();
                baglan.Close();
                //////////////////////////////////////////////////





                //////////////////////////////////////////////////
                SqlCommand cmd3 = new SqlCommand();
                baglan.Open();
                cmd3.Connection = baglan;
                cmd3.CommandText = "select StartDate from  Period where Title = '" + comboBox1.Text + "'";              
                DateTime startdate = (DateTime)cmd3.ExecuteScalar();
                cmd3.Dispose();
                baglan.Close();
                //////////////////////////////////////////////////
                //////////////////////////////////////////////////
                SqlCommand cmd8 = new SqlCommand();
                baglan.Open();
                cmd8.Connection = baglan;
                cmd8.CommandText = "select Enddate from  Period where Title = '" + comboBox1.Text + "'";
                DateTime Enddate = (DateTime)cmd8.ExecuteScalar();
                cmd8.Dispose();
                baglan.Close();
                //////////////////////////////////////////////////


                //////////////////////////////////////////////////
                int CurrencyType = 53;
                int CompanyId = 2;
                string CreatedDate = "2010 - 10 - 10 00:00:00.000";
                string CreatedUser = "Muhammed";
                int s = (Byte)1;
                //////////////////////////////////////////////////

                //////////////////////////////////////////////////
                SqlCommand cmd = new SqlCommand();
                baglan.Open();
                cmd.Connection = baglan;
                cmd.CommandText = "insert into ProductPrice(ProductId,CompanyId,CurrencyType,CreatedDate,CreatedUser,IsDeleted,StartDate,EndDate,CostPrice,Price,MarketingPrice,CatalogPrice,RemitDiscountRate,Point) values(@ProductId,@CompanyId,@CurrencyType,@CreatedDate,@CreatedUser,@IsDeleted,@StartDate,@EndDate,@CostPrice,@Price,@MarketingPrice,@CatalogPrice,@RemitDiscountRate,@Point)";
                cmd.Parameters.Add("@CompanyId", CompanyId);
                cmd.Parameters.Add("@ProductId", productid);
                cmd.Parameters.Add("@CurrencyType", CurrencyType);
                cmd.Parameters.Add("@CostPrice", decimal.Parse(dt.Rows[i][2].ToString()));
                cmd.Parameters.Add("@Price", decimal.Parse(dt.Rows[i][3].ToString()));
                cmd.Parameters.Add("@MarketingPrice", decimal.Parse(dt.Rows[i][4].ToString()));
                cmd.Parameters.Add("@CatalogPrice", decimal.Parse(dt.Rows[i][5].ToString()));
                cmd.Parameters.Add("@RemitDiscountRate", decimal.Parse(dt.Rows[i][6].ToString()));
                cmd.Parameters.Add("@Point", decimal.Parse(dt.Rows[i][7].ToString()));
                cmd.Parameters.Add("@CreatedUser", CreatedUser);
                cmd.Parameters.Add("@StartDate", startdate);
                cmd.Parameters.Add("@EndDate", Enddate);
                cmd.Parameters.Add("@IsDeleted", s);
                cmd.Parameters.Add("@CreatedDate", CreatedDate);
                cmd.ExecuteNonQuery();
                cmd.Dispose();
                baglan.Close();
                //////////////////////////////////////////////////


                //////////////////////////////////////////////////
                SqlCommand cmd5 = new SqlCommand();
                baglan.Open();
                cmd5.Connection = baglan;
                cmd5.CommandText = "UPDATE Product set Point=@Point,TaxRate=@TaxRate,CatalogPageNumber=@CatalogPageNumber  where ProductId = '" + productid + "'";                   
                cmd5.Parameters.Add("@Point", decimal.Parse(dt.Rows[i][7].ToString()));
                cmd5.Parameters.Add("@TaxRate", decimal.Parse(dt.Rows[i][8].ToString()));
                cmd5.Parameters.Add("@CatalogPageNumber", decimal.Parse(dt.Rows[i][9].ToString()));
                cmd5.ExecuteNonQuery();
                cmd.Dispose();
                baglan.Close();
                //////////////////////////////////////////////////


            }

            MessageBox.Show("İşleminiz Başarıyla gerçekleşmiştir");
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            //////////////////////////////////////////////////
            SqlConnection baglanti = new SqlConnection();
            baglanti.ConnectionString = "Data Source=.; Initial Catalog=.; User Id=.; password=.";
            SqlCommand komut = new SqlCommand();
            komut.CommandText = "SELECT * FROM Period";
            komut.Connection = baglanti;
            komut.CommandType = CommandType.Text;

            SqlDataReader dr;
            baglanti.Open();
            dr = komut.ExecuteReader();
            while (dr.Read())
            {
                comboBox1.Items.Add(dr["Title"]);
            }
            baglanti.Close();
            //////////////////////////////////////////////////
        }

        private void dtGoster_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {

        }
    }
}
        
