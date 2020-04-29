using UnityEngine;

public class BloodSpriteSpawner : MonoBehaviour
{
	public GameObject[] bloodPrefab;
	public float bloodRange = 8;
	
	public void SpawnSpriteOnHit(RaycastHit hit, Vector3 direction, Transform attachedModel)
	{
		var projectorRotation = Quaternion.FromToRotation(-Vector3.forward, hit.normal);
		
		var projectorInstance = Instantiate(
			bloodPrefab[Random.Range(0, bloodPrefab.Length)], 
			hit.point + hit.normal * 0.25f, 
			projectorRotation);

		projectorInstance.transform.parent = attachedModel;

		if (!Physics.Raycast(transform.position, direction, out hit, bloodRange)) return;
		
		var spritedObject = hit.transform;

		if (!spritedObject.CompareTag("Wall")) return;
		
		spritedObject.gameObject.GetComponent<SpriteSpawner>().
			SpawnProjector(bloodPrefab[Random.Range(0, bloodPrefab.Length)], hit);
	}

	
}
