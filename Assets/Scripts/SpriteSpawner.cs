using UnityEngine;

public class SpriteSpawner : MonoBehaviour
{
    public GameObject[] spritesPrefab;
    public float surfaceLayerOffset = 0.0001f;

    public void SpawnSpriteOnHit(RaycastHit hit)
    {
        SpawnProjector(spritesPrefab[Random.Range(0, spritesPrefab.Length)], hit);
    }

    public void SpawnOtherSpriteOnHit(GameObject sprite, RaycastHit hit)
    {
        InstantiateSprite(sprite, hit);
    }

    public void SpawnProjector(GameObject projector, RaycastHit hit)
    {
        // var offsetByRange = 1 / Mathf.Pow((hit.point - transform.position).magnitude, 0.5f);
        
        var projectorInstance = InstantiateProjector(projector, hit);
        
        // projectorInstance.GetComponent<Projector>().orthographicSize += offsetByRange;
    }

    private GameObject InstantiateSprite(GameObject sprite, RaycastHit hit)
    {
        var spriteInstance = Instantiate(
            sprite, 
            hit.point + hit.normal * surfaceLayerOffset, 
            Quaternion.FromToRotation(Vector3.up, hit.normal));
        
        return spriteInstance;
    }

    private GameObject InstantiateProjector(GameObject projector, RaycastHit hit)
    {
        var projectorInstance = Instantiate(
            projector, 
            hit.point + hit.normal * surfaceLayerOffset,
            Quaternion.FromToRotation(-Vector3.forward, hit.normal));

        projectorInstance.transform.parent = transform;

        return projectorInstance;
    }
    
}
