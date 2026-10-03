using System;
using System.Drawing;
using System.Windows.Forms;

namespace Comfort
{
    public class WorkshopTile : UserControl
    {
        public int WorkshopID { get; set; }
        public event EventHandler<int> EditClicked;

        private Label lblTitle;
        private Label lblType;
        private Label lblWorkersCount;

        public WorkshopTile()
        {
            this.Size = new Size(200, 150);
            this.BorderStyle = BorderStyle.FixedSingle;
            this.BackColor = Color.White;
            this.Cursor = Cursors.Hand; // Показывает, что плитка кликабельна

            lblTitle = new Label { Location = new Point(10, 10), AutoSize = true };
            lblType = new Label { Location = new Point(10, 40), AutoSize = true };
            lblWorkersCount = new Label { Location = new Point(10, 70), AutoSize = true };

            this.Controls.Add(lblTitle);
            this.Controls.Add(lblType);
            this.Controls.Add(lblWorkersCount);

            // Обработчик клика по всей плитке
            this.Click += (s, e) => EditClicked?.Invoke(this, WorkshopID);
        }

        public void SetWorkshopData(string title, string type, int workersCount)
        {
            lblTitle.Text = title;
            lblType.Text = $"Тип: {type}";
            lblWorkersCount.Text = $"Рабочих: {workersCount}";
        }
    }
}