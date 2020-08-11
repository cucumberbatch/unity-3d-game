using System.Collections;
using Movement;
using UnityEngine;

namespace Constants.Sounds
{
	public class Soundboards : MonoBehaviour
	{
		[SerializeField] public FootstepsSoundboard Asphalt;
		[SerializeField] public FootstepsSoundboard Grass;

		private Hashtable footstepsHashtable;

		public FootstepsSoundboard GetSoundboard(GroundType type)
		{
			return (FootstepsSoundboard) footstepsHashtable[type];
		}
		
		private void InitHashtables()
		{
			footstepsHashtable = new Hashtable
			{
				{GroundType.Asphalt, 	Asphalt}, 
				{GroundType.Grass, 		Grass}
			};
		}

		private void Start()
		{
			InitHashtables();
		}
	}
	
}

