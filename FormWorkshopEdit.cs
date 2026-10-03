using System;
using System.Data.SqlClient;
using System.Drawing;
using System.Windows.Forms;

namespace Comfort
{

    /// Форма для добавления или редактирования информации о цехе в базе данных.
    public partial class FormWorkshopEdit : Form
    {
        // Идентификатор цеха (null, если создается новый цех)
        private int? workshopId;
        
        // Строка подключения к базе данных SQL Server
        private string connectionString = "Data Source=(localdb)\MSSQLLocalDB;Initial Catalog=ComfortBD;Integrated Security=True";
        
        // Поля ввода для данных о цехе
        private TextBox txtWorkshopName; // Поле для ввода названия цеха
        private TextBox txtWorkshopType; // Поле для ввода типа цеха
        private NumericUpDown nudWorkersCount; // Поле для ввода количества рабочих
        
        // Кнопки управления формой
        private Button btnSave; // Кнопка для сохранения данных
        private Button btnCancel; // Кнопка для отмены и закрытия формы

        /// Конструктор формы для создания или редактирования цеха.
        public FormWorkshopEdit(int? workshopId = null)
        {
            this.workshopId = workshopId; // Сохранение переданного идентификатора
            this.Icon = Properties.Resources.ComfortIcon; // Установка иконки формы из ресурсов проекта
            InitializeComponents(); // Инициализация всех элементов управления формы
            
            if (workshopId.HasValue)
            {
                LoadWorkshopData(workshopId.Value); // Загрузка данных для редактирования существующего цеха
                this.Text = "Редактирование цеха"; // Установка заголовка формы для редактирования
            }
            else
            {
                this.Text = "Добавление цеха"; // Установка заголовка формы для добавления
            }
        }


        /// Инициализация компонентов пользовательского интерфейса формы.
        private void InitializeComponents()
        {
            this.Size = new Size(400, 200); // Установка размеров формы
            this.Font = new Font("Candara", 10); // Установка шрифта формы
            this.BackColor = Color.White; // Установка белого фона формы
            this.StartPosition = FormStartPosition.CenterParent; // Центрирование формы относительно родителя

            // Создание меток и полей ввода
            Label lblName = new Label { Text = "Название цеха:", Location = new Point(10, 10), AutoSize = true };
            txtWorkshopName = new TextBox { Location = new Point(150, 10), Size = new Size(200, 20) };

            Label lblType = new Label { Text = "Тип цеха:", Location = new Point(10, 40), AutoSize = true };
            txtWorkshopType = new TextBox { Location = new Point(150, 40), Size = new Size(200, 20) };

            Label lblWorkers = new Label { Text = "Количество рабочих:", Location = new Point(10, 70), AutoSize = true };
            nudWorkersCount = new NumericUpDown { Location = new Point(150, 70), Size = new Size(200, 20), Minimum = 0, Maximum = 1000 };

            // Создание кнопки "Сохранить"
            btnSave = new Button
            {
                Text = "Сохранить",
                Location = new Point(10, 110),
                Size = new Size(100, 30),
                BackColor = Color.FromArgb(53, 92, 189), // Синий цвет фона кнопки
                ForeColor = Color.White, // Белый цвет текста
                Font = new Font("Candara", 10)
            };
            btnSave.Click += BtnSave_Click; // Привязка обработчика события нажатия кнопки

            // Создание кнопки "Отмена"
            btnCancel = new Button
            {
                Text = "Отмена",
                Location = new Point(120, 110),
                Size = new Size(100, 30),
                BackColor = Color.FromArgb(53, 92, 189), // Синий цвет фона кнопки
                ForeColor = Color.White, // Белый цвет текста
                Font = new Font("Candara", 10)
            };
            btnCancel.Click += BtnCancel_Click; // Привязка обработчика события нажатия кнопки

            // Добавление всех компонентов на форму
            this.Controls.AddRange(new Control[] { lblName, txtWorkshopName, lblType, txtWorkshopType, lblWorkers, nudWorkersCount, btnSave, btnCancel });
        }


