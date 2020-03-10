using System;
using System.Runtime.InteropServices.ComTypes;
using UnityEngine;

namespace Movement
{
	public class Movement : MonoBehaviour
	{
		public CharacterController characterController;
		public float walkSpeed = 1;
		public float sprintSpeed = 2;
		public float crouchSpeed = 0.7f;
		
		public float jumpHeight = 1;
		public float standHeight = 2;
		public float crouchHeight = 1;
		
		public float innerGravity = -9.81f;

		public Transform groundCheck;
		public LayerMask groundMask;
		public float groundDistance;

		private Transform _transform;
		private Vector3 _movementDirection;
		private Vector3 _lookOrientation;
		public MovementState MovementState { get; private set; }

		private Vector3 move;
		private Vector3 velocity;

		private bool isGrounded;


		private void Start()
		{
			_transform = transform;
		}

		private void Update()
		{
			PlayerMovement();
		}


		private void PlayerMovement()
		{
			isGrounded = Physics.CheckSphere(groundCheck.position, groundDistance, groundMask);
			
			float x = Input.GetAxis("Horizontal");
			float z = Input.GetAxis("Vertical");

			move = _transform.right * x + _transform.forward * z;
			
			if (isGrounded)
			{
				if (velocity.y < 0)
				{
					velocity.y = -2f;
				}
				
				if (Input.GetKey(KeyCode.LeftShift))
				{
					move *= sprintSpeed;
					MovementState = MovementState.Running;
				}
				else if (Math.Abs(move.magnitude) > 0.25)
				{
					MovementState = MovementState.Walking;
				}
				else
				{
					MovementState = MovementState.Idle;
				}
			}
			else
			{
				MovementState = MovementState.Flying;
			}
			
			characterController.Move(move * (walkSpeed * Time.deltaTime));

			if (Input.GetButtonDown("Jump") && isGrounded)
			{
				velocity.y = (float) Math.Sqrt(jumpHeight * -2f * innerGravity);
				MovementState = MovementState.Jumping;
			}

			// Crouching section
			// TODO: it must feels more fluent when you release a crouching button
			if (Input.GetKey(KeyCode.LeftControl))
			{
				characterController.height = crouchHeight;
			}
			else
			{
				characterController.height = standHeight;
			}

			if (isGrounded && MovementState == MovementState.Flying)
			{
				MovementState = MovementState.Landing;
			}

			velocity.y += innerGravity * Time.deltaTime;

			characterController.Move(velocity * Time.deltaTime);

		}
	}
}



