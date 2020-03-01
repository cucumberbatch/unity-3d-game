using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bullet : MonoBehaviour
{

	public GameObject shootedPerson;
	// Start is called before the first frame update
    void Start()
    {
        
    }

    // private void OnCollisionEnter(Collider other) {
    // 	if (other.gameObject.CompareTag("Player"))
    //     {
    //     	Physics.IgnoreCollision(gameObject.GetComponent<Collider>(), other, true);
    //     }
    // }

    private void OnCollisionEnter(Collision other)
    {
	    if (other.gameObject.CompareTag("Player"))
	    {
		    Physics.IgnoreCollision(gameObject.GetComponent<Collider>(), other.gameObject.GetComponent<Collider>(), true);
	    }
    }

    // private void OnCollisionStay(Collider other) {
    // 	if (other.gameObject.CompareTag("Player"))
    //     {
    //     	Physics.IgnoreCollision(gameObject.GetComponent<Collider>(), other, true);
    //     }
    // }

    private void OnCollisionStay(Collision other)
    {
	    if (other.gameObject.CompareTag("Player"))
        {
        	Physics.IgnoreCollision(gameObject.GetComponent<Collider>(), other.gameObject.GetComponent<Collider>(), true);
        }
    }

    // Update is called once per frame
    void Update()
    {
	    

    }
	   
}

