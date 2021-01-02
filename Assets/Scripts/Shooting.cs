using BulletsPoolNamespace;
using UnityEngine;

public class Shooting : MonoBehaviour
{

	public Transform bulletTransform;
	public GameObject poolDestination;
	public GameObject pointLight;
	public AudioClip fire;
	
	// public GameObject Pistol;
	// public GameObject poolHandle;
	
	public int speedAmm = 1000;
	public float offset = 0f;
	public bool isAutomatic = true;

	//Capacity of pool for bullets
	public int poolCapacity = 30;
	
	//Pool for bullets
	private BulletsPool _usedBulletsPool;
	private BulletsPool _freeBulletsPool;
	
	//Waiting between shots 
	private float delay = 0.5f;
	private float currentTime = 0;
	private float bulletLifeTime = 0;
	private float indent = 0.1f;


	public void Start() {
		currentTime += offset;
		
		//Create pool and fill it with bullets
		_usedBulletsPool = new BulletsPool(poolCapacity);
		_freeBulletsPool = new BulletsPool(poolCapacity);
		for (int i = 0; i < poolCapacity; i++)
		{
			Transform bullet = Instantiate(bulletTransform, poolDestination.transform, true);
			bullet.gameObject.SetActive(false);
			_freeBulletsPool.putBullet(bullet);

		}
	}

	private void Update() {
		if (Input.GetKeyDown(KeyCode.Mouse2)) {
			isAutomatic = !isAutomatic;
		}

		if (isAutomatic)
		{
			if ((currentTime >= delay) && (Input.GetKeyDown(KeyCode.Mouse0)))
			{
				// Pistol.GetComponent<Animator>().SetTrigger("Shoot");
				currentTime = 0;
				Shoot(_freeBulletsPool.getBullet());
			}
			else
			{
				// pointLight.GetComponent<Light>().enabled = false;
			}
		}

		if (_usedBulletsPool.pool.Count != 0 && bulletLifeTime >= delay * 5)
		{
			Transform temporary = _usedBulletsPool.getBullet();
			temporary.gameObject.GetComponent<Rigidbody>().ResetInertiaTensor();
			temporary.gameObject.SetActive(false);
			_freeBulletsPool.putBullet(temporary);
			bulletLifeTime = 0;
		}
		currentTime += indent;
		bulletLifeTime += indent;
	}

	public void Shoot(Transform bullet)
	{
		bullet.gameObject.SetActive(true);
		
		bullet.GetComponent<Bullet>().shootedPerson = gameObject;

		// 
		bullet.transform.position = transform.position;
		bullet.transform.rotation = transform.rotation;

		// придаем импульс пуле
		bullet.gameObject.GetComponent<Rigidbody>().AddForce(transform.forward * speedAmm);
		
		// перемещаем пулю в резерв для пуль которые используются в данный момент
		_usedBulletsPool.putBullet(bullet);
		
		// вспышка у дула ствола
		pointLight.GetComponent<Light>().enabled = true;
		
		
		GetComponent<AudioSource>().PlayOneShot(this.fire);

	}
	
}
