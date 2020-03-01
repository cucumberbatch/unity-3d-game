using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bullet : MonoBehaviour
{
	public GameObject shootedPerson;
	public int amountOfDamage = 10;

	
    private void OnCollisionEnter(Collision other)
    {
	    if (other.gameObject.CompareTag("Player"))
	    {
		    Physics.IgnoreCollision(gameObject.GetComponent<Collider>(), other.gameObject.GetComponent<Collider>(), true);
	    }
    }
    
    private void OnCollisionStay(Collision other)
    {
	    if (other.gameObject.CompareTag("Player"))
        {
        	Physics.IgnoreCollision(gameObject.GetComponent<Collider>(), other.gameObject.GetComponent<Collider>(), true);
        }
    }
  
}

