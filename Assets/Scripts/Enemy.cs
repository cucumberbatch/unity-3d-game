using UnityEngine;
using UnityEngine.AI;

public class Enemy : MonoBehaviour
{
    public GameObject ragdoll;
    public GameObject predator;
    public GameObject coverSpot;
    
    private NavMeshAgent _agent;
    private GameObject _player;
    private Transform _agentTransform;
    private Transform _target;
    private Animator _animator;


    private void Start()
    {
        _agent = GetComponent<NavMeshAgent>();
        _player = GameObject.FindWithTag("Player");
        _agentTransform = GetComponent<Transform>();
        _target = _player.GetComponent<Transform>();  //преследуем игрока
        _animator = gameObject.GetComponent<Animator>();
    }

    public void Update()
    {    
        if (_agent.velocity == Vector3.zero)
        {
            _animator.SetTrigger("Idle");
        }
        
        if (_agent.velocity != Vector3.zero)
        {
            if (Mathf.Abs((_agentTransform.position - _target.position).magnitude) > _agent.stoppingDistance * 2.0f)
            {
                _agent.speed = 6;
                _animator.SetTrigger("Run");
            }
            else
            {
                _agent.speed = 3;
                _animator.SetTrigger("Walk");
            }
        }
        
        _agent.SetDestination(coverSpot.GetComponent<HidingSpot>().ChooseCover(transform.position, predator.transform.position).position);
    }

    private void OnTriggerStay(Collider other)
    { 
        if (other.CompareTag("XR"))
        {
            gameObject.SetActive(false);      
            ragdoll.SetActive(true);     //спауним труп
            Instantiate(ragdoll, transform.position, transform.rotation);
        }
    }
}
