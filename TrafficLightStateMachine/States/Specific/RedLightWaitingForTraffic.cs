using System;

namespace TrafficLightStateMachine.States.Specific
{
	/// <summary>
	/// No-one is queuing up, this is a red light with no intention of changing
	/// </summary>
	public class RedLightWaitingForTraffic : IAmATrafficLightState
	{
		public ColourOptions Colour { get { return ColourOptions.RedOnly; } }

		public IAmATrafficLightState RegisterCarQueueing()
		{
			return new RedLightAboutToChangeLight();
		}

		/// <summary>
		/// This will represent the passing of an arbitrary slice of time. The "real time" duration of it is not important, its duration could be decreased or
		/// increased to make the simulation proceed more quickly or more slowly.
		/// </summary>
		public IAmATrafficLightState RegisterPassageOfTime()
		{
			// If all that's happening is that time is ticking along then there is nothing to action here
			return this;
		}
	}
}
