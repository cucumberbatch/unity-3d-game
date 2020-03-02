using UnityEngine;

namespace Movement
{
	public class PersonFootstepsStateMachine
	{
		public IState state;
		public float timeToStep;
		public AudioClip footstepSound;
		public AudioClip jumpSound;
		public AudioSource audioSource;

		internal float _toStepCycles = 0.05f;
		internal float _waitingTime = 0.0f;

		public PersonFootstepsStateMachine(IState state, float timeToStep, AudioClip footstepSound, AudioClip jumpSound, AudioSource audioSource)
		{
			this.state = state;
			this.timeToStep = timeToStep;
			this.footstepSound = footstepSound;
			this.jumpSound = jumpSound;
			this.audioSource = audioSource;
		}

		public void Landing()
		{
			state.Landing(this);
		}
		
		public void Jump()
		{
			state.Jump(this);
		}
		
		public void Flying()
		{
			state.Flying(this);
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
	}

	public interface IState
	{
		void Landing(PersonFootstepsStateMachine machine);
		void Jump(PersonFootstepsStateMachine machine);
		void Flying(PersonFootstepsStateMachine machine);
		void Running(PersonFootstepsStateMachine machine);
		void Walking(PersonFootstepsStateMachine machine);
		void Standing(PersonFootstepsStateMachine machine);
	}

	public class Idle : IState
	{
		public void Landing(PersonFootstepsStateMachine machine)
		{
			machine.audioSource.PlayOneShot(machine.footstepSound);
		}

		public void Jump(PersonFootstepsStateMachine machine)
		{
			machine.audioSource.PlayOneShot(machine.jumpSound);
		}

		public void Flying(PersonFootstepsStateMachine machine)
		{
			return;
		}

		public void Running(PersonFootstepsStateMachine machine)
		{
			Walking(machine);
		}
		
		public void Walking(PersonFootstepsStateMachine machine)
		{
			machine.audioSource.PlayOneShot(machine.footstepSound);
			
			machine.state = new WaitingForStep();
		}

		public void Standing(PersonFootstepsStateMachine machine)
		{
			return;
		}
	}

	public class WaitingForStep : IState
	{
		public void Landing(PersonFootstepsStateMachine machine)
		{
			machine.audioSource.PlayOneShot(machine.footstepSound);
		}

		public void Jump(PersonFootstepsStateMachine machine)
		{
			machine.audioSource.PlayOneShot(machine.jumpSound);
			machine.state = new Idle();
		}

		public void Flying(PersonFootstepsStateMachine machine)
		{
			return;
		}
		
		public void Running(PersonFootstepsStateMachine machine)
		{
			if (machine._waitingTime < machine.timeToStep)
			{
				machine._waitingTime += machine._toStepCycles * 1.75f;
				return;
			}
		
			machine.audioSource.PlayOneShot(machine.footstepSound);
			machine._waitingTime = .0f;
		}
		
		public void Walking(PersonFootstepsStateMachine machine)
		{
			if (machine._waitingTime < machine.timeToStep)
			{
				machine._waitingTime += machine._toStepCycles;
				return;
			}
		
			machine.audioSource.PlayOneShot(machine.footstepSound);
			machine._waitingTime = .0f;
		}
	
		public void Standing(PersonFootstepsStateMachine machine)
		{
			if (machine._waitingTime > machine.timeToStep / 1.25)
			{
				machine.audioSource.PlayOneShot(machine.footstepSound);
			}
			
			machine.state = new Idle();
		}
	}
}