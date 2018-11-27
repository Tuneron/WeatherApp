namespace WeatherApp
{
    partial class MainForm
    {
        /// <summary>
        /// Обязательная переменная конструктора.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Освободить все используемые ресурсы.
        /// </summary>
        /// <param name="disposing">истинно, если управляемый ресурс должен быть удален; иначе ложно.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Код, автоматически созданный конструктором форм Windows

        /// <summary>
        /// Требуемый метод для поддержки конструктора — не изменяйте 
        /// содержимое этого метода с помощью редактора кода.
        /// </summary>
        private void InitializeComponent()
        {
            this.labelConnectionInternet = new System.Windows.Forms.Label();
            this.labelConnectionDatabase = new System.Windows.Forms.Label();
            this.ControllPanel = new System.Windows.Forms.GroupBox();
            this.TodayForecast = new System.Windows.Forms.GroupBox();
            this.TodayForecastPicture = new System.Windows.Forms.PictureBox();
            this.ControllPanel.SuspendLayout();
            this.TodayForecast.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.TodayForecastPicture)).BeginInit();
            this.SuspendLayout();
            // 
            // labelConnectionInternet
            // 
            this.labelConnectionInternet.AutoSize = true;
            this.labelConnectionInternet.Font = new System.Drawing.Font("Microsoft Sans Serif", 11F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.labelConnectionInternet.Location = new System.Drawing.Point(6, 16);
            this.labelConnectionInternet.Name = "labelConnectionInternet";
            this.labelConnectionInternet.Size = new System.Drawing.Size(76, 18);
            this.labelConnectionInternet.TabIndex = 0;
            this.labelConnectionInternet.Text = "Connect...";
            // 
            // labelConnectionDatabase
            // 
            this.labelConnectionDatabase.AutoSize = true;
            this.labelConnectionDatabase.Font = new System.Drawing.Font("Microsoft Sans Serif", 11F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.labelConnectionDatabase.Location = new System.Drawing.Point(6, 34);
            this.labelConnectionDatabase.Name = "labelConnectionDatabase";
            this.labelConnectionDatabase.Size = new System.Drawing.Size(76, 18);
            this.labelConnectionDatabase.TabIndex = 1;
            this.labelConnectionDatabase.Text = "Connect...";
            // 
            // ControllPanel
            // 
            this.ControllPanel.Controls.Add(this.labelConnectionInternet);
            this.ControllPanel.Controls.Add(this.labelConnectionDatabase);
            this.ControllPanel.Location = new System.Drawing.Point(12, 12);
            this.ControllPanel.Name = "ControllPanel";
            this.ControllPanel.Size = new System.Drawing.Size(673, 58);
            this.ControllPanel.TabIndex = 2;
            this.ControllPanel.TabStop = false;
            this.ControllPanel.Text = "Controll";
            // 
            // TodayForecast
            // 
            this.TodayForecast.Controls.Add(this.TodayForecastPicture);
            this.TodayForecast.Location = new System.Drawing.Point(12, 76);
            this.TodayForecast.Name = "TodayForecast";
            this.TodayForecast.Size = new System.Drawing.Size(173, 304);
            this.TodayForecast.TabIndex = 3;
            this.TodayForecast.TabStop = false;
            this.TodayForecast.Text = "Today";
            // 
            // TodayForecastPicture
            // 
            this.TodayForecastPicture.Location = new System.Drawing.Point(6, 19);
            this.TodayForecastPicture.Name = "TodayForecastPicture";
            this.TodayForecastPicture.Size = new System.Drawing.Size(161, 104);
            this.TodayForecastPicture.TabIndex = 0;
            this.TodayForecastPicture.TabStop = false;
            // 
            // MainForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(705, 392);
            this.Controls.Add(this.TodayForecast);
            this.Controls.Add(this.ControllPanel);
            this.Name = "MainForm";
            this.Text = "Form1";
            this.ControllPanel.ResumeLayout(false);
            this.ControllPanel.PerformLayout();
            this.TodayForecast.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.TodayForecastPicture)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Label labelConnectionInternet;
        private System.Windows.Forms.Label labelConnectionDatabase;
        private System.Windows.Forms.GroupBox ControllPanel;
        private System.Windows.Forms.GroupBox TodayForecast;
        private System.Windows.Forms.PictureBox TodayForecastPicture;
    }
}

