using System;

namespace TrafficLightStateMachine.States.Specific
{
	/// <summary>
	/// After a light goes back to Red, there is a short delay before it will consider any queuing traffic. This is to ensure that if there are traffic lights that interact
	/// with each other, then if one is having to wait to allow traffic that it has an opportunity to let it the traffic flow after the current light has reset to Red.
	/// </summary>
	public class RedLightPausedBeforeWaitingForTraffic : TimeBasedTransitiveState
	{
		private const int TIME_AFTER_RESETTING_TO_RED_BEFORE_CONSIDERING_TRAFFIC = 5;

		public RedLightPausedBeforeWaitingForTraffic() : base(TIME_AFTER_RESETTING_TO_RED_BEFORE_CONSIDERING_TRAFFIC, StateTransition.Pop()) { }

		public override ColourOptions Colour { get { return ColourOptions.RedOnly; } }
		public override StatusOptions Status { get { return StatusOptions.NotHandlingTraffic; } }
	}
}
