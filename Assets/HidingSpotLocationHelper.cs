using System.Collections.Generic;
using UnityEngine;

public class HidingSpotLocationHelper : MonoBehaviour
{
    public float timer = 1;

    private List<ILocationEvent> _events;
    private CoverPoint[] _coverPoints;
    private bool _isCoverageCalculated;
    
    private float _currentTimeBeforeTick;
    
    /* A bunch of initialised events */
    private readonly ILocationEvent _predatorPositionUpdateEvent = new PredatorPositionUpdateEvent();

    private void Start()
    {
        _events = new List<ILocationEvent>();
        
        Vector3 halfExtents = GetComponent<BoxCollider>().size / 2;
        
        /* Get all colliders at position and in a range of trigger zone of this object */
        Collider[] colliders = Physics.OverlapBox(transform.position, halfExtents, transform.rotation, AI.Layers.CoverLayer);
        
        _coverPoints = new CoverPoint[colliders.Length];
        
        for (int i = 0; i < colliders.Length; i++)
        {
            _coverPoints[i] = colliders[i].GetComponent<CoverPoint>();
        }
    }

    private void LateUpdate()
    {
        foreach (var locationEvent in _events)
        {
            locationEvent.Apply(this);
            _events.Remove(locationEvent);
        }
    }

    public Transform GetVictimHidingSpotTransform(Transform victim, Transform predator)
    {
        if (!_isCoverageCalculated)
        {
            CalculateCoverage(victim, predator);
            _isCoverageCalculated = true;
        }

        return GetHidingSpotTransform();
    }

    private Transform GetHidingSpotTransform()
    {
        if (_coverPoints.Length == 0) return null;
        
        CoverPoint preferableCover = _coverPoints[0];
        float preferableCoverCoverageAmount = preferableCover.GetCoverageAmount();

        foreach (var cover in _coverPoints)
        {
            float thatCoverageAmount = cover.GetCoverageAmount();

            if (!cover.IsCoverTaken() && thatCoverageAmount > preferableCoverCoverageAmount)
            {
                preferableCover = cover;
                preferableCoverCoverageAmount = thatCoverageAmount;
            }
        }

        return preferableCover.transform;
    }

    private void CalculateCoverage(Transform victim, Transform predator)
    {
        foreach (var cover in _coverPoints)
        {
            cover.CalculateCoverageAmount(victim, predator);
        }
    }

    public void GeneratePredatorPositionUpdateEvent()
    {
        if (_events.Contains(_predatorPositionUpdateEvent)) return;
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
