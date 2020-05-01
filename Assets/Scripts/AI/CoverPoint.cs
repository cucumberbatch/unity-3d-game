using UnityEngine;

public class CoverPoint : MonoBehaviour
{
    public Vector3 coverFromDirection;

    private void Start()
    {
        coverFromDirection = transform.localPosition.normalized;
    }

    public float CalculateCoverProbability(Vector3 victim, Vector3 predator)
    {
        Vector3 viewOnPredatorDirection = (victim - predator).normalized;
        
        return Mathf.Pow(Vector3.Dot(coverFromDirection, viewOnPredatorDirection), 3);
    }
}
