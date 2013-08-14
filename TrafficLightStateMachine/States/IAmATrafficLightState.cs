namespace TrafficLightStateMachine.States
{
	public interface IAmATrafficLightState
	{
		ColourOptions Colour { get; }
		
		StatusOptions Status { get; }

		StateTransition RegisterCarQueueing();

		/// <summary>
		/// This will represent the passing of an arbitrary slice of time. The "real time" duration of it is not important, its duration could be decreased or
		/// increased to make the simulation proceed more quickly or more slowly.
		/// </summary>
		StateTransition RegisterPassageOfTime();
	}
}
