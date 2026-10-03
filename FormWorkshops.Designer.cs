namespace Comfort
{
    partial class FormWorkshops
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
            this.flpWorkshops = new System.Windows.Forms.FlowLayoutPanel();
            this.btnAddWorkshop = new System.Windows.Forms.Button();
            this.btnBack = new System.Windows.Forms.Button();
            this.SuspendLayout();
            // 
            // flpWorkshops
            // 
            this.flpWorkshops.FlowDirection = System.Windows.Forms.FlowDirection.LeftToRight;
            this.flpWorkshops.Location = new System.Drawing.Point(10, 70);
            this.flpWorkshops.Name = "flpWorkshops";
            this.flpWorkshops.Size = new System.Drawing.Size(760, 400);
            this.flpWorkshops.AutoScroll = true;
            this.flpWorkshops.WrapContents = true;
            // 
            // btnAddWorkshop
            // 
            this.btnAddWorkshop.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(53)))), ((int)(((byte)(92)))), ((int)(((byte)(189)))));
            this.btnAddWorkshop.ForeColor = System.Drawing.Color.White;
            this.btnAddWorkshop.Location = new System.Drawing.Point(10, 480);
            this.btnAddWorkshop.Name = "btnAddWorkshop";
            this.btnAddWorkshop.Size = new System.Drawing.Size(75, 23);
            this.btnAddWorkshop.TabIndex = 1;
            this.btnAddWorkshop.Text = "Добавить";
            this.btnAddWorkshop.UseVisualStyleBackColor = false;
            this.btnAddWorkshop.Click += new System.EventHandler(this.BtnAddWorkshop_Click);
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
            // FormWorkshops
            // 
            this.BackColor = System.Drawing.Color.White;
            this.ClientSize = new System.Drawing.Size(784, 561);
            this.Controls.Add(this.btnBack);
            this.Controls.Add(this.btnAddWorkshop);
            this.Controls.Add(this.flpWorkshops);
            this.Name = "FormWorkshops";
            this.Text = "Список цехов";
            this.ResumeLayout(false);
        }

        private System.Windows.Forms.FlowLayoutPanel flpWorkshops;
        private System.Windows.Forms.Button btnAddWorkshop;
        private System.Windows.Forms.Button btnBack;
    }
}