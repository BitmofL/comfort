using System;
using System.Drawing;
using System.Windows.Forms;

namespace Comfort
{
    public class ProductTile : Panel
    {
        private TableLayoutPanel tlpMain;
        private Panel leftPanel;
        private Panel rightPanel;
        private FlowLayoutPanel flpLeft;
        private FlowLayoutPanel flpRight;
        private Label lblTitle;
        private Label lblArticle;
        private Label lblPrice;
        private Label lblMaterial;
        private Label lblManufacturingTimeLabel;
        private Label lblManufacturingTimeValue;

        public int ProductID { get; set; }
        public event EventHandler<int> EditClicked;

        public ProductTile()
        {
            this.BorderStyle = BorderStyle.FixedSingle;
            this.BackColor = Color.White;
            this.Width = 740; // Регулировано в соответствии с FlowLayoutPanel с помощью Scrollbar
            this.Height = 120;

            tlpMain = new TableLayoutPanel() { Dock = DockStyle.Fill, ColumnCount = 2, RowCount = 1 };
            tlpMain.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 60F));
            tlpMain.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 40F));

            leftPanel = new Panel() { Dock = DockStyle.Fill };
            rightPanel = new Panel() { Dock = DockStyle.Fill };

            tlpMain.Controls.Add(leftPanel, 0, 0);
            tlpMain.Controls.Add(rightPanel, 1, 0);
            this.Controls.Add(tlpMain);

            // Левая панель с FlowLayoutPanel для укладки меток
            flpLeft = new FlowLayoutPanel() { Dock = DockStyle.Fill, FlowDirection = FlowDirection.TopDown };
            leftPanel.Controls.Add(flpLeft);

            lblTitle = new Label() { Font = new Font("Candara", 12, FontStyle.Bold), AutoSize = true, Margin = new Padding(5) };
            flpLeft.Controls.Add(lblTitle);

            lblArticle = new Label() { AutoSize = true, Margin = new Padding(5) };
            flpLeft.Controls.Add(lblArticle);

            lblPrice = new Label() { AutoSize = true, Margin = new Padding(5) };
            flpLeft.Controls.Add(lblPrice);

            lblMaterial = new Label() { AutoSize = true, Margin = new Padding(5) };
            flpLeft.Controls.Add(lblMaterial);

            // Правая панель с FlowLayoutPanel для укладки меток
            flpRight = new FlowLayoutPanel() { Dock = DockStyle.Fill, FlowDirection = FlowDirection.TopDown };
            rightPanel.Controls.Add(flpRight);

            lblManufacturingTimeLabel = new Label() { Text = "Время изготовления:", AutoSize = true, Margin = new Padding(5), TextAlign = ContentAlignment.MiddleRight };
            flpRight.Controls.Add(lblManufacturingTimeLabel);

            lblManufacturingTimeValue = new Label() { AutoSize = true, Margin = new Padding(5), TextAlign = ContentAlignment.MiddleRight };
            flpRight.Controls.Add(lblManufacturingTimeValue);

            // Сделать плитку кликабельной
            SetClickEvent(this);
        }

        private void SetClickEvent(Control control)
        {
            control.Click += ProductTile_Click;
            foreach (Control c in control.Controls)
            {
                SetClickEvent(c);
            }
        }

        private void ProductTile_Click(object sender, EventArgs e)
        {
            EditClicked?.Invoke(this, ProductID);
        }

        public void SetProductData(string title, string article, decimal price, string material, int manufacturingTime)
        {
            lblTitle.Text = title;
            lblArticle.Text = $"Артикул: {article}";
            lblPrice.Text = $"Мин. стоимость для партнера: {price:C}";
            lblMaterial.Text = $"Основной материал: {material}";
            lblManufacturingTimeValue.Text = manufacturingTime.ToString();
        }
    }
}