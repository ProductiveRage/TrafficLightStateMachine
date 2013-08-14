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

				// The eastWestTrafficLight reference is required by the isAllowedToLetTrafficThrough filter passed to the initial RedLightWaitingForTraffic
				// state for the North-South traffic light so it has to be set to something. At this point that has to be null but it will be set to the real
				// value immediately after. The filter won't be used until the RegisterCarQueueing and RegisterPassageOfTime methods are called, so it doesn't
				// matter that the filter temporarily has a null reference.
				TrafficLight eastWestTrafficLight = null;
				var northSouthTrafficLight = new TrafficLight(
					"N-S",
					new RedLightWaitingForTraffic(
						() => (eastWestTrafficLight.Status == StatusOptions.NotHandlingTraffic)
					)
				);
				eastWestTrafficLight = new TrafficLight(
					"E-W",
					new RedLightWaitingForTraffic(
						() => (northSouthTrafficLight.Status == StatusOptions.NotHandlingTraffic)
					)
				);

				var allTrafficLights = new[] { northSouthTrafficLight, eastWestTrafficLight };
				var rnd = new Random();
				while (true)
				{
					foreach (var trafficLight in allTrafficLights)
					{
						if (rnd.NextDouble() < probabilityOfCarArrivingEachTimeSlice)
						{
							var trafficLightColour = trafficLight.Colour;
							if (trafficLightColour == ColourOptions.GreenOnly)
								Console.WriteLine("Car didn't have to queue {0}, went straight through", trafficLight.TrafficLightId);
							else if (trafficLightColour == ColourOptions.YellowOnly)
								Console.WriteLine("Car didn't have to queue {0}, went straight through (naughty!)", trafficLight.TrafficLightId);
							else
							{
								Console.WriteLine("Register car queuing {0}..", trafficLight.TrafficLightId);
								trafficLight.RegisterCarQueueing();
							}
						}
					}

					Thread.Sleep(TimeSpan.FromMilliseconds(baseTimeSlice.TotalMilliseconds));

					foreach (var trafficLight in allTrafficLights)
						trafficLight.RegisterPassageOfTime();
				}
			}
		}
}
