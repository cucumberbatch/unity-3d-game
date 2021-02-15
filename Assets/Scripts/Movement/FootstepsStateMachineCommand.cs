namespace Movement
{
	public interface ISteppingCommand
	{
		void Execute(PersonFootstepsStateMachine machine);
	}

	public class WalkCommand : ISteppingCommand
	{
		public void Execute(PersonFootstepsStateMachine machine)
		{
			machine.state.Walking(machine);
		}
	}

	public class StandCommand : ISteppingCommand
	{
		public void Execute(PersonFootstepsStateMachine machine)
		{
			machine.state.Standing(machine);
		}
	}

	public class RunCommand : ISteppingCommand
	{
		public void Execute(PersonFootstepsStateMachine machine)
		{
			machine.state.Running(machine);
		}
	}

	public class FlyCommand : ISteppingCommand
	{
		public void Execute(PersonFootstepsStateMachine machine)
		{
			machine.state.Flying(machine);
		}
	}

}