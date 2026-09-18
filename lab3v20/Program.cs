using System;

namespace Lab3
{
    public class GPSTracker : IDisposable
    {
        private bool _disposed = false;
        private string _deviceId;
        private bool _isTracking;

        public string DeviceId => _deviceId;
        public bool IsTracking => _isTracking;

        public GPSTracker(string deviceId)
        {
            _deviceId = deviceId;
            _isTracking = true;
            Console.WriteLine($"[GPSTracker '{_deviceId}'] Трекер увімкнено. Відстеження розпочато.");
        }

        public void GetCurrentLocation()
        {
            if (_disposed)
            {
                throw new ObjectDisposedException(nameof(GPSTracker), "Неможливо отримати координати: трекер вимкнено/знищено.");
            }

            if (_isTracking)
            {
                Console.WriteLine($"[GPSTracker '{_deviceId}'] Поточні координати: 50.4501° N, 30.5234° E");
            }
            else
            {
                Console.WriteLine($"[GPSTracker '{_deviceId}'] Відстеження зупинено.");
            }
        }

        public void Dispose()
        {
            Dispose(true);
            GC.SuppressFinalize(this);
        }

        protected virtual void Dispose(bool disposing)
        {
            if (!_disposed)
            {
                if (disposing)
                {
                    Console.WriteLine($"[Dispose(true)] Звільнення керованих ресурсів для '{_deviceId}'.");
                }

                if (_isTracking)
                {
                    Console.WriteLine($"[Dispose] Зупинка відстеження (некерований ресурс) для '{_deviceId}'.");
                    _isTracking = false;
                }

                _disposed = true;
            }
        }

        ~GPSTracker()
        {
            Console.WriteLine($"[~GPSTracker] Деструктор викликано для '{_deviceId}'.");
            Dispose(false);
        }
    }

    class Program
    {
        static void Main(string[] args)
        {
            Console.OutputEncoding = System.Text.Encoding.UTF8;

            Console.WriteLine("=== Сценарій 1: Використання блоку using ===");
            using (var tracker1 = new GPSTracker("GPS-001"))
            {
                tracker1.GetCurrentLocation();
            }
            Console.WriteLine();

            Console.WriteLine("=== Сценарій 2: Явний виклик Dispose() ===");
            var tracker2 = new GPSTracker("GPS-002");
            tracker2.GetCurrentLocation();
            tracker2.Dispose();
            Console.WriteLine();

            Console.WriteLine("=== Сценарій 3: Робота деструктора через Garbage Collector ===");
            CreateAndForgetTracker();

            Console.WriteLine("Виклик GC.Collect()...");
            GC.Collect();
            GC.WaitForPendingFinalizers();

            Console.WriteLine("\nРоботу програми завершено.");
        }

        static void CreateAndForgetTracker()
        {
            var tracker3 = new GPSTracker("GPS-003");
            tracker3.GetCurrentLocation();
        }
    }
}