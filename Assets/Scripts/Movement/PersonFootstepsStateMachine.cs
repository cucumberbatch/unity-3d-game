using UnityEngine;

namespace Movement
{
	public class PersonFootstepsStateMachine
	{
		public static readonly IState Standing         = new StandingState();
		public static readonly IState Walking          = new WalkingState();
		public static readonly IState Flying           = new FlyingState();
		
		public IState state;
		public float timeToStep;
		public AudioClip[] footstepSounds;
		public AudioClip jumpSound;
		public AudioSource audioSource;

		internal float _toStepCycles = 0.05f;
		internal float _waitingTime = 0.0f;

		public PersonFootstepsStateMachine(IState state, float timeToStep, AudioClip[] footstepSounds, AudioClip jumpSound, AudioSource audioSource)
		{
			this.state = state;
			this.timeToStep = timeToStep;
			this.footstepSounds = footstepSounds;
			this.jumpSound = jumpSound;
			this.audioSource = audioSource;
		}

		public void Execute(ISteppingCommand command)
		{
			command.Execute(this);
		}

		public void playFootstepsSound()
		{
			int soundIndex = Random.Range(1, footstepSounds.Length);
			audioSource.PlayOneShot(footstepSounds[soundIndex]);
			AudioClip temporary = footstepSounds[0];
			footstepSounds[0] = footstepSounds[soundIndex];
			footstepSounds[soundIndex] = temporary;
		}

		public void playJumpSound()
		{
			audioSource.PlayOneShot(jumpSound);
		}
	}

	public class FlyingState : IState
	{
		public void Flying(PersonFootstepsStateMachine machine)
		{
			
		}

		public void Running(PersonFootstepsStateMachine machine)
		{
			Walking(machine);
		}

		public void Walking(PersonFootstepsStateMachine machine)
		{
			machine.playFootstepsSound();
			machine.state = PersonFootstepsStateMachine.Walking;
		}

		public void Standing(PersonFootstepsStateMachine machine)
		{ 
			machine.playFootstepsSound();
			machine.state = PersonFootstepsStateMachine.Standing;
		}
	}

	public class WalkingState : IState
	{
		public void Flying(PersonFootstepsStateMachine machine)
		{
			machine.playJumpSound();
			machine.state = PersonFootstepsStateMachine.Flying;
		}

		public void Running(PersonFootstepsStateMachine machine)
		{
			if (machine._waitingTime < machine.timeToStep)
			{
				machine._waitingTime += machine._toStepCycles * 1.85f;
				return;
			}
		
			machine._waitingTime = .0f;
			machine.playFootstepsSound();
		}

		public void Walking(PersonFootstepsStateMachine machine)
		{
			if (machine._waitingTime < machine.timeToStep)
			{
				machine._waitingTime += machine._toStepCycles;
				return;
			}
		
			machine._waitingTime = .0f;
			machine.playFootstepsSound();
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
			machine.playJumpSound();
			machine.state = PersonFootstepsStateMachine.Flying;
		}

		public void Running(PersonFootstepsStateMachine machine)
		{
			Walking(machine);
		}

		public void Walking(PersonFootstepsStateMachine machine)
		{
			machine.playFootstepsSound();
			
			machine.state = PersonFootstepsStateMachine.Walking;
		}

		public void Standing(PersonFootstepsStateMachine machine)
		{
			
		}
	}

	public interface IState
	{
		void Flying(PersonFootstepsStateMachine machine);
		void Running(PersonFootstepsStateMachine machine);
		void Walking(PersonFootstepsStateMachine machine);
		void Standing(PersonFootstepsStateMachine machine);
	}
	
}