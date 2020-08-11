using System.Collections;
using Constants.Sounds;
using UnityEngine;

namespace Movement
{

	public class CharacterFootsteps : MonoBehaviour
	{

		public GameObject 	player;
		public Soundboards 	soundboards;
		public float 		timeToStep;

		private Movement 					_movementComponent;
		private PersonFootstepsStateMachine _stateMachine;
		private AudioSource 				_audioSource;

		private Hashtable 					_hashtable;

		
		private void Start()
		{
			InitCommandsInHashtable();
			_movementComponent 	= player.GetComponent<Movement>();
			_audioSource 		= GetComponent<AudioSource>();
			_stateMachine 		= new PersonFootstepsStateMachine(new FootstepsPlayer(soundboards.Asphalt, _audioSource), timeToStep);
		}	

		private void Update()
		{
			UpdateFootstepsSoundboard();
			ApplyCharacterFootstepsStrategy();
		}

		private void UpdateFootstepsSoundboard()
		{
			_stateMachine.player.SetSoundboard(soundboards.GetSoundboard(_movementComponent.GroundType));
		}

		private void ApplyCharacterFootstepsStrategy()
		{
			
			ISteppingCommand command = (ISteppingCommand) _hashtable[_movementComponent.MovementState];
			_stateMachine.Execute(command);
		}

		private void InitCommandsInHashtable()
		{
			_hashtable = new Hashtable
			{
				{MovementState.Idle, 	new StandCommand()},
				{MovementState.Walk, 	new WalkCommand()},
				{MovementState.Run, 	new RunCommand()},
				{MovementState.Fly, 	new FlyCommand()}
			};
		}

		public PersonFootstepsStateMachine StateMachine()
		{
			return _stateMachine;
		}
	}
}