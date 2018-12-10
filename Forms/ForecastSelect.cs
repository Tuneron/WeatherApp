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

namespace WeatherApp.Forms
{
    public partial class ForecastSelect : Form
    {
        private DatabaseController database;
        public ForecastSelect()
        {
            InitializeComponent();

            database = new DatabaseController("Data Source=.\\SQLEXPRESS01; Initial Catalog = WeatherApp; Integrated Security = True; Pooling=False");

            UpdateUserSavedCities();
        }

        private void UpdateUserSavedCities()
        {
            for (int i = 0; i < database.GetSavedLocations().Length; i++)
            {
                comboBoxCitiesForecast.Items.Add(database.GetSavedLocations()[i]);
            }
        }

        private void UpdateTimeInterval(int SelectedItemIndex)
        {
            LabelTimeBorder.Text = database.GetTimeInterval(SelectedItemIndex);
        }

        private void buttonLoad_Click(object sender, EventArgs e)
        {
           this.Hide();
           new MainForm(false, comboBoxCitiesForecast.SelectedIndex + 1).Show();
        }

        private void comboBoxCitiesForecast_SelectedIndexChanged(object sender, EventArgs e)
        {
            UpdateTimeInterval(comboBoxCitiesForecast.SelectedIndex + 1);
        }
    }
}
