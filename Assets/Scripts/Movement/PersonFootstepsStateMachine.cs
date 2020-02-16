namespace Movement
{
	public class PersonFootstepsStateMachine
	{
		public IState state;
		public float timeToStep;
		public bool playStepSound;
		
		internal float _toStepCycles = 0.05f;
		internal float _waitingTime = 0.0f;

		public PersonFootstepsStateMachine(IState state, float timeToStep)
		{
			this.state = state;
			this.timeToStep = timeToStep;
		}

		public void Running()
		{
			state.Running(this);
		}
		
		public void Walking()
		{
			state.Walking(this);
		}

		public void Standing()
		{
			state.Standing(this);
		}

		public bool IsPlayingStepSound()
		{
			return playStepSound;
		}
	}

	public interface IState
	{
		void Running(PersonFootstepsStateMachine machine);
		void Walking(PersonFootstepsStateMachine machine);
		void Standing(PersonFootstepsStateMachine machine);
	}

	public class Idle : IState
	{
		public void Running(PersonFootstepsStateMachine machine)
		{
			Walking(machine);
		}
		
		public void Walking(PersonFootstepsStateMachine machine)
		{
			machine.playStepSound = true;
			machine.state = new WaitingForStep();
		}

		public void Standing(PersonFootstepsStateMachine machine)
		{
			machine.playStepSound = false;
			return;
		}
	}

	public class WaitingForStep : IState
	{
		public void Running(PersonFootstepsStateMachine machine)
		{
			if (machine._waitingTime < machine.timeToStep)
			{
				machine._waitingTime += machine._toStepCycles * 1.75f;
				machine.playStepSound = false;
				return;
			}
		
			machine.playStepSound = true;
			machine._waitingTime = .0f;
		}
		
		public void Walking(PersonFootstepsStateMachine machine)
		{
			if (machine._waitingTime < machine.timeToStep)
			{
				machine._waitingTime += machine._toStepCycles;
				machine.playStepSound = false;
				return;
			}
		
			machine.playStepSound = true;
			machine._waitingTime = .0f;
		}
	
		public void Standing(PersonFootstepsStateMachine machine)
		{
			if (machine._waitingTime > machine.timeToStep / 1.25)
			{
				machine.playStepSound = true;
			}
			
			machine.state = new Idle();
		}
	}
}