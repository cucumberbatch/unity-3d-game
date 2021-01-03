using System;
using UnityEditor;
using UnityEngine;

public class HidingSpot : MonoBehaviour
{
    public Vector2 coverageDirection;

    private Transform _hidingVictimTransform;
    private float _coverageAmount;
    private bool _isCoverTaken;


    private void OnDrawGizmos()
    {
        Vector3 position = transform.position;
        
        // Draw hiding spot coverage amount and direction what it covers from
        Gizmos.color = Color.black;
        Gizmos.DrawRay(position, NormalizedCoverageDirection());
        Handles.Label(position, "hiding spot\n" + _coverageAmount);
        Gizmos.color = Color.white;
    }

    public void CalculateCoverageAmount(Transform predator)
    {
        Vector3 viewOnPredatorDirection = (transform.position - predator.position).normalized;
        _coverageAmount = Mathf.Pow(Vector3.Dot(NormalizedCoverageDirection(), viewOnPredatorDirection), 3);
    }

    private Vector3 NormalizedCoverageDirection()
    {
        Vector2 normalizedDirection = coverageDirection.normalized;
        return new Vector3(normalizedDirection.x, 0, normalizedDirection.y);
    }

    public Transform IsTaken()
    {
        return _hidingVictimTransform;
    }

    /* Returns fact the concrete victim is hiding there */
    public bool IsTakenBy(Transform victim)
    {
        return _hidingVictimTransform == victim;
    }
    
    // TODO: need more complicated logic of this mechanic for different cases
    public void TakeCover(Transform victim)
    {
        _hidingVictimTransform = victim;
    }
    
    public float GetCoverageAmount()
    {
        return _coverageAmount;
    }

    public void GetOut()
    {
        _hidingVictimTransform = null;
    }
}

namespace AI
{
    public static class Layers
    {
        public static readonly int CoverLayer = LayerMask.GetMask("Cover");
    }
    
    public class UnavailableCoverException : Exception
    {
        public override Exception GetBaseException()
        {
            return base.GetBaseException();
        }
        
    }
    
}
