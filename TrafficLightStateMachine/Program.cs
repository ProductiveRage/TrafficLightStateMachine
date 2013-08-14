using System;
using System.Threading;
using TrafficLightStateMachine.States;
using TrafficLightStateMachine.States.Specific;

namespace TrafficLightStateMachine
{
	class Program
	{
		static void Main(string[] args)
		{
			// This controls how fast the simulation proceeds at
			var baseTimeSlice = TimeSpan.FromMilliseconds(100);

			// This is the chance that each time slice a car arrives
			var probabilityOfCarArrivingEachTimeSlice = 0.1;

			var rnd = new Random();
			var trafficLight = new TrafficLight(new RedLightWaitingForTraffic());
			while (true)
			{
				if (rnd.NextDouble() >= (1 - probabilityOfCarArrivingEachTimeSlice))
				{
					var trafficLightColour = trafficLight.Colour;
					if (trafficLightColour == ColourOptions.GreenOnly)
						Console.WriteLine("Car didn't have to queue, went straight through");
					else if (trafficLightColour == ColourOptions.YellowOnly)
						Console.WriteLine("Car didn't have to queue, went straight through (naughty!)");
					else
					{
						Console.WriteLine("Register car queuing..");
						trafficLight.RegisterCarQueueing();
					}
				}

				Thread.Sleep(TimeSpan.FromMilliseconds(baseTimeSlice.TotalMilliseconds));
				trafficLight.RegisterPassageOfTime();
			}
		}
	}
}
