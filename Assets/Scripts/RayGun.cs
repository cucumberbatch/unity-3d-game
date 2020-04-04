using System;
using UnityEngine;
using Random = UnityEngine.Random;
    
public class RayGun : MonoBehaviour {
    
    public Camera fpsCam;
    public int damage = 10;
    public float shootingRange = 100f;
    [Range(0.1f, 10f)] 
    public float delayBetweenShots = 1;
    public GameObject TraceBullet;
    public GameObject[] bulletHoleArray;// Particle for RayGun
    public GameObject projectorWall;
    public GameObject projectorEnemy;
    private GameObject[] _projectorsArray;
    public int maxProjectors = 50;

    private float currentTimeForShot;
    private float timeStep = 0.1f;
    private RaycastHit hit;
    private CharacterHealth characterHealth;
    private int tmpCount;
    private GameObject projector;

// Update is called once per frame
    void Start () 
    {
        _projectorsArray = new GameObject[maxProjectors];
    }
    
    void Update () {
        RaycastHit hit;
        Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
            if (Physics.Raycast(ray, out hit))
        {
            if(Input.GetMouseButtonDown(0))
           {
                Quaternion projectorRotation = Quaternion.FromToRotation(-Vector3.forward, hit.normal);

                switch (hit.transform.gameObject.layer)
                {
                    case 8: // номер слоя с плоскими объектами
                        projector = projectorWall;
                        break;
                    case 10: // номер слоя с моделями персонажей или рельефных объектов
                        projector = projectorEnemy;
                        break;
                }

                if(projector == null) return;
                GameObject obj = Instantiate(projector, hit.point + hit.normal * 0.25f, projectorRotation) as GameObject;

                obj.transform.parent = hit.transform;

                Quaternion randomRotZ = Quaternion.Euler(obj.transform.eulerAngles.x, obj.transform.eulerAngles.y, Random.Range(0, 360));
                obj.transform.rotation = randomRotZ;

                if(tmpCount == maxProjectors-1) tmpCount = 0; else tmpCount++;
            }
        }

        if (Input.GetMouseButton(0))  
        {
            TraceBullet.gameObject.SetActive(true); // включение трассировки пуль
            
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
                
            } else if (hit.transform.tag == "Wall")
            {
                Instantiate(bulletHoleArray[Random.Range(0, bulletHoleArray.Length)], hit.point-(hit.point - transform.position).normalized * (float) 0.01, Quaternion.FromToRotation(Vector3.up, hit.normal));
            }
             Debug.Log(hit.transform.name);
            
            
        }
    }
}
