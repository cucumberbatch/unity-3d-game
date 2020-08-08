using System.Collections.Generic;
using UnityEngine;

public class HidingSpotLocationHelper : MonoBehaviour
{
    private HashSet<ILocationEvent> _events;
    private CoverageProcessor _processor;
    private bool _isCoverageCalculated;
    
    
    private void Start()
    {
        _events = new HashSet<ILocationEvent>();
        _processor = new CoverageProcessor(this);
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
            _processor.CalculateCoverage(victim, predator);
            _isCoverageCalculated = true;
        }
        
        return _processor.GetHidingSpotTransform(victim, predator);
    }

    //---------- ??? -----------------
    public void TakeCover(HidingSpot preferableSpot, Transform victim)
    {
        victim.GetComponent<EnemySphere>().GetOutOfHidingSpot();
        preferableSpot.TakeCover(victim);
    }

    public void GeneratePredatorPositionUpdateEvent()
    {
        _events.Add(LocationEvent.PredatorPositionUpdateEvent);
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

public static class LocationEvent
{
    /* A bunch of initialised events */
    public static readonly ILocationEvent PredatorPositionUpdateEvent = new PredatorPositionUpdateEvent();

}
