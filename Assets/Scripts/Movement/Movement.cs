using System;
using UnityEngine;

namespace Movement
{
	public class Movement : MonoBehaviour
	{
		public Transform playerTransform;
		public Transform playerRespawnTransform;
		public CharacterController characterController;
		public float walkSpeed = 1;
		public float sprintSpeed = 2;
		public float increaseVelocitySmoothFactor = 0.4f;

		public float jumpHeight = 1;

		public float innerGravity = -9.81f;

		public Transform groundCheck;
		public LayerMask groundMask;
		public float groundDistance;

		public MovementState MovementState { get; private set; }
		public GroundType GroundType { get; private set; }
		public Camera playerCamera;
		public float minFOV;
		public float maxFOV;

		private CharacterCrouch _crouch;
		private Vector3 _velocity;
		private Vector3 _movement;

		private bool _isGrounded;

		private RaycastHit _hit;

		private Vector3 _movementVector;
		private float _sprintAccumulator;
		private MovementState _previousMovementState;

		float t;
		private Vector3 _previousOnGroundMovement;
		private bool isRespawned = false;

		private void Start()
		{
			Application.targetFrameRate = 60;

			_crouch = new CharacterCrouch();
		}

		private void Update()
		{
			playerTransform = GetComponent<Transform>();
			PlayerMovement();
		}


		private void PlayerMovement()
		{
			
			if (isRespawned)
			{
				characterController.enabled = true;
				isRespawned = false;
			}
			
			if (Input.GetKeyDown(KeyCode.R))
			{
				playerTransform.SetPositionAndRotation(playerRespawnTransform.position, playerRespawnTransform.rotation);
				// playerCamera.transform.rotation.Set(playerRespawnTransform.rotation);
				characterController.enabled = false;
				isRespawned = true;
			}
			
			_isGrounded = Physics.CheckSphere(groundCheck.position, groundDistance, groundMask);

			float x = Input.GetAxis("Horizontal");
			float z = Input.GetAxis("Vertical");

			_movement = Vector3.ClampMagnitude(transform.right * x + transform.forward * z, 1.0f);

			if (_isGrounded)
			{
				if (_velocity.y < 0)
				{
					_velocity.y = -2f;
				}

				if (Input.GetKey(KeyCode.LeftShift) && _movement.magnitude > 0.1f)
				{
					t += increaseVelocitySmoothFactor * Time.deltaTime;
					MovementState = MovementState.Run;
				}
				else if (Math.Abs(_movement.magnitude) > 0.25)
				{
					MovementState = MovementState.Walk;
					t -= increaseVelocitySmoothFactor * Time.deltaTime;
				}
				else
				{
					MovementState = MovementState.Idle;
					t -= increaseVelocitySmoothFactor * Time.deltaTime;
				}
				
				t = Mathf.Clamp(t, 0.0f, 1.0f);
				
				// Apply player velocity
				_movement *= Mathf.Lerp(Mathf.Lerp(walkSpeed, sprintSpeed, t), sprintSpeed, t) / walkSpeed;

				_previousOnGroundMovement = new Vector3(_movement.x, _movement.y, _movement.z);

				if (Physics.Raycast(transform.position, Vector3.down, out _hit, 4, groundMask))
				{
					Ground ground = _hit.transform.GetComponent<Ground>();
				
					if (ground)
					{
						GroundType = ground.groundType;
					}
				}

				if (Input.GetButtonDown("Jump"))
				{
					_velocity.x *= 1.1f;
					_velocity.y  = (float) Math.Sqrt(jumpHeight * -2.5f * innerGravity);
					_velocity.z *= 1.1f;
					MovementState = MovementState.Fly;
				}

				characterController.Move(_movement * (walkSpeed * Time.deltaTime));
			}
			else
			{
				// TODO: need to fix movement control on the fly, fast speed decreasing problem
				MovementState = MovementState.Fly;
				t -= 0.25f * increaseVelocitySmoothFactor * Time.deltaTime;
				characterController.Move(Vector3.Lerp(_movement, _previousOnGroundMovement, t) * (walkSpeed * Time.deltaTime));
			}
			
			// Change FOV while sprinting
			playerCamera.fieldOfView = Mathf.Lerp(Mathf.Lerp(minFOV, maxFOV, t), maxFOV, t);

			// Crouching section
			// TODO: it must feels more fluent when you release a crouching button
			characterController.height = _crouch.CharacterGettingUp(Input.GetKey(KeyCode.LeftControl));
			
			_velocity.y += innerGravity * Time.deltaTime;

			characterController.Move(_velocity * Time.deltaTime);

			_previousMovementState = MovementState;
		}

	}
}