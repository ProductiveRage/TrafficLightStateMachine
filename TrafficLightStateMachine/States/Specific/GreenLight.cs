using System;

namespace TrafficLightStateMachine.States.Specific
{
	/// <summary>
	/// This is the time spent in the stage between Green changing back to Red
	/// </summary>
	public class GreenLight : TimeBasedTransitiveState
	{
		public const int TIME_TO_STAY_ON_GREEN = 100;

		public GreenLight() : base(TIME_TO_STAY_ON_GREEN, StateTransition.Replace(new YellowLight())) { }

		public override ColourOptions Colour { get { return ColourOptions.GreenOnly; } }
		public override StatusOptions Status { get { return StatusOptions.HandlingTraffic; } }
	}
}
