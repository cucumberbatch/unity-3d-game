using System;
using UnityEngine;

namespace Movement
{

	public class CharacterFootsteps : MonoBehaviour
	{

		public GameObject player;
		public AudioClip footstepSound;
		public AudioClip jumpSound;
		public float timeToStep = 1.0f;

		private Movement _movementComponent;
		private PersonFootstepsStateMachine _stateMachine;
		private CommandListener _commandListener;
		private AudioSource _audioSource;


		private void Start()
		{
			_movementComponent = player.GetComponent<Movement>();
			_audioSource = GetComponent<AudioSource>();
			_stateMachine = new PersonFootstepsStateMachine(new Idle(), timeToStep, footstepSound, jumpSound, _audioSource);
			_commandListener = new CommandListener(_stateMachine);
		}

		private void Update()
		{
			ApplyCharacterFootstepsStrategy();
		}

		private void ApplyCharacterFootstepsStrategy()
		{
			switch (_movementComponent.MovementState)
			{
				case MovementState.Idle: 
					_commandListener.SetCommand(new Standing());
					break;
				
				case MovementState.Walking:
					_commandListener.SetCommand(new Walking());
					break;
				
				case MovementState.Running:
					_commandListener.SetCommand(new Running());
					break;
				
				case MovementState.Jumping:
					_commandListener.SetCommand(new Jumping());
					break;
				
				case MovementState.Flying:
					_commandListener.SetCommand(new Flying());
					break;
				
				case MovementState.Landing:
					_commandListener.SetCommand(new Landing());
					break;
				
				default:
					throw new ArgumentOutOfRangeException();
			}
			
			_commandListener.ExecuteCommand();
		}
	}
}