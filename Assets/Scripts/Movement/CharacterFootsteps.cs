using System;
using System.Collections;
using UnityEngine;

namespace Movement
{

	public class CharacterFootsteps : MonoBehaviour
	{

		public GameObject player;
		public AudioClip[] footstepSounds;
		public AudioClip jumpSound;
		public float timeToStep = 1.0f;

		private Movement _movementComponent;
		private PersonFootstepsStateMachine _stateMachine;
		private AudioSource _audioSource;

		private Hashtable _hashtable = new Hashtable();
		

		private void initCommandsInHashtable()
		{
			_hashtable.Add(MovementState.Idle,   new StandCommand());
			_hashtable.Add(MovementState.Walk,   new WalkCommand());
			_hashtable.Add(MovementState.Run,    new RunCommand());
			_hashtable.Add(MovementState.Fly,    new FlyCommand());
		}

		private void Start()
		{
			initCommandsInHashtable();
			_movementComponent = player.GetComponent<Movement>();
			_audioSource = GetComponent<AudioSource>();
			_stateMachine = new PersonFootstepsStateMachine(PersonFootstepsStateMachine.Walking, timeToStep, footstepSounds, jumpSound, _audioSource);
		}

		private void Update()
		{
			ApplyCharacterFootstepsStrategy();
		}

		private void ApplyCharacterFootstepsStrategy()
		{
			ISteppingCommand command = (ISteppingCommand) _hashtable[_movementComponent.MovementState];
			_stateMachine.Execute(command);
			
			// switch (_movementComponent.MovementState)
			// {
			// 	case MovementState.Idle: 
			// 		_commandListener.SetCommand(new Standing());
			// 		break;
			// 	
			// 	case MovementState.Walk:
			// 		_commandListener.SetCommand(new Walking());
			// 		break;
			// 	
			// 	case MovementState.Run:
			// 		_commandListener.SetCommand(new Running());
			// 		break;
			// 	
			// 	case MovementState.Jump:
			// 		_commandListener.SetCommand(new Jumping());
			// 		break;
			// 	
			// 	case MovementState.Fly:
			// 		_commandListener.SetCommand(new Flying());
			// 		break;
			// 	
			// 	default:
			// 		throw new ArgumentOutOfRangeException();
			// }
			//
			// _commandListener.ExecuteCommand();
		}
	}
}