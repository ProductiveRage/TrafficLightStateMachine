using System;

namespace TrafficLightStateMachine.States.Specific
{
	/// <summary>
	/// This is a red light that wants to change state to let through queuing traffic but something is stopping it. It can determine when this dependency changes
	/// by consulting the specific isAllowedToLetTrafficThrough filter. This could be a red light at a crossroads where traffic is currently flowing on the
	/// perpendicular road, preventing traffic on this light's road from being allowed to leave.
	/// </summary>
	public class RedLightWaitingForAccess : IAmATrafficLightState
	{
		private readonly Func<bool> _isAllowedToLetTrafficThrough;
		public RedLightWaitingForAccess(Func<bool> isAllowedToLetTrafficThrough)
		{
			if (isAllowedToLetTrafficThrough == null)
				throw new ArgumentNullException("isAllowedToLetTrafficThrough");

			_isAllowedToLetTrafficThrough = isAllowedToLetTrafficThrough;
		}

		public ColourOptions Colour { get { return ColourOptions.RedOnly; } }
		public StatusOptions Status { get { return StatusOptions.NotHandlingTraffic; } }

		public StateTransition RegisterCarQueueing()
		{
			// We can't do anything here, we're already waiting
			return StateTransition.NoChange();
		}

		/// <summary>
		/// This will represent the passing of an arbitrary slice of time. The "real time" duration of it is not important, its duration could be decreased or
		/// increased to make the simulation proceed more quickly or more slowly.
		/// </summary>
		public StateTransition RegisterPassageOfTime()
		{
			if (_isAllowedToLetTrafficThrough())
				return StateTransition.Replace(new RedAndYellowLight());

			return StateTransition.NoChange();
		}
	}
}
