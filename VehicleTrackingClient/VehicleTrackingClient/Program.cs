using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace VehicleTrackingClient
{
    class Program
    {
        static void Main(string[] args)
        {
            Console.WriteLine("Invoking VehicleTrackingClient.....\n");

            var tasks = new List<Task<int>>();
            for (var i = 0; i < Constants.NoOfDevice; i++)
            {
                tasks.Add(Task<int>.Factory.StartNew(InvokeClient));
            }

            var allTask = Task.WhenAll(tasks.ToArray());
            var sum = allTask.Result.Sum();

            Console.WriteLine(sum == Constants.NoOfDevice
                                ? "\nCompleted Invoking VehicleTrackingClient....."
                                : "\nAny of the task did not execute!");
            Console.ReadLine();
        }

        public static int InvokeClient()
        {
            var ob = new Client();
            return ob.Invoke().Result;
        }
    }
}
