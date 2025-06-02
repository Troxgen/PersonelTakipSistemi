namespace PersonelTakipSistemi.Personel
{
    partial class PersonelDenetimKaydi
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            this.metroListView1 = new MetroFramework.Controls.MetroListView();
            this.metroDateTime1 = new MetroFramework.Controls.MetroDateTime();
            this.metroButton1 = new MetroFramework.Controls.MetroButton();
            this.SuspendLayout();
            // 
            // metroListView1
            // 
            this.metroListView1.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(15)))), ((int)(((byte)(15)))), ((int)(((byte)(15)))));
            this.metroListView1.Font = new System.Drawing.Font("Segoe UI", 12F);
            this.metroListView1.FullRowSelect = true;
            this.metroListView1.Location = new System.Drawing.Point(23, 120);
            this.metroListView1.Name = "metroListView1";
            this.metroListView1.OwnerDraw = true;
            this.metroListView1.Size = new System.Drawing.Size(1030, 610);
            this.metroListView1.Style = MetroFramework.MetroColorStyle.Yellow;
            this.metroListView1.TabIndex = 0;
            this.metroListView1.Theme = MetroFramework.MetroThemeStyle.Dark;
            this.metroListView1.UseCompatibleStateImageBehavior = false;
            this.metroListView1.UseSelectable = true;
            // 
            // metroDateTime1
            // 
            this.metroDateTime1.Location = new System.Drawing.Point(364, 85);
            this.metroDateTime1.MinimumSize = new System.Drawing.Size(0, 29);
            this.metroDateTime1.Name = "metroDateTime1";
            this.metroDateTime1.Size = new System.Drawing.Size(200, 29);
            this.metroDateTime1.TabIndex = 1;
            this.metroDateTime1.Theme = MetroFramework.MetroThemeStyle.Dark;
            // 
            // metroButton1
            // 
            this.metroButton1.Highlight = true;
            this.metroButton1.Location = new System.Drawing.Point(570, 85);
            this.metroButton1.Name = "metroButton1";
            this.metroButton1.Size = new System.Drawing.Size(142, 29);
            this.metroButton1.Style = MetroFramework.MetroColorStyle.Yellow;
            this.metroButton1.TabIndex = 17;
            this.metroButton1.Text = "Ara";
            this.metroButton1.Theme = MetroFramework.MetroThemeStyle.Dark;
            this.metroButton1.UseSelectable = true;
            this.metroButton1.Click += new System.EventHandler(this.metroButton1_Click);
            // 
            // PersonelDenetimKaydi
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1077, 753);
            this.Controls.Add(this.metroButton1);
            this.Controls.Add(this.metroDateTime1);
            this.Controls.Add(this.metroListView1);
            this.ForeColor = System.Drawing.SystemColors.ActiveBorder;
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "PersonelDenetimKaydi";
            this.Resizable = false;
            this.Style = MetroFramework.MetroColorStyle.Yellow;
            this.Text = "Denetim Kaydı";
            this.TextAlign = MetroFramework.Forms.MetroFormTextAlign.Center;
            this.Theme = MetroFramework.MetroThemeStyle.Dark;
            this.Load += new System.EventHandler(this.PersonelDenetimKaydi_Load);
            this.ResumeLayout(false);

        }

        #endregion

        private MetroFramework.Controls.MetroListView metroListView1;
        private MetroFramework.Controls.MetroDateTime metroDateTime1;
        private MetroFramework.Controls.MetroButton metroButton1;
    }
}