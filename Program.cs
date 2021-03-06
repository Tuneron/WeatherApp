﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Windows.Forms;
using WeatherApp.Controllers;
using System.Net;
using WeatherApp.Forms;

namespace WeatherApp
{
    static class Program
    {
        /// <summary>
        /// Главная точка входа для приложения.
        /// </summary> 
        /// 

        public static bool CheckForInternetConnection()
        {
            try
            {
                using (var client = new WebClient())
                using (client.OpenRead("http://clients3.google.com/generate_204"))
                {
                    return true;
                }
            }
            catch
            {
                return false;
            }
        }

        [STAThread]
        static void Main()
        {
            Application.EnableVisualStyles();
            Application.SetCompatibleTextRenderingDefault(false);

            if (!CheckForInternetConnection())
                Application.Run(new MainForm(true, 0));
            else
            {
                Application.Run(new ForecastSelect());
            }
        }
    }
}
