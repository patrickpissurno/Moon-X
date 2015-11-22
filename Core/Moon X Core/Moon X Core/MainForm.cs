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
        public static MainForm instance = null;

        public List<App> InstalledApps = new List<App>();
        public MainForm()
        {
            instance = this;
            InitializeComponent();
            InstalledApps.AddRange(Steam.GetInstalledGames());
            Service s = new Service();
            s.Init();
        }

        public void ShowMessage(string str)
        {
            MessageBox.Show(str);
        }
    }
}
