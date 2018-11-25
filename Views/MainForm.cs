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
        JsonParserController parser;
        
        public MainForm()
        {
            InitializeComponent();
            parser = new JsonParserController();
            this.label1.Text = parser.CheckForInternetConnection();
        }
    }
}
