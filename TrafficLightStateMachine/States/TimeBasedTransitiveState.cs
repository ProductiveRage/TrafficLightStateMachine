using System;

namespace TrafficLightStateMachine.States
{
	/// <summary>
	/// This represents a state that is predetermined to change and that may only be affected by the passing of time, any more cars queuing up at the light will
	/// have no effect. This may be a green light that will stay green for a fixed period of time before cycling back through red-and-yellow to red.
	/// </summary>
	public class TimeBasedTransitiveState : IAmATrafficLightState
	{
		private readonly int _timeSlicesToWaitFor;
		private readonly StateTransition _nextState;
		public TimeBasedTransitiveState(ColourOptions colour, StatusOptions status, int timeSlicesToWaitFor, StateTransition nextTransition)
		{
			if (!Enum.IsDefined(typeof(ColourOptions), colour))
				throw new ArgumentOutOfRangeException("colour");
			if (!Enum.IsDefined(typeof(StatusOptions), status))
				throw new ArgumentOutOfRangeException("status");
			if (timeSlicesToWaitFor <= 0)
				throw new ArgumentOutOfRangeException("timeSlicesToWaitFor");
			if (nextTransition == null)
				throw new ArgumentNullException("nextTransition");

			Colour = colour;
			Status = status;
			_timeSlicesToWaitFor = timeSlicesToWaitFor;
			_nextState = nextTransition;
		}

		public ColourOptions Colour { get; private set; }
		public StatusOptions Status { get; private set; }

		public StateTransition RegisterCarQueueing()
		{
			return StateTransition.NoChange();
		}

		/// <summary>
		/// This will represent the passing of an arbitrary slice of time. The "real time" duration of it is not important, its duration could be decreased or increased
		/// to make the simulation proceed more quickly or more slowly.
		/// </summary>
		public StateTransition RegisterPassageOfTime()
		{
			if (_timeSlicesToWaitFor == 1)
				return _nextState;

			return StateTransition.Replace(
				new TimeBasedTransitiveState(Colour, Status, _timeSlicesToWaitFor - 1, _nextState)
			);
		}
	}
}
