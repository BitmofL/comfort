using System;
using System.Data;
using System.Data.SqlClient;
using System.Windows.Forms;

namespace Comfort
{
    public partial class FormProducts : Form
    {
        private readonly string connectionString = "Data Source=(localdb)\MSSQLLocalDB;Initial Catalog=ComfortBD;Integrated Security=True";

        public FormProducts()
        {
            InitializeComponent();
            this.Icon = Properties.Resources.ComfortIcon; // Установка иконки
            LoadProducts();
        }

        // Загрузка списка продукции из базы данных
        private void LoadProducts()
        {
            try
            {
                using (SqlConnection conn = new SqlConnection(connectionString))
                {
                    // SQL-запрос для получения данных о продукции с расчетом времени изготовления
                    string query = @"
                SELECT p.ProductID, p.Article, pt.TypeName AS ProductType, p.ProductName, 
                       p.MinPartnerPrice, p.MainMaterial,
                       ISNULL(SUM(pw.TimeInWorkshop), 0) AS ManufacturingTime
                FROM Products p
                JOIN ProductTypes pt ON p.ProductTypeID = pt.ProductTypeID
                LEFT JOIN ProductWorkshops pw ON p.ProductID = pw.ProductID
                GROUP BY p.ProductID, p.Article, pt.TypeName, p.ProductName, p.MinPartnerPrice, p.MainMaterial";

                    SqlDataAdapter adapter = new SqlDataAdapter(query, conn);
                    DataTable dt = new DataTable();
                    adapter.Fill(dt);

                    flpProducts.Controls.Clear();

                    foreach (DataRow row in dt.Rows)
                    {
                        string title = $"{row["ProductType"]} | {row["ProductName"]}";
                        string article = row["Article"].ToString();
                        decimal price = (decimal)row["MinPartnerPrice"];
                        string material = row["MainMaterial"].ToString();
                        int manufacturingTime = Convert.ToInt32(row["ManufacturingTime"]);

                        ProductTile tile = new ProductTile();
                        tile.SetProductData(title, article, price, material, manufacturingTime);
                        tile.ProductID = (int)row["ProductID"];
                        tile.Margin = new Padding(5);
                        // Обработчик события для редактирования продукта при нажатии на элемент списка
                        tile.EditClicked += (s, productId) =>
                        {
                            using (FormProductEdit form = new FormProductEdit(productId))
                            {
                                if (form.ShowDialog() == DialogResult.OK)
                                {
                                    LoadProducts(); // Обновление списка после редактирования
                                }
                            }
                        };
                        flpProducts.Controls.Add(tile);
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Ошибка загрузки данных: {ex.Message}", "Ошибка", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void BtnAddProduct_Click(object sender, EventArgs e)
        {
            using (FormProductEdit form = new FormProductEdit())
            {
                if (form.ShowDialog() == DialogResult.OK)
                {
                    LoadProducts(); // Обновление списка продуктов после добавления
                }
            }
        }

        private void BtnBack_Click(object sender, EventArgs e)
        {
            this.Close(); // Закрытие формы
        }
    }
}