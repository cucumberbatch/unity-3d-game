namespace Movement
{
	public class PersonFootstepsStateMachine
	{
		public static readonly IState Standing         = new StandingState();
		public static readonly IState Walking          = new WalkingState();
		public static readonly IState Flying           = new FlyingState();
		
		public IState 			state;
		public FootstepsPlayer 	player;
		public float 			timeToStep;

		internal float _walkIncrement 	= 0.05f;
		internal float _runIncrement 	= 0.08f;
		internal float _waitingTime;

		public PersonFootstepsStateMachine(FootstepsPlayer player, float timeToStep)
		{
			this.player 	= player;
			this.timeToStep = timeToStep;
			state 			= Standing;
		}

		public void Execute(ISteppingCommand command)
		{
			command.Execute(this);
		}

		public void PlayFootstepsSound()
		{
			player.PlayFootstepsSound();
		}

		public void PlayJumpSound()
		{
			player.PlayJumpSound();
		}
	}

	public class FlyingState : IState
	{
		public void Flying(PersonFootstepsStateMachine machine) { }

		public void Running(PersonFootstepsStateMachine machine)
		{
			Walking(machine);
		}

		public void Walking(PersonFootstepsStateMachine machine)
		{
			machine.PlayFootstepsSound();
			machine.state = PersonFootstepsStateMachine.Walking;
		}

		public void Standing(PersonFootstepsStateMachine machine)
		{ 
			machine.PlayFootstepsSound();
			machine.state = PersonFootstepsStateMachine.Standing;
		}
	}

	public class WalkingState : IState
	{
		public void Flying(PersonFootstepsStateMachine machine)
		{
			machine.PlayJumpSound();
			machine.state = PersonFootstepsStateMachine.Flying;
		}

		public void Running(PersonFootstepsStateMachine machine)
		{
			if (machine._waitingTime < machine.timeToStep)
			{
				machine._waitingTime += machine._runIncrement;
				return;
			}
		
			machine._waitingTime = .0f;
			machine.PlayFootstepsSound();
		}

		public void Walking(PersonFootstepsStateMachine machine)
		{
			if (machine._waitingTime < machine.timeToStep)
			{
				machine._waitingTime += machine._walkIncrement;
				return;
			}
		
			machine._waitingTime = .0f;
			machine.PlayFootstepsSound();
		}

		public void Standing(PersonFootstepsStateMachine machine)
		{
			machine._waitingTime = .0f;
			machine.state = PersonFootstepsStateMachine.Standing;
		}
	}

	public class StandingState : IState
	{
		public void Flying(PersonFootstepsStateMachine machine)
		{
			machine.PlayJumpSound();
			machine.state = PersonFootstepsStateMachine.Flying;
		}

		public void Running(PersonFootstepsStateMachine machine)
		{
			Walking(machine);
		}

		public void Walking(PersonFootstepsStateMachine machine)
		{
			machine.PlayFootstepsSound();
			
			machine.state = PersonFootstepsStateMachine.Walking;
		}

		public void Standing(PersonFootstepsStateMachine machine) { }
	}

	public interface IState
	{
		void Flying(PersonFootstepsStateMachine machine);
		void Running(PersonFootstepsStateMachine machine);
		void Walking(PersonFootstepsStateMachine machine);
		void Standing(PersonFootstepsStateMachine machine);
	}
	
}