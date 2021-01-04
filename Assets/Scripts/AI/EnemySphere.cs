using System;
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


    //=========    ENGINE METHODS    ===========
    
    private void Start()
    {
        _rigidBody     = GetComponent<Rigidbody>();
        _meshRenderer  = GetComponent<MeshRenderer>();
        _agent         = GetComponent<NavMeshAgent>();
        UpdatePreviousPredatorPosition();
    }


    private void Update()
    {
        if (IsPredatorMoved())
        {
            UpdatePreviousPredatorPosition();
            GeneratePredatorPositionUpdateEvent();
            ReserveVictimHidingSpot();
        }
        SetAgentDestination();
    }
    

    private void OnTriggerEnter(Collider other)
    {
        if (IsTagsEquals(other, "LocationBox"))
        {
            SetReferenceToEnteredLocationHelper(other);
        }
    }

    
    
    //=========    OTHER METHODS    ===========
    
    private Vector3 UpdatePreviousPredatorPosition()
    {
        return _previousPredatorPosition = predator.position;
    }

    private void SetAgentDestination()
    {
        _agent.SetDestination(SelectDestinationPoint());
    }

    private Vector3 SelectDestinationPoint()
    {
        return _reservedSpot.transform.position;
    }

    private bool IsPredatorMoved()
    {
        return predator.position != _previousPredatorPosition;
    }

    private void GeneratePredatorPositionUpdateEvent()
    {
        helper.GeneratePredatorPositionUpdateEvent();
    }

    private void ReserveVictimHidingSpot()
    {
        _reservedSpot = helper.GetVictimHidingSpotTransform(transform, predator).GetComponent<HidingSpot>();
    }

    private void SetReferenceToEnteredLocationHelper(Collider other)
    {
        helper = other.GetComponent<HidingSpotLocationHelper>();
    }

    public static bool IsTagsEquals(Collider other, String tag)
    {
        return other.tag.Equals(tag);
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

    public HidingSpot GetHidingSpot()
    {
        return _reservedSpot;
    }
}
