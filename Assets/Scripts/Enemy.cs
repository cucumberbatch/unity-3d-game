using UnityEngine;
using UnityEngine.AI;

public class Enemy : MonoBehaviour
{
    public GameObject ragdoll;
    public GameObject coverSpot;
    public Transform target;
    public float runSpeed = 6.0f;
    public float walkSpeed = 3.0f;
    
    private NavMeshAgent _agent;
    private Animator _animator;
    
    /* Enemy animator states */
    private static readonly int Idle = Animator.StringToHash("Idle");
    private static readonly int Walk = Animator.StringToHash("Walk");
    private static readonly int Run = Animator.StringToHash("Run");


    private void Start()
    {
        /* Searching for player transform in the world */
        target = GameObject.FindWithTag("Player").transform;
        
        _agent = GetComponent<NavMeshAgent>();
        _animator = gameObject.GetComponent<Animator>();
    }

    public void Update()
    {
        
        _agent.SetDestination(coverSpot.GetComponent<HidingSpot>().ChooseCover(transform.position, target.transform.position).position);
        ChooseAnimatorStrategy();
    }

    
    
    /**
     * 
     */
    private void ChooseAnimatorStrategy()
    {
        if (_agent.velocity == Vector3.zero)
        {
            _animator.SetTrigger(Idle);
        } 
        else 
        {
            if (Mathf.Abs((transform.position - target.position).magnitude) > _agent.stoppingDistance)
            {
                _agent.speed = runSpeed;
                _animator.SetTrigger(Run);
            }
            else
            {
                _agent.speed = walkSpeed;
                _animator.SetTrigger(Walk);
            }
        }
    }
    
    private void OnTriggerStay(Collider other)
    {
        /* Check for bullet tag of collider */
        if (!other.CompareTag("XR")) return;
        
        /*  */
        gameObject.SetActive(false);
        ragdoll.SetActive(true);
        Instantiate(ragdoll, transform);
    }
}
