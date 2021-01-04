using System;
using UnityEngine;

public class CoverageProcessor
{
	private HidingSpot[] _hidingSpots;
	private HidingSpotLocationHelper _helper;

	private static readonly float _minimumAcceptableCoverageAmount = 0.65f;
	private static readonly float _safeRelocationValue = 0.0f;
	private static readonly float _relocationApprovementValueDifference = 0.3f;
	
	public CoverageProcessor(HidingSpotLocationHelper hidingSpotLocationHelper)
	{
		Vector3 halfExtents = hidingSpotLocationHelper.GetComponent<BoxCollider>().size / 2;
		
		_helper = hidingSpotLocationHelper;
		var transform = _helper.transform;

		/* Get all colliders at position and in a range of trigger zone of this object */
		Collider[] colliders = Physics.OverlapBox(transform.position, halfExtents, transform.rotation, AI.Layers.CoverLayer);
        
		_hidingSpots = new HidingSpot[colliders.Length];
        
		for (int i = 0; i < colliders.Length; i++)
		{
			_hidingSpots[i] = colliders[i].GetComponent<HidingSpot>();
		}
	}

	
	public void CalculateCoverage(Transform victim, Transform predator)
	{
		foreach (var spot in _hidingSpots)
        {
            spot.CalculateCoverageAmount(predator);
        }
	}

	
	public Transform GetHidingSpotTransform(Transform victim, Transform predator)
	{
		if (_hidingSpots.Length == 0)
		{
			throw new NullReferenceException("There is no one hiding spot in this location!");
		}
		var selectedSpot = SelectHidingSpot(victim, predator);
		_helper.TakeCover(selectedSpot, victim);
		return selectedSpot.transform;
	}

	
	private HidingSpot SelectHidingSpot(Transform victim, Transform predator)
	{
		HidingSpot selectedSpot = _hidingSpots[0];

		foreach (var spot in _hidingSpots)
		{
			selectedSpot = spot;

			if (selectedSpot.IsTaken() && !selectedSpot.IsTakenBy(victim)) continue;
			if (!IsCoverageAcceptable(selectedSpot)) continue;
			if (IsPositionChangingSafe(selectedSpot, victim, predator)) break;
		}

		HidingSpot victimSpot = victim.GetComponent<EnemySphere>().GetHidingSpot();

		if (victimSpot == null)
		{
			return selectedSpot;
		}

		return IsRelocationApproved(selectedSpot, victimSpot) ? selectedSpot : victimSpot;
	}

	private bool IsRelocationApproved(HidingSpot selectedSpot, HidingSpot victimSpot)
	{
		return selectedSpot.GetCoverageAmount() - victimSpot.GetCoverageAmount() >
		       _relocationApprovementValueDifference;
	}
	
	
	private bool IsCoverageAcceptable(HidingSpot hidingSpot)
	{
		return hidingSpot.GetCoverageAmount() >= _minimumAcceptableCoverageAmount;
	}

	
	private bool IsPositionChangingSafe(HidingSpot hidingSpot, Transform victim, Transform predator)
	{
		// there is gonna be math
		Vector3 victimPredatorVector = victim.position - predator.position;
		Vector3 victimHidingSpotVector = victim.position - hidingSpot.transform.position;
		float dotProduct = Vector3.Dot(victimPredatorVector, victimHidingSpotVector);
		
		return dotProduct < _safeRelocationValue;
	}
}