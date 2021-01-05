using UnityEditor;
using UnityEngine;


public class HidingSpot : MonoBehaviour
{
    private Transform _hidingVictimTransform;
    private Vector3 _coverageDirection;
    private float _coverageAmount;
    private bool _isCoverTaken;

    
    
    //=========    EDITOR METHODS    ===========

    private void OnDrawGizmos()
    {
        Vector3 position = transform.position;
        Vector3 coverageDirection = transform.rotation * Vector3.forward / 2;
        
        // Draw hiding spot coverage amount and direction what it covers from
        Gizmos.color = Color.red;
        
        // Rotate global matrix for correct box drawing
        Gizmos.matrix = transform.localToWorldMatrix;
    
        // Gizmos.DrawCube(Vector3.zero - Vector3.forward / 2, Vector3.one * 0.20f);
        Gizmos.DrawWireCube(Vector3.zero - Vector3.forward / 2, Vector3.one * 0.20f);
        Gizmos.DrawRay(Vector3.back / 2, Vector3.forward);
       
        Handles.Label(position, _coverageAmount.ToString());
    }

    
    //=========    ENGINE METHODS    ===========

    private void Start()
    {
        _coverageDirection = transform.rotation * Vector3.forward;
    }

    
    
    //=========    OTHER METHODS    ===========

    public void CalculateCoverageAmount(Transform predator)
    {
        Vector3 viewOnPredatorDirection = (transform.position - predator.position).normalized;
        _coverageAmount = Mathf.Pow(Vector3.Dot(NormalizedCoverageDirection(), viewOnPredatorDirection), 3);
    }

    private Vector3 NormalizedCoverageDirection()
    {
        return _coverageDirection.normalized;
        // return new Vector3(normalizedDirection.x, 0, normalizedDirection.y);
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
}
