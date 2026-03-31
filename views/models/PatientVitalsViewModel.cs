using LiveCharts;
using LiveCharts.Wpf;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
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
#endregion

        public PatientVitalsViewModel()
        {
            PlaceholderText = "Patient Vitals Tab";
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

            SubscribeToHeartRate();
        }

        private void SubscribeToHeartRate()
        {
            // TODO: dispose of this function when closed
            // TODO: connect to redis
            // Simulate heart rate updates
            Task.Run(async () =>
            {
                HeartRateValues.Add(0);
                Random rand = new Random();
                while (true)
                {
                    // Simulate heart rate between 60-100
                    double newHeartRate = 60 + rand.NextDouble() * 40;
                    App.Current.Dispatcher.Invoke(() =>
                    {
                        HeartRateValues.Add(newHeartRate);
                        // Keep only the latest values
                        if (HeartRateValues.Count > 100) HeartRateValues.RemoveAt(0);
                        BpmNumber = ((int)newHeartRate).ToString() + " bpm";
                        // blood pressure
                        BloodPressureSys = (100 + rand.Next(20)).ToString();
                        BloodPressureDia = (60 + rand.Next(20)).ToString();
                        // temperature
                        Temperature = 36.ToString() + "." + rand.Next(0, 10).ToString() + " °C";
                    });
                    await Task.Delay(500); 
                    UpdateYAxis();
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
    }
}
