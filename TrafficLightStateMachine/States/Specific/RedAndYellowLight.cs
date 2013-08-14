using System;

namespace TrafficLightStateMachine.States.Specific
{
	/// <summary>
	/// This is the time spent in the stage between Green changing back to Red
	/// </summary>
	public class RedAndYellowLight : TimeBasedTransitiveState
	{
		public const int TIME_TO_WAIT_ON_RED_AND_YELLOW = 5;

		public RedAndYellowLight() : base(TIME_TO_WAIT_ON_RED_AND_YELLOW, StateTransition.Replace(new GreenLight())) { }

		public override ColourOptions Colour { get { return ColourOptions.RedAndYellow; } }
		public override StatusOptions Status { get { return StatusOptions.HandlingTraffic; } }
	}
}
