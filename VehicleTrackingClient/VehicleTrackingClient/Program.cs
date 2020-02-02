using System;
using System.Threading.Tasks;

namespace VehicleTrackingClient
{
    class Program
    {
        static void Main(string[] args)
        {
            Console.WriteLine("Invoking VehicleTrackingClient.....");

            for (var i = 0; i < Constants.NoOfDevice; i++)
            {
                var i1 = i;
                var task = new Task(() =>
                {
                    var ob = new Client(i1);
                    ob.Invoke();
                });
                task.Start();
            }

            Console.WriteLine("Completed Invoking VehicleTrackingClient.....");
            Console.ReadLine();
        }
    }
}
