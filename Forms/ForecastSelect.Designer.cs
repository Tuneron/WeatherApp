namespace WeatherApp.Forms
{
    partial class ForecastSelect
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
            this.LabelSelect = new System.Windows.Forms.Label();
            this.comboBoxCitiesForecast = new System.Windows.Forms.ComboBox();
            this.buttonLoad = new System.Windows.Forms.Button();
            this.LabelTimeBorder = new System.Windows.Forms.Label();
            this.SuspendLayout();
            // 
            // LabelSelect
            // 
            this.LabelSelect.AutoSize = true;
            this.LabelSelect.Font = new System.Drawing.Font("Microsoft Sans Serif", 10F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.LabelSelect.Location = new System.Drawing.Point(12, 9);
            this.LabelSelect.Name = "LabelSelect";
            this.LabelSelect.Size = new System.Drawing.Size(188, 17);
            this.LabelSelect.TabIndex = 0;
            this.LabelSelect.Text = "Select a forecast from saved";
            // 
            // comboBoxCitiesForecast
            // 
            this.comboBoxCitiesForecast.Font = new System.Drawing.Font("Microsoft Sans Serif", 10F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.comboBoxCitiesForecast.FormattingEnabled = true;
            this.comboBoxCitiesForecast.Location = new System.Drawing.Point(15, 29);
            this.comboBoxCitiesForecast.Name = "comboBoxCitiesForecast";
            this.comboBoxCitiesForecast.Size = new System.Drawing.Size(185, 24);
            this.comboBoxCitiesForecast.TabIndex = 1;
            this.comboBoxCitiesForecast.SelectedIndexChanged += new System.EventHandler(this.comboBoxCitiesForecast_SelectedIndexChanged);
            // 
            // buttonLoad
            // 
            this.buttonLoad.Location = new System.Drawing.Point(12, 91);
            this.buttonLoad.Name = "buttonLoad";
            this.buttonLoad.Size = new System.Drawing.Size(188, 23);
            this.buttonLoad.TabIndex = 2;
            this.buttonLoad.Text = "Load";
            this.buttonLoad.UseVisualStyleBackColor = true;
            this.buttonLoad.Click += new System.EventHandler(this.buttonLoad_Click);
            // 
            // LabelTimeBorder
            // 
            this.LabelTimeBorder.AutoSize = true;
            this.LabelTimeBorder.Location = new System.Drawing.Point(12, 66);
            this.LabelTimeBorder.Name = "LabelTimeBorder";
            this.LabelTimeBorder.Size = new System.Drawing.Size(28, 13);
            this.LabelTimeBorder.TabIndex = 3;
            this.LabelTimeBorder.Text = "? - ?";
            // 
            // ForecastSelect
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(213, 124);
            this.Controls.Add(this.LabelTimeBorder);
            this.Controls.Add(this.buttonLoad);
            this.Controls.Add(this.comboBoxCitiesForecast);
            this.Controls.Add(this.LabelSelect);
            this.Name = "ForecastSelect";
            this.Text = "ForecastSelect";
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Label LabelSelect;
        private System.Windows.Forms.ComboBox comboBoxCitiesForecast;
        private System.Windows.Forms.Button buttonLoad;
        private System.Windows.Forms.Label LabelTimeBorder;
    }
}