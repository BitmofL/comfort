using System;
using System.Data;
using System.Data.SqlClient;
using System.Drawing;
using System.Windows.Forms;

namespace Comfort
{
    public partial class FormWorkshops : Form
    {
        private readonly string connectionString = "Data Source=(localdb)\MSSQLLocalDB;Initial Catalog=ComfortBD;Integrated Security=True";
        private readonly ToolTip toolTip; // Добавление ToolTip выноски

        public FormWorkshops()
        {
            InitializeComponent();
            this.Icon = Properties.Resources.ComfortIcon; // Установка иконки
            toolTip = new ToolTip(); // Инициализация выноски ToolTip
            LoadWorkshops();
        }

        private void LoadWorkshops()
        {
            try
            {
                using (SqlConnection conn = new SqlConnection(connectionString))
                {
                    string query = "SELECT WorkshopID, WorkshopName, WorkshopType, WorkersCount FROM Workshops";
                    SqlDataAdapter adapter = new SqlDataAdapter(query, conn);
                    DataTable dt = new DataTable();
                    adapter.Fill(dt);

                    flpWorkshops.Controls.Clear();

                    foreach (DataRow row in dt.Rows)
                    {
                        string title = row["WorkshopName"].ToString();
                        string type = row["WorkshopType"].ToString();
                        int workersCount = Convert.ToInt32(row["WorkersCount"]);

                        WorkshopTile tile = new WorkshopTile();
                        tile.SetWorkshopData(title, type, workersCount);
                        tile.WorkshopID = (int)row["WorkshopID"];
                        tile.Margin = new Padding(5);
                        // Выноска названия
                        toolTip.SetToolTip(tile, title);
                        tile.EditClicked += (s, workshopId) =>
                        {
                            using (FormWorkshopEdit form = new FormWorkshopEdit(workshopId))
                            {
                                if (form.ShowDialog() == DialogResult.OK)
                                {
                                    LoadWorkshops(); // Обновление списка после редактирования
                                }
                            }
                        };
                        flpWorkshops.Controls.Add(tile);
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Ошибка загрузки данных: {ex.Message}", "Ошибка", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void BtnAddWorkshop_Click(object sender, EventArgs e)
        {
            using (FormWorkshopEdit form = new FormWorkshopEdit())
            {
                if (form.ShowDialog() == DialogResult.OK)
                {
                    LoadWorkshops(); // Обновление списка после добавления
                }
            }
        }

        private void BtnBack_Click(object sender, EventArgs e)
        {
            this.Close(); // Закрытие формы
        }
    }
}