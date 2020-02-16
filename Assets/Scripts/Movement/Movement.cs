using UnityEngine;

namespace Movement
{
	public class Movement : MonoBehaviour {

		public AudioClip jumpSound;
		public AudioClip footstepSound;
	
		public int movementSpeed = 5;
		public int jumpSpeed = 10;
		public float timeToStep = 1.0f;

		private PersonFootstepsStateMachine _machine;
		private CommandListener _listener;
		private Vector3 _walkDirection;
		private bool _isOnGround;

		private void Start()
		{
			_machine = new PersonFootstepsStateMachine(new Idle(), timeToStep);
			_listener = new CommandListener(_machine);
		}

		private void OnCollisionStay(Collision other)
		{
			if (other.gameObject.CompareTag("ground"))
			{
				_isOnGround = true;
			}
		}

		void Update() {

			if (!_isOnGround)
			{
				return;
			}
		
			_walkDirection = Vector3.zero;
			
			_listener.SetCommand(new Standing());
			

			if (Input.GetKey(KeyCode.W) || Input.GetKey(KeyCode.UpArrow))
			{
				_walkDirection += MakeMovement(transform.forward);
				_listener.SetCommand(new Walking());
			}

			if (Input.GetKey(KeyCode.S) || Input.GetKey(KeyCode.DownArrow))
			{
				_walkDirection -= MakeMovement(transform.forward);
				_listener.SetCommand(new Walking());
			}

			if (Input.GetKey(KeyCode.A) || Input.GetKey(KeyCode.LeftArrow))
			{
				_walkDirection -= MakeMovement(transform.right);
				_listener.SetCommand(new Walking());
			}

			if (Input.GetKey(KeyCode.D) || Input.GetKey(KeyCode.RightArrow))
			{
				_walkDirection += MakeMovement(transform.right);
				_listener.SetCommand(new Walking());
			}

			if (Input.GetKey(KeyCode.LeftShift))
			{
				movementSpeed = 10;
				_listener.SetCommand(new Running());
			}
			else
			{
				movementSpeed = 5;
			}
			
			if (Input.GetKeyDown(KeyCode.Space))
			{
				GetComponent<Rigidbody>().AddForce(MakeJump(),ForceMode.Impulse);
				GetComponent<AudioSource>().PlayOneShot(jumpSound);
			}
			
			_isOnGround = false;
	

			// Applying the result of all impulses
			transform.position += _walkDirection;
		
			// Applying the moving command to state machine
			_listener.ExecuteCommand();

			// Checking for step sound needs
			if (_machine.IsPlayingStepSound())
			{
				GetComponent<AudioSource>().PlayOneShot(footstepSound);
			}
		}


		private Vector3 MakeMovement(Vector3 direction)
		{
			return Time.deltaTime * movementSpeed * direction;
		}

		private Vector3 MakeJump()
		{
			return Time.deltaTime * jumpSpeed * 50 * transform.up;
		}

	}
}



