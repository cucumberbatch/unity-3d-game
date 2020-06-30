using UnityEngine;
using UnityEngine.AI;

public class EnemySphere : MonoBehaviour
{
    public float speed = 1;
    public Transform predator;
    public HidingSpotLocationHelper helper;

    [Header("State Materials")] 
    public Material onSteadyMaterial;
    public Material onAttackMaterial;

    private bool _isAttack;
    private Rigidbody _rigidBody;
    private MeshRenderer _meshRenderer;
    private NavMeshAgent _agent;

    private Vector3 _previousPredatorPosition;


    private void Start()
    {
        _rigidBody = GetComponent<Rigidbody>();
        _meshRenderer = GetComponent<MeshRenderer>();
        _agent = GetComponent<NavMeshAgent>();
        _previousPredatorPosition = predator.position;
    }

    private void Update()
    {
        if (predator.position != _previousPredatorPosition)
        {
            helper.GeneratePredatorPositionUpdateEvent(); 
        }
        
        Vector3 selectedDestinationPoint = helper.GetVictimHidingSpotTransform(transform, predator).position;
        
        _agent.SetDestination(selectedDestinationPoint);
    }

    private void OnTriggerEnter(Collider other)
    {
        /* Set reference to an entered location object */
        if (other.gameObject.layer == AI.Layers.CoverLayer)
        {
            helper = other.GetComponent<HidingSpotLocationHelper>();
        }
    }
}
