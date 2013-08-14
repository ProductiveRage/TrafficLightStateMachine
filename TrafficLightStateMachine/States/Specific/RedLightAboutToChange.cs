using System;

namespace TrafficLightStateMachine.States.Specific
{
	/// <summary>
	/// When a car is first registered at a traffic light that is on red, waiting for traffic, there will be a short delay before changing for "traffic easing"
	/// </summary>
	public class RedLightAboutToChange : TimeBasedTransitiveState
	{
		public const int TIME_TO_STAY_RED_AFTER_CAR_ARRIVES = 10;

		public RedLightAboutToChange() : base(TIME_TO_STAY_RED_AFTER_CAR_ARRIVES, StateTransition.Replace(new RedAndYellowLight())) { }

		public override ColourOptions Colour { get { return ColourOptions.RedOnly; } }
		
		/// <summary>
		/// We're committed to letting traffic pass at this point so declare HandlingTraffic
		/// </summary>
		public override StatusOptions Status { get { return StatusOptions.HandlingTraffic; } }
	}
}
