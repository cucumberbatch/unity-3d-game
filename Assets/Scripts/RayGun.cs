using UnityEngine;
    
public class RayGun : MonoBehaviour {
    
    public Camera fpsCamera;
    public int damage = 10;
    public float shootingRange = 100f;
    [Range(0.1f, 10f)] 
    public float delayBetweenShots = 0.5f;
    public GameObject TraceBullet;

    private float currentTimeForShot;
    private float timeStep = 0.1f;
    private RaycastHit hit;

    private void Update()
    {
        bool isMouseButtonPressed   = Input.GetMouseButton(0);
        bool isRaycastHitSomething  = Physics.Raycast(fpsCamera.ScreenPointToRay(Input.mousePosition), out hit, shootingRange);
        bool isReadyForShot         = currentTimeForShot >= delayBetweenShots;
        bool isParticleEmitted      = false;
        
        currentTimeForShot += timeStep;

        if (!isMouseButtonPressed) return;
     
        if (isReadyForShot)
        {
            currentTimeForShot = 0;
            isParticleEmitted = true;

            if (isRaycastHitSomething)
            {
                ApplyShootingStrategy();
            }
        }

        if (isParticleEmitted)
        {
            TraceBullet.GetComponent<ParticleSystem>().Emit(1);
        }
    }

    private void ApplyShootingStrategy()
    {
        Transform hittedObject = hit.transform;
        
        // Skip if hitted object is null
        if (!hittedObject) return;

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
