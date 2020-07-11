using System.Collections.Generic;
using UnityEngine;

public class HidingSpotLocationHelper : MonoBehaviour
{
    private HashSet<ILocationEvent> _events;
    private HidingSpot[] _hidingSpots;
    private bool _isCoverageCalculated;
    
    /* A bunch of initialised events */
    private readonly ILocationEvent _predatorPositionUpdateEvent = new PredatorPositionUpdateEvent();

    private void Start()
    {
        _events = new HashSet<ILocationEvent>();
        
        Vector3 halfExtents = GetComponent<BoxCollider>().size / 2;
        
        /* Get all colliders at position and in a range of trigger zone of this object */
        Collider[] colliders = Physics.OverlapBox(transform.position, halfExtents, transform.rotation, AI.Layers.CoverLayer);
        
        _hidingSpots = new HidingSpot[colliders.Length];
        
        for (int i = 0; i < colliders.Length; i++)
        {
            _hidingSpots[i] = colliders[i].GetComponent<HidingSpot>();
        }
    }

    private void LateUpdate()
    {
        /* Apply all events that was added */
        foreach (var locationEvent in _events)
        {
            locationEvent.Apply(this);
        }

        _events.Clear();
    }

    /* Check all near hiding spots for coverage and select the most useful */
    public Transform GetVictimHidingSpotTransform(Transform victim, Transform predator)
    {
        if (!_isCoverageCalculated)
        {
            CalculateCoverage(predator);
            _isCoverageCalculated = true;
        }
        
        return GetHidingSpotTransform(victim);
    }

    /* Select the most useful hiding spot */
    private Transform GetHidingSpotTransform(Transform victim)
    {
        if (_hidingSpots.Length == 0) return null;
        
        HidingSpot preferableSpot = _hidingSpots[0];
        float preferableSpotCoverageAmount = preferableSpot.GetCoverageAmount();

        foreach (var spot in _hidingSpots)
        {
            if (spot.IsTaken()) continue;
            
            float thatCoverageAmount = spot.GetCoverageAmount();
            
            if (!spot.IsTakenBy(victim) && thatCoverageAmount > preferableSpotCoverageAmount)
            {
                preferableSpot = spot;
                preferableSpotCoverageAmount = thatCoverageAmount;
            }
        }

        preferableSpot.TakeCover(victim);
        
        return preferableSpot.transform;
    }

    private void CalculateCoverage(Transform predator)
    {
        foreach (var spot in _hidingSpots)
        {
            spot.CalculateCoverageAmount(predator);
        }
    }

    public void GeneratePredatorPositionUpdateEvent()
    {
        _events.Add(_predatorPositionUpdateEvent);
    }

    public void RegisterPredatorPositionUpdate()
    {
        _isCoverageCalculated = false;
    }
}

public interface ILocationEvent
{
    void Apply(HidingSpotLocationHelper helper);
}

public class PredatorPositionUpdateEvent : ILocationEvent
{
    public void Apply(HidingSpotLocationHelper helper)
    {
        helper.RegisterPredatorPositionUpdate();
    }
}
