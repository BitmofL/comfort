using System;
using System.Data;
using System.Data.SqlClient;
using System.Drawing;
using System.Windows.Forms;

namespace Comfort
{
    public partial class FormProductEdit : Form
    {
        private TextBox txtArticle;
        private ComboBox cmbProductType;
        private TextBox txtProductName;
        private NumericUpDown nudMinPartnerPrice; // Замена TextBox на NumericUpDown
        private TextBox txtMainMaterial;
        private Button btnSave;
        private Button btnCancel;
        private ToolTip toolTip; // Добавлено для подсказок

        private int? productId = null;
        private string connectionString = "Data Source=(localdb)\MSSQLLocalDB;Initial Catalog=ComfortBD;Integrated Security=True";

        public FormProductEdit(int? productId = null)
        {
            this.productId = productId;
            InitializeCustomComponents();
            LoadProductTypes();
            this.Icon = Properties.Resources.ComfortIcon; // Установка иконки

            if (productId.HasValue)
            {
                LoadProductData(productId.Value);
                this.Text = "Редактирование продукта";
            }
            else
            {
                this.Text = "Добавление продукта";
            }
        }

        // Инициализация компонентов формы со стилями и подсказками
        private void InitializeCustomComponents()
        {
            this.Size = new Size(450, 350);
            this.Font = new Font("Candara", 10);
            this.BackColor = Color.White;

            toolTip = new ToolTip();

            Label lblArticle = new Label() { Text = "Артикул:", Location = new Point(10, 20), AutoSize = true };
            txtArticle = new TextBox() { Location = new Point(220, 20), Width = 200 };
            toolTip.SetToolTip(txtArticle, "Введите артикул продукта");

            Label lblProductType = new Label() { Text = "Тип продукта:", Location = new Point(10, 60), AutoSize = true };
            cmbProductType = new ComboBox() { Location = new Point(220, 60), Width = 200, DropDownStyle = ComboBoxStyle.DropDownList };
            toolTip.SetToolTip(cmbProductType, "Выберите тип продукта");

            Label lblProductName = new Label() { Text = "Наименование:", Location = new Point(10, 100), AutoSize = true };
            txtProductName = new TextBox() { Location = new Point(220, 100), Width = 200 };
            toolTip.SetToolTip(txtProductName, "Введите наименование продукта");

            Label lblMinPartnerPrice = new Label() { Text = "Мин. стоимость для партнера:", Location = new Point(10, 140), AutoSize = true };
            nudMinPartnerPrice = new NumericUpDown() { Location = new Point(220, 140), Width = 200, DecimalPlaces = 2, Minimum = 0, Maximum = 1000000 };
            toolTip.SetToolTip(nudMinPartnerPrice, "Введите минимальную стоимость (неотрицательное число с точностью до сотых)");

            Label lblMainMaterial = new Label() { Text = "Основной материал:", Location = new Point(10, 180), AutoSize = true };
            txtMainMaterial = new TextBox() { Location = new Point(220, 180), Width = 200 };
            toolTip.SetToolTip(txtMainMaterial, "Введите основной материал");

            btnSave = new Button() { Text = "Сохранить", Location = new Point(150, 230), Width = 90, BackColor = Color.FromArgb(0x35, 0x5C, 0xBD), ForeColor = Color.White };
            btnSave.Click += BtnSave_Click;

            btnCancel = new Button() { Text = "Отмена", Location = new Point(260, 230), Width = 90 };
            btnCancel.Click += (s, e) => this.Close();

            this.Controls.AddRange(new Control[] { lblArticle, txtArticle, lblProductType, cmbProductType, lblProductName, txtProductName,
                lblMinPartnerPrice, nudMinPartnerPrice, lblMainMaterial, txtMainMaterial, btnSave, btnCancel });
        }

