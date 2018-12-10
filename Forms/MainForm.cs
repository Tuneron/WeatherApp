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
        private DatabaseController database;

        private bool OnlineMode;
        private int startId;

        public enum Days
        {
            week = 7
        }

        public MainForm(bool mode, int startId)
        {
            InitializeComponent();

            OnlineMode = mode;
            this.startId = startId;

            database = new DatabaseController("Data Source=.\\SQLEXPRESS01; Initial Catalog = WeatherApp; Integrated Security = True; Pooling=False");

            UpdateInternetConnectionStatus();
            UpdateDatabaseConnectionStatus();

            string[] defaultLocation = database.GetDefaultLocation();

            if (OnlineMode)
            {
                LoadUserCatalogs();
                comboBoxUserCities.Text = defaultLocation[0];
                comboBoxUserRegions.Text = defaultLocation[1];
                comboBoxUserCountries.Text = defaultLocation[2];
            }
            else
            {
                comboBoxUserCities.Text = database.GetOfflineLocation("City", startId);
                comboBoxUserRegions.Text = database.GetOfflineLocation("Region", startId);
                comboBoxUserCountries.Text = database.GetOfflineLocation("Country", startId);
            }

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
            if (OnlineMode)
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
            else
            {
                TodayForecast.Text = database.GetSavedValue("Date", GetStartSavedId());

                TodayForecastPicture.Load(database.GetSavedValue("Icon", GetStartSavedId()));
                LabelCurrentUpdateValue.Text = database.GetSavedValue("LastUpdate", GetStartSavedId()).Remove(0,5);
                LabelCurrentTempValue.Text = database.GetSavedValue("MinTemp", GetStartSavedId()) +
                    " - " + database.GetSavedValue("MaxTemp", GetStartSavedId()) + " ℃";
                LabelCurrentWindValue.Text = database.GetSavedValue("Wind", GetStartSavedId()) + " km/h";

                LabelCurrentWindDir.Visible = false;
                LabelCurrentWindDirValue.Visible = false;

                LabelCurrentHymidity.Location = new Point(LabelCurrentWindDir.Location.X, LabelCurrentWindDir.Location.Y);
                LabelCurrentHymidityValue.Location = new Point(LabelCurrentWindDirValue.Location.X, LabelCurrentWindDirValue.Location.Y);

                LabelCurrentHymidityValue.Text = database.GetSavedValue("Humidity", GetStartSavedId()) + "%";

                LabelCurrentPressure.Visible = true;
                LabelCurrentPressureValue.Visible = true;

                LabelCurrentPressure.Location = new Point(LabelCurrentHymidity.Location.X, LabelCurrentHymidity.Location.Y + 29);
                LabelCurrentPressureValue.Location = new Point(LabelCurrentHymidityValue.Location.X, LabelCurrentHymidityValue.Location.Y + 29);

                LabelCurrentPressureValue.Text = database.GetSavedValue("Pressure", GetStartSavedId()) + " mrb";
            }
        }

        private void UpdateCurrentWeather(string city, int day)
        {
            if (OnlineMode)
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
            else
            {
                TodayForecast.Text = database.GetSavedValue("Date", GetStartSavedId() + day);

                TodayForecastPicture.Load(database.GetSavedValue("Icon", GetStartSavedId() + day));
                LabelCurrentUpdateValue.Text = database.GetSavedValue("LastUpdate", GetStartSavedId() + day).Remove(0, 5);
                LabelCurrentTempValue.Text =
                    database.GetSavedValue("MinTemp", GetStartSavedId() + day) + " ... " +
                    database.GetSavedValue("MaxTemp", GetStartSavedId() + day) + " ℃";

                LabelCurrentWindValue.Text = database.GetSavedValue("Wind", GetStartSavedId() + day) + " km/h";

                LabelCurrentWindDir.Visible = false;
                LabelCurrentWindDirValue.Visible = false;

                LabelCurrentHymidity.Location = LabelCurrentWindDir.Location;
                LabelCurrentHymidityValue.Location = LabelCurrentWindDirValue.Location;

                LabelCurrentHymidityValue.Text = database.GetSavedValue("Humidity", GetStartSavedId() + day) + "%";

                LabelCurrentPressure.Visible = false;
                LabelCurrentPressureValue.Visible = false;
            }
        }

        private void UpdateForecastDay(int dayCount, string date, Models.Json.Day day)
        {
            List<Control> c = Controls.OfType<GroupBox>().Cast<Control>().ToList();

            ((Label)c[dayCount].Controls[0]).Text = date;
            ((Label)c[dayCount].Controls[1]).Text = day.avgtemp_c.ToString() + " ℃";
            ((Label)c[dayCount].Controls[2]).Text = day.avgvis_km.ToString() + " km/h";
            ((PictureBox)c[dayCount].Controls[3]).Load("http:" + day.condition.icon);
        }

        private void UpdateForecastDay(int dayCount)
        {
            List<Control> c = Controls.OfType<GroupBox>().Cast<Control>().ToList();

            ((Label)c[dayCount].Controls[0]).Text = database.GetSavedValue("Date", dayCount + GetStartSavedId() + 1);
            ((Label)c[dayCount].Controls[1]).Text = database.GetSavedValue("MinTemp", dayCount + GetStartSavedId() + 1) + " - " +
               database.GetSavedValue("MaxTemp", dayCount + GetStartSavedId() + 1) + " ℃";
            ((Label)c[dayCount].Controls[2]).Text = database.GetSavedValue("Wind", dayCount + GetStartSavedId() + 1) + " km/h";
            ((PictureBox)c[dayCount].Controls[3]).Load(database.GetSavedValue("Icon", dayCount + GetStartSavedId() + 1));
        }

        private void UpdateForecastWeek(string city)
        {
            for(int i = 1; i < (int)Days.week; i++)
            {
                if (OnlineMode)
                {
                    UpdateForecastDay(i - 1, database.GetWeekForecast(city)[i].date,
                        database.GetWeekForecast(city)[i].day);
                }
                else
                {
                    UpdateForecastDay(i - 1);
                }
            }
        }

        private void UpdateCurrentForecastLocation(string city)
        {
            if (OnlineMode)
            {
                LabelCurrentForecastCityValue.Text = database.GetCurrentLocation(city).name;
                LabelCurrentForecastRegionValue.Text = database.GetCurrentLocation(city).region;
                LabelCurrentForecastCountryValue.Text = database.GetCurrentLocation(city).country;
            }
            else
            {
                LabelCurrentForecastCityValue.Text = database.GetOfflineLocation("City", startId);
                LabelCurrentForecastRegionValue.Text = database.GetOfflineLocation("Region", startId);
                LabelCurrentForecastCountryValue.Text = database.GetOfflineLocation("Country", startId);

            }
        }

        private void buttonGetForecast_Click(object sender, EventArgs e)
        {
            if (OnlineMode)
            {
                UpdateCurrentWeather(GetForecastRequest());
                UpdateForecastWeek(GetForecastRequest());
                UpdateCurrentForecastLocation(GetForecastRequest());
                this.Refresh();
            }
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
            database.SetDefaultLocation(
                database.GetCurrentLocation(GetForecastRequest()).name,
                database.GetCurrentLocation(GetForecastRequest()).region,
                database.GetCurrentLocation(GetForecastRequest()).country
                );
        }

        private void LoadUserCatalogs()
        {
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
        }

        public void buttonSaveForecastCopy_Click(object sender, EventArgs e)
        {
            database.SaveForecast(GetForecastRequest());
        }

        private void MainForm_FormClosing(object sender, FormClosingEventArgs e)
        {   
            database.SaveUsersCatalogs(
                GetCatalogItems(comboBoxUserCities),
                GetCatalogItems(comboBoxUserRegions),
                GetCatalogItems(comboBoxUserCountries)
                );

            if (!OnlineMode)
            {
                Application.Exit();
            }
        }

        private string[] GetCatalogItems(ComboBox comboBox)
        {
            string[] items = new string[comboBox.Items.Count];
            for(int i = 0; i < comboBox.Items.Count; i++)
                items[i] = comboBox.Items[i].ToString();
            return items;
        }

        private int GetStartSavedId()
        {
            return GetEndSavedId() - 6;
        }

        private int GetEndSavedId()
        {
            return startId * 7;
        }
    }
}
