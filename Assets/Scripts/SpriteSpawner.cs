using UnityEngine;

public class SpriteSpawner : MonoBehaviour
{
    public GameObject[] spritesPrefab;
    public float surfaceLayerOffset = 0.01f;

    public void SpawnSpriteOnHit(RaycastHit hit)
    {
        InstantiateSprite(spritesPrefab[Random.Range(0, spritesPrefab.Length)], hit);
    }

    public void SpawnOtherSpriteOnHit(GameObject sprite, RaycastHit hit)
    {
        InstantiateSprite(sprite, hit);
    }

    public void SpawnProjector(GameObject projector, RaycastHit hit)
    {
        float offsetByRange = 1 / Mathf.Pow((hit.point - transform.position).magnitude, 0.5f);
        
        GameObject projectorInstance = InstantiateProjector(projector, hit);
        
        projectorInstance.GetComponent<Projector>().orthographicSize += offsetByRange;
    }

    private void InstantiateSprite(GameObject sprite, RaycastHit hit)
    {
        Instantiate(
            sprite, 
            hit.point + hit.normal * surfaceLayerOffset, 
            Quaternion.FromToRotation(Vector3.up, hit.normal));
    }

    private GameObject InstantiateProjector(GameObject projector, RaycastHit hit)
    {
        GameObject projectorInstance = Instantiate(
            projector,hit.point + hit.normal * 0.25f,
            Quaternion.FromToRotation(-Vector3.forward, hit.normal));

        projectorInstance.transform.parent = transform;

        return projectorInstance;
    }
    
}
