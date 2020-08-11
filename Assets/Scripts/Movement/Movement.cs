using System;
using UnityEngine;

namespace Movement
{
	public class Movement : MonoBehaviour
	{
		public CharacterController 	characterController;
		public float 				walkSpeed = 1;
		public float 				sprintSpeed = 2;
		
		public float 				jumpHeight = 1;
		
		public float 				innerGravity = -9.81f;

		public Transform 			groundCheck;
		public LayerMask 			groundMask;
		public float 				groundDistance;

		public 	MovementState 		MovementState { get; private set; }
		
		private CharacterCrouch 	_crouch;
		private Vector3 			_velocity;
		private Vector3 			_movement;
		private Transform 			_transform;

		private bool 				_isGrounded;


		private void Start()
		{
			_crouch 	= new CharacterCrouch();
			_transform 	= transform;
		}

		private void Update()
		{
			PlayerMovement();
		}


		private void PlayerMovement()
		{
			_isGrounded = Physics.CheckSphere(groundCheck.position, groundDistance, groundMask);
			
			float x = Input.GetAxis("Horizontal");
			float z = Input.GetAxis("Vertical");

			_movement = _transform.right * x + _transform.forward * z;
			
			if (_isGrounded)
			{
				if (_velocity.y < 0)
				{
					_velocity.y = -2f;
				}
				
				if (Input.GetKey(KeyCode.LeftShift) && _movement.magnitude > 0.1f)
				{
					_movement 		*= sprintSpeed;
					MovementState 	= MovementState.Run;
				}
				else if (Math.Abs(_movement.magnitude) > 0.25)
				{
					MovementState 	= MovementState.Walk;
				}
				else
				{
					MovementState 	= MovementState.Idle;
				}
			}
			else
			{
				MovementState 		= MovementState.Fly;
			}
			
			characterController.Move(_movement * (walkSpeed * Time.deltaTime));

			if (Input.GetButtonDown("Jump") && _isGrounded)
			{
				_velocity.y 		= (float) Math.Sqrt(jumpHeight * -2f * innerGravity);
				MovementState 		= MovementState.Fly;
			}

			
			// Crouching section
			// TODO: it must feels more fluent when you release a crouching button
			characterController.height = _crouch.CharacterGettingUp(Input.GetKey(KeyCode.LeftControl));
			
			_velocity.y += innerGravity * Time.deltaTime;

			characterController.Move(_velocity * Time.deltaTime);

		}

	}
}



