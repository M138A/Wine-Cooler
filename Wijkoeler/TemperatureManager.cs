using System;
using System.Diagnostics;
using System.Timers;
using System.Windows.Threading;
using Wijkoeler;

namespace WineCooler
{
    class TemperatureManager
    {
        public double temperature { get; set; }
        public double setTemperature { get; set; }
        private DoorManager Dm;
        private Engine engine;
        private double difference;
        private bool increase;
        private Timer TemperatureTimer;
        private MainWindow mainWindow;
        public TemperatureManager(DoorManager DoorM, Engine en, MainWindow mw)
        {
            mainWindow = mw;
            engine = en;
            temperature = 7.2;
            Dm = DoorM;
            
        }
        
        public void dooropened()
        {
            if (TemperatureTimer != null)
            {
                TemperatureTimer.Stop();
            }
            increase = true;
            difference = 0.015;
            startTimer();
        }
        private void startTimer()
        {
            engine.turnOn();
            TemperatureTimer = new Timer();
            TemperatureTimer.Elapsed += new ElapsedEventHandler(CoolDown);
            TemperatureTimer.Interval = 1000; // 1000 ms is one second
            TemperatureTimer.Start();
            
        }

        public void GoToRequestedTemp()
        {
            if (TemperatureTimer != null)
            {
                TemperatureTimer.Stop();
            }
            difference = 0.00067;
            increase = false;
            startTimer();
            

        }
         
        private void CoolDown(object sender, ElapsedEventArgs e)
        {
            if (Dm.status && increase)
            {
                temperature += difference;
                MainWindow.main.Status = "Current temperature: " + temperature.ToString("#.###");

            }
            else if (!increase && temperature > setTemperature)
            {
                temperature -= difference;
                MainWindow.main.Status = "Current temperature: " + temperature.ToString("#.###");
            }

            else
            {
                TemperatureTimer.Stop();
                engine.turnOff();
            }
        }
        
    }
}