        /// Загрузка данных о цехе из базы данных для редактирования.
        private void LoadWorkshopData(int workshopId)
        {
            try
            {
                using (SqlConnection conn = new SqlConnection(connectionString))
                {
                    conn.Open(); // Открытие подключения к базе данных
                    // SQL-запрос для выборки данных о цехе по его идентификатору
                    string query = "SELECT WorkshopName, WorkshopType, WorkersCount FROM Workshops WHERE WorkshopID = @WorkshopID";
                    using (SqlCommand cmd = new SqlCommand(query, conn))
                    {
                        cmd.Parameters.AddWithValue("@WorkshopID", workshopId); // Передача параметра в запрос
                        using (SqlDataReader reader = cmd.ExecuteReader())
                        {
                            if (reader.Read()) // Если данные найдены
                            {
                                // Заполнение полей формы значениями из базы данных
                                txtWorkshopName.Text = reader["WorkshopName"].ToString();
                                txtWorkshopType.Text = reader["WorkshopType"].ToString();
                                nudWorkersCount.Value = Convert.ToInt32(reader["WorkersCount"]);
                            }
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                // Обработка исключений при загрузке данных
                MessageBox.Show($"Ошибка загрузки данных цеха: {ex.Message}", "Ошибка", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }


        /// Проверка корректности введенных пользователем данных перед сохранением.
        /// Возвращает true, если данные прошли проверку, иначе false.
        private bool ValidateInputs()
        {
            if (string.IsNullOrWhiteSpace(txtWorkshopName.Text))
            {
                MessageBox.Show("Введите название цеха.", "Предупреждение", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return false; // Название цеха обязательно для заполнения
            }
            if (string.IsNullOrWhiteSpace(txtWorkshopType.Text))
            {
                MessageBox.Show("Введите тип цеха.", "Предупреждение", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return false; // Тип цеха обязателен для заполнения
            }
            if (nudWorkersCount.Value < 0)
            {
                MessageBox.Show("Количество рабочих не может быть отрицательным.", "Предупреждение", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return false; // Количество рабочих не может быть меньше нуля
            }
            return true; // Все проверки пройдены успешно
        }


        /// Обработчик события нажатия кнопки "Сохранить".
        /// Сохраняет данные о цехе в базу данных.
        private void BtnSave_Click(object sender, EventArgs e)
        {
            if (!ValidateInputs()) return; // Прерывание, если данные некорректны

            try
            {
                using (SqlConnection conn = new SqlConnection(connectionString))
                {
                    conn.Open(); // Открытие подключения к базе данных
                    string query;
                    if (workshopId.HasValue)
                    {
                        // SQL-запрос для обновления данных существующего цеха
                        query = @"UPDATE Workshops SET WorkshopName = @WorkshopName, WorkshopType = @WorkshopType, WorkersCount = @WorkersCount WHERE WorkshopID = @WorkshopID";
                    }
                    else
                    {
                        // SQL-запрос для вставки данных о новом цехе
                        query = @"INSERT INTO Workshops (WorkshopName, WorkshopType, WorkersCount) VALUES (@WorkshopName, @WorkshopType, @WorkersCount)";
                    }

                    using (SqlCommand cmd = new SqlCommand(query, conn))
                    {
                        // Передача параметров в SQL-запрос
                        cmd.Parameters.AddWithValue("@WorkshopName", txtWorkshopName.Text.Trim());
                        cmd.Parameters.AddWithValue("@WorkshopType", txtWorkshopType.Text.Trim());
                        cmd.Parameters.AddWithValue("@WorkersCount", nudWorkersCount.Value);
                        if (workshopId.HasValue)
                            cmd.Parameters.AddWithValue("@WorkshopID", workshopId.Value); // Указание ID для обновления

                        cmd.ExecuteNonQuery(); // Выполнение запроса
                    }
                }
                // Уведомление об успешном сохранении данных
                MessageBox.Show("Данные успешно сохранены.", "Информация", MessageBoxButtons.OK, MessageBoxIcon.Information);
                this.DialogResult = DialogResult.OK; // Установка результата диалога
                this.Close(); // Закрытие
            }
            catch (Exception ex)
            {
                // Обработка исключений при сохранении данных
                MessageBox.Show($"Ошибка сохранения данных: {ex.Message}", "Ошибка", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }


        /// Обработчик события нажатия кнопки "Отмена".
        /// Закрывает форму без сохранения данных.
        private void BtnCancel_Click(object sender, EventArgs e)
        {
            this.DialogResult = DialogResult.Cancel; // Установка результата диалога как "Отмена"
            this.Close(); // Закрытие
        }
    }
}