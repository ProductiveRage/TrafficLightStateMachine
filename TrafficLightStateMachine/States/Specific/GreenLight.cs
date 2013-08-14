using System;

namespace TrafficLightStateMachine.States.Specific
{
	/// <summary>
	/// This is the time spent in the stage between Green changing back to Red
	/// </summary>
	public class GreenLight : TimeBasedTransitiveState
	{
		public const int TIME_TO_STAY_ON_GREEN = 100;
		public GreenLight() : base(
			ColourOptions.GreenOnly,
			StatusOptions.HandlingTraffic,
			TIME_TO_STAY_ON_GREEN,
			StateTransition.Replace(new YellowLight())) { }
	}
}
