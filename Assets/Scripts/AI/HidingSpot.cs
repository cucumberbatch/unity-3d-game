using System;
using UnityEngine;

public class HidingSpot : MonoBehaviour
{
    public GameObject[] triggerZone;

    public Transform ChooseCover(Vector3 victim, Vector3 predator)
    {
        Transform cover = transform;
        // float maxProbability = float.MinValue;
        // float temp;
        //
        // foreach (GameObject zone in triggerZone)
        // {
        //     temp = zone.GetComponent<CoverPoint>().CalculateCoverageAmount(victim, predator);
        //     
        //     if (temp > maxProbability)
        //     {
        //         cover = zone.transform;
        //         maxProbability = temp;
        //     }
        // }
        //
        return cover;
    }   
    
}
