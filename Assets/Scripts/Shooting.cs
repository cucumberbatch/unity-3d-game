using BulletsPoolNamespace;
using UnityEngine;

public class Shooting : MonoBehaviour
{

	public Transform bulletTransform;
	public GameObject pointLight;
	public AudioClip fire;
	public GameObject Pistol;
	

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
	private float currentTime;
	private float anotherTime;
	private float indent = 0.1f;
	
	
	void Start() {
		currentTime += offset;
		
		//Create pool and fill it with bullets
		_usedBulletsPool = new BulletsPool(poolCapacity);
		_freeBulletsPool = new BulletsPool(poolCapacity);
		for (int i = 0; i < poolCapacity; i++)
		{
			Transform bullet = Instantiate(bulletTransform);
			//bullet.gameObject.scene.handle = poolHandle;
			bullet.gameObject.SetActive(false);
			_freeBulletsPool.putBullet(bullet);

		}
	}

	void Update() {
		if (Input.GetKeyDown(KeyCode.Mouse2)) {
			isAutomatic = !isAutomatic;
		}

		if (isAutomatic) {
			if (Input.GetKey(KeyCode.Mouse0) && currentTime >= delay) {
		        Transform bullet = _freeBulletsPool.getBullet();
		    	bullet.gameObject.SetActive(true);
		        
		        bullet.transform.position = transform.position;
		        bullet.transform.rotation = transform.rotation;
		        
		        bullet.gameObject.GetComponent<Rigidbody>().AddForce(transform.forward * speedAmm);
		        
		        _usedBulletsPool.putBullet(bullet);
		        
		        pointLight.GetComponent<Light>().enabled = true;
		    	GetComponent<AudioSource>().PlayOneShot(fire);
		        currentTime = 0;
			}
			else {
		    	pointLight.GetComponent<Light>().enabled = false;
			}
			

		} else {
			if (Input.GetKeyDown(KeyCode.Mouse0) && currentTime >= delay) {
				Pistol.GetComponent<Animator>().SetTrigger("Shoot");
				Transform bullet = _freeBulletsPool.getBullet();
		        bullet.gameObject.SetActive(true);
		        
		        bullet.transform.position = transform.position;
		        bullet.transform.rotation = transform.rotation;
		        bullet.GetComponent<Bullet>().shootedPerson = gameObject;
		        bullet.gameObject.GetComponent<Rigidbody>().AddForce(transform.forward * speedAmm);
		        
		        _usedBulletsPool.putBullet(bullet);
		    	
		        pointLight.GetComponent<Light>().enabled = true;
		    	GetComponent<AudioSource>().PlayOneShot(fire);
		        currentTime = 0;
			}
			else {
		    	pointLight.GetComponent<Light>().enabled = false;
			}
		}
		
		if (_usedBulletsPool.pool.Count != 0 && anotherTime >= delay * 5)
		{
			Transform temporary = _usedBulletsPool.getBullet();
			temporary.gameObject.GetComponent<Rigidbody>().ResetInertiaTensor();
			temporary.gameObject.SetActive(false);
			_freeBulletsPool.putBullet(temporary);
			anotherTime = 0;
		}
		currentTime += indent;
		anotherTime += indent;
	}

	
}
