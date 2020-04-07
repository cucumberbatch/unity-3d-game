using System;
using UnityEngine;
    
public class RayGun : MonoBehaviour {
    
    public Camera fpsCamera;
    public int damage = 10;
    public float shootingRange = 100f;
    [Range(0.1f, 10f)] 
    public float delayBetweenShots = 1;
    public GameObject TraceBullet;

    private float currentTimeForShot;
    private float timeStep = 0.1f;
    private RaycastHit hit;

    void Update()
    {
        if (Physics.Raycast(fpsCamera.ScreenPointToRay(Input.mousePosition), out hit, shootingRange))
        {
            Boolean isMouseButtonPressed = Input.GetMouseButton(0);
            Boolean isReadyForShot = currentTimeForShot >= delayBetweenShots;

            currentTimeForShot += timeStep;

            if (!isMouseButtonPressed)
            {
                TraceBullet.SetActive(false);
                return;
            }
            
            if (isReadyForShot)
            {
                Shoot();
                TraceBullet.SetActive(true);
                currentTimeForShot = 0;
            }
        }
    }

    void Shoot()
    {
        Transform hittedObject = hit.transform;
        
        // Skip if hitted object is null
        if (!hittedObject) {return;}

        switch (hittedObject.gameObject.layer)
        {
            // Check for enemy collision
            case 10 : 
                Transform bodyPart = hittedObject;

                while (!hittedObject.CompareTag("Enemy"))
                {
                    hittedObject = hittedObject.parent;
                }

                hittedObject.GetComponent<CharacterHealth>().TakeDamage(damage);
                hittedObject.GetComponent<BloodSpriteSpawner>().SpawnSpriteOnHit(hit, fpsCamera.transform.forward, bodyPart);
                break;
            
            // Check for wall collision
            case 11 :
                hittedObject.gameObject.GetComponent<SpriteSpawner>().SpawnSpriteOnHit(hit);
                break;
        }
    }
}
