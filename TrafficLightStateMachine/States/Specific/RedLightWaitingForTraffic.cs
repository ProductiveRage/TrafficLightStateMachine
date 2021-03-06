﻿using System;

namespace TrafficLightStateMachine.States.Specific
{
	/// <summary>
	/// No-one is queuing up, this is a red light with no intention of changing
	/// </summary>
	public class RedLightWaitingForTraffic : IAmATrafficLightState
	{
		private readonly Func<bool> _isAllowedToLetTrafficThrough;
		public RedLightWaitingForTraffic(Func<bool> isAllowedToLetTrafficThrough)
		{
			if (isAllowedToLetTrafficThrough == null)
				throw new ArgumentNullException("isAllowedToLetTrafficThrough");

			_isAllowedToLetTrafficThrough = isAllowedToLetTrafficThrough;
		}

		public ColourOptions Colour { get { return ColourOptions.RedOnly; } }
		public StatusOptions Status { get { return StatusOptions.NotHandlingTraffic; } }

		public StateTransition RegisterCarQueueing()
		{
			if (_isAllowedToLetTrafficThrough())
				return StateTransition.Push(new RedLightAboutToChange());

			return StateTransition.Push(new RedLightWaitingForAccess(_isAllowedToLetTrafficThrough));
		}

		/// <summary>
		/// This will represent the passing of an arbitrary slice of time. The "real time" duration of it is not important, its duration could be decreased or
		/// increased to make the simulation proceed more quickly or more slowly.
		/// </summary>
		public StateTransition RegisterPassageOfTime()
		{
			// If all that's happening is that time is ticking along then there is nothing to action here
			return StateTransition.NoChange();
		}
	}
}