        private void LoadProductTypes()
        {
            try
            {
                using (SqlConnection conn = new SqlConnection(connectionString))
                {
                    string query = "SELECT ProductTypeID, TypeName FROM ProductTypes";
                    SqlDataAdapter adapter = new SqlDataAdapter(query, conn);
                    DataTable dt = new DataTable();
                    adapter.Fill(dt);

                    cmbProductType.DataSource = dt;
                    cmbProductType.DisplayMember = "TypeName";
                    cmbProductType.ValueMember = "ProductTypeID";
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Ошибка загрузки типов продукции: {ex.Message}", "Ошибка", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void LoadProductData(int productId)
        {
            try
            {
                using (SqlConnection conn = new SqlConnection(connectionString))
                {
                    string query = "SELECT Article, ProductTypeID, ProductName, MinPartnerPrice, MainMaterial FROM Products WHERE ProductID = @ProductID";
                    SqlCommand cmd = new SqlCommand(query, conn);
                    cmd.Parameters.AddWithValue("@ProductID", productId);
                    conn.Open();
                    SqlDataReader reader = cmd.ExecuteReader();
                    if (reader.Read())
                    {
                        txtArticle.Text = reader["Article"].ToString();
                        cmbProductType.SelectedValue = (int)reader["ProductTypeID"];
                        txtProductName.Text = reader["ProductName"].ToString();
                        nudMinPartnerPrice.Value = (decimal)reader["MinPartnerPrice"];
                        txtMainMaterial.Text = reader["MainMaterial"].ToString();
                    }
                    conn.Close();
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Ошибка загрузки данных продукта: {ex.Message}", "Ошибка", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        // Сохранение данных продукта в базу данных
        private void BtnSave_Click(object sender, EventArgs e)
        {
            if (!ValidateInputs())
                return;

            try
            {
                using (SqlConnection conn = new SqlConnection(connectionString))
                {
                    // Выбор запроса: обновление или вставка в зависимости от наличия productId
                    string query = productId.HasValue ?
                        "UPDATE Products SET Article = @Article, ProductTypeID = @ProductTypeID, ProductName = @ProductName, MinPartnerPrice = @MinPartnerPrice, MainMaterial = @MainMaterial WHERE ProductID = @ProductID" :
                        "INSERT INTO Products (Article, ProductTypeID, ProductName, MinPartnerPrice, MainMaterial) VALUES (@Article, @ProductTypeID, @ProductName, @MinPartnerPrice, @MainMaterial)";

                    SqlCommand cmd = new SqlCommand(query, conn);
                    cmd.Parameters.AddWithValue("@Article", txtArticle.Text.Trim());
                    cmd.Parameters.AddWithValue("@ProductTypeID", (int)cmbProductType.SelectedValue);
                    cmd.Parameters.AddWithValue("@ProductName", txtProductName.Text.Trim());
                    cmd.Parameters.AddWithValue("@MinPartnerPrice", nudMinPartnerPrice.Value);
                    cmd.Parameters.AddWithValue("@MainMaterial", txtMainMaterial.Text.Trim());
                    if (productId.HasValue) cmd.Parameters.AddWithValue("@ProductID", productId.Value);

                    conn.Open();
                    cmd.ExecuteNonQuery();
                    conn.Close();

                    MessageBox.Show("Данные успешно сохранены.", "Информация", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    this.DialogResult = DialogResult.OK;
                    this.Close();
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Ошибка сохранения данных: {ex.Message}", "Ошибка", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        // Проверка введенных данных перед сохранением
        private bool ValidateInputs()
        {
            if (string.IsNullOrWhiteSpace(txtArticle.Text))
            {
                MessageBox.Show("Введите артикул.", "Ошибка", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return false;
            }
            if (cmbProductType.SelectedIndex < 0)
            {
                MessageBox.Show("Выберите тип продукта.", "Ошибка", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return false;
            }
            if (string.IsNullOrWhiteSpace(txtProductName.Text))
            {
                MessageBox.Show("Введите наименование продукта.", "Ошибка", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return false;
            }
            if (string.IsNullOrWhiteSpace(txtMainMaterial.Text))
            {
                MessageBox.Show("Введите основной материал.", "Ошибка", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return false;
            }
            // Нет необходимости проверять nudMinPartnerPrice, так как NumericUpDown гарантирует корректные значения
            return true;
        }

        private void InitializeComponent()
        {
            this.SuspendLayout();
            // 
            // FormProductEdit
            // 
            this.ClientSize = new System.Drawing.Size(284, 261);
            this.Name = "FormProductEdit";
            this.ResumeLayout(false);
        }
    }
}