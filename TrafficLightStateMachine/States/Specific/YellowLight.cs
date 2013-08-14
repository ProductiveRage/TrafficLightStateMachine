﻿using System;

namespace TrafficLightStateMachine.States.Specific
{
	/// <summary>
	/// This is the time spent in the stage between Green changing back to Red
	/// </summary>
	public class YellowLight : TimeBasedTransitiveState
	{
		private const int TIME_TO_WAIT_ON_YELLOW = 5;

		public YellowLight() : base(TIME_TO_WAIT_ON_YELLOW, new RedLightWaitingForTraffic()) { }

		public override ColourOptions Colour { get { return ColourOptions.YellowOnly; } }
	}
}