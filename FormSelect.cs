using System;
using System.Drawing;
using System.Windows.Forms;

namespace Comfort
{
    public partial class FormSelect : Form
    {
        public FormSelect()
        {
            InitializeComponent();
            this.BackgroundImage = Properties.Resources.ComfortBackground; // Фон
            this.BackgroundImageLayout = ImageLayout.Stretch;
            this.Icon = Properties.Resources.ComfortIcon; // Иконка 
            this.Size = new Size(400, 300); // Размер формы 

            Button btnProducts = new Button() // Продукция кнопка
            {
                Text = "Продукция",
                Location = new Point(100, 100),
                Size = new Size(200, 50),
                BackColor = Color.FromArgb(53, 92, 189),
                ForeColor = Color.White,
                Font = new Font("Candara", 12)
            };
            btnProducts.Click += (s, e) =>
            {
                this.Hide();
                new FormProducts().ShowDialog();
                this.Show();
            };

            Button btnWorkshops = new Button() // Цехи кнопка
            {
                Text = "Цехи",
                Location = new Point(100, 160),
                Size = new Size(200, 50),
                BackColor = Color.FromArgb(53, 92, 189),
                ForeColor = Color.White,
                Font = new Font("Candara", 12)
            };
            btnWorkshops.Click += (s, e) =>
            {
                this.Hide();
                new FormWorkshops().ShowDialog();
                this.Show();
            };

            this.Controls.Add(btnProducts);
            this.Controls.Add(btnWorkshops);
        }
    }
}