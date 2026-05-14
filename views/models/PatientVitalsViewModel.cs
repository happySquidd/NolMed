using LiveCharts;
using LiveCharts.Wpf;
using NolMed.database;
using NolMed.model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection.Metadata;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Threading;

namespace NolMed.views.models
{
    public class PatientVitalsViewModel : BaseView
    {
        #region Definitions
        public ChartValues<double> HeartRateValues { get; set; }
        public SeriesCollection ChartSeries { get; set; }

        private string _placeholderText;
        public string PlaceholderText
        {
            get => _placeholderText;
            set { _placeholderText = value; OnPropertyChanged(); }
        }
        public Axis[] YAxes { get; set; }
        private double _yMin;
        public double YMin
        {
            get => _yMin;
            set { _yMin = value; OnPropertyChanged(); }
        }
        private double _yMax;
        public double YMax
        {
            get => _yMax;
            set { _yMax = value; OnPropertyChanged(); }
        }
        private const double ChartPadding = 10;
        private string _bloodPressureSys;
        public string BloodPressureSys
        {
            get => _bloodPressureSys;
            set { _bloodPressureSys = value; OnPropertyChanged(); }
        }
        private string _bloodPressureDia;
        public string BloodPressureDia
        {
            get => _bloodPressureDia;
            set { _bloodPressureDia = value; OnPropertyChanged(); }
        }
        private string _temperature;
        public string Temperature
        {
            get => _temperature;
            set { _temperature = value; OnPropertyChanged(); }
        }
        private string _bpmNumber;
        public string BpmNumber
        {
            get => _bpmNumber;
            set { _bpmNumber = value; OnPropertyChanged(); }
        }
        private string _roomTitle;
        public string RoomTitle
        {
            get => _roomTitle;
            set { _roomTitle = value; OnPropertyChanged(); }
        }
        public ICommand BackClicked { get; }
        public ICommand DischargePatient { get; }
        private readonly Action _goBack;
        private ErOverviewBox _roomInfo;
#endregion

        public PatientVitalsViewModel(ErOverviewBox roomInfo, Action goBack)
        {
            _goBack = goBack;
            _roomInfo = roomInfo;
            // buttons
            BackClicked = new RelayCommand(_ => _goBack());
            DischargePatient = new RelayCommand(RemovePatient);

            PlaceholderText = "Patient Vitals";
            RoomTitle = $"Room: {_roomInfo.RoomNumber}";
            HeartRateValues = new ChartValues<double>();

            ChartSeries = new SeriesCollection
            {
                new LineSeries
                {
                    Title = "Heart Rate",
                    Values = HeartRateValues,
                    PointGeometry = null,
                    LineSmoothness = 0,
                    StrokeThickness = 2,
                    Fill = Brushes.Transparent,
                    Stroke = Brushes.LimeGreen
                }
            };
            YMin = -2;
            YMax = 3;

            // function to simulate blood pressure and temperature
            GenerateVitals();
            // subscribe to room
            App.Redis.SubscribeToRoom(_roomInfo.RoomNumber, MonitorHeartRate);
            var timer = new DispatcherTimer();
            timer.Interval = TimeSpan.FromMilliseconds(40);
            timer.Tick += SimulateEkg;
            timer.Start();
        }

        private void GenerateVitals()
        {
            // Simulate blood pressure and temperature
            Task.Run(async () =>
            {
                Random rand = new Random();
                while (true)
                {
                    // blood pressure
                    BloodPressureSys = (100 + rand.Next(20)).ToString();
                    BloodPressureDia = (60 + rand.Next(20)).ToString();
                    // temperature
                    Temperature = 36.ToString() + "." + rand.Next(0, 10).ToString() + " °C";

                    await Task.Delay(500); 
                }
            });
        }

        private void MonitorHeartRate(string message)
        {
            if (double.TryParse(message, out double bpm))
            {
                // call the dispatcher to move from the background thread to ui thread
                App.Current.Dispatcher.BeginInvoke(new Action(() =>
                {
                    // ui thread
                    HeartRateValues.Add(bpm);
                    if (HeartRateValues.Count > 100) HeartRateValues.RemoveAt(0);
                    BpmNumber = bpm.ToString();
                }));
            }
        }

        private void RemovePatient(object sender)
        {
            // remove patient from the room
            // first confirmation
            string message1 = "Confirm";
            string title1 = "Confirmation";
            MessageBoxButton buttons = MessageBoxButton.YesNo;
            MessageBoxImage icon = MessageBoxImage.Warning;

            MessageBoxResult result1 = MessageBox.Show(message1, title1, buttons, icon);
            if (result1 != MessageBoxResult.Yes) return;

            // second confirmation
            string message2 = "Are you sure you want to discharge patient?";
            string title2 = "Confirm discharge";

            MessageBoxResult result2 = MessageBox.Show(message2, title2, buttons, icon);
            if (result2 != MessageBoxResult.Yes) return;

            // after confirmation
            DatabaseFunctions.RemovePatientFromRoom(_roomInfo.RoomId);
            _goBack();
        }

        
        int _beatIndex = 0;
        private void SimulateEkg(object sender, EventArgs e)
        {
            HeartRateValues.Add(ekgBeat[_beatIndex % ekgBeat.Length]);
            _beatIndex++;
            if (HeartRateValues.Count > 100) HeartRateValues.RemoveAt(0);
        }

        // One complete heartbeat cycle — repeat this pattern for continuous EKG
        double[] ekgBeat = new double[]
        {
        // Flatline before P wave
        0.0, 0.0, 0.0, 0.0, 0.0,

        // P wave (small bump)
        0.1, 0.2, 0.3, 0.25, 0.1, 0.0,

        // PR segment (flat)
        0.0, 0.0, 0.0,

        // QRS complex (sharp spike — the iconic part)
        -0.1,           // Q dip
        2.0,  // R peak (tall, sharp)
        -1.2,    // S dip
        -0.1, 0.0,      // return to baseline

        // ST segment (flat)
        0.0, 0.0, 0.0,

        // T wave (gentle bump)
        0.1, 0.25, 0.35, 0.3, 0.2, 0.1, 0.0,

        // Flatline after beat
        0.0, 0.0, 0.0, 0.0, 0.0, 0.0, 0.0, 0.0
        };
    }
}
