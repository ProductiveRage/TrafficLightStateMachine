using System;
using System.Collections.Generic;
using TrafficLightStateMachine.States;

namespace TrafficLightStateMachine
{
	public class TrafficLight
	{
		private readonly Stack<IAmATrafficLightState> _states;
		public TrafficLight(IAmATrafficLightState initialState)
		{
			if (initialState == null)
				throw new ArgumentNullException("initialState");

			_states = new Stack<IAmATrafficLightState>();
			_states.Push(initialState);
		}

		public ColourOptions Colour
		{
			get { return _states.Peek().Colour; }
		}

		public void RegisterCarQueueing()
		{
			ApplyTransition(_states.Peek().RegisterCarQueueing());
		}

		/// <summary>
		/// This will represent the passing of an arbitrary slice of time. The "real time" duration of it is not important, its duration could be decreased or increased
		/// to make the simulation proceed more quickly or more slowly.
		/// </summary>
		public void RegisterPassageOfTime()
		{
			ApplyTransition(_states.Peek().RegisterPassageOfTime());
		}

		private void ApplyTransition(StateTransition transition)
		{
			if (transition == null)
				throw new ArgumentNullException("transition");

			var previousColour = _states.Peek().Colour;
			if (transition.TransitionType == StateTransition.TransitionTypeOptions.NoChange)
			{
				// Do nothing
			}
			else if (transition.TransitionType == StateTransition.TransitionTypeOptions.Pop)
			{
				if (_states.Count == 1)
					throw new ArgumentException("Invalid transition - may not remove last state in the stack");
				_states.Pop();
			}
			else if (transition.TransitionType == StateTransition.TransitionTypeOptions.Push)
				_states.Push(transition.NewState);
			else if (transition.TransitionType == StateTransition.TransitionTypeOptions.Replace)
			{
				_states.Pop();
				_states.Push(transition.NewState);
			}
			else
				throw new ArgumentException("Unsupported transition type: " + transition.TransitionType);
			var newColour = _states.Peek().Colour;
			if (newColour != previousColour)
				Console.WriteLine("* Colour changed from " + previousColour + " to " + newColour);
		}
	}
}
