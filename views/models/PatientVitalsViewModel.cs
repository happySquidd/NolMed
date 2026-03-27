using LiveCharts;
using LiveCharts.Wpf;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

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
                    LineSmoothness = 0.5
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
                Random rand = new Random();
                while (true)
                {
                    // Simulate heart rate between 60-100
                    double newHeartRate = 60 + rand.NextDouble() * 40;
                    App.Current.Dispatcher.Invoke(() =>
                    {
                        HeartRateValues.Add(newHeartRate);
                        // Keep only the latest 30 values
                        if (HeartRateValues.Count > 30) HeartRateValues.RemoveAt(0);
                    });
                    // Update every second
                    await Task.Delay(1000); 
                }
            });
        }
    }
}
