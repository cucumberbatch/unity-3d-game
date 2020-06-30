using System;
using UnityEngine;

public class CoverPoint : MonoBehaviour
{
    public Vector3 coverageDirection;

    private float _coverageAmount;
    private bool _isCoverTaken;
    
    
    private void Start()
    {
        coverageDirection = transform.localPosition.normalized;
    }

    public float CalculateCoverageAmount(Transform victim, Transform predator)
    {
        Vector3 viewOnPredatorDirection = (victim.position - predator.position).normalized;
        _coverageAmount = Mathf.Pow(Vector3.Dot(coverageDirection, viewOnPredatorDirection), 3);
        return _coverageAmount;
    }

    public bool IsCoverTaken()
    {
        return _isCoverTaken;
    }

    public void TakeCover()
    {
        _isCoverTaken = true;
    }
    
    public float GetCoverageAmount()
    {
        return _coverageAmount;
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
