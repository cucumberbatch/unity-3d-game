using UnityEngine;
using UnityEngine.AI;

public class EnemySphere : MonoBehaviour
{
    public Transform predator;
    public HidingSpotLocationHelper helper;

    [Header("State Materials")] 
    public Material onSteadyMaterial;
    public Material onAttackMaterial;

    private bool _isAttack;
    private Rigidbody _rigidBody;
    private MeshRenderer _meshRenderer;
    private NavMeshAgent _agent;
    private HidingSpot _reservedSpot;
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
            _previousPredatorPosition = predator.position;
            helper.GeneratePredatorPositionUpdateEvent();
        }

        _reservedSpot = helper.GetVictimHidingSpotTransform(transform, predator).GetComponent<HidingSpot>();
        
        Vector3 selectedDestinationPoint = _reservedSpot.transform.position;
        
        _agent.SetDestination(selectedDestinationPoint);
    }

    private void OnTriggerEnter(Collider other)
    {
        /* Set reference to an entered location object */
        if (other.tag.Equals("LocationBox"))
        {
            helper = other.GetComponent<HidingSpotLocationHelper>();
        }
    }

    private void SwitchAttackStatement()
    {
        _isAttack = !_isAttack;
        
        _meshRenderer.material = _isAttack ? onAttackMaterial : onSteadyMaterial;
    }

    public void GetOutOfHidingSpot()
    {
        if (!_reservedSpot) return;
        
        _reservedSpot.GetOut();
        _reservedSpot = null;
    }
}
