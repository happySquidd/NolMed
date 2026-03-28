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
        public ChartValues<double> HeartRateValues { get; set; }
        public SeriesCollection ChartSeries { get; set; }

        private string _placeholderText;
        public string PlaceholderText
        {
            get => _placeholderText;
            set { _placeholderText = value; OnPropertyChanged(); }
        }

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

            SubscribeToHeartRate();
        }

        private void SubscribeToHeartRate()
        {
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
                    });
                    // Update every second
                    await Task.Delay(200); 
                }
            });
        }
    }
}
