using System;
using UnityEngine;

public class RayGun : MonoBehaviour {
    
    public Camera fpsCam;
    public int damage = 10;
    public float shootingRange = 100f;
    [Range(0.1f, 10f)] 
    public float delayBetweenShots = 1;
    public GameObject TraceBullet;    // Particle for RayGun
    
    private float currentTimeForShot;
    private float timeStep = 0.1f;
    private RaycastHit hit;
    private CharacterHealth characterHealth;

    // Update is called once per frame
    void Update () {

        if (Input.GetMouseButton(0))
        {
            TraceBullet.gameObject.SetActive(true);
            
        }else{TraceBullet.gameObject.SetActive(false);
    }

               
        if (Input.GetKey(KeyCode.Mouse0) && currentTimeForShot >= delayBetweenShots)
        {
            Shoot();
            currentTimeForShot = 0;
        }
        
        currentTimeForShot += timeStep;
    }
    
    void Shoot ()
    {
        if (Physics.Raycast(fpsCam.transform.position, fpsCam.transform.forward, out hit, shootingRange))
        {
            characterHealth = hit.transform.GetComponent<CharacterHealth>();
            if (characterHealth != null)
            {
                characterHealth.TakeDamage(damage);
            }
            Debug.Log(hit.transform.name);
            
        }
    }
}
