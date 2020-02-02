using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Shooting : MonoBehaviour
{

	public Transform bulletPosition;
	public GameObject pointLight;
	public AudioClip fire;
	public int speedAmm = 1000;
	public float offset = 0f;
	public bool isAutomatic = true;

	private float delay = 0.5f;
	private float currentTime = 0f;
	private float indent = 0.1f;

	void Start() {
		currentTime += offset;
	}

	void Update() {
		if (Input.GetKeyDown(KeyCode.Mouse2)) {
			isAutomatic = !isAutomatic;
		}

		if (isAutomatic) {
			if (Input.GetKey(KeyCode.Mouse0) && currentTime >= delay){
		    	Transform g = (Transform)Instantiate(bulletPosition, transform.position, transform.rotation);
		    	g.GetComponent<Rigidbody>().AddForce(transform.forward * speedAmm);
		    	pointLight.GetComponent<Light>().enabled = true;
		    	currentTime = 0f;
		    	GetComponent<AudioSource>().PlayOneShot(fire);
			}
			else {
		    	pointLight.GetComponent<Light>().enabled = false;
			}
			

		} else {
			if (Input.GetKeyDown(KeyCode.Mouse0) && currentTime >= delay){
		    	Transform g = (Transform)Instantiate(bulletPosition, transform.position, transform.rotation);
		    	g.GetComponent<Rigidbody>().AddForce(transform.forward * speedAmm);
		    	pointLight.GetComponent<Light>().enabled = true;
		    	currentTime = 0f;
		    	GetComponent<AudioSource>().PlayOneShot(fire);
			}
			else {
		    	pointLight.GetComponent<Light>().enabled = false;
			}
			
		}
		currentTime += indent;
	}

}
