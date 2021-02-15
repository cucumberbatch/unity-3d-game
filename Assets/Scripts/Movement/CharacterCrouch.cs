using UnityEngine;

namespace Movement
{
	public class CharacterCrouch
	{
		public float crouchSpeed = 0.7f;
		public float gettingUpSpeed = 0.085f;

		public float standHeight = 2;
		public float crouchHeight = 1;

		private float _gettingState;
		private bool _isGettingUp;
		
		public float CharacterGettingUp(bool isCrouch)
		{
			if (isCrouch)
			{
				_gettingState = crouchHeight;
				_isGettingUp = false;
				return crouchHeight;
			}
			
			_isGettingUp = true;

			if (!_isGettingUp) return _gettingState;
			
			_gettingState += gettingUpSpeed;
			
			if (_gettingState >= standHeight)
			{
				_gettingState = standHeight;
				return standHeight;
			}

			_isGettingUp = true;

			return _gettingState;
		}
	}
}