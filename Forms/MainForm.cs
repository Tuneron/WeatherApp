using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using WeatherApp.Controllers;

namespace WeatherApp
{
    public partial class MainForm : Form
    {
        DatabaseController database;

        public MainForm()
        {
            InitializeComponent();
            database = new DatabaseController("Data Source=.\\SQLEXPRESS01; Initial Catalog = BlogDatabase; Integrated Security = True; Pooling=False");
            UpdateInternetConnectionStatus();
            UpdateDatabaseConnectionStatus();
            TodayForecastPicture.ImageLocation = "C:/Users/Alex/source/repos/WeatherApp/WeatherApp/Resources/UnknownWheather.jpg";
            TodayForecastPicture.SizeMode = PictureBoxSizeMode.StretchImage;
        }

        private bool UpdateInternetConnectionStatus()
        {
            if (Program.CheckForInternetConnection())
            {
                this.labelConnectionInternet.Text = "Internet √";
                this.labelConnectionInternet.ForeColor = System.Drawing.Color.Green;
                return true;
            }
            else
            {
                this.labelConnectionInternet.Text = "Internet x";
                this.labelConnectionInternet.ForeColor = System.Drawing.Color.Red;
                return false;
            }
        }

        private bool UpdateDatabaseConnectionStatus()
        {
            if (database.CheckDatabaseConnect())
            {
                this.labelConnectionDatabase.Text = "Database √";
                this.labelConnectionDatabase.ForeColor = System.Drawing.Color.Green;
                return true;
            }
            else
            {
                this.labelConnectionDatabase.Text = "Database x";
                this.labelConnectionDatabase.ForeColor = System.Drawing.Color.Red;
                return false;
            }
        }
    }
}
