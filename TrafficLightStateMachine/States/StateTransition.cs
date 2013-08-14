using System;

namespace TrafficLightStateMachine.States
{
	public class StateTransition
	{
		private StateTransition(TransitionTypeOptions transitionType, IAmATrafficLightState newState)
		{
			if (!Enum.IsDefined(typeof(TransitionTypeOptions), transitionType))
				throw new ArgumentOutOfRangeException("transitionType");
			if ((transitionType == TransitionTypeOptions.NoChange) || (transitionType == TransitionTypeOptions.Pop))
			{
				if (newState != null)
					throw new ArgumentException("newState must be null if transitionType is NoChange or Pop");
			}
			else if (newState == null)
				throw new ArgumentException("newState must be non-null if transitionType is Push or Replace");

			TransitionType = transitionType;
			NewState = newState;
		}

		public static StateTransition NoChange() { return new StateTransition(TransitionTypeOptions.NoChange, null); }
		public static StateTransition Pop() { return new StateTransition(TransitionTypeOptions.Pop, null); }
		public static StateTransition Push(IAmATrafficLightState state) { return new StateTransition(TransitionTypeOptions.Push, state); }
		public static StateTransition Replace(IAmATrafficLightState state) { return new StateTransition(TransitionTypeOptions.Replace, state); }

		public enum TransitionTypeOptions
		{
			NoChange,
			Pop,
			Push,
			Replace
		}

		public TransitionTypeOptions TransitionType { get; private set; }
		
		/// <summary>
		/// This will be null if TransitionType is NoChange or Pop and non-null if TransitionType is Push or Replace
		/// </summary>
		public IAmATrafficLightState NewState { get; private set; }
	}
}
