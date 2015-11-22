using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Moon_X_Core
{
    public partial class MainForm : Form
    {
        public List<App> InstalledApps = new List<App>();
        public MainForm()
        {
            InitializeComponent();
            InstalledApps.AddRange(Steam.GetInstalledGames());
        }
    }
}
