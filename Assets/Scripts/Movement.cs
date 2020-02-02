using UnityEngine;
using System.Collections;


public class Movement : MonoBehaviour {

	public GameObject player;
	public AudioClip step;

	public int speedRotation = 3;
	public int speed = 5;
	public int jumpSpeed = 350;

	//private float delay = 3.5f;
	//private float currentTime = 0f;
	//private float indent = 0.1f;


	void Start () { 
		player = (GameObject)this.gameObject; 
	}

	void Update() {
		if (Input.GetKey(KeyCode.W) || Input.GetKey(KeyCode.UpArrow)) { 
			player.transform.position += player.transform.forward * speed * Time.deltaTime;
		} 

		if (Input.GetKey(KeyCode.S) || Input.GetKey(KeyCode.DownArrow)) { 
			player.transform.position -= player.transform.forward * speed * Time.deltaTime; 
		} 

		if (Input.GetKey(KeyCode.A) || Input.GetKey(KeyCode.LeftArrow)) { 
			player.transform.position -= player.transform.right * speed * Time.deltaTime; 
		} 

		if (Input.GetKey(KeyCode.D) || Input.GetKey(KeyCode.RightArrow)) { 
			player.transform.position += player.transform.right * speed * Time.deltaTime; 
		} 


		if (Input.GetKeyDown(KeyCode.Space)) {
			player.GetComponent<Rigidbody>().AddForce(player.transform.up * jumpSpeed * Time.deltaTime, ForceMode.Impulse);
			GetComponent<AudioSource>().PlayOneShot(step);
			//player.transform.position += player.transform.up * jumpSpeed * Time.deltaTime; 
		} 
	}

}