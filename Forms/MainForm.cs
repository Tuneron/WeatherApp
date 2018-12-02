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

            comboBoxUserCities.Items.Add("Odesa");
            comboBoxUserCities.Items.Add("Paris");
            comboBoxUserCities.Items.Add("Moscow");
            comboBoxUserCities.Text = "Odesa";

            UpdateCurrentWeather(GetForecastRequest());
            UpdateForecastWeek(GetForecastRequest());
            UpdateCurrentForecastLocation(GetForecastRequest());
        }

        private string GetForecastRequest()
        {
            return comboBoxUserCities.Text + " " + comboBoxUserRegions.Text + " " + comboBoxUserCountries.Text;
        }

        private bool UpdateInternetConnectionStatus()
        {
            if (Program.CheckForInternetConnection())
            {
                this.labelConnectionInternet.Text = "Internet √";
                this.labelConnectionInternet.ForeColor = System.Drawing.Color.Green;
                return true;
            }

            this.labelConnectionInternet.Text = "Internet x";
            this.labelConnectionInternet.ForeColor = System.Drawing.Color.Red;
            return false;
        }

        private bool UpdateDatabaseConnectionStatus()
        {
            if (database.CheckDatabaseConnect())
            {
                this.labelConnectionDatabase.Text = "Database √";
                this.labelConnectionDatabase.ForeColor = System.Drawing.Color.Green;
                return true;
            }

            this.labelConnectionDatabase.Text = "Database x";
            this.labelConnectionDatabase.ForeColor = System.Drawing.Color.Red;
            return false;
        }

        private void UpdateCurrentWeather(string city)
        {
            TodayForecastPicture.Load("http:" + database.GetCurrentWeather(city).condition.icon);
            LabelCurrentUpdateValue.Text = database.GetCurrentWeather(city).last_updated.Remove(0, 11);
            LabelCurrentTempValue.Text = database.GetCurrentWeather(city).temp_c + " ℃";
            LabelCurrentWindValue.Text = database.GetCurrentWeather(city).wind_kph.ToString() + " km/h";
            LabelCurrentWindDirValue.Text = database.GetCurrentWeather(city).wind_dir;
            LabelCurrentHymidityValue.Text = database.GetCurrentWeather(city).humidity.ToString() + "%";
            LabelCurrentPressureValue.Text = database.GetCurrentWeather(city).pressure_in.ToString() + " mrb";
        }

        private void UpdateForecastDay(int dayCount, string date, Models.Json.Day day)
        {
            List<Control> c = Controls.OfType<GroupBox>().Cast<Control>().ToList();

            ((Label)c[dayCount].Controls[0]).Text = date;
            ((Label)c[dayCount].Controls[1]).Text = day.avgtemp_c.ToString() + " ℃";
            ((Label)c[dayCount].Controls[2]).Text = day.avgvis_km.ToString() + " km/h";
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

        private void UpdateCurrentForecastLocation(string city)
        {
            LabelCurrentForecastCityValue.Text = database.GetCurrentLocation(city).name;
            LabelCurrentForecastRegionValue.Text = database.GetCurrentLocation(city).region;
            LabelCurrentForecastCountryValue.Text = database.GetCurrentLocation(city).country;
        }

        private void buttonGetForecast_Click(object sender, EventArgs e)
        {
            UpdateCurrentWeather(GetForecastRequest());
            UpdateForecastWeek(GetForecastRequest());
            UpdateCurrentForecastLocation(GetForecastRequest());
            this.Refresh();
        }

        private void buttonAddCity_Click(object sender, EventArgs e)
        {
            comboBoxUserCities.Items.Add(comboBoxUserCities.Text);
            comboBoxUserCities.Refresh();
        }

        private void buttonDeleteCity_Click(object sender, EventArgs e)
        {
            comboBoxUserCities.Items.Remove(comboBoxUserCities.SelectedItem);
            comboBoxUserCities.Refresh();
        }

        private void buttonAddCountry_Click(object sender, EventArgs e)
        {
            comboBoxUserCountries.Items.Add(comboBoxUserCountries.Text);
            comboBoxUserCountries.Refresh();
        }

        private void buttonDeleteCountry_Click(object sender, EventArgs e)
        {
            comboBoxUserCountries.Items.Remove(comboBoxUserCountries.SelectedItem);
            comboBoxUserCountries.Refresh();
        }

        private void buttonAddRegion_Click(object sender, EventArgs e)
        {
            comboBoxUserRegions.Items.Add(comboBoxUserRegions.Text);
            comboBoxUserRegions.Refresh();
        }

        private void buttonDeleteRegion_Click(object sender, EventArgs e)
        {
            comboBoxUserRegions.Items.Remove(comboBoxUserRegions.SelectedItem);
            comboBoxUserRegions.Refresh();
        }
    }
}
