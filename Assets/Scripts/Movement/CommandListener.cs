namespace Movement
{
	public class CommandListener
	{
		private ISteppingCommand _steppingCommand;
		private PersonFootstepsStateMachine _stateMachine;

		public CommandListener(PersonFootstepsStateMachine stateMachine)
		{
			_stateMachine = stateMachine;
		}

		public void SetCommand(ISteppingCommand steppingCommand)
		{
			_steppingCommand = steppingCommand;
		}

		public void ExecuteCommand()
		{
			_steppingCommand.Execute(_stateMachine);
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

	public class Jumping : ISteppingCommand
	{
		public void Execute(PersonFootstepsStateMachine machine)
		{
			machine.Jump();
		}
	}
	
	public class Flying : ISteppingCommand
	{
		public void Execute(PersonFootstepsStateMachine machine)
		{
			machine.Flying();
		}
	}

	public class Landing : ISteppingCommand
	{
		public void Execute(PersonFootstepsStateMachine machine)
		{
			machine.Landing();
		}
	}
}