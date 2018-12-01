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
using WeatherApp.Models;
using WeatherApp.Models.Json;

namespace WeatherApp
{
    public partial class MainForm : Form
    {
        DatabaseController database;

        enum Days
        {
            week = 7
        }

        public MainForm()
        {
            InitializeComponent();
            database = new DatabaseController("Data Source=.\\SQLEXPRESS01; Initial Catalog = BlogDatabase; Integrated Security = True; Pooling=False");
            UpdateInternetConnectionStatus();
            UpdateDatabaseConnectionStatus();
            TodayForecastPicture.ImageLocation = "C:/Users/Alex/source/repos/WeatherApp/WeatherApp/Resources/UnknownWheather.jpg";
            TodayForecastPicture.Load("http:" + database.GetCurrentWeather("Odesa").condition.icon);
            TodayForecastPicture.SizeMode = PictureBoxSizeMode.StretchImage;
            UpdateCurrentWeather("Odesa");
            UpdateForecastWeek("Odesa");
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

        private void UpdateCurrentWeather(string city)
        {
            LabelCurrentUpdateValue.Text = database.GetCurrentWeather(city).last_updated.Remove(0, 10);
            LabelCurrentTempValue.Text = database.GetCurrentWeather(city).temp_c + " ℃";
            LabelCurrentWindValue.Text = database.GetCurrentWeather(city).wind_kph.ToString() + " km/h";
            LabelCurrentWindDirValue.Text = database.GetCurrentWeather(city).wind_dir;
            LabelCurrentHymidityValue.Text = database.GetCurrentWeather(city).humidity.ToString() + "%";
            LabelCurrentPressureValue.Text = database.GetCurrentWeather(city).pressure_in.ToString() + " mrb";
        }

        private void UpdateForecastDay(int dayCount, string date, Models.Json.Day day)
        {
            List<Control> c = Controls.OfType<GroupBox>().Cast<Control>().ToList();

            c[dayCount].Controls[0].Text = date;
            c[dayCount].Controls[1].Text = day.avgtemp_c.ToString() + " ℃";
            c[dayCount].Controls[2].Text = day.avgvis_km.ToString() + " km/h";
            ((PictureBox)c[dayCount].Controls[3]).Load("http:" + day.condition.icon);
        }

        private void UpdateForecastWeek(string city)
        {
            for(int i = 1; i < (int)Days.week; i++)
            {
                    UpdateForecastDay(i - 1, database.GetWeekForecast(city)[i].date,
                        database.GetWeekForecast(city)[i].day);
            }
        }
    }
}
