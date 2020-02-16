namespace Movement
{
	public class CommandListener
	{
		private ISteppingCommand _steppingCommand;
		private PersonFootstepsStateMachine machine;

		public CommandListener(PersonFootstepsStateMachine machine)
		{
			this.machine = machine;
		}

		public void SetCommand(ISteppingCommand steppingCommand)
		{
			_steppingCommand = steppingCommand;
		}

		public void ExecuteCommand()
		{
			_steppingCommand.Execute(machine);
		}
	}

	public interface ISteppingCommand
	{
		void Execute(PersonFootstepsStateMachine machine);
	}

	public class Walking : ISteppingCommand
	{
		public void Execute(PersonFootstepsStateMachine machine)
		{
			machine.Walking();
		}
	}

	public class Standing : ISteppingCommand
	{
		public void Execute(PersonFootstepsStateMachine machine)
		{
			machine.Standing();
		}
	}

	public class Running : ISteppingCommand
	{
		public void Execute(PersonFootstepsStateMachine machine)
		{
			machine.Running();
		}
	}
}