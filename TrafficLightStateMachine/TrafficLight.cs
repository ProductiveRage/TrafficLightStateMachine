using System;
using TrafficLightStateMachine.States;

namespace TrafficLightStateMachine
{
	public class TrafficLight
	{
		private IAmATrafficLightState _state;
		public TrafficLight(IAmATrafficLightState initialState)
		{
			if (initialState == null)
				throw new ArgumentNullException("initialState");

			_state = initialState;
		}

		public ColourOptions Colour
		{
			get { return _state.Colour; }
		}

		public void RegisterCarQueueing()
		{
			var previousColour = _state.Colour;
			_state = _state.RegisterCarQueueing();
			if (_state.Colour != previousColour)
				Console.WriteLine("* Colour changed from " + previousColour + " to " + _state.Colour);
		}

		/// <summary>
		/// This will represent the passing of an arbitrary slice of time. The "real time" duration of it is not important, its duration could be decreased or increased
		/// to make the simulation proceed more quickly or more slowly.
		/// </summary>
		public void RegisterPassageOfTime()
		{
			var previousColour = _state.Colour;
			_state = _state.RegisterPassageOfTime();
			if (_state.Colour != previousColour)
				Console.WriteLine("* Colour changed from " + previousColour + " to " + _state.Colour);
		}
	}
}
