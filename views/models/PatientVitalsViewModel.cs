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
using System.Windows.Input;
using System.Windows.Media;

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
        private string _roomNumber;
        public string RoomNumber
        {
            get => _roomNumber;
            set { _roomNumber = value; OnPropertyChanged(); }
        }
        public ICommand BackClicked { get; }
#endregion

        public PatientVitalsViewModel(int roomNum, Action goBack)
        {
            BackClicked = new RelayCommand(_ => goBack());
            PlaceholderText = "Patient Vitals";
            RoomNumber = $"Room: {roomNum}";
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
            YMin = 40;
            YMax = 120;

            // function to simulate blood pressure and temperature
            GenerateVitals();
            // subscribe to room
            App.Redis.SubscribeToRoom(roomNum, MonitorHeartRate);
        }

        private void GenerateVitals()
        {
            // Simulate blood pressure and temperature
            Task.Run(async () =>
            {
                Random rand = new Random();
                while (true)
                {
                    App.Current.Dispatcher.Invoke(() =>
                    {
                        // blood pressure
                        BloodPressureSys = (100 + rand.Next(20)).ToString();
                        BloodPressureDia = (60 + rand.Next(20)).ToString();
                        // temperature
                        Temperature = 36.ToString() + "." + rand.Next(0, 10).ToString() + " °C";
                    });
                    await Task.Delay(500); 
                }
            });
        }

        private void UpdateYAxis()
        {
            // TODO: create a smoother scaling algorithm
            if (HeartRateValues.Count == 0) return;
            var min = HeartRateValues.Min();
            var max = HeartRateValues.Max();

            YMin = min - ChartPadding;
            YMax = max + ChartPadding;
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
                    UpdateYAxis();
                }));
            }
        }
    }
}
