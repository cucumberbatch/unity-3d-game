using System;
using UnityEngine;

namespace Movement
{
	public class Movement : MonoBehaviour
	{
//		public AudioClip jumpSound;
//		public AudioClip footstepSound;
//	
//		public int jumpSpeed = 10;
//		public float movementSpeed = 5;
//		public float timeToStep = 1.0f;
//
//		private PersonFootstepsStateMachine _machine;
//		private CommandListener _listener;
//		private AudioSource _audioSource;
//		private Rigidbody _rigidBody;
//		private Vector3 _walkDirection;
//		
//		private bool _isOnGround;
//		
//
//		private void Start()
//		{
//			_machine = new PersonFootstepsStateMachine(new Idle(), timeToStep);
//			_listener = new CommandListener(_machine);
//
//			_rigidBody = GetComponent<Rigidbody>();
//			_audioSource = GetComponent<AudioSource>();
//		}
//
//		private void OnCollisionStay(Collision other)
//		{
//			if (other.gameObject.CompareTag("ground"))
//			{
//				_isOnGround = true;
//			}
//		}
//
//		private void OnCollisionExit(Collision other)
//		{
//			if (other.gameObject.CompareTag("ground"))
//			{
//				_isOnGround = false;
//			}
//		}
//
//		void Update() {
//
//			if (!_isOnGround)
//			{
//				return;
//			}
//		
//			_walkDirection = Vector3.zero;
//			
//			_listener.SetCommand(new Standing());
//			
//
//			if (Input.GetKey(KeyCode.W) || Input.GetKey(KeyCode.UpArrow))
//			{
//				_walkDirection += MakeMovement(transform.forward);
//				_listener.SetCommand(new Walking());
//			} 
//			else if (Input.GetKey(KeyCode.S) || Input.GetKey(KeyCode.DownArrow))
//			{
//				_walkDirection -= MakeMovement(transform.forward);
//				_listener.SetCommand(new Walking());
//			}
//
//			if (Input.GetKey(KeyCode.A) || Input.GetKey(KeyCode.LeftArrow))
//			{
//				_walkDirection -= MakeMovement(transform.right);
//				_listener.SetCommand(new Walking());
//			}
//			else if (Input.GetKey(KeyCode.D) || Input.GetKey(KeyCode.RightArrow))
//			{
//				_walkDirection += MakeMovement(transform.right);
//				_listener.SetCommand(new Walking());
//			}
//
//			if (Input.GetKey(KeyCode.LeftShift))
//			{
//				movementSpeed = 9.0f;
//				_listener.SetCommand(new Running());
//			}
//			else
//			{
//				movementSpeed = 5.0f;
//			}
//			
//			if (Input.GetKeyDown(KeyCode.Space))
//			{
//				_rigidBody.AddForce(MakeJump(),ForceMode.Impulse);
//				_audioSource.PlayOneShot(jumpSound);
//				_isOnGround = false;
//			}
//
//
//			// Applying the result of all impulses
////			transform.position += (_walkDirection / _walkDirection.magnitude) * movementSpeed / 60f;
//			_rigidBody.velocity += _walkDirection / _walkDirection.magnitude;// * movementSpeed;
//			
//			// Applying the moving command to state machine
//			_listener.ExecuteCommand();
//
//			// Checking for step sound needs
//			if (_machine.IsPlayingStepSound())
//			{
//				_audioSource.PlayOneShot(footstepSound);
//			}
//		}
//
//
//		private Vector3 MakeMovement(Vector3 direction)
//		{
//			return Time.deltaTime * movementSpeed * direction;
//		}
//
//		private Vector3 MakeJump()
//		{
//			return jumpSpeed * transform.up;
//		}

		public CharacterController characterController;
		public float movementSpeed = 1;
		public float sprintingSpeed = 2;
		public float jumpSpeed = 1;
		public float innerGravity = -9.81f;

		public Transform groundCheck;
		public float groundDistance;
		public LayerMask groundMask;

		private Vector3 _movementDirection;
		private Vector3 _lookOrientation;
		public MovementState MovementState { get; private set; }

		private Vector3 move;
		private Vector3 velocity;

		private bool isGrounded;


		private void Update()
		{
			PlayerMovement();
		}


		private void PlayerMovement()
		{
			isGrounded = Physics.CheckSphere(groundCheck.position, groundDistance, groundMask);

			

			float x = Input.GetAxis("Horizontal");
			float z = Input.GetAxis("Vertical");

			move = transform.right * x + transform.forward * z;
			
			if (isGrounded)
			{
				if (velocity.y < 0)
				{
					velocity.y = -2f;
				}
				
				if (Input.GetKey(KeyCode.LeftShift))
				{
					move *= sprintingSpeed;
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
			
			characterController.Move(move * (movementSpeed * Time.deltaTime));

			if (Input.GetButtonDown("Jump") && isGrounded)
			{
				velocity.y = (float) Math.Sqrt(jumpSpeed * -2f * innerGravity);
				MovementState = MovementState.Jumping;
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



