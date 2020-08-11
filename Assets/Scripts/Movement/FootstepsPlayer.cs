using System;
using UnityEngine;
using Random = UnityEngine.Random;

namespace Movement
{
	[Serializable]
	public class FootstepsPlayer
	{
		[SerializeField]
		private FootstepsSoundboard _soundboard;
		private AudioSource 		_source;

		
		public FootstepsPlayer(FootstepsSoundboard soundboard, AudioSource source)
		{
			_soundboard = soundboard;
			_source 	= source;
		}

		public void SetSoundboard(FootstepsSoundboard soundboard)
		{
			_soundboard = soundboard;
		}
		
		public void PlayFootstepsSound()
		{
			AudioClip[] footstepSounds 	= _soundboard.footstepSounds;
			
			int soundIndex = Random.Range(1, footstepSounds.Length); 
			_source.PlayOneShot(footstepSounds[soundIndex]);

			Swap(ref footstepSounds[0], ref footstepSounds[soundIndex]);
		}

		public void PlayJumpSound()
		{
			_source.PlayOneShot(_soundboard.jumpSound);
		}

		public void Swap<T>(ref T that, ref T other)
		{
			T temp 	= that; 
			that 	= other; 
			other 	= temp;
		}
		
	}

	[Serializable]
	public class FootstepsSoundboard
	{
		public AudioClip[] 	footstepSounds;
		public AudioClip 	jumpSound;

		public FootstepsSoundboard(AudioClip jumpSound, AudioClip[] footstepSounds)
		{
			this.footstepSounds = footstepSounds;
			this.jumpSound 		= jumpSound;
		}
	}
}
