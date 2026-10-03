namespace Comfort
{
    partial class FormProducts
    {
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Освободить все используемые ресурсы.
        /// </summary>
        /// <param name="disposing">истинно, если управляемые ресурсы должны быть освобождены; иначе, ложно.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        /// <summary>
        /// Обязательный метод для поддержки конструктора - не изменяйте
        /// содержимое данного метода с помощью редактора кода.
        /// </summary>
        private void InitializeComponent()
        {
            this.flpProducts = new System.Windows.Forms.FlowLayoutPanel();
            this.btnAddProduct = new System.Windows.Forms.Button();
            this.btnBack = new System.Windows.Forms.Button();
            this.SuspendLayout();
            // 
            // flpProducts
            // 
            this.flpProducts.FlowDirection = System.Windows.Forms.FlowDirection.TopDown;
            this.flpProducts.Location = new System.Drawing.Point(10, 70);
            this.flpProducts.Name = "flpProducts";
            this.flpProducts.Size = new System.Drawing.Size(760, 400);
            this.flpProducts.AutoScroll = true;
            this.flpProducts.WrapContents = false; // Prevent horizontal wrapping
            // 
            // btnAddProduct
            // 
            this.btnAddProduct.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(53)))), ((int)(((byte)(92)))), ((int)(((byte)(189)))));
            this.btnAddProduct.ForeColor = System.Drawing.Color.White;
            this.btnAddProduct.Location = new System.Drawing.Point(10, 480);
            this.btnAddProduct.Name = "btnAddProduct";
            this.btnAddProduct.Size = new System.Drawing.Size(75, 23);
            this.btnAddProduct.TabIndex = 1;
            this.btnAddProduct.Text = "Добавить";
            this.btnAddProduct.UseVisualStyleBackColor = false;
            this.btnAddProduct.Click += new System.EventHandler(this.BtnAddProduct_Click);
            // 
            // btnBack
            // 
            this.btnBack.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(53)))), ((int)(((byte)(92)))), ((int)(((byte)(189)))));
            this.btnBack.ForeColor = System.Drawing.Color.White;
            this.btnBack.Location = new System.Drawing.Point(100, 480);
            this.btnBack.Name = "btnBack";
            this.btnBack.Size = new System.Drawing.Size(75, 23);
            this.btnBack.TabIndex = 2;
            this.btnBack.Text = "Назад";
            this.btnBack.UseVisualStyleBackColor = false;
            this.btnBack.Click += new System.EventHandler(this.BtnBack_Click);
            // 
            // FormProducts
            // 
            this.BackColor = System.Drawing.Color.White;
            this.ClientSize = new System.Drawing.Size(784, 561);
            this.Controls.Add(this.btnBack);
            this.Controls.Add(this.btnAddProduct);
            this.Controls.Add(this.flpProducts);
            this.Name = "FormProducts";
            this.Text = "Список продукции";
            this.ResumeLayout(false);
        }

        private System.Windows.Forms.FlowLayoutPanel flpProducts;
        private System.Windows.Forms.Button btnAddProduct;
        private System.Windows.Forms.Button btnBack;
    }
}