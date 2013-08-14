using System;

namespace TrafficLightStateMachine.States
{
	/// <summary>
	/// This represents a state that is predetermined to change and that may only be affected by the passing of time, any more cars queuing up at the light will
	/// have no effect. This may be a green light that will stay green for a fixed period of time before cycling back through red-and-yellow to red.
	/// </summary>
	public abstract class TimeBasedTransitiveState : IAmATrafficLightState
	{
		private readonly int _timeSlicesToWaitFor;
		private readonly StateTransition _nextState;
		protected TimeBasedTransitiveState(int timeSlicesToWaitFor, StateTransition nextState)
		{
			if (timeSlicesToWaitFor <= 0)
				throw new ArgumentOutOfRangeException("timeSlicesToWaitFor");
			if (nextState == null)
				throw new ArgumentNullException("nextState");

			_timeSlicesToWaitFor = timeSlicesToWaitFor;
			_nextState = nextState;
		}

		public abstract ColourOptions Colour { get; }

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
				new TimeBasedTransitiveStateInstance(this, _timeSlicesToWaitFor - 1, _nextState)
			);
		}

		/// <summary>
		/// This is used to describe the states that are passed through while other classes derived from TimeBasedTransitiveState count down until they are allowed
		/// to reach their "nextState" reference
		/// </summary>
		private class TimeBasedTransitiveStateInstance : TimeBasedTransitiveState
		{
			public TimeBasedTransitiveStateInstance(IAmATrafficLightState source, int timeSlicesToWaitFor, StateTransition nextState)
				: base(timeSlicesToWaitFor, nextState)
			{
				if (source == null)
					throw new ArgumentNullException("source");

				Source = (source is TimeBasedTransitiveStateInstance)
					? ((TimeBasedTransitiveStateInstance)source).Source
					: source;
			}

			/// <summary>
			/// This will never be null and will never be a TimeBasedTransitiveStateInstance, it will always be the state that inherited TimeBasedTransitiveState
			/// and that has transitioned into a TimeBasedTransitiveStateInstance until the timer ticks down before the "nextState" is arrived at
			/// </summary>
			public IAmATrafficLightState Source { get; private set; }

			public override ColourOptions Colour { get { return Source.Colour; } }
		}
	}
}
