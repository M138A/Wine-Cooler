using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Timers;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;
using Wijkoeler;
using WineCooler;

namespace WineCooler
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// 
    /// </summary>
    public partial class MainWindow : Window
    {
        private TemperatureManager Tm;
        private DoorManager Dm;
        private Engine engine;
        public MainWindow()
        {
            InitializeComponent();
            doorLabel.Content = "Door is closed";
            engine = new Engine();
            Dm = new DoorManager();
            Tm = new TemperatureManager(Dm, engine, this);
            main = this;
            currentTemperature.Content = "Current temperature : " + Tm.temperature.ToString();


        }
        private void updateUI()
        {
            if (engine.status == true)
            {
                currentTemperature.Content = "Current temperature : " + Tm.temperature.ToString();
            }

        }

        public void updateTemperatureLabel()
        {
            currentTemperature.Content = "Current Temperature :" + Tm.temperature.ToString();
        }

        private void OperateDoor(object sender, RoutedEventArgs e)
        {
            if (Dm.status)

            {
                Dm.status = false;
                doorLabel.Content = "Door is closed";                
                Tm.GoToRequestedTemp();
            }
            else
            {
                Dm.status = true;
                doorLabel.Content = "Door is opened";
                Tm.dooropened();
            }
            
        }
        internal static MainWindow main;
        internal string Status
        {
            get { return currentTemperature.Content.ToString(); }
            set { Dispatcher.Invoke(new Action(() => { currentTemperature.Content = value; })); }
        }


        private void SetTemperature(object sender, RoutedEventArgs e)
        {
            Tm.setTemperature = Convert.ToDouble(neededTemperature.Text);
            TemperatureSetByUser.Content = "Temperature set: " + neededTemperature.Text;
            neededTemperature.Clear();
            Tm.GoToRequestedTemp();
            updateUI();
        }
    }
}
