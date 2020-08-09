using System;
using System.Collections;
using UnityEngine;

namespace Movement
{

	public class CharacterFootsteps : MonoBehaviour
	{

		public GameObject 	player;
		public AudioClip[] 	footstepSounds;
		public AudioClip 	jumpSound;
		public float 		timeToStep;

		private Movement 					_movementComponent;
		private PersonFootstepsStateMachine _stateMachine;
		private AudioSource 				_audioSource;

		private readonly Hashtable _hashtable = new Hashtable();
		

		private void Start()
		{
			InitCommandsInHashtable();
			_movementComponent = player.GetComponent<Movement>();
			_audioSource = GetComponent<AudioSource>();
			_stateMachine = new PersonFootstepsStateMachine(timeToStep, footstepSounds, jumpSound, _audioSource);
		}

		private void Update()
		{
			ApplyCharacterFootstepsStrategy();
		}

		private void ApplyCharacterFootstepsStrategy()
		{
			ISteppingCommand command = (ISteppingCommand) _hashtable[_movementComponent.MovementState];
			_stateMachine.Execute(command);
		}

		private void InitCommandsInHashtable()
		{
			_hashtable.Add(MovementState.Idle,	new StandCommand());
			_hashtable.Add(MovementState.Walk,	new WalkCommand());
			_hashtable.Add(MovementState.Run,	new RunCommand());
			_hashtable.Add(MovementState.Fly,	new FlyCommand());
		}
	}
}