using UnityEngine;

public class RayGun : MonoBehaviour {

    public int damage = 10;
    public float range = 100f;

    public Camera fpsCam;

    // Update is called once per frame
    void Update () {
  
        if (Input.GetKey(KeyCode.Mouse0))
        {
            Shoot();
        }


    }
    void Shoot ()
    {
        RaycastHit hit;
        if (Physics.Raycast(fpsCam.transform.position, fpsCam.transform.forward, out hit, range))
        {
            Debug.Log(hit.transform.name);
            CharacterHealth characterhealth = hit.transform.GetComponent<CharacterHealth>();
            if (characterhealth != null)
            {
                characterhealth.TakeDamage(damage);
            }
        }
 
    }



}
