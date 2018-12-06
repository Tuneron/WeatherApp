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

            database = new DatabaseController("Data Source=.\\SQLEXPRESS01; Initial Catalog = WeatherApp; Integrated Security = True; Pooling=False");

            UpdateInternetConnectionStatus();
            UpdateDatabaseConnectionStatus();

            database.StartConnection();
            string[] defaultLocation = database.GetDefaultLocation();
            database.CloseConnection();

            LoadUserCatalogs();
            comboBoxUserCities.Text = defaultLocation[0];
            comboBoxUserRegions.Text = defaultLocation[1];
            comboBoxUserCountries.Text = defaultLocation[2];
            
            UpdateCurrentWeather(GetForecastRequest());
            UpdateForecastWeek(GetForecastRequest());
            UpdateCurrentForecastLocation(GetForecastRequest());

            foreach (System.Windows.Forms.Control item in this.SlotDay1.Controls)
                item.Click += new System.EventHandler(SlotDay1_Click);
            foreach (System.Windows.Forms.Control item in this.SlotDay2.Controls)
                item.Click += new System.EventHandler(SlotDay2_Click);
            foreach (System.Windows.Forms.Control item in this.SlotDay3.Controls)
                item.Click += new System.EventHandler(SlotDay3_Click);
            foreach (System.Windows.Forms.Control item in this.SlotDay4.Controls)
                item.Click += new System.EventHandler(SlotDay4_Click);
            foreach (System.Windows.Forms.Control item in this.SlotDay5.Controls)
                item.Click += new System.EventHandler(SlotDay5_Click);
            foreach (System.Windows.Forms.Control item in this.SlotDay6.Controls)
                item.Click += new System.EventHandler(SlotDay6_Click);
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
            TodayForecast.Text = "Current";

            TodayForecastPicture.Load("http:" + database.GetCurrentWeather(city).condition.icon);
            LabelCurrentUpdateValue.Text = database.GetCurrentWeather(city).last_updated.Remove(0, 11);
            LabelCurrentTempValue.Text = database.GetCurrentWeather(city).temp_c + " ℃";
            LabelCurrentWindValue.Text = database.GetCurrentWeather(city).wind_kph.ToString() + " km/h";

            LabelCurrentWindDir.Visible = true;
            LabelCurrentWindDirValue.Visible = true;

            LabelCurrentWindDirValue.Text = database.GetCurrentWeather(city).wind_dir;

            LabelCurrentHymidity.Location = new Point(LabelCurrentWindDir.Location.X, LabelCurrentWindDir.Location.Y + 29);
            LabelCurrentHymidityValue.Location = new Point(LabelCurrentWindDirValue.Location.X, LabelCurrentWindDirValue.Location.Y + 29);

            LabelCurrentHymidityValue.Text = database.GetCurrentWeather(city).humidity.ToString() + "%";

            LabelCurrentPressure.Visible = true;
            LabelCurrentPressureValue.Visible = true;

            LabelCurrentPressureValue.Text = database.GetCurrentWeather(city).pressure_in.ToString() + " mrb";
        }

        private void UpdateCurrentWeather(string city, int day)
        {
            TodayForecast.Text = database.GetWeekForecast(city)[day].date;

            TodayForecastPicture.Load("http:" + database.GetWeekForecast(city)[day].day.condition.icon);
            LabelCurrentUpdateValue.Text = database.GetCurrentWeather(city).last_updated.Remove(0, 11);
            LabelCurrentTempValue.Text = 
                database.GetWeekForecast(city)[day].day.mintemp_c + " ... " +
                database.GetWeekForecast(city)[day].day.maxtemp_c + " ℃";

            LabelCurrentWindValue.Text = database.GetWeekForecast(city)[day].day.avgvis_km.ToString() + " km/h";

            LabelCurrentWindDir.Visible = false;
            LabelCurrentWindDirValue.Visible = false;

            LabelCurrentHymidity.Location = LabelCurrentWindDir.Location;
            LabelCurrentHymidityValue.Location = LabelCurrentWindDirValue.Location;

            LabelCurrentHymidityValue.Text = database.GetWeekForecast(city)[day].day.avghumidity.ToString() + "%";

            LabelCurrentPressure.Visible = false;
            LabelCurrentPressureValue.Visible = false;
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

        private void buttonSetCurrentWeather_Click(object sender, EventArgs e)
        {
            UpdateCurrentWeather(GetForecastRequest());
        }

        private void SlotDay1_Click(object sender, EventArgs e)
        {
            UpdateCurrentWeather(GetForecastRequest(), 1);
        }

        private void SlotDay2_Click(object sender, EventArgs e)
        {
            UpdateCurrentWeather(GetForecastRequest(), 2);
        }

        private void SlotDay3_Click(object sender, EventArgs e)
        {
            UpdateCurrentWeather(GetForecastRequest(), 3);
        }

        private void SlotDay4_Click(object sender, EventArgs e)
        {
            UpdateCurrentWeather(GetForecastRequest(), 4);
        }

        private void SlotDay5_Click(object sender, EventArgs e)
        {
            UpdateCurrentWeather(GetForecastRequest(), 5);
        }

        private void SlotDay6_Click(object sender, EventArgs e)
        {
            UpdateCurrentWeather(GetForecastRequest(), 6);
        }

        private void buttonSetDefaultLocation_Click(object sender, EventArgs e)
        {
            database.StartConnection();
            database.SetDefaultLocation(
                database.GetCurrentLocation(GetForecastRequest()).name,
                database.GetCurrentLocation(GetForecastRequest()).region,
                database.GetCurrentLocation(GetForecastRequest()).country
                );
            database.CloseConnection();
        }

        private void LoadUserCatalogs()
        {
            database.StartConnection();
            foreach (string item in database.LoadUserCatalog("City"))
            {
                comboBoxUserCities.Items.Add(item);
            }
            foreach (string item in database.LoadUserCatalog("Region"))
            {
                comboBoxUserRegions.Items.Add(item);
            }
            foreach (string item in database.LoadUserCatalog("Country"))
            {
                comboBoxUserCountries.Items.Add(item);
            }
            database.CloseConnection();
        }

        public void buttonSaveForecastCopy_Click(object sender, EventArgs e)
        {

        }

        private void MainForm_FormClosing(object sender, FormClosingEventArgs e)
        {
            database.StartConnection();
            database.SaveUsersCatalogs(
                GetCatalogItems(comboBoxUserCities),
                GetCatalogItems(comboBoxUserRegions),
                GetCatalogItems(comboBoxUserCountries)
                );
            database.CloseConnection();
        }

        private string[] GetCatalogItems(ComboBox comboBox)
        {
            string[] items = new string[comboBox.Items.Count];
            for(int i = 0; i < comboBox.Items.Count; i++)
                items[i] = comboBox.Items[i].ToString();
            return items;
        }
    }
}
