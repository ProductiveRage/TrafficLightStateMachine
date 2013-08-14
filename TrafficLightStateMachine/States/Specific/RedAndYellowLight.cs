using System;

namespace TrafficLightStateMachine.States.Specific
{
	/// <summary>
	/// This is the time spent in the stage between Green changing back to Red
	/// </summary>
	public class RedAndYellowLight : TimeBasedTransitiveState
	{
		public const int TIME_TO_WAIT_ON_RED_AND_YELLOW = 5;
		public RedAndYellowLight() : base(
			ColourOptions.RedAndYellow,
			StatusOptions.HandlingTraffic,
			TIME_TO_WAIT_ON_RED_AND_YELLOW,
			StateTransition.Replace(new GreenLight())) { }
	}
}
